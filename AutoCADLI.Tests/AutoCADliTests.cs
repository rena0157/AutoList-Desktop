// AutoCADLI.Tests
// AutoCADliTests.cs
// 
// ============================================================
// 
// Created: 2018-07-22
// Last Updated: 2018-08-13-7:29 PM
// By: Adam Renaud
// 
// ============================================================

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
            using ( var sr = File.OpenText(path) )
            {
                string line;
                while ( ( line = sr.ReadLine() ) != null ) returnList.Add(line);
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
            foreach ( var obj in testExtractionPolylines ) totalLength += obj;
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
            foreach ( var area in testHatchExtraction ) totalArea += area;
            Assert.IsTrue(Math.Abs(MathTools.Convert(totalArea, Conversions.M2Ha) - 1.64679315) < 0.0001,
                "Total Area: " + totalArea);

#if false
            foreach (var listContents in testHatchExtraction)
            {
                Debug.WriteLine(listContents);
            }
            #endif
        }

        [TestMethod]
        public void ArcExtractionTest()
        {
            var stringList = ReadLiFile(@"C:\Dev\AutoCADLIGUI\AutoCADLI.Tests\TestFiles\ArcsTest.txt");
            var arcTestExtraction = AutoCadliTools.ExtractObjects(stringList, ExtractionObject.PolylinesAndLines);

            var totalLength = 0.0;
            foreach ( var item in arcTestExtraction ) totalLength += item;
            Assert.IsTrue(Math.Abs(totalLength - 2.318) < 0.001);
        }

        [TestMethod]
        public void Issue1Test_CausingException()
        {
            // Arrange
            const string pathToFile = @"C:\Dev\AutoCADLIGUI\AutoCADLI.Tests\TestFiles\Issue1FailingText.txt";
            var stringList = ReadLiFile(pathToFile);

            // Act
            var testExtraction = AutoCadliTools.ExtractObjects(stringList, ExtractionObject.PolylinesAndLines);
        }

        [TestMethod]
        public void ThreeDLengthTest()
        {
            // Arrange
            const string pathToFile = @"C:\Dev\AutoCADLIGUI\AutoCADLI.Tests\TestFiles\3DLength.txt";

            // Act
            var stringList = ReadLiFile(pathToFile);
            var testExtraction = AutoCadliTools.ExtractObjects(stringList, ExtractionObject.PolylinesAndLines);
            var numberOfItems = testExtraction.Count;

            // Assert
            Assert.IsTrue(numberOfItems == 1);
            Assert.IsTrue(Math.Abs(testExtraction[0] - 18.8941) < 0.01);
        }
    }
}