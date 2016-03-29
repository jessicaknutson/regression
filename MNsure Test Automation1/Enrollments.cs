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

using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI; /// for dropdown


using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace MNsure_Regression_1
{
    class Enrollments
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoEnrollMNsureMA(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));

            try
            {
                driver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(3000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[1]/div[1]")));
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                int tempI;
                tempI = Convert.ToInt32(myEnrollment.myIncomeAmount);
                if (tempI <= 23540)
                {
                    //check for text at the bottom
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[1]/div[2]/div[1]"))));
                    System.Threading.Thread.Sleep(1000);
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

        public int DoEstimator(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iFrame")));

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

        public int DoEnroll(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                myDriver.SwitchTo().DefaultContent();
                System.Threading.Thread.Sleep(3000);

                if (myHistoryInfo.myRelogin == "Yes")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span/span/span/span[3]/span"))));
                } 
                else
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span"))));
                }

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                IWebElement buttonEnroll;
                if (myHistoryInfo.myRelogin == "Yes")
                {
                    buttonEnroll = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span/span/span/span[3]/span"));
                }
                else
                {
                    buttonEnroll = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span"));
                }

                buttonEnroll.Click();                

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

        public int DoSelectHH(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(2000);
                //check for text at the bottom
                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_2"))));

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement buttonContinue2 = myDriver.FindElement(By.Id("dijit_form_Button_2"));
                buttonContinue2.Click();

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

        public int DoFindProvider(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(40000);
                WebDriverWait wait = new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iFrame")));

                var iFrameElement = myDriver.FindElement(By.TagName("iFrame"));

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                myDriver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.CssSelector("a.buttonNext._startPA")));
                IWebElement buttonStart = myDriver.FindElement(By.CssSelector("a.buttonNext._startPA"));
                buttonStart.Click();

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

        public int DoPlanType(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(2000);
                //check for link Skip to Plans
                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.CssSelector("a._skipNavigation._viewPlans"))));

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                // click skip to plan
                IWebElement linkSkipToPlan = myDriver.FindElement(By.CssSelector("a._skipNavigation._viewPlans"));
                linkSkipToPlan.Click();

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

        public int DoPrivacy(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(2000);
                //check for checkbox acceptance
                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("checkAcceptance"))));

                IWebElement checkboxAccept = myDriver.FindElement(By.Id("checkAcceptance"));
                checkboxAccept.Click();

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement checkboxNext2 = myDriver.FindElement(By.Id("nextPlansMessage"));
                checkboxNext2.Click();

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

        public int DoPlanDetails(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[1]/div[3]/input")));

                IWebElement checkboxFirstOne = myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[1]/div[3]/input"));
                checkboxFirstOne.Click();

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                //check for text at the bottom
                IWebElement buttonSelectThisPlan = myDriver.FindElement(By.CssSelector("a.buttonPrimary._enroll"));
                buttonSelectThisPlan.Click();

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

        public int DoPlanSummary(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"))));
                IWebElement buttonEnroll3 = myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"));
                buttonEnroll3.Click();

                System.Threading.Thread.Sleep(25000);
                myDriver.SwitchTo().DefaultContent();
                WebDriverWait wait = new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iFrame")));

                var iFrameElement3 = myDriver.FindElement(By.TagName("iFrame"));
                myDriver.SwitchTo().Frame(iFrameElement3);
                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.CssSelector("a.buttonNext"))));
                IWebElement buttonContinue = myDriver.FindElement(By.CssSelector("a.buttonNext"));
                buttonContinue.Click();

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

        public int DoTax(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(3000);
                driver.SwitchTo().DefaultContent();
                IWebElement taxAmount = driver.FindElement(By.XPath("//div[@class='hcrBenefitValue']"));
                string tax = taxAmount.Text;
                tax = tax.Substring(0, 2);

                if (tax == "$0")
                {
                    //do nothing
                    writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                }
                else
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                    var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    driver.SwitchTo().Frame(iFrameElement);

                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Name("enrollment.individual.primaryTaxpayer"))));
                    IWebElement textboxSignature = driver.FindElement(By.Name("enrollment.individual.primaryTaxpayer"));
                    textboxSignature.SendKeys(myEnrollment.myFirstName + " " + myEnrollment.myLastName);

                    writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div[3]/div[3]/span[2]/a"))));
                    IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[3]/span[2]/a"));
                    buttonNext.Click();
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

        public int DoConfirmQHP(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(8000);
                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Name("enrollment.individual.signature.firstName"))));
                IWebElement textboxSignatureFirst = myDriver.FindElement(By.Name("enrollment.individual.signature.firstName"));
                textboxSignatureFirst.SendKeys(myEnrollment.myFirstName);

                IWebElement textboxSignatureMiddle = myDriver.FindElement(By.Name("enrollment.individual.signature.middleInitial"));
                textboxSignatureMiddle.SendKeys(myEnrollment.myMiddleName);

                IWebElement textboxSignatureLast = myDriver.FindElement(By.Name("enrollment.individual.signature.lastName"));
                textboxSignatureLast.SendKeys(myEnrollment.myLastName);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement buttonSubmit = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[3]/span[3]/a"));
                buttonSubmit.Click();

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

        public int DoConfirmUQHP(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(2000);
                //check for text at the bottom
                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Name("enrollment.individual.signature.firstName"))));
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement textboxSignatureFirst = myDriver.FindElement(By.Name("enrollment.individual.signature.firstName"));
                textboxSignatureFirst.SendKeys(myEnrollment.myFirstName);

                IWebElement textboxSignatureMiddle = myDriver.FindElement(By.Name("enrollment.individual.signature.middleInitial"));
                textboxSignatureMiddle.SendKeys(myEnrollment.myMiddleName);

                IWebElement textboxSignatureLast = myDriver.FindElement(By.Name("enrollment.individual.signature.lastName"));
                textboxSignatureLast.SendKeys(myEnrollment.myLastName);

                //every now and then it fails here stating that the signature doesn't match, i tried to set the suffix and nothing works, not sure what to do here, you can't get past this
                //IWebElement textboxSignatureSuffix = myDriver.FindElement(By.Name("enrollment.individual.signature.suffix"));
                //textboxSignatureLast.SendKeys(myEnrollment.mySuffix);

                IWebElement buttonSubmit = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[3]/span[3]/a"));
                buttonSubmit.Click();

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

        public int DoSuccessfulQHP(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(4000);
                myDriver.SwitchTo().DefaultContent();

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                myDriver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("back_curam")));
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement buttonDone = myDriver.FindElement(By.Id("back_curam"));
                buttonDone.Click();

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

        public int DoSuccessfulUQHP(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.TagName("iFrame")));
                var iFrameElement4 = myDriver.FindElement(By.TagName("iFrame"));
                myDriver.SwitchTo().Frame(iFrameElement4);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("back_curam"))));

                IWebElement buttonDone = myDriver.FindElement(By.Id("back_curam"));
                buttonDone.Click();

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

        public int DoViewEnrolledPlans(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                System.Threading.Thread.Sleep(2000);
                myDriver.SwitchTo().DefaultContent();

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/table/tbody/tr/td[1]/div/div[2]")));
                IWebElement buttonSubmit = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/table/tbody/tr/td[1]/div/div[2]"));
                buttonSubmit.Click();

                System.Threading.Thread.Sleep(4000);
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

        public int DoSignature(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(3000);
                //check for first name input box at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[8]/fieldset/div[2]/div[1]/div[1]/input"))));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement textboxSignatureFN = driver.FindElement(By.Name("enrollment.individual.signature.firstName"));
                textboxSignatureFN.SendKeys(myEnrollment.myFirstName);

                IWebElement textboxSignatureMI = driver.FindElement(By.Name("enrollment.individual.signature.middleInitial"));
                textboxSignatureMI.SendKeys(myEnrollment.myMiddleName);

                IWebElement textboxSignatureLN = driver.FindElement(By.Name("enrollment.individual.signature.lastName"));
                textboxSignatureLN.SendKeys(myEnrollment.myLastName);

                IWebElement buttonSubmit = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[3]/span[3]/a"));
                buttonSubmit.Click();

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
