﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI; /// for dropdown

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

        public int DoPrivacy(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    myDriver = driver3;
                }
                myDriver.Manage().Window.Maximize();
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement checkBoxAgree = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[3]/input"));
                checkBoxAgree.Click();

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement myAccept = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoIdentityInformation(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[1]"), myHistoryInfo);

                IWebElement myselectFirstName = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[1]"));
                myselectFirstName.SendKeys(myAccountCreate.myFirstName);

                //Enter Middle Name
                IWebElement myselectMiddleName = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[2]"));
                if (myAccountCreate.myMiddleName != null)
                {
                    myselectMiddleName.SendKeys(myAccountCreate.myMiddleName);
                }
                //Enter Last Name
                IWebElement myselectLasteName = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[2]/div/input"));
                myselectLasteName.SendKeys(myAccountCreate.myLastName);

                //Enter Suffix
                IWebElement myselectSuffix = driver.FindElement(By.XPath("//html/body/div/div/div[2]/div[2]/form/div[1]/div[2]/div/select"));
                if (myAccountCreate.mySuffix != null)
                {
                    myselectSuffix.SendKeys(myAccountCreate.mySuffix);
                }
                //Enter Address1
                IWebElement myselectAddress1 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[3]/input"));
                myselectAddress1.SendKeys(myApplication.myHomeAddress1);

                //Enter Address2
                IWebElement myselectAddress2 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[4]/input"));
                if (myApplication.myHomeAddress2 != null)
                {
                    myselectAddress2.SendKeys(myApplication.myHomeAddress2);
                }
                //Enter City
                IWebElement myselectCity = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[5]/div/input"));
                myselectCity.SendKeys(myApplication.myHomeCity);

                //need another outside click, won't move forward
                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[4]/input"));
                outsideClick.Click();

                //Enter State
                IWebElement myselectState = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[5]/div/select"));
                myselectState.SendKeys(myApplication.myHomeState);

                //Enter Zip
                IWebElement myselectZip = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[6]/div/input[1]"));
                myselectZip.SendKeys(myApplication.myHomeZip);

                //Enter Zip4
                IWebElement myselectZip4 = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[6]/div/input[2]"));
                if (myApplication.myHomeZip4 != null)
                {
                    myselectZip4.SendKeys(myApplication.myHomeZip4);
                }
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
                if (myApplication.myDOB != "01/01/2011")
                {
                    myselectDOB.SendKeys(myApplication.myDOB);
                }
                else
                {
                    myselectDOB.SendKeys(myAccountCreate.myDOB);
                }
                //Enter Captcha
                IWebElement myselectCaptcha = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[10]/span/div/div/table/tbody/tr[4]/td[1]/div/input"));
                myselectCaptcha.SendKeys("Google");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement clickNextButton = driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
                clickNextButton.Click();

                CaseWorker myCW = new CaseWorker();
                myCW.DoUpdateSSN(myHistoryInfo, myAccountCreate.mySSN, myAccountCreate.myFirstName, myAccountCreate.myLastName);

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

        public int DoAccountCreate(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[1]/div/input"), myHistoryInfo);

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

        public int DoProofing(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a/button"), myHistoryInfo);

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

        public int DoAccountLogin(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    myDriver = driver3;
                }
                myDriver.Manage().Window.Maximize();

                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                /*if (myHistoryInfo.myEnvironment == "STST2")//this may be changing
                {
                    driver.Navigate().GoToUrl("https://auth.stst2.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                    myDriver.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();//Click signin button
                }*/
                WebDriverWait wait = new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("username")));
                IWebElement myselectUsername = myDriver.FindElement(By.Id("username"));
                myselectUsername.SendKeys(myAccountCreate.myUsername);

                //Enter password
                IWebElement myselectPassword = myDriver.FindElement(By.Id("password"));
                myselectPassword.SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                //Click next button
                IWebElement myclickNext2 = myDriver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button"));
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSignin(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    myDriver = driver3;
                }
                myDriver.Manage().Window.Maximize();
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                myDriver.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();//Click signin button                

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }
        public int DoApplyWithDiscounts(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[1]/ul/li/a"), myHistoryInfo);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //Click apply with discount link                                                  
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

        public int DoApplyWithoutDiscounts(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[3]/ul/li[1]/a"), myHistoryInfo);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //Click apply without discount link                                                  
                IWebElement myclickLinkContinue = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[3]/ul/li[1]/a"));
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

        public int DoMyAccount(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                int appwait;
                ApplicationDo myApp = new ApplicationDo();
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    myDriver = driver3;

                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);

                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[2]/ul/li/a"), myHistoryInfo);

                    //Click my account link                                                  
                    IWebElement myAccount = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[2]/ul/li/a"));
                    myAccount.Click();
                }
                else
                {

                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);

                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[1]/ul/li/a"), myHistoryInfo);

                    //Click my account link                                                  
                    IWebElement myAccount = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[1]/ul/li/a"));
                    myAccount.Click();

                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[3]/div[2]/div[3]/div/div[2]/div/div/div[1]/div/div/h1[2]/ul/li/a"), myHistoryInfo);

                    IWebElement myAccount2 = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div[2]/div/div/div[1]/div/div/h1[2]/ul/li/a"));
                    myAccount2.Click();
                }

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myApp.DoWaitForElement(driver, By.TagName("iFrame"), myHistoryInfo);

                /*myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[2]/div[2]/div/nav/ol/li[2]/span/span/span/span[1]")).Click();//select payments first

                myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[2]/div[2]/div/nav/ol/li[1]/span/span/span/span[1]")).Click();//select home
                */
                var iFrameElement = myDriver.FindElement(By.TagName("iFrame"));
                myDriver.SwitchTo().Frame(iFrameElement);

                IWebElement myViewResults = myDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/div[2]/div/div[1]/div/div/a"));//sometimes view results doesn't show right away
                myViewResults.Click();

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoHomePage(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    myDriver = driver3;
                }

                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div/div/div/div/div[3]/a/button"), myHistoryInfo);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                //Click continue
                IWebElement myclickContinue = myDriver.FindElement(By.XPath("/html/body/div/div/div/div/div[3]/a/button"));
                myclickContinue.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoLoginTimeTravel(IWebDriver driver, IWebDriver driver3, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHistoryInfo myHistoryInfo,
               ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                driver.SwitchTo().DefaultContent();

                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();

                IWebElement textboxLogin;
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"), myHistoryInfo);
                    textboxLogin = driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"));
                }
                else
                {
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input"), myHistoryInfo);
                    textboxLogin = driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input"));
                }

                textboxLogin.SendKeys(myAccountCreate.myUsername);

                IWebElement textboxPW;
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    textboxPW = driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input"));
                }
                else
                {
                    textboxPW = driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input"));
                }
                textboxPW.SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSignIn;
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    buttonSignIn = driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]"));
                }
                else
                {
                    buttonSignIn = driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button"));
                }
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
