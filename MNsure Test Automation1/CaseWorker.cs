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

        public int DoCaseWorkerLogin(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, 
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.Manage().Window.Maximize();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"));
                driver.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();//Click signin button so you can signout as individual

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("USRST117");//Enter username
                    myAccountCreate.myCaseWorkerLoginId = "USRST117";
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@12345");//Enter password
                }
                else
                {
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("cwceb01");//Enter username
                    myAccountCreate.myCaseWorkerLoginId = "cwceb01";
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Password1#");//Enter password
                }
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[3]/div/div/button")).Click();//Click next button

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

        public int DoCaseWorkerLoginTimeTravel(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.Manage().Window.Maximize();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"));

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input")).SendKeys("USRST117");//Enter username
                    myAccountCreate.myCaseWorkerLoginId = "USRST117";
                    driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input")).SendKeys("Welcome@12345");//Enter password
                }
                else
                {
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("cwceb01");//Enter username
                    myAccountCreate.myCaseWorkerLoginId = "cwceb01";
                    driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Password1#");//Enter password                
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]")).Click();//Click sign in button

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

        public int DoHCRCases(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, 
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(12000);
                ApplicationDo myApp = new ApplicationDo();
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel"));
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
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div"));
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div")).Click(); //shortcuts tab                    
                }
               
                driver.FindElement(By.Id("dijit_layout_AccordionPane_1_button")).Click();//searches             
                driver.FindElement(By.LinkText("Person…")).Click();

                IWebElement firstSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[1]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(firstSearchTab); //right click
                System.Threading.Thread.Sleep(2000);
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close all tabs')]")).Click(); 

                System.Threading.Thread.Sleep(1000);

                driver.FindElement(By.LinkText("Person…")).Click();             

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);
                System.Threading.Thread.Sleep(1000);

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
                        textboxSSN.SendKeys(myEnrollment.mySSNNum);
                        DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                        //textboxSSN.SendKeys("344688097"); 
                    }
                }
                else
                {
                    IWebElement textboxFirst = driver.FindElement(By.Id("__o3id1"));
                    textboxFirst.Clear();
                    textboxFirst.SendKeys(myEnrollment.myFirstName);
                    IWebElement textboxLast = driver.FindElement(By.Id("__o3id3"));
                    textboxLast.Clear();
                    textboxFirst.SendKeys(myEnrollment.myLastName);
                    IWebElement textboxDOB = driver.FindElement(By.Id("__o3id5"));
                    textboxDOB.Clear();
                    textboxFirst.SendKeys(myEnrollment.myDOB);
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

        public int DoPersonHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, 
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"));
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click();

                //System.Threading.Thread.Sleep(6000);                

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

        public int DoRegisterPerson(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
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
                    appwait = (15 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel"));

                driver.FindElement(By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel")).Click();//hcr cases tab

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div")).Click(); //shortcuts tab

                driver.FindElement(By.Id("dijit_layout_AccordionPane_1_button")).Click();//searches
                driver.FindElement(By.LinkText("Person…")).Click();
                System.Threading.Thread.Sleep(1000);
                IWebElement personSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[1]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(personSearchTab); //right click
                System.Threading.Thread.Sleep(1000);
                rClick.Perform();
                driver.FindElement(By.XPath("/html/body/div[3]/table/tbody/tr[2]/td[1]")).Click();//close all tabs
                System.Threading.Thread.Sleep(1000);

                driver.FindElement(By.LinkText("Person…")).Click();
                System.Threading.Thread.Sleep(2000);
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div/div/div[2]/div/div/div/span/span/span/span[2]"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div/div/div[2]/div/div/div/span/span/span/span[2]")).Click();//actions
                //driver.FindElement(By.XPath("/html/body/div[4]/table/tbody/tr[2]/td[2]")).Click();//register person
                driver.FindElement(By.XPath("//td[contains(text(), 'Register Person…')]")).Click();

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

        public int DoNotification(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[2]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[2]")).Click();//select person tab

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[6]/div/div/div/span[1]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[6]/div/div/div/span[1]")).Click();//select notification tab

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIX_listNoticesPage.do')]"));                                
                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIX_listNoticesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a"));
                driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//select down arrow

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'MNHIX_viewNoticePage.do')]"));
                var iFrameElement5 = driver.FindElement(By.XPath("//iframe[contains(@src,'MNHIX_viewNoticePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement5);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[3]/div[5]/div/table/tbody/tr/td[1]/a"));
                
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/div[3]/div[5]/div/table/tbody/tr/td[1]/a")).Click();//select pdf link

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

        public int DoIAHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                DoWaitForElementRefresh(driver, By.XPath("//a[contains(@href,'HCRIC_home')]"), By.XPath("/html/body/div[1]/div/div[3]/a[1]"));
                
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("//a[contains(@href,'HCRIC_home')]")).Click(); //select insurance affordability

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRIC_homePage.do')]"));                
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRIC_homePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]"));
                IWebElement myIcnum = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]"));
                returnICNumber = myIcnum.Text;

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

        public int DoEvidenceIA(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(6000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div/div/span[1]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div/div/span[1]")).Click();
                
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

        public int DoPersonEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(6000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div/div/span[1]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div/div/span[1]")).Click();//evidence tab

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

        public int DoPersonEvidenceOldAddressCorrection(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_listEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/table/tbody/tr[1]/td[7]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveDynEvdModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

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

        public int DoIAEvidenceOldAddressCorrection(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Address']")).Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                System.Threading.Thread.Sleep(5000);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);
                
                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//toggle
                System.Threading.Thread.Sleep(3000);

                IWebElement textboxOriginal = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[3]/td[9]/span/span/span"));
                textboxOriginal.Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

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

                IWebElement textboxEndDate = driver.FindElement(By.Id("__o3id15"));
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
                buttonSave.Click();//this saves to the 1st row instead of the 2nd
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
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

        public int DoNewEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);                 
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'New Evidence')]")).Click(); //new evidence button

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
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_listEvidencePage.do')]"));

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
        public int DoActiveEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(3000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[3]/div"));
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

        public String FindPrimaryEvidence(IWebDriver driver, string name)
        {
            IWebElement firstPart = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[4]"));
            string firstParticipant = firstPart.Text;
            
            if (firstParticipant != name)
            {
                return "/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a";
            }
            else
            {
                return "/html/body/div[2]/div[2]/div/table/tbody/tr[3]/td[1]/a";
            }
        }

        /*public String FindPrimaryActionMenuEvidence(IWebDriver driver, string name)
        {
            if (driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[3]")).Text != name)
            {
                return "/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span";
            }
            else
            {
                return "/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span";
            }
        }*/

        public int DoUpdateTaxEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Tax Filing Status']")).Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName); 
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

                IWebElement textboxJointly = driver.FindElement(By.Id("__o3idc"));
                textboxJointly.Click();

                IWebElement textboxDate = driver.FindElement(By.Id("__o3id10"));
                textboxDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                System.Threading.Thread.Sleep(4000);
                
                driver.SwitchTo().DefaultContent();
                IWebElement firstSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(firstSearchTab); //right click
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

        public int DoUpdateIncomeEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Income']")).Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName); 
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

                IWebElement textboxAmount = driver.FindElement(By.Id("__o3ide"));
                textboxAmount.Clear();
                textboxAmount.SendKeys(myEnrollment.myIncomeAmount);               

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                System.Threading.Thread.Sleep(4000);
                
                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
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

        public int DoUpdateAddressEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Address']")).Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                System.Threading.Thread.Sleep(5000);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

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

                IWebElement textboxStreet = driver.FindElement(By.Id("__o3idd"));
                textboxStreet.Clear();
                textboxStreet.SendKeys(myEnrollment.myHomeAddress1);

                IWebElement textboxCity = driver.FindElement(By.Id("__o3idf"));
                textboxCity.Clear();
                textboxCity.SendKeys(myEnrollment.myHomeCity);

                IWebElement textboxCounty = driver.FindElement(By.Id("__o3id10"));
                textboxCounty.Clear();
                textboxCounty.SendKeys(myEnrollment.myHomeCounty);

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
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
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

        public int DoUpdateStateEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='State Residency']")).Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName);
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

                IWebElement textboxCounty = driver.FindElement(By.Id("__o3idf"));
                textboxCounty.Clear();
                textboxCounty.SendKeys(myEnrollment.myHomeCounty);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                System.Threading.Thread.Sleep(5000);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
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

        public int DoUpdatePAIEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Projected Annual Income']")).Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName);
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
                System.Threading.Thread.Sleep(5000);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
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

        public int DoClosePDCTab(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                IWebElement fourthTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();

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

        public int DoUpdateMaritalEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_workspaceActiveHighLevelPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_workspaceActiveHighLevelPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                //driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/table/tbody/tr[1]/td[1]/a")).Click();//toggle
                FindEvidence(driver, By.LinkText("Projected Annual Income"));
                driver.FindElement(By.LinkText("Projected Annual Income")).Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryPath = FindPrimaryEvidence(driver, myEnrollment.myFirstName + " " + myEnrollment.myLastName);
                driver.FindElement(By.XPath(primaryPath)).Click();//toggle
                System.Threading.Thread.Sleep(3000);

                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[9]/span/span/span")).Click();//action menu
                //System.Threading.Thread.Sleep(1000);
                driver.FindElement(By.XPath("//td[contains(text(), 'Edit…')]")).Click();
                System.Threading.Thread.Sleep(3000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_resolveModifyEvidencePagePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

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
                //textboxReason.Clear();
                textboxReason.SendKeys("Reported by Client");

                IWebElement textboxAmount = driver.FindElement(By.Id("__o3idb"));
                textboxAmount.Clear();
                textboxAmount.SendKeys(myEnrollment.myIncomeAmount);

                /*IWebElement textboxStartDate = driver.FindElement(By.Id("__o3idd"));
                textboxStartDate.Clear();
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxStartDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxStartDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }*/

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(fourthSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click();
                System.Threading.Thread.Sleep(1000);

                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.LinkText("Person…")).Click();

                var iFrameElement5 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement5);

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
                        textboxSSN.SendKeys(myEnrollment.mySSNNum);
                        DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                        //textboxSSN.SendKeys("344688097"); 
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

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"));
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                var iFrameElement6 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement6);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh

                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//a[contains(@href,'HCRIC_home')]")).Click(); //select insurance affordability

                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div/div/span[1]")).Click(); //evidence

                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[1]/div")).Click();//dashboard

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

        public int DoUpdateBirthEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("//a[text()='Birth and Death Details']")).Click();
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                string primaryName = DoDay2PrimaryName(myEnrollment.myDay2TestId);
                string primaryPath = FindPrimaryEvidence(driver, primaryName); 
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
                System.Threading.Thread.Sleep(4000);

                driver.SwitchTo().DefaultContent();
                IWebElement fourthSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[4]"));
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

        public int DoNewEvidenceESC(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[35]/td[3]/span/span/span")).Click();//esc actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

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
                
                //System.Threading.Thread.Sleep(3000);
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

        public int DoNewEvidenceAppDetails(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[7]/td[3]/span/span/span")).Click();//app details actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();                
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                
                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                //System.Threading.Thread.Sleep(2000);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                System.Threading.Thread.Sleep(1000);
                IWebElement textboxAppDate = driver.FindElement(By.Id("__o3id9"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxAppDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxAppDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }
                

                IWebElement textboxMotivationType = driver.FindElement(By.Id("__o3idc"));
                textboxMotivationType.Clear();
                textboxMotivationType.SendKeys("Insurance Affordability");

                IWebElement textboxDetermination = driver.FindElement(By.Id("__o3ide"));
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

        public int DoNewEvidenceSSNDetails(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[87]/td[3]/span/span/span")).Click();//ssn details actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                System.Threading.Thread.Sleep(1000);
                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                System.Threading.Thread.Sleep(1000);
                IWebElement textboxSSN = driver.FindElement(By.Id("__o3id6"));
                textboxSSN.SendKeys(myEnrollment.mySSNNum);

                System.Threading.Thread.Sleep(1000);
                IWebElement textboxAppDate = driver.FindElement(By.Id("__o3id9"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxAppDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxAppDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
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

        public int DoNewEvidenceMaritalStatus(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[60]/td[3]/span/span/span")).Click();//marital status actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxStatus = driver.FindElement(By.Id("__o3id6"));
                textboxStatus.SendKeys(myEnrollment.myMaritalStatus);

                IWebElement textboxDate = driver.FindElement(By.Id("__o3id7"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
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

        public int DoNewEvidenceStateResidency(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[90]/td[3]/span/span/span")).Click();//state residency actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxDate = driver.FindElement(By.Id("__o3id6"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                IWebElement textboxStateRes = driver.FindElement(By.Id("__o3id8"));
                System.Threading.Thread.Sleep(1000);
                textboxStateRes.Click();

                IWebElement textboxCounty = driver.FindElement(By.Id("__o3ida"));
                textboxCounty.SendKeys(myEnrollment.myHomeCounty);

                IWebElement textboxHomeless = driver.FindElement(By.Id("__o3idb"));
                textboxHomeless.SendKeys(myEnrollment.myHomeless);

                IWebElement textboxMNHome = driver.FindElement(By.Id("__o3idc"));
                textboxMNHome.SendKeys(myEnrollment.myPlanLiveMN);

                IWebElement textboxEmployment = driver.FindElement(By.Id("__o3idd"));
                textboxEmployment.SendKeys("No");

                IWebElement textboxMedicalCare = driver.FindElement(By.Id("__o3id12"));
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

        public int DoNewEvidenceParticipantAddress(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[75]/td[3]/span/span/span")).Click();//participant addr actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxAddress = driver.FindElement(By.Id("__o3id3"));
                textboxAddress.Click();

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[5]/div[2]/a/span/span/span"));
                buttonNext.Click();
                System.Threading.Thread.Sleep(3000);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[4]/div[2]/a[2]/span/span/span"));
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

        public int DoPersonNewEvidenceAddress(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_addNewEvidencePage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/table/tbody/tr[1]/td[3]/span/span/span")).Click();//addresses actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/PDCEvidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

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

                System.Threading.Thread.Sleep(4000);
                //driver.SwitchTo().DefaultContent(); //sometimes the new evidence screen does not close by itself
                //driver.FindElement(By.XPath("/html/body/div[4]/div[1]/span[5]")).Click();//close new evidence

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

        public int DoNewEvidenceParticipantAddressMailing(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[75]/td[3]/span/span/span")).Click();//participant addr actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxAddress = driver.FindElement(By.Id("__o3id3"));
                textboxAddress.Click();

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[5]/div[2]/a/span/span/span"));
                buttonNext.Click();
                System.Threading.Thread.Sleep(3000);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxType = driver.FindElement(By.Id("__o3id7"));
                textboxType.Clear();
                textboxType.SendKeys("Mailing");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[4]/div[2]/a[2]/span/span/span"));
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

        public int DoNewEvidenceProjectedAnnualIncome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[79]/td[3]/span/span/span")).Click();//projected annual income actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxAmount = driver.FindElement(By.Id("__o3id6"));
                textboxAmount.SendKeys("0");

                IWebElement textboxDate = driver.FindElement(By.Id("__o3id8"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
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

        public int DoNewEvidenceMedicaid(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[61]/td[3]/span/span/span")).Click();//medicaid enrollment actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

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

        public int DoNewEvidenceBirth(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[16]/td[3]/span/span/span")).Click();//birth and death details actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

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

        public int DoNewEvidenceRelationship(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[63]/td[3]/span/span/span")).Click();//member relationship actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";                

                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxRelationship = driver.FindElement(By.Id("__o3id7"));
                textboxRelationship.SendKeys("Is the Spouse of");

                IWebElement textboxDate = driver.FindElement(By.Id("__o3ida"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
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

        public int DoNewEvidenceCitizenStatus(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[20]/td[3]/span/span/span")).Click();//citizen status actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                IWebElement textboxDate = driver.FindElement(By.Id("__o3idb"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
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

        public int DoNewEvidenceTaxStatus(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[93]/td[3]/span/span/span")).Click();//tax filing status actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

                IWebElement textboxParticipant = driver.FindElement(By.Id("__o3id5"));
                textboxParticipant.Clear();
                textboxParticipant.SendKeys(fullName);

                System.Threading.Thread.Sleep(1000);
                IWebElement textboxTaxStatus = driver.FindElement(By.Id("__o3id6"));
                textboxTaxStatus.SendKeys("Tax Filer");

                IWebElement textboxJointly = driver.FindElement(By.Id("__o3id7"));
                textboxJointly.Click();

                IWebElement textboxDate = driver.FindElement(By.Id("__o3idb"));
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    textboxDate.SendKeys(myHistoryInfo.myTimeTravelDate.ToString("MM/dd/yyyy"));
                }
                else
                {
                    textboxDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
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

        public int DoNewEvidenceGender(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[49]/td[3]/span/span/span")).Click();//gender actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add…')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                string first = myEnrollment.myFirstName;
                string last = myEnrollment.myLastName;
                DateTime birth = Convert.ToDateTime(myEnrollment.myDOB);
                TimeSpan span;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    span = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth;
                }
                else
                {
                    span = DateTime.Now - birth;
                }
                DateTime fullAge = DateTime.MinValue + span;
                String age = Convert.ToString(fullAge.Year - 1);
                string fullName = first + " " + last + " (" + age + ")";

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

        public int DoValidateEvidenceChanges(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Validate Changes')]")).Click(); //validate changes button

                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_validateChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                IWebElement checkallbox = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/thead/tr/th[1]/input"));
                checkallbox.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
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

        public int DoApplyEvidenceChanges(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Apply Changes')]")).Click(); //apply changes button

                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCREvidence_applyChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                IWebElement checkallbox = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/thead/tr/th[1]/input"));
                checkallbox.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonSave = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonSave.Click();
                System.Threading.Thread.Sleep(15000);

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

        public int DoAppFilerConsent(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, 
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                //System.Threading.Thread.Sleep(2000);
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[1]/div"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[1]/div")).Click();//dashboard

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/div[4]/div[2]/div/div/table/tbody/tr[2]/td[1]/a")).Click();//application filer
                
                System.Threading.Thread.Sleep(5000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_workspaceTypeListPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);
                
                driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//toggle

                //System.Threading.Thread.Sleep(2000);
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'Evidence_listEvdInstanceChangesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);
                
                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//toggle2

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

        public int DoVerification(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                ApplicationDo myApp = new ApplicationDo();
                if (myEnrollment.myRenewalCov == "0")
                {
                    driver.SwitchTo().DefaultContent();
                    driver.FindElement(By.LinkText("Person…")).Click();//select person... tab
                    
                    myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                    var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                    driver.SwitchTo().Frame(iFrameElement);

                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click();//select person link

                    driver.SwitchTo().DefaultContent();
                    var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement2);

                    myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'HCRIC_home')]"));
                    driver.FindElement(By.XPath("//a[contains(@href,'HCRIC_home')]")).Click(); //select insurance affordability

                    driver.SwitchTo().DefaultContent();
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div"));
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div")).Click();//select  evidence
                }

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[5]/div"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[5]/div")).Click();//select verifications

                System.Threading.Thread.Sleep(4000);
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

        public int DoTasks(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, 
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[8]/div/div/div/span[1]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[8]/div/div/div/span[1]")).Click();

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

        public int DoCloseTasks(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/DefaultIC_listTaskPage.do')]"));
                
                driver.SwitchTo().DefaultContent();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultIC_listTaskPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);                 

                //Find outstanding tasks
                var elems = driver.FindElements(By.XPath("//a[@class='field-link']"));
                IList<IWebElement> list = elems;
                for (int j = 0; j < list.Count; j++)
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/table/tbody/tr/td[2]/a")).Click();//select link
                    System.Threading.Thread.Sleep(4000);

                    driver.SwitchTo().DefaultContent();
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div[3]/div[3]/div[2]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click();//select actions
                    driver.FindElement(By.XPath("//td[contains(text(), 'Add To My Tasks')]")).Click(); //add to my tasks
                    System.Threading.Thread.Sleep(2000);

                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    driver.SwitchTo().DefaultContent();
                    var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/TaskManagement_ReserveTaskPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement2);
                    
                    driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //save and view
                    System.Threading.Thread.Sleep(5000);

                    driver.SwitchTo().DefaultContent();
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div[3]/div[3]/div[2]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click();//select actions
                    driver.FindElement(By.XPath("//td[contains(text(), 'Close')]")).Click(); //close
                    System.Threading.Thread.Sleep(2000);

                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    driver.SwitchTo().DefaultContent();
                    var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/TaskManagement_closeTaskPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement3);
                    
                    driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //save
                    System.Threading.Thread.Sleep(5000);

                    driver.SwitchTo().DefaultContent();
                    IWebElement firstTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[3]/div[3]/div[1]/div[4]/div/div[2]"));
                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                    var rClick = action.ContextClick(firstTab); //right click
                    //System.Threading.Thread.Sleep(2000);
                    rClick.Perform();
                    //driver.FindElement(By.XPath("//span[contains(text(), 'Close')]")).Click(); //close
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

        public int DoAddProof(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRIC_listVerificationsForCasePage.do')]"));               
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
                    IWebElement hh1last;
                    IWebElement hh1ageplus;
                    if (householdCount == 1)
                    {
                        hh1first = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div/div[2]/div[3]/div/a"));
                        hh1last = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div/div[2]/div[4]/div/a"));
                        hh1ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div/div[2]/div[6]/div[2]"));
                    }
                    else
                    {
                        hh1first = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[1]/div/div[2]/div[3]/div/a"));
                        hh1last = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[1]/div/div[2]/div[4]/div/a"));
                        hh1ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[1]/div/div[2]/div[6]/div[2]"));                        
                    }
                    string hh1firstname = hh1first.Text;
                    string hh1lastname = hh1last.Text;
                    string hh1fullname = hh1firstname + " " + hh1lastname;
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
                        IWebElement hh2first = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div[2]/div[3]/div/a"));
                        hh2firstname = hh2first.Text;
                        IWebElement hh2last = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div[2]/div[4]/div/a"));
                        hh2lastname = hh2last.Text;
                        hh2fullname = hh2firstname + " " + hh2lastname;
                        IWebElement hh2ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[2]/div/div[2]/div[6]/div[2]"));
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
                        IWebElement hh3first = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[3]/div/div[2]/div[3]/div/a"));
                        hh3firstname = hh3first.Text;
                        IWebElement hh3last = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[3]/div/div[2]/div[4]/div/a"));
                        hh3lastname = hh3last.Text;
                        hh3fullname = hh3firstname + " " + hh3lastname;
                        IWebElement hh3ageplus = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/div[1]/div/div/div/div[3]/div/div[2]/div[6]/div[2]"));
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

                    driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div/table/tbody/tr[1]/td[7]/span/span/span")).Click();//select arrow
                    driver.FindElement(By.XPath("/html/body/div[4]/table/tbody/tr/td[2]")).Click();//select add proof

                    driver.SwitchTo().Window(driver.WindowHandles.Last());

                    myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/VerificationApplication_createVerificationItemProvisionPage.do')]"));
                    var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/VerificationApplication_createVerificationItemProvisionPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement3);

                    IWebElement participant = driver.FindElement(By.Id("__o3id2"));
                    participant.SendKeys(p + " (" + age + ")");

                    System.Threading.Thread.Sleep(2000);
                    writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                    driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]")).Click();//select save
                    System.Threading.Thread.Sleep(6000);
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

        public int DoMAHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.LinkText("Person…"));
                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));              
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
                        textboxSSN.SendKeys(myEnrollment.mySSNNum);
                        DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                        //textboxSSN.SendKeys("344688097"); 
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

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"));
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));             
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"));               
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh

                myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'StreamlinedMedicaid_home')]"));               
                driver.FindElement(By.XPath("//a[contains(@href,'StreamlinedMedicaid_home')]")).Click(); //select ma

                System.Threading.Thread.Sleep(5000); 
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

        public int DoMAActivateCase(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, 
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                //System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click(); //actions button
                
                driver.FindElement(By.XPath("//td[contains(text(), 'Activate Case')]")).Click(); //activate case button

                System.Threading.Thread.Sleep(4000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().Window(driver.WindowHandles.Last());

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_activatePage.do')]"));
                
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_activatePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                //System.Threading.Thread.Sleep(2000);
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

        public int DoClosePDC(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                //System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click(); //actions button

                driver.FindElement(By.XPath("//td[contains(text(), 'Close Case')]")).Click(); //close case button

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().Window(driver.WindowHandles.Last());

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_closePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_closePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                //IWebElement textboxDate = driver.FindElement(By.Id("__o3id0"));
                //textboxDate.SendKeys("04/01/2016");

                IWebElement textboxOutcome = driver.FindElement(By.Id("__o3id1"));
                textboxOutcome.Clear();
                textboxOutcome.SendKeys("Not Attained");

                IWebElement textboxReason = driver.FindElement(By.Id("__o3id2"));
                textboxReason.Clear();
                textboxReason.SendKeys("Not Eligible");

                //System.Threading.Thread.Sleep(2000);
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

        public int DoCloseIC(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                //System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click(); //actions button

                driver.FindElement(By.XPath("//td[contains(text(), 'Close Case')]")).Click(); //close case button

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().Window(driver.WindowHandles.Last());

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/IntegratedCase_closePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/IntegratedCase_closePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxComments = driver.FindElement(By.Id("__o3id1"));
                textboxComments.SendKeys("Closed the case - HH member is deceased");

                //System.Threading.Thread.Sleep(2000);
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

        public int DoBHPHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(6000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.LinkText("Person…"));                
                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));               
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
                        textboxSSN.SendKeys(myEnrollment.mySSNNum);
                        DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                        //textboxSSN.SendKeys("344688097"); 
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

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"));               
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));               
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"));               
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'HCRStateBasicHealthPlanPDHome')]"));                
                driver.FindElement(By.XPath("//a[contains(@href,'HCRStateBasicHealthPlanPDHome')]")).Click(); //select bhp
                
                System.Threading.Thread.Sleep(5000);
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

        public int DoQHPHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(6000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.LinkText("Person…"));
                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));               
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
                        textboxSSN.SendKeys(myEnrollment.mySSNNum);
                        DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                        //textboxSSN.SendKeys("344688097"); 
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

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"));              
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));               
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"));               
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'HCRInsuranceAssistance')]"));               
                driver.FindElement(By.XPath("//a[contains(@href,'HCRInsuranceAssistance')]")).Click(); //select qhp

                System.Threading.Thread.Sleep(5000);
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

        public int DoUQHPHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.LinkText("Person…"));
                driver.FindElement(By.LinkText("Person…")).Click();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));                
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
                        textboxSSN.SendKeys(myEnrollment.mySSNNum);
                        DoUpdateSSN(myHistoryInfo, myEnrollment.mySSNNum, myEnrollment.myFirstName, myEnrollment.myLastName);
                        //textboxSSN.SendKeys("344688097"); 
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

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"));               
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));                
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div/div[3]/a[1]"));               
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                myApp.DoWaitForElement(driver, By.XPath("//a[contains(@href,'HCRUnassistedQualifiedHealthPlanHome')]"));                
                driver.FindElement(By.XPath("//a[contains(@href,'HCRUnassistedQualifiedHealthPlanHome')]")).Click(); //select uqhp

                System.Threading.Thread.Sleep(5000);
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

        public int DoDeterminations(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, 
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(10000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                if (myEnrollment.myRenewalCov == "0")
                {
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[5]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"));
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[5]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //determinations tab
                }
                else
                {
                    myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"));
                    driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //determinations tab
                }

                System.Threading.Thread.Sleep(4000);
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/DefaultICProduct_resolveDeterminationCurrentPage.do')]"));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultICProduct_resolveDeterminationCurrentPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                /*new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div/div[2]/a[1]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/a[1]")).Click();//select refresh
                System.Threading.Thread.Sleep(2000);
                */
                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click(); //coverage period arrow                

                System.Threading.Thread.Sleep(4000);
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

        public int DoDecision(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/div[3]/div[2]/div/table/tbody/tr[1]/td[2]/a"));                
                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/table/tbody/tr[1]/td[2]/a")).Click(); //coverage link

                System.Threading.Thread.Sleep(5000);
                driver.SwitchTo().DefaultContent();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[6]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"));

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

        public int DoIncome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[6]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[6]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //income tab

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[6]/div/div[4]/div/div/div[2]/div[3]/div/ul/li[2]/div"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[6]/div/div[4]/div/div/div[2]/div[3]/div/ul/li[2]/div")).Click(); //income tab

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

    }
}
