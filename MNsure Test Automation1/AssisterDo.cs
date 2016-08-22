using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using System.Data.Sql;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Support.UI;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;

namespace MNsure_Regression_1
{
    class AssisterDo
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoManagerLogin(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5, 
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo, 
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver3.Manage().Window.Maximize();
                /*driver1.Manage().Window.Size = new Size(0, 0);
                driver1.Manage().Window.Position = new System.Drawing.Point(1, 875);
                driver2.Manage().Window.Size = new Size(0, 0);
                driver2.Manage().Window.Position = new System.Drawing.Point(1, 875);*/

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                driver3.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("STSTPWSXK94RM");
                    myAccountCreate.myCaseWorkerLoginId = "STSTPWSXK94RM";
                    driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@12345");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("STSTPWSXK94RM");
                    myAccountCreate.myCaseWorkerLoginId = "USRST117";
                    driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@12345");
                }
                else
                {
                    driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("STSTPWSXK94RM");
                    myAccountCreate.myCaseWorkerLoginId = "cwceb01";
                    driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@12345");
                }

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoManagerLoginTimeTravel(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver3.Manage().Window.Maximize();
                /*driver1.Manage().Window.Size = new Size(0, 0);
                driver1.Manage().Window.Position = new System.Drawing.Point(1, 875);
                driver2.Manage().Window.Size = new Size(0, 0);
                driver2.Manage().Window.Position = new System.Drawing.Point(1, 875);*/

                driver3.SwitchTo().DefaultContent();

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
                    myApp.DoWaitForElement(driver3, By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"), myHistoryInfo);
                    textboxLogin = driver3.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"));
                }
                else
                {
                    myApp.DoWaitForElement(driver3, By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input"), myHistoryInfo);
                    textboxLogin = driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input"));
                }

                textboxLogin.SendKeys("STSTPWSXK94RM");

                IWebElement textboxPW;
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    textboxPW = driver3.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input"));
                }
                else
                {
                    textboxPW = driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input"));
                }
                textboxPW.SendKeys("Welcome@12345");

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                IWebElement buttonSignIn;
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    buttonSignIn = driver3.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]"));
                }
                else
                {
                    buttonSignIn = driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button"));
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
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }


        public int DoBrokerCitizenLogin(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            IWebDriver myDriver = driver1;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
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
                myDriver.Manage().Window.Maximize();
                
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button"), myHistoryInfo);
               
                myDriver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys(myAccountCreate.myUsername);
                myDriver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                myDriver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();

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

        public int DoBrokerCitizenLoginTimeTravel(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            IWebDriver myDriver = driver1;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
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
                myDriver.Manage().Window.Maximize();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"), myHistoryInfo);
                //myDriver.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();

                myDriver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input")).SendKeys(myAccountCreate.myUsername);
                myDriver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input")).SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                myDriver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]")).Click();

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

        public int DoSignin(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            IWebDriver myDriver = driver1;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
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
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                myDriver.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();
                
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

        public int DoBrokerAssisterLogin(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver4.Manage().Window.Maximize();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver4, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                driver4.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();//sign in button
                System.Threading.Thread.Sleep(2000);

                driver4.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys(myAccountCreate.myUsername);
                driver4.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(driver4, ref myHistoryInfo);

                driver4.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver4, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoBrokerAssisterLoginTimeTravel(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {            
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
                driver4.Manage().Window.Maximize();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver4, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                //myApp.DoWaitForElement(driver4, By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"), myHistoryInfo);
                driver4.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();
                System.Threading.Thread.Sleep(2000);

                driver4.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys(myAccountCreate.myUsername);
                driver4.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys(myAccountCreate.myPassword);
                //driver4.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input")).SendKeys(myAccountCreate.myUsername);
                //driver4.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input")).SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(driver4, ref myHistoryInfo);

                driver4.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();
                //driver4.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]")).Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver4, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoNavigatorLogin(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver5.Manage().Window.Maximize();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver5, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                driver5.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();//sign in button
                System.Threading.Thread.Sleep(2000);

                driver5.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys(myAccountCreate.myUsername);
                driver5.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(driver5, ref myHistoryInfo);

                driver5.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver5, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoNavigatorLoginTimeTravel(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver5.Manage().Window.Maximize();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver5, By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]"), myHistoryInfo);

                driver5.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input")).SendKeys(myAccountCreate.myUsername);
                driver5.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input")).SendKeys(myAccountCreate.myPassword);

                writeLogs.DoGetScreenshot(driver5, ref myHistoryInfo);

                driver5.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]")).Click();

                myHistoryInfo.myAssisterNavigator = "Yes";

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver5, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoManageAssister(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {               
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver1, By.XPath("/html/body/div[3]/div[1]/div/div/div[3]/ul/li[1]/a"), myHistoryInfo);

                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);

                driver1.FindElement(By.XPath("/html/body/div[3]/div[1]/div/div/div[3]/ul/li[1]/a")).Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAddAssister(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver1, By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/CitizenAccount_viewNavigatorDetailsWithoutNavPage.do')]"), myHistoryInfo);
                var iFrameElement = driver1.FindElement(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/CitizenAccount_viewNavigatorDetailsWithoutNavPage.do')]"));
                driver1.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);

                driver1.FindElement(By.XPath("/html/body/div[2]/div[3]/a[1]/span/span/span")).Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoHelping(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                driver1.Manage().Window.Maximize();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver1, By.Id("__o3id0"), myHistoryInfo);

                IWebElement textboxName = driver1.FindElement(By.Id("__o3id0"));
                textboxName.SendKeys(myAssister.myRefNumber);

                driver1.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search
                System.Threading.Thread.Sleep(6000);
                driver1.FindElement(By.XPath("/html/body/div[2]/form/div/div[6]/a/span/span/span")).Click();//next

                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAuthorization(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver1.Manage().Window.Maximize();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver1, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr/td[2]/div[2]/a[1]/span/span/span"), myHistoryInfo);

                driver1.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr/td[2]/div[2]/a[1]/span/span/span")).Click();//confirm
                System.Threading.Thread.Sleep(4000);
                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);

                driver1.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver1, By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/CitizenAccount_viewNavigatorDetailsWithoutNavPage.do')]"), myHistoryInfo);
                var iFrameElement = driver1.FindElement(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/CitizenAccount_viewNavigatorDetailsWithoutNavPage.do')]"));
                driver1.SwitchTo().Frame(iFrameElement);

                driver1.FindElement(By.XPath("/html/body/div[2]/div[3]/a[3]/span/span/span")).Click();//close
                System.Threading.Thread.Sleep(2000);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoApply(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver1, By.XPath("/html/body/div[2]/div[2]/div/a"), myHistoryInfo);

                driver1.FindElement(By.XPath("/html/body/div[2]/div[2]/div/a")).Click();//apply

                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);               

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }        

        public int DoProviders(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (16 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div[1]/div[4]/div[1]/div[4]/div/div[2]/div/div/div/span[1]"), myHistoryInfo);
                driver3.FindElement(By.XPath("/html/body/div[1]/div[4]/div[1]/div[4]/div/div[2]/div/div/div/span[1]")).Click();//providers and services

                IWebElement firstTab = driver3.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver3);
                var rClick = action.ContextClick(firstTab); //right click
                
                rClick.Perform();
                driver3.FindElement(By.XPath("//td[contains(text(), 'Close all tabs')]")).Click();
                System.Threading.Thread.Sleep(1000);

                driver3.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div")).Click();
                driver3.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[2]/div/div/div[1]/div[2]/div/ul/li[1]/a")).Click();//my providers

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSearch(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_listProvidersForResourceManagerPage.do')]"), myHistoryInfo);
                var iFrameElement = driver3.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_listProvidersForResourceManagerPage.do')]"));
                driver3.SwitchTo().Frame(iFrameElement);

                driver3.FindElement(By.XPath("/html/body/div[1]/div/div[2]/a")).Click();//enroll provider

                driver3.SwitchTo().DefaultContent();
                var iFrameElement2 = driver3.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_searchProviderNotRegisteredPage.do')]"));
                driver3.SwitchTo().Frame(iFrameElement2);

                driver3.FindElement(By.XPath("/html/body/div[4]/div[2]/a/span/span/span")).Click();//next

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoDetails(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_searchProviderNotRegisteredPage.do')]"), myHistoryInfo);
                var iFrameElement = driver3.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_searchProviderNotRegisteredPage.do')]"));
                driver3.SwitchTo().Frame(iFrameElement);

                IWebElement textboxName = driver3.FindElement(By.Id("__o3id0"));
                textboxName.SendKeys(myAssister.myFirstName + " " + myAssister.myLastName);

                IWebElement textboxId = driver3.FindElement(By.Id("__o3id4"));
                textboxId.SendKeys(myAssister.AssisterId);//this must be unique

                if (myAssister.myAptSuite != null)
                {
                    IWebElement textboxApt = driver3.FindElement(By.Id("__o3id6"));
                    textboxApt.SendKeys(myAssister.myAptSuite);
                }
                IWebElement textboxStreet1 = driver3.FindElement(By.Id("__o3id7"));
                textboxStreet1.SendKeys(myAssister.myAddress1);

                if (myAssister.myAddress2 != null)
                {
                    IWebElement textboxStreet2 = driver3.FindElement(By.Id("__o3id8"));
                    textboxStreet2.SendKeys(myAssister.myAddress2);
                }
                IWebElement textboxCity = driver3.FindElement(By.Id("__o3id9"));
                textboxCity.SendKeys(myAssister.myCity);

                IWebElement textboxCounty = driver3.FindElement(By.Id("__o3ida"));
                textboxCounty.SendKeys(myAssister.myCounty);

                IWebElement textboxState = driver3.FindElement(By.Id("__o3idb"));
                textboxState.SendKeys(myAssister.myState);

                IWebElement textboxZip = driver3.FindElement(By.Id("__o3idc"));
                textboxZip.SendKeys(myAssister.myZip);

                IWebElement textboxCategory = driver3.FindElement(By.Id("__o3id17_0"));
                textboxCategory.SendKeys(myAssister.myCategory);
                textboxZip.Click();

                IWebElement textboxType = driver3.FindElement(By.XPath("/html/body/div[3]/form/div/div[7]/div/table/tbody/tr/td/table/tbody/tr[2]/td/div/div[3]/input[1]"));
                textboxType.SendKeys(myAssister.myType);

                driver3.FindElement(By.XPath("/html/body/div[4]/div[2]/a[2]/span/span/span")).Click();//finish

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoContact(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[10]/div/div/div/span[1]"), myHistoryInfo);
                driver3.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[10]/div/div/div/span[1]")).Click();//contact

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoEmail(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[2]/div[10]/div/ul/li[5]/div"), myHistoryInfo);
                driver3.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[2]/div[10]/div/ul/li[5]/div")).Click();//email

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoNewEmail(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_listEmailAddressPage.do')]"), myHistoryInfo);
                var iFrameElement = driver3.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_listEmailAddressPage.do')]"));
                driver3.SwitchTo().Frame(iFrameElement);

                driver3.FindElement(By.XPath("/html/body/div[1]/div/div[2]/a")).Click();//new
                System.Threading.Thread.Sleep(2000);

                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("//iframe[contains(@src,'en_US/Participant_createEmailAddressPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver3.FindElement(By.XPath("//iframe[contains(@src,'en_US/Participant_createEmailAddressPage.do')]"));
                driver3.SwitchTo().Frame(iFrameElement2);

                IWebElement textboxAddress = driver3.FindElement(By.Id("__o3id0"));
                textboxAddress.SendKeys(myAssister.myEmail);

                driver3.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click();//save

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoHome(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[1]/div/div/div/span[1]"), myHistoryInfo);
                driver3.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[1]/div/div/div/span[1]")).Click();//home

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoApprove(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_providerHomePage.do')]"), myHistoryInfo);
                var iFrameElement = driver3.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_providerHomePage.do')]"));
                driver3.SwitchTo().Frame(iFrameElement);

                driver3.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                System.Threading.Thread.Sleep(1000);
                driver3.FindElement(By.XPath("//td[contains(text(), 'Approve')]")).Click(); //approve   

                driver3.SwitchTo().DefaultContent();
                var iFrameElement2 = driver3.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_approveProviderPage.do')]"));
                driver3.SwitchTo().Frame(iFrameElement2);

                System.Threading.Thread.Sleep(1000);
                driver3.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //yes

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                System.Threading.Thread.Sleep(4000);

                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_viewProviderTabDetailsPage.do')]"), myHistoryInfo);
                var iFrameElement3 = driver3.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_viewProviderTabDetailsPage.do')]"));
                driver3.SwitchTo().Frame(iFrameElement3);

                IWebElement myReference = driver3.FindElement(By.XPath("/html/body/div[4]/div/div[2]/div/div[2]/div[2]"));
                myAssister.myRefNumber = myReference.Text;
                DoUpdateRefNumber(myHistoryInfo, myAssister.myRefNumber);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoProofing(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                ApplicationDo myApp = new ApplicationDo();
                driver3.SwitchTo().DefaultContent();      
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a/button"), myHistoryInfo);
                driver3.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a/button")).Click();               

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoPrivacy(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[3]/input"), myHistoryInfo);

                IWebElement checkBoxAgree = driver3.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[3]/input"));
                checkBoxAgree.Click();

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                IWebElement myAccept = driver3.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
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
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoIdentityInformation(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button[2]"), myHistoryInfo);
                IWebElement myselectFirstName = driver3.FindElement(By.Id("first_name"));
                myselectFirstName.SendKeys(myAssister.myFirstName);

                IWebElement myselectLastName = driver3.FindElement(By.Id("last_name"));
                myselectLastName.SendKeys(myAssister.myLastName);

                IWebElement myselectAddress1 = driver3.FindElement(By.Id("street_address"));
                myselectAddress1.SendKeys(myAssister.myAddress1);

                IWebElement myselectAddress2 = driver3.FindElement(By.Id("street_address_2"));
                if (myAssister.myAddress2 != null)
                {
                    myselectAddress2.SendKeys(myAssister.myAddress2);
                }

                IWebElement myselectCity = driver3.FindElement(By.Id("city"));
                myselectCity.SendKeys(myAssister.myCity);

                IWebElement myselectState = driver3.FindElement(By.Id("state"));
                myselectState.SendKeys(myAssister.myState);

                IWebElement myselectZip = driver3.FindElement(By.Id("zip"));
                myselectZip.SendKeys(myAssister.myZip);

                IWebElement myselectEmail = driver3.FindElement(By.Id("email"));
                myselectEmail.SendKeys(myAssister.myEmail);

                IWebElement myselectEmail2 = driver3.FindElement(By.Id("reenteremail"));
                myselectEmail2.SendKeys(myAssister.myEmail);

                IWebElement myselectPhone = driver3.FindElement(By.Id("phone_number"));
                string mysPhone1 = myAssister.myPhoneNum.Substring(0, 3);
                string mysPhone2 = myAssister.myPhoneNum.Substring(3, 3);
                string mysPhone3 = myAssister.myPhoneNum.Substring(6, 4);
                myselectPhone.Click();
                myselectPhone.SendKeys(mysPhone1 + mysPhone2 + mysPhone3);

                IWebElement mySSN1 = driver3.FindElement(By.Id("ssn_1"));
                mySSN1.SendKeys(myAssister.mySSN.Substring(0, 3));

                IWebElement mySSN2 = driver3.FindElement(By.Id("ssn_2"));
                mySSN2.SendKeys(myAssister.mySSN.Substring(3, 2));

                IWebElement mySSN3 = driver3.FindElement(By.Id("ssn_3"));
                mySSN3.SendKeys(myAssister.mySSN.Substring(5, 4));

                IWebElement myDOB = driver3.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[8]/div[1]/input[5]"));
                myDOB.Click();
                string tempDOB;
                tempDOB = Convert.ToString(myAssister.myDOB);
                tempDOB = DateTime.Parse(tempDOB).ToString("MM/dd/yyyy");
                myDOB.SendKeys(tempDOB);
                                
                IWebElement myRegNumber = driver3.FindElement(By.Id("registration_number"));                
                RegistrationNumberForm _RegistrationNumber = new RegistrationNumberForm();               
                DialogResult dialogResult = _RegistrationNumber.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    myRegNumber.SendKeys(_RegistrationNumber.RegistrationNumber);
                }                

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                IWebElement clickNextButton = driver3.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button[2]"));
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
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }        

        public int DoAccountCreate(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
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
                driver3.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver3, By.Id("user_name"), myHistoryInfo);

                IWebElement myselectUserName = driver3.FindElement(By.Id("user_name"));
                myselectUserName.SendKeys(myAccountCreate.myUsername);

                IWebElement myselectPassword = driver3.FindElement(By.Id("user_password"));
                myselectPassword.SendKeys(myAccountCreate.myPassword);

                IWebElement myselectPassword2 = driver3.FindElement(By.Id("reenter_password"));
                myselectPassword2.SendKeys(myAccountCreate.myPassword);

                IWebElement myselectSecret = driver3.FindElement(By.Id("shared_secret"));
                myselectSecret.SendKeys(myAccountCreate.mySecret);

                string temp1;
                temp1 = myAccountCreate.myQuestion1;
                IWebElement myselectQuestion1 = driver3.FindElement(By.Id("ques1"));
                var selectQuestion1 = new SelectElement(myselectQuestion1);
                selectQuestion1.SelectByValue(myAccountCreate.myQuestion1);

                IWebElement myselectAnswer1 = driver3.FindElement(By.Id("answer1"));
                myselectAnswer1.SendKeys(myAccountCreate.myAnswer1);

                IWebElement myselectQuestion2 = driver3.FindElement(By.Id("ques2"));
                var selectQuestion2 = new SelectElement(myselectQuestion2);
                selectQuestion2.SelectByValue(myAccountCreate.myQuestion2);

                IWebElement myselectAnswer2 = driver3.FindElement(By.Id("answer2"));
                myselectAnswer2.SendKeys(myAccountCreate.myAnswer2);

                IWebElement myselectQuestion3 = driver3.FindElement(By.Id("ques3"));
                var selectQuestion3 = new SelectElement(myselectQuestion3);
                selectQuestion3.SelectByValue(myAccountCreate.myQuestion3);

                IWebElement myselectAnswer3 = driver3.FindElement(By.Id("answer3"));
                myselectAnswer3.SendKeys(myAccountCreate.myAnswer3);

                IWebElement myselectQuestion4 = driver3.FindElement(By.Id("ques4"));
                var selectQuestion4 = new SelectElement(myselectQuestion4);
                selectQuestion4.SelectByValue(myAccountCreate.myQuestion4);

                IWebElement myselectAnswer4 = driver3.FindElement(By.Id("answer4"));
                myselectAnswer4.SendKeys(myAccountCreate.myAnswer4);

                IWebElement myselectQuestion5 = driver3.FindElement(By.Id("ques5"));
                var selectQuestion5 = new SelectElement(myselectQuestion5);
                selectQuestion5.SelectByValue(myAccountCreate.myQuestion5);

                IWebElement myselectAnswer5 = driver3.FindElement(By.Id("answer5"));
                myselectAnswer5.SendKeys(myAccountCreate.myAnswer5);

                /*IWebElement myselectAddress1 = driver3.FindElement(By.Id("business_street_address"));
                myselectAddress1.SendKeys(myAssister.myAddress1);

                IWebElement myselectAddress2 = driver3.FindElement(By.Id("business_street_address_2"));
                if (myAssister.myAddress2 != null)
                {
                    myselectAddress2.SendKeys(myAssister.myAddress2);
                }

                IWebElement myselectCity = driver3.FindElement(By.Id("city"));
                myselectCity.SendKeys(myAssister.myCity);

                IWebElement myselectState = driver3.FindElement(By.Id("business_state"));
                myselectState.SendKeys(myAssister.myState);

                IWebElement myselectZip = driver3.FindElement(By.Id("business_zip"));
                myselectZip.SendKeys(myAssister.myZip);

                IWebElement myselectLicense = driver3.FindElement(By.Id("mn_state_license"));
                myselectLicense.SendKeys("5404961");

                IWebElement myselectNPN = driver3.FindElement(By.Id("national_producer_number"));
                myselectNPN.SendKeys("54894618");*/

                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);

                IWebElement myclickNext = driver3.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
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
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoUpdateRefNumber(mystructHistoryInfo myHistoryInfo, string updateValue)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;


            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand(
                    "SELECT * FROM Assister where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Assister set RefNumber = @RefNumber where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com2 = new SqlCeCommand(myUpdateString, con))
                        {
                            com2.Parameters.AddWithValue("RefNumber", updateValue);
                            com2.ExecuteNonQuery();
                            com2.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update pass count didn't work");
            }
            return 1;
        }


    }
}
