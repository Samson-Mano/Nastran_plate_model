using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nastran_plate_model
{
    public class mesh_data_store
    {

        public struct Nodes_store
        {
            public int node_id { get; private set; }
            double node_x;
            double node_y;
            double node_z;

            public Nodes_store(int t_node_id, double t_node_x, double t_node_y)
            {
                node_id = t_node_id;
                node_x = t_node_x;
                node_y = t_node_y;
                node_z = 0.0;
            }

            public string return_nastran_format()
            {
                string str = string.Format("GRID,{0},{1},{2},{3},{4}",
                    node_id, "0", node_x.ToString("F1"), node_y.ToString("F1"), node_z.ToString("F1")) + Environment.NewLine;

                return str;
            }

        }

        public struct Quad_store
        {
            int quad_id;
            int node_1;
            int node_2;
            int node_3;
            int node_4;

            public Quad_store(int t_quad_id, int t_node_1, int t_node_2, int t_node_3, int t_node_4)
            {
                // 2___3
                // |   |
                // 1___4
                quad_id = t_quad_id;
                node_1 = t_node_1;
                node_2 = t_node_2;
                node_3 = t_node_3;
                node_4 = t_node_4;
            }

            public string return_nastran_format()
            {
                string str = string.Format("CQUAD4,{0},{1},{2},{3},{4},{5}",
                    quad_id, 1, node_1, node_2, node_3, node_4) + Environment.NewLine;
                return str;
            }
        }

        public struct Beam_store
        {
            int beam_id;
            int node_1;
            int node_2;
            double beam_offset_z;

            public Beam_store(int t_beam_id, int t_node_1, int t_node_2, double t_beam_offset_z)
            {
                // 1___2
                beam_id = t_beam_id;
                node_1 = t_node_1;
                node_2 = t_node_2;
                beam_offset_z = t_beam_offset_z;
            }

            public string return_nastran_format()
            {

               string str = string.Format("CBEAM,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                    beam_id, 2, node_1, node_2, 0.0, 0.0,1.0,"","","", 0.0, 0.0, beam_offset_z, 0.0, 0.0, beam_offset_z) + Environment.NewLine;
                return str;
            }
        }

        public bool is_mesh_created { get; private set; }

        private List<Nodes_store> cnodes;
        private List<Quad_store> cquad;
        private List<Beam_store> cbeam;
        private List<int> quad_ids;
        private List<int> side_1_nds;
        private List<int> side_2_nds;
        private List<int> side_3_nds;
        private List<int> side_4_nds;
        private List<int> side_bcs;
        private string other_outputs = "";

        public mesh_data_store()
        {
            // Emptry constructor
            this.is_mesh_created = false;
            cnodes = new List<Nodes_store>();
            cquad = new List<Quad_store>();
            cbeam = new List<Beam_store>();
            quad_ids = new List<int>();

            // List of boundary nodes
            side_1_nds = new List<int>();
            side_2_nds = new List<int>();
            side_3_nds = new List<int>();
            side_4_nds = new List<int>();

            // Boundary conditions
            side_bcs = new List<int>();
        }

        public void create_mesh(int length_a, int breadth_b, int stiff_spacing, int mesh_size, double t_beam_offset_z, List<int> bc_values)
        {
            this.is_mesh_created = false;
            other_outputs = "";

            // Find the ratio between stiffener spacing and mesh size
            int stiff_ratio = integer_ratio(stiff_spacing, mesh_size);
            double spacing_ratio = (double)stiff_spacing / (double)stiff_ratio;
            double a_mesh_size = (double)spacing_ratio;

            int row_count = (int)Math.Floor(((double)breadth_b / (double)spacing_ratio)) + 1;
            int column_count = (int)Math.Floor(((double)length_a / (double)spacing_ratio)) + 1;

            Nodes_store[,] nodes = new Nodes_store[row_count, column_count];

            // Create the Nodes
            int nd_id = 1;
            for (int i = 0; i < row_count; i++)
            {
                for (int j = 0; j < column_count; j++)
                {
                    double x_val = j * a_mesh_size;
                    double y_val = i * a_mesh_size;

                    if (j == (column_count - 1))
                    {
                        // End of Length
                        x_val = length_a;
                    }

                    if (i == (row_count - 1))
                    {
                        // End of Breadth
                        y_val = breadth_b;
                    }

                    nodes[i, j] = new Nodes_store(nd_id, x_val, y_val);
                    nd_id++;
                }
            }

            // Find the nodes of Borders
            side_bcs = new List<int>(bc_values);
            // Side 1
            side_1_nds = new List<int>();
            for (int i = 0; i < column_count; i++)
            {
                side_1_nds.Add(nodes[0, i].node_id);
            }

            // Side 2
            side_2_nds = new List<int>();
            for (int i = 0; i < row_count; i++)
            {
                side_2_nds.Add(nodes[i, 0].node_id);
            }

            // Side 3
            side_3_nds = new List<int>();
            for (int i = 0; i < column_count; i++)
            {
                side_3_nds.Add(nodes[row_count - 1, i].node_id);
            }

            // Side 4
            side_4_nds = new List<int>();
            for (int i = 0; i < row_count; i++)
            {
                side_4_nds.Add(nodes[i, column_count - 1].node_id);
            }


            Quad_store[,] quads = new Quad_store[row_count - 1, column_count - 1];
            quad_ids = new List<int>();

            // Create the Quad Elements
            int el_id = 1;
            for (int i = 0; i < row_count - 1; i++)
            {
                for (int j = 0; j < column_count - 1; j++)
                {
                    quads[i, j] = new Quad_store(el_id, nodes[i, j].node_id, nodes[i+1, j].node_id, nodes[i + 1, j + 1].node_id, nodes[i, j+1].node_id);
                    quad_ids.Add(el_id);
                    el_id++;
                }
            }



            int beam_row_count = (int)Math.Ceiling((double)breadth_b / (double)stiff_spacing) - 1;

            Beam_store[,] beams = new Beam_store[beam_row_count, column_count - 1];

            // Create the Beam Elements
            for (int i = 0; i < beam_row_count; i++)
            {
                for (int j = 0; j < column_count - 1; j++)
                {
                    beams[i, j] = new Beam_store(el_id, nodes[(i + 1) * stiff_ratio, j].node_id, nodes[(i + 1) * stiff_ratio, j + 1].node_id, -1.0 * t_beam_offset_z);
                    el_id++;
                }
            }

            // Add the values to List
            cnodes = new List<Nodes_store>();
            // Iterate through the array and add each element to the list
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    cnodes.Add(nodes[i, j]);
                }
            }

            cquad = new List<Quad_store>();
            // Iterate through the array and add each element to the list
            for (int i = 0; i < quads.GetLength(0); i++)
            {
                for (int j = 0; j < quads.GetLength(1); j++)
                {
                    cquad.Add(quads[i, j]);
                }
            }


            cbeam = new List<Beam_store>();
            // Iterate through the array and add each element to the list
            for (int i = 0; i < beams.GetLength(0); i++)
            {
                for (int j = 0; j < beams.GetLength(1); j++)
                {
                    cbeam.Add(beams[i, j]);
                }
            }


            // Mesh created succesfully
            this.is_mesh_created = true;
        }

        public void set_other_input_str(string thickness_data, string stiffener_data, string material_data)
        {
            other_outputs = "";
            other_outputs = thickness_data + Environment.NewLine +
                stiffener_data + Environment.NewLine +
                material_data + Environment.NewLine;
        }

        public string get_Nastran_mesh()
        {
            if (this.is_mesh_created == false)
                return null;

            string str_bc = get_Nastran_Boundary_conditions();

            string str_nodes = "";
            foreach (Nodes_store nd in cnodes)
            {
                str_nodes = str_nodes + nd.return_nastran_format();
            }

            string str_quad = "";
            foreach (Quad_store qd in cquad)
            {
                str_quad = str_quad + qd.return_nastran_format();
            }

            string str_beam = "";
            foreach (Beam_store bm in cbeam)
            {
                str_beam = str_beam + bm.return_nastran_format();
            }

            return str_bc + str_nodes + other_outputs + str_quad + str_beam;
            // str_bc + other_outputs + str_nodes + str_quad+ str_beam;
            // return str_nodes;

        }

        private string get_Nastran_Boundary_conditions()
        {
            // Side1
            string side1_bc_str = "";
            if (side_bcs[0] == 1)
            {
                // Fixed side
                for (int i = 0; i < side_1_nds.Count - 1; i++)
                {
                    side1_bc_str = side1_bc_str + get_node_bndry_condition_str(side_bcs[0], side_1_nds[i]);
                }
            }
            else
            {
                // Pinned or Free side
                if (side_bcs[1] == 0 || side_bcs[1] == 1)
                {
                    // First node is Pinned or Fixed (Comming from Side 2)
                    side1_bc_str = side1_bc_str + get_node_bndry_condition_str(side_bcs[1], side_1_nds[0]);
                }
                else if (side_bcs[0] == 0)
                {
                    //Pinned Side Use the Boundary condition of side 1
                    side1_bc_str = side1_bc_str + get_node_bndry_condition_str(side_bcs[0], side_1_nds[0]);
                }

                for (int i = 1; i < (side_1_nds.Count - 1); i++)
                {
                    side1_bc_str = side1_bc_str + get_node_bndry_condition_str(side_bcs[0], side_1_nds[i]);
                }
            }


            // Side2
            string side2_bc_str = "";
            if (side_bcs[1] == 1)
            {
                // Fixed side
                for (int i = 1; i < side_2_nds.Count; i++)
                {
                    side2_bc_str = side2_bc_str + get_node_bndry_condition_str(side_bcs[1], side_2_nds[i]);
                }
            }
            else
            {
                for (int i = 1; i < (side_2_nds.Count - 1); i++)
                {
                    side2_bc_str = side2_bc_str + get_node_bndry_condition_str(side_bcs[1], side_2_nds[i]);
                }

                // Pinned or Free  side
                if (side_bcs[2] == 0 || side_bcs[2] == 1)
                {
                    // First node is Pinned or Fixed (Comming from Side 2)
                    side2_bc_str = side2_bc_str + get_node_bndry_condition_str(side_bcs[2], side_2_nds[side_2_nds.Count - 1]);
                }
                else if (side_bcs[1] == 0)
                {
                    //Pinned Side Use the Boundary condition of side 1
                    side2_bc_str = side2_bc_str + get_node_bndry_condition_str(side_bcs[1], side_2_nds[side_2_nds.Count - 1]);
                }

            }


            // Side3
            string side3_bc_str = "";
            if (side_bcs[2] == 1)
            {
                // Fixed side
                for (int i = 1; i < side_3_nds.Count; i++)
                {
                    side3_bc_str = side3_bc_str + get_node_bndry_condition_str(side_bcs[2], side_3_nds[i]);
                }
            }
            else
            {
                for (int i = 1; i < (side_3_nds.Count - 1); i++)
                {
                    side3_bc_str = side3_bc_str + get_node_bndry_condition_str(side_bcs[2], side_3_nds[i]);
                }

                // Pinned or Free  side
                if (side_bcs[3] == 0 || side_bcs[3] == 1)
                {
                    // First node is Pinned or Fixed (Comming from Side 2)
                    side3_bc_str = side3_bc_str + get_node_bndry_condition_str(side_bcs[3], side_3_nds[side_3_nds.Count - 1]);
                }
                else if (side_bcs[2] == 0)
                {
                    //Pinned Side Use the Boundary condition of side 1
                    side3_bc_str = side3_bc_str + get_node_bndry_condition_str(side_bcs[2], side_3_nds[side_3_nds.Count - 1]);
                }

            }


            // Side 4
            string side4_bc_str = "";
            if (side_bcs[3] == 1)
            {
                // Fixed side
                for (int i = 0; i < side_4_nds.Count - 1; i++)
                {
                    side4_bc_str = side4_bc_str + get_node_bndry_condition_str(side_bcs[3], side_4_nds[i]);
                }
            }
            else
            {
                // Pinned or Free  side
                if (side_bcs[0] == 0 || side_bcs[0] == 1)
                {
                    // First node is Pinned or Fixed (Comming from Side 2)
                    side4_bc_str = side4_bc_str + get_node_bndry_condition_str(side_bcs[0], side_4_nds[0]);
                }
                else if (side_bcs[3] == 0)
                {
                    //Pinned Side Use the Boundary condition of side 1
                    side4_bc_str = side4_bc_str + get_node_bndry_condition_str(side_bcs[3], side_4_nds[0]);
                }

                for (int i = 1; i < (side_4_nds.Count - 1); i++)
                {
                    side4_bc_str = side4_bc_str + get_node_bndry_condition_str(side_bcs[3], side_4_nds[i]);
                }
            }

            return side1_bc_str + side2_bc_str + side3_bc_str + side4_bc_str;
        }


        private string get_node_bndry_condition_str(int type, int id)
        {
            if (type == 0)
            {
                // Pinned
                return "SPC1,1,123," + id +","+ Environment.NewLine;
            }
            else if (type == 1)
            {
                // Fixed
                return "SPC1,1,123456," + id +","+ Environment.NewLine;
            }

            return "";
        }

        private int integer_ratio(int a, int b)
        {
            // Find the ratio of stiff_spacing to mesh_size
            double ratio = (double)a / (double)b;

            // Find the ceiling of the ratio value
            return (int)Math.Ceiling(ratio);
        }

    }
}
