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
            inputFileTextBox.ReadOnly = true;
            outputFileTextBox.ReadOnly = true;
            loggerTextBox.ReadOnly = true;
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
            Button btn = (Button)sender;
            loggerTextBox.Clear();
            btn.Enabled = false;
            string inputPath = inputFileTextBox.Text;
            string outputPath = outputFileTextBox.Text;
            int threadNum = Int32.Parse(threadNumComboBox.Text);
            LibraryType lib = LibraryType.ASM;

            if (!ComplexNumUtils.areLibrariesPresent())
            {
                alertLibsNotPresent();
                resetCalculationEvent(btn);
                return;
            }

            if (cppRadioBtn.Checked)
                lib = LibraryType.CPP;

            loggerTextBox.AppendText("======================================\n");
            loggerTextBox.AppendText("Loading input data...\n");

            string inputJson;
            double fileLenMB = 0;
            List<ComplexAlgebraic> inputs = null;
            try
            {   
                //check input file size
                fileLenMB = new System.IO.FileInfo(inputPath).Length / 1000000.0;

                if (fileLenMB > 20)
                {
                    //about 800 000 roots to calculate
                    if (fileLenMB >= 30)
                    {
                        alertFileTooLarge();
                        resetCalculationEvent(btn);
                        return;
                    }

                    DialogResult result = alertFileSize();
                    if (result == DialogResult.No)
                    {
                        resetCalculationEvent(btn);
                        return;
                    }

                }
                inputJson = File.ReadAllText(inputPath);
                inputs = JsonConvert.DeserializeObject<List<ComplexAlgebraic>>(inputJson);
            }
            catch (System.ArgumentException)
            {
                alertInvalidInputFile();
                resetCalculationEvent(btn);
                return;
            }
            catch (Newtonsoft.Json.JsonReaderException )
            {
                alertInvalidJSON();
                resetCalculationEvent(btn);
                return;
            }
            catch (Newtonsoft.Json.JsonSerializationException)
            {
                alertInvalidJSON();
                resetCalculationEvent(btn);
                return;
            }

            loggerTextBox.AppendText("Input data has been loaded successfully.\n");
            loggerTextBox.AppendText("Number of input entries: " + inputs.Count + "\n");
            loggerTextBox.AppendText("Input file size (MB): " + fileLenMB + "\n");

            ResultsPresentation resultsPresentation = ComplexNumUtils.calculateRoots(
                inputs, threadNum, lib, loggerTextBox);

            loggerTextBox.AppendText("Saving results...\n");
            string resultsJson = JsonConvert.SerializeObject(resultsPresentation, Formatting.Indented);
            File.WriteAllText(outputPath, resultsJson);

            loggerTextBox.AppendText("Results have been saved to output file successfully.\n");
            loggerTextBox.AppendText("======================================\n");

            //wait until file is completely saved
            FileInfo fi = new FileInfo(outputPath);
            while (IsFileLocked(fi)) { }

            btn.Enabled = true;
        }

        private void resetCalculationEvent(Button btn)
        {
            btn.Enabled = true;
            inputFileTextBox.Text = "";
            loggerTextBox.AppendText("Calculation failed.\n");
            loggerTextBox.AppendText("======================================\n");
        }

        private void alertInvalidJSON()
        {
            MessageBox.Show("Input file is malformed. Please provide a valid one.", "Error");
        }

        private void alertInvalidInputFile()
        {
            MessageBox.Show("Input file is not valid.", "Error");
        }

        private DialogResult alertFileSize()
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox
                .Show("Input file is relativly big, which can result in incorrect functioning. " +
                    "Would you like to continue?",
                    "Caution",
                    buttons);
            return result;
        }

        private void alertFileTooLarge()
        {
            MessageBox.Show("Input file is too big.", "Error");
        }

        private void alertLibsNotPresent()
        {
            MessageBox.Show("ComplexRootLibCpp.dll or ComplexRootLibAsm.dll or Newtonsoft.Json.dll missing." +
                "Cannot perform calculation", "Error");
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }
    }
}
