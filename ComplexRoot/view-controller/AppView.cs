using ComplexRoot.complex_num;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
/*
Autor: Michał Rzepka
Rodzaj studiów: SSI
Kierunek: Informatyka
Semestr: 5
Grupa dziekańska: 1
Sekcja lab: 2
Przedmiot: Języki Asemblerowe
Email: michrze558@student.polsl.pl
Temat projektu: Wyznaczanie pierwiastków liczb zespolonych
Data oddania projektu: 10-02-2020
*/

/* CHANGELOG
 * 04-02-2020 create model
 * 04-02-2020 add json i/o support
 * 04-02-2020 add thread support
 * 05-02-2020 fix asm multithreading bug (replace .data section with stack usage)
 * 05-02-2020 create GUI dialog
 * 05-02-2020 add dialog control listeners
 * 09-02-2020 provide input data and enviroment validation
 * 10-02-2020 rework to put result list in the same order as the input list
 * 10-02-2020 add documentation
 */

namespace ComplexRoot
{
    /// <summary>
    /// Controller for the app view.
    /// </summary>
    public partial class AppView : Form
    {
        //initialize view components
        public AppView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lifecycle hook triggered, when the view loads.
        /// Set default properties of components.
        /// </summary>
        /// <param name="sender">Reference to event sender.</param>
        /// <param name="e">Reference to event arguments.</param>
        private void AppView_Load(object sender, EventArgs e)
        {
            //set default output file path
            outputFileTextBox.Text = Environment.CurrentDirectory + "\\output.json";

            //set default thread number in combobox to 1
            var enumerator = threadNumComboBox.Items.GetEnumerator();
            enumerator.MoveNext();
            threadNumComboBox.Text = enumerator.Current.ToString();

            //set radio button to asm by default
            asmRadioBtn.Checked = true;

            //disable manual editing of input/output file paths
            inputFileTextBox.ReadOnly = true;
            outputFileTextBox.ReadOnly = true;

            //disable manual editing of console text box
            loggerTextBox.ReadOnly = true;
        }

        /// <summary>
        /// Action performed, when the "open..." button corresponding to the input file text box is clicked.
        /// Creates an OpenFileDialog allowing to choose the desired path.
        /// </summary>
        /// <param name="sender">Reference to the event sender.</param>
        /// <param name="e">Reference to the event arguments.</param>
        private void inputFileBtn_Click(object sender, EventArgs e)
        {
            //create dialog to choose the input file
            OpenFileDialog inputFileDialog = new OpenFileDialog
            {
                //start dir = current project dir
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Choose json input for calculation",

                CheckFileExists = true,
                CheckPathExists = true,
                
                //look only for json files
                DefaultExt = "json",
                Filter = "json files (*.json)|*.json",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            
            //show the dialog
            if (inputFileDialog.ShowDialog() == DialogResult.OK)
            {
                //copy the input path from the dialog
                inputFileTextBox.Text = inputFileDialog.FileName;
            }
        }

        /// <summary>
        /// Action performed, when the "open..." button corresponding to the output file text box is clicked.
        /// Creates an OpenFileDialog allowing to choose the desired path.
        /// </summary>
        /// <param name="sender">Reference to the event sender.</param>
        /// <param name="e">Reference to the event arguments.</param>
        private void outputFileBtn_Click(object sender, EventArgs e)
        {
            //create dialog to choose the output file
            OpenFileDialog outputFileDialog = new OpenFileDialog
            {
                //start dir = current project dir
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Choose json output for calculation",

                CheckFileExists = false,
                CheckPathExists = true,

                //look only for json files
                DefaultExt = "json",
                Filter = "json files (*.json)|*.json",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            //show the dialog
            if (outputFileDialog.ShowDialog() == DialogResult.OK)
            {
                //copy the output path from the dialog
                outputFileTextBox.Text = outputFileDialog.FileName;
            }

        }

        /// <summary>
        /// Action that is performed, when the "Calculate" button is clicked.
        /// Input file is read, calculation is performed, result is saved to output file.
        /// </summary>
        /// <param name="sender">Reference to the event sender.</param>
        /// <param name="e">Reference to the event arguments.</param>
        private void calculateBtn_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;

            //disable button, so that it can not be clicked, while calculations are still performed
            btn.Enabled = false;

            loggerTextBox.Clear();

            //read i/o paths
            string inputPath = inputFileTextBox.Text;
            string outputPath = outputFileTextBox.Text;

            //read number of selected threads
            int threadNum = Int32.Parse(threadNumComboBox.Text);

            //set default library to ASM
            LibraryType lib = LibraryType.ASM;

            //check if C++ and Asm libraries could be loaded
            if (!ComplexNumUtils.areLibrariesPresent())
            {
                //show error dialog and break calculation
                alertLibsNotPresent();
                resetCalculationEvent(btn);
                return;
            }

            //check whether the c++ library is chosen
            if (cppRadioBtn.Checked)
                lib = LibraryType.CPP;

            loggerTextBox.AppendText("======================================\n");
            loggerTextBox.AppendText("Loading input data...\n");

            //buffer for file input
            string inputJson;

            //buffer destined input representation
            List<ComplexAlgebraic> inputs = null;

            //input file size in MB
            double fileLenMB = 0;

            try
            {   
                //check input file size in MB
                fileLenMB = new System.IO.FileInfo(inputPath).Length / 1000000.0;

                //check if the file is large
                if (fileLenMB > 20)
                {
                    //about 800 000 roots to calculate
                    //file is too large, to be processed
                    if (fileLenMB >= 30)
                    {
                        //show error dialog and break calculation
                        alertFileTooLarge();
                        resetCalculationEvent(btn);
                        return;
                    }

                    //show alert that the file is dangerously large
                    //prompt the user to decide, whether calculation shoul be continued or not
                    DialogResult result = alertFileSize();

                    //User chose not to continue the calculation
                    if (result == DialogResult.No)
                    {
                        //show error dialog and break calculation
                        resetCalculationEvent(btn);
                        return;
                    }

                }

                //read file data
                inputJson = File.ReadAllText(inputPath);

                //deserialize json data
                inputs = JsonConvert.DeserializeObject<List<ComplexAlgebraic>>(inputJson);
            }
            //catch exceptions which occur while deserializing input
            catch (System.ArgumentException)
            {
                //show error dialog and break calculation
                alertInvalidInputFile();
                resetCalculationEvent(btn);
                return;
            }
            catch (Newtonsoft.Json.JsonReaderException )
            {
                //show error dialog and break calculation
                alertInvalidJSON();
                resetCalculationEvent(btn);
                return;
            }
            catch (Newtonsoft.Json.JsonSerializationException)
            {
                //show error dialog and break calculation
                alertInvalidJSON();
                resetCalculationEvent(btn);
                return;
            }

            loggerTextBox.AppendText("Input data has been loaded successfully.\n");
            loggerTextBox.AppendText("Number of input entries: " + inputs.Count + "\n");
            loggerTextBox.AppendText("Input file size (MB): " + fileLenMB + "\n");

            //perform the calculations
            ResultsPresentation resultsPresentation = ComplexNumUtils.calculateRoots(
                inputs, threadNum, lib, loggerTextBox);

            loggerTextBox.AppendText("Saving results...\n");

            //serialize results
            string resultsJson = JsonConvert.SerializeObject(resultsPresentation, Formatting.Indented);

            //write results to file
            File.WriteAllText(outputPath, resultsJson);

            loggerTextBox.AppendText("Results have been saved to output file successfully.\n");
            loggerTextBox.AppendText("======================================\n");

            //wait until file is completely written and closed
            FileInfo fi = new FileInfo(outputPath);
            while (IsFileLocked(fi)) { }

            //enable button, make new calculation possible
            btn.Enabled = true;
        }

        /// <summary>
        /// Reset performed before breaking the calculation.
        /// </summary>
        /// <param name="btn">Reference to the calculation button</param>
        private void resetCalculationEvent(Button btn)
        {
            //enable button for next calculation
            btn.Enabled = true;

            //clear input file path
            inputFileTextBox.Text = "";

            //write approriate log
            loggerTextBox.AppendText("Calculation failed.\n");
            loggerTextBox.AppendText("======================================\n");
        }

        /// <summary>
        /// Show message box informing about malformed json input file.
        /// </summary>
        private void alertInvalidJSON()
        {
            MessageBox.Show("Input file is malformed. Please provide a valid one.", "Error");
        }

        /// <summary>
        /// Show message box informing about invalid input data stored in json file.
        /// </summary>
        private void alertInvalidInputFile()
        {
            MessageBox.Show("Input file is not valid.", "Error");
        }

        /// <summary>
        /// Show message box informing, that the input file is relatively large.
        /// Promt the user, whether to continue the calculation or not.
        /// </summary>
        /// <returns>Reference to the dialog result chosen by the user.</returns>
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

        /// <summary>
        /// Show message box informing, that the given input file is too large to be processed.
        /// </summary>
        private void alertFileTooLarge()
        {
            MessageBox.Show("Input file is too big.", "Error");
        }

        /// <summary>
        /// Show message box informing, that the required dll libraries could not be loaded.
        /// </summary>
        private void alertLibsNotPresent()
        {
            MessageBox.Show("ComplexRootLibCpp.dll or ComplexRootLibAsm.dll or Newtonsoft.Json.dll missing." +
                "Cannot perform calculation", "Error");
        }

        /// <summary>
        /// Check, whether the given file is still written to.
        /// </summary>
        /// <param name="file">Reference to the file's FileInfo</param>
        /// <returns>True, if the file is still written to. Returns false, when the file is free to use.</returns>
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
