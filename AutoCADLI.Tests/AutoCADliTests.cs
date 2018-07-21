using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoCADLI.Tests
{
    [TestClass]
    public class AutoCADLiToolsTests
    {
        private const string PolylineLineTestFile
            = @"C:\Dev\AutoCADLIGUI\AutoCADLI.Tests\TestFiles\PolylineTests.txt";

        private const string HatchTestFile
            = @"C:\Dev\AutoCADLIGUI\AutoCADLI.Tests\TestFiles\HatchTest.txt";

        private static List<string> ReadLiFile(string path)
        {
            var returnList = new List<string>();
            // extract the information from the text file.
            using (var sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null) returnList.Add(line);
            }

            return returnList;
        }

        [TestMethod]
        public void PolylineLineExtractionTests()
        {
            var stringList = ReadLiFile(PolylineLineTestFile);
            // testing extraction of Polylines and Lines
            var testExtractionPolylines = AutoCadliTools.ExtractObjects(stringList,
                ExtractionObject.PolylinesAndLines);

            // Total length tests
            var totalLength = 0.0;
            foreach (var obj in testExtractionPolylines) totalLength += obj;
            // Assertion test for length extraction
            Assert.IsTrue(Math.Abs(totalLength - 693.7963) < 0.0001);


            // Printing of all the lines in the stringList
            #if false
            foreach (var line in stringList)
            {
                Debug.WriteLine(line);
            }
            #endif

            // Printing of all of the lengths in the polylines and lines list
            #if false
            foreach (var extractedObj in testExtractionPolylines)
            {
                Debug.WriteLine(extractedObj);
            }
            #endif

            // testing extraction of hatches
            var testExtractionHatches = AutoCadliTools.ExtractObjects(stringList,
                ExtractionObject.Hatches);
        }

        [TestMethod]
        public void HatchExtractionTests()
        {
            var stringList = ReadLiFile(HatchTestFile);
            // Run the extraction code
            var testHatchExtraction = AutoCadliTools.ExtractObjects(stringList,
                ExtractionObject.Hatches);

            var totalArea = 0.0;
            foreach (var area in testHatchExtraction) totalArea += area;
            Assert.IsTrue(Math.Abs(MathTools.Convert(totalArea, Conversions.M2Ha) - 1.64679315) < 0.0001,
                "Total Area: " + totalArea);

            #if false
            foreach (var listContents in testHatchExtraction)
            {
                Debug.WriteLine(listContents);
            }
            #endif
        }
    }
}