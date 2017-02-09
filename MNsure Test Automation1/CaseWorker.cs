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
    class CaseWorker
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoCaseWorkerLogin(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                driver.Manage().Window.Maximize();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("USR9889");
                    myAccountCreate.myCaseWorkerLoginId = "USR9889";
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome123#");
                    /* Please do not use this username. It was created solely for use in the mets automation application in STST.
                     * if this user needs to be changed here are the key pieces of information on this user: 
                     * email: greg.pesall@state.mn.us, shared secret: Pontiac, Q1: Pontiac, Q2: Mark, Q3: Twins, Q4: Grand Rapids, Q5: Lasagna
                     * */
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("USR9889S2");
                    myAccountCreate.myCaseWorkerLoginId = "USR9889S2";
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome123#");
                    /* Please do not use this username. It was created solely for use in the mets automation application in STST2.
                     * if this user needs to be changed here are the key pieces of information on this user: 
                     * email: greg.pesall@state.mn.us, shared secret: Pontiac, Q1: Pontiac, Q2: Mark, Q3: Twins, Q4: Grand Rapids, Q5: Lasagna
                     * */
                }
                else
                {
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("soacw200");
                    myAccountCreate.myCaseWorkerLoginId = "soacw200";
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome10#");

                }
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();

                myEnrollment.myPassCount = "1";//reset count back to 1 on start in case an error happened during previous run
                myApp.DoUpdateAppPassCount(myHistoryInfo, myEnrollment.myPassCount);
                DoUpdateCWUserName(myHistoryInfo, myAccountCreate.myCaseWorkerLoginId);

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

        public int DoCaseWorkerLoginTimeTravel(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                driver.Manage().Window.Maximize();
                ApplicationDo myApp = new ApplicationDo();

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("USR9889");
                    myAccountCreate.myCaseWorkerLoginId = "USR9889";
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome123#");
                    /* Please do not use this username. It was created solely for use in the mets automation application in STST.
                     * if this user needs to be changed here are the key pieces of information on this user: 
                     * email: greg.pesall@state.mn.us, shared secret: Pontiac, Q1: Pontiac, Q2: Mark, Q3: Twins, Q4: Grand Rapids, Q5: Lasagna
                     * */
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input")).SendKeys("USR9889S2");
                    myAccountCreate.myCaseWorkerLoginId = "USR9889S2";
                    driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input")).SendKeys("Welcome123#");
                    /* Please do not use this username. It was created solely for use in the mets automation application in STST2.
                     * if this user needs to be changed here are the key pieces of information on this user: 
                     * email: greg.pesall@state.mn.us, shared secret: Pontiac, Q1: Pontiac, Q2: Mark, Q3: Twins, Q4: Grand Rapids, Q5: Lasagna
                     * */
                } else
                {
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("soacw200");
                    myAccountCreate.myCaseWorkerLoginId = "soacw200";
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome10#");
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]")).Click();

                DoUpdateCWUserName(myHistoryInfo, myAccountCreate.myCaseWorkerLoginId);

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

        public int DoHCRCases(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                int appwait;

                if (myEnrollment.myPassCount == "1")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        if (myHistoryInfo.myBrowser == "Chrome")
                        {
                            appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (16 + myHistoryInfo.myAppWait) * 1000;
                        }
                    }
                    else
                    {
                        if (myHistoryInfo.myBrowser == "Chrome")
                        {
                            appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (16 + myHistoryInfo.myAppWait) * 1000;
                        }
                    }
                }
                else
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        if (myHistoryInfo.myBrowser == "Chrome")
                        {
                            appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (16 + myHistoryInfo.myAppWait) * 1000;
                        }
                    }
                    else
                    {
                        if (myHistoryInfo.myBrowser == "Chrome")
                        {
                            appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (16 + myHistoryInfo.myAppWait) * 1000;
                        }
                    }
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel"), myHistoryInfo);
                driver.FindElement(By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel")).Click();//hcr cases tab

                var present = false;
                try
                {
                    present = driver.FindElement(By.LinkText("Person…")).Displayed;
                    present = driver.FindElement(By.LinkText("Person…")).Enabled;
                }
                catch (NoSuchElementException)
                {
                    present = false;
                }

                if (present == false)
                {
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div")).Click(); //shortcuts tab                    
                }
                System.Threading.Thread.Sleep(1000);
                driver.FindElement(By.Id("dijit_layout_AccordionPane_1_button")).Click();//searches   
                System.Threading.Thread.Sleep(1000);
                driver.FindElement(By.LinkText("Person…")).Click();

                IWebElement firstSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[1]"), myHistoryInfo);
                firstSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[1]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(firstSearchTab); //right click
                System.Threading.Thread.Sleep(2000);
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close all tabs')]")).Click();

                System.Threading.Thread.Sleep(1000);

                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"), myHistoryInfo);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);
                System.Threading.Thread.Sleep(1000);

                if (myEnrollment.mySSN == "Yes")
                {
                    IWebElement textboxSSN = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[1]/div/table/tbody/tr/td[1]/input"));
                    textboxSSN.Clear();
                    if (myEnrollment.myDay2TestId != null)
                    {
                        textboxSSN.SendKeys(DoDay2PrimarySSN(myEnrollment.myDay2TestId)); //search for primary member
                    }
                    else
                    {
                        if (myEnrollment.myApplyYourself == "No")
                        {
                            int temp = Convert.ToInt32(myEnrollment.mySSNNum) + 1;
                            textboxSSN.SendKeys(Convert.ToString(temp));
                        }
                        else
                        {
                            string hhssn = myEnrollment.mySSNNum;
                            textboxSSN.SendKeys(hhssn);
                            //DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                            //textboxSSN.SendKeys("344688097"); 
                        }
                    }
                }
                else
                {
                    IWebElement textboxFirst = driver.FindElement(By.Id("__o3id1"));
                    textboxFirst.Clear();
                    textboxFirst.SendKeys(myEnrollment.myFirstName);
                    IWebElement textboxLast = driver.FindElement(By.Id("__o3id3"));
                    textboxLast.Clear();
                    textboxLast.SendKeys(myEnrollment.myLastName);
                    IWebElement textboxDOB = driver.FindElement(By.Id("__o3id5"));
                    textboxDOB.Clear();
                    textboxDOB.SendKeys(myEnrollment.myDOB);
                }
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoPersonHome(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"), myHistoryInfo);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click();

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homeTabDetailsPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homeTabDetailsPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement mnSureID = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[2]/div/div[2]/div[2]"));
                returnMNSureID = mnSureID.Text;

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

        public int DoRegisterPerson(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (20 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (20 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel"), myHistoryInfo);

                driver.FindElement(By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel")).Click();//hcr cases tab
                driver.FindElement(By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel")).Click();//hcr cases tab, this doesn't always work, not sure why

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div")).Click(); //shortcuts tab
                System.Threading.Thread.Sleep(1000);
                driver.FindElement(By.Id("dijit_layout_AccordionPane_1_button")).Click();//searches
                System.Threading.Thread.Sleep(1000);
                driver.FindElement(By.LinkText("Person…")).Click();
                System.Threading.Thread.Sleep(1000);
                IWebElement personSearchTab;
                personSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[1]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(personSearchTab); //right click
                System.Threading.Thread.Sleep(1000);
                rClick.Perform();
                System.Threading.Thread.Sleep(1000);
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.Id("curam_widget_MenuItem_2_text")).Click();//close all tabs
                }
                else
                {
                    driver.FindElement(By.XPath("/html/body/div[3]/table/tbody/tr[2]/td[1]")).Click();//close all tabs
                }
                System.Threading.Thread.Sleep(1000);

                driver.FindElement(By.LinkText("Person…")).Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div/div/div/div[2]/div/div/div/span/span/span/span[2]"), myHistoryInfo);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div/div/div/div[2]/div/div/div/span/span/span/span[2]")).Click();//actions
                //System.Threading.Thread.Sleep(1000); 
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    //driver.FindElement(By.XPath("/html/body/div[5]/table/tbody/tr[2]/td[2]")).Click(); //keeps changing
                    //OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);                   
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                }
                else
                {
                    driver.FindElement(By.XPath("//td[contains(text(), 'Register Person…')]")).Click();
                }
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

        public int DoNotification(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[2]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[2]")).Click();//select person tab   

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[2]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[7]/div/div/div/span[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[2]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[7]/div/div/div/span[1]")).Click();//select notification tab

                // TFR 11-10-2016 There is no data so the drop down cannot be accessed. add method to wait for notification                
                System.Threading.Thread.Sleep(3000);
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIX_listNoticesPage.do')]"), myHistoryInfo);
                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIX_listNoticesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//select down arrow
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'MNHIX_viewNoticePage.do')]"), myHistoryInfo);
                var iFrameElement5 = driver.FindElement(By.XPath("//iframe[contains(@src,'MNHIX_viewNoticePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement5);
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[3]/div[5]/div/table/tbody/tr/td[1]/a"), myHistoryInfo);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[3]/div[5]/div/table/tbody/tr/td[1]/a")).Click();//select pdf link

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                /*string pdfpath = @"C:\Mnsure Regression 1\EligibilityNotice_eec97f1453479993212.pdf";
                ProcessStartInfo psi = new ProcessStartInfo(pdfpath);
                Process firefoxProcess = Process.Start(psi);

                MessageBox.Show(new Form() { TopMost = true }, "Please Open or Save PDF.",
                "Open or Save PDF", MessageBoxButtons.OK, MessageBoxIcon.Stop);*/

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

        public int DoIAHome(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                DoWaitForElementRefresh(driver, By.XPath("//a[contains(@href,'HCRIC_home')]"), By.XPath("/html/body/div[1]/div/div[3]/a[1]"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("//a[contains(@href,'HCRIC_home')]")).Click(); //select insurance affordability

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRIC_homePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRIC_homePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]"), myHistoryInfo);
                IWebElement myIcnum = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]"));
                returnICNumber = myIcnum.Text;

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoEvidenceIA(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click();

                System.Threading.Thread.Sleep(3000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoLifeEventsAdd(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[2]/div[3]/div/div/ul/li[8]/div"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[2]/div[3]/div/div/ul/li[8]/div")).Click();
                System.Threading.Thread.Sleep(3000);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_listCaseMemberPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindNonPrimaryLifeEvent(driver, primaryName);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_listCaseMemberPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'MNHIXIntegratedCase_viewLifeEventsPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);
                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/div/table/tbody/tr[3]/td[4]/span/span/span")).Click(); //add hh member wizard  
                driver.FindElement(By.XPath("//td[contains(text(), 'Launch…')]")).Click();
                System.Threading.Thread.Sleep(2000);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().DefaultContent();
                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);
                driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //continue 
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoLifeEventsRemove(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[2]/div[3]/div/div/ul/li[8]/div"), myHistoryInfo);
                IWebElement lifeevents = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[2]/div[3]/div/div/ul/li[8]/div"));
                lifeevents.Click();
                System.Threading.Thread.Sleep(3000);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_listCaseMemberPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindNonPrimaryLifeEvent(driver, primaryName);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_listCaseMemberPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'MNHIXIntegratedCase_viewLifeEventsPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);
                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/div/table/tbody/tr[2]/td[4]/span/span/span")).Click(); //remove hh member wizard  
                driver.FindElement(By.XPath("//td[contains(text(), 'Launch…')]")).Click();
                System.Threading.Thread.Sleep(2000);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().DefaultContent();
                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                IWebElement textboxEndDate = driver.FindElement(By.Id("__o3id6"));
                textboxEndDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                driver.FindElement(By.XPath("/html/body/div/div[2]/a/span/span/span")).Click(); //next
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoLifeEventsCoverage(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[2]/div[3]/div/div/ul/li[8]/div"), myHistoryInfo);
                IWebElement lifeevents = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[2]/div[3]/div/div/ul/li[8]/div"));
                lifeevents.Click();
                System.Threading.Thread.Sleep(3000);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_listCaseMemberPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindNonPrimaryLifeEvent(driver, primaryName);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_listCaseMemberPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'MNHIXIntegratedCase_viewLifeEventsPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);
                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/div/table/tbody/tr[1]/td[4]/span/span/span")).Click(); //add coverage wizard  
                driver.FindElement(By.XPath("//td[contains(text(), 'Launch…')]")).Click();
                System.Threading.Thread.Sleep(2000);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoPersonEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[2]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[2]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click();//evidence tab
                System.Threading.Thread.Sleep(3000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoPersonEvidenceOldAddressCorrection(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_listEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/table/tbody/tr[3]/td[7]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveDynEvdModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxRecDate = driver.FindElement(By.Id("__o3id7"));
                textboxRecDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxRecDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxRecDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id8"));
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxEndDate = driver.FindElement(By.Id("__o3ida"));
                textboxEndDate.Clear();
                DateTime d1;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    d1 = myHistoryInfo.myTimeTravelDate;
                    d1 = d1.AddDays(-1);
                    textboxEndDate.SendKeys(d1.ToString("MM/dd/yyyy"));
                }
                else
                {
                    d1 = DateTime.Now;
                    d1 = d1.AddDays(-1);
                    textboxEndDate.SendKeys(d1.ToString("MM/dd/yyyy"));
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoIAEvidenceOldAddressCorrection(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Addresses']")).Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                IWebElement changeDate = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[6]"));
                DateTime today = DateTime.Today; // As DateTime
                string s_today = today.ToString("M/d/yyyy"); // As String
                string strDate = changeDate.Text.Substring(0, changeDate.Text.Length - 2);
                if (strDate != s_today)
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//toggle
                }
                else
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[3]/td[1]/a")).Click();//toggle, cw24, 20
                }

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                IWebElement textboxOriginal;
                if (strDate != s_today)
                {
                    textboxOriginal = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[3]/td[9]/span/span/span"));
                }
                else
                {
                    textboxOriginal = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span"));//cw24, 20
                }
                textboxOriginal.Click();//action menu
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                IWebElement textboxEndDate;
                myApp.DoWaitForElement(driver, By.Id("__o3ida"), myHistoryInfo);
                textboxEndDate = driver.FindElement(By.Id("__o3ida"));
                textboxEndDate.Clear();
                DateTime d1;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    d1 = myHistoryInfo.myTimeTravelDate;
                    d1 = d1.AddDays(-1);
                    textboxEndDate.SendKeys(d1.ToString("MM/dd/yyyy"));
                }
                else
                {
                    d1 = DateTime.Now;
                    d1 = d1.AddDays(-1);
                    textboxEndDate.SendKeys(d1.ToString("MM/dd/yyyy"));
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));

                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoNewEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.Id("dijit_MenuItem_0")).Click(); //new evidence button
                }
                else
                {
                    driver.FindElement(By.XPath("//td[contains(text(), 'New Evidence')]")).Click(); //new evidence button
                }
                System.Threading.Thread.Sleep(3000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoPersonNewEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_listEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_listEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/a")).Click(); //new button

                System.Threading.Thread.Sleep(3000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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
        public int DoActiveEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (3 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (3 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[3]/div"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[3]/div")).Click();

                System.Threading.Thread.Sleep(1000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public String FindEvidence(IWebDriver driver, By selector)
        {
            int pages = 2;
            for (int i = 0; i < pages; i++)
            {
                var elems2 = driver.FindElements(selector);
                IList<IWebElement> elements = elems2;
                if (elements != null && elements.Count > 0)
                {
                    return "true";
                }
                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div/ul/li[6]/a")).Click();//select next page
                System.Threading.Thread.Sleep(1000);
            }
            return "false";
        }

        public String FindPrimaryEvidence(IWebDriver driver, string name, ref mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo)
        {
            IWebElement firstPart = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[4]"));
            string firstParticipant = firstPart.Text;

            FillStructures myFillStructures = new FillStructures();
            int result;
            result = myFillStructures.doFillAppCountStructures(ref myEnrollment, ref myHistoryInfo);

            if (firstParticipant == name)
            {
                return "/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a";
            }
            else
            {
                return "/html/body/div[2]/div[2]/div/table/tbody/tr[3]/td[1]/a";
            }
        }

        public String FindNonPrimaryEvidence(IWebDriver driver, string name, ref mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo)
        {
            IWebElement firstPart = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[4]"));
            string firstParticipant = firstPart.Text;

            FillStructures myFillStructures = new FillStructures();
            int result;
            result = myFillStructures.doFillAppCountStructures(ref myEnrollment, ref myHistoryInfo);

            if (firstParticipant != name)
            {
                return "/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a";
            }
            else
            {
                return "/html/body/div[2]/div[2]/div/table/tbody/tr[3]/td[1]/a";
            }
        }

        public String FindNonPrimaryLifeEvent(IWebDriver driver, string name)
        {
            IWebElement first = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[2]/span/a"));
            string firstName = first.Text;

            if (firstName != name)
            {
                return "/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a";
            }
            else
            {
                return "/html/body/div[2]/div[2]/div/table/tbody/tr[3]/td[1]/a";
            }
        }

        public int DoUpdateTaxEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Tax Filing Status']")).Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string path;
                if (myEnrollment.myPassCount == "1")
                {
                    path = FindPrimaryEvidence(driver, primaryName, ref myEnrollment, ref myHistoryInfo);
                }
                else
                {
                    path = FindNonPrimaryEvidence(driver, primaryName, ref myEnrollment, ref myHistoryInfo);
                }
                driver.FindElement(By.XPath(path)).Click();//toggle
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxRecDate = driver.FindElement(By.Id("__o3id7"));
                textboxRecDate.Clear();
                textboxRecDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id8"));
                //textboxReason.Clear();
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxEffDate = driver.FindElement(By.Id("__o3id9"));
                textboxEffDate.Clear();
                textboxEffDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                IWebElement textboxJointly = driver.FindElement(By.Id("__o3idc"));
                textboxJointly.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement firstSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                firstSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(firstSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

                if (myEnrollment.myPassCount == "1")
                {
                    myEnrollment.myPassCount = "2";//update count to 2 to do the screens another time
                    myApp.DoUpdateAppPassCount(myHistoryInfo, myEnrollment.myPassCount);
                }
                else
                {
                    myEnrollment.myPassCount = "1";//update count back to 1 to move forward
                    myApp.DoUpdateAppPassCount(myHistoryInfo, myEnrollment.myPassCount);
                }

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

        public int DoUpdateIncomeEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Income']")).Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName, ref myEnrollment, ref myHistoryInfo);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                System.Threading.Thread.Sleep(3000);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxRecDate = driver.FindElement(By.Id("__o3id7"));
                textboxRecDate.Clear();
                textboxRecDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id8"));
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxEffDate = driver.FindElement(By.Id("__o3id9"));
                textboxEffDate.Clear();
                textboxEffDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                IWebElement textboxAmount = driver.FindElement(By.Id("__o3ide"));
                textboxAmount.Clear();
                textboxAmount.SendKeys(myEnrollment.myIncomeAmount);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (9 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (9 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));

                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoUpdateAddressEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Addresses']")).Click();

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName, ref myEnrollment, ref myHistoryInfo);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxRecDate = driver.FindElement(By.Id("__o3id7"));
                textboxRecDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxRecDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxRecDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id8"));
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxEffDate = driver.FindElement(By.Id("__o3id9"));
                textboxEffDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxEffDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxEffDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);

                IWebElement textboxStreet = driver.FindElement(By.Id("__o3idd"));
                textboxStreet.Clear();
                textboxStreet.SendKeys(myEnrollment.myHomeAddress1);

                IWebElement textboxCity = driver.FindElement(By.Id("__o3idf"));
                textboxCity.Clear();
                textboxCity.SendKeys(myEnrollment.myHomeCity);

                IWebElement textboxCounty = driver.FindElement(By.Id("__o3id10"));
                textboxCounty.Clear();
                textboxCounty.SendKeys(myEnrollment.myHomeCounty);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxState = driver.FindElement(By.Id("__o3id11"));
                textboxState.Clear();
                textboxState.SendKeys(myEnrollment.myHomeState);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxZip = driver.FindElement(By.Id("__o3id12"));
                textboxZip.Clear();
                textboxZip.SendKeys(myEnrollment.myHomeZip);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));
                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoUpdateStateEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='State Residency']")).Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName, ref myEnrollment, ref myHistoryInfo);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                System.Threading.Thread.Sleep(3000);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxRecDate = driver.FindElement(By.Id("__o3id7"));
                textboxRecDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxRecDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxRecDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id8"));
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxEffDate = driver.FindElement(By.Id("__o3id9"));
                textboxEffDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxEffDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxEffDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                IWebElement textboxStateRes = driver.FindElement(By.Id("__o3idd"));
                textboxStateRes.Click();

                IWebElement textboxCounty;
                textboxCounty = driver.FindElement(By.Id("__o3ide"));
                textboxCounty.Clear();
                textboxCounty.SendKeys(myEnrollment.myHomeCounty);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxHomeless;
                textboxHomeless = driver.FindElement(By.Id("__o3idf"));
                textboxHomeless.Clear();
                textboxHomeless.SendKeys(myEnrollment.myHomeless);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxMNHome;
                textboxMNHome = driver.FindElement(By.Id("__o3id10"));
                textboxMNHome.Clear();
                textboxMNHome.SendKeys(myEnrollment.myLiveMN);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]"));
                buttonSave.Click();

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (7 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (7 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));

                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoUpdatePAIEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Projected Annual Income']")).Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName, ref myEnrollment, ref myHistoryInfo);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                System.Threading.Thread.Sleep(3000);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxRecDate = driver.FindElement(By.Id("__o3id7"));
                textboxRecDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxRecDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxRecDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id8"));
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxEffDate = driver.FindElement(By.Id("__o3id9"));
                textboxEffDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxEffDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxEffDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                IWebElement textboxAmount = driver.FindElement(By.Id("__o3idb"));
                textboxAmount.Clear();
                textboxAmount.SendKeys(myEnrollment.myIncomeAmount);

                IWebElement textboxOverriden = driver.FindElement(By.Id("__o3idc"));
                textboxOverriden.Clear();
                textboxOverriden.SendKeys("Yes");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (11 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (11 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));

                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoClosePDCTab(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                IWebElement fourthTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));

                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthTab); //right click
                rClick.Perform();
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoUpdateMaritalEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Marital Status']")).Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName, ref myEnrollment, ref myHistoryInfo);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxRecDate = driver.FindElement(By.Id("__o3id7"));
                textboxRecDate.Clear();
                textboxRecDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id8"));
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxEffDate = driver.FindElement(By.Id("__o3id9"));
                textboxEffDate.Clear();
                textboxEffDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                IWebElement textboxStatus = driver.FindElement(By.Id("__o3idb"));
                textboxStatus.Clear();
                textboxStatus.SendKeys("Married");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));

                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoUpdateBirthEvidence(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Birth and Death Details']")).Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName, ref myEnrollment, ref myHistoryInfo);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                System.Threading.Thread.Sleep(3000);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxRecDate = driver.FindElement(By.Id("__o3id7"));
                textboxRecDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxRecDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxRecDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id8"));
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxDeath = driver.FindElement(By.Id("__o3idc"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxDeath.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxDeath.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoNewEvidenceESC(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/tbody/tr[30]/td[3]/span/span/span")).Click();//esc actions button
                    driver.FindElement(By.Id("dijit_MenuItem_0")).Click(); 
                }
                else
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[30]/td[3]/span/span/span")).Click();//esc actions button
                    driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); 
                }                

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[3]/div/div/table/tbody/tr[1]/td[1]/div/div[3]/input[1]"), myHistoryInfo);
                IWebElement textboxEmploymentType = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/div/div/table/tbody/tr[1]/td[1]/div/div[3]/input[1]"));
                textboxEmploymentType.SendKeys("Full Time");

                IWebElement textboxCoverageStatus = driver.FindElement(By.Id("__o3id7"));
                textboxCoverageStatus.SendKeys("Enrolled");

                IWebElement textboxStartDate = driver.FindElement(By.Id("__o3id8"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxStartDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxStartDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }
                IWebElement textboxEmploymentId = driver.FindElement(By.Id("__o3ida"));
                textboxEmploymentId.SendKeys("12345678");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

                /*driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                var error = driver.FindElements(By.XPath("//span[contains(text(), 'Error:')]")).Count();//if error displays on save  
                if (error > 0)
                {
                    textboxEmploymentType.SendKeys("Full Time");

                    textboxCoverageStatus.SendKeys("Enrolled");

                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        textboxStartDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        textboxStartDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                    }

                    textboxEmploymentId.SendKeys("12345678");

                    writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                    buttonSave.Click();
                }*/

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

        public int DoNewEvidenceAppDetails(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[7]/td[3]/span/span/span")).Click();//app details actions button               
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                System.Threading.Thread.Sleep(1000);
                IWebElement checkboxApplicant = driver.FindElement(By.Id("__o3id7"));
                checkboxApplicant.Click();

                IWebElement textboxAppDate = driver.FindElement(By.Id("__o3id9"));
                textboxAppDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                IWebElement textboxMotivationType = driver.FindElement(By.Id("__o3idc"));
                textboxMotivationType.Clear();
                textboxMotivationType.SendKeys("Insurance Affordability");

                IWebElement textboxDetermination;
                textboxDetermination = driver.FindElement(By.Id("__o3idf"));
                textboxDetermination.SendKeys("No");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceSSNDetails(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[75]/td[3]/span/span/span")).Click();//ssn details actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                System.Threading.Thread.Sleep(1000);
                myApp.DoWaitForElement(driver, By.Id("__o3id6"), myHistoryInfo);
                IWebElement textboxSSN = driver.FindElement(By.Id("__o3id6"));
                string hhssn = myEnrollment.mySSNNum;
                textboxSSN.SendKeys(hhssn);

                System.Threading.Thread.Sleep(1000);
                myApp.DoWaitForElement(driver, By.Id("__o3id9"), myHistoryInfo);
                IWebElement textboxAppDate = driver.FindElement(By.Id("__o3id9"));
                textboxAppDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceMaritalStatus(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[53]/td[3]/span/span/span")).Click();//marital status actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxStatus = driver.FindElement(By.Id("__o3id6"));
                textboxStatus.SendKeys(myEnrollment.myMaritalStatus);

                IWebElement textboxDate = driver.FindElement(By.Id("__o3id7"));
                textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceStateResidency(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[77]/td[3]/span/span/span")).Click();//state residency actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxDate = driver.FindElement(By.Id("__o3id6"));
                textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                IWebElement textboxStateRes = driver.FindElement(By.Id("__o3id8"));
                System.Threading.Thread.Sleep(1000);
                textboxStateRes.Click();

                IWebElement textboxCounty = driver.FindElement(By.Id("__o3id9"));
                textboxCounty.SendKeys(myEnrollment.myHomeCounty);

                IWebElement textboxHomeless = driver.FindElement(By.Id("__o3ida"));
                textboxHomeless.SendKeys(myEnrollment.myHomeless);

                IWebElement textboxMNHome = driver.FindElement(By.Id("__o3idb"));
                textboxMNHome.SendKeys(myEnrollment.myPlanLiveMN);

                IWebElement textboxEmployment = driver.FindElement(By.Id("__o3idc"));
                textboxEmployment.SendKeys("No");

                IWebElement textboxMedicalCare = driver.FindElement(By.Id("__o3id11"));
                textboxMedicalCare.SendKeys("No");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidencePregnancy(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[68]/td[3]/span/span/span")).Click();//pregnancy actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (12 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (12 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string wifeNameAge = DoGetWifeNameAge(driver, myEnrollment, ref myHistoryInfo);

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement listboxParticipant = driver.FindElement(By.Id("__o3id5"));
                listboxParticipant.Clear();
                listboxParticipant.SendKeys(wifeNameAge);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxChildren = driver.FindElement(By.Id("__o3id6"));
                textboxChildren.SendKeys("1");
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxDueDate = driver.FindElement(By.Id("__o3id7"));
                textboxDueDate.SendKeys("01/01/2017");

                IWebElement textboxStartDate = driver.FindElement(By.Id("__o3id9"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxStartDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxStartDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceParticipantAddress(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[4]/td[3]/span/span/span")).Click();//addr actions button                
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                string firstName = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                if (firstName == fullName)
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[1]/span/input")).Click();
                else
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/div[2]/table/tbody/tr[2]/td[1]/span/input")).Click();
                }

                IWebElement textboxApt = driver.FindElement(By.Id("__o3idd"));
                if (myEnrollment.myHomeAptSuite != null)
                {
                    textboxApt.SendKeys(myEnrollment.myHomeAptSuite);
                }
                IWebElement textboxStreet = driver.FindElement(By.Id("__o3ide"));
                textboxStreet.SendKeys(myEnrollment.myHomeAddress1);

                if (myEnrollment.myHomeAddress2 != null)
                {
                    IWebElement textboxStreet2 = driver.FindElement(By.Id("__o3idf"));
                    textboxStreet2.SendKeys(myEnrollment.myHomeAddress2);
                }
                IWebElement textboxCity = driver.FindElement(By.Id("__o3id10"));
                textboxCity.SendKeys(myEnrollment.myHomeCity);

                IWebElement textboxCounty = driver.FindElement(By.Id("__o3id11"));
                textboxCounty.SendKeys(myEnrollment.myHomeCounty);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxState = driver.FindElement(By.Id("__o3id12"));
                textboxState.SendKeys(myEnrollment.myHomeState);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxZip = driver.FindElement(By.Id("__o3id13"));
                textboxZip.SendKeys(myEnrollment.myHomeZip);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                IWebElement buttonSave;
                buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoPersonNewEvidenceAddress(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/table/tbody/tr[1]/td[3]/span/span/span")).Click();//addresses actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                myApp.DoWaitForElement(driver, By.Id("__o3idb"), myHistoryInfo);
                IWebElement textboxStreet = driver.FindElement(By.Id("__o3idb"));
                textboxStreet.Clear();
                textboxStreet.SendKeys(myEnrollment.myHomeAddress1);

                IWebElement textboxCity = driver.FindElement(By.Id("__o3idd"));
                textboxCity.Clear();
                textboxCity.SendKeys(myEnrollment.myHomeCity);

                IWebElement textboxCounty = driver.FindElement(By.Id("__o3ide"));
                textboxCounty.Clear();
                textboxCounty.SendKeys(myEnrollment.myHomeCounty);

                IWebElement textboxState = driver.FindElement(By.Id("__o3idf"));
                textboxState.Clear();
                textboxState.SendKeys(myEnrollment.myHomeState);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxZip = driver.FindElement(By.Id("__o3id10"));
                textboxZip.Clear();
                textboxZip.SendKeys(myEnrollment.myHomeZip);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceParticipantAddressMailing(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[4]/td[3]/span/span/span")).Click();//addr actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                string firstName = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[2]")).Text;
                if (firstName == fullName)
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/div[2]/table/tbody/tr[1]/td[1]/span/input")).Click();
                else
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/div[2]/table/tbody/tr[2]/td[1]/span/input")).Click();
                }
                IWebElement textboxType;
                textboxType = driver.FindElement(By.Id("__o3id9"));
                textboxType.Clear();
                textboxType.SendKeys("Mailing");

                if (myEnrollment.myMailAptSuite != null)
                {
                    IWebElement textboxApt = driver.FindElement(By.Id("__o3idd"));
                    textboxApt.SendKeys(myEnrollment.myMailAptSuite);
                }

                IWebElement textboxStreet = driver.FindElement(By.Id("__o3ide"));
                //textboxStreet.SendKeys(myEnrollment.myMailAddress1);
                textboxStreet.SendKeys("1000 Car Street");

                if (myEnrollment.myMailAddress2 != null)
                {
                    IWebElement textboxStreet2 = driver.FindElement(By.Id("__o3idf"));
                    textboxStreet2.SendKeys(myEnrollment.myMailAddress2);
                }
                IWebElement textboxCity = driver.FindElement(By.Id("__o3id10"));
                //textboxCity.SendKeys(myEnrollment.myMailCity);
                textboxCity.SendKeys("Eden Prairie");

                IWebElement textboxCounty = driver.FindElement(By.Id("__o3id11"));
                //textboxCounty.SendKeys(myEnrollment.myMailCounty);
                textboxCounty.SendKeys("Hennepin");
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxState = driver.FindElement(By.Id("__o3id12"));
                //textboxState.SendKeys(myEnrollment.myMailState);
                textboxState.SendKeys("Minnesota");
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxZip = driver.FindElement(By.Id("__o3id13"));
                //textboxZip.SendKeys(myEnrollment.myMailZip);
                textboxZip.SendKeys("55347");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave;
                buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceProjectedAnnualIncome(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[69]/td[3]/span/span/span")).Click();//projected annual income actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxAmount = driver.FindElement(By.Id("__o3id6"));
                textboxAmount.SendKeys("0");

                IWebElement textboxDate = driver.FindElement(By.Id("__o3id8"));
                textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceMedicaid(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[54]/td[3]/span/span/span")).Click();//medicaid enrollment actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceBirth(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[16]/td[3]/span/span/span")).Click();//birth and death details actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);
                System.Threading.Thread.Sleep(1000);

                IWebElement textboxDate = driver.FindElement(By.Id("__o3id8"));
                textboxDate.SendKeys(myEnrollment.myDOB);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceRelationship(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[56]/td[3]/span/span/span")).Click();//member relationship actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                System.Threading.Thread.Sleep(1000);
                IWebElement textboxRelationship = driver.FindElement(By.Id("__o3id7"));
                textboxRelationship.SendKeys("Is the Spouse of");

                IWebElement textboxDate = driver.FindElement(By.Id("__o3ida"));
                textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceCitizenStatus(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[19]/td[3]/span/span/span")).Click();//citizen status actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxDate = driver.FindElement(By.Id("__o3idb"));
                textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceTaxStatus(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[80]/td[3]/span/span/span")).Click();//tax filing status actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                System.Threading.Thread.Sleep(1000);
                IWebElement textboxTaxStatus = driver.FindElement(By.Id("__o3id6"));
                textboxTaxStatus.SendKeys("Tax Filer");

                IWebElement textboxJointly = driver.FindElement(By.Id("__o3id7"));
                textboxJointly.Click();

                IWebElement textboxDate;
                textboxDate = driver.FindElement(By.Id("__o3id8"));
                textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoNewEvidenceGender(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[43]/td[3]/span/span/span")).Click();//gender actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                span = DateTime.Now - birth;
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                myApp.DoWaitForElement(driver, By.Id("__o3id5"), myHistoryInfo);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                System.Threading.Thread.Sleep(1000);
                IWebElement textboxGender = driver.FindElement(By.Id("__o3id6"));
                textboxGender.Clear();
                textboxGender.SendKeys(myEnrollment.myGender);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();

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

        public int DoValidateEvidenceChanges(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (3 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (3 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.Id("dijit_MenuItem_3")).Click();//validate changes button
                }
                else
                {
                    driver.FindElement(By.XPath("//td[contains(text(), 'Validate Changes')]")).Click(); //validate changes button
                }
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_validateChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);         
                IWebElement checkallbox;
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    checkallbox = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/thead/tr/th[1]"));
                }
                else
                {
                    checkallbox = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/thead/tr/th[1]/input"));
                }
                checkallbox.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

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

        public int DoApplyEvidenceChanges(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.Id("dijit_MenuItem_2")).Click();//apply changes button
                }
                else
                {
                    driver.FindElement(By.XPath("//td[contains(text(), 'Apply Changes')]")).Click(); //apply changes button
                }
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCREvidence_applyChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                IWebElement checkallbox;
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    checkallbox = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/thead/tr/th[1]"));
                }
                else
                {
                    checkallbox = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/thead/tr/th[1]/input"));
                }
                checkallbox.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (30 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (30 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

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

        public int DoAppFilerConsent(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/div[3]/div[2]/div/div/table/tbody/tr/td[1]/div/tr[4]/td[1]/div/span/a")).Click();//application filer consent

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"), myHistoryInfo);
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//toggle

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"), myHistoryInfo);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//toggle2

                System.Threading.Thread.Sleep(3000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"), myHistoryInfo);
                fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[1]/div[4]/div/div[4]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoVerification(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                if (myEnrollment.myRenewalCov == "0")
                {
                    driver.SwitchTo().DefaultContent();
                    driver.FindElement(By.LinkText("Person…")).Click();//select person... tab

                    myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"), myHistoryInfo);
                    var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                    driver.SwitchTo().Frame(iFrameElement);

                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click();//select person link
                    System.Threading.Thread.Sleep(1000);

                    driver.SwitchTo().DefaultContent();
                    var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement2);

                    myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'HCRIC_home')]"), myHistoryInfo);
                    driver.FindElement(By.XPath("//a[contains(@href,'HCRIC_home')]")).Click(); //select insurance affordability
                    System.Threading.Thread.Sleep(2000);

                    driver.SwitchTo().DefaultContent();
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div")).Click();//select  evidence
                    System.Threading.Thread.Sleep(2000);
                }

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[2]/div[3]/div/div/ul/li[5]/div"), myHistoryInfo);
                if (myHistoryInfo.myEnvironment == "STST") {
                    driver.FindElement(By.XPath("//div[contains(@page-ref,'en_US/HCRIC_listVerification')]")).Click();//select verifications                    
                } else {
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[2]/div[3]/div/div/ul/li[5]/div")).Click();//select verifications
                }
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoTasks(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[9]/div/div/div/span[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[9]/div/div/div/span[1]")).Click();
                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().DefaultContent();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultIC_listTaskPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/table/tbody/tr/td[2]/a")).Click();//select first task
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                driver.SwitchTo().DefaultContent();
                IWebElement secondTab;
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div/div[3]/div[1]/div[4]/div/div[2]"), myHistoryInfo);
                secondTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div/div[3]/div[1]/div[4]/div/div[2]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(secondTab); //right click
                rClick.Perform();
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel")).Click();//hcr cases tab

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

        public int DoCloseTasks(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/DefaultIC_listTaskPage.do')]"), myHistoryInfo);

                driver.SwitchTo().DefaultContent();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultIC_listTaskPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                //Find outstanding tasks
                var elems = driver.FindElements(By.XPath("//a[@class='field-link']"));
                IList<IWebElement> list = elems;
                for (int j = 0; j < list.Count; j++)
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/table/tbody/tr/td[2]/a")).Click();//select link
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);

                    driver.SwitchTo().DefaultContent();
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div/div[3]/div[3]/div[2]/div/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click();//select actions
                    driver.FindElement(By.XPath("//td[contains(text(), 'Add To My Tasks')]")).Click(); //add to my tasks
                    System.Threading.Thread.Sleep(3000);

                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    driver.SwitchTo().DefaultContent();
                    var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/TaskManagement_ReserveTaskPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement2);

                    driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //save and view
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);

                    driver.SwitchTo().DefaultContent();
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div/div[3]/div[3]/div[2]/div/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click();//select actions
                    System.Threading.Thread.Sleep(1000);
                    driver.FindElement(By.XPath("//td[contains(text(), 'Close…')]")).Click(); //close, close is there twice so must state ...
                    System.Threading.Thread.Sleep(2000);

                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    driver.SwitchTo().DefaultContent();
                    var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/TaskManagement_closeTaskPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement3);

                    driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //save
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);

                    driver.SwitchTo().DefaultContent();
                    IWebElement secondTab;
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div/div[3]/div[1]/div[4]/div/div[2]"), myHistoryInfo);
                    secondTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div/div[3]/div[1]/div[4]/div/div[2]"));
                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                    var rClick = action.ContextClick(secondTab); //right click
                    rClick.Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                    driver.SwitchTo().DefaultContent();
                    driver.FindElement(By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel")).Click();//hcr cases tab

                    driver.SwitchTo().DefaultContent();
                    var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultIC_listTaskPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement4);

                    driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click(); //refresh

                    System.Threading.Thread.Sleep(2000);
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoAddProof(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRIC_listVerificationsForCasePage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRIC_listVerificationsForCasePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                DoWaitForElementRefresh(driver, By.XPath("/html/body/div[2]/div[3]/div/div/table/tbody/tr[1]/td[7]/span/span/span"), By.XPath("/html/body/div[1]/div/div[2]/a[1]"));

                //Find outstanding verification items
                var elems = driver.FindElements(By.XPath("//td[@class='last-field list-row-menu']"));
                IList<IWebElement> list = elems;
                for (int j = 0; j < list.Count; j++)
                {
                    int householdCount = 1;
                    if (myEnrollment.myHouseholdOther == "Yes")
                    {
                        HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                        householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    }
                    driver.SwitchTo().DefaultContent();

                    var iFrameElement1 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultIC_tabDetailsPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement1);

                    IWebElement hh1first;
                    IWebElement hh1last = null;
                    IWebElement hh1ageplus;
                    if (householdCount == 1)
                    {
                        hh1first = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div/div[2]/div[2]/div/a"));
                        if (myHistoryInfo.myEnvironment == "STST2")
                        {
                            hh1last = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div/div[2]/div[3]/div/a"));
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            hh1ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div/div[2]/div[5]/div/div[3]")); 
                        } else {
                            hh1ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div/div[2]/div[6]/div[2]"));
                        }
                        
                    }
                    else
                    {
                        hh1first = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[1]/div/div[2]/div[2]/div/a"));
                        if (myHistoryInfo.myEnvironment == "STST2")
                        {
                            hh1last = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[1]/div/div[2]/div[3]/div/a"));
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            hh1ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[1]/div/div[2]/div[5]/div/div[3]")); 
                        }
                        else
                        {
                            hh1ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[1]/div/div[2]/div[6]/div[2]"));
                        }
                    }
                    string hh1firstname = hh1first.Text;
                    string hh1lastname;
                    string hh1fullname;

                    if (myHistoryInfo.myEnvironment == "STST")
                    {
                        hh1fullname = hh1firstname; 
                    } else {
                        hh1lastname = hh1last.Text;
                        hh1fullname = hh1firstname + " " + hh1lastname;
                    }                    
                    string hh1age = hh1ageplus.Text.Substring(0, 2);

                    string hh2firstname = null;
                    string hh2lastname = null;
                    string hh2fullname = null;
                    string hh3firstname = null;
                    string hh3lastname = null;
                    string hh3fullname = null;
                    string hh2age = null;
                    string hh3age = null;
                    if (householdCount == 2 || householdCount == 3)
                    {
                        IWebElement hh2first;
                        IWebElement hh2last;
                        IWebElement hh2ageplus;
                        hh2first = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[2]/div/div[2]/div[2]/div/a"));
                        hh2firstname = hh2first.Text;
                        if (myHistoryInfo.myEnvironment == "STST2")
                        {
                            hh2last = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[2]/div/div[2]/div[3]/div/a"));
                            hh2lastname = hh2last.Text;
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            hh2ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[2]/div/div[2]/div[5]/div/div[3]"));
                        }
                        else
                        {
                            hh2ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[2]/div/div[2]/div[6]/div[2]")); 
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            hh2fullname = hh2firstname;
                        }
                        else
                        {
                            hh2fullname = hh2firstname + " " + hh2lastname;
                        }
                        if (hh2ageplus.Text.Contains("months"))
                        {
                            hh2age = "0";
                        }
                        else
                        {
                            hh2age = hh2ageplus.Text.Substring(0, 2).Trim();
                        }
                    }
                    if (householdCount == 3)
                    {
                        IWebElement hh3first;
                        IWebElement hh3last;
                        IWebElement hh3ageplus;
                        hh3first = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[3]/div/div[2]/div[2]/div/a"));
                        hh3firstname = hh3first.Text;
                        if (myHistoryInfo.myEnvironment == "STST2")
                        {
                            hh3last = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[3]/div/div[2]/div[3]/div/a"));
                            hh3lastname = hh3last.Text;
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            hh3ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[3]/div/div[2]/div[5]/div/div[3]"));
                        }
                        else
                        {
                            hh3ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div/div[3]/div/div[2]/div[6]/div[2]")); 
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            hh3fullname = hh3firstname; 
                        }
                        else
                        {
                            hh3fullname = hh3firstname + " " + hh3lastname;
                        }
                        if (hh3ageplus.Text.Contains("months"))
                        {
                            hh3age = "0";
                        }
                        else
                        {
                            hh3age = hh3ageplus.Text.Substring(0, 2).Trim();
                        }
                    }

                    driver.SwitchTo().DefaultContent();

                    var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRIC_listVerificationsForCasePage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement2);

                    DoWaitForElementRefresh(driver, By.XPath("/html/body/div[2]/div[3]/div/div/table/tbody/tr[1]/td[7]/span/span/span"), By.XPath("/html/body/div[1]/div/div[2]/a[1]"));

                    //Find outstanding verification participants
                    var elems2 = driver.FindElements(By.XPath("//a[contains(@href,'Participant')]"));
                    string p = elems2[0].Text;

                    string age;
                    if (householdCount == 3 && elems2[0].Text.Contains(hh3fullname))
                    {
                        age = hh3age;
                    }
                    else if ((householdCount == 2 || householdCount == 3) && elems2[0].Text.Contains(hh2fullname))
                    {
                        age = hh2age;
                    }
                    else
                    {
                        age = hh1age;
                    }
                    System.Threading.Thread.Sleep(1000);
                    driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div/table/tbody/tr[1]/td[7]/span/span/span")).Click();//select arrow    
                    if (myHistoryInfo.myEnvironment == "STST")
                    {
                        driver.FindElement(By.Id("dijit_MenuItem_1_text")).Click();
                        // driver.FindElement(By.XPath("//td[contains(text(), 'Add Proof……')]")).Click();//select add proof   
                    }
                    else
                    {
                        driver.FindElement(By.XPath("//td[contains(text(), 'Add Proof…')]")).Click();//select add proof        
                    }
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);

                    driver.SwitchTo().Window(driver.WindowHandles.Last());

                    myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/VerificationApplication_createVerificationItemProvisionPage.do')]"), myHistoryInfo);
                    var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/VerificationApplication_createVerificationItemProvisionPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement3);

                    IWebElement participant = driver.FindElement(By.Id("__o3id2"));
                    participant.SendKeys(p + " (" + age + ")");

                    System.Threading.Thread.Sleep(2000);
                    writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                    driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]")).Click();//select save
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        if (myHistoryInfo.myBrowser == "Chrome")
                        {
                            appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (9 + myHistoryInfo.myAppWait) * 1000;
                        }
                    }
                    else
                    {
                        if (myHistoryInfo.myBrowser == "Chrome")
                        {
                            appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (9 + myHistoryInfo.myAppWait) * 1000;
                        }
                    }
                    System.Threading.Thread.Sleep(appwait);
                }

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

        public int DoMAHome(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.LinkText("Person…"), myHistoryInfo);
                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"), myHistoryInfo);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                if (myEnrollment.mySSN == "Yes")
                {
                    IWebElement textboxSSN = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[1]/div/table/tbody/tr/td[1]/input"));
                    textboxSSN.Clear();
                    if (myEnrollment.myDay2TestId != null)
                    {
                        textboxSSN.SendKeys(DoDay2PrimarySSN(myEnrollment.myDay2TestId));
                    }
                    else
                    {
                        if (myEnrollment.myApplyYourself == "No")
                        {
                            int temp = Convert.ToInt32(myEnrollment.mySSNNum) + 1;
                            textboxSSN.SendKeys(Convert.ToString(temp));
                        }
                        else
                        {
                            string hhssn = myEnrollment.mySSNNum;
                            textboxSSN.SendKeys(hhssn);
                            //DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                            //textboxSSN.SendKeys("344688097"); 
                        }
                    }
                }
                else
                {
                    IWebElement textboxFirst = driver.FindElement(By.Id("__o3id1"));
                    textboxFirst.Clear();
                    textboxFirst.SendKeys(myEnrollment.myFirstName);

                    IWebElement textboxLast = driver.FindElement(By.Id("__o3id3"));
                    textboxLast.Clear();
                    textboxLast.SendKeys(myEnrollment.myLastName);

                    IWebElement textboxDOB = driver.FindElement(By.Id("__o3id5"));
                    textboxDOB.Clear();
                    textboxDOB.SendKeys(myEnrollment.myDOB);
                }
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search button

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'StreamlinedMedicaid_home')]"), myHistoryInfo);
                driver.FindElement(By.XPath("//a[contains(@href,'StreamlinedMedicaid_home')]")).Click(); //select ma

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoMAActivateCase(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click(); //actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Activate Case')]")).Click(); //activate case button

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().Window(driver.WindowHandles.Last());

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_activatePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_activatePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //Yes button

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

        public int DoClosePDC(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click(); //actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Close Case')]")).Click(); //close case button

                System.Threading.Thread.Sleep(3000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().Window(driver.WindowHandles.Last());

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_closePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_closePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxOutcome = driver.FindElement(By.Id("__o3id1"));
                textboxOutcome.Clear();
                textboxOutcome.SendKeys("Not Attained");

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id2"));
                textboxReason.Clear();
                textboxReason.SendKeys("Not Eligible");

                driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //Yes button

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

        public int DoCloseIC(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[3]/div/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click(); //actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Close Case')]")).Click(); //close case button

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().Window(driver.WindowHandles.Last());

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/IntegratedCase_closePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/IntegratedCase_closePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxComments = driver.FindElement(By.Id("__o3id1"));
                textboxComments.SendKeys("Closed the case - HH member is deceased");

                driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //Yes button

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

        public int DoBHPHome(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.LinkText("Person…"), myHistoryInfo);
                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"), myHistoryInfo);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                if (myEnrollment.mySSN == "Yes")
                {
                    IWebElement textboxSSN = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[1]/div/table/tbody/tr/td[1]/input"));
                    textboxSSN.Clear();
                    if (myEnrollment.myDay2TestId != null)
                    {
                        textboxSSN.SendKeys(DoDay2PrimarySSN(myEnrollment.myDay2TestId));
                    }
                    else
                    {
                        if (myEnrollment.myApplyYourself == "No")
                        {
                            int temp = Convert.ToInt32(myEnrollment.mySSNNum) + 1;
                            textboxSSN.SendKeys(Convert.ToString(temp));
                        }
                        else
                        {
                            string hhssn = myEnrollment.mySSNNum;
                            textboxSSN.SendKeys(hhssn);
                            //DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                            //textboxSSN.SendKeys("344688097"); 
                        }
                    }
                }
                else
                {
                    IWebElement textboxFirst = driver.FindElement(By.Id("__o3id1"));
                    textboxFirst.Clear();
                    textboxFirst.SendKeys(myEnrollment.myFirstName);

                    IWebElement textboxLast = driver.FindElement(By.Id("__o3id3"));
                    textboxLast.Clear();
                    textboxLast.SendKeys(myEnrollment.myLastName);

                    IWebElement textboxDOB = driver.FindElement(By.Id("__o3id5"));
                    textboxDOB.Clear();
                    textboxDOB.SendKeys(myEnrollment.myDOB);
                }
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search button

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'HCRStateBasicHealthPlanPDHome')]"), myHistoryInfo);
                driver.FindElement(By.XPath("//a[contains(@href,'HCRStateBasicHealthPlanPDHome')]")).Click(); //select bhp

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoQHPHome(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.LinkText("Person…"), myHistoryInfo);
                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"), myHistoryInfo);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                if (myEnrollment.mySSN == "Yes")
                {
                    IWebElement textboxSSN = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[1]/div/table/tbody/tr/td[1]/input"));
                    textboxSSN.Clear();
                    if (myEnrollment.myDay2TestId != null)
                    {
                        textboxSSN.SendKeys(DoDay2PrimarySSN(myEnrollment.myDay2TestId));
                    }
                    else
                    {
                        if (myEnrollment.myApplyYourself == "No")
                        {
                            int temp = Convert.ToInt32(myEnrollment.mySSNNum) + 1;
                            textboxSSN.SendKeys(Convert.ToString(temp));
                        }
                        else
                        {
                            string hhssn = myEnrollment.mySSNNum;
                            textboxSSN.SendKeys(hhssn);
                            //DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                            //textboxSSN.SendKeys("344688097"); 
                        }
                    }
                }
                else
                {
                    IWebElement textboxFirst = driver.FindElement(By.Id("__o3id1"));
                    textboxFirst.Clear();
                    textboxFirst.SendKeys(myEnrollment.myFirstName);

                    IWebElement textboxLast = driver.FindElement(By.Id("__o3id3"));
                    textboxLast.Clear();
                    textboxLast.SendKeys(myEnrollment.myLastName);

                    IWebElement textboxDOB = driver.FindElement(By.Id("__o3id5"));
                    textboxDOB.Clear();
                    textboxDOB.SendKeys(myEnrollment.myDOB);
                }
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search button

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'HCRInsuranceAssistance')]"), myHistoryInfo);
                driver.FindElement(By.XPath("//a[contains(@href,'HCRInsuranceAssistance')]")).Click(); //select qhp

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoUQHPHome(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.LinkText("Person…"), myHistoryInfo);
                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"), myHistoryInfo);
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                if (myEnrollment.mySSN == "Yes")
                {
                    IWebElement textboxSSN = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[1]/div/table/tbody/tr/td[1]/input"));
                    textboxSSN.Clear();
                    if (myEnrollment.myDay2TestId != null)
                    {
                        textboxSSN.SendKeys(DoDay2PrimarySSN(myEnrollment.myDay2TestId));
                    }
                    else
                    {
                        if (myEnrollment.myApplyYourself == "No")
                        {
                            int temp = Convert.ToInt32(myEnrollment.mySSNNum) + 1;
                            textboxSSN.SendKeys(Convert.ToString(temp));
                        }
                        else
                        {
                            string hhssn = myEnrollment.mySSNNum;
                            textboxSSN.SendKeys(hhssn);
                            //DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                            //textboxSSN.SendKeys("344688097"); 
                        }
                    }
                }
                else
                {
                    IWebElement textboxFirst = driver.FindElement(By.Id("__o3id1"));
                    textboxFirst.Clear();
                    textboxFirst.SendKeys(myEnrollment.myFirstName);

                    IWebElement textboxLast = driver.FindElement(By.Id("__o3id3"));
                    textboxLast.Clear();
                    textboxLast.SendKeys(myEnrollment.myLastName);

                    IWebElement textboxDOB = driver.FindElement(By.Id("__o3id5"));
                    textboxDOB.Clear();
                    textboxDOB.SendKeys(myEnrollment.myDOB);
                }
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search button

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'HCRUnassistedQualifiedHealthPlanHome')]"), myHistoryInfo);
                driver.FindElement(By.XPath("//a[contains(@href,'HCRUnassistedQualifiedHealthPlanHome')]")).Click(); //select uqhp

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoDeterminations(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                if (myEnrollment.myRenewalCov == "0")
                {
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //determinations tab
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //determinations tab, this doesn't always work
                }
                else
                {
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //determinations tab
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[4]/div/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //determinations tab, this doesn't always work
                }

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/DefaultICProduct_resolveDeterminationCurrentPage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultICProduct_resolveDeterminationCurrentPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    DoWaitForDeterminations(driver, By.XPath("/html/body/div[2]/div[3]/div/table/tbody/tr[1]/td[1]/a")); 
                } else {
                    DoWaitForDeterminations(driver, By.XPath("/html/body/div[2]/div[3]/div[2]/div/table/tbody/tr[1]/td[1]/a"));
                }
                driver.SwitchTo().DefaultContent();
                driver.SwitchTo().Frame(iFrameElement);
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/table/tbody/tr[1]/td[1]/a")).Click(); //coverage period arrow   
                }
                else
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click(); //coverage period arrow   
                }
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoDecision(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/div[3]/div[2]/div/table/tbody/tr[1]/td[2]/a"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/table/tbody/tr[1]/td[2]/a")).Click(); //coverage link

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[6]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"), myHistoryInfo);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoIncome(IWebDriver driver, ref mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber, ref string returnMNSureID)
        {
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
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[5]/div/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[5]/div/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //income tab

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[5]/div/div/div[4]/div/div/div[2]/div[3]/div/div/ul/li[2]/div"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div/div[3]/div[3]/div[5]/div/div/div[4]/div/div/div[2]/div[3]/div/div/ul/li[2]/div")).Click(); //income tab

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public String DoWaitForElementRefresh(IWebDriver driver, By selector, By refresh)
        {
            int wait = 500000;
            int iterations = (wait / 1000);
            long startmilliSec = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            for (int i = 0; i < iterations; i++)
            {
                if (((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startmilliSec) > wait)
                {
                    return "false";
                }
                var elems2 = driver.FindElements(selector);
                IList<IWebElement> elements = elems2;
                if (elements != null && elements.Count > 0)
                {
                    System.Threading.Thread.Sleep(2000);
                    return "true";
                }
                System.Threading.Thread.Sleep(1000);
                driver.FindElement(refresh).Click();//select refresh
            }
            return "false";
        }

        public String DoWaitForDeterminations(IWebDriver driver, By selector)
        {
            int wait = 500000;
            int iterations = (wait / 1000);
            long startmilliSec = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            for (int i = 0; i < iterations; i++)
            {
                if (((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startmilliSec) > wait)
                {
                    return "false";
                }
                driver.SwitchTo().DefaultContent();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultICProduct_resolveDeterminationCurrentPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                var elems2 = driver.FindElements(selector);
                IList<IWebElement> elements = elems2;
                if (elements != null && elements.Count > 0)
                {
                    return "true";
                }
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[2]/div/div/div/span[1]")).Click(); //select evidence tab
                System.Threading.Thread.Sleep(3000);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //select determinations tab
                System.Threading.Thread.Sleep(2000);
            }
            return "false";
        }

        public String DoNewEvidenceBlankScreen(IWebDriver driver, By selector, By refresh)
        {
            int wait = 500000;
            int iterations = (wait / 1000);
            long startmilliSec = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            for (int i = 0; i < iterations; i++)
            {
                if (((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startmilliSec) > wait)
                {
                    return "false";
                }
                var elems2 = driver.FindElements(selector);
                IList<IWebElement> elements = elems2;
                if (elements != null && elements.Count > 0)
                {
                    System.Threading.Thread.Sleep(2000);
                    return "true";
                }
                System.Threading.Thread.Sleep(1000);
                driver.FindElement(refresh).Click();//select refresh
            }
            return "false";
        }

        public string DoDay2PrimarySSN(string primaryTestId)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM Application where TestId = " + "'" + primaryTestId + "'", con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetString(29);
                    }
                    else
                    {
                        return "Error locating primary ssn";
                    }
                }
            }
            catch
            {
                return "Error locating primary ssn";
            }

        }

        public string DoDay2PrimaryName(string primaryTestId)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM Application where TestId = " + "'" + primaryTestId + "'", con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetString(2) + " " + reader.GetString(4);
                    }
                    else
                    {
                        return "Error locating primary name";
                    }
                }
            }
            catch
            {
                return "Error locating primary name";
            }
        }

        public string DoDay2PrimaryAddress(string primaryTestId)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM Address where TestId = " + "'" + primaryTestId + "'" + " and Type = 'Home'", con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        string addr2 = null;
                        string apt = null;
                        string zip4 = null;
                        if (!reader.IsDBNull(4))
                        {
                            addr2 = ", " + reader.GetString(4);
                        }
                        if (!reader.IsDBNull(8))
                        {
                            zip4 = " " + reader.GetString(8);
                        }
                        if (!reader.IsDBNull(11))
                        {
                            apt = ", " + reader.GetString(11);
                        }
                        return reader.GetString(3) + addr2 + apt + ", " + reader.GetString(5)
                            + ", " + reader.GetString(6) + " " + reader.GetString(7) + zip4;
                    }
                    else
                    {
                        return "Error locating primary address";
                    }
                }
            }
            catch
            {
                return "Error locating primary name";
            }
        }

        public string DoGetWifeNameAge(IWebDriver driver, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM HouseMembers where TestID = " + myEnrollment.myDay2TestId + " and HouseMembersID = 2", con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        DateTime birth = Convert.ToDateTime(reader.GetString(8));
                        TimeSpan span;
                        span = DateTime.Now - birth;
                        DateTime age = DateTime.MinValue + span;

                        return reader.GetString(2) + " " + reader.GetString(4) + " (" + (age.Year - 1) + ")";
                    }
                    else
                    {
                        return "Error locating wife name & age";
                    }
                }
            }
            catch
            {
                return "Error locating wife name & age";
            }

        }

        public int DoUpdateSSN(mystructHistoryInfo myHistoryInfo, string updateSSN, string updateFirst, string updateLast)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand(
                    "SELECT * FROM Application where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Application set SSNNum = @SSN, FirstName = @First, LastName = @Last where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com2 = new SqlCeCommand(myUpdateString, con))
                        {
                            com2.Parameters.AddWithValue("SSN", updateSSN);
                            com2.Parameters.AddWithValue("First", updateFirst);
                            com2.Parameters.AddWithValue("Last", updateLast);
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

        public int DoUpdateCWUserName(mystructHistoryInfo myHistoryInfo, string updateUser)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand(
                    "SELECT * FROM Account where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Account set CWUsername = @CWUser where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com2 = new SqlCeCommand(myUpdateString, con))
                        {
                            com2.Parameters.AddWithValue("CWUser", updateUser);
                            com2.ExecuteNonQuery();
                            com2.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update cw username didn't work");
            }
            return 1;
        }

    }
}
