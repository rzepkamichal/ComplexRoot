using System.Windows.Forms;

namespace ComplexRoot
{
    /// <summary>
    /// View of the app.
    /// </summary>
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
            this.appTitle = new System.Windows.Forms.Label();
            this.appSubTitle = new System.Windows.Forms.Label();
            this.threadNumComboBox = new System.Windows.Forms.ComboBox();
            this.threadSelectLabel = new System.Windows.Forms.Label();
            this.calculateBtn = new System.Windows.Forms.Button();
            this.inputFileTextBox = new System.Windows.Forms.TextBox();
            this.outputFileTextBox = new System.Windows.Forms.TextBox();
            this.inputFileBtn = new System.Windows.Forms.Button();
            this.outputFileBtn = new System.Windows.Forms.Button();
            this.inputBoxLabel = new System.Windows.Forms.Label();
            this.outputBoxLabel = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.asmRadioBtn = new System.Windows.Forms.RadioButton();
            this.cppRadioBtn = new System.Windows.Forms.RadioButton();
            this.librarySelectLabel = new System.Windows.Forms.Label();
            this.loggerTextBox = new System.Windows.Forms.RichTextBox();
            this.consoleLogLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // appTitle
            // 
            this.appTitle.AutoSize = true;
            this.appTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.appTitle.Location = new System.Drawing.Point(12, 9);
            this.appTitle.Name = "appTitle";
            this.appTitle.Size = new System.Drawing.Size(244, 42);
            this.appTitle.TabIndex = 0;
            this.appTitle.Text = "ComplexRoot";
            // 
            // appSubTitle
            // 
            this.appSubTitle.AutoSize = true;
            this.appSubTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.appSubTitle.Location = new System.Drawing.Point(16, 51);
            this.appSubTitle.Name = "appSubTitle";
            this.appSubTitle.Size = new System.Drawing.Size(240, 20);
            this.appSubTitle.TabIndex = 1;
            this.appSubTitle.Text = "Complex number roots calculator";
            // 
            // threadNumComboBox
            // 
            this.threadNumComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.threadNumComboBox.FormattingEnabled = true;
            //set default options to 1-64
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
            this.threadNumComboBox.Location = new System.Drawing.Point(590, 105);
            this.threadNumComboBox.Name = "threadNumComboBox";
            this.threadNumComboBox.Size = new System.Drawing.Size(115, 21);
            this.threadNumComboBox.TabIndex = 3;
            // 
            // threadSelectLabel
            // 
            this.threadSelectLabel.AutoSize = true;
            this.threadSelectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.threadSelectLabel.Location = new System.Drawing.Point(587, 84);
            this.threadSelectLabel.Name = "threadSelectLabel";
            this.threadSelectLabel.Size = new System.Drawing.Size(118, 16);
            this.threadSelectLabel.TabIndex = 4;
            this.threadSelectLabel.Text = "Number of threads";
            // 
            // calculateBtn
            // 
            this.calculateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.calculateBtn.Location = new System.Drawing.Point(590, 383);
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
            this.inputFileTextBox.Location = new System.Drawing.Point(19, 103);
            this.inputFileTextBox.Name = "inputFileTextBox";
            this.inputFileTextBox.Size = new System.Drawing.Size(447, 22);
            this.inputFileTextBox.TabIndex = 6;
            // 
            // outputFileTextBox
            // 
            this.outputFileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputFileTextBox.Location = new System.Drawing.Point(20, 176);
            this.outputFileTextBox.Name = "outputFileTextBox";
            this.outputFileTextBox.Size = new System.Drawing.Size(446, 22);
            this.outputFileTextBox.TabIndex = 7;
            // 
            // inputFileBtn
            // 
            this.inputFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.inputFileBtn.Location = new System.Drawing.Point(472, 100);
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
            this.outputFileBtn.Location = new System.Drawing.Point(472, 173);
            this.outputFileBtn.Name = "outputFileBtn";
            this.outputFileBtn.Size = new System.Drawing.Size(79, 29);
            this.outputFileBtn.TabIndex = 9;
            this.outputFileBtn.Text = "Open...";
            this.outputFileBtn.UseVisualStyleBackColor = true;
            this.outputFileBtn.Click += new System.EventHandler(this.outputFileBtn_Click);
            // 
            // inputBoxLabel
            // 
            this.inputBoxLabel.AutoSize = true;
            this.inputBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBoxLabel.Location = new System.Drawing.Point(20, 84);
            this.inputBoxLabel.Name = "inputBoxLabel";
            this.inputBoxLabel.Size = new System.Drawing.Size(116, 16);
            this.inputBoxLabel.TabIndex = 11;
            this.inputBoxLabel.Text = "Select JSON input";
            // 
            // outputBoxLabel
            // 
            this.outputBoxLabel.AutoSize = true;
            this.outputBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputBoxLabel.Location = new System.Drawing.Point(20, 157);
            this.outputBoxLabel.Name = "outputBoxLabel";
            this.outputBoxLabel.Size = new System.Drawing.Size(124, 16);
            this.outputBoxLabel.TabIndex = 12;
            this.outputBoxLabel.Text = "Select JSON output";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 440);
            this.splitter1.TabIndex = 13;
            this.splitter1.TabStop = false;
            // 
            // asmRadioBtn
            // 
            this.asmRadioBtn.AutoSize = true;
            this.asmRadioBtn.Location = new System.Drawing.Point(588, 185);
            this.asmRadioBtn.Name = "asmRadioBtn";
            this.asmRadioBtn.Size = new System.Drawing.Size(48, 17);
            this.asmRadioBtn.TabIndex = 14;
            this.asmRadioBtn.TabStop = true;
            this.asmRadioBtn.Text = "ASM";
            this.asmRadioBtn.UseVisualStyleBackColor = true;
            // 
            // cppRadioBtn
            // 
            this.cppRadioBtn.AutoSize = true;
            this.cppRadioBtn.Location = new System.Drawing.Point(588, 208);
            this.cppRadioBtn.Name = "cppRadioBtn";
            this.cppRadioBtn.Size = new System.Drawing.Size(46, 17);
            this.cppRadioBtn.TabIndex = 15;
            this.cppRadioBtn.TabStop = true;
            this.cppRadioBtn.Text = "CPP";
            this.cppRadioBtn.UseVisualStyleBackColor = true;
            // 
            // librarySelectLabel
            // 
            this.librarySelectLabel.AutoSize = true;
            this.librarySelectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.librarySelectLabel.Location = new System.Drawing.Point(587, 157);
            this.librarySelectLabel.Name = "librarySelectLabel";
            this.librarySelectLabel.Size = new System.Drawing.Size(124, 16);
            this.librarySelectLabel.TabIndex = 16;
            this.librarySelectLabel.Text = "Choose library type";
            // 
            // loggerTextBox
            // 
            this.loggerTextBox.Location = new System.Drawing.Point(19, 240);
            this.loggerTextBox.Name = "loggerTextBox";
            this.loggerTextBox.Size = new System.Drawing.Size(532, 193);
            this.loggerTextBox.TabIndex = 17;
            this.loggerTextBox.Text = "";
            // 
            // consoleLogLabel
            // 
            this.consoleLogLabel.AutoSize = true;
            this.consoleLogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleLogLabel.Location = new System.Drawing.Point(20, 221);
            this.consoleLogLabel.Name = "consoleLogLabel";
            this.consoleLogLabel.Size = new System.Drawing.Size(72, 16);
            this.consoleLogLabel.TabIndex = 18;
            this.consoleLogLabel.Text = "Output Log";
            // 
            // AppView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 440);
            this.Controls.Add(this.consoleLogLabel);
            this.Controls.Add(this.loggerTextBox);
            this.Controls.Add(this.librarySelectLabel);
            this.Controls.Add(this.cppRadioBtn);
            this.Controls.Add(this.asmRadioBtn);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.outputBoxLabel);
            this.Controls.Add(this.inputBoxLabel);
            this.Controls.Add(this.outputFileBtn);
            this.Controls.Add(this.inputFileBtn);
            this.Controls.Add(this.outputFileTextBox);
            this.Controls.Add(this.inputFileTextBox);
            this.Controls.Add(this.calculateBtn);
            this.Controls.Add(this.threadSelectLabel);
            this.Controls.Add(this.threadNumComboBox);
            this.Controls.Add(this.appSubTitle);
            this.Controls.Add(this.appTitle);
            this.Name = "AppView";
            this.Text = "ComplexRoot by Michal Rzepka 2019";
            this.Load += new System.EventHandler(this.AppView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label appTitle;
        private System.Windows.Forms.Label appSubTitle;
        private System.Windows.Forms.ComboBox threadNumComboBox;
        private System.Windows.Forms.Label threadSelectLabel;
        private System.Windows.Forms.Button calculateBtn;
        private TextBox inputFileTextBox;
        private TextBox outputFileTextBox;
        private Button inputFileBtn;
        private Button outputFileBtn;
        private Label inputBoxLabel;
        private Label outputBoxLabel;
        private Splitter splitter1;
        private RadioButton asmRadioBtn;
        private RadioButton cppRadioBtn;
        private Label librarySelectLabel;
        private RichTextBox loggerTextBox;
        private Label consoleLogLabel;
    }
}