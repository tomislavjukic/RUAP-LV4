using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Untitled
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://demo.opencart.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        public static int GetRandomNumber()
        {
            return new Random().Next(1, 20);
        }

        [Test]
        public void TheUntitledTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            Assert.AreEqual("Your Store", driver.Title);
            driver.FindElement(By.XPath("//div[@id='top-links']/ul/li[2]/a/span")).Click();
            driver.FindElement(By.CssSelector("ul.dropdown-menu.dropdown-menu-right > li > a")).Click();
            Assert.AreEqual("My Account", driver.Title);
            driver.FindElement(By.LinkText("Edit your account information")).Click();
            Assert.AreEqual("My Account Information", driver.Title);
            driver.FindElement(By.Id("input-email")).Clear();
            driver.FindElement(By.Id("input-email")).SendKeys("noviemail" + GetRandomNumber() + "@gmail.com");
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            Assert.AreEqual("My Account", driver.Title);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}