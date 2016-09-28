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
using OpenQA.Selenium.Support.UI;
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

        public int DoPrivacy(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    myDriver = driver3;
                }
                else if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
                }
                myDriver.Manage().Window.Maximize();
                int appwait;
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;//was 0
                    }
                    else
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (0 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement checkBoxAgree = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[13]/input"));
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

        public int DoIdentityInformation(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
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
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[1]"), myHistoryInfo);

                IWebElement myselectFirstName = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[1]"));
                myselectFirstName.SendKeys(myAccountCreate.myFirstName);

                IWebElement myselectMiddleName = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[1]/div/input[2]"));
                if (myAccountCreate.myMiddleName != null)
                {
                    myselectMiddleName.SendKeys(myAccountCreate.myMiddleName);
                }
                IWebElement myselectLasteName = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[2]/div/input"));
                myselectLasteName.SendKeys(myAccountCreate.myLastName);

                IWebElement myselectSuffix = myDriver.FindElement(By.XPath("//html/body/div/div/div[2]/div[2]/form/div[1]/div[2]/div/select"));
                if (myAccountCreate.mySuffix != null)
                {
                    myselectSuffix.SendKeys(myAccountCreate.mySuffix);
                }

                IWebElement myselectAddress1 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[3]/input"));
                myselectAddress1.SendKeys(myApplication.myHomeAddress1);

                IWebElement myselectAddress2 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[4]/input"));
                if (myApplication.myHomeAddress2 != null)
                {
                    myselectAddress2.SendKeys(myApplication.myHomeAddress2);
                }

                IWebElement myselectCity = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[5]/div/input"));
                myselectCity.SendKeys(myApplication.myHomeCity);

                //need another outside click, won't move forward
                IWebElement outsideClick = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[4]/input"));
                outsideClick.Click();

                IWebElement myselectState = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[5]/div/select"));
                myselectState.SendKeys(myApplication.myHomeState);

                IWebElement myselectZip = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[6]/div/input[1]"));
                myselectZip.SendKeys(myApplication.myHomeZip);

                IWebElement myselectZip4 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[6]/div/input[2]"));
                if (myApplication.myHomeZip4 != null)
                {
                    myselectZip4.SendKeys(myApplication.myHomeZip4);
                }

                IWebElement myselectEmail = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[7]/div/input[1]"));
                myselectEmail.SendKeys(myAccountCreate.myEmail);

                IWebElement myselectEmail2 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[7]/div/input[2]"));
                myselectEmail2.SendKeys(myAccountCreate.myEmail);

                IWebElement myselectPhone = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[1]"));
                myselectPhone.SendKeys(myAccountCreate.myPhone);

                string mysSSN1 = myAccountCreate.mySSN.Substring(0, 3);
                string mysSSN2 = myAccountCreate.mySSN.Substring(3, 2);
                string mysSSN3 = myAccountCreate.mySSN.Substring(5, 4);
                IWebElement myselectSSN1 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[2]"));
                myselectSSN1.SendKeys(mysSSN1);

                IWebElement myselectSSN2 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[3]"));
                myselectSSN2.SendKeys(mysSSN2);

                IWebElement myselectSSN3 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[4]"));
                myselectSSN3.SendKeys(mysSSN3);

                IWebElement myselectDOB = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div/input[5]"));
                if (myApplication.myDOB != "01/01/2011")
                {
                    myselectDOB.Click();
                    string tempDOB;
                    tempDOB = Convert.ToString(myApplication.myDOB);
                    tempDOB = DateTime.Parse(tempDOB).ToString("MM/dd/yyyy");
                    myselectDOB.SendKeys(tempDOB);
                }
                else
                {
                    myselectDOB.SendKeys(myAccountCreate.myDOB);
                }

                IWebElement myselectCaptcha = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[10]/span/div/div/table/tbody/tr[4]/td[1]/div/input"));
                myselectCaptcha.SendKeys("Google");

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement clickNextButton = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAccountCreate(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
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
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[1]/div/input"), myHistoryInfo);

                IWebElement myselectUserName = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[1]/div/input"));
                myselectUserName.SendKeys(myAccountCreate.myUsername);

                IWebElement myselectPassword = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[4]/div/input"));
                myselectPassword.SendKeys(myAccountCreate.myPassword);

                IWebElement myselectPassword2 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[5]/div/input"));
                myselectPassword2.SendKeys(myAccountCreate.myPassword);

                IWebElement myselectSecret = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[8]/div/input"));
                myselectSecret.SendKeys(myAccountCreate.mySecret);

                string temp1;
                temp1 = myAccountCreate.myQuestion1;
                IWebElement myselectQuestion1 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[12]/div[1]/select"));
                var selectQuestion1 = new SelectElement(myselectQuestion1);
                selectQuestion1.SelectByValue(myAccountCreate.myQuestion1);

                IWebElement myselectAnswer1 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[12]/div[1]/input"));
                myselectAnswer1.SendKeys(myAccountCreate.myAnswer1);

                IWebElement myselectQuestion2 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[13]/div[1]/select"));
                var selectQuestion2 = new SelectElement(myselectQuestion2);
                selectQuestion2.SelectByValue(myAccountCreate.myQuestion2);

                IWebElement myselectAnswer2 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[13]/div[1]/input"));
                myselectAnswer2.SendKeys(myAccountCreate.myAnswer2);

                IWebElement myselectQuestion3 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[14]/div[1]/select"));
                var selectQuestion3 = new SelectElement(myselectQuestion3);
                selectQuestion3.SelectByValue(myAccountCreate.myQuestion3);

                IWebElement myselectAnswer3 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[14]/div[1]/input"));
                myselectAnswer3.SendKeys(myAccountCreate.myAnswer3);

                IWebElement myselectQuestion4 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[15]/div[1]/select"));
                var selectQuestion4 = new SelectElement(myselectQuestion4);
                selectQuestion4.SelectByValue(myAccountCreate.myQuestion4);

                IWebElement myselectAnswer4 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[15]/div[1]/input"));
                myselectAnswer4.SendKeys(myAccountCreate.myAnswer4);

                IWebElement myselectQuestion5 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[16]/div[1]/select"));
                var selectQuestion5 = new SelectElement(myselectQuestion5);
                selectQuestion5.SelectByValue(myAccountCreate.myQuestion5);

                IWebElement myselectAnswer5 = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[1]/form/div[16]/div[1]/input"));
                myselectAnswer5.SendKeys(myAccountCreate.myAnswer5);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement myclickNext = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[2]/button"));
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoProofing(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
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
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a/button"), myHistoryInfo);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement myclickNext = myDriver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a/button"));
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAccountLogin(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    myDriver = driver3;
                }
                else if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
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

        public int DoSignin(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
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
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                myDriver.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();

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
        public int DoApplyWithDiscounts(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
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

        public int DoApplyWithoutDiscounts(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
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

        public int DoMyAccount(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
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
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);

                    myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[2]/ul/li/a"), myHistoryInfo);

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

                    myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[1]/ul/li/a"), myHistoryInfo);

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
                    myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div[3]/div[2]/div[3]/div/div[2]/div/div/div[1]/div/div/h1[2]/ul/li/a"), myHistoryInfo);

                    IWebElement myAccount2 = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div[2]/div/div/div[1]/div/div/h1[2]/ul/li/a"));
                    myAccount2.Click();
                }

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myApp.DoWaitForElement(myDriver, By.TagName("iFrame"), myHistoryInfo);

                var iFrameElement = myDriver.FindElement(By.TagName("iFrame"));
                myDriver.SwitchTo().Frame(iFrameElement);
                   
                IWebElement myViewResults;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    myViewResults = myDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/div/div[1]/div/div/a"));
                }
                else
                {
                    myViewResults = myDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/div/div[1]/div/div/a")); //works for qhp16

                    //myViewResults = myDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/div[2]/div/div[1]/div/div/a"));//sometimes view results doesn't show right away
                }
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

        public int DoResumeApp(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                int appwait;
                ApplicationDo myApp = new ApplicationDo();               

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myDriver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(myDriver, By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/StandardUser_homePage.do')]"), myHistoryInfo);
                var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/StandardUser_homePage.do')]"));
                myDriver.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                myDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/a")).Click();
                System.Threading.Thread.Sleep(2000);

                resume = "Yes";

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

        public int DoHomePage(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    myDriver = driver3;
                }
                else if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
                }

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
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div/div/div/div/div[3]/a/button"), myHistoryInfo);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

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

        public int DoLoginTimeTravel(IWebDriver driver, IWebDriver driver3, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string resume)
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
                myDriver.SwitchTo().DefaultContent();

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
                
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"), myHistoryInfo);
                IWebElement textboxLogin = myDriver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"));
               
                textboxLogin.SendKeys(myAccountCreate.myUsername);

                IWebElement textboxPW = myDriver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input"));                
                textboxPW.SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement buttonSignIn = myDriver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]"));
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;

            }
        }

    }
}
