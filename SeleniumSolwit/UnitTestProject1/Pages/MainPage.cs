using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Pages
{
    class MainPage:BasicPage
    {
        public MainPage(IWebDriver driver) : base(driver)
        {
            driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com/");
        }

        public SearchPage Search(string text)
        {
            var searchButton = Driver.FindElement(By.XPath("//*[@id='et_top_search']"));
            searchButton.Click();
            var searchField = Driver.FindElement(By.ClassName("et-search-field"));
            searchField.Clear();
            searchField.SendKeys(text + "\n");
            return new SearchPage(Driver);
        }
    }
}
