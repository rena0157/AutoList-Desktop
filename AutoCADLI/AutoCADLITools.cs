// AutoCADLITools.cs
// By: Adam Renaud
// Created: July, 2018
// Purpose: to house the AutoCADLITools class that holds the functionality 
// for extracting data from List strings from AutoCAD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoCADLI
{
    public static class AutoCadliTools
    {
        // Regex presets
        private static readonly Regex PolylineRegex = new Regex(@"length\s*\d*\.\d*");
        private static readonly Regex LineRegex = new Regex(@"Length\s*=\s*\d*\.\d*");
        private static readonly Regex HatchRegex = new Regex(@"^\s*Area\s*\d*\.\d*");

        /// <summary>
        ///     Method used to extract objects from a string list which is based on the
        ///     output of an AutoCAD List command
        /// </summary>
        /// <remarks>
        ///     Note that the properties that are extracted from the object are as follows:
        ///     From Polylines and lines lengths are extracted,
        ///     From Hatches areas are extracted,
        ///     From blocks polylines, lines and hatches are all extracted and placed into groups
        ///     for the exporting to a CSV file.
        /// </remarks>
        /// <param name="textList">
        ///     The AutoCAD List command text broken up into
        ///     a string list
        /// </param>
        /// <param name="obj">The objects that you would like to extract</param>
        /// <returns>a list of lengths for lines or a list of areas for hatches</returns>
        public static List<double> ExtractObjects(IEnumerable<string> textList,
            ExtractionObject obj)
        {
            var returnList = new List<double>();

            // Switch on the object type that is being extracted.
            switch (obj)
            {
                case ExtractionObject.PolylinesAndLines:
                {
                    // Go through each line, parse the text from a line that
                    // matches a regex and then parse that line. When the line is parsed
                    // then place the property into a list 
                    foreach (var thisLine in textList)
                        if (PolylineRegex.IsMatch(thisLine))
                            returnList.Add(double.Parse(ParsePolylineText(thisLine)));
                        else if (LineRegex.IsMatch(thisLine))
                            returnList.Add(double.Parse(ParseLineText(thisLine)));
                }
                    break;

                case ExtractionObject.Hatches:
                    // Parses the line and converts it to a double, then adds it to 
                    // the return list
                    returnList.AddRange(
                        from thisLine in textList
                        where HatchRegex.IsMatch(thisLine)
                        select double.Parse(ParseHatchText(thisLine)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }

            return returnList;
        }

        /// <summary>
        ///     This function is used to take a single string of text and separate it
        ///     by newlines and return an array of strings.
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public static string[] SplitByNewline(string inputText)
        {
            return inputText.Split(new[] {Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);
        }

        // Used to parse the text taken from the line of the LI text output - for Polylines
        private static string ParsePolylineText(string inputText)
        {
            return inputText.Trim('l', 'e', 'n', 'g', 'h', 't', ' ');
        }

        // Used to parse the text taken from the line of the LI text output - for Lines
        private static string ParseLineText(string inputText)
        {
            var tempString = inputText.Split(',')[0];
            return tempString.Trim('L', 'e', 'n', 'g', 'h', 't', '=', ' ');
        }

        // parses the text from Hatch li text output
        private static string ParseHatchText(string inputText)
        {
            return inputText.Trim(' ', 'A', 'a', 'r', 'e');
        }
    }

    /// <summary>
    ///     All of the object that can be extracted from the LI text box
    /// </summary>
    public enum ExtractionObject
    {
        PolylinesAndLines,
        Hatches
    }
}