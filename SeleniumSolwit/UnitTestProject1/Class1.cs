using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Chrome;
using SPA_SeleniumTestProject.Pages;
using OpenQA.Selenium;

namespace SPA_SeleniumTestProject
{
    [TestClass]
    class UltimateQaTest
    {
        private IWebDriver _driver;


        [TestInitialize]
        public void TestSetup()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _driver = new ChromeDriver(outPutDirectory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Close();
            _driver.Quit();
        }

        [TestMethod]
        public void ComplicatedPageSearch()
        {
            var mainPage = new MainPage(_driver);
            mainPage.Search("complicated page");
        }
    }
}
