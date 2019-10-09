using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunit
{
    class Dashboard
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void WiseClicHomePage()
        {
            driver.Url = "http://sandbox.clinicwise.net/reset"; //URL
            driver.Manage().Window.Maximize(); //Opens the web page maximized
            Assert.AreEqual("Wise Clinic", driver.Title); //Asserts the page title
        }
        [Test]
        public void SignInButton() //Click sign in button without username & password
        {
            driver.Url = "http://sandbox.clinicwise.net/reset"; //URL
            driver.Manage().Window.Maximize(); //Opens the web page maximized
            driver.FindElement(By.Id("signin_button")).Click();
            String matching_str = "Incorrect password";
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
        }
        [Test]
        public void SignInButton1() //This test is identical to the previous test, however runs 15 seconds faster
        {
            driver.Url = "http://sandbox.clinicwise.net/reset"; //URL
            driver.Manage().Window.Maximize(); //Opens the web page maximized
            driver.FindElement(By.Id("signin_button")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Incorrect password"));

        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close(); //closes browser
        }
            
        
    }
}
