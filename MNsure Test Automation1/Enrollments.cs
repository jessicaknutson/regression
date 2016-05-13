﻿using System;
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
            ref string returnStatus, ref string returnException, ref string returnScreenshot, mystructHouseholdMembers myHouseholdMembers)
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
                    else
                    {
                        myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/span")).Click(); 
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
                if (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No")
                {
                    System.Threading.Thread.Sleep(6000);
                }
                else
                {
                    System.Threading.Thread.Sleep(4000);
                }
                //check for text at the bottom
                if (myEnrollment.myHouseholdOther == "No" || (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_2"))));
                }
                else if (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "2")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_4"))));
                }
                else if ((myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "3")
                    || (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2")) 
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_6"))));
                }
                else if (myHouseholdMembers.myReEnroll == "Yes" && myEnrollment.myHouseholdOther == "Yes")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_14"))));
                }
                else
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_10"))));
                }
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement buttonContinue;
                if (myEnrollment.myHouseholdOther == "No" || (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_2"));
                }
                else if (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "2")
                {
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_4"));
                }
                else if ((myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "3")
                    || (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2")) 
                {
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_6"));
                }
                else if (myHouseholdMembers.myReEnroll == "Yes" && myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1") 
                {
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_14"));
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input")).Click();
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[2]/div/input")).Click();
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[3]/div/input")).Click();
                }                
                else
                {
                    buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_10"));
                }
                ApplicationDo myApp = new ApplicationDo();
                if (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "1")
                {
                    myHouseholdMembers.myPassCount = "2";//update count to 2 to do the screen another time
                    myApp.DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                }
                else if (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "2")
                {
                    myHouseholdMembers.myPassCount = "3";//update count to 3 to do the screen another time
                    myApp.DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);                    
                }
                else if (myHouseholdMembers.myReEnroll == "No" && myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No" && myHouseholdMembers.myPassCount == "3")
                {
                    myHouseholdMembers.myPassCount = "1";//update count to 1 to continue to next screens
                    myApp.DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                    buttonContinue.Click();//this does not always work so much select these 2 screens 3 times, this is a current known production bug
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
                System.Threading.Thread.Sleep(2000);
                //check for text at the bottom
                if (myHouseholdMembers.myReEnroll == "Yes" && myEnrollment.myHouseholdOther == "Yes")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_16"))));
                    IWebElement checkboxPrimary = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input"));
                    checkboxPrimary.Click(); 
                }                
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_4"))));
                    if (myHouseholdMembers.myReEnroll == "No" && myHouseholdMembers.mySaveExit == "Yes")
                    {
                        IWebElement checkboxPrimary = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[3]/div/div[3]/div/div[1]/div/input"));
                        checkboxPrimary.Click(); 
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_8"))));
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
                    myApp.DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                    if (myHouseholdMembers.myReEnroll == "No" && myHouseholdMembers.mySaveExit == "Yes")
                    {
                        IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_4"));
                        buttonContinue.Click();
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2" && myHouseholdMembers.myReEnroll == "No")
                {
                    myHouseholdMembers.myPassCount = "3";//update count to 2 to do the screens another time
                    myApp.DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                }
                else
                {
                    if (myHouseholdMembers.myReEnroll == "Yes")
                    {
                        IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_16"));
                        buttonContinue.Click();
                    }                   
                    else
                    {
                        IWebElement buttonContinue = myDriver.FindElement(By.Id("dijit_form_Button_12"));
                        buttonContinue.Click(); //this does not always work so much select these 3 screens 3 times, this is a current known production bug
                        myHouseholdMembers.myPassCount = "1";//update count to 1 to move forward
                        myApp.DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
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
                appwait = (45 + myHistoryInfo.myAppWait) * 1000;//norm 40                
                System.Threading.Thread.Sleep(appwait);
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
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);                

                /*if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myReEnroll == "Yes")
                {                                                                  
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[2]/div[3]/input")).Click();
                }
                else
                {
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[1]/div[3]/input")).Click();
                }*/

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
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;//norm 8
                }
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[2]")));

                myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[2]")).Click();
                System.Threading.Thread.Sleep(1000);
                /*if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myReEnroll == "Yes")
                {
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/div[4]/div/div/div[2]/div[3]/input")).Click();
                }
                else
                {
                    myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[2]")).Click();
                }*/
                
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
                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"))));
                IWebElement buttonEnroll3 = myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"));
                buttonEnroll3.Click();

                System.Threading.Thread.Sleep(30000);
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
                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"))));
                IWebElement buttonEnroll = myDriver.FindElement(By.XPath("/html/body/div[3]/div[3]/div[2]/div[3]/div/div[1]/div/div/div[2]/span/a[1]"));
                buttonEnroll.Click();

                System.Threading.Thread.Sleep(30000);
                myDriver.SwitchTo().DefaultContent();
                WebDriverWait wait = new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iFrame")));

                var iFrameElement = myDriver.FindElement(By.TagName("iFrame"));
                myDriver.SwitchTo().Frame(iFrameElement);
                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[1]/div[3]/div[3]/span[5]/a"))));
                IWebElement buttonExit = myDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div[3]/span[5]/a"));
                buttonExit.Click();

                System.Threading.Thread.Sleep(1000);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[8]/div/div[2]/input[2]"))));
                IWebElement buttonExit2 = myDriver.FindElement(By.XPath("/html/body/div[8]/div/div[2]/input[2]"));
                buttonExit2.Click();

                myHouseholdMembers.mySaveExit = "Yes"; //update saveexit to select primary
                DoUpdateSaveExit(myHistoryInfo, myHouseholdMembers.mySaveExit);

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
                System.Threading.Thread.Sleep(12000);
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
                System.Threading.Thread.Sleep(2000);
                //check for text at the bottom
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
                System.Threading.Thread.Sleep(2000);
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
                System.Threading.Thread.Sleep(4000);
                myDriver.SwitchTo().DefaultContent();

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/table/tbody/tr/td[2]/div[1]")));
                IWebElement buttonViewDropdown = myDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[2]/div[2]/div/table/tbody/tr/td[2]/div[1]"));
                buttonViewDropdown.Click();

                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();                
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                System.Threading.Thread.Sleep(2000);
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                myHouseholdMembers.myReEnroll = "Yes"; //update reenroll to do the screens another time
                DoUpdateReEnroll(myHistoryInfo, myHouseholdMembers.myReEnroll);

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


    }
}
