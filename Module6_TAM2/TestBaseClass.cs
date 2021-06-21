using System;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace Module6_TAM2
{
    [TestFixture]
    public class TestBaseClass
    {
        protected string email = "june.t14352@gmail.com";
        protected string password = "H98kS_02b";
        public IWebDriver driver;
        private string baseUrl;

        [OneTimeSetUp]
        public void SetUp()
        {
           // var service = ChromeDriverService.CreateDefaultService("D:/webdriver");
            driver = new ChromeDriver();
            baseUrl = "https://mail.google.com";
            driver.Navigate().GoToUrl(baseUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);
            driver.FindElement(By.CssSelector("input[type='email']")).SendKeys(email);
            driver.FindElement(By.XPath("//*[text() = 'Далее']")).Click();
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@type = 'password']"))).SendKeys(password);
            driver.FindElement(By.XPath("//*[text() = 'Далее']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.XPath("//img[@class='gb_Ca gbii']")).Click();
        }

        [TearDown]
        public void TeatDown()
        {
            driver.Close();
            driver.Quit();
        }

        //public void IsElementVisible(By element, int timeoutSecs = 10)
        //{
        //    new WebDriverWait(this.driver, TimeSpan.FromSeconds(timeoutSecs)).Until(ExpectedConditions.ElementIsVisible(element));
        //}
    } 
}
