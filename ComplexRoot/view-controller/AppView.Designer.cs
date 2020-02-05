﻿using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ComplexRoot
{
    partial class AppView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.threadNumComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.calculateBtn = new System.Windows.Forms.Button();
            this.inputFileTextBox = new System.Windows.Forms.TextBox();
            this.outputFileTextBox = new System.Windows.Forms.TextBox();
            this.inputFileBtn = new System.Windows.Forms.Button();
            this.outputFileBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.asmRadioBtn = new System.Windows.Forms.RadioButton();
            this.cppRadioBtn = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "ComplexRoot";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(16, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Complex number roots calculator";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 375);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(693, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // threadNumComboBox
            // 
            this.threadNumComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.threadNumComboBox.FormattingEnabled = true;
            this.threadNumComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64"});
            this.threadNumComboBox.Location = new System.Drawing.Point(590, 142);
            this.threadNumComboBox.Name = "threadNumComboBox";
            this.threadNumComboBox.Size = new System.Drawing.Size(115, 21);
            this.threadNumComboBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(587, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Number of threads";
            // 
            // calculateBtn
            // 
            this.calculateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.calculateBtn.Location = new System.Drawing.Point(590, 310);
            this.calculateBtn.Name = "calculateBtn";
            this.calculateBtn.Size = new System.Drawing.Size(115, 45);
            this.calculateBtn.TabIndex = 5;
            this.calculateBtn.Text = "Calculate";
            this.calculateBtn.UseVisualStyleBackColor = true;
            this.calculateBtn.Click += new System.EventHandler(this.calculateBtn_Click);
            // 
            // inputFileTextBox
            // 
            this.inputFileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputFileTextBox.Location = new System.Drawing.Point(19, 140);
            this.inputFileTextBox.Name = "inputFileTextBox";
            this.inputFileTextBox.Size = new System.Drawing.Size(447, 22);
            this.inputFileTextBox.TabIndex = 6;
            // 
            // outputFileTextBox
            // 
            this.outputFileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputFileTextBox.Location = new System.Drawing.Point(20, 213);
            this.outputFileTextBox.Name = "outputFileTextBox";
            this.outputFileTextBox.Size = new System.Drawing.Size(446, 22);
            this.outputFileTextBox.TabIndex = 7;
            // 
            // inputFileBtn
            // 
            this.inputFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.inputFileBtn.Location = new System.Drawing.Point(472, 137);
            this.inputFileBtn.Name = "inputFileBtn";
            this.inputFileBtn.Size = new System.Drawing.Size(79, 28);
            this.inputFileBtn.TabIndex = 8;
            this.inputFileBtn.Text = "Open...";
            this.inputFileBtn.UseVisualStyleBackColor = true;
            this.inputFileBtn.Click += new System.EventHandler(this.inputFileBtn_Click);
            // 
            // outputFileBtn
            // 
            this.outputFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.outputFileBtn.Location = new System.Drawing.Point(472, 210);
            this.outputFileBtn.Name = "outputFileBtn";
            this.outputFileBtn.Size = new System.Drawing.Size(79, 29);
            this.outputFileBtn.TabIndex = 9;
            this.outputFileBtn.Text = "Open...";
            this.outputFileBtn.UseVisualStyleBackColor = true;
            this.outputFileBtn.Click += new System.EventHandler(this.outputFileBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Select JSON input";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Select JSON output";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 410);
            this.splitter1.TabIndex = 13;
            this.splitter1.TabStop = false;
            // 
            // radioButton1
            // 
            this.asmRadioBtn.AutoSize = true;
            this.asmRadioBtn.Location = new System.Drawing.Point(588, 222);
            this.asmRadioBtn.Name = "radioButton1";
            this.asmRadioBtn.Size = new System.Drawing.Size(48, 17);
            this.asmRadioBtn.TabIndex = 14;
            this.asmRadioBtn.TabStop = true;
            this.asmRadioBtn.Text = "ASM";
            this.asmRadioBtn.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.cppRadioBtn.AutoSize = true;
            this.cppRadioBtn.Location = new System.Drawing.Point(588, 245);
            this.cppRadioBtn.Name = "radioButton2";
            this.cppRadioBtn.Size = new System.Drawing.Size(46, 17);
            this.cppRadioBtn.TabIndex = 15;
            this.cppRadioBtn.TabStop = true;
            this.cppRadioBtn.Text = "CPP";
            this.cppRadioBtn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(587, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Choose library type";
            // 
            // AppView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 410);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cppRadioBtn);
            this.Controls.Add(this.asmRadioBtn);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.outputFileBtn);
            this.Controls.Add(this.inputFileBtn);
            this.Controls.Add(this.outputFileTextBox);
            this.Controls.Add(this.inputFileTextBox);
            this.Controls.Add(this.calculateBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.threadNumComboBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AppView";
            this.Text = "ComplexRoot by Michal Rzepka 2019";
            this.Load += new System.EventHandler(this.AppView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox threadNumComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button calculateBtn;
        private TextBox inputFileTextBox;
        private TextBox outputFileTextBox;
        private Button inputFileBtn;
        private Button outputFileBtn;
        private Label label5;
        private Label label6;
        private Splitter splitter1;
        private RadioButton asmRadioBtn;
        private RadioButton cppRadioBtn;
        private Label label4;
    }
}