using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunit
{
    class Clients
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver(); //Chrome browser
            //driver = new FirefoxDriver(); //Firefox browser
            driver.Manage().Window.Maximize(); //Opems the browser maximized
            driver.Url = "http://sandbox.clinicwise.net/reset";
            driver.FindElement(By.Id("username")).SendKeys("admin"); //Enters username
            driver.FindElement(By.Id("password")).SendKeys("test"); //Enters password
            driver.FindElement(By.Id("signin_button")).Click(); //Clicks the Signin button
            driver.FindElement(By.XPath("//*[@id='nav_clients']/span")).Click(); //Clicks the Clients option from left panel
        }
        [Test]
        public void InvalidLookUpClient() //Invalid search criteria
        {
            //System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.Id("first_name")).SendKeys("Tom"); //Populates firstname field
            driver.FindElement(By.Id("last_name")).SendKeys("Williams"); //Populates last name field
            driver.FindElement(By.Id("invoice_ref")).SendKeys("123456qaaz"); //Populates the invoice field
            driver.FindElement(By.Id("phone")).SendKeys("qasetres"); //Populates the Phone field
            String matching_str = "No matching clients found.";
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
            driver.FindElement(By.XPath("//*[@id='patients_search_form']/h5/input")).Click(); //Clicks the Reset button
        }
        [Test]
        public void ValidLookUpClient() //Valid serach - I NEED TO COMPLETE THE ASSERTION
        {
            driver.FindElement(By.Id("last_name")).SendKeys("Bond"); //Populates last name field
            driver.FindElement(By.XPath("//*[@id='patients_search_form']/h5/input")).Click(); //Clicks the Reset button
        }
        [Test]
        public void NewClientNavigation() //Navigate to new client page and verifies mandatory fields
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New client button
            Assert.IsTrue(driver.PageSource.Contains("New client")); //Verifies the New Client text
            driver.FindElement(By.Id("create_client_btn")).Click(); //Clicks the Create button
            Assert.IsTrue(driver.PageSource.Contains("The form contains 3 errors")); //Verifies mandatory field text is displayed
        }
        [Test]
        public void AllClients() //Navigates to the All Clients screen
        {
            driver.FindElement(By.Id("clients_list_link")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Look up client"));
        }
        [Test]
        public void SearchResetButton() //Asserts the Rest button is enabled
        {
            Assert.IsTrue(driver.FindElement(By.XPath("//*[@id='patients_search_form']/h5/input")).Displayed); //Verifies the Reset button is displayed
        }
        [Test]
        public void ResetButtonEnabled() //Asserts if the Reset button is enabled
        {
            Assert.IsTrue(driver.FindElement(By.XPath("//*[@id='patients_search_form']/h5/input")).Enabled);
        }
        [Test]
        public void ResetButton() //Reset funhctionality
        {
            driver.FindElement(By.Id("first_name")).SendKeys("Thomas"); //Enters invalid firstname
            driver.FindElement(By.Id("last_name")).SendKeys("Taylor"); //Enters invalid lastname
            driver.FindElement(By.Id("invoice_ref")).SendKeys("01789asdguiji/333/abs/9"); //Enters invalid invoics number
            driver.FindElement(By.Id("phone")).SendKeys("01554 899925"); //Enters a invalid phone number
            driver.FindElement(By.XPath("//*[@id='patients_search_form']/h5/input")).Click(); //Clicks the reset button
            driver.FindElement(By.Id("first_name")).SendKeys("James"); //Enters a valid firstname
            driver.FindElement(By.Id("last_name")).SendKeys("Bond"); //Enters a valid last name
            //driver.FindElement(By.Id("invoice_ref")).SendKeys("01789asdguiji/333/abs/9");
            driver.FindElement(By.Id("phone")).SendKeys("0432121200"); //Enters a valid phone number
            driver.FindElement(By.XPath("//*[@id='patients_search_form']/h5/input")).Click(); //Clicks reset button
        }
        [Test]
        public void NewClientButton() //Verifies the new client button is enabled
        {
            Assert.IsTrue(driver.FindElement(By.Id("new_client_link")).Displayed); //Verifies the New Client button is displayed
            Assert.IsTrue(driver.FindElement(By.Id("new_client_link")).Enabled); //Verifies the New Client button is enabled
            Assert.IsTrue(driver.FindElement(By.Id("clients_list_link")).Displayed); //Verifies the All Client button is displayed
            Assert.IsTrue(driver.FindElement(By.Id("clients_list_link")).Enabled); //Verifies the All Client button is enabled
        }
        [Test]
        public void NewClientCancel() //Cancel button functionality
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            driver.FindElement(By.XPath("//*[@id='new_client']/fieldset/div[2]/div/a")).Click(); //Clicks the Cancel button
            String matching_str = "Enter Search Criteria"; //Verifies text on the Look Up Client page
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
        }
        [Test]
        public void MandatoryFields() //Populates the First Name field and then clicks the create button
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            driver.FindElement(By.Id("client_first_name")).SendKeys("James"); //Populates the First Name field
            driver.FindElement(By.Id("create_client_btn")).Click(); //Clicks the Create button
            String matching_str = "The form contains 3 errors"; //Verifies text on the Look Up Client page
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
        }
        [Test]
        public void MandatoryFieldsOneError() //Populates the First Name and lastname then clicks thencreate button
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            driver.FindElement(By.Id("client_first_name")).SendKeys("James"); //Populates the First Name field
            driver.FindElement(By.Id("client_last_name")).SendKeys("Thoams"); //Populates the lastname field
            driver.FindElement(By.Id("create_client_btn")).Click(); //Clicks the Create button
            String matching_str = "The form contains 2 errors"; //Verifies text on the Look Up Client page
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
        }
        [Test] 
        public void NewClientCreation() //Successfully creates a new client
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            driver.FindElement(By.Id("client_first_name")).SendKeys("James"); //Populates the First Name field
            driver.FindElement(By.Id("client_last_name")).SendKeys("Thomas"); //Populates the lastname field
            driver.FindElement(By.Id("client_mobile")).SendKeys("0406843248"); //Populates the mobile field with Australian mobile format
            driver.FindElement(By.Id("create_client_btn")).Click(); //Clicks the Create button
            String matching_str = "James Thomas"; //Verifies client name
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
        }
        [Test]
        public void DuplicateClient() //Creates a duplicate client and validates message displayed
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            driver.FindElement(By.Id("client_first_name")).SendKeys("James"); //Populates the First Name field
            driver.FindElement(By.Id("client_last_name")).SendKeys("Bond"); //Populates the lastname field
            driver.FindElement(By.Id("client_mobile")).SendKeys("0432121200"); //Populates the mobile field with Australian mobile format
            driver.FindElement(By.Id("client_gender_male")).Click(); //Clicks the Male radio button
            driver.FindElement(By.Id("create_client_btn")).Click(); //Clicks the Create button
            String matching_str = "There are clients with exactly the same name in the system, please confirm."; //Validates the correct warning text is displayed
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
        }
        [Test]
        public void DeleteCancelButton() //Cancels the deletion and verifies the client remains
        {
            driver.FindElement(By.Id("menu_search_clients_link")).Click();
            driver.FindElement(By.Id("patients_search")).SendKeys("Jeremy Lin");
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.LinkText("Cancel")).Click();
            driver.FindElement(By.Id("delete_patient_link")).Click();
            System.Threading.Thread.Sleep(500);
            driver.FindElement(By.XPath("//div[@class='modal-footer']/button[text()='Cancel']")).Click(); //Clicks the Cancel button on a modal dialog
            Assert.IsTrue(driver.PageSource.Contains("Jeremy Lin"));
        }

        [Test]
        public void DeletionOfClient() //Tests that clients can be deleted
        {
            driver.FindElement(By.Id("menu_search_clients_link")).Click();
            driver.FindElement(By.Id("patients_search")).SendKeys("James Bond");
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.LinkText("Cancel")).Click();
            driver.FindElement(By.Id("delete_patient_link")).Click();
            System.Threading.Thread.Sleep(500);
            driver.FindElement(By.XPath("//div[@class='modal-footer']/button[text()='OK']")).Click(); //Clicks the ok button on a modal dialog
            Assert.IsTrue(driver.PageSource.Contains("Patient deleted successfully."));
        }

        [Test]
        public void BirthDateValidation() //Birth date field validation
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            driver.FindElement(By.Id("client_first_name")).SendKeys("James"); //Populates the First Name field
            driver.FindElement(By.Id("client_last_name")).SendKeys("Bond"); //Populates the lastname field
            driver.FindElement(By.Id("client_mobile")).SendKeys("0432121200"); //Populates the mobile field with Australian mobile format
            //driver.FindElement(By.Id("client_gender_male")).Click(); //Clicks the Male radio button
            driver.FindElement(By.Id("client_birth_date")).SendKeys("27/06/2021"); //Enters a invalid birth date
            driver.FindElement(By.Id("create_client_btn")).Click(); //Clicks the Create button 
            String matching_str = "The date must be before today"; //Validates the correct warning text is displayed
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
        }
        [Test]        
        public void DateClearButton() //Verifies the Clear button is enabled and other field validation
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            driver.FindElement(By.Id("client_first_name")).SendKeys("James"); //Populates the First Name field
            driver.FindElement(By.Id("client_last_name")).SendKeys("Bond"); //Populates the lastname field
            driver.FindElement(By.Id("client_mobile")).SendKeys("0432121200"); //Populates the mobile field with Australian mobile format
            driver.FindElement(By.Id("client_birth_date")).SendKeys("27/10/1977"); //populates the dob field
            driver.FindElement(By.Id("client_phone")).SendKeys("01558847987"); //populates home tel
            Assert.IsTrue(driver.FindElement(By.XPath("//*[@id='clear_birth_date_age_link']")).Displayed); //validates the clear button is displayed
            Assert.IsTrue(driver.FindElement(By.XPath("//*[@id='clear_birth_date_age_link']")).Enabled); //Verifies if the Clear buton is enabled
            Assert.IsTrue(driver.FindElement(By.Id("client_gender_male")).Displayed); //verifies if the male radio button is displayed 
            Assert.IsTrue(driver.FindElement(By.Id("client_gender_male")).Enabled);  //verifies if the male radio button is ensbled
            Assert.IsTrue(driver.FindElement(By.Id("client_gender_female")).Displayed); //Verifies if the Female radio button is displayed
            Assert.IsTrue(driver.FindElement(By.Id("client_gender_female")).Enabled); //verifies if the Female radio button is enabled
        }
        [Test]
        public void ClickClearButton() //Enter a date press the clear button and then verify the Age field state
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            driver.FindElement(By.Name("client[birth_date]")).SendKeys("01/01/1993"); //Enters date
            driver.FindElement(By.Id("client_first_name")).SendKeys("Thomas"); //Enters a first name in order to change focus
            Assert.IsFalse(driver.FindElement(By.Id("client_age")).Enabled); //Verifies if the age field is enabled
            driver.FindElement(By.XPath("//*[@id='clear_birth_date_age_link']")).Click(); //Clicks the clear button
            Assert.IsTrue(driver.FindElement(By.Id("client_age")).Enabled); //verifies if the Age field is enabled
        }
        [Test]
        public void NewReferralLink() //Tests the New Referral link works and verifies you are taken to the correct page
        {
            driver.FindElement(By.Id("new_client_link")).Click(); //Clicks the New Client button
            Assert.IsTrue(driver.FindElement(By.LinkText("New Referral")).Displayed);
            driver.FindElement(By.LinkText("New Referral")).Click();
            Assert.IsTrue(driver.PageSource.Contains("New Referral"));
        }
        [Test]
        public void ClientPage() //Verification of the buttons on the client page
        {
            driver.FindElement(By.XPath("//*[@id='clients_list_link']/span")).Click();
            driver.FindElement(By.Id("patients_search")).SendKeys("Bruce Lee"); //Filters search results
            driver.FindElement(By.LinkText("Edit")).Click(); //Clicks the Edit button
            driver.FindElement(By.LinkText("Cancel")).Click(); //Clicks the Cancel button
            Assert.IsTrue(driver.FindElement(By.Id("edit-client-link")).Displayed); //Verifies the Edit button button is displayed
            Assert.IsTrue(driver.FindElement(By.Id("edit-client-link")).Enabled); //Verifies the Edit button is enabled
            Assert.IsTrue(driver.FindElement(By.Id("delete_patient_link")).Displayed); //Verifies the Delete button is displayed
            Assert.IsTrue(driver.FindElement(By.Id("delete_patient_link")).Enabled); //Verifies the Delete button is enabled
            Assert.IsTrue(driver.FindElement(By.LinkText("Send Letter")).Displayed); //Verifies the Send Letter button is displayed
            Assert.IsTrue(driver.FindElement(By.LinkText("Send Letter")).Enabled); //Verifies the send Letter button is enabled
            Assert.IsTrue(driver.FindElement(By.LinkText("Measurements")).Displayed); //Verifies the Measurements button is displayed
            Assert.IsTrue(driver.FindElement(By.LinkText("Measurements")).Enabled); //Verifies the Measurement button is enabled
            Assert.IsTrue(driver.FindElement(By.Id("new_pass_link")).Displayed); //Verifies the New Pass button is displayed
            Assert.IsTrue(driver.FindElement(By.Id("new_pass_link")).Enabled); //Verifies the New Pass button is enabled
        }
        [Test]
        public void CreateNewPassandNavigateToNewPassScreen() //Creates a new pass and assigns a pass to a client
        {
            driver.FindElement(By.LinkText("Treatment")).Click(); //Clicks the Treatment nav menu option
            System.Threading.Thread.Sleep(2000); //2 Sec wait
            driver.FindElement(By.LinkText("Pass")).Click(); //Clicks the Pass option from treatment nav option
            driver.FindElement(By.Id("new_pass_type_link")).Click(); //Clicks the New Pass Type button
            driver.FindElement(By.Id("pass_type_name")).SendKeys("Medicare Pass"); //Populates the Name field
            driver.FindElement(By.Id("by_use")).Click(); //Checks the Max Use checkbox
            driver.FindElement(By.Id("pass_type_max_use")).SendKeys("5"); //Enters the value 5 
            driver.FindElement(By.Id("pass_type_product_category_id")).FindElement(By.XPath("//*[@id='pass_type_product_category_id']/option[2]")).Click(); //populates the category
            driver.FindElement(By.Id("create_pass_type_btn")).Click(); //Clicks the create button
            System.Threading.Thread.Sleep(2000); //2 Sec wait
            driver.FindElement(By.Id("create_pass_type_btn")).Click(); //Clicks the create button
            String matching_str = "Pass type created successfully"; //Validates the correct warning text is displayed
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str));
            driver.FindElement(By.Id("nav_clients")).Click(); //Clients nav menu option
            System.Threading.Thread.Sleep(2000); //2 Sec wait
            driver.FindElement(By.Id("menu_search_clients_link")).Click(); //Clicks the All Clients button
            driver.FindElement(By.Id("patients_search")).SendKeys("Bruce Lee"); //Filters search results
            driver.FindElement(By.LinkText("Edit")).Click(); //Clicks the Edit button
            driver.FindElement(By.LinkText("Cancel")).Click(); //Clicks the Cancel button
            driver.FindElement(By.Id("new_pass_link")).Click(); //cliks the New Pass button
            driver.FindElement(By.Id("pass_type_id")).FindElement(By.XPath("//*[@id='pass_type_id']/option[2]")).Click(); //selects pass type
            System.Threading.Thread.Sleep(2000); //2 Sec wait
            driver.FindElement(By.Id("create_and_pay_later_btn")).Click();
            String matching_str1 = "Pass was successfully created."; //Validates the correct warning text is displayed
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(matching_str1));
        }
            
            

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit(); //closes browser
        }
    }
}
