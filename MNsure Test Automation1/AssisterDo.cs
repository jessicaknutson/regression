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

        public int DoLogin(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver2.Manage().Window.Maximize();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver2, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                driver2.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver2.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("STSTPWSXK94RM");
                    myAccountCreate.myCaseWorkerLoginId = "STSTPWSXK94RM";
                    driver2.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@87654");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver2.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("STSTPWSXK94RM");
                    myAccountCreate.myCaseWorkerLoginId = "USRST117";
                    driver2.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@87654");
                }
                else
                {
                    driver2.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("STSTPWSXK94RM");
                    myAccountCreate.myCaseWorkerLoginId = "cwceb01";
                    driver2.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@87654");
                }

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                driver2.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoBrokerLogin(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                driver3.Manage().Window.Maximize();
                
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver3, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                driver3.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();//sign in button
                System.Threading.Thread.Sleep(2000);

                driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys(myAccountCreate.myUsername);
                driver3.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys(myAccountCreate.myPassword);

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

        public int DoCitizenGenericLogin(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                myApp.DoWaitForElement(driver1, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                driver1.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();//sign in button
                System.Threading.Thread.Sleep(2000);

                driver1.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("usrst117");
                driver1.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@12345");

                writeLogs.DoGetScreenshot(driver1, ref myHistoryInfo);

                driver1.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();

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

        public int DoProviders(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                int appwait;

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("/html/body/div[1]/div[4]/div[1]/div[4]/div/div[2]/div/div/div/span[1]"), myHistoryInfo);
                driver2.FindElement(By.XPath("/html/body/div[1]/div[4]/div[1]/div[4]/div/div[2]/div/div/div/span[1]")).Click();//providers and services

                IWebElement firstTab = driver2.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver2);
                var rClick = action.ContextClick(firstTab); //right click
                //System.Threading.Thread.Sleep(2000);
                rClick.Perform();
                driver2.FindElement(By.XPath("//td[contains(text(), 'Close all tabs')]")).Click();
                System.Threading.Thread.Sleep(1000);

                driver2.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div")).Click();
                driver2.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[2]/div/div/div[1]/div[2]/div/ul/li[1]/a")).Click();//my providers

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSearch(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_listProvidersForResourceManagerPage.do')]"), myHistoryInfo);
                var iFrameElement = driver2.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_listProvidersForResourceManagerPage.do')]"));
                driver2.SwitchTo().Frame(iFrameElement);

                driver2.FindElement(By.XPath("/html/body/div[1]/div/div[2]/a")).Click();//enroll provider

                driver2.SwitchTo().DefaultContent();
                var iFrameElement2 = driver2.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_searchProviderNotRegisteredPage.do')]"));
                driver2.SwitchTo().Frame(iFrameElement2);

                driver2.FindElement(By.XPath("/html/body/div[4]/div[2]/a/span/span/span")).Click();//next

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoDetails(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_searchProviderNotRegisteredPage.do')]"), myHistoryInfo);
                var iFrameElement = driver2.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_searchProviderNotRegisteredPage.do')]"));
                driver2.SwitchTo().Frame(iFrameElement);

                IWebElement textboxName = driver2.FindElement(By.Id("__o3id0"));
                textboxName.SendKeys(myAssister.myFirstName + " " + myAssister.myLastName);

                IWebElement textboxId = driver2.FindElement(By.Id("__o3id4"));
                textboxId.SendKeys(myAssister.AssisterId);//this must be unique

                IWebElement textboxApt = driver2.FindElement(By.Id("__o3id6"));
                textboxApt.SendKeys(myAssister.myAptSuite);

                IWebElement textboxStreet1 = driver2.FindElement(By.Id("__o3id7"));
                textboxStreet1.SendKeys(myAssister.myAddress1);

                IWebElement textboxStreet2 = driver2.FindElement(By.Id("__o3id8"));
                textboxStreet2.SendKeys(myAssister.myAddress2);

                IWebElement textboxCity = driver2.FindElement(By.Id("__o3id9"));
                textboxCity.SendKeys(myAssister.myCity);

                IWebElement textboxCounty = driver2.FindElement(By.Id("__o3ida"));
                textboxCounty.SendKeys(myAssister.myCounty);

                IWebElement textboxState = driver2.FindElement(By.Id("__o3idb"));
                textboxState.SendKeys(myAssister.myState);

                IWebElement textboxZip = driver2.FindElement(By.Id("__o3idc"));
                textboxZip.SendKeys(myAssister.myZip);

                IWebElement textboxCategory = driver2.FindElement(By.Id("__o3id17_0"));
                textboxCategory.SendKeys(myAssister.myCategory);
                textboxZip.Click();

                IWebElement textboxType = driver2.FindElement(By.XPath("/html/body/div[3]/form/div/div[7]/div/table/tbody/tr/td/table/tbody/tr[2]/td/div/div[3]/input[1]"));
                textboxType.SendKeys(myAssister.myType);

                driver2.FindElement(By.XPath("/html/body/div[4]/div[2]/a[2]/span/span/span")).Click();//finish

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoContact(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[10]/div/div/div/span[1]"), myHistoryInfo);
                driver2.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[10]/div/div/div/span[1]")).Click();//contact

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoEmail(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[2]/div[10]/div/ul/li[5]/div"), myHistoryInfo);
                driver2.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[2]/div[10]/div/ul/li[5]/div")).Click();//email

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoNewEmail(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_listEmailAddressPage.do')]"), myHistoryInfo);
                var iFrameElement = driver2.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_listEmailAddressPage.do')]"));
                driver2.SwitchTo().Frame(iFrameElement);

                driver2.FindElement(By.XPath("/html/body/div[1]/div/div[2]/a")).Click();//new
                System.Threading.Thread.Sleep(2000);

                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("//iframe[contains(@src,'en_US/Participant_createEmailAddressPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver2.FindElement(By.XPath("//iframe[contains(@src,'en_US/Participant_createEmailAddressPage.do')]"));
                driver2.SwitchTo().Frame(iFrameElement2);

                IWebElement textboxAddress = driver2.FindElement(By.Id("__o3id0"));
                textboxAddress.SendKeys(myAssister.myEmail);

                driver2.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click();//save

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoHome(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[1]/div/div/div/span[1]"), myHistoryInfo);
                driver2.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[1]/div/div/div/span[1]")).Click();//home

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoApprove(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }

                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_providerHomePage.do')]"), myHistoryInfo);
                var iFrameElement = driver2.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_providerHomePage.do')]"));
                driver2.SwitchTo().Frame(iFrameElement);

                driver2.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                System.Threading.Thread.Sleep(1000);
                driver2.FindElement(By.XPath("//td[contains(text(), 'Approve')]")).Click(); //approve   

                driver2.SwitchTo().DefaultContent();
                var iFrameElement2 = driver2.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProviderManagement_approveProviderPage.do')]"));
                driver2.SwitchTo().Frame(iFrameElement2);

                driver2.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //yes

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoProofing(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                ApplicationDo myApp = new ApplicationDo();
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a[2]/button"), myHistoryInfo);
                driver2.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/a[2]/button")).Click();               

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoPrivacy(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"), myHistoryInfo);

                IWebElement checkBoxAgree = driver2.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[1]/div[3]/input"));
                checkBoxAgree.Click();

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                IWebElement myAccept = driver2.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
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
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoIdentityInformation(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"), myHistoryInfo);
                IWebElement myselectFirstName = driver2.FindElement(By.Id("first_name"));
                myselectFirstName.SendKeys(myAssister.myFirstName);

                IWebElement myselectLastName = driver2.FindElement(By.Id("last_name"));
                myselectLastName.SendKeys(myAssister.myLastName);

                IWebElement myselectAddress1 = driver2.FindElement(By.Id("street_address"));
                myselectAddress1.SendKeys(myAssister.myAddress1);

                IWebElement myselectAddress2 = driver2.FindElement(By.Id("street_address_2"));
                if (myAssister.myAddress2 != null)
                {
                    myselectAddress2.SendKeys(myAssister.myAddress2);
                }

                IWebElement myselectCity = driver2.FindElement(By.Id("city"));
                myselectCity.SendKeys(myAssister.myCity);

                IWebElement myselectState = driver2.FindElement(By.Id("state"));
                myselectState.SendKeys(myAssister.myState);

                IWebElement myselectZip = driver2.FindElement(By.Id("zip"));
                myselectZip.SendKeys(myAssister.myZip);

                IWebElement myselectEmail = driver2.FindElement(By.Id("email"));
                myselectEmail.SendKeys(myAssister.myEmail);

                IWebElement myselectEmail2 = driver2.FindElement(By.Id("reenteremail"));
                myselectEmail2.SendKeys(myAssister.myEmail);

                IWebElement myselectPhone = driver2.FindElement(By.Id("phone_number"));
                string mysPhone1 = myAssister.myPhoneNum.Substring(0, 3);
                string mysPhone2 = myAssister.myPhoneNum.Substring(3, 3);
                string mysPhone3 = myAssister.myPhoneNum.Substring(6, 4);
                myselectPhone.SendKeys("(" + mysPhone1 + ")" + mysPhone2 + "-" + mysPhone3);

                IWebElement myselectCaptcha = driver2.FindElement(By.Id("recaptcha_response_field"));
                myselectCaptcha.SendKeys("Google");

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                IWebElement clickNextButton = driver2.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/form/div[2]/button"));
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
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAccountCreate(IWebDriver driver1, IWebDriver driver2, IWebDriver driver3, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                driver2.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver2, By.Id("user_name"), myHistoryInfo);

                IWebElement myselectUserName = driver2.FindElement(By.Id("user_name"));
                myselectUserName.SendKeys(myAccountCreate.myUsername);

                IWebElement myselectPassword = driver2.FindElement(By.Id("user_password"));
                myselectPassword.SendKeys(myAccountCreate.myPassword);

                IWebElement myselectPassword2 = driver2.FindElement(By.Id("reenter_password"));
                myselectPassword2.SendKeys(myAccountCreate.myPassword);

                IWebElement myselectSecret = driver2.FindElement(By.Id("shared_secret"));
                myselectSecret.SendKeys(myAccountCreate.mySecret);

                string temp1;
                temp1 = myAccountCreate.myQuestion1;
                IWebElement myselectQuestion1 = driver2.FindElement(By.Id("ques1"));
                var selectQuestion1 = new SelectElement(myselectQuestion1);
                selectQuestion1.SelectByValue(myAccountCreate.myQuestion1);

                IWebElement myselectAnswer1 = driver2.FindElement(By.Id("answer1"));
                myselectAnswer1.SendKeys(myAccountCreate.myAnswer1);

                IWebElement myselectQuestion2 = driver2.FindElement(By.Id("ques2"));
                var selectQuestion2 = new SelectElement(myselectQuestion2);
                selectQuestion2.SelectByValue(myAccountCreate.myQuestion2);

                IWebElement myselectAnswer2 = driver2.FindElement(By.Id("answer2"));
                myselectAnswer2.SendKeys(myAccountCreate.myAnswer2);

                IWebElement myselectQuestion3 = driver2.FindElement(By.Id("ques3"));
                var selectQuestion3 = new SelectElement(myselectQuestion3);
                selectQuestion3.SelectByValue(myAccountCreate.myQuestion3);

                IWebElement myselectAnswer3 = driver2.FindElement(By.Id("answer3"));
                myselectAnswer3.SendKeys(myAccountCreate.myAnswer3);

                IWebElement myselectQuestion4 = driver2.FindElement(By.Id("ques4"));
                var selectQuestion4 = new SelectElement(myselectQuestion4);
                selectQuestion4.SelectByValue(myAccountCreate.myQuestion4);

                IWebElement myselectAnswer4 = driver2.FindElement(By.Id("answer4"));
                myselectAnswer4.SendKeys(myAccountCreate.myAnswer4);

                IWebElement myselectQuestion5 = driver2.FindElement(By.Id("ques5"));
                var selectQuestion5 = new SelectElement(myselectQuestion5);
                selectQuestion5.SelectByValue(myAccountCreate.myQuestion5);

                IWebElement myselectAnswer5 = driver2.FindElement(By.Id("answer5"));
                myselectAnswer5.SendKeys(myAccountCreate.myAnswer5);

                IWebElement myselectAddress1 = driver2.FindElement(By.Id("business_street_address"));
                myselectAddress1.SendKeys(myAssister.myAddress1);

                IWebElement myselectAddress2 = driver2.FindElement(By.Id("business_street_address_2"));
                if (myAssister.myAddress2 != null)
                {
                    myselectAddress2.SendKeys(myAssister.myAddress2);
                }

                IWebElement myselectCity = driver2.FindElement(By.Id("city"));
                myselectCity.SendKeys(myAssister.myCity);

                IWebElement myselectState = driver2.FindElement(By.Id("business_state"));
                myselectState.SendKeys(myAssister.myState);

                IWebElement myselectZip = driver2.FindElement(By.Id("business_zip"));
                myselectZip.SendKeys(myAssister.myZip);

                IWebElement myselectLicense = driver2.FindElement(By.Id("mn_state_license"));
                myselectLicense.SendKeys("5404961");

                IWebElement myselectNPN = driver2.FindElement(By.Id("national_producer_number"));
                myselectNPN.SendKeys("54894618");

                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);

                IWebElement myclickNext = driver2.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div[2]/button"));
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
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }


    }
}
