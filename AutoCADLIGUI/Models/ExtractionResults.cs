// AutoCADLIGUI
// ExtractionResults.cs
// 
// ============================================================
// 
// Created: 2018-07-22
// Last Updated: 2018-10-28-12:22 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using AutoList;
using Microsoft.Win32;

namespace AutoCADLIGUI.Models
{
    /// <summary>
    ///     A Class that is used to abstract extraction results from
    ///     the AutoCAD LI tools library
    /// </summary>
    public class ExtractionResults
    {
        private const double M2ToHa = 0.0001;
        private readonly string _text;

        /// <summary>
        ///     Extraction results object constructor that
        ///     Contains all of the properties of Polylines, Lines and Hatches
        /// </summary>
        /// <param name="inputText">String from an AutoCADLI source</param>
        public ExtractionResults(string inputText)
        {
            _text = inputText;

            // Extract the Polylines and Lines
            PolyLinesLines = AutoList.AutoList
                .GetDouble(inputText, AutoListPatterns.LinesLengthPattern);

            // Extract the Hatches
            Hatches = AutoList.AutoList
                .GetDouble(inputText, AutoListPatterns.HatchAreaPattern);

            // The total length of all polylines and lines
            TotalLength = PolyLinesLines.Sum();

            // The total Area of all Hatches and convert them to ha
            TotalArea = Hatches.Sum() * M2ToHa;
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
        ///     Writes the blocks information to a CSV file then opens excel.
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

            // Get a csv string from the AutoList Library
            var csvString = AutoList.AutoList.GetBlocks(_text);

            // Write to file and handle any writing exceptions
            try
            {
                File.WriteAllText(sfDialog.FileName, csvString);
            }
            catch ( IOException e )
            {
                MessageBox.Show("Error: " + e.Message, "Save as Error", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }
            catch ( Exception e )
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
                return true;
            }

            return true;
        }
    }
}