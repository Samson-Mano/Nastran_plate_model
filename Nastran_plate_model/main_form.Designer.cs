namespace Nastran_plate_model
{
    partial class main_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_form));
            this.main_pic = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_thickness = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_breadth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_length = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_stiffspacing = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_stiffener = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_meshsize = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox_material = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox_side4_bc = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_side3_bc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_side2_bc = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_side1_bc = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox_addedmass = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_nmass = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_create = new System.Windows.Forms.Button();
            this.button_export = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_pic
            // 
            this.main_pic.BackgroundImage = global::Nastran_plate_model.Properties.Resources.plante_nat_freq;
            this.main_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.main_pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.main_pic.Location = new System.Drawing.Point(12, 12);
            this.main_pic.Name = "main_pic";
            this.main_pic.Size = new System.Drawing.Size(560, 280);
            this.main_pic.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_thickness);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBox_breadth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_length);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 125);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plate";
            // 
            // textBox_thickness
            // 
            this.textBox_thickness.Location = new System.Drawing.Point(124, 86);
            this.textBox_thickness.Name = "textBox_thickness";
            this.textBox_thickness.Size = new System.Drawing.Size(68, 23);
            this.textBox_thickness.TabIndex = 5;
            this.textBox_thickness.Text = "8";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 15);
            this.label12.TabIndex = 4;
            this.label12.Text = "Thickness, t (mm):";
            // 
            // textBox_breadth
            // 
            this.textBox_breadth.Location = new System.Drawing.Point(124, 57);
            this.textBox_breadth.Name = "textBox_breadth";
            this.textBox_breadth.Size = new System.Drawing.Size(68, 23);
            this.textBox_breadth.TabIndex = 3;
            this.textBox_breadth.Text = "2800";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Breadth, b (mm):";
            // 
            // textBox_length
            // 
            this.textBox_length.Location = new System.Drawing.Point(124, 28);
            this.textBox_length.Name = "textBox_length";
            this.textBox_length.Size = new System.Drawing.Size(68, 23);
            this.textBox_length.TabIndex = 1;
            this.textBox_length.Text = "2400";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Length, a (mm):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_stiffspacing);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBox_stiffener);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(10, 429);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 92);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stiffener";
            // 
            // textBox_stiffspacing
            // 
            this.textBox_stiffspacing.Location = new System.Drawing.Point(126, 56);
            this.textBox_stiffspacing.Name = "textBox_stiffspacing";
            this.textBox_stiffspacing.Size = new System.Drawing.Size(68, 23);
            this.textBox_stiffspacing.TabIndex = 3;
            this.textBox_stiffspacing.Text = "800";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Spacing, s (mm):";
            // 
            // comboBox_stiffener
            // 
            this.comboBox_stiffener.FormattingEnabled = true;
            this.comboBox_stiffener.Location = new System.Drawing.Point(126, 27);
            this.comboBox_stiffener.Name = "comboBox_stiffener";
            this.comboBox_stiffener.Size = new System.Drawing.Size(116, 23);
            this.comboBox_stiffener.TabIndex = 1;
            this.comboBox_stiffener.Text = "L100x75x7";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Stiffener Beam:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_meshsize);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.comboBox_material);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 527);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 83);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Material";
            // 
            // textBox_meshsize
            // 
            this.textBox_meshsize.Location = new System.Drawing.Point(125, 48);
            this.textBox_meshsize.Name = "textBox_meshsize";
            this.textBox_meshsize.Size = new System.Drawing.Size(68, 23);
            this.textBox_meshsize.TabIndex = 3;
            this.textBox_meshsize.Text = "100";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 15);
            this.label13.TabIndex = 2;
            this.label13.Text = "Mesh size (mm):";
            // 
            // comboBox_material
            // 
            this.comboBox_material.FormattingEnabled = true;
            this.comboBox_material.Location = new System.Drawing.Point(124, 19);
            this.comboBox_material.Name = "comboBox_material";
            this.comboBox_material.Size = new System.Drawing.Size(148, 23);
            this.comboBox_material.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Material Data:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox_side4_bc);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.comboBox_side3_bc);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.comboBox_side2_bc);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.comboBox_side1_bc);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(294, 298);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 125);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Boundary Conditions";
            // 
            // comboBox_side4_bc
            // 
            this.comboBox_side4_bc.FormattingEnabled = true;
            this.comboBox_side4_bc.Location = new System.Drawing.Point(175, 57);
            this.comboBox_side4_bc.Name = "comboBox_side4_bc";
            this.comboBox_side4_bc.Size = new System.Drawing.Size(97, 23);
            this.comboBox_side4_bc.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(155, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 15);
            this.label9.TabIndex = 6;
            this.label9.Text = "4";
            // 
            // comboBox_side3_bc
            // 
            this.comboBox_side3_bc.FormattingEnabled = true;
            this.comboBox_side3_bc.Location = new System.Drawing.Point(98, 28);
            this.comboBox_side3_bc.Name = "comboBox_side3_bc";
            this.comboBox_side3_bc.Size = new System.Drawing.Size(86, 23);
            this.comboBox_side3_bc.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(78, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "3";
            // 
            // comboBox_side2_bc
            // 
            this.comboBox_side2_bc.FormattingEnabled = true;
            this.comboBox_side2_bc.Location = new System.Drawing.Point(28, 57);
            this.comboBox_side2_bc.Name = "comboBox_side2_bc";
            this.comboBox_side2_bc.Size = new System.Drawing.Size(86, 23);
            this.comboBox_side2_bc.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "2";
            // 
            // comboBox_side1_bc
            // 
            this.comboBox_side1_bc.FormattingEnabled = true;
            this.comboBox_side1_bc.Location = new System.Drawing.Point(98, 86);
            this.comboBox_side1_bc.Name = "comboBox_side1_bc";
            this.comboBox_side1_bc.Size = new System.Drawing.Size(86, 23);
            this.comboBox_side1_bc.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox_addedmass);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.textBox_nmass);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(294, 429);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(278, 91);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Non-Structural Mass Details";
            // 
            // comboBox_addedmass
            // 
            this.comboBox_addedmass.Enabled = false;
            this.comboBox_addedmass.FormattingEnabled = true;
            this.comboBox_addedmass.Location = new System.Drawing.Point(175, 51);
            this.comboBox_addedmass.Name = "comboBox_addedmass";
            this.comboBox_addedmass.Size = new System.Drawing.Size(97, 23);
            this.comboBox_addedmass.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(97, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 15);
            this.label11.TabIndex = 2;
            this.label11.Text = "Added Mass:";
            // 
            // textBox_nmass
            // 
            this.textBox_nmass.Location = new System.Drawing.Point(175, 23);
            this.textBox_nmass.Name = "textBox_nmass";
            this.textBox_nmass.Size = new System.Drawing.Size(68, 23);
            this.textBox_nmass.TabIndex = 1;
            this.textBox_nmass.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(173, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "Non structural mass (Kg/m2):";
            // 
            // button_create
            // 
            this.button_create.Location = new System.Drawing.Point(322, 549);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(115, 44);
            this.button_create.TabIndex = 6;
            this.button_create.Text = "Create Mesh";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // button_export
            // 
            this.button_export.Location = new System.Drawing.Point(452, 549);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(113, 44);
            this.button_export.TabIndex = 7;
            this.button_export.Text = "Export BDF";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 621);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.main_pic);
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(600, 660);
            this.MinimumSize = new System.Drawing.Size(600, 660);
            this.Name = "main_form";
            this.Text = "Nastran Plate Input Creator for Modal analysis ----- Developed by Samson Mano ";
            this.Load += new System.EventHandler(this.main_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel main_pic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_breadth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_length;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_stiffspacing;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_stiffener;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox_material;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox_side4_bc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_side3_bc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_side2_bc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_side1_bc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox_nmass;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_addedmass;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button_create;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.TextBox textBox_thickness;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_meshsize;
        private System.Windows.Forms.Label label13;
    }
}