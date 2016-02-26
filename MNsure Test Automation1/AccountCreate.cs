using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI; // for dropdown

using System.Net;
using System.Data.Sql;

using System.Data.SqlClient;
using System.Data.SqlServerCe;
using OpenQA.Selenium.Chrome;

namespace MNsure_Regression_1
{
    class AccountCreation
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoPrivacy(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                driver.Manage().Window.Maximize();

                //Wait for sign in button 
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"))));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement checkBoxAgree = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[3]/input"));
                checkBoxAgree.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement myAccept = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
                myAccept.SendKeys("\n");

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoIdentityInformation(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                //Wait for First Name button 
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[1]"))));

                //Enter First Name
                IWebElement myselectFirstName = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[1]"));
                myselectFirstName.SendKeys(myAccountCreate.myFirstName);

                //Enter Middle Name
                IWebElement myselectMiddleName = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[2]"));
                myselectMiddleName.SendKeys(myAccountCreate.myMiddleName);

                //Enter Last Name
                IWebElement myselectLasteName = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[2]/div/input"));
                myselectLasteName.SendKeys(myAccountCreate.myLastName);

                //Enter Suffix
                IWebElement myselectSuffix = driver.FindElement(By.XPath("//html/body/div/div/div[2]/div[2]/form/div[1]/div[2]/div/select"));
                myselectSuffix.SendKeys(myAccountCreate.mySuffix);

                //Enter Address1
                IWebElement myselectAddress1 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[3]/input"));
                myselectAddress1.SendKeys(myAccountCreate.myAddress1);

                //Enter Address2
                IWebElement myselectAddress2 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[4]/input"));
                myselectAddress2.SendKeys(myAccountCreate.myAddress2);

                //Enter City
                IWebElement myselectCity = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[5]/div/input"));
                myselectCity.SendKeys(myAccountCreate.myCity);

                //need another outside click, won't move forward
                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[4]/input"));
                outsideClick.Click();

                //Enter State
                IWebElement myselectState = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[5]/div/select"));
                myselectState.SendKeys(myAccountCreate.myState);

                //Enter Zip
                IWebElement myselectZip = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[6]/div/input[1]"));
                myselectZip.SendKeys(myAccountCreate.myZip);

                //Enter Zip4
                IWebElement myselectZip4 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[6]/div/input[2]"));
                myselectZip4.SendKeys(myAccountCreate.myZip4);

                //Enter Email
                IWebElement myselectEmail = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[7]/div/input[1]"));
                myselectEmail.SendKeys(myAccountCreate.myEmail);

                //Enter Email again
                IWebElement myselectEmail2 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[7]/div/input[2]"));
                myselectEmail2.SendKeys(myAccountCreate.myEmail);

                //Enter Phone number
                IWebElement myselectPhone = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[1]"));
                myselectPhone.SendKeys(myAccountCreate.myPhone);

                string mysSSN1 = myAccountCreate.mySSN.Substring(0, 3);
                string mysSSN2 = myAccountCreate.mySSN.Substring(3, 2);
                string mysSSN3 = myAccountCreate.mySSN.Substring(5, 4);
                //Enter SSN1
                IWebElement myselectSSN1 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[2]"));
                myselectSSN1.SendKeys(mysSSN1);

                //Enter SSN2
                IWebElement myselectSSN2 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[3]"));
                myselectSSN2.SendKeys(mysSSN2);

                //Enter SSN3
                IWebElement myselectSSN3 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[4]"));
                myselectSSN3.SendKeys(mysSSN3);

                //Enter DOB
                IWebElement myselectDOB = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[5]"));
                myselectDOB.SendKeys(myAccountCreate.myDOB);

                //Enter Captcha
                IWebElement myselectCaptcha = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[10]/span/div/div/table/tbody/tr[4]/td[1]/div/input"));
                myselectCaptcha.SendKeys("Google");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement clickNextButton = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
                clickNextButton.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAccountCreate(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                //Wait for User Name field 
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[1]/div/input"))));

                //Enter User Name
                IWebElement myselectUserName = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[1]/div/input"));
                myselectUserName.SendKeys(myAccountCreate.myUsername);

                IWebElement myselectPassword = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[4]/div/input"));
                myselectPassword.SendKeys(myAccountCreate.myPassword);

                IWebElement myselectPassword2 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[5]/div/input"));
                myselectPassword2.SendKeys(myAccountCreate.myPassword);

                IWebElement myselectSecret = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[8]/div/input"));
                myselectSecret.SendKeys(myAccountCreate.mySecret);

                string temp1;
                temp1 = myAccountCreate.myQuestion1;
                IWebElement myselectQuestion1 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[12]/div[1]/select"));
                var selectQuestion1 = new SelectElement(myselectQuestion1);
                selectQuestion1.SelectByValue(myAccountCreate.myQuestion1);

                IWebElement myselectAnswer1 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[12]/div[1]/input"));
                myselectAnswer1.SendKeys(myAccountCreate.myAnswer1);

                IWebElement myselectQuestion2 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[13]/div[1]/select"));
                var selectQuestion2 = new SelectElement(myselectQuestion2);
                selectQuestion2.SelectByValue(myAccountCreate.myQuestion2);

                IWebElement myselectAnswer2 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[13]/div[1]/input"));
                myselectAnswer2.SendKeys(myAccountCreate.myAnswer2);

                IWebElement myselectQuestion3 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[14]/div[1]/select"));
                var selectQuestion3 = new SelectElement(myselectQuestion3);
                selectQuestion3.SelectByValue(myAccountCreate.myQuestion3);

                IWebElement myselectAnswer3 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[14]/div[1]/input"));
                myselectAnswer3.SendKeys(myAccountCreate.myAnswer3);

                IWebElement myselectQuestion4 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[15]/div[1]/select"));
                var selectQuestion4 = new SelectElement(myselectQuestion4);
                selectQuestion4.SelectByValue(myAccountCreate.myQuestion4);

                IWebElement myselectAnswer4 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[15]/div[1]/input"));
                myselectAnswer4.SendKeys(myAccountCreate.myAnswer4);

                IWebElement myselectQuestion5 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[16]/div[1]/select"));
                var selectQuestion5 = new SelectElement(myselectQuestion5);
                selectQuestion5.SelectByValue(myAccountCreate.myQuestion5);

                IWebElement myselectAnswer5 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[16]/div[1]/input"));
                myselectAnswer5.SendKeys(myAccountCreate.myAnswer5);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement myclickNext = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[2]/button"));
                myclickNext.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoProofing(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                //Wait for Next button 
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a/button"))));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //Click next button to get to account login
                IWebElement myclickNext = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a/button"));
                myclickNext.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAccountLogin(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                driver.Manage().Window.Maximize();

                //Wait for Username field
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input"))));

                //Enter username
                IWebElement myselectUsername = driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input"));
                myselectUsername.SendKeys(myAccountCreate.myUsername);

                //Enter password
                IWebElement myselectPassword = driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input"));
                myselectPassword.SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //Click next button
                IWebElement myclickNext2 = driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button"));
                myclickNext2.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoApply(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                //Wait for Link - Individuals and families...
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[1]/ul/li/a"))));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //Click Link                                                  
                IWebElement myclickLinkContinue = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[1]/ul/li/a"));
                myclickLinkContinue.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoHomePage(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                //Wait for Continue
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div/div/div/div/div[3]/a/button"))));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //Click continue
                IWebElement myclickContinue = driver.FindElement(By.XPath("/html/body/div/div/div/div/div[3]/a/button"));
                myclickContinue.Click();

                //new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[1]/ul/li/a"))));
                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoLoginTimeTravel(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                driver.SwitchTo().DefaultContent();

                System.Threading.Thread.Sleep(1000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxLogin = driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"));
                textboxLogin.SendKeys(myAccountCreate.myUsername);

                IWebElement textboxPW = driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input"));
                textboxPW.SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSignIn = driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]"));
                buttonSignIn.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;

            }
        }

    }
}
