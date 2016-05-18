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

                driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")).SendKeys("USRST117");//Enter username
                myAccountCreate.myCaseWorkerLoginId = "USRST117";

                driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[2]/div/input")).SendKeys("Welcome@12345");//Enter password

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
                driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input")).SendKeys("USRST117");//Enter username
                myAccountCreate.myCaseWorkerLoginId = "USRST117";

                driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[2]/td/input")).SendKeys("Welcome@12345");//Enter password

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
                System.Threading.Thread.Sleep(10000);
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

                    /*driver.FindElement(By.Id("dijit_layout_AccordionPane_1_button")).Click();//searches             
                    driver.FindElement(By.LinkText("Person…")).Click();

                    IWebElement personSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[1]"));
                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                    var rClick = action.ContextClick(personSearchTab); //right click
                    System.Threading.Thread.Sleep(1000);
                    rClick.Perform();
                    driver.FindElement(By.XPath("/html/body/div[3]/table/tbody/tr[2]/td[1]")).Click();//close all tabs
                    System.Threading.Thread.Sleep(1000);

                    driver.FindElement(By.LinkText("Person…")).Click();*/
                }
                /*else
                {
                    driver.FindElement(By.Id("dijit_layout_AccordionPane_1_button")).Click();//searches             
                    driver.FindElement(By.LinkText("Person…")).Click();
                }*/

                driver.FindElement(By.Id("dijit_layout_AccordionPane_1_button")).Click();//searches             
                driver.FindElement(By.LinkText("Person…")).Click();

                IWebElement firstSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[1]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(firstSearchTab); //right click
                System.Threading.Thread.Sleep(2000);
                rClick.Perform();
                //driver.FindElement(By.XPath("/html/body/div[3]/table/tbody/tr[2]/td[1]")).Click();
                //driver.FindElement(By.XPath("/html/body/div[5]/table/tbody/tr[2]/td[2]")).Click();
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
                    textboxSSN.SendKeys(myEnrollment.mySSNNum);
                    //textboxSSN.SendKeys("344688097");                    
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

        public int DoEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,  
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div"));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div")).Click();

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

        public int DoNewEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                System.Threading.Thread.Sleep(8000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRDefaultIC_dashboardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);                 
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/span/span/span")).Click();//actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'New Evidence')]")).Click(); //new evidence button

                System.Threading.Thread.Sleep(1000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);                
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/div/table/tbody/tr[35]/td[3]/span/span/span")).Click();//esc actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Add')]")).Click(); //new evidence button

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
                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Evidence_addNewEvidencePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);
                IWebElement textboxEmploymentType = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/div/div/table/tbody/tr[1]/td[1]/div/div[3]/input[1]"));
                textboxEmploymentType.SendKeys("Full Time");
                IWebElement textboxCoverageStatus = driver.FindElement(By.Id("__o3id7"));
                textboxCoverageStatus.SendKeys("Enrolled");
                IWebElement textboxStartDate = driver.FindElement(By.Id("__o3id8"));
                textboxStartDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
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
                    /*IWebElement participantArrow = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/div/table/tbody/tr[1]/td/div/div/table/tbody/tr/td/div/div[1]"));
                     * participantArrow.Click();//select participant arrow, this needs to be changed to select the correct participant (2hh and 3hh)
                    System.Threading.Thread.Sleep(1000);
                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();*/

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
                    textboxSSN.SendKeys(myEnrollment.mySSNNum);
                    //textboxSSN.SendKeys("344687079");                    
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
                    textboxSSN.SendKeys(myEnrollment.mySSNNum);
                    //textboxSSN.SendKeys("344687079");                    
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
                    textboxSSN.SendKeys(myEnrollment.mySSNNum);
                    //textboxSSN.SendKeys("344687079");                    
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
                    textboxSSN.SendKeys(myEnrollment.mySSNNum);
                    //textboxSSN.SendKeys("344687079");                    
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

        

    }
}
