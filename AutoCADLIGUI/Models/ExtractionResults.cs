using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using AutoCADLI;
using Microsoft.Win32;

namespace AutoCADLIGUI.Models
{
    /// <summary>
    ///     A Class that is used to abstract extraction results from
    ///     the AutoCAD LI tools library
    /// </summary>
    public class ExtractionResults
    {
        /// <summary>
        ///     Extraction results object constructor that
        ///     Contains all of the properties of Polylines, Lines and Hatches
        /// </summary>
        /// <param name="inputText">String from an AutoCADLI source</param>
        public ExtractionResults(string inputText)
        {
            // Convert the input text into a string list
            var stringArray = AutoCadliTools.SplitByNewline(inputText);

            // Extract the Polylines and Lines
            PolyLinesLines = AutoCadliTools.ExtractObjects(
                stringArray, ExtractionObject.PolylinesAndLines);

            // Extract the Hatches
            Hatches = AutoCadliTools.ExtractObjects(
                stringArray, ExtractionObject.Hatches);

            // The total length of all polylines and lines
            TotalLength = PolyLinesLines.Sum();

            // The total Area of all Hatches
            TotalArea = MathTools.Convert(Hatches.Sum(), Conversions.M2Ha);
        }


        /// <summary>
        ///     All of the lines and polylines lengths
        /// </summary>
        public List<double> PolyLinesLines { get; }

        /// <summary>
        ///     All of the Hatches areas in meters squared
        /// </summary>
        public List<double> Hatches { get; }

        /// <summary>
        ///     The total Length of all Hatches
        /// </summary>
        public double TotalLength { get; }

        /// <summary>
        ///     The Total Area of all Hatches
        /// </summary>
        public double TotalArea { get; }

        /// <summary>
        ///     Writes the blocks information to a CSV file
        /// </summary>
        /// <returns>If the write was successful or not</returns>
        public bool OutputBlocksToCsv()
        {
            var sfDialog = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                OverwritePrompt = true,
                Filter = "Comma Separated CSV|*.csv",
                Title = "Save CSV File"
            };
            sfDialog.ShowDialog();

            // The string builder that will hold all of the information
            // that will be written to the CSV file
            var stringBuilder = new StringBuilder();

            // Adding the headers to the CSV file
            stringBuilder.AppendLine("Block Number,Polyline/Line Length, Hatch Area");

            // Appending to the string builder - Note that the area is converted to hectares
            for (var lineIndex = 0; lineIndex < Math.Max(PolyLinesLines.Count, Hatches.Count); ++lineIndex)
                if (lineIndex >= PolyLinesLines.Count)
                    stringBuilder.AppendLine($"{lineIndex + 1},"
                                             + ","
                                             + $"{MathTools.Convert(Hatches[lineIndex], Conversions.M2Ha)}");
                else if (lineIndex >= Hatches.Count)
                    stringBuilder.AppendLine($"{lineIndex + 1}," +
                                             $"{PolyLinesLines[lineIndex]}," +
                                             "");
                else
                    stringBuilder.AppendLine($"{lineIndex + 1}," +
                                             $"{PolyLinesLines[lineIndex]}," +
                                             $"{MathTools.Convert(Hatches[lineIndex], Conversions.M2Ha)}");

            // Write to file and handle any writing exceptions
            try
            {
                File.WriteAllText(sfDialog.FileName, stringBuilder.ToString());
            }
            catch (IOException e)
            {
                MessageBox.Show("Error: " + e.Message, "Save as Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while writing file: " + e.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Open Excel on the current file.
            var excelProcess = new Process
            {
                StartInfo = {FileName = "excel.exe", Arguments = sfDialog.FileName}
            };

            // Try to open the excel file
            try
            {
                excelProcess.Start();
            }
            catch ( Exception e )
            {
                MessageBox.Show("Error while trying to open Excel: " + e.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return true;
        }
    }
}