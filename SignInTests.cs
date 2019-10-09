using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunit
{
    class SignInTests
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(); //Chrome browser
            //driver = new FirefoxDriver(); //Firefox browser
            driver.Manage().Window.Maximize(); //Opems the browser maximized
            driver.Url = "http://sandbox.clinicwise.net/reset";
        }

        [Test]
        public void PageTitle()
        {
            //driver.Url = "http://sandbox.clinicwise.net/reset";
            Assert.AreEqual("Wise Clinic", driver.Title);
        }
        [Test]
        public void NoUsernameOrPassword() //Clicks the Sign In button without entering username & password
        {
            //driver.Url = "http://sandbox.clinicwise.net/reset";
            driver.FindElement(By.Id("signin_button")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Incorrect password"));
        }
        [Test]
        public void ValidUsernameNoPassword() //Valid username with no password entered
        {
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Id("signin_button")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Incorrect password"));
        }
        [Test]
        public void NoUsernameValidPassword() //No username entered just valid password
        {
            driver.FindElement(By.Id("password")).SendKeys("test");
            driver.FindElement(By.Id("signin_button")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Incorrect password"));
        }
        [Test]
        public void WrongUsernameValidPassword() //wrong username entered with valid password
        {
            driver.FindElement(By.Id("username")).SendKeys("qwerty");
            driver.FindElement(By.Id("password")).SendKeys("test");
            driver.FindElement(By.Id("signin_button")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Incorrect password"));
        }
        [Test]
        public void ValidUsernameWrongPassword() //Valid username entered with wrong password
        {
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Id("password")).SendKeys("qwerty111");
            driver.FindElement(By.Id("signin_button")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Incorrect password"));
        }
        [Test]
        public void ValidUsernameAndPassword()
        {
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Id("password")).SendKeys("test");
            driver.FindElement(By.Id("signin_button")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Signed in successfully."));
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }


    }
}
