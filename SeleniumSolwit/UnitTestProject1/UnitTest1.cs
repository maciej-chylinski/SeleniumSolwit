using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SPA_SeleniumTestProject.Pages;

namespace SPA_SeleniumTestProject
{
    [TestClass]
    public class UnitTest1
    {
        static IWebDriver driver;

        [TestInitialize]
        public void TestSetup()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            driver = new ChromeDriver(outPutDirectory);
        }

        [TestMethod]
        public void TestMethod1()
        {
            driver.Navigate().GoToUrl("https://www.qtptutorial.net/automation-practice/");

            var elem = driver.FindElement(By.Id("idExample"));
            elem.Click();
            driver.Navigate().Back();
        }

        [TestMethod]
        public void Navigate()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/");
            Assert.AreEqual("Home - Ultimate QA", driver.Title);
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation/");
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);
            var elem = driver.FindElement(By.XPath("//*[@href='/complicated-page']"));
            elem.Click();
            Assert.AreEqual("Complicated Page - Ultimate QA", driver.Title);
            driver.Navigate().Back();
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);
        }

        [TestMethod]
        public void FillingForms()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");
            var elem = driver.FindElements(By.Id("et_pb_contact_name_1"));
            elem[1].Clear();
            elem[1].SendKeys("moje imie");

            elem = driver.FindElements(By.Id("et_pb_contact_message_1"));
            elem[1].Clear();
            elem[1].SendKeys("bardzo dużo tekstu, nudy, nudy, nudy. Jak w polskim kinie");

            var elem2 = driver.FindElement(By.ClassName("et_pb_contact_captcha_question"));
            var numbers = elem2.Text.Split('+');

            var sum = Convert.ToInt32(numbers[0]) + Convert.ToInt32(numbers[1]);

            elem2 = driver.FindElement(By.ClassName("et_pb_contact_captcha"));
            elem2.Clear();
            elem2.SendKeys(Convert.ToString(sum));

            elem = driver.FindElements(By.ClassName("et_pb_contact_submit"));
            elem[1].Submit();

            elem2 = driver.FindElement(By.XPath("//*[@class='et-pb-contact-message']/p"));
            Assert.AreEqual("Success", elem2.Text);
        }

        [TestMethod]
        public void ElementInterrogation()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/simple-html-elements-for-automation/");

            var elem = driver.FindElement(By.Id("button1"));
            Assert.AreEqual("submit", elem.GetAttribute("type"));
            Assert.AreEqual("normal", elem.GetCssValue("letter-spacing"));
            Assert.AreEqual(true, elem.Displayed);
            Assert.AreEqual(true, elem.Enabled);
            Assert.AreEqual(false, elem.Selected);
            Assert.AreEqual("Click Me!", elem.Text);
            Assert.AreEqual("button", elem.TagName);
            Assert.AreEqual(21, elem.Size.Height);
            //Assert.AreEqual(new System.Drawing.Point(91, 338), elem.Location);
        }

        [TestMethod]
        public void DragAndDrop()
        {
            var actions = new Actions(driver);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));

            driver.Navigate().GoToUrl("http://www.pureexample.com/jquery-ui/basic-droppable.html");
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("ExampleFrame-94")));

            var source = driver.FindElement(By.XPath("//*[@class='square ui-draggable']"));
            var target = driver.FindElement(By.XPath("//*[@class='squaredotted ui-droppable']"));
            actions.DragAndDrop(source, target).Perform();
            var text = driver.FindElement(By.Id("info"));
            Assert.AreEqual("dropped!", text.Text);
        }

        [TestMethod]
        public void DragAndDropHTML5()
        {
            var actions = new Actions(driver);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));

            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/drag_and_drop");
            //wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("ExampleFrame-94")));

            var source = driver.FindElement(By.Id("column-a"));
            var target = driver.FindElement(By.Id("column-b"));

            //actions.ClickAndHold(source).MoveToElement(target).Release(target).Perform();
            actions.ClickAndHold(source).MoveByOffset(215, 0).Release().Perform();

            //var text = driver.FindElement(By.Id("info"));
            //Assert.AreEqual("dropped!", text.Text);
        }

        [TestMethod]
        public void Canvas()
        {
            var actions = new Actions(driver);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));

            driver.Navigate().GoToUrl("https://compendiumdev.co.uk/selenium/gui_user_interactions.html");

            var elem = driver.FindElement(By.Id("canvas"));

            actions.ClickAndHold(elem).MoveByOffset(50, 50).MoveByOffset(-10, 50).MoveByOffset(-10, -100).Release().Perform();
        }

        [TestMethod]
        public void ComplicatePageSearch()
        {
            var mainPage = new MainPage(driver);
            var searchPage = mainPage.Search("complicated page");
            Assert.AreEqual("https://www.ultimateqa.com/?s=complicated+page", searchPage.Driver.Url);

            searchPage.ClickTopLink();
            Assert.AreEqual("https://www.ultimateqa.com/complicated-page/", searchPage.Driver.Url);
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
