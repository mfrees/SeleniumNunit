using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunit
{
    class Appointments
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(); //Chrome browser
            //driver = new FirefoxDriver(); //Firefox browser
            driver.Manage().Window.Maximize(); //Opems the browser maximized
            driver.Url = "http://sandbox.clinicwise.net/reset";
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Id("password")).SendKeys("test");
            driver.FindElement(By.Id("signin_button")).Click();
            driver.FindElement(By.LinkText("Appointments")).Click();
        }

        [Test]
        public void CreateNewAppointment()
        {
            driver.FindElement(By.LinkText("30")).Click();
            
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close(); //closes browser
        }
    }
}
