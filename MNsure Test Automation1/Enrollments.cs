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
using OpenQA.Selenium.Support.UI;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace MNsure_Regression_1
{
    class Enrollments
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoEnrollMNsureMA(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));

            try
            {
                driver.SwitchTo().DefaultContent();
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

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[1]/div[1]")));
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                int tempI;
                tempI = Convert.ToInt32(myEnrollment.myIncomeAmount);
                if (tempI <= 23540)//must check for multi household amounts....
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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                driver.SwitchTo().DefaultContent();
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    DoWaitForEstimator(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers, By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    DoWaitForEstimator(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers, By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                }
                else
                {
                    DoWaitForEstimator(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers, By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
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

        public int DoEnroll(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                try
                {
                    if (myHistoryInfo.myRelogin == "Yes")
                    {
                        new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span/span/span/span[3]/span")));
                    }
                    else if (myEnrollment.myHouseholdOther == "No" && myEnrollment.myEnrollmentPlanType == "MN Care UQHP")
                    {
                        new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span/span")));
                    }
                    else
                    {
                        new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span")));
                    }

                    writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                    if (myHistoryInfo.myRelogin == "Yes")
                    {
                        myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span/span/span/span[3]/span")).Click();
                    }
                    else
                    {
                        HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                        int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                        if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 3)
                        {
                            FillStructures myFillStructures = new FillStructures();
                            int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        }
                        if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 3 && myHouseholdMembers.myHasIncome == "Yes" && myHouseholdMembers.myDependants == "No" && myHouseholdMembers.myTaxFiler == "Yes")
                        {
                            myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div[2]/div[2]/div[2]/div/span/span/span/span[3]/span")).Click();
                        }
                        else if (myEnrollment.myHouseholdOther == "No" && myEnrollment.myEnrollmentPlanType == "MN Care UQHP")
                        {
                            myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span/span")).Click();
                        }
                        else
                        {
                            myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span")).Click();
                        }
                    }
                }
                catch (NoSuchElementException)
                {
                    MessageBox.Show("Connecture Issue, one of the nodes is probably down, investigate further", "Connecture Issue", MessageBoxButtons.OK, MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSelectHH(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                if (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);
                }
                else
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);
                }

                IWebElement buttonContinue;
                if ((myEnrollment.myHouseholdOther == "No" && myEnrollment.myPassCount == "1")
                    || (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_2"))));
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_2"));
                }
                else if ((myEnrollment.myHouseholdOther == "No" && myEnrollment.myPassCount == "2")
                    || myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "2")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_4"))));
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_4"));
                }
                else if ((myEnrollment.myHouseholdOther == "No" && myEnrollment.myPassCount == "3")
                    || (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "3")
                    || (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2"))
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_6"))));
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_6"));
                }
                else if (myHouseholdMembers.myReEnroll == "Yes" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_6"))));
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_6"));
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input")).Click();
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[2]/div/input")).Click();
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[3]/div/input")).Click();
                }
                else if (myHouseholdMembers.myReEnroll == "Yes" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_8"))));
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_8"));
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input")).Click();
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[2]/div/input")).Click();
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[3]/div/input")).Click();
                }
                else if (myHouseholdMembers.myReEnroll == "Yes" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "3")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_10")))); //3rd pass multi hh
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_10"));
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input")).Click();
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[2]/div/input")).Click();
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[3]/div/input")).Click();
                }
                else if (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "3") //3rd pass multi hh
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_12"))));
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_12"));
                }
                else
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_14"))));
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_14"));
                }

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                ApplicationDo myApp = new ApplicationDo();
                if (myEnrollment.myHouseholdOther == "No" && myEnrollment.myPassCount == "1")
                {
                    myEnrollment.myPassCount = "2";//update count to 2 to do the screens another time
                    myApp.DoUpdateAppPassCount(myHistoryInfo, myEnrollment.myPassCount);
                    buttonContinue.Click();
                }
                else if (myEnrollment.myHouseholdOther == "No" && myEnrollment.myPassCount == "2")
                {
                    myEnrollment.myPassCount = "3";//update count to 2 to do the screens another time
                    myApp.DoUpdateAppPassCount(myHistoryInfo, myEnrollment.myPassCount);
                    buttonContinue.Click();
                }
                else
                {
                    buttonContinue.Click();
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSelectPrimary(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                if (myHouseholdMembers.myReEnroll == "Yes" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_8"))));
                    IWebElement checkboxPrimary = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input"));
                    checkboxPrimary.Click();
                }
                else if (myHouseholdMembers.myReEnroll == "Yes" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "3")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_16"))));
                    IWebElement checkboxPrimary = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input"));
                    checkboxPrimary.Click();
                }
                else if (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_4"))));
                    IWebElement checkboxPrimary = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input"));
                    checkboxPrimary.Click();
                }
                else if (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_8"))));
                    IWebElement checkboxPrimary = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input"));
                    checkboxPrimary.Click();
                }
                else
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_12"))));
                    IWebElement checkboxPrimary = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input"));
                    checkboxPrimary.Click();
                }

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                ApplicationDo myApp = new ApplicationDo();
                if (myHouseholdMembers.myPassCount == "1" && myHouseholdMembers.myReEnroll == "No")
                {
                    myHouseholdMembers.myPassCount = "2";//update count to 2 to do the screens another time
                    myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                    IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_4"));
                    buttonContinue.Click();
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2" && myHouseholdMembers.myReEnroll == "No")
                {
                    myHouseholdMembers.myPassCount = "3";//update count to 3 to do the screens another time
                    myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                    IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_8"));
                    buttonContinue.Click();
                }
                else
                {
                    if (myHouseholdMembers.myReEnroll == "Yes" && myHouseholdMembers.myPassCount == "1")
                    {
                        myHouseholdMembers.myPassCount = "2";//update count to 2 to do the screens another time
                        myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                        IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_8"));
                        buttonContinue.Click();
                    }
                    else if (myHouseholdMembers.myReEnroll == "Yes" && myHouseholdMembers.myPassCount == "2")
                    {
                        myHouseholdMembers.myPassCount = "3";//update count to 2 to do the screens another time
                        myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                        IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_12"));
                        buttonContinue.Click();
                    }
                    else if (myHouseholdMembers.myReEnroll == "Yes" && myHouseholdMembers.myPassCount == "3")
                    {
                        myHouseholdMembers.myPassCount = "4";//update count to 2 to do the screens another time
                        myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                        IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_16"));
                        buttonContinue.Click();
                    }
                    else
                    {
                        IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_12"));
                        buttonContinue.Click();
                        myHouseholdMembers.myPassCount = "4";//update count to 4 to do the screens another time
                        myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                    }
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoFindProvider(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                DoWaitForFindProvider(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers, By.CssSelector("a.buttonNext._startPA"));

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                /*driver.SwitchTo().DefaultContent();
                var iFrameElement = myDriver.FindElement(By.TagName("iFrame")); //needed for qhp
                myDriver.SwitchTo().Frame(iFrameElement);*/
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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.CssSelector("a._skipNavigation._viewPlans"))));

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement linkSkipToPlan = myDriver.FindElement(By.CssSelector("a._skipNavigation._viewPlans")); // click skip to plan
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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
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

        public int DoMedicalPlanDetails(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                IWebElement element;
                if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myReEnroll == "Yes")
                {
                    element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[2]/div[2]/div[3]/div/a[1]")));
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[2]/div[2]/div[3]/div/a[1]")).Click();
                }
                else
                {
                    element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[1]/div[2]/div[3]/div/a[1]")));
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[1]/div[2]/div[3]/div/a[1]")).Click();
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoDentalPlanDetails(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                myApp.DoWaitForElement(myDriver, By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[2]"), myHistoryInfo);

                myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[2]")).Click();
                System.Threading.Thread.Sleep(1000);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myReEnroll == "Yes")
                {
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[2]/div[2]/div[3]/div/a[1]")).Click();
                    myHouseholdMembers.myReEnroll = "No"; //update to reset for another run
                    DoUpdateReEnroll(myHistoryInfo, myHouseholdMembers.myReEnroll);
                }
                else
                {
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[1]/div[2]/div[3]/div/a[1]")).Click();
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoPlanSummary(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"))));
                IWebElement buttonEnroll3 = myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"));
                buttonEnroll3.Click();               

                DoWaitForPlanSummary(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers, By.CssSelector("a.buttonNext"));

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

        public int DoPlanSummaryExit(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"))));
                IWebElement buttonEnroll = myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"));
                buttonEnroll.Click();

                DoWaitForPlanSummary(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers, By.XPath("/html/body/div[1]/div[3]/div[3]/span[5]/a"));

                IWebElement buttonExit = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[3]/span[5]/a"));
                buttonExit.Click();

                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[8]/div/div[2]/input[2]"))));
                IWebElement buttonExit2 = myDriver.FindElement(By.XPath("/html/body/div[8]/div/div[2]/input[2]"));
                buttonExit2.Click();

                myHouseholdMembers.mySaveExit = "Yes"; //update saveexit to select primary
                DoUpdateSaveExit(myHistoryInfo, myHouseholdMembers.mySaveExit);
                myHouseholdMembers.myPassCount = "1";//update count to 1 to continue to next screens
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);

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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                IWebElement taxAmount = driver.FindElement(By.XPath("//div[@class='hcrBenefitValue']"));
                string tax = taxAmount.Text;
                tax = tax.Substring(1, tax.Length - 11);

                if (tax != "0")
                {
                    if (myHistoryInfo.myEnvironment == "STST")
                    {
                        new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                        var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                        driver.SwitchTo().Frame(iFrameElement);
                    }
                    else if (myHistoryInfo.myEnvironment == "STST2")
                    {
                        new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                        var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                        driver.SwitchTo().Frame(iFrameElement);
                    }
                    else
                    {
                        new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                        var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                        driver.SwitchTo().Frame(iFrameElement);
                    }

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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myDriver.SwitchTo().DefaultContent();
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                else
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[9]/fieldset/div[2]/div[1]/div[1]/input")));
                IWebElement textboxSignatureFirst = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[9]/fieldset/div[2]/div[1]/div[1]/input"));
                textboxSignatureFirst.SendKeys(myEnrollment.myFirstName);

                IWebElement textboxSignatureMiddle = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[9]/fieldset/div[2]/div[1]/div[2]/input"));
                textboxSignatureMiddle.SendKeys(myEnrollment.myMiddleName);

                IWebElement textboxSignatureLast = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[9]/fieldset/div[2]/div[1]/div[3]/input"));
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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myDriver.SwitchTo().DefaultContent();
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                else
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[9]/fieldset/div[2]/div[1]/div[1]/input")));
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement textboxSignatureFirst = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[9]/fieldset/div[2]/div[1]/div[1]/input"));
                textboxSignatureFirst.SendKeys(myEnrollment.myFirstName);

                if (myEnrollment.myWithDiscounts == "Yes")//only valid for the with discounts
                {
                    IWebElement textboxSignatureMiddle = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[9]/fieldset/div[2]/div[1]/div[2]/input"));
                    textboxSignatureMiddle.SendKeys(myEnrollment.myMiddleName);
                }

                IWebElement textboxSignatureLast = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[2]/form/div[9]/fieldset/div[2]/div[1]/div[3]/input"));
                textboxSignatureLast.SendKeys(myEnrollment.myLastName);

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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myDriver.SwitchTo().DefaultContent();

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                else
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]")));
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }

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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myDriver.SwitchTo().DefaultContent();

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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);                
               
                if (myHistoryInfo.myEnvironment == "STST2")
                {
                    myDriver.SwitchTo().DefaultContent();
                    //driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]")).Click();
                    //driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[1]/div/div[2]")).Click();
                    //driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[1]/div/div[2]/div[1]/div")).Click();                
                    //driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[1]/div/div[2]/div[1]/div/ul[2]/li[1]/a")).Click();
                    driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[1]/div/div[2]/div[1]/div/ul[2]/li[1]/a")).Click();
                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                    action.SendKeys(OpenQA.Selenium.Keys.Tab).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.PageUp).Build().Perform();

                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/CitizenAccount_homePage.do')]")));
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/CitizenAccount_homePage.do')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                    myDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/div/div[1]/div/div/a")).Click();   
                }
                myDriver.SwitchTo().DefaultContent();
                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/table/tbody/tr/td[1]/div/div[2]")));
                IWebElement buttonSubmit = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/table/tbody/tr/td[1]/div/div[2]"));
                buttonSubmit.Click();              
                    

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
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

        public int DoReEnrollPlans(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                myDriver.SwitchTo().DefaultContent();

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/table/tbody/tr/td[2]/div[1]")));
                IWebElement buttonViewDropdown = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/table/tbody/tr/td[2]/div[1]"));
                buttonViewDropdown.Click();

                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(myDriver);
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                myHouseholdMembers.myReEnroll = "Yes"; //update reenroll to do the screens another time
                DoUpdateReEnroll(myHistoryInfo, myHouseholdMembers.myReEnroll);
                if (myHouseholdMembers.myPassCount == "4")
                {
                    myHouseholdMembers.myPassCount = "3";//update count to current for hitting these screens another time
                }
                else if (myHouseholdMembers.myPassCount == "3")
                {
                    myHouseholdMembers.myPassCount = "2";
                }
                else
                {
                    myHouseholdMembers.myPassCount = "1";
                }
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);

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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

        public int DoUpdateReEnroll(mystructHistoryInfo myHistoryInfo, string updateValue)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;


            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand(
                    "SELECT * FROM HouseMembers where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update HouseMembers set Reenroll = @Reenroll where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com2 = new SqlCeCommand(myUpdateString, con))
                        {
                            com2.Parameters.AddWithValue("Reenroll", updateValue);
                            com2.ExecuteNonQuery();
                            com2.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update reenroll didn't work");
            }
            return 1;
        }

        public int DoUpdateSaveExit(mystructHistoryInfo myHistoryInfo, string updateValue)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;


            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand(
                    "SELECT * FROM HouseMembers where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update HouseMembers set SaveExit = @SaveExit where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com2 = new SqlCeCommand(myUpdateString, con))
                        {
                            com2.Parameters.AddWithValue("SaveExit", updateValue);
                            com2.ExecuteNonQuery();
                            com2.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update SaveExit didn't work");
            }
            return 1;
        }

        public String DoWaitForFindProvider(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers, By selector)
        {
            int wait = 200000;
            int iterations = (wait / 1000);
            long startmilliSec = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            int appwait;
            if (myHistoryInfo.myBrowser == "Chrome")
            {
                appwait = (20 + myHistoryInfo.myAppWait) * 1000;
            }
            else
            {
                appwait = (45 + myHistoryInfo.myAppWait) * 1000;
            }
            System.Threading.Thread.Sleep(appwait);
            IWebDriver myDriver = driver;
            if (myHistoryInfo.myRelogin == "Yes")
            {
                myDriver = driver3;
            }

            for (int i = 0; i < iterations; i++)
            {
                if (((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startmilliSec) > wait)
                {
                    return "false";
                }
                myDriver.SwitchTo().DefaultContent();
                try
                {
                    if (myHistoryInfo.myEnvironment == "STST")
                    {
                        var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                        myDriver.SwitchTo().Frame(iFrameElement);
                    }
                    else if (myHistoryInfo.myEnvironment == "STST2")
                    {
                        var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                        myDriver.SwitchTo().Frame(iFrameElement);
                    }
                    else
                    {
                        var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                        myDriver.SwitchTo().Frame(iFrameElement);
                    }

                    var elems2 = myDriver.FindElements(selector);
                    IList<IWebElement> elements = elems2;
                    if (elements != null && elements.Count > 0)
                    {
                        System.Threading.Thread.Sleep(2000);
                        return "true";
                    }
                }
                catch (NoSuchElementException)
                {
                    //do nothing and continue
                }
                DoEnroll(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                DoSelectHH(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                if (householdCount == 1)
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (20 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (45 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);
                }

                if (householdCount > 1)
                {
                    DoSelectPrimary(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                    appwait = (45 + myHistoryInfo.myAppWait) * 1000;
                    System.Threading.Thread.Sleep(appwait);
                }

                FillStructures myFillStructures = new FillStructures();
                int result = myFillStructures.doFillAppCountStructures(ref myEnrollment, ref myHistoryInfo);
                result = myFillStructures.doFillHouseholdCountStructures(ref myHouseholdMembers, ref myHistoryInfo);
            }
            return "false";
        }

        public String DoWaitForEstimator(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers, By selector)
        {
            int wait = 70000;
            int iterations = (wait / 1000);
            long startmilliSec = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            int appwait;
            if (myHistoryInfo.myBrowser == "Chrome")
            {
                appwait = (10 + myHistoryInfo.myAppWait) * 1000;
            }
            else
            {
                appwait = (30 + myHistoryInfo.myAppWait) * 1000;
            }
            System.Threading.Thread.Sleep(appwait);
            IWebDriver myDriver = driver;

            for (int i = 0; i < iterations; i++)
            {
                if (((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startmilliSec) > wait)
                {
                    return "false";
                }

                var elems2 = myDriver.FindElements(selector);
                IList<IWebElement> elements = elems2;
                if (elements != null && elements.Count > 0)
                {
                    var iFrameElement = myDriver.FindElement(selector);
                    myDriver.SwitchTo().Frame(iFrameElement);

                    System.Threading.Thread.Sleep(2000);
                    var elems3 = myDriver.FindElements(By.XPath("//h3[contains(text(), 'Estimator')]"));
                    IList<IWebElement> elements3 = elems3;
                    if (elements3 != null && elements3.Count > 0)
                    {
                        return "true";
                    }
                }
                DoEnroll(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                DoSelectHH(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                if (householdCount == 1)
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        if (myHistoryInfo.myBrowser == "Chrome")
                        {
                            appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (30 + myHistoryInfo.myAppWait) * 1000;
                        }
                        System.Threading.Thread.Sleep(appwait);
                    }
                }
                if (householdCount > 1)
                {
                    DoSelectPrimary(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (30 + myHistoryInfo.myAppWait) * 1000;
                    }
                    System.Threading.Thread.Sleep(appwait);
                }

                FillStructures myFillStructures = new FillStructures();
                int result = myFillStructures.doFillAppCountStructures(ref myEnrollment, ref myHistoryInfo);
                result = myFillStructures.doFillHouseholdCountStructures(ref myHouseholdMembers, ref myHistoryInfo);
            }
            return "false";
        }

        public String DoWaitForPlanSummary(IWebDriver driver, IWebDriver driver3, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers, By selector)
        {
            int wait = 70000;
            int iterations = (wait / 1000);
            long startmilliSec = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            int appwait;
            if (myHistoryInfo.myBrowser == "Chrome")
            {
                appwait = (15 + myHistoryInfo.myAppWait) * 1000;
            }
            else
            {
                appwait = (30 + myHistoryInfo.myAppWait) * 1000;
            }
            System.Threading.Thread.Sleep(appwait);
            IWebDriver myDriver = driver;
            if (myHistoryInfo.myRelogin == "Yes")
            {
                myDriver = driver3;
            }
            for (int i = 0; i < iterations; i++)
            {
                if (((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startmilliSec) > wait)
                {
                    return "false";
                }
                myDriver.SwitchTo().DefaultContent();
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.stst2.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                else
                {
                    var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'https://plans.atst.mnsure.org/mnsa/stateadvantage/Enroll.action')]"));
                    myDriver.SwitchTo().Frame(iFrameElement);
                }
                var elems2 = myDriver.FindElements(selector);
                IList<IWebElement> elements = elems2;
                if (elements != null && elements.Count > 0)
                {
                    return "true";
                }
                DoEnroll(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                DoSelectHH(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                if (householdCount > 1)
                {
                    DoSelectPrimary(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                }
                DoFindProvider(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                DoPlanType(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                DoPrivacy(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                DoMedicalPlanDetails(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                DoDentalPlanDetails(driver, driver3, myEnrollment, myHistoryInfo, ref returnStatus, ref returnException, ref returnScreenshot,
                    myHouseholdMembers);
                if (myHistoryInfo.myBrowser == "Chrome")
                {
                    appwait = (15 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (30 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
            }
            return "false";
        }


    }
}
