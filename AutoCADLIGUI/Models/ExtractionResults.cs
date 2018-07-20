using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoCADLI;

namespace AutoCADLIGUI.Models
{
    /// <summary>
    /// A Class that is used to abstract extraction results from
    /// the AutoCAD LI tools library
    /// </summary>
    public class ExtractionResults
    {
        /// <summary>
        /// Extraction results object constructor that
        /// Contains all of the properties of Polylines, Lines and Hatches
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
        /// All of the lines and polylines lengths
        /// </summary>
        public List<double> PolyLinesLines { get; }
        
        /// <summary>
        /// All of the Hatches areas in meters squared
        /// </summary>
        public List<double> Hatches { get; }
        
        /// <summary>
        /// The total Length of all Hatches
        /// </summary>
        public double TotalLength { get; }

        /// <summary>
        /// The Total Area of all Hatches
        /// </summary>
        public double TotalArea { get; }
    }
}
