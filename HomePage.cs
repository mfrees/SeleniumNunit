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
    class HomePage
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
        }
        [Test]
        public void VerifyHomePageText() //Verifies correct flsh message is displayed once you have looged into the dashboard for the first time
        {
        String checktext = "Signed in successfully.";
        Assert.IsTrue(driver.FindElement(By.Id("flash_notice")).Text.Contains(checktext));
        }
        [Test]
        public void NewClientImage() //verifies New Client image is visible
        {
            Assert.IsTrue(driver.FindElement(By.Id("shortcut_plus_client_btn")).Displayed);
        }
        


        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }


    }
}
