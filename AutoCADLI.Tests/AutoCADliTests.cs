
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoCADLI;

namespace AutoCADLI.Tests
{
    [TestClass]
    public class AutoCADLiToolsTests
    {
        private string _pathToFile = @"C:\Dev\AutoCADLIGUI\AutoCADLI.Tests\TestFiles\ExtractionTest.txt";

        [TestMethod]
        public void ExtractObjectsTests()
        {
            var stringList = new List<string>();
            using (var sr = File.OpenText(_pathToFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    stringList.Add(line);
                }
            }

            var test = AutoCadliTools.ExtractObjects(stringList, ExtractionObject.PolylinesAndLines);

            #if false
            foreach (var line in stringList)
            {
                Debug.WriteLine(line);
            }
            #endif


        }
    }
}
