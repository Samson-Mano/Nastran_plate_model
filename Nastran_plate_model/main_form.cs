using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nastran_plate_model
{
    public partial class main_form : Form
    {
        mesh_data_store mesh_data = new mesh_data_store();

        public main_form()
        {
            InitializeComponent();
        }

        private void main_form_Load(object sender, EventArgs e)
        {
            // Load the combobox and default value
            // Call the GetMaterialNames() function to get the list of material names
            List<string> materialNames = global_static.GetMaterialNames();
            comboBox_material.Items.Clear();
            comboBox_material.Items.AddRange(materialNames.ToArray());
            comboBox_material.SelectedIndex = 0;

            // Call the GetBoundaryConditions() function to get the list of Boundary conditions
            List<string> bconditions = global_static.GetBoundaryConditions();
            comboBox_side1_bc.Items.Clear();
            comboBox_side1_bc.Items.AddRange(bconditions.ToArray());
            comboBox_side1_bc.SelectedIndex = 0;
            comboBox_side2_bc.Items.Clear();
            comboBox_side2_bc.Items.AddRange(bconditions.ToArray());
            comboBox_side2_bc.SelectedIndex = 0;
            comboBox_side3_bc.Items.Clear();
            comboBox_side3_bc.Items.AddRange(bconditions.ToArray());
            comboBox_side3_bc.SelectedIndex = 0;
            comboBox_side4_bc.Items.Clear();
            comboBox_side4_bc.Items.AddRange(bconditions.ToArray());
            comboBox_side4_bc.SelectedIndex = 0;

            // Call the GetAddedMassConditions() function to get the list of Added mass type
            List<string> gaddedmass = global_static.GetAddedMassConditions();
            comboBox_addedmass.Items.Clear();
            comboBox_addedmass.Items.AddRange(gaddedmass.ToArray());
            comboBox_addedmass.SelectedIndex = 0;

            // Call the GetStiffNames(get_type) function to get the list of Stiffener names
            List<string> Lstiff = global_static.GetStiffNames(0);
            List<string> Hstiff = global_static.GetStiffNames(1);
            List<string> Tstiff = global_static.GetStiffNames(2);
            comboBox_stiffener.Items.Clear();
            comboBox_stiffener.Items.AddRange(Lstiff.ToArray());
            comboBox_stiffener.Items.AddRange(Hstiff.ToArray());
            comboBox_stiffener.Items.AddRange(Tstiff.ToArray());
            comboBox_stiffener.SelectedIndex = 0;


        }

        private void button_create_Click(object sender, EventArgs e)
        {
            // Check whether the text inputs are valid
            if (IsValidInput() == false)
            {
                // Inputs not valid
                MessageBox.Show("Input not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create the mesh
            int length_a, breadth_b, stiff_spacing, mesh_size;
            double non_structural_mass, thickness;
            length_a = Convert.ToInt32(textBox_length.Text);
            breadth_b = Convert.ToInt32(textBox_breadth.Text);
            stiff_spacing = Convert.ToInt32(textBox_stiffspacing.Text);
            mesh_size = Convert.ToInt32(textBox_meshsize.Text);

            // Thickness properties
            thickness = Convert.ToDouble(textBox_thickness.Text);
            non_structural_mass = Convert.ToDouble(textBox_nmass.Text);
            non_structural_mass = non_structural_mass / (double)(1000000 * 1000);
            string thickness_output = global_static.Thickness_ConvertToNastranFormat(thickness, non_structural_mass);
            // global_static.Show_error_Dialog("Thickness", thickness_output);

            // Stiffener properties
            double beam_offset = 0.0d;
            string stiffener_output = global_static.Stiffner_ConvertToNastranFormat(comboBox_stiffener.SelectedItem.ToString(), ref beam_offset);
            // global_static.Show_error_Dialog("Stiffener", stiffener_output);

            // Material properties
            string material_output = global_static.Material_ConvertToNastranFormat(comboBox_material.SelectedItem.ToString());
            // global_static.Show_error_Dialog("Material", material_output);

            // Boundary conditions
            List<int> bc_values = new List<int>();
            bc_values.Add(comboBox_side1_bc.SelectedIndex);
            bc_values.Add(comboBox_side2_bc.SelectedIndex);
            bc_values.Add(comboBox_side3_bc.SelectedIndex);
            bc_values.Add(comboBox_side4_bc.SelectedIndex);

            // Adjust the Offset value depending on the stiffener type
            if(comboBox_stiffener.SelectedItem.ToString().StartsWith("L") == true)
            {
                // L Stiffener (do nothing)
            }
            else if(comboBox_stiffener.SelectedItem.ToString().StartsWith("T") == true)
            {
                // T stiffener
                beam_offset = -1*beam_offset;
            }
            else if (comboBox_stiffener.SelectedItem.ToString().StartsWith("F") == true)
            {
                // Flat bar
                beam_offset = beam_offset * 0.5d;
            }

                mesh_data.create_mesh(length_a, breadth_b, stiff_spacing, mesh_size,beam_offset,bc_values);
            mesh_data.set_other_input_str(thickness_output,stiffener_output,material_output);

            // MessageBox.Show(comboBox_side1_bc.SelectedIndex.ToString());
            MessageBox.Show("Mesh Creation Complete !","Nastran Mesh",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }



        // Function to check if a string is a valid positive integer
        private bool IsPositiveInteger(string input)
        {
            if (int.TryParse(input, out int result))
            {
                if (result > 0)
                {
                    return true;
                }
            }
            return false;
        }

        // Function to check if a string is a valid positive decimal value
        private bool IsPositiveNumber(string input)
        {
            if (decimal.TryParse(input, out decimal result))
            {
                if (result >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        // Check values in tbox_a, tbox_b, tbox_s, and tbox_m
        private bool IsValidInput()
        {
            int a, b, s, m;

            if (IsPositiveInteger(textBox_length.Text) && IsPositiveInteger(textBox_breadth.Text) &&
                IsPositiveInteger(textBox_stiffspacing.Text) && IsPositiveInteger(textBox_meshsize.Text) &&
                IsPositiveNumber(textBox_nmass.Text) && IsPositiveNumber(textBox_thickness.Text))
            {
                a = Convert.ToInt32(textBox_length.Text);
                b = Convert.ToInt32(textBox_breadth.Text);
                s = Convert.ToInt32(textBox_stiffspacing.Text);
                m = Convert.ToInt32(textBox_meshsize.Text);

                if (s < a && s < b && m < s)
                {
                    return true;
                }
            }

            return false;
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            if (mesh_data.is_mesh_created == false)
                return;

            // global_static.Show_error_Dialog("BDF Data", mesh_data.get_Nastran_mesh());

            // Your string to save as .dat file
            string content = mesh_data.get_Nastran_mesh();

            // Create a SaveFileDialog instance
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Data Files|*.dat";
            saveFileDialog.Title = "Save as .dat File";
            saveFileDialog.FileName = "myfile.dat";

            // Show the SaveFileDialog and get the result
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Get the selected file path from the SaveFileDialog
                string filePath = saveFileDialog.FileName;

                // Save the string as .dat file
                File.WriteAllText(filePath, content);

                Console.WriteLine("File saved as: " + filePath);
            }


        }

        private void button_export_fem_Click(object sender, EventArgs e)
        {
            if (mesh_data.is_mesh_created == false)
                return;

            // global_static.Show_error_Dialog("BDF Data", mesh_data.get_Nastran_mesh());

            // Your string to save as .dat file
            string content = mesh_data.get_Nastran_mesh();

            // Create a SaveFileDialog instance
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Data Files|*.dat";
            saveFileDialog.Title = "Save as .dat File";
            saveFileDialog.FileName = "myfile.dat";

            // Show the SaveFileDialog and get the result
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Get the selected file path from the SaveFileDialog
                string filePath = saveFileDialog.FileName;

                // Save the string as .dat file
                File.WriteAllText(filePath, content);

                Console.WriteLine("File saved as: " + filePath);
            }


        }


        // _________ INPUTS
        // textBox_length
        // textBox_breadth
        // textBox_meshsize
        // comboBox_stiffener
        // textBox_stiffspacing
        // comboBox_material
        // comboBox_side1_bc
        // comboBox_side2_bc
        // comboBox_side3_bc
        // comboBox_side4_bc
        // textBox_nmass
        // comboBox_addedmass
        // _________ OUTPUTS
        // button_show
        // button_export

    }
}
