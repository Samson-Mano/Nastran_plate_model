using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

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

            public string return_nastran_dat_format()
            {
                string str = string.Format("GRID,{0},{1},{2},{3},{4}",
                    node_id, "0", node_x.ToString("F1"), node_y.ToString("F1"), node_z.ToString("F1")) + Environment.NewLine;

                return str;
            }


            public string return_nastran_bdf_format()
            {
                string line = string.Format("{0,-8}{1,8}{2,8}{3,8}{4,8}{5,8}",
                "GRID", node_id, 0,
                FormatRealForNastranBDF(node_x),
                FormatRealForNastranBDF(node_y),
                FormatRealForNastranBDF(node_z));

                return line + Environment.NewLine;
            }


            private string FormatRealForNastranBDF(double value)
            {
                // Try fixed-point first, fallback to scientific if needed
                string fixedFormat = value.ToString("0.######");
                if (fixedFormat.Length <= 8)
                    return fixedFormat.PadLeft(8);

                string sciFormat = value.ToString("0.0####E+00");
                return sciFormat.Length <= 8 ? sciFormat.PadLeft(8) : sciFormat.Substring(0, 8);
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

            public string return_nastran_dat_format()
            {
                string str = string.Format("CQUAD4,{0},{1},{2},{3},{4},{5}",
                    quad_id, 1, node_1, node_2, node_3, node_4);
                return str + Environment.NewLine;
            }


            public string return_nastran_bdf_format()
            {
                string str = string.Format("{0,-8}{1,8}{2,8}{3,8}{4,8}{5,8}{6,8}",
                 "CQUAD4", quad_id, 1, node_1, node_2, node_3, node_4);

                return str + Environment.NewLine;

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

            public string return_nastran_dat_format()
            {

                string str = string.Format("CBEAM,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                     beam_id, 2, node_1, node_2, 0.0, 0.0, 1.0, "", "", "", 0.0, 0.0, beam_offset_z, 0.0, 0.0, beam_offset_z);
                return str+ Environment.NewLine;
            }

            public string return_nastran_bdf_format()
            {

                string line = string.Format("{0,-8}{1,8}{2,8}{3,8}{4,8}{5,8}{6,8}{7,8}",
                 "CBEAM", beam_id, 2, node_1, node_2,
                 FormatRealForNastranBDF(0.0),
                 FormatRealForNastranBDF(0.0),
                 FormatRealForNastranBDF(1.0));

                return line + Environment.NewLine;
            }


            private string FormatRealForNastranBDF(double value)
            {
                // Format to fit within 8 characters, using scientific notation if needed
                string fixedFormat = value.ToString("0.######");
                if (fixedFormat.Length <= 8)
                    return fixedFormat.PadLeft(8);

                string sciFormat = value.ToString("0.0####E+00");
                return sciFormat.Length <= 8 ? sciFormat.PadLeft(8) : sciFormat.Substring(0, 8);
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
        private string other_dat_outputs = "";
        private string other_bdf_outputs = "";
        private string other_fem_outputs = "";

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
            other_dat_outputs = "";
            other_bdf_outputs = "";
            other_fem_outputs = "";

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
                    quads[i, j] = new Quad_store(el_id, nodes[i, j].node_id, nodes[i + 1, j].node_id, nodes[i + 1, j + 1].node_id, nodes[i, j + 1].node_id);
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

        public void set_other_input_str(string thickness_dat_data, string stiffener_dat_data, string material_dat_data,
            string thickness_bdf_data, string stiffener_bdf_data, string material_bdf_data)
        {
            other_dat_outputs = "";
            other_dat_outputs = thickness_dat_data + Environment.NewLine +
                stiffener_dat_data + Environment.NewLine +
                material_dat_data + Environment.NewLine;

            other_bdf_outputs = "";
            other_bdf_outputs = thickness_bdf_data +
            stiffener_bdf_data +
            material_bdf_data;


            other_fem_outputs = "";

        }

        public string get_Nastran_dat_mesh()
        {
            if (this.is_mesh_created == false)
                return null;

            // string str_heading = get_Nastran_heading();

            string str_dat_bc = get_Nastran_Boundary_conditions(0);

            string str_dat_nodes = "";
            foreach (Nodes_store nd in cnodes)
            {
                str_dat_nodes = str_dat_nodes + nd.return_nastran_dat_format();
            }

            string str_dat_quad = "";
            foreach (Quad_store qd in cquad)
            {
                str_dat_quad = str_dat_quad + qd.return_nastran_dat_format();
            }

            string str_dat_beam = "";
            foreach (Beam_store bm in cbeam)
            {
                str_dat_beam = str_dat_beam + bm.return_nastran_dat_format();
            }

            return str_dat_bc + str_dat_nodes + other_dat_outputs + str_dat_quad + str_dat_beam;

        }


        public string get_Nastran_bdf_mesh()
        {
            if (this.is_mesh_created == false)
                return null;

            string str_bdf_heading = get_Nastran_bdf_heading();

            string str_bdf_bc = get_Nastran_Boundary_conditions(1);

            string str_bdf_nodes = "";
            foreach (Nodes_store nd in cnodes)
            {
                str_bdf_nodes = str_bdf_nodes + nd.return_nastran_bdf_format();
            }

            string str_bdf_quad = "";
            foreach (Quad_store qd in cquad)
            {
                str_bdf_quad = str_bdf_quad + qd.return_nastran_bdf_format();
            }

            string str_bdf_beam = "";
            foreach (Beam_store bm in cbeam)
            {
                str_bdf_beam = str_bdf_beam + bm.return_nastran_bdf_format();
            }

            return str_bdf_heading + str_bdf_bc + other_bdf_outputs + str_bdf_nodes + str_bdf_quad + str_bdf_beam + "ENDDATA d1cb3ea0";


        }


        private string get_Nastran_bdf_heading()
        {

            string currentDateTime = DateTime.Now.ToString("ddd MMM dd HH:mm:ss yyyy");


            string header = $@"INIT MASTER(S)
NASTRAN SYSTEM(442)= -1,SYSTEM(319) = 1
ID Femap, Femap
SOL SEMODES
TIME 10000
CEND
  TITLE = NX Nastran Modes Analysis Set
  ECHO = NONE
  DISPLACEMENT(PLOT) = ALL
  SPCFORCE(PLOT) = ALL
  ESE(PLOT) = ALL
  METHOD = 1
  SPC = 1
BEGIN BULK
$ ***************************************************************************
$   Written by : Nastran plate model
$   Translator: NX Nastran
$   Date: {currentDateTime}
$ ***************************************************************************
$
PARAM,POST,-1
PARAM,OGEOM,NO
PARAM, AUTOSPC, YES
PARAM,GRDPNT,0
EIGRL          1                      10       0                    MASS
CORD2C         1       0      0.      0.      0.      0.      0.      1.+ FEMAPC1
+ FEMAPC1      1.      0.      1.
CORD2S         2       0      0.      0.      0.      0.      0.      1.+ FEMAPC2
+ FEMAPC2      1.      0.      1.
$ Femap with NX Nastran Constraint Set 1 : NASTRAN SPC 1";

            return header + Environment.NewLine;

        }


        private string get_Nastran_Boundary_conditions(int mesh_type)
        {
            // mesh_type = 0 (DAT)
            // mesh_type = 1 (BDF)
            // mesh_type = 2 (FEM)


            // Side1
            string side1_bc_str = "";
            if (side_bcs[0] == 1)
            {
                // Fixed side
                for (int i = 0; i < side_1_nds.Count - 1; i++)
                {
                    side1_bc_str = side1_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[0], side_1_nds[i]);
                }
            }
            else
            {
                // Pinned or Free side
                if (side_bcs[1] == 0 || side_bcs[1] == 1)
                {
                    // First node is Pinned or Fixed (Comming from Side 2)
                    side1_bc_str = side1_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[1], side_1_nds[0]);
                }
                else if (side_bcs[0] == 0)
                {
                    //Pinned Side Use the Boundary condition of side 1
                    side1_bc_str = side1_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[0], side_1_nds[0]);
                }

                for (int i = 1; i < (side_1_nds.Count - 1); i++)
                {
                    side1_bc_str = side1_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[0], side_1_nds[i]);
                }
            }


            // Side2
            string side2_bc_str = "";
            if (side_bcs[1] == 1)
            {
                // Fixed side
                for (int i = 1; i < side_2_nds.Count; i++)
                {
                    side2_bc_str = side2_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[1], side_2_nds[i]);
                }
            }
            else
            {
                for (int i = 1; i < (side_2_nds.Count - 1); i++)
                {
                    side2_bc_str = side2_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[1], side_2_nds[i]);
                }

                // Pinned or Free  side
                if (side_bcs[2] == 0 || side_bcs[2] == 1)
                {
                    // First node is Pinned or Fixed (Comming from Side 2)
                    side2_bc_str = side2_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[2], side_2_nds[side_2_nds.Count - 1]);
                }
                else if (side_bcs[1] == 0)
                {
                    //Pinned Side Use the Boundary condition of side 1
                    side2_bc_str = side2_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[1], side_2_nds[side_2_nds.Count - 1]);
                }

            }


            // Side3
            string side3_bc_str = "";
            if (side_bcs[2] == 1)
            {
                // Fixed side
                for (int i = 1; i < side_3_nds.Count; i++)
                {
                    side3_bc_str = side3_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[2], side_3_nds[i]);
                }
            }
            else
            {
                for (int i = 1; i < (side_3_nds.Count - 1); i++)
                {
                    side3_bc_str = side3_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[2], side_3_nds[i]);
                }

                // Pinned or Free  side
                if (side_bcs[3] == 0 || side_bcs[3] == 1)
                {
                    // First node is Pinned or Fixed (Comming from Side 2)
                    side3_bc_str = side3_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[3], side_3_nds[side_3_nds.Count - 1]);
                }
                else if (side_bcs[2] == 0)
                {
                    //Pinned Side Use the Boundary condition of side 1
                    side3_bc_str = side3_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[2], side_3_nds[side_3_nds.Count - 1]);
                }

            }


            // Side 4
            string side4_bc_str = "";
            if (side_bcs[3] == 1)
            {
                // Fixed side
                for (int i = 0; i < side_4_nds.Count - 1; i++)
                {
                    side4_bc_str = side4_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[3], side_4_nds[i]);
                }
            }
            else
            {
                // Pinned or Free  side
                if (side_bcs[0] == 0 || side_bcs[0] == 1)
                {
                    // First node is Pinned or Fixed (Comming from Side 2)
                    side4_bc_str = side4_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[0], side_4_nds[0]);
                }
                else if (side_bcs[3] == 0)
                {
                    //Pinned Side Use the Boundary condition of side 1
                    side4_bc_str = side4_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[3], side_4_nds[0]);
                }

                for (int i = 1; i < (side_4_nds.Count - 1); i++)
                {
                    side4_bc_str = side4_bc_str + get_node_bndry_condition_str(mesh_type, side_bcs[3], side_4_nds[i]);
                }
            }

            return side1_bc_str + side2_bc_str + side3_bc_str + side4_bc_str;
        }


        private string get_node_bndry_condition_str(int mesh_type, int type, int id)
        {
            string line = "";

            if(mesh_type == 0)
            {
                // dat file type
                if (type == 0)
                {
                    // Pinned
                    line = "SPC1,1,123," + id + ",";
                }
                else if (type == 1)
                {
                    // Fixed
                    line = "SPC1,1,123456," + id + ",";
                }

            }
            else if (mesh_type == 1)
            {
                // BDF file type
                string dof = "";

                if (type == 0)
                {
                    // Pinned: constrain DOF 1, 2, 3
                    dof = "123";
                }
                else if (type == 1)
                {
                    // Fixed: constrain DOF 1 through 6
                    dof = "123456";
                }
                else
                {
                    return "";
                }

                // Format: 8-character fields, right-aligned
                line = string.Format("{0,-8}{1,8}{2,8}{3,8}", "SPC1", 1, dof, id);
            }
            else if(mesh_type == 2)
            {
                // FEM file type



            }


                return line + Environment.NewLine;

        }

        private int integer_ratio(int a, int b)
        {
            // Find the ratio of stiff_spacing to mesh_size
            double ratio = (double)a / (double)b;

            // Find the ceiling of the ratio value
            return (int)Math.Ceiling(ratio);
        }


        public string get_Optistruct_mesh()
        {
            if (this.is_mesh_created == false)
                return null;



            return "";

        }







    }
}
