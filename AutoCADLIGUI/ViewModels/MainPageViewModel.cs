using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoCADLIGUI.Framework;
using AutoCADLIGUI.Models;

namespace AutoCADLIGUI.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        // Extraction Results from the AutoCAD LI Tools toolkit
        private ExtractionResults _extractionResults;

        // Text string that is binded to the textbox
        private string _text;

        // Checkbox bools
        private bool _extractPolylinesAndLines;
        private bool _extractHatches;
        private bool _extractBlocks;

        /// <summary>
        /// Text from the Textbox
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
        /// Checkbox IsChecked Property
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
        /// Checkbox IsChecked Property for Extract Hatches Checkbox
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
        /// Checkbox IsChecked Property for Extract Blocks Checkbox
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
        /// ICommand for the extract data command
        /// </summary>
        public ICommand ExtractDataCommand => new DelegateCommand(ExtractData);

        // Executes the Extract Data command
        private void ExtractData()
        {
            // Error handling
            if (Text == null)
            {
                MessageBox.Show("The text box is empty. Please place something in" +
                                "the text box to be Parsed.", "Nothing to Extract", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                return;
            }

            if (!ExtractBlocks && !ExtractHatches && !ExtractPolylinesAndLines)
            {
                MessageBox.Show("You have not selected anything to be extracted. Please " +
                                "Select the objects that you would like to extract", 
                                "Object Selection Error", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Information);
            }

            // Create new _extraction results
            var extractionResults = new ExtractionResults(_text);

            // Calculate the total Objects extracted
            var totalObjectsExtracted
                = extractionResults.PolyLinesLines.Count + extractionResults.Hatches.Count;

            // The base string that is to be printed to the user
            string baseString = $"Total Objects Extracted: {totalObjectsExtracted}\n" +
                                $"Lines and Polylines: {extractionResults.PolyLinesLines.Count}\n" +
                                $"Hatches: {extractionResults.Hatches.Count}\n\n";
            if (ExtractBlocks)
            {

            }
            else if (ExtractPolylinesAndLines && ExtractHatches)
            {
                Text = baseString + $"Total Length: {extractionResults.TotalLength}m\n" +
                        $"Total Area: {extractionResults.TotalArea} ha\n";
            }
            else if (ExtractPolylinesAndLines && !ExtractHatches)
            {
                Text = baseString + $"Total Length:  {extractionResults.TotalLength}m\n";
                Clipboard.SetText(extractionResults.TotalLength.ToString(CultureInfo.CurrentCulture));
            }
            else if (!ExtractPolylinesAndLines && ExtractHatches)
            {
                
            }
        }
    }
}
