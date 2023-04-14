using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Text;
using System.Drawing;

namespace Nastran_plate_model
{
    public static class global_static
    {
        // Static class to store the global variables
        public static string Material_data()
        {
            return "Material name,Structural Steel;" +
                    "Youngs modulus,206000;" +
                    "Shear modulus,79230.8;" +
                    "Poissons ratio,0.3;" +
                    "Density,7.85E-9&" +
                    "Material name,Aluminium 6061-T6;" +
                    "Youngs modulus,68000;" +
                    "Shear modulus,26000;" +
                    "Poissons ratio,0.33;" +
                    "Density,2.70E-9&";
        }

        public static string Material_ConvertToNastranFormat(string materialName)
        {
            string[] materialData = Material_data().Split('&');
            foreach (string material in materialData)
            {
                string[] properties = material.Split(';');
                string name = "";
                string youngsModulus = "";
                string shearModulus = "";
                string poissonsRatio = "";
                string density = "";
                foreach (string property in properties)
                {
                    string[] keyValue = property.Split(',');
                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();
                        switch (key)
                        {
                            case "Material name":
                                name = value;
                                break;
                            case "Youngs modulus":
                                youngsModulus = value;
                                break;
                            case "Shear modulus":
                                shearModulus = value;
                                break;
                            case "Poissons ratio":
                                poissonsRatio = value;
                                break;
                            case "Density":
                                density = value;
                                break;
                        }
                    }
                }

                if (name.Equals(materialName, StringComparison.OrdinalIgnoreCase))
                {
                    return $"MAT1,1,{youngsModulus},{shearModulus},{poissonsRatio},{density}";
                }
            }

            return null;
        }


        /*
___________________________________________________________________________________
$ Femap with NX Nastran Property 213 : L_250X125X12X15
PBEAML       213       7 MSCBML0       L                                +       
+           125.    250.     15.     12.      0.
___________________________________________________________________________________
         */
        public static string L_Stiffener_data()
        {
            return "Stiff,L 75x50x6;" +
                "DIM1,50;" +
                "DIM2,75;" +
                "DIM3,6;" +
                "DIM4,6&" +
                "Stiff,L 75x75x6;" +
                "DIM1,75;" +
                "DIM2,75;" +
                "DIM3,6;" +
                "DIM4,6&" +
                "Stiff,L 80x50x6;" +
                "DIM1,50;" +
                "DIM2,80;" +
                "DIM3,6;" +
                "DIM4,6&" +
                "Stiff,L 80x50x8;" +
                "DIM1,50;" +
                "DIM2,80;" +
                "DIM3,8;" +
                "DIM4,8&" +
                "Stiff,L 100x50x6;" +
                "DIM1,50;" +
                "DIM2,100;" +
                "DIM3,6;" +
                "DIM4,6&" +
                "Stiff,L 100x50x8;" +
                "DIM1,50;" +
                "DIM2,100;" +
                "DIM3,8;" +
                "DIM4,8&" +
                "Stiff,L 100x75x7;" +
                "DIM1,75;" +
                "DIM2,100;" +
                "DIM3,7;" +
                "DIM4,7&" +
                "Stiff,L 120x60x6;" +
                "DIM1,60;" +
                "DIM2,120;" +
                "DIM3,6;" +
                "DIM4,6&" +
                "Stiff,L 120x80x8;" +
                "DIM1,80;" +
                "DIM2,120;" +
                "DIM3,8;" +
                "DIM4,8&" +
                "Stiff,L 125x75x7;" +
                "DIM1,75;" +
                "DIM2,125;" +
                "DIM3,7;" +
                "DIM4,7&" +
                "Stiff,L 150x90x9;" +
                "DIM1,90;" +
                "DIM2,150;" +
                "DIM3,9;" +
                "DIM4,9&" +
                "Stiff,L 150x90x12;" +
                "DIM1,90;" +
                "DIM2,150;" +
                "DIM3,12;" +
                "DIM4,12&" +
                "Stiff,L 160x60x6;" +
                "DIM1,90;" +
                "DIM2,150;" +
                "DIM3,12;" +
                "DIM4,12&" +
                "Stiff,L 200x90x10x14;" +
                "DIM1,90;" +
                "DIM2,200;" +
                "DIM3,14;" +
                "DIM4,10&"+
                "Stiff,L 250x90x10x15;" +
                "DIM1,90;" +
                "DIM2,250;" +
                "DIM3,15;" +
                "DIM4,10&";
        }

        public static string Stiffner_ConvertToNastranFormat(string input, ref double offset_val)
        {
            if (input.StartsWith("L"))
            {
                // L Stiffener
                string[] data = L_Stiffener_data().Split('&');

                foreach (var item in data)
                {
                    if (item.Contains(input))
                    {
                        string[] itemData = item.Split(';');
                        string dim1 = "", dim2 = "", dim3 = "", dim4 = "";

                        foreach (var dim in itemData)
                        {
                            if (dim.Contains("DIM1"))
                                dim1 = dim.Replace("DIM1,", "");
                            else if (dim.Contains("DIM2"))
                                dim2 = dim.Replace("DIM2,", "");
                            else if (dim.Contains("DIM3"))
                                dim3 = dim.Replace("DIM3,", "");
                            else if (dim.Contains("DIM4"))
                                dim4 = dim.Replace("DIM4,", "");
                        }

                        double.TryParse(dim2, out offset_val);

                        string result = string.Format("PBEAML,{0},{1},MSCBML0,L,,,,,{2},{3},{4},{5}",
                                                    "2", "1", dim1, dim2, dim3, dim4);
                        return result;
                    }
                }
            }
            else if (input.StartsWith("T"))
            {
                // T Stiffener
                string[] data = T_Stiffener_data().Split('&');

                foreach (var item in data)
                {
                    if (item.Contains(input))
                    {
                        string[] itemData = item.Split(';');
                        string dim1 = "", dim2 = "", dim3 = "", dim4 = "";

                        foreach (var dim in itemData)
                        {
                            if (dim.Contains("DIM1"))
                                dim1 = dim.Replace("DIM1,", "");
                            else if (dim.Contains("DIM2"))
                                dim2 = dim.Replace("DIM2,", "");
                            else if (dim.Contains("DIM3"))
                                dim3 = dim.Replace("DIM3,", "");
                            else if (dim.Contains("DIM4"))
                                dim4 = dim.Replace("DIM4,", "");
                        }

                        double.TryParse(dim2, out offset_val);

                        string result = string.Format("PBEAML,{0},{1},MSCBML0,T,,,,,{2},{3},{4},{5}",
                                                    "2", "1", dim1, dim2, dim3, dim4);
                        return result;
                    }
                }
            }
            else if (input.StartsWith("F"))
            {
                string[] data = HP_Stiffener_data().Split('&'); // Split the static string by '&' to separate the lines

                foreach (var item in data)
                {
                    if (item.Contains(input))
                    {
                        string[] itemData = item.Split(';');
                        string dim1 = "", dim2 = "";

                        foreach (var dim in itemData)
                        {
                            if (dim.Contains("DIM1"))
                                dim1 = dim.Replace("DIM1,", "");
                            else if (dim.Contains("DIM2"))
                                dim2 = dim.Replace("DIM2,", "");
                        }

                        double.TryParse(dim2, out offset_val);

                        string result = string.Format("PBEAML,{0},{1},MSCBML0,BAR,,,,,{2},{3}",
                                                    "2", "1", dim1, dim2);
                        return result;
                    }
                }
            }

            return null; // return null if input not found in the static string
        }

        /*
___________________________________________________________________________________
$ Femap with NX Nastran Property 508 : SM_001_H_250X10_125X10_X1
PBEAML       508      12 MSCBML0       T                                +       
+           125.    260.     10.     10.      0.
___________________________________________________________________________________
         */

        public static string T_Stiffener_data()
        {
            return "Stiff,T 80x46x3.8x5.2;" +
                "DIM1,46;" +
                "DIM2,80;" +
                "DIM3,5.2;" +
                "DIM4,3.8&" +
                "Stiff,T 100x55x4.1x5.7;" +
                "DIM1,55;" +
                "DIM2,100;" +
                "DIM3,5.7;" +
                "DIM4,4.1&" +
                "Stiff,T 120x64x4.4x6.3;" +
                "DIM1,64;" +
                "DIM2,120;" +
                "DIM3,6.3;" +
                "DIM4,4.4&" +
                "Stiff,T 140x73x4.7x6.9;" +
                "DIM1,73;" +
                "DIM2,140;" +
                "DIM3,6.9;" +
                "DIM4,4.7&" +
                "Stiff,T 160x82x5x7.4;" +
                "DIM1,82;" +
                "DIM2,160;" +
                "DIM3,5;" +
                "DIM4,7.4&" +
                "Stiff,T 180x91x5.3x8;" +
                "DIM1,91;" +
                "DIM2,180;" +
                "DIM3,8;" +
                "DIM4,5.3&" +
                "Stiff,T 200x100x5.6x8.5;" +
                "DIM1,100;" +
                "DIM2,200;" +
                "DIM3,5.6;" +
                "DIM4,8.5&" +
                "Stiff,T 200x80x6x15;" +
                "DIM1,80;" +
                "DIM2,200;" +
                "DIM3,15;" +
                "DIM4,6&" +
                "Stiff,T 200x100x10x15;" +
                "DIM1,100;" +
                "DIM2,200;" +
                "DIM3,15;" +
                "DIM4,10&"+
                "Stiff,T 400x100x14x14;" +
                "DIM1,100;" +
                "DIM2,400;" +
                "DIM3,14;" +
                "DIM4,14&";
        }

        /*
___________________________________________________________________________________
$ Femap with NX Nastran Property 11 : H_150X15
PBEAML        11       6 MSCBML0     BAR                                +       
+            15.    150.      0.         
___________________________________________________________________________________
         */
        public static string HP_Stiffener_data()
        {
            return "Stiff,F 60x6;" +
                "DIM1,6;" +
                "DIM2,60&" +
                "Stiff,F 80x8;" +
                "DIM1,8;" +
                "DIM2,80&" +
                "Stiff,F 100x8;" +
                "DIM1,8;" +
                "DIM2,100&" +
                "Stiff,F 100x10;" +
                "DIM1,10;" +
                "DIM2,100&" +
                "Stiff,F 100x12;" +
                "DIM1,12;" +
                "DIM2,100&" +
                "Stiff,F 120x8;" +
                "DIM1,8;" +
                "DIM2,120&" +
                "Stiff,F 140x7;" +
                "DIM1,7;" +
                "DIM2,140&" +
                "Stiff,F 150x12;" +
                "DIM1,12;" +
                "DIM2,150&" +
                "Stiff,F 160x8;" +
                "DIM1,8;" +
                "DIM2,160&";
        }

        public static List<string> GetBoundaryConditions()
        {
            List<string> Bconditions = new List<string>();
            Bconditions.Add("Pinned");
            Bconditions.Add("Fixed");
            Bconditions.Add("Free");
            return Bconditions;
        }

        public static List<string> GetAddedMassConditions()
        {
            List<string> AMassconditions = new List<string>();
            AMassconditions.Add("Nil");
            AMassconditions.Add("Single Side");
            AMassconditions.Add("Both Sides");
            return AMassconditions;
        }

        public static List<string> GetStiffNames(int get_type)
        {
            string stiffData = "";
            if (get_type == 0)
            {
                // L Stiffener
                stiffData = L_Stiffener_data();
            }
            else if (get_type == 1)
            {
                // HP Stiffener
                stiffData = HP_Stiffener_data();
            }
            else if (get_type == 2)
            {
                // T Stiffener
                stiffData = T_Stiffener_data();
            }

            List<string> stiffNames = new List<string>();

            // Split the string by semicolon, comma, and ampersand delimiters
            string[] stiffDataArray = stiffData.Split(';', ',', '&');

            // Loop through the array and extract the stiff names
            for (int i = 0; i < stiffDataArray.Length; i += 2)
            {
                if (stiffDataArray[i].Trim().Equals("Stiff", StringComparison.OrdinalIgnoreCase))
                {
                    stiffNames.Add(stiffDataArray[i + 1].Trim());
                }
            }

            return stiffNames;
        }

        public static List<string> GetMaterialNames()
        {
            string materialData = Material_data(); // Call the Material_data() function to get the delimited string
            List<string> materialNames = new List<string>();

            // Split the string by semicolon, comma, and ampersand delimiters
            string[] materialDataArray = materialData.Split(';', ',', '&');

            int j = 0;
            // Loop through the array and extract the material names
            for (int i = 0; i < materialDataArray.Length; i += 2)
            {
                if (materialDataArray[i].Trim().Equals("Material name", StringComparison.OrdinalIgnoreCase))
                {
                    materialNames.Add(materialDataArray[i + 1].Trim());
                    j++;
                }
            }

            return materialNames;
        }

        public static string Thickness_ConvertToNastranFormat(double thickness, double non_structural_mass)
        {
            // Format the values into a Nastran format string
            string thickness_nastranFormat = string.Format("PSHELL,1,1,{0,-7:F1},1,1.0,1,0.833333,{1,12:0.0E+0}",
                thickness, non_structural_mass);

            return thickness_nastranFormat;
        }


        public static void Show_error_Dialog(string title, string text)
        {
            var form = new Form()
            {
                Text = title,
                Size = new Size(800, 600)
            };

            form.Controls.Add(new TextBox()
            {
                Font = new Font("Segoe UI", 12),
                Text = text,
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                Dock = DockStyle.Fill
            });

            form.ShowDialog();
            form.Controls.OfType<TextBox>().First().Dispose();
            form.Dispose();
        }
    }
}
