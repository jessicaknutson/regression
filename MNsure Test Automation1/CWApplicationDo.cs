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
    class CWApplicationDo
    {
        WriteLogs writeLogs = new WriteLogs();



        public int DoPersonCheck(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/RegisterPerson_resolveStartWizardPage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/RegisterPerson_resolveStartWizardPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/div[4]/div[2]/a/span/span/span")).Click();//next

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

        public int DoRegistration(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.Id("__o3id1"), myHistoryInfo);

                IWebElement listboxSSNNum = driver.FindElement(By.Id("__o3id1"));
                listboxSSNNum.SendKeys(myEnrollment.mySSNNum);

                IWebElement textboxFirstName = driver.FindElement(By.Id("__o3id2"));
                textboxFirstName.SendKeys(myEnrollment.myFirstName);

                IWebElement textboxMiddleName = driver.FindElement(By.Id("__o3id5"));
                if (myEnrollment.myMiddleName != null)
                {
                    textboxMiddleName.SendKeys(myEnrollment.myMiddleName);
                }
                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id4"));
                textboxLastName.SendKeys(myEnrollment.myLastName);

                IWebElement textboxSuffix = driver.FindElement(By.Id("__o3id7"));
                if (myEnrollment.mySuffix != null && myEnrollment.mySuffix != "")
                {
                    textboxSuffix.SendKeys(myEnrollment.mySuffix);
                }
                if (myEnrollment.myGender == "Female")
                {
                    IWebElement textboxGender = driver.FindElement(By.Id("__o3ida"));
                    textboxGender.Clear();
                    textboxGender.SendKeys(myEnrollment.myGender);
                }
                string tempDOB;
                int tempDOBLength;
                tempDOB = Convert.ToString(myEnrollment.myDOB);
                tempDOBLength = tempDOB.Length;
                tempDOB = tempDOB.Substring(0, tempDOBLength);
                IWebElement textboxDOB = driver.FindElement(By.Id("__o3idb"));
                textboxDOB.SendKeys(tempDOB);
                if (myEnrollment.myRegDate != null && myEnrollment.myRegDate != "")
                {
                    IWebElement textboxRegDate = driver.FindElement(By.Id("__o3idd"));
                    textboxRegDate.Clear();
                    textboxRegDate.SendKeys(myEnrollment.myRegDate);
                }
                IWebElement listboxLanguage = driver.FindElement(By.Id("__o3ide"));
                listboxLanguage.SendKeys(myEnrollment.myLanguageMost);

                IWebElement listboxPreferredComm = driver.FindElement(By.Id("__o3idf"));
                listboxPreferredComm.SendKeys(myEnrollment.myPrefContact);

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
                listboxPhoneType.SendKeys(myEnrollment.myPhoneType);

                string mysPhone1 = myEnrollment.myPhoneNum.Substring(0, 3);
                string mysPhone2 = myEnrollment.myPhoneNum.Substring(3, 3);
                string mysPhone3 = myEnrollment.myPhoneNum.Substring(6, 4);
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

        public int DoActionNewAppForm(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);*/
                System.Threading.Thread.Sleep(2000);
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[2]/div/div[2]/div/div[1]/div/span[1]/span/span/span[2]")).Click();//actions

                driver.FindElement(By.XPath("//td[contains(text(), 'New Application Form')]")).Click();

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

        public int DoAppFormType(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxFirst = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/tbody/tr[1]/td[2]"));
                IWebElement listboxSecond = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/tbody/tr[2]/td[2]"));
                IWebElement listboxThird = driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/tbody/tr[3]/td[2]"));
                if (listboxFirst.Text == "Health Care Application")
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/tbody/tr[1]/td[1]/span/input")).Click();
                }
                else if (listboxSecond.Text == "Health Care Application")
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/tbody/tr[2]/td[1]/span/input")).Click();
                }
                else
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[2]/div/table/tbody/tr[3]/td[1]/span/input")).Click();
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/div[3]/div[2]/a/span/span/span")).Click();//next                

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

        public int DoAppFilingDate(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);*/
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxRegDate = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div/div/div/div[3]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/table/tbody/tr/td[1]/input"));
                textboxRegDate.Clear();
                if (myEnrollment.myRegDate != null && myEnrollment.myRegDate != "")
                {
                    textboxRegDate.SendKeys(myEnrollment.myRegDate);
                }
                else
                {
                    textboxRegDate.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));
                }

                driver.FindElement(By.XPath("/html/body/div/div/a/span/span/span")).Click();//next

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

        public int DoGettingStarted(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (10 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);*/
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement checkboxAgree = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div/div/div/div[3]/table/tbody/tr/td/div[2]/input"));
                checkboxAgree.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[1]/div/a/span/span/span"));
                buttonNext.Click();

                myHouseholdMembers.myPassCount = "1";//reset count back to 1 on start in case an error happened during previous run
                myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                myEnrollment.myPassCount = "1";//reset count back to 1 on start in case an error happened during previous run
                myApp.DoUpdateAppPassCount(myHistoryInfo, myEnrollment.myPassCount);

                Enrollments myEnrollment2 = new Enrollments();
                myHouseholdMembers.myReEnroll = "No"; //reset reenroll on start in case an error happened during previous run
                myEnrollment2.DoUpdateReEnroll(myHistoryInfo, myHouseholdMembers.myReEnroll);
                myHouseholdMembers.mySaveExit = "No"; //reset saveexit on start in case an error happened during previous run
                myEnrollment2.DoUpdateSaveExit(myHistoryInfo, myHouseholdMembers.mySaveExit);

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

        public int DoApplicantDetailsAbout(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);*/
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext2 = driver.FindElement(By.XPath("/html/body/div[1]/div/a/span/span/span"));
                buttonNext2.Click();

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
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxMaritalStatus = driver.FindElement(By.Id("__o3idb"));
                textboxMaritalStatus.SendKeys(myEnrollment.myMaritalStatus);

                IWebElement listboxLiveMN = driver.FindElement(By.Id("__o3idd"));
                listboxLiveMN.SendKeys(myEnrollment.myLiveMN);

                if (myEnrollment.myLiveMN == "Yes")
                {
                    myApp.DoWaitForElement(driver, By.Id("__o3ide"), myHistoryInfo);
                    IWebElement listboxHomeless = driver.FindElement(By.Id("__o3ide"));
                    listboxHomeless.SendKeys(myEnrollment.myHomeless);
                }
                else
                {
                    myApp.DoWaitForElement(driver, By.Id("__o3idf"), myHistoryInfo);
                    IWebElement listboxTempAbsent = driver.FindElement(By.Id("__o3idf"));
                    listboxTempAbsent.SendKeys("No");
                }

                IWebElement listboxAddressSame = driver.FindElement(By.Id("__o3id18"));
                listboxAddressSame.SendKeys(myEnrollment.myAddressSame);

                IWebElement listboxPlanLiveMN = driver.FindElement(By.Id("__o3id21"));
                listboxPlanLiveMN.SendKeys(myEnrollment.myPlanLiveMN);

                string mysAPhone1 = myEnrollment.myAltNum.Substring(0, 3);
                string mysAPhone2 = myEnrollment.myAltNum.Substring(3, 3);
                string mysAPhone3 = myEnrollment.myAltNum.Substring(6, 4);
                IWebElement textboxAPhoneNum = driver.FindElement(By.Id("__o3id28"));
                textboxAPhoneNum.SendKeys(mysAPhone1);
                IWebElement textboxAPhoneNum2 = driver.FindElement(By.Id("__o3id29"));
                textboxAPhoneNum2.SendKeys(mysAPhone2);
                IWebElement textboxAPhoneNum3 = driver.FindElement(By.Id("__o3id2a"));
                textboxAPhoneNum3.SendKeys(mysAPhone3);

                IWebElement listboxAPhoneType = driver.FindElement(By.Id("__o3id2b"));
                listboxAPhoneType.SendKeys(myEnrollment.myAltNumType);

                IWebElement textboxEmail = driver.FindElement(By.Id("__o3id2c"));
                textboxEmail.SendKeys(myEnrollment.myEmail);

                IWebElement listboxVoterCard = driver.FindElement(By.Id("__o3id32"));
                listboxVoterCard.SendKeys(myEnrollment.myVoterCard);

                IWebElement listboxNotices = driver.FindElement(By.Id("__o3id33"));
                listboxNotices.SendKeys(myEnrollment.myNotices);

                IWebElement listboxAuthRep = driver.FindElement(By.Id("__o3id34"));
                listboxAuthRep.SendKeys(myEnrollment.myAuthRep);

                if (myEnrollment.myApplyYourself == "No")
                {
                    IWebElement listboxApplyYouself = driver.FindElement(By.Id("__o3id35"));
                    listboxApplyYouself.SendKeys(myEnrollment.myApplyYourself);
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

        public int DoMoreAboutYou(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);*/
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxHispanic = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/select"));
                listboxHispanic.SendKeys(myEnrollment.myHispanic);

                if (myEnrollment.myRace == "Indian")
                {
                    IWebElement checkboxRace = driver.FindElement(By.Id("__o3idc"));
                    checkboxRace.Click();
                }
                else
                {
                    IWebElement checkboxRace = driver.FindElement(By.Id("__o3id17"));
                    checkboxRace.Click();
                }

                IWebElement listboxSSN = driver.FindElement(By.Id("__o3id1c"));
                listboxSSN.SendKeys(myEnrollment.mySSN);

                if (myEnrollment.mySSN != "Yes")
                {
                    IWebElement listboxAppliedSSN = driver.FindElement(By.Id("__o3id1e"));
                    listboxAppliedSSN.SendKeys(myEnrollment.myAppliedSSN);
                    if (myEnrollment.myAppliedSSN == "No")
                    {
                        IWebElement listboxWhyNoSSN = driver.FindElement(By.Id("__o3id1f"));
                        listboxWhyNoSSN.SendKeys(myEnrollment.myWhyNoSSN);
                    }

                    if (myEnrollment.myWhyNoSSN == "Other")
                    {
                        IWebElement listboxAssistSSN = driver.FindElement(By.Id("__o3id20"));
                        listboxAssistSSN.SendKeys(myEnrollment.myAssistSSN);
                    }
                }

                IWebElement listboxCitizen = driver.FindElement(By.Id("__o3id21"));
                listboxCitizen.SendKeys(myEnrollment.myCitizen);

                string isPregnant = "No";
                string isFemale = "No";
                string householdMember = "1";
                if (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No")
                {
                    /*if (myHouseholdMembers.myGender == "Female")
                    {
                        isFemale = "Yes";
                        if (myHouseholdMembers.myIsPregnant == "Yes")
                        {
                            isPregnant = "Yes";
                            householdMember = "2";
                        }
                    }*/
                }
                else
                {
                    if (myEnrollment.myGender == "Female")
                    {
                        isFemale = "Yes";
                        if (myEnrollment.myIsPregnant == "Yes")
                        {
                            isPregnant = "Yes";
                        }
                    }
                }

                if (isFemale == "Yes")
                {
                    IWebElement listboxPregnant = driver.FindElement(By.Id("__o3id2c"));
                    listboxPregnant.SendKeys(myEnrollment.myIsPregnant);
                }

                if (isPregnant == "Yes")
                {
                    string children;
                    string dueDate;
                    string pregnancyEnded;
                    children = myEnrollment.myChildren;
                    dueDate = myEnrollment.myDueDate;
                    pregnancyEnded = myEnrollment.myPregnancyEnded;
                    driver.FindElement(By.Id("__o3id20")).SendKeys(children);
                    driver.FindElement(By.Id("__o3id20")).SendKeys(dueDate);
                    driver.FindElement(By.Id("__o3id20")).SendKeys(pregnancyEnded);
                }

                //This will only appear if age 18-27
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
                DateTime age = DateTime.MinValue + span;

                DateTime age2 = DateTime.MinValue;

                string fosterCare = "No";
                if (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No")
                {
                    if (age2.Year - 1 > 17 && age2.Year - 1 < 26) //2 hh
                    {
                        fosterCare = "Yes";
                    }
                }
                else
                {
                    if (age.Year - 1 > 17 && age.Year - 1 < 26) //1 hh
                    {
                        fosterCare = "Yes";
                    }
                }
                if (fosterCare == "Yes")
                {
                    IWebElement listboxFosterCare;
                    if (myEnrollment.myGender == "Female")
                    {
                        listboxFosterCare = driver.FindElement(By.Id("__o3id30"));
                    }
                    else
                    {
                        listboxFosterCare = driver.FindElement(By.Id("__o3id2f"));
                    }
                    listboxFosterCare.SendKeys(myEnrollment.myFosterCare);
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

        public int DoHouseholdAbout(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (94 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);*/
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);                

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[2]/a/span/span/span"), myHistoryInfo);

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

        public int DoHouseholdMembers(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxAnyoneElse = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div/div/div/div[4]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/select"));
                listboxAnyoneElse.SendKeys(myEnrollment.myHouseholdOther);

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
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                if (myEnrollment.myHouseholdOther == "No")
                {
                    IWebElement checkboxPerson = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td/div[2]/input"));
                    checkboxPerson.Click();
                }
                else
                {
                    IWebElement checkboxPerson = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td/div[2]/input"));
                    checkboxPerson.Click();

                    if (myHouseholdMembers.myTaxFiler == "Yes")
                    {
                        IWebElement checkboxPerson2 = driver.FindElement(By.Id("__o3id7"));
                        checkboxPerson2.Click();
                    }
                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    if (householdCount == 3)
                    {
                        FillStructures myFillStructures = new FillStructures();
                        int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    }
                    if (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes")
                    {
                        IWebElement checkboxPerson3 = driver.FindElement(By.Id("__o3id8"));
                        checkboxPerson3.Click();
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

        public int DoTaxDependents(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                IWebElement listboxDependent;
                if (myEnrollment.myHouseholdOther == "No")
                {
                    listboxDependent = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div[2]/div/div/div[5]/table/tbody/tr/td[2]/select"));
                    listboxDependent.SendKeys(myEnrollment.myDependants);
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    if (householdCount == 3)
                    {
                        FillStructures myFillStructures = new FillStructures();
                        int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        if (myHouseholdMembers.myHasIncome == "Yes" && myHouseholdMembers.myTaxFiler == "Yes")
                        {
                            listboxDependent = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input"));
                            listboxDependent.SendKeys(myHouseholdMembers.myDependants);
                        }
                        else
                        {
                            listboxDependent = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div[2]/div/div/div[7]/table/tbody/tr[1]/td[2]/select"));
                            listboxDependent.SendKeys(myHouseholdMembers.myDependants);
                        }
                        if (myHouseholdMembers.myDependants == "Yes")
                        {
                            System.Threading.Thread.Sleep(1000);
                            IWebElement checkboxPerson = driver.FindElement(By.Id("__o3id7"));
                            checkboxPerson.Click();
                        }
                        if (myHouseholdMembers.myDependants == "No" && householdCount != 1)
                        {
                            driver.FindElement(By.Id("__o3id8")).SendKeys("No");
                        }
                    }
                    else
                    {
                        listboxDependent = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input"));
                        listboxDependent.SendKeys(myHouseholdMembers.myDependants);

                        if (myHouseholdMembers.myDependants == "No" && householdCount != 1)
                        {
                            driver.FindElement(By.Id("__o3id8")).SendKeys("No");
                        }

                        myHouseholdMembers.myPassCount = "2";//update count to 2 to do the dependant screen another time
                        myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                    }
                }
                else  //pass count = 2
                {
                    listboxDependent = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input"));
                    listboxDependent.SendKeys(myHouseholdMembers.myDependants);

                    if (myHouseholdMembers.myDependants == "Yes")
                    {
                        System.Threading.Thread.Sleep(1000);
                        IWebElement checkboxPerson = driver.FindElement(By.Id("__o3id7"));
                        checkboxPerson.Click();
                    }
                    if (myHouseholdMembers.myDependants == "No" && householdCount != 1)
                    {
                        driver.FindElement(By.Id("__o3id8")).SendKeys("No");
                    }

                    myHouseholdMembers.myPassCount = "1";//update count back to 1 to continue on to next screens
                    myApp.DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
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

        public int DoHouseholdSummary(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);*/
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
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

        public int DoIncomeAbout(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
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

        public int DoAnyIncome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                /*int appwait;
                if (myEnrollment.myHouseholdOther == "Yes" && (myHouseholdMembers.myPassCount == "2" || myHouseholdMembers.myPassCount == "3"))
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (15 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                    }
                }

                System.Threading.Thread.Sleep(appwait);*/
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxAnyIncome;

                if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    listboxAnyIncome = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/select"));
                    listboxAnyIncome.SendKeys(myEnrollment.myIncomeYN);
                }
                else
                {
                    listboxAnyIncome = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div[2]/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/select"));

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

        public int DoEnterIncomeDetails(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();

                String type = "";
                String employer = "";
                String seasonal = "";
                String amount = "";
                String frequency = "";
                String more = "";
                if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    type = myEnrollment.myIncomeType;
                    employer = myEnrollment.myIncomeEmployer;
                    seasonal = myEnrollment.myIncomeSeasonal;
                    amount = myEnrollment.myIncomeAmount;
                    frequency = myEnrollment.myIncomeFrequency;
                    more = myEnrollment.myIncomeMore;
                }
                else
                {
                    FillStructures myFillStructures = new FillStructures();
                    if (myHouseholdMembers.myPassCount == "2")
                    {
                        int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    }
                    else
                    {
                        int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    }

                    type = myHouseholdMembers.myIncomeType;
                    employer = myHouseholdMembers.myIncomeEmployer;
                    seasonal = myHouseholdMembers.myIncomeSeasonal;
                    amount = myHouseholdMembers.myIncomeAmount;
                    frequency = myHouseholdMembers.myIncomeFrequency;
                    more = myHouseholdMembers.myIncomeMore;
                }

                /*int appwait;

                if (myEnrollment.myHouseholdOther == "No")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (25 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000; //1 hh
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (235 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (24 + myHistoryInfo.myAppWait) * 1000;//norm 8
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && (myHouseholdMembers.myHasIncome == "Yes" || myEnrollment.myIncomeYN == "Yes"))
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (155 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (25 + myHistoryInfo.myAppWait) * 1000;//was 16
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "3" && myHouseholdMembers.myHasIncome == "Yes")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (155 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (40 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (16 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (20 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);*/
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement textboxIncomeType = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[3]/div/div[2]/div/div/div/div[16]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/select"));
                textboxIncomeType.SendKeys(type);

                myApp.DoWaitForElement(driver, By.Id("__o3id7"), myHistoryInfo);
                IWebElement textboxIncomeEmployer = driver.FindElement(By.Id("__o3id7"));
                textboxIncomeEmployer.SendKeys(employer);

                IWebElement listboxIncomeSeasonal = driver.FindElement(By.Id("__o3id9"));
                listboxIncomeSeasonal.SendKeys(seasonal);

                IWebElement textboxIncomeAmount = driver.FindElement(By.Id("__o3ida"));
                textboxIncomeAmount.SendKeys(amount);

                IWebElement textboxIncomeFrequency = driver.FindElement(By.Id("__o3idc"));
                textboxIncomeFrequency.SendKeys(frequency);

                IWebElement textboxIncomeMore = driver.FindElement(By.Id("__o3idd"));
                textboxIncomeMore.SendKeys(more);

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
                /*int appwait;
                if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "2")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (175 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (12 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "3")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (45 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (12 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (7 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                    }
                }

                System.Threading.Thread.Sleep(appwait);*/
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
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

        public int DoIncomeAdjustments(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                String incomeAdjusted = "";
                if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    incomeAdjusted = myEnrollment.myIncomeAdjusted;
                }
                else
                {
                    FillStructures myFillStructures = new FillStructures();
                    if (myHouseholdMembers.myPassCount == "2")
                    {
                        int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    }
                    else
                    {
                        int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    }
                    incomeAdjusted = myHouseholdMembers.myIncomeAdjusted;
                }

                if (incomeAdjusted != "No")
                {
                    IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeAdjusted.SendKeys(incomeAdjusted);
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

        public int DoAnnualIncome(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
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

        public int DoAdditionalHouseholdInformation(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
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

        public int DoAdditionalInfoUnassistedInsurance(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                //This will only appear if age < 19
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
                DateTime age = DateTime.MinValue + span;

                DateTime age2 = DateTime.MinValue;
                HouseholdMembersDo myHousehold2 = new HouseholdMembersDo();
                int householdCount2 = myHousehold2.DoHouseholdCount(myHistoryInfo);
                if (householdCount2 == 2 || householdCount2 == 3)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    DateTime birth2 = Convert.ToDateTime(myHouseholdMembers.myDOB);
                    TimeSpan span2;
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        span2 = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth2;
                    }
                    else
                    {
                        span2 = DateTime.Now - birth2;
                    }
                    age2 = DateTime.MinValue + span2;
                }

                DateTime age3 = DateTime.MinValue;
                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                if (householdCount == 3)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    DateTime birth3 = Convert.ToDateTime(myHouseholdMembers.myDOB);
                    TimeSpan span3;
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        span3 = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth3;
                    }
                    else
                    {
                        span3 = DateTime.Now - birth3;
                    }
                    age3 = DateTime.MinValue + span3;
                }

                if (myEnrollment.myHouseholdOther == "No" && age.Year - 1 < 19 && householdCount == 1 //1 hh
                    || (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2 && (age.Year - 1 < 19 || age2.Year - 1 < 19)) // 2 hh
                    || (myEnrollment.myHouseholdOther == "Yes" && householdCount == 3 && (age.Year - 1 < 19 || age2.Year - 1 < 19 || age3.Year - 1 < 19))) // 3 hh
                {
                    IWebElement listboxOutsideHome;
                    string isMA = myApp.DoIsInTypeRange(myEnrollment, myHouseholdMembers, myHistoryInfo, "MA");
                    if (isMA == "True")
                    {
                        listboxOutsideHome = driver.FindElement(By.Id("__o3id12"));
                    }
                    else
                    {
                        listboxOutsideHome = driver.FindElement(By.Id("__o3ida"));
                    }
                    listboxOutsideHome.SendKeys("No");
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

        public int DoEmployerSponsoredCoverage(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                if (myEnrollment.myESC == "Yes")
                {
                    IWebElement listboxESC;
                    if (myEnrollment.myHouseholdOther == "No")
                    {
                        listboxESC = driver.FindElement(By.Id("__o3id6"));
                    }
                    else
                    {
                        listboxESC = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/fieldset/table/tbody/tr/td[1]/div[2]/div/div[1]/input"));
                    }
                    listboxESC.Click();
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

        public int DoEmployerSponsoredCoverageMore(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3id6")));

                IWebElement listboxESC = driver.FindElement(By.Id("__o3id6"));
                listboxESC.SendKeys("No");

                IWebElement listboxAccessESC = driver.FindElement(By.Id("__o3id7"));
                listboxAccessESC.SendKeys(myEnrollment.myESC);

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

        public int DoEmployerDetails(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3id6")));

                IWebElement textBoxName = driver.FindElement(By.Id("__o3id6"));
                textBoxName.SendKeys("Target");

                IWebElement textBoxId = driver.FindElement(By.Id("__o3id7"));
                textBoxId.SendKeys("12345");

                IWebElement textBoxFulltime = driver.FindElement(By.Id("__o3id8"));
                textBoxFulltime.SendKeys("Yes");

                IWebElement listboxEmployee = driver.FindElement(By.Id("__o3id9"));
                listboxEmployee.SendKeys("Yes");

                IWebElement listboxAddress1 = driver.FindElement(By.Id("__o3idb"));
                listboxAddress1.SendKeys("1 Main St");

                IWebElement listboxCity = driver.FindElement(By.Id("__o3ide"));
                listboxCity.SendKeys("Minneapolis");

                IWebElement listboxCounty = driver.FindElement(By.Id("__o3idf"));
                listboxCounty.SendKeys("Hennepin");

                IWebElement listboxState = driver.FindElement(By.Id("__o3id10"));
                listboxState.SendKeys("Minnesota");

                IWebElement listboxZip = driver.FindElement(By.Id("__o3id11"));
                listboxZip.SendKeys("55418");

                IWebElement textboxWaiting = driver.FindElement(By.Id("__o3id16"));
                textboxWaiting.SendKeys("No");

                IWebElement textboxNewYear = driver.FindElement(By.Id("__o3id18"));
                textboxNewYear.SendKeys("None");

                IWebElement textboxMinimum = driver.FindElement(By.Id("__o3id19"));
                textboxMinimum.SendKeys("No");

                IWebElement textboxCoverageEnd = driver.FindElement(By.Id("__o3id1d"));
                textboxCoverageEnd.SendKeys(myEnrollment.myCoverageEnd);

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

        public int DoAdditionalInformationForAll(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement listboxBlind = driver.FindElement(By.Id("__o3id6"));
                listboxBlind.SendKeys("No");

                IWebElement listboxCondition;
                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxCondition = driver.FindElement(By.Id("__o3id8"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxCondition = driver.FindElement(By.Id("__o3id9"));
                }
                else//3 hh
                {
                    listboxCondition = driver.FindElement(By.Id("__o3ida"));
                }
                listboxCondition.SendKeys("No");

                IWebElement listboxNative;
                if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxNative = driver.FindElement(By.Id("__o3ida"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxNative = driver.FindElement(By.Id("__o3idc"));
                }
                else//3 hh
                {
                    listboxNative = driver.FindElement(By.Id("__o3ide"));
                }
                string indian = "No";
                int indianMember = 1;
                if (householdCount == 2)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    if (myHouseholdMembers.myRace == "Indian")
                    {
                        indian = "Yes";
                        indianMember = 2;
                    }
                }
                else if (householdCount == 3)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    if (myHouseholdMembers.myRace == "Indian")
                    {
                        indian = "Yes";
                        indianMember = 2;
                    }
                    else
                    {
                        myFillStructures = new FillStructures();
                        result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        if (myHouseholdMembers.myRace == "Indian")
                        {
                            indian = "Yes";
                            indianMember = 3;
                        }
                    }
                }
                else //1 hh
                {
                    if (myEnrollment.myRace == "Indian")
                    {
                        indian = "Yes";
                        indianMember = 1;
                    }
                }

                if (indian == "Yes")
                {
                    listboxNative.SendKeys("Yes");
                    listboxNative.Click();
                    System.Threading.Thread.Sleep(1000);
                    IWebElement listboxNativePerson;
                    if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                    {
                        listboxNativePerson = driver.FindElement(By.Id("__o3idb"));
                    }
                    else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                    {
                        if (indianMember == 2)
                        {
                            listboxNativePerson = driver.FindElement(By.Id("__o3idd"));//need to grab this id still
                        }
                        else // hh 1
                        {
                            listboxNativePerson = driver.FindElement(By.Id("__o3idd"));
                        }
                    }
                    else//3 hh
                    {
                        if (indianMember == 3)
                        {
                            listboxNativePerson = driver.FindElement(By.Id("__o3id11"));
                        }
                        else if (indianMember == 2)
                        {
                            listboxNativePerson = driver.FindElement(By.Id("__o3id10"));
                        }
                        else
                        {
                            listboxNativePerson = driver.FindElement(By.Id("__o3idf"));
                        }
                    }
                    listboxNativePerson.Click();
                }
                else
                {
                    listboxNative.SendKeys("No");
                }

                IWebElement listboxVisitMN;
                if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxVisitMN = driver.FindElement(By.Id("__o3idc"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxVisitMN = driver.FindElement(By.Id("__o3idf"));
                }
                else//3 hh
                {
                    listboxVisitMN = driver.FindElement(By.Id("__o3id12"));
                }
                listboxVisitMN.SendKeys("No");

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
                DateTime age = DateTime.MinValue + span;

                DateTime age2 = DateTime.MinValue;
                if (myEnrollment.myHouseholdOther == "Yes")
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    DateTime birth2 = Convert.ToDateTime(myHouseholdMembers.myDOB);
                    TimeSpan span2;
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        span2 = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth2;
                    }
                    else
                    {
                        span2 = DateTime.Now - birth2;
                    }
                    age2 = DateTime.MinValue + span2;
                }

                DateTime age3 = DateTime.MinValue;
                if (householdCount == 3)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myEnrollment, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    DateTime birth3 = Convert.ToDateTime(myHouseholdMembers.myDOB);
                    TimeSpan span3;
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        span3 = Convert.ToDateTime(myHistoryInfo.myTimeTravelDate) - birth3;
                    }
                    else
                    {
                        span3 = DateTime.Now - birth3;
                    }
                    age3 = DateTime.MinValue + span3;
                }
                IWebElement listboxChildActiveDuty;
                IWebElement listboxChildCourtOrder;
                if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxChildActiveDuty = driver.FindElement(By.Id("__o3id16"));
                    listboxChildActiveDuty.SendKeys("No");

                    listboxChildCourtOrder = driver.FindElement(By.Id("__o3id18"));
                    listboxChildCourtOrder.SendKeys("No");
                }
                else
                    if (householdCount == 1 && age.Year - 1 < 19)
                    {
                        listboxChildActiveDuty = driver.FindElement(By.Id("__o3ide"));
                        listboxChildActiveDuty.SendKeys("No");

                        listboxChildCourtOrder = driver.FindElement(By.Id("__o3id10"));
                        listboxChildCourtOrder.SendKeys("No");
                    }

                IWebElement listboxLongTermCare;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id12"));
                }
                else if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3ide"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id12"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id1a"));
                }
                else
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id16"));
                }
                listboxLongTermCare.SendKeys("No");

                IWebElement listboxResidentialTreatment;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id14"));
                }
                else if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id10"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id15"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id1e"));
                }
                else
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id1a"));
                }
                listboxResidentialTreatment.SendKeys("No");

                IWebElement listboxHaveMedicare;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id16"));
                }
                else if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id12"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id18"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id22"));
                }
                else
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id1e"));
                }
                listboxHaveMedicare.SendKeys(myEnrollment.myOtherIns);

                if (myEnrollment.myOtherIns == "Yes")
                {
                    IWebElement listboxMedicarePerson = driver.FindElement(By.Id("__o3id13"));
                    listboxMedicarePerson.Click();
                }

                IWebElement listboxTorture;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id18"));
                }
                else if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id14"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id1b"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id26"));
                }
                else
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id22"));
                }
                listboxTorture.SendKeys("No");

                IWebElement listboxMedicaidEligibility;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id1a"));
                }
                else if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id16"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id1e"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id2a"));
                }
                else
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id26"));
                }
                listboxMedicaidEligibility.SendKeys("No");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement listboxMedicaidHome;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id1c"));
                }
                else if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id18"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id21"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id2e"));
                }
                else
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id2a"));
                }
                listboxMedicaidHome.SendKeys("No");

                IWebElement listboxMedicaidLongTerm;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id1e"));
                }
                else if (myEnrollment.myHouseholdOther == "No" || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                {
                    //new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id1a")));
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id1a"));
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id24")));
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id24"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id32"));
                }
                else
                {
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id2e"));
                }
                listboxMedicaidLongTerm.SendKeys("No");

                string isMA = myApp.DoIsInTypeRange(myEnrollment, myHouseholdMembers, myHistoryInfo, "MA");
                string isBHP = myApp.DoIsInTypeRange(myEnrollment, myHouseholdMembers, myHistoryInfo, "BHP");

                if ((myEnrollment.myHouseholdOther == "No" && householdCount == 1 && myEnrollment.myHomeState == "Minnesota" && (isMA == "True" || isBHP == "True" || age.Year - 1 < 20)) //1 hh
                    || (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2 && (isMA == "True" || isBHP == "True" || age.Year - 1 < 20 || age2.Year - 1 < 20)) // 2 hh
                    || (myEnrollment.myHouseholdOther == "Yes" && householdCount == 3 && (isMA == "True" || isBHP == "True" || age.Year - 1 < 20 || age2.Year - 1 < 20 || age3.Year - 1 < 20)) // 3 hh
                )
                {
                    IWebElement listboxMedicareInjury;
                    if (householdCount == 1 && age.Year - 1 < 19)
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id20"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (householdCount == 1 || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id1c"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id27"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (householdCount == 3 && (age3.Year - 1 < 12 || age3.Year - 1 == 0))
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id36"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (indian == "Yes" || (householdCount == 3 && age3.Year - 1 > 11))
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id32"));
                        listboxMedicareInjury.SendKeys("No");
                    }

                    IWebElement listboxMAStartDate;
                    if (householdCount == 1 && age.Year - 1 < 19)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id22"));
                    }
                    else if (householdCount == 1 || (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No"))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));
                    }
                    else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2a"));
                    }
                    else if (householdCount == 3 && ((age3.Year - 1 > 11 && age3.Year - 1 < 19) || age3.Year - 1 == 0))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id3a"));//was id38
                    }
                    else if (indian == "Yes" || (householdCount == 3 && age3.Year - 1 < 12))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id3a"));
                    }
                    else if (householdCount == 3 && age3.Year - 1 == 18)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id36"));
                    }
                    else
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id34"));
                    }
                    listboxMAStartDate.SendKeys("No");
                }


                if ((myEnrollment.myHouseholdOther == "No" && householdCount == 1 && age.Year - 1 > 19 && myEnrollment.myHomeState != "Minnesota") //1 hh out of state
                    || (myEnrollment.myHouseholdOther == "No" && householdCount == 1 && isMA == "False" && isBHP == "False" && age.Year - 1 > 19) //1 hh
                    || (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2 && isMA == "False" && isBHP == "False" && age.Year - 1 > 19 && age2.Year - 1 > 19) // 2 hh
                    || (myEnrollment.myHouseholdOther == "Yes" && householdCount == 3 && isMA == "False" && isBHP == "False" && age.Year - 1 > 19 && age2.Year - 1 > 19 && age3.Year - 1 > 19) // 3 hh
                )
                {
                    IWebElement listboxMAStartDate;
                    if (householdCount == 1 && age.Year - 1 < 19)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id22"));
                    }
                    else if (householdCount == 1)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1c"));
                    }
                    else if (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myApplyYourself == "No")
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));
                    }
                    else if (householdCount == 2)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2a"));
                    }
                    else if (indian == "Yes" || (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No"))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id34"));
                    }
                    else
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id32"));
                    }
                    listboxMAStartDate.SendKeys("No");
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

        public int DoSummary(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);*/
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
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

        public int DoSubmit(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                /*int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (95 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    if (householdCount == 3)
                    {
                        appwait = (65 + myHistoryInfo.myAppWait) * 1000;//30
                    }
                    else if (householdCount == 2)
                    {
                        appwait = (45 + myHistoryInfo.myAppWait) * 1000;//20
                    }
                    else
                    {
                        appwait = (10 + myHistoryInfo.myAppWait) * 1000;//10
                    }
                }
                System.Threading.Thread.Sleep(appwait);*/
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                myApp.DoWaitForElement(driver, By.Id("__o3id0"), myHistoryInfo);
                driver.FindElement(By.Id("__o3id0")).Click();//renew                              

                IWebElement checkboxIAgreeNoticeRR = driver.FindElement(By.Id("__o3id1"));
                checkboxIAgreeNoticeRR.Click();

                IWebElement checkboxIAgreeInfoApplication = driver.FindElement(By.Id("__o3id2"));
                checkboxIAgreeInfoApplication.Click();

                IWebElement checkboxIDeclare = driver.FindElement(By.Id("__o3id3"));
                checkboxIDeclare.Click();

                IWebElement checkboxIAgreeStatementsBelow = driver.FindElement(By.Id("__o3id4"));
                checkboxIAgreeStatementsBelow.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonNext.Click();

                System.Threading.Thread.Sleep(2000);
                IWebElement checkboxRenewOptions = driver.FindElement(By.Id("__o3id1"));
                checkboxRenewOptions.Click();

                IWebElement buttonNext2 = driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span"));
                buttonNext2.Click();

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

        public int DoAddInfoAPTC(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                driver.SwitchTo().DefaultContent();
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"), myHistoryInfo);

                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/CommonIntake_createApplicationFormForConcernRolePage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                IWebElement checkboxMilitary;
                if (myEnrollment.myHouseholdOther == "No" && myEnrollment.myMilitary == "Yes")
                {
                    checkboxMilitary = driver.FindElement(By.Id("__o3id9"));
                    checkboxMilitary.Click();
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myEnrollment.myMilitary == "Yes")
                {
                    checkboxMilitary = driver.FindElement(By.Id("__o3idc"));//primary
                    checkboxMilitary.Click();
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && myHouseholdMembers.myMilitary == "Yes")
                {
                    checkboxMilitary = driver.FindElement(By.Id("__o3idd"));//2nd member
                    checkboxMilitary.Click();
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

        public int DoParticipants(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                System.Threading.Thread.Sleep(3000);
                driver.SwitchTo().DefaultContent();

                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[3]/div[2]/div[3]/div[3]/div[3]/div/div[4]/div/div/div[1]/div/div[1]/div[1]/div[4]/div/div[3]/div/div/div/span[1]")).Click(); //participants tab

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

        public int DoParticipantsNew(IWebDriver driver, ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                ApplicationDo myApp = new ApplicationDo();
                var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/DefaultIC_listCaseMemberPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement);

                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/a")).Click(); //new

                System.Threading.Thread.Sleep(3000);
                driver.SwitchTo().DefaultContent();
                var iFrameElement2 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/Case_createCaseMemberPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement2);

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[1]/div/table/tbody/tr[1]/td/span/span[2]/a[1]/img")).Click(); //search

                System.Threading.Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                var iFrameElement3 = driver.FindElement(By.XPath("//iframe[contains(@src,'en_US/TaskQuery_searchPersonPopupPage.do')]"));
                driver.SwitchTo().Frame(iFrameElement3);

                IWebElement textboxSSN = driver.FindElement(By.Id("__o3id0"));
                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)
                {
                    string householdMember2SSN = DoGetHouseholdMemberSSN(myHistoryInfo.myTestId, "2");
                    if (householdMember2SSN != null)
                    {
                        textboxSSN.SendKeys(householdMember2SSN); //household member 2
                    }
                    else
                    {
                        textboxSSN.SendKeys(myEnrollment.mySSNNum); //primary member
                    }
                }
                else
                {
                    textboxSSN.SendKeys(myEnrollment.mySSNNum); //primary member
                }

                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[5]/a[1]/span/span/span")).Click(); //search
                System.Threading.Thread.Sleep(2000);

                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[2]/form/div/div[6]/div/div[2]/table/tbody/tr/td[1]/span/a/span/span/span"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[2]/form/div/div[6]/div/div[2]/table/tbody/tr/td[1]/span/a/span/span/span")).Click(); //select
                System.Threading.Thread.Sleep(1000);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.SwitchTo().DefaultContent();
                driver.SwitchTo().Frame(iFrameElement2);
                myApp.DoWaitForElement(driver, By.XPath("/html/body/div[3]/div/a[1]/span/span/span"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/div[3]/div/a[1]/span/span/span")).Click(); //save

                System.Threading.Thread.Sleep(10000);
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


        public string DoGetHouseholdMemberSSN(string testId, string memberId)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM HouseMembers where TestId = " + "'" + testId + "'" + " and HouseMembersID = " + "'" + memberId + "'", con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetString(20);
                    }
                    else
                    {
                        return "Error locating household member ssn";
                    }
                }
            }
            catch
            {
                return "Error locating household member ssn";
            }

        }


    }
}
