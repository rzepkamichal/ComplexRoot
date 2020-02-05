using ComplexRoot.complex_num;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ComplexRoot
{
    public partial class AppView : Form
    {
        public AppView()
        {
            InitializeComponent();
        }

        private void AppView_Load(object sender, EventArgs e)
        {
            outputFileTextBox.Text = Environment.CurrentDirectory + "\\output.json";
            var enumerator = threadNumComboBox.Items.GetEnumerator();
            enumerator.MoveNext();
            threadNumComboBox.Text = enumerator.Current.ToString();
            asmRadioBtn.Checked = true;
        }

        private void inputFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog inputFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Choose json input for calculation",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "json",
                Filter = "json files (*.json)|*.json",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (inputFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFileTextBox.Text = inputFileDialog.FileName;
            }
        }

        private void outputFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog outputFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Choose json output for calculation",

                CheckFileExists = false,
                CheckPathExists = true,

                DefaultExt = "json",
                Filter = "json files (*.json)|*.json",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (outputFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputFileTextBox.Text = outputFileDialog.FileName;
            }

        }

        private void calculateBtn_Click(object sender, EventArgs e)
        {
            string inputPath = inputFileTextBox.Text;
            string outputPath = outputFileTextBox.Text;

            string inputJson;
            inputJson = File.ReadAllText(inputPath);

            LibraryType lib = LibraryType.ASM;
            if (cppRadioBtn.Checked)
                lib = LibraryType.CPP;

            int threadNum = Int32.Parse(threadNumComboBox.Text);

            List<ComplexAlgebraic> inputs = JsonConvert.DeserializeObject<List<ComplexAlgebraic>>(inputJson);
            ResultsPresentation resultsPresentation = ComplexNumUtils.calculateRoots(inputs, threadNum, lib);

            string resultsJson = JsonConvert.SerializeObject(resultsPresentation, Formatting.Indented);
            File.WriteAllText(outputPath, resultsJson);
        }
    }
}
