using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class MainForm : Form
    {
        string DataOfMainFile;
        string LineWithCode = "", NewDataOfMainFileWithoutComments = "";

        public MainForm()
        {
            InitializeComponent();
        }
        string DoReadStringOfNewDataOfMainFileWithoutComments(ref int LengthOfStringOfDataOfMainFile, ref int AdditionalValueForCountSymbolsInString, ref int LengthOfDataOfMainFile)
        {
            LineWithCode = "";

            while ((LengthOfStringOfDataOfMainFile < NewDataOfMainFileWithoutComments.Length) && (NewDataOfMainFileWithoutComments[LengthOfStringOfDataOfMainFile] != '\n'))
            {
                LineWithCode += Convert.ToString(NewDataOfMainFileWithoutComments[LengthOfStringOfDataOfMainFile]);
                LengthOfStringOfDataOfMainFile++;
                AdditionalValueForCountSymbolsInString++;
            }

            LengthOfDataOfMainFile += AdditionalValueForCountSymbolsInString + 1;
            LengthOfStringOfDataOfMainFile++;
            AdditionalValueForCountSymbolsInString = 0;

            return LineWithCode;
        }
        void DoDeleteComments()
        {
            bool FlagOfStringComment = false;

            for (int i = 0; i < DataOfMainFile.Length; i++)
            {
                if ((DataOfMainFile[i] == '/') && (DataOfMainFile[i + 1] == '/'))
                    while (DataOfMainFile[i] != '\r')
                    {
                        i++;
                    }

                if ((DataOfMainFile[i] == '/') && (DataOfMainFile[i + 1] == '*'))
                {
                    while (FlagOfStringComment == false)
                    {
                        if ((DataOfMainFile[i] == '*') && (DataOfMainFile[i + 1] == '/'))
                            FlagOfStringComment = true;
                        i++;
                    }
                    i += 2;
                    FlagOfStringComment = false;
                }
                NewDataOfMainFileWithoutComments += Convert.ToString(DataOfMainFile[i]);
            }
        }
        void DoFindBlockOfFunctions(int CounterOfPositionInCode, ref int[,] ArrayOfPositionsProcedures, ref int CountOfProcedures, int LengthOfDataOfMainFile, int LengthOfStringOfDataOfMainFile)
        {
            int CountOfBracket = 0, AdditionalValueForCountSymbolsInString=0, StartPositionInStringOfCode=0, LengthOfStringOfCodeForCycle = 0, FinishPositionInStringOfCode=0;
            bool FLagOfBracket = false, FLagOfBlock = true;

            while (LengthOfDataOfMainFile < NewDataOfMainFileWithoutComments.Length)
            {
                while (FLagOfBlock)
                {
                    if (LengthOfStringOfDataOfMainFile < NewDataOfMainFileWithoutComments.Length)
                    {
                        LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref AdditionalValueForCountSymbolsInString, ref LengthOfDataOfMainFile);

                        CounterOfPositionInCode++;
                        LengthOfStringOfCodeForCycle = LineWithCode.Length;

                        for (int i = 1; i < LengthOfStringOfCodeForCycle; i++)
                        {
                            if ((LineWithCode[i - 1] == ')') && (LineWithCode[i] == '\r'))
                            {
                                FLagOfBlock = true;
                                FLagOfBracket = true;
                                StartPositionInStringOfCode = CounterOfPositionInCode;
                                ArrayOfPositionsProcedures[CountOfProcedures, 0] = StartPositionInStringOfCode;
                            }
                            else
                                FLagOfBlock = false;
                        }
                    }

                    LineWithCode = "";

                    while (((LengthOfDataOfMainFile != NewDataOfMainFileWithoutComments.Length) && (FLagOfBracket == true)))
                    {
                        if (LengthOfStringOfDataOfMainFile < NewDataOfMainFileWithoutComments.Length)
                        {
                            LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref AdditionalValueForCountSymbolsInString, ref LengthOfDataOfMainFile);

                            CounterOfPositionInCode++;
                            LengthOfStringOfCodeForCycle = LineWithCode.Length;

                            for (int i = 0; i < LengthOfStringOfCodeForCycle; i++)
                            {
                                if (Convert.ToChar(LineWithCode[i]) == '{')
                                    CountOfBracket++;
                                if (Convert.ToChar(LineWithCode[i]) == '}')
                                    CountOfBracket--;
                            }

                            LineWithCode = "";

                            if (CountOfBracket == 0)
                            {
                                FLagOfBracket = false;
                                FinishPositionInStringOfCode = CounterOfPositionInCode;
                                ArrayOfPositionsProcedures[CountOfProcedures, 1] = FinishPositionInStringOfCode;
                                CountOfProcedures++;
                            }
                        }
                    }
                    if (LengthOfDataOfMainFile >= NewDataOfMainFileWithoutComments.Length)
                        FLagOfBlock = false;
                    else
                        FLagOfBlock = true;
                }
            }
        }

        void DoFindGlobalValues(int LengthOfDataOfMainFile, int LengthOfStringOfDataOfMainFile, ref int CountOfGlobalVariable)
        {
            int LengthOfStringOfLineWithCode = 0, AdditionalValueForCountSymbolsInString=0;
            string StringOfPUBLICSTATIC = "public static";

            while (LengthOfDataOfMainFile < NewDataOfMainFileWithoutComments.Length)
            {
                LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref AdditionalValueForCountSymbolsInString, ref LengthOfDataOfMainFile);

                LengthOfStringOfLineWithCode = LineWithCode.Length;

                if (LineWithCode.IndexOf(StringOfPUBLICSTATIC) != -1)
                {
                    if (LineWithCode[LengthOfStringOfLineWithCode - 2] == ';')
                        CountOfGlobalVariable++;
                    LineWithCode = "";
                }
                else
                    LineWithCode = "";
            }
        }

        int DoViewAppelVariable(int Aup, int[,] ArrayOfPositionsProcedures, int CountOfProcedures)
        {
            int LengthOfStringOfLineWithCode = 0, StartPositionInStringOfCode = 0, FinishPositionInStringOfCode = 0, CounterOfFunctions = 0,
                k = 0, LengthOfStringOfDataOfMainFile = 0, AdditionalValueForCountSymbolsInString = 0, LengthOfDataOfMainFile = 0;
            string NameOfVariable = "";
            bool FlagOfReallyAppealToVariable = true;

            Aup = 0;
            LengthOfStringOfLineWithCode = LineWithCode.Length;

            for (int i = LengthOfStringOfLineWithCode - 1; i > 0; i--)
            {
                if (LineWithCode[i] == '=')
                {
                    FinishPositionInStringOfCode = i - 2;
                    break;
                }
            }
            for (int i = FinishPositionInStringOfCode; i > 0; i--)
            {
                if (LineWithCode[i] == ' ')
                {
                    StartPositionInStringOfCode = i + 1;
                    break;
                }
            }

            int LengthOfVariable = FinishPositionInStringOfCode - StartPositionInStringOfCode + 2;

            for (int i = 0; i < LengthOfVariable; i++)
            {
                if (LineWithCode[StartPositionInStringOfCode + i] == ' ')
                    break;
                NameOfVariable += LineWithCode[StartPositionInStringOfCode + i];
            }

            while (CounterOfFunctions < CountOfProcedures)
            {
                FlagOfReallyAppealToVariable = true;

                for (k = 0; k < ArrayOfPositionsProcedures[CounterOfFunctions, 0]; k++)
                {
                    LineWithCode = "";
                    LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref AdditionalValueForCountSymbolsInString, ref LengthOfDataOfMainFile);
                }

                while ((LengthOfDataOfMainFile < NewDataOfMainFileWithoutComments.Length) && (k < ArrayOfPositionsProcedures[CounterOfFunctions, 1]) && (FlagOfReallyAppealToVariable == true))
                {
                    if (LineWithCode.IndexOf(NameOfVariable) != -1)
                    {
                        Aup++;
                        FlagOfReallyAppealToVariable = false;
                    }

                    LineWithCode = "";
                    k++;

                    LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref AdditionalValueForCountSymbolsInString, ref LengthOfDataOfMainFile);
                }

                LengthOfDataOfMainFile = 0;
                LengthOfStringOfDataOfMainFile = 0;
                CounterOfFunctions++;
            }
            return Aup;
        }
        int DoFindCountOfAppealToGlobalVariable(int Aup, ref int[,] ArrayOfPositionsProcedures, int CountOfProcedures, int CountOfGlobalVariable, int CounterOfPositionInCode, int LengthOfDataOfMainFile, int LengthOfStringOfDataOfMainFile)
        {
            int CountOfViewedVariables = 0, LengthOfStringOfLineWithCode = 0, AdditionalValueForCountSymbolsInString=0;
            bool FlagOfTheLastPlaceGlobalVariable = true;
            string StringOfPUBLICSTATIC = "public static";

            while ((LengthOfDataOfMainFile < NewDataOfMainFileWithoutComments.Length) && (CountOfGlobalVariable > CountOfViewedVariables))
            {

                if (FlagOfTheLastPlaceGlobalVariable == true)
                {
                    for (int i = 0; i < CounterOfPositionInCode; i++)
                    {
                        LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref AdditionalValueForCountSymbolsInString, ref LengthOfDataOfMainFile);
                        LineWithCode = "";
                    }
                }

                FlagOfTheLastPlaceGlobalVariable = false;

                LineWithCode = DoReadStringOfNewDataOfMainFileWithoutComments(ref LengthOfStringOfDataOfMainFile, ref AdditionalValueForCountSymbolsInString, ref LengthOfDataOfMainFile);

                CounterOfPositionInCode++;
                LengthOfStringOfLineWithCode = LineWithCode.Length;

                if (LineWithCode.IndexOf(StringOfPUBLICSTATIC) != -1)
                {
                    if (LineWithCode[LengthOfStringOfLineWithCode - 2] == ';')
                    {
                        CountOfViewedVariables++;
                        Aup += DoViewAppelVariable(Aup, ArrayOfPositionsProcedures, CountOfProcedures);
                        FlagOfTheLastPlaceGlobalVariable = true;
                        LengthOfStringOfDataOfMainFile = 0;
                        LengthOfDataOfMainFile = 0;
                    }
                }
                LineWithCode = "";
            }
            return Aup;
        }

        private void openFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MainCodeTextBox.Text = "";
                FinalResultsTextBox.Text = "";
                NewDataOfMainFileWithoutComments = "";
                DataOfMainFile = "";
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName, Encoding.Default);
                DataOfMainFile = File.ReadAllText((openFileDialog1.FileName));

                MainCodeTextBox.Text = sr.ReadToEnd();
                sr.Close();

                if (DataOfMainFile.Length != 0)
                    ButtonOfProceed.Enabled = true;
            }
        }

        private void ButtonOfProceed_Click(object sender, EventArgs e)
        {

            int[,] ArrayOfPositionsProcedures = new int[100, 2];
            int CountOfGlobalVariable = 0,  CounterOfPositionInCode = 0, CountOfProcedures = 0, LengthOfDataOfMainFile = 0, LengthOfStringOfDataOfMainFile = 0;
            int Aup = 0, Pup=0;
            float Rup = 0;
            ButtonOfProceed.Enabled = false;
            DoDeleteComments();

            DoFindBlockOfFunctions(CounterOfPositionInCode, ref ArrayOfPositionsProcedures, ref CountOfProcedures, LengthOfDataOfMainFile, LengthOfStringOfDataOfMainFile);

            DoFindGlobalValues(LengthOfDataOfMainFile, LengthOfStringOfDataOfMainFile, ref CountOfGlobalVariable);

            Aup = DoFindCountOfAppealToGlobalVariable(Aup, ref ArrayOfPositionsProcedures, CountOfProcedures, CountOfGlobalVariable, CounterOfPositionInCode, LengthOfDataOfMainFile, LengthOfStringOfDataOfMainFile);

            Pup = CountOfGlobalVariable * CountOfProcedures;
            Rup = (float)Aup / (float)Pup;

            FinalResultsTextBox.Text = "The count global variable =" + Convert.ToString(CountOfGlobalVariable) + '\r' + '\n';
            FinalResultsTextBox.Text += "The value Aup = " + Convert.ToString(Aup) + '\r' + '\n';
            FinalResultsTextBox.Text += "The value Pup = " + Convert.ToString(Pup) + '\r' + '\n';
            FinalResultsTextBox.Text += "The value Rup = Aup/Pup = " + Convert.ToString(Rup) + '\r' + '\n';
        }
    }
}
