using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Chrome;
using SPA_SeleniumTestProject.Pages;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace SPA_SeleniumTestProject
{
    /// <summary>
    /// Summary description for GuiTests
    /// </summary>
    [TestClass]
    public class GuiTests
    {
        private static IWebDriver _driver;
        //public Settings settings = new Settings();        
        public static Settings settings;

        [TestInitialize]
        public void TestSetup()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _driver = new ChromeDriver(outPutDirectory);
            settings = new Settings();
        }

        [TestMethod]
        public void logInPage()
        {
            var mainPage = new MainPage(_driver);
            mainPage.Search(settings.loginPopupText);
        }


        [TestCleanup]
        public void CleanUp()
        {
            _driver.Close();
            _driver.Quit();
        }
    }
}
