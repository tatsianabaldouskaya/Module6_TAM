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
    class MailTests : TestBaseClass
    {
        private const string addressee = "tatiana95.77@gmail.com";
        private const string subject = "For test";
        private const string body = "This is test email";
        private bool elementAbsent;

        [Test]
        public void MailTest()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='gb_nb']")));
            var actualMail = driver.FindElement(By.XPath("//div[@class='gb_nb']")).Text;
            Assert.AreEqual(email, actualMail, "Login is unsuccessful");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//div[@class='T-I T-I-KE L3']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.Name("to")).SendKeys(addressee);
            driver.FindElement(By.Name("subjectbox")).SendKeys(subject);
            driver.FindElement(By.CssSelector("div.editable")).SendKeys(body);
            driver.FindElement(By.XPath("//img[@alt ='Close']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.LinkText("Drafts")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement letter = driver.FindElement(By.XPath("//span/span[text() = 'For test']"));

            Assert.IsTrue(letter.Displayed, "Letter is not found in Draft folder");

            driver.FindElement(By.XPath("//span/span[text() = 'For test']")).Click();
            Assert.AreEqual(addressee, driver.FindElement(By.CssSelector("div.oL>span[email]"))
                .Text, "Addressee doesn't correspond to expected");
            Assert.AreEqual(body, driver.FindElement(By.CssSelector("div.editable"))
                .Text, "Message body doesn't correspond to expected");
            //Assert.AreEqual(body, driver.FindElement(By.Name("subjectbox"))
            //.Text, "Subject doesn't correspond to expected");

            driver.FindElement(By.CssSelector("div[data-tooltip^='Send']")).Click();
            try
            {
                IWebElement element = driver.FindElement(By.XPath("//span/span[text() = 'For test']"));
            }
            catch (NoSuchElementException)
            {
                elementAbsent = true;
            }
            Assert.IsTrue(elementAbsent, "Letter is still in draft folder");

            driver.FindElement(By.XPath("//a[contains(@href,'#sent')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.IsTrue(driver.FindElements(By.XPath("//tr[@class='zA yO']")).Count > 0,
                "Letter is not found in Sent folder");

            driver.FindElement(By.XPath("//img[@class='gb_Ca gbii']")).Click();
            driver.FindElement(By.XPath("//a[text()= 'Sign out']")).Click();
            
            IWebElement logOutText = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until<IWebElement>((d) =>
            {
                return d.FindElement(By.XPath("//div[text()= 'Вы не вошли в аккаунт']"));
            });
            Assert.That(logOutText.Displayed, "Log out is unsuccessful");
        }
    }
}
