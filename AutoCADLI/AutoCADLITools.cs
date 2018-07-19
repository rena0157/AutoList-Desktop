using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AutoCADLI
{
    public class AutoCadliTools
    {
        private static readonly Regex PolylineRegex = new Regex(@"length\s*\d*\.\d{1-4}");
        private static readonly Regex LineRegex;
        public static Regex HatchRegex;

        /// <summary>
        /// Method used to extract objects from a string list which is based on the
        /// output of an AutoCAD List command
        /// </summary>
        /// <param name="textList">The AutoCAD List command text broken up into
        /// a string list</param>
        /// <param name="obj">The objects that you would like to extract</param>
        /// <returns>a list of lengths for lines or a list of areas for hatches</returns>
        public static List<double> ExtractObjects(IEnumerable<string> textList,
            ExtractionObject obj)
        {
            var returnList = new List<double>();

            switch (obj)
            {
                case ExtractionObject.PolylinesAndLines:
                {
                    foreach (var thisLine in textList)
                    {
                        if (PolylineRegex.IsMatch(thisLine))
                        {
                            Debug.WriteLine("This Line: " + thisLine);
                        }
                        else if (LineRegex.IsMatch(thisLine))
                        {
                            
                        }
                    }
                }break;

                case ExtractionObject.Hatches:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }

            return returnList;
        }

        public static string[] ParseText(string inputText)
        {
            return inputText.Split(new[] {Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);
        }
    }


    public enum ExtractionObject
    {
        PolylinesAndLines,
        Hatches,
    }
}
