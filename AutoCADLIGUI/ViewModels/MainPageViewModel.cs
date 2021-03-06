﻿// AutoCADLIGUI
// MainPageViewModel.cs
// 
// ============================================================
// 
// Created: 2018-07-22
// Last Updated: 2018-07-28-3:34 PM
// By: Adam Renaud
// 
// ============================================================
// 
// Purpose:

using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using AutoCADLIGUI.Framework;
using AutoCADLIGUI.Models;
using Microsoft.Win32;

namespace AutoCADLIGUI.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        //
        private string _browseText;
        // Property Elements

        private bool _extractBlocks;
        private bool _extractHatches;

        // Checkbox switches
        private bool _extractPolylinesAndLines;

        // Text string that is bound to the text box
        private string _text;

        /// <summary>
        ///     Text from the Text box
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                RaisePropertyChangedEvent("Text");
            }
        }

        /// <summary>
        ///     Checkbox IsChecked Property
        /// </summary>
        public bool ExtractPolylinesAndLines
        {
            get => _extractPolylinesAndLines;
            set
            {
                _extractPolylinesAndLines = value;
                RaisePropertyChangedEvent("ExtractPolylinesAndLines");
            }
        }

        /// <summary>
        ///     Checkbox IsChecked Property for Extract Hatches Checkbox
        /// </summary>
        public bool ExtractHatches
        {
            get => _extractHatches;
            set
            {
                _extractHatches = value;
                RaisePropertyChangedEvent("ExtractHatches");
            }
        }

        /// <summary>
        ///     Checkbox IsChecked Property for Extract Blocks Checkbox
        /// </summary>
        public bool ExtractBlocks
        {
            get => _extractBlocks;
            set
            {
                _extractBlocks = value;
                ExtractHatches = value;
                ExtractPolylinesAndLines = value;
                RaisePropertyChangedEvent("ExtractBlocks");
            }
        }

        /// <summary>
        ///     The string from the BrowseText box
        /// </summary>
        public string BrowseText
        {
            get => _browseText;
            set
            {
                _browseText = value;
                RaisePropertyChangedEvent("BrowseText");
            }
        }

        // Commands

        /// <summary>
        ///     ICommand for the extract data command
        /// </summary>
        public ICommand ExtractDataCommand => new DelegateCommand(ExtractData);

        /// <summary>
        ///     Opens a file dialog to select a file and then copies that files contents to the text box for extraction
        /// </summary>
        public ICommand BrowseButtonCommand => new DelegateCommand(GetTextFromFile);

        /// <summary>
        ///     <see cref="BrowseButtonCommand" />
        /// </summary>
        private void GetTextFromFile()
        {
            try
            {
                // Open the file dialog for text files and save the contents 
                // of the file into the Text box on screen
                var opnFileDialog = new OpenFileDialog
                {
                    CheckPathExists = true,
                    Filter = "Text Files|*.txt"
                };
                opnFileDialog.ShowDialog();
                BrowseText = opnFileDialog.FileName;
                Text = File.ReadAllText(opnFileDialog.FileName);
            }
            catch (IOException exception)
            {
                MessageBox.Show("Error: " + exception.Message,
                    "Read Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (ArgumentException)
            {
                // Do nothing... no file was chosen the user canceled
            }
        }

        /// <summary>
        ///     Runs all of the back end required to extract the data from the string
        /// </summary>
        private void ExtractData()
        {
            // Error handling - No string to parse
            if (Text == null)
            {
                MessageBox.Show("The text box is empty. Please place something in" +
                                "the text box to be Parsed.", "Nothing to Extract",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            // Error handling - Nothing selected
            if (!ExtractBlocks && !ExtractHatches && !ExtractPolylinesAndLines)
                MessageBox.Show("You have not selected anything to be extracted. Please " +
                                "Select the objects that you would like to extract",
                    "Object Selection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

            // Create new _extraction results
            var extractionResults = new ExtractionResults(_text);

            // Calculate the total Objects extracted
            var totalObjectsExtracted
                = extractionResults.PolyLinesLines.Count + extractionResults.Hatches.Count;

            // The base string that is to be printed to the user
            var baseString = $"Total Objects Extracted: {totalObjectsExtracted}\n" +
                             $"Lines and Polylines: {extractionResults.PolyLinesLines.Count}\n" +
                             $"Hatches: {extractionResults.Hatches.Count}\n\n";

            // Blocks
            if (ExtractBlocks)
            {
                if (extractionResults.OutputBlocksToCsv())
                    Text = "Extraction Successful";
            }

            // Polylines, Lines and Hatches
            else if (ExtractPolylinesAndLines && ExtractHatches)
            {
                Text = baseString + $"Total Length: {extractionResults.TotalLength}m\n" +
                       $"Total Area: {extractionResults.TotalArea} ha\n";
            }

            // Polylines and Lines - Copies the total length to the clipboard
            else if (ExtractPolylinesAndLines && !ExtractHatches)
            {
                Text = baseString + $"Total Length:  {extractionResults.TotalLength}m\n";
                Clipboard.SetText(extractionResults.TotalLength.ToString(CultureInfo.CurrentCulture));
            }

            // Hatches - Copies the total length to the clipboard
            else if (!ExtractPolylinesAndLines && ExtractHatches)
            {
                Text = baseString + $"Total Area: {extractionResults.TotalArea}ha\n";
                Clipboard.SetText(extractionResults.TotalArea.ToString(CultureInfo.CurrentCulture));
            }
        }
    }
}