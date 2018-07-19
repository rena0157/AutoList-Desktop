using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoCADLIGUI;
namespace AutoCADLIGUI.Tests
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testWindow = new MainWindow();
            Debug.WriteLine("Window Opened");
        }
    }
}
