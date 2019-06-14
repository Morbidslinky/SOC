﻿namespace SOC.QuestObjects.Vehicle
{
    partial class VehicleBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox v_groupBox_main;
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_class = new System.Windows.Forms.ComboBox();
            this.comboBox_vehicle = new System.Windows.Forms.ComboBox();
            this.checkBox_target = new System.Windows.Forms.CheckBox();
            this.textBox_rot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_zcoord = new System.Windows.Forms.TextBox();
            this.textBox_ycoord = new System.Windows.Forms.TextBox();
            this.textBox_xcoord = new System.Windows.Forms.TextBox();
            v_groupBox_main = new System.Windows.Forms.GroupBox();
            v_groupBox_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // v_groupBox_main
            // 
            v_groupBox_main.BackColor = System.Drawing.Color.DarkGray;
            v_groupBox_main.Controls.Add(this.label4);
            v_groupBox_main.Controls.Add(this.label3);
            v_groupBox_main.Controls.Add(this.label2);
            v_groupBox_main.Controls.Add(this.comboBox_class);
            v_groupBox_main.Controls.Add(this.comboBox_vehicle);
            v_groupBox_main.Controls.Add(this.checkBox_target);
            v_groupBox_main.Controls.Add(this.textBox_rot);
            v_groupBox_main.Controls.Add(this.label1);
            v_groupBox_main.Controls.Add(this.textBox_zcoord);
            v_groupBox_main.Controls.Add(this.textBox_ycoord);
            v_groupBox_main.Controls.Add(this.textBox_xcoord);
            v_groupBox_main.Dock = System.Windows.Forms.DockStyle.Fill;
            v_groupBox_main.Location = new System.Drawing.Point(0, 0);
            v_groupBox_main.Name = "v_groupBox_main";
            v_groupBox_main.Size = new System.Drawing.Size(268, 156);
            v_groupBox_main.TabIndex = 0;
            v_groupBox_main.TabStop = false;
            v_groupBox_main.Text = "VehicleBox";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 21);
            this.label4.TabIndex = 25;
            this.label4.Text = "Class:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 24;
            this.label3.Text = "Vehicle:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "Rotation:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // v_comboBox_class
            // 
            this.comboBox_class.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_class.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_class.FormattingEnabled = true;
            this.comboBox_class.Location = new System.Drawing.Point(85, 129);
            this.comboBox_class.Name = "v_comboBox_class";
            this.comboBox_class.Size = new System.Drawing.Size(174, 21);
            this.comboBox_class.TabIndex = 22;
            // 
            // v_comboBox_vehicle
            // 
            this.comboBox_vehicle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_vehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_vehicle.FormattingEnabled = true;
            this.comboBox_vehicle.Location = new System.Drawing.Point(85, 102);
            this.comboBox_vehicle.Name = "v_comboBox_vehicle";
            this.comboBox_vehicle.Size = new System.Drawing.Size(174, 21);
            this.comboBox_vehicle.TabIndex = 21;
            // 
            // v_checkBox_target
            // 
            this.checkBox_target.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBox_target.AutoSize = true;
            this.checkBox_target.Location = new System.Drawing.Point(85, 75);
            this.checkBox_target.Name = "v_checkBox_target";
            this.checkBox_target.Size = new System.Drawing.Size(68, 17);
            this.checkBox_target.TabIndex = 20;
            this.checkBox_target.Text = "Is Target";
            this.checkBox_target.UseVisualStyleBackColor = true;
            // 
            // v_textBox_rot
            // 
            this.textBox_rot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_rot.Location = new System.Drawing.Point(85, 45);
            this.textBox_rot.Name = "v_textBox_rot";
            this.textBox_rot.Size = new System.Drawing.Size(174, 20);
            this.textBox_rot.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Coordinates:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // v_textBox_zcoord
            // 
            this.textBox_zcoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_zcoord.Location = new System.Drawing.Point(205, 19);
            this.textBox_zcoord.Name = "v_textBox_zcoord";
            this.textBox_zcoord.Size = new System.Drawing.Size(54, 20);
            this.textBox_zcoord.TabIndex = 17;
            // 
            // v_textBox_ycoord
            // 
            this.textBox_ycoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_ycoord.Location = new System.Drawing.Point(145, 19);
            this.textBox_ycoord.Name = "v_textBox_ycoord";
            this.textBox_ycoord.Size = new System.Drawing.Size(54, 20);
            this.textBox_ycoord.TabIndex = 16;
            // 
            // v_textBox_xcoord
            // 
            this.textBox_xcoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox_xcoord.Location = new System.Drawing.Point(85, 19);
            this.textBox_xcoord.Name = "v_textBox_xcoord";
            this.textBox_xcoord.Size = new System.Drawing.Size(54, 20);
            this.textBox_xcoord.TabIndex = 15;
            // 
            // VehicleBoxF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(v_groupBox_main);
            this.Name = "VehicleBoxF";
            this.Size = new System.Drawing.Size(268, 156);
            v_groupBox_main.ResumeLayout(false);
            v_groupBox_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox_class;
        public System.Windows.Forms.ComboBox comboBox_vehicle;
        public System.Windows.Forms.CheckBox checkBox_target;
        public System.Windows.Forms.TextBox textBox_rot;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_zcoord;
        public System.Windows.Forms.TextBox textBox_ycoord;
        public System.Windows.Forms.TextBox textBox_xcoord;
    }
}
