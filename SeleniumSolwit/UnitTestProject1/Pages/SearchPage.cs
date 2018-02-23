using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA_SeleniumTestProject.Pages
{
    class SearchPage:BasicPage
    {
        public SearchPage(IWebDriver driver) : base(driver) { }
        public void ClickTopLink()
        {
            var link = Driver.FindElement(By.XPath("//*[@href='https://www.ultimateqa.com/complicated-page/']"));
            link.Click();
        }
    }
}
