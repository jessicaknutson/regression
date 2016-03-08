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

        public int DoEnrollMNsureMA(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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
                if (tempI <= 24500)
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

        public int DoEstimator(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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

        public int DoEnroll(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                driver.SwitchTo().DefaultContent();

                System.Threading.Thread.Sleep(3000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span"))));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                
                IWebElement buttonEnroll = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSelectHH(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                //check for text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_2"))));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonContinue2 = driver.FindElement(By.Id("dijit_form_Button_2"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoFindProvider(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(40000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iFrame")));

                var iFrameElement = driver.FindElement(By.TagName("iFrame"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().Frame(iFrameElement);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.CssSelector("a.buttonNext._startPA")));
                IWebElement buttonStart = driver.FindElement(By.CssSelector("a.buttonNext._startPA"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoPlanType(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                //check for link Skip to Plans
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.CssSelector("a._skipNavigation._viewPlans"))));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                // click skip to plan
                IWebElement linkSkipToPlan = driver.FindElement(By.CssSelector("a._skipNavigation._viewPlans"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }
        public int DoPrivacy(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                //check for checkbox acceptance
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("checkAcceptance"))));

                IWebElement checkboxAccept = driver.FindElement(By.Id("checkAcceptance"));
                checkboxAccept.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement checkboxNext2 = driver.FindElement(By.Id("nextPlansMessage"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }
        public int DoPlanDetails(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[1]/div[3]/input")));

                IWebElement checkboxFirstOne = driver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[1]/div[3]/input"));
                checkboxFirstOne.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //check for text at the bottom
                IWebElement buttonSelectThisPlan = driver.FindElement(By.CssSelector("a.buttonPrimary._enroll"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoPlanSummary(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"))));
                IWebElement buttonEnroll3 = driver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"));
                buttonEnroll3.Click();

                System.Threading.Thread.Sleep(18000);
                driver.SwitchTo().DefaultContent();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iFrame")));

                var iFrameElement3 = driver.FindElement(By.TagName("iFrame"));
                driver.SwitchTo().Frame(iFrameElement3);
                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.CssSelector("a.buttonNext"))));
                IWebElement buttonContinue = driver.FindElement(By.CssSelector("a.buttonNext"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoTax(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
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

        public int DoConfirmQHP(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(6000);                
                //check for text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Name("enrollment.individual.signature.firstName"))));
                IWebElement textboxSignatureFirst = driver.FindElement(By.Name("enrollment.individual.signature.firstName"));
                textboxSignatureFirst.SendKeys(myEnrollment.myFirstName);

                IWebElement textboxSignatureMiddle = driver.FindElement(By.Name("enrollment.individual.signature.middleInitial"));
                textboxSignatureMiddle.SendKeys(myEnrollment.myMiddleName);

                IWebElement textboxSignatureLast = driver.FindElement(By.Name("enrollment.individual.signature.lastName"));
                textboxSignatureLast.SendKeys(myEnrollment.myLastName);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

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

        public int DoConfirmUQHP(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                //check for text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Name("enrollment.individual.signature.firstName"))));
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement textboxSignatureFirst = driver.FindElement(By.Name("enrollment.individual.signature.firstName"));
                textboxSignatureFirst.SendKeys(myEnrollment.myFirstName);

                IWebElement textboxSignatureMiddle = driver.FindElement(By.Name("enrollment.individual.signature.middleInitial"));
                textboxSignatureMiddle.SendKeys(myEnrollment.myMiddleName);


                IWebElement textboxSignatureLast = driver.FindElement(By.Name("enrollment.individual.signature.lastName"));
                textboxSignatureLast.SendKeys(myEnrollment.myLastName);

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

        public int DoSuccessfulQHP(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                driver.SwitchTo().Frame(iFrameElement);            

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("back_curam")));
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonDone = driver.FindElement(By.Id("back_curam"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSuccessfulUQHP(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.TagName("iFrame")));
                var iFrameElement4 = driver.FindElement(By.TagName("iFrame"));
                driver.SwitchTo().Frame(iFrameElement4);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("back_curam"))));

                IWebElement buttonDone = driver.FindElement(By.Id("back_curam"));
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
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSignature(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
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
