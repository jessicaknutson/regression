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

        public int DoCaseWorkerLogin(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                driver.Manage().Window.Maximize();
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"))));
                driver.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a")).Click();//Click signin button so you can signout as individual

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div/div/div/div/div[2]/div[1]/div/form/div[1]/div/input")));
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

        public int DoCaseWorkerLoginTimeTravel(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                driver.Manage().Window.Maximize();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"))));

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

        public int DoHCRCases(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(15000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel")));

                driver.FindElement(By.Id("app-sections-container-dc_tablist_HCRCASEAPPWorkspaceSection-sbc_tabLabel")).Click();//hcr cases tab

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div")));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[1]/div[1]/div")).Click(); //shortcuts tab

                driver.FindElement(By.Id("dijit_layout_AccordionPane_1_button")).Click();//searches
                driver.FindElement(By.LinkText("Person…")).Click();

                IWebElement personSearchTab = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[1]"));
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                var rClick = action.ContextClick(personSearchTab); //right click
                rClick.Perform();
                driver.FindElement(By.XPath("/html/body/div[3]/table/tbody/tr[2]/td[1]")).Click();//close all tabs

                driver.FindElement(By.LinkText("Person…")).Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]")));
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[1]/div/table/tbody/tr/td[1]/input")).SendKeys(myEnrollment.mySSNNum);
                //driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[1]/div/table/tbody/tr/td[1]/input")).SendKeys("344685854");
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search by ssn

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

        public int DoPersonHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                //make this wait longer, sometimes it doesn't show up right away
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")));

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click();

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

        public int DoNotification(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[2]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[1]/div[4]/div/div[2]")).Click();//select person tab

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[6]/div/div/div/span[1]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[6]/div/div/div/span[1]")).Click();//select notification tab

                System.Threading.Thread.Sleep(3000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/MNHIX_listNoticesPage.do')]")));
                var iFrameElement4 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIX_listNoticesPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement4);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a"))));
                driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click();//select down arrow

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'MNHIX_viewNoticePage.do')]")));
                var iFrameElement5 = driver.FindElement(By.XPath("//iframe[contains(@src,'MNHIX_viewNoticePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement5);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[5]/div/table/tbody/tr/td[1]/a"))));
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

        public int DoIAHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                driver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(4000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]")));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div/div[3]/a[1]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh, ia and ma may not be here yet
                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("//a[contains(@href,'HCRIC_home')]"))));
                driver.FindElement(By.XPath("//a[contains(@href,'HCRIC_home')]")).Click(); //select insurance affordability

                System.Threading.Thread.Sleep(7000);
                driver.SwitchTo().DefaultContent();
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/HCRIC_homePage.do')]")));
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRIC_homePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]"))));
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

        public int DoEvidence(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                driver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(2000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[2]/div/div")).Click();

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

        public int DoVerification(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[5]/div"))));
                driver.FindElement(By.XPath("html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[2]/div[2]/div/ul/li[5]/div")).Click();

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

        public int DoTasks(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                driver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(2000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[8]/div/div/div/span[1]"))));
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

        public int DoAddProof(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("//iframe[contains(@src,'en_US/HCRIC_listVerificationsForCasePage.do')]"))));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRIC_listVerificationsForCasePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                //Find outstanding verification items
                var elems = driver.FindElements(By.XPath("//td[@class='last-field list-row-menu']"));
                IList<IWebElement> list = elems;
                for (int j = 0; j < list.Count; j++)
                {
                    driver.SwitchTo().DefaultContent();
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("//iframe[contains(@src,'en_US/HCRIC_listVerificationsForCasePage.do')]"))));
                    var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRIC_listVerificationsForCasePage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement2);

                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[2]/div[3]/div/div/table/tbody/tr[1]/td[7]/span/span/span"))));
                    driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div/table/tbody/tr[1]/td[7]/span/span/span")).Click();//select arrow
                    driver.FindElement(By.XPath("/html/body/div[4]/table/tbody/tr/td[2]")).Click();//select add proof

                    System.Threading.Thread.Sleep(2000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());

                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/VerificationApplication_createVerificationItemProvisionPage.do')]")));
                    var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/VerificationApplication_createVerificationItemProvisionPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement3);

                    IWebElement participantArrow = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/div/table/tbody/tr[1]/td/div/div/table/tbody/tr/td/div/div[1]"));
                    participantArrow.Click();//select participant arrow
                    System.Threading.Thread.Sleep(1000);
                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

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

        public int DoMAHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(6000);
                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.LinkText("Person…")).Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]")));
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search button

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"))));
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]")));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div/div[3]/a[1]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("//a[contains(@href,'StreamlinedMedicaid_home')]"))));
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

        public int DoMAActivateCase(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click(); //actions button
                driver.FindElement(By.XPath("//td[contains(text(), 'Activate Case…')]")).Click(); //activate case button

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().Window(driver.WindowHandles.Last());

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/ProductDelivery_activatePage.do')]")));
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

        public int DoBHPHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(6000);
                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.LinkText("Person…")).Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]")));
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search button

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"))));
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]")));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div/div[3]/a[1]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("//a[contains(@href,'HCRStateBasicHealthPlanPDHome')]"))));
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

        public int DoQHPHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(6000);
                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.LinkText("Person…")).Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]")));
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search button

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"))));
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]")));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div/div[3]/a[1]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("//a[contains(@href,'HCRInsuranceAssistance')]"))));
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

        public int DoUQHPHome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.LinkText("Person…")).Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]")));
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_search1Page.do?o3ctx=4096')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[3]/a[1]/span/span/span")).Click(); //search button

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a"))));
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/div/table/tbody/tr[1]/td[2]/div/div/a")).Click(); //select person

                driver.SwitchTo().DefaultContent();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]")));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Person_homePagePDCPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div/div[3]/a[1]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[3]/a[1]")).Click();//select refresh
                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("//a[contains(@href,'HCRUnassistedQualifiedHealthPlanHome')]"))));
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

        public int DoDeterminations(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                System.Threading.Thread.Sleep(8000);
                driver.SwitchTo().DefaultContent();

                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[4]/div/div[4]/div/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //determinations tab
                System.Threading.Thread.Sleep(4000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'en_US/DefaultICProduct_resolveDeterminationCurrentPage.do')]")));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultICProduct_resolveDeterminationCurrentPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div/div[2]/a[1]"))));
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/a[1]")).Click();//select refresh
                System.Threading.Thread.Sleep(2000);

                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/table/tbody/tr[1]/td[1]/a")).Click(); //coverage period arrow                

                System.Threading.Thread.Sleep(4000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;//gp added 27 new change Greg jess
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

        public int DoCuramBuild(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, ref mystructHistoryInfo myHistoryInfo,
           ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            int timeOut = myHistoryInfo.myCaseWorkerWait;

            try
            {
                driver.SwitchTo().DefaultContent();
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[3]/span/span/span/span[3]"))));
                IWebElement myHelpArrow = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div[2]/div[2]/ul/li[3]/span/span/span/span[3]"));
                myHelpArrow.Click();

                IWebElement myHelpAbout = driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr/td[3]"));
                myHelpAbout.Click();

                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/HCRIC_homePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                IWebElement myIcnum = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/table/tbody/tr[1]/td[1]"));
                myHistoryInfo.myIcnumber = myIcnum.Text;

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
