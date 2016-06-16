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
    class WizardApplicationDo
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoEffectiveDate(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxEffectiveDate = driver.FindElement(By.Id("__o3id6"));
                textboxEffectiveDate.Clear();
                textboxEffectiveDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/div/div/a/span/span/span")).Click();//next    

                myHouseholdMembers.myPassCount = "1";//reset count back to 1 on start in case an error happened during previous run
                myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                myEnrollment.myPassCount = "1";//reset count back to 1 on start in case an error happened during previous run
                myApp.DoUpdateAppPassCount(myHistoryInfo, myEnrollment.myPassCount);

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

        public int DoRelationships(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {                
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxEffectiveDate = driver.FindElement(By.Id("__o3id6"));
                textboxEffectiveDate.SendKeys(myHouseholdMembers.myRelationship);
                System.Threading.Thread.Sleep(1000);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span")).Click();//next                

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

        public int DoApplicantDetails(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxMaritalStatus = driver.FindElement(By.Id("__o3idb"));
                textboxMaritalStatus.SendKeys(myHouseholdMembers.myMaritalStatus);

                IWebElement listboxLiveInHH = driver.FindElement(By.Id("__o3idd"));
                listboxLiveInHH.SendKeys(myHouseholdMembers.myLiveWithYou);
                System.Threading.Thread.Sleep(1000);

                IWebElement listboxAddress = driver.FindElement(By.Id("__o3ide"));
                listboxAddress.Click();

                IWebElement listboxPlanLiveMN = driver.FindElement(By.Id("__o3id28"));
                listboxPlanLiveMN.SendKeys(myHouseholdMembers.myLiveInMN);

                IWebElement listboxApply = driver.FindElement(By.Id("__o3id2a"));
                listboxApply.SendKeys(myHouseholdMembers.myPersonHighlighted);

                if (myHouseholdMembers.myPersonHighlighted == "Yes")
                {
                    IWebElement listboxMedicaid = driver.FindElement(By.Id("__o3id2b"));
                    listboxMedicaid.SendKeys("No");
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a/span/span/span"));
                buttonNext.Click();

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

        public int DoAdditionalInfo(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                if (myHouseholdMembers.myPersonHighlighted == "Yes")
                {
                    IWebElement listboxSSN = driver.FindElement(By.Id("__o3id6"));
                    listboxSSN.SendKeys(myHouseholdMembers.myHaveSSN);
                }
                
                IWebElement listboxSSNNum = driver.FindElement(By.Id("__o3id7"));
                listboxSSNNum.SendKeys(myHouseholdMembers.mySSN);

                if (myHouseholdMembers.myPersonHighlighted == "Yes")
                {
                    IWebElement listboxCitizen = driver.FindElement(By.Id("__o3idb"));
                    listboxCitizen.SendKeys(myHouseholdMembers.myUSCitizen);
                }

                string isPregnant = "No";
                string isFemale = "No";
                if (myHouseholdMembers.myGender == "Female")
                {
                    isFemale = "Yes";
                    if (myHouseholdMembers.myIsPregnant == "Yes")
                    {
                        isPregnant = "Yes";
                    }
                }

                if (isFemale == "Yes")
                {
                    IWebElement listboxPregnant;
                    if (myHouseholdMembers.myPersonHighlighted == "Yes")
                    {
                        listboxPregnant = driver.FindElement(By.Id("__o3id16"));
                    }
                    else
                    {
                        listboxPregnant = driver.FindElement(By.Id("__o3id15"));
                    }
                    listboxPregnant.SendKeys(myHouseholdMembers.myIsPregnant);
                }

                if (isPregnant == "Yes")
                {
                    string children;
                    string dueDate;
                    string pregnancyEnded;
                    children = myHouseholdMembers.myChildren;
                    dueDate = myHouseholdMembers.myDueDate;
                    pregnancyEnded = myHouseholdMembers.myPregnancyEnded;
                    driver.FindElement(By.Id("__o3id17")).SendKeys(children);
                    driver.FindElement(By.Id("__o3id18")).SendKeys(dueDate);
                    driver.FindElement(By.Id("__o3id19")).SendKeys(pregnancyEnded);
                }
                              
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoState(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxNDAgreement = driver.FindElement(By.Id("__o3id6"));
                listboxNDAgreement.SendKeys("Not Applicable");

                IWebElement listboxNDNursing = driver.FindElement(By.Id("__o3id7"));
                listboxNDNursing.SendKeys("No");

                IWebElement listboxNDMedical = driver.FindElement(By.Id("__o3id8"));
                listboxNDMedical.SendKeys("No");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoRace(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxIndian = driver.FindElement(By.Id("__o3id6"));
                if (myHouseholdMembers.myRace == "Indian")
                {
                    listboxIndian.SendKeys("Yes");
                }
                else
                {
                    listboxIndian.SendKeys("No");
                }

                IWebElement listboxHispanic = driver.FindElement(By.Id("__o3idc"));
                listboxHispanic.SendKeys(myHouseholdMembers.myHispanic);

                if (myHouseholdMembers.myRace == "Indian")
                {
                    IWebElement checkboxRace = driver.FindElement(By.Id("__o3id12"));
                    checkboxRace.Click();
                }
                else
                {
                    IWebElement checkboxRace = driver.FindElement(By.Id("__o3id1d"));
                    checkboxRace.Click();
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoTaxFiler(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                if (myHouseholdMembers.myTaxFiler == "Yes")
                {
                    IWebElement checkboxPerson = driver.FindElement(By.Id("__o3id6"));
                    checkboxPerson.Click();
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoTaxStatus(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxStatus = driver.FindElement(By.Id("__o3id6"));
                listboxStatus.SendKeys(myHouseholdMembers.myFileJointly);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoAnyIncome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxAnyIncome;

                if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    listboxAnyIncome = driver.FindElement(By.Id("__o3id6"));
                    listboxAnyIncome.SendKeys(myHouseholdMembers.myHasIncome);
                }
                else
                {
                    listboxAnyIncome = driver.FindElement(By.Id("__o3id6"));

                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    string hasIncome = "No";
                    if (householdCount == 3)
                    {
                        FillStructures myFillStructures = new FillStructures();
                        int result;
                        if (myHouseholdMembers.myPassCount == "2")
                        {
                            result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "2");
                        }
                        else
                        {
                            result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        }
                    }
                    if (myHouseholdMembers.myHasIncome == "Yes")
                    {
                        hasIncome = "Yes";
                    }

                    if (hasIncome == "No")
                    {
                        listboxAnyIncome.SendKeys("No");
                    }
                    else
                    {
                        listboxAnyIncome.SendKeys("Yes");
                    }
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoAdditionalIncomeDetails(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();               
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoAnnualIncome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                String incomeExpected = "";
                if (myEnrollment.myHouseholdOther == "No")
                {
                    incomeExpected = myEnrollment.myIncomeExpected;
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    incomeExpected = myHouseholdMembers.myIncomeExpected;
                    myHouseholdMembers.myPassCount = "2";//update count to 2 to do the income screens another time
                    myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                }
                else //pass count = 2
                {
                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    if (householdCount == 3)
                    {
                        FillStructures myFillStructures = new FillStructures();
                        if (myHouseholdMembers.myPassCount == "2")
                        {
                            int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "2");
                            myHouseholdMembers.myPassCount = "3";//update count to 3 to do the income screens another time
                            myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                        }
                        else
                        {
                            int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        }
                    }
                    incomeExpected = myHouseholdMembers.myIncomeExpected;
                }

                if (incomeExpected != "Yes")
                {
                    IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeAdjusted.SendKeys(incomeExpected);
                }

                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myTimeTravelDate > Convert.ToDateTime("10/31/2016") &&
                        myHistoryInfo.myTimeTravelDate < Convert.ToDateTime("1/1/2017"))
                    {
                        IWebElement listboxIncomeNextYear = driver.FindElement(By.Id("__o3id8"));
                        listboxIncomeNextYear.SendKeys(incomeExpected);
                    }
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoConditionalEvidences(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoSummary(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a[2]/span/span/span"));
                buttonNext.Click();

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

        public int DoConfirmation(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/MNHIXIntegratedCase_resolveLaunchLifeEventScriptPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/a/span/span/span"));
                buttonNext.Click();

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

        public int DoHouseholdRegistration(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.Id("__o3id1"));

                IWebElement listboxSSNNum = driver.FindElement(By.Id("__o3id1"));
                listboxSSNNum.SendKeys(myHouseholdMembers.mySSN);

                IWebElement textboxFirstName = driver.FindElement(By.Id("__o3id2"));
                textboxFirstName.SendKeys(myHouseholdMembers.myFirstName);

                IWebElement textboxMiddleName = driver.FindElement(By.Id("__o3id5"));
                if (myHouseholdMembers.myMiddleName != null)
                {
                    textboxMiddleName.SendKeys(myHouseholdMembers.myMiddleName);
                }
                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id4"));
                textboxLastName.SendKeys(myHouseholdMembers.myLastName);

                IWebElement textboxSuffix = driver.FindElement(By.Id("__o3id7"));
                if (myHouseholdMembers.mySuffix != null && myHouseholdMembers.mySuffix != "")
                {
                    textboxSuffix.SendKeys(myHouseholdMembers.mySuffix);
                }
                if (myHouseholdMembers.myGender == "Female")
                {
                    IWebElement textboxGender = driver.FindElement(By.Id("__o3ida"));
                    textboxGender.Clear();
                    textboxGender.SendKeys(myHouseholdMembers.myGender);
                }
                string tempDOB;
                int tempDOBLength;
                tempDOB = Convert.ToString(myHouseholdMembers.myDOB);
                tempDOBLength = tempDOB.Length;
                tempDOB = tempDOB.Substring(0, tempDOBLength);
                IWebElement textboxDOB = driver.FindElement(By.Id("__o3idb"));
                textboxDOB.SendKeys(tempDOB);
                
                IWebElement listboxLanguage = driver.FindElement(By.Id("__o3ide"));
                listboxLanguage.SendKeys("English");

                IWebElement listboxPreferredComm = driver.FindElement(By.Id("__o3idf"));
                listboxPreferredComm.SendKeys(myHouseholdMembers.myPrefContact);

                if (myEnrollment.myHomeAptSuite != null)
                {
                    IWebElement listboxAptSuite = driver.FindElement(By.Id("__o3id11"));
                    listboxAptSuite.SendKeys(myEnrollment.myHomeAptSuite);
                }
                IWebElement listboxAddress1 = driver.FindElement(By.Id("__o3id12"));
                listboxAddress1.SendKeys(myEnrollment.myHomeAddress1);

                if (myEnrollment.myHomeAddress2 != null)
                {
                    IWebElement listboxAddress2 = driver.FindElement(By.Id("__o3id13"));
                    listboxAddress2.SendKeys(myEnrollment.myHomeAddress2);
                }
                IWebElement listboxCity = driver.FindElement(By.Id("__o3id14"));
                listboxCity.SendKeys(myEnrollment.myHomeCity);
                IWebElement listboxCounty = driver.FindElement(By.Id("__o3id15"));
                listboxCounty.SendKeys(myEnrollment.myHomeCounty);
                IWebElement listboxState = driver.FindElement(By.Id("__o3id16"));
                listboxState.SendKeys(myEnrollment.myHomeState);
                IWebElement listboxZip = driver.FindElement(By.Id("__o3id17"));
                listboxZip.SendKeys(myEnrollment.myHomeZip);                

                IWebElement listboxPhoneType = driver.FindElement(By.Id("__o3id20"));
                listboxPhoneType.SendKeys(myHouseholdMembers.myPhoneType);

                string mysPhone1 = myHouseholdMembers.myPhoneNum.Substring(0, 3);
                string mysPhone2 = myHouseholdMembers.myPhoneNum.Substring(3, 3);
                string mysPhone3 = myHouseholdMembers.myPhoneNum.Substring(6, 4);
                IWebElement textboxPhoneNum = driver.FindElement(By.Id("__o3id21"));
                textboxPhoneNum.SendKeys(mysPhone1);
                IWebElement textboxPhoneNum2 = driver.FindElement(By.Id("__o3id22"));
                textboxPhoneNum2.SendKeys(mysPhone2);
                IWebElement textboxPhoneNum3 = driver.FindElement(By.Id("__o3id23"));
                textboxPhoneNum3.SendKeys(mysPhone3);

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

    }
}