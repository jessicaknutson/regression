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
using OpenQA.Selenium.Support.UI;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Threading;

namespace MNsure_Regression_1
{
    class ApplicationDo
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoGettingStarted(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
                }

                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (6 + myHistoryInfo.myAppWait) * 1000;//was 2
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
                myDriver.SwitchTo().DefaultContent();
                DoWaitForElement(myDriver, By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/MNHIXCitizenWorkspace_setup')]"), myHistoryInfo);
                var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/MNHIXCitizenWorkspace_setup')]"));
                myDriver.SwitchTo().Frame(iFrameElement);

                //wait for link
                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3btn.next_label"))));
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id6")));
                IWebElement checkboxAgree = myDriver.FindElement(By.Id("__o3id6"));
                checkboxAgree.Click();

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3btn.next_label")));
                IWebElement buttonNext = myDriver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHouseholdMembers.myPassCount = "1";//reset count back to 1 on start in case an error happened during previous run
                DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                myApplication.myPassCount = "1";//reset count back to 1 on start in case an error happened during previous run
                DoUpdateAppPassCount(myHistoryInfo, myApplication.myPassCount);

                Enrollments myEnrollment = new Enrollments();
                myHouseholdMembers.myReEnroll = "No"; //reset reenroll on start in case an error happened during previous run
                myEnrollment.DoUpdateReEnroll(myHistoryInfo, myHouseholdMembers.myReEnroll);
                myHouseholdMembers.mySaveExit = "No"; //reset saveexit on start in case an error happened during previous run
                myEnrollment.DoUpdateSaveExit(myHistoryInfo, myHouseholdMembers.mySaveExit);

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

        public int DoApplicantDetailsAbout(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
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

                DoWaitForElement(myDriver, By.Id("__o3btn.next"), myHistoryInfo);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement buttonNext2 = myDriver.FindElement(By.Id("__o3btn.next"));
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoApplicantDetails(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
                }

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

                DoWaitForElement(myDriver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"), myHistoryInfo);
                IWebElement textboxFirstName = myDriver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxFirstName.SendKeys(myApplication.myFirstName);

                IWebElement textboxMiddleName = myDriver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[4]/table/tbody/tr/td[1]/div/div/input"));
                if (myApplication.myMiddleName != null)
                {
                    textboxMiddleName.SendKeys(myApplication.myMiddleName);
                }
                IWebElement textboxLastName = myDriver.FindElement(By.Id("__o3id8"));
                textboxLastName.SendKeys(myApplication.myLastName);

                IWebElement textboxSuffix = myDriver.FindElement(By.Id("__o3id9"));
                if (myApplication.mySuffix != null)
                {
                    textboxSuffix.SendKeys(myApplication.mySuffix);
                }

                IWebElement textboxGender = myDriver.FindElement(By.Id("__o3ida"));
                textboxGender.SendKeys(myApplication.myGender);

                IWebElement textboxMaritalStatus = myDriver.FindElement(By.Id("__o3idb"));
                textboxMaritalStatus.SendKeys(myApplication.myMaritalStatus);

                IWebElement textboxDOB = myDriver.FindElement(By.Id("__o3idc"));
                textboxDOB.Click();
                string tempDOB;
                tempDOB = Convert.ToString(myApplication.myDOB);
                tempDOB = DateTime.Parse(tempDOB).ToString("MM/dd/yyyy");
                textboxDOB.SendKeys(tempDOB);

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3idd")));
                IWebElement listboxLiveMN = myDriver.FindElement(By.Id("__o3idd"));
                listboxLiveMN.SendKeys(myApplication.myLiveMN);
                listboxLiveMN.Click();

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[1]/td[1]/span[1]")));
                IWebElement outsideClick = myDriver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[1]/td[1]/span[1]"));
                outsideClick.Click();

                if (myApplication.myLiveMN == "Yes")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3ide")));
                    IWebElement listboxHomeless = myDriver.FindElement(By.Id("__o3ide"));
                    listboxHomeless.SendKeys(myApplication.myHomeless);
                    listboxHomeless.Click();

                    outsideClick.Click();
                }
                else
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3idf")));
                    IWebElement listboxTempAbsent = myDriver.FindElement(By.Id("__o3idf"));
                    listboxTempAbsent.SendKeys("No");
                }

                if (myApplication.myHomeless == "No")
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id11")));
                    IWebElement listboxAddress1 = myDriver.FindElement(By.Id("__o3id11"));
                    listboxAddress1.SendKeys(myApplication.myHomeAddress1);

                    if (myApplication.myHomeAddress2 != null)
                    {
                        IWebElement listboxAddress2 = myDriver.FindElement(By.Id("__o3id12"));
                        listboxAddress2.SendKeys(myApplication.myHomeAddress2);
                    }
                    if (myApplication.myHomeAptSuite != null)
                    {
                        IWebElement listboxAptSuite = myDriver.FindElement(By.Id("__o3id13"));
                        listboxAptSuite.SendKeys(myApplication.myHomeAptSuite);
                    }
                    IWebElement listboxCity = myDriver.FindElement(By.Id("__o3id14"));
                    listboxCity.SendKeys(myApplication.myHomeCity);

                    IWebElement listboxCounty = myDriver.FindElement(By.Id("__o3id15"));
                    listboxCounty.SendKeys(myApplication.myHomeCounty);

                    IWebElement listboxState = myDriver.FindElement(By.Id("__o3id16"));
                    listboxState.SendKeys(myApplication.myHomeState);

                    IWebElement listboxZip = myDriver.FindElement(By.Id("__o3id17"));
                    listboxZip.SendKeys(myApplication.myHomeZip);

                    IWebElement listboxAddressSame = myDriver.FindElement(By.Id("__o3id18"));
                    listboxAddressSame.SendKeys(myApplication.myAddressSame);
                }
                else
                {
                    new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id10")));
                    IWebElement listboxCounty = myDriver.FindElement(By.Id("__o3id10"));
                    listboxCounty.SendKeys(myApplication.myHomeCounty);

                    IWebElement listboxHaveMailingAddress = myDriver.FindElement(By.Id("__o3id19"));
                    listboxHaveMailingAddress.SendKeys(myApplication.myMailingAddressYN);
                }

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id21")));
                IWebElement listboxPlanLiveMN = myDriver.FindElement(By.Id("__o3id21"));
                listboxPlanLiveMN.SendKeys(myApplication.myPlanLiveMN);

                if (myApplication.myMailingAddressYN == "Yes")
                {
                    myDriver.SwitchTo().DefaultContent();
                    DoWaitForElement(myDriver, By.TagName("iframe"), myHistoryInfo);

                    var iFrameElement = myDriver.FindElement(By.TagName("iframe"));
                    myDriver.SwitchTo().Frame(iFrameElement);

                    IWebElement listboxAddress1 = myDriver.FindElement(By.Id("__o3id1a"));
                    listboxAddress1.SendKeys(myApplication.myMailAddress1);
                    if (myApplication.myMailAddress2 != null)
                    {
                        IWebElement listboxAddress2 = myDriver.FindElement(By.Id("__o3id1b"));
                        listboxAddress2.SendKeys(myApplication.myMailAddress2);
                    }
                    if (myApplication.myMailAptSuite != null)
                    {
                        IWebElement listboxAptSuite = myDriver.FindElement(By.Id("__o3id1c"));
                        listboxAptSuite.SendKeys(myApplication.myMailAptSuite);
                    }
                    IWebElement listboxCity = myDriver.FindElement(By.Id("__o3id1d"));
                    listboxCity.SendKeys(myApplication.myMailCity);

                    IWebElement listboxCounty = myDriver.FindElement(By.Id("__o3id1e"));
                    listboxCounty.SendKeys(myApplication.myMailCounty);

                    IWebElement listboxState = myDriver.FindElement(By.Id("__o3id1f"));
                    listboxState.SendKeys(myApplication.myMailState);

                    IWebElement listboxZip = myDriver.FindElement(By.Id("__o3id20"));
                    listboxZip.SendKeys(myApplication.myMailZip);
                }

                IWebElement listboxPreferedContact = myDriver.FindElement(By.Id("__o3id23"));
                listboxPreferedContact.SendKeys(myApplication.myPrefContact);

                string mysPhone1 = myApplication.myPhoneNum.Substring(0, 3);
                string mysPhone2 = myApplication.myPhoneNum.Substring(3, 3);
                string mysPhone3 = myApplication.myPhoneNum.Substring(6, 4);
                IWebElement textboxPhoneNum = myDriver.FindElement(By.Id("__o3id24"));
                textboxPhoneNum.SendKeys(mysPhone1);
                IWebElement textboxPhoneNum2 = myDriver.FindElement(By.Id("__o3id25"));
                textboxPhoneNum2.SendKeys(mysPhone2);
                IWebElement textboxPhoneNum3 = myDriver.FindElement(By.Id("__o3id26"));
                textboxPhoneNum3.SendKeys(mysPhone3);

                IWebElement listboxPhoneType = myDriver.FindElement(By.Id("__o3id27"));
                listboxPhoneType.SendKeys(myApplication.myPhoneType);
                System.Threading.Thread.Sleep(500);

                if (myApplication.myAltNum != null)
                {
                    string mysAPhone1 = myApplication.myAltNum.Substring(0, 3);
                    string mysAPhone2 = myApplication.myAltNum.Substring(3, 3);
                    string mysAPhone3 = myApplication.myAltNum.Substring(6, 4);
                    IWebElement textboxAPhoneNum = myDriver.FindElement(By.Id("__o3id28"));
                    textboxAPhoneNum.SendKeys(mysAPhone1);
                    IWebElement textboxAPhoneNum2 = myDriver.FindElement(By.Id("__o3id29"));
                    textboxAPhoneNum2.SendKeys(mysAPhone2);
                    IWebElement textboxAPhoneNum3 = myDriver.FindElement(By.Id("__o3id2a"));
                    textboxAPhoneNum3.SendKeys(mysAPhone3);

                    IWebElement listboxAPhoneType = myDriver.FindElement(By.Id("__o3id2b"));
                    listboxAPhoneType.SendKeys(myApplication.myAltNumType);
                }
                if (myApplication.myEmail != null)
                {
                    IWebElement textboxEmail = myDriver.FindElement(By.Id("__o3id2c"));
                    textboxEmail.SendKeys(myApplication.myEmail);
                }
                IWebElement listboxVoterCard = myDriver.FindElement(By.Id("__o3id32"));
                listboxVoterCard.SendKeys(myApplication.myVoterCard);

                IWebElement listboxNotices = myDriver.FindElement(By.Id("__o3id33"));
                listboxNotices.SendKeys(myApplication.myNotices);

                IWebElement listboxAuthRep = myDriver.FindElement(By.Id("__o3id34"));
                listboxAuthRep.SendKeys(myApplication.myAuthRep);

                IWebElement listboxApplyYouself = myDriver.FindElement(By.Id("__o3id35"));
                listboxApplyYouself.SendKeys(myApplication.myApplyYourself);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement buttonNext = myDriver.FindElement(By.Id("__o3btn.next_label"));
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
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoApplicantDetailsWithoutDiscounts(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"), myHistoryInfo);

                IWebElement textboxFirstName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxFirstName.SendKeys(myApplication.myFirstName);
                if (myApplication.myMiddleName != null && myApplication.myMiddleName != "")
                {
                    IWebElement textboxMiddleName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr[1]/td[4]/table/tbody/tr/td[1]/div/div/input"));
                    textboxMiddleName.SendKeys(myApplication.myMiddleName);
                }
                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id8"));
                textboxLastName.SendKeys(myApplication.myLastName);

                if (myApplication.mySuffix != null && myApplication.mySuffix != "")
                {
                    IWebElement textboxSuffix = driver.FindElement(By.Id("__o3id9"));
                    textboxSuffix.SendKeys(myApplication.mySuffix);
                }

                IWebElement textboxGender = driver.FindElement(By.Id("__o3ida"));
                textboxGender.SendKeys(myApplication.myGender);

                IWebElement textboxMaritalStatus = driver.FindElement(By.Id("__o3idb"));
                textboxMaritalStatus.SendKeys(myApplication.myMaritalStatus);

                string tempDOB;
                int tempDOBLength;
                tempDOB = Convert.ToString(myApplication.myDOB);
                tempDOBLength = tempDOB.Length;
                tempDOB = tempDOB.Substring(0, tempDOBLength);
                IWebElement textboxDOB = driver.FindElement(By.Id("__o3idc"));
                textboxDOB.SendKeys(tempDOB);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3idd")));
                IWebElement listboxLiveMN = driver.FindElement(By.Id("__o3idd"));
                listboxLiveMN.SendKeys(myApplication.myLiveMN);

                IWebElement outsideClick = driver.FindElement(By.Id("__o3idc"));
                outsideClick.Click();

                if (myApplication.myLiveMN == "Yes")
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3ide")));
                    IWebElement listboxHomeless = driver.FindElement(By.Id("__o3ide"));
                    listboxHomeless.SendKeys(myApplication.myHomeless);
                    driver.FindElement(By.Id("__o3idd")).Click();
                }

                if (myApplication.myHomeless == "No")
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id11")));
                    IWebElement listboxAddress1 = driver.FindElement(By.Id("__o3id11"));
                    listboxAddress1.SendKeys(myApplication.myHomeAddress1);

                    IWebElement listboxAddress2 = driver.FindElement(By.Id("__o3id12"));
                    listboxAddress2.SendKeys(myApplication.myHomeAddress2);

                    IWebElement listboxAptSuite = driver.FindElement(By.Id("__o3id13"));
                    listboxAptSuite.SendKeys(myApplication.myHomeAptSuite);

                    IWebElement listboxCity = driver.FindElement(By.Id("__o3id14"));
                    listboxCity.SendKeys(myApplication.myHomeCity);

                    IWebElement listboxCounty = driver.FindElement(By.Id("__o3id15"));
                    listboxCounty.SendKeys(myApplication.myHomeCounty);

                    IWebElement listboxState = driver.FindElement(By.Id("__o3id16"));
                    listboxState.SendKeys(myApplication.myHomeState);

                    IWebElement listboxZip = driver.FindElement(By.Id("__o3id17"));
                    listboxZip.SendKeys(myApplication.myHomeZip);

                    IWebElement listboxAddressSame = driver.FindElement(By.Id("__o3id18"));
                    listboxAddressSame.SendKeys(myApplication.myAddressSame);
                }
                else
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id10")));
                    IWebElement listboxCounty = driver.FindElement(By.Id("__o3id10"));
                    listboxCounty.SendKeys(myApplication.myHomeCounty);

                    IWebElement listboxHaveMailingAddress = driver.FindElement(By.Id("__o3id19"));
                    listboxHaveMailingAddress.SendKeys(myApplication.myMailingAddressYN);
                }

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id21")));
                IWebElement listboxPlanLiveMN = driver.FindElement(By.Id("__o3id21"));
                listboxPlanLiveMN.SendKeys(myApplication.myPlanLiveMN);

                if (myApplication.myMailingAddressYN == "Yes")
                {
                    driver.SwitchTo().DefaultContent();
                    DoWaitForElement(driver, By.TagName("iframe"), myHistoryInfo);

                    var iFrameElement = driver.FindElement(By.TagName("iframe"));
                    driver.SwitchTo().Frame(iFrameElement);

                    IWebElement listboxAddress1 = driver.FindElement(By.Id("__o3id1a"));
                    listboxAddress1.SendKeys(myApplication.myMailAddress1);
                    if (myApplication.myMailAddress2 != null)
                    {
                        IWebElement listboxAddress2 = driver.FindElement(By.Id("__o3id1b"));
                        listboxAddress2.SendKeys(myApplication.myMailAddress2);
                    }
                    if (myApplication.myMailAptSuite != null)
                    {
                        IWebElement listboxAptSuite = driver.FindElement(By.Id("__o3id1c"));
                        listboxAptSuite.SendKeys(myApplication.myMailAptSuite);
                    }
                    IWebElement listboxCity = driver.FindElement(By.Id("__o3id1d"));
                    listboxCity.SendKeys(myApplication.myMailCity);

                    IWebElement listboxCounty = driver.FindElement(By.Id("__o3id1e"));
                    listboxCounty.SendKeys(myApplication.myMailCounty);

                    IWebElement listboxState = driver.FindElement(By.Id("__o3id1f"));
                    listboxState.SendKeys(myApplication.myMailState);

                    IWebElement listboxZip = driver.FindElement(By.Id("__o3id20"));
                    listboxZip.SendKeys(myApplication.myMailZip);
                }

                IWebElement listboxHispanic = driver.FindElement(By.Id("__o3id23"));
                listboxHispanic.SendKeys(myApplication.myHispanic);
                driver.FindElement(By.Id("__o3id21")).Click();

                if (myApplication.myRace == "Indian")
                {
                    IWebElement checkboxRace = driver.FindElement(By.Id("__o3id29"));
                    checkboxRace.Click();
                }
                else
                {
                    IWebElement checkboxRace = driver.FindElement(By.Id("__o3id34"));
                    checkboxRace.Click();
                }

                IWebElement listboxSSN = driver.FindElement(By.Id("__o3id39"));
                listboxSSN.SendKeys(myApplication.mySSN);
                driver.FindElement(By.Id("__o3id38")).Click();

                if (myApplication.mySSN == "Yes")
                {
                    IWebElement listboxSSNNum = driver.FindElement(By.Id("__o3id3a"));
                    string hhssn = myApplication.mySSNNum;
                    listboxSSNNum.SendKeys(hhssn);
                    driver.FindElement(By.Id("__o3id39")).Click();
                }
                else
                {
                    IWebElement listboxAppliedSSN = driver.FindElement(By.Id("__o3id3b"));
                    listboxAppliedSSN.SendKeys(myApplication.myAppliedSSN);
                    driver.FindElement(By.Id("__o3id39")).Click();
                    if (myApplication.myAppliedSSN == "No")
                    {
                        IWebElement listboxWhyNoSSN = driver.FindElement(By.Id("__o3id3c"));
                        listboxWhyNoSSN.SendKeys(myApplication.myWhyNoSSN);
                        driver.FindElement(By.Id("__o3id39")).Click();
                    }

                    if (myApplication.myWhyNoSSN == "Other")
                    {
                        IWebElement listboxAssistSSN = driver.FindElement(By.Id("__o3id3d"));
                        listboxAssistSSN.SendKeys(myApplication.myAssistSSN);
                        driver.FindElement(By.Id("__o3id39")).Click();
                    }

                }

                IWebElement listboxApplyYouself = driver.FindElement(By.Id("__o3id3e"));
                listboxApplyYouself.SendKeys(myApplication.myApplyYourself);
                driver.FindElement(By.Id("__o3id39")).Click();

                IWebElement listboxCitizen = driver.FindElement(By.Id("__o3id3f"));
                listboxCitizen.SendKeys(myApplication.myCitizen);
                driver.FindElement(By.Id("__o3id39")).Click();

                IWebElement listboxInJail = driver.FindElement(By.Id("__o3id45"));
                listboxInJail.SendKeys("No");

                IWebElement listboxMedicalIns = driver.FindElement(By.Id("__o3id47"));
                listboxMedicalIns.SendKeys("No");

                IWebElement listboxUseTobacco = driver.FindElement(By.Id("__o3id49"));
                listboxUseTobacco.SendKeys(myApplication.myTobacco);

                if (myApplication.myTobacco == "Yes")
                {
                    IWebElement listboxTobaccoLast = driver.FindElement(By.Id("__o3id4a"));
                    listboxTobaccoLast.SendKeys(myApplication.myTobaccoLast);
                }

                IWebElement listboxIndian = driver.FindElement(By.Id("__o3id4b"));
                listboxIndian.SendKeys("No");

                IWebElement listboxPreferredContact = driver.FindElement(By.Id("__o3id4f"));
                listboxPreferredContact.SendKeys(myApplication.myPrefContact);

                string mysPhone1 = myApplication.myPhoneNum.Substring(0, 3);
                string mysPhone2 = myApplication.myPhoneNum.Substring(3, 3);
                string mysPhone3 = myApplication.myPhoneNum.Substring(6, 4);
                IWebElement textboxPhoneNum = driver.FindElement(By.Id("__o3id50"));
                textboxPhoneNum.SendKeys(mysPhone1);
                IWebElement textboxPhoneNum2 = driver.FindElement(By.Id("__o3id51"));
                textboxPhoneNum2.SendKeys(mysPhone2);
                IWebElement textboxPhoneNum3 = driver.FindElement(By.Id("__o3id52"));
                textboxPhoneNum3.SendKeys(mysPhone3);

                IWebElement listboxPhoneType = driver.FindElement(By.Id("__o3id53"));
                listboxPhoneType.SendKeys(myApplication.myPhoneType);
                System.Threading.Thread.Sleep(500);

                if (myApplication.myAltNum != null)
                {
                    string mysAPhone1 = myApplication.myAltNum.Substring(0, 3);
                    string mysAPhone2 = myApplication.myAltNum.Substring(3, 3);
                    string mysAPhone3 = myApplication.myAltNum.Substring(6, 4);
                    IWebElement textboxAPhoneNum = driver.FindElement(By.Id("__o3id54"));
                    textboxAPhoneNum.SendKeys(mysAPhone1);
                    IWebElement textboxAPhoneNum2 = driver.FindElement(By.Id("__o3id55"));
                    textboxAPhoneNum2.SendKeys(mysAPhone2);
                    IWebElement textboxAPhoneNum3 = driver.FindElement(By.Id("__o3id56"));
                    textboxAPhoneNum3.SendKeys(mysAPhone3);

                    IWebElement listboxAPhoneType = driver.FindElement(By.Id("__o3id57"));
                    listboxAPhoneType.SendKeys(myApplication.myAltNumType);
                }

                if (myApplication.myEmail != null)
                {
                    IWebElement textboxEmail = driver.FindElement(By.Id("__o3id58"));
                    textboxEmail.SendKeys(myApplication.myEmail);
                }

                IWebElement listboxVoterCard = driver.FindElement(By.Id("__o3id5e"));
                listboxVoterCard.SendKeys(myApplication.myVoterCard);

                IWebElement listboxNotices = driver.FindElement(By.Id("__o3id5f"));
                listboxNotices.SendKeys(myApplication.myNotices);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoApplicantDetailsPrimary(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"), myHistoryInfo);

                IWebElement textboxFirstName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxFirstName.SendKeys(myHouseholdMembers.myFirstName);

                if (myHouseholdMembers.myMiddleName != null && myHouseholdMembers.myMiddleName != "")
                {
                    IWebElement textboxMiddleName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[4]/table/tbody/tr/td[1]/div/div/input"));
                    textboxMiddleName.SendKeys(myHouseholdMembers.myMiddleName);
                }
                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id8"));
                textboxLastName.SendKeys(myHouseholdMembers.myLastName);

                if (myHouseholdMembers.mySuffix != null && myHouseholdMembers.mySuffix != "")
                {
                    IWebElement textboxSuffix = driver.FindElement(By.Id("__o3id9"));
                    textboxSuffix.SendKeys(myHouseholdMembers.mySuffix);
                }

                IWebElement textboxGender = driver.FindElement(By.Id("__o3ida"));
                textboxGender.SendKeys(myHouseholdMembers.myGender);

                IWebElement textboxMaritalStatus = driver.FindElement(By.Id("__o3idb"));
                textboxMaritalStatus.SendKeys(myHouseholdMembers.myMaritalStatus);

                string tempDOB;
                int tempDOBLength;
                tempDOB = Convert.ToString(myHouseholdMembers.myDOB);
                tempDOBLength = tempDOB.Length;
                tempDOB = tempDOB.Substring(0, tempDOBLength);
                IWebElement textboxDOB = driver.FindElement(By.Id("__o3idc"));
                textboxDOB.SendKeys(tempDOB);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3idd")));
                IWebElement listboxLiveMN = driver.FindElement(By.Id("__o3idd"));
                listboxLiveMN.SendKeys(myHouseholdMembers.myLiveInMN);
                listboxLiveMN.Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[1]/td[1]/span[1]")));
                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[1]/td[1]/span[1]"));
                outsideClick.Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id19")));
                IWebElement listboxPlanLiveMN = driver.FindElement(By.Id("__o3id19"));
                listboxPlanLiveMN.SendKeys(myHouseholdMembers.myPlanMakeMNHome);

                IWebElement listboxPreferedContact = driver.FindElement(By.Id("__o3id1b"));
                listboxPreferedContact.SendKeys(myHouseholdMembers.myPrefContact);

                string mysPhone1 = myHouseholdMembers.myPhoneNum.Substring(0, 3);
                string mysPhone2 = myHouseholdMembers.myPhoneNum.Substring(3, 3);
                string mysPhone3 = myHouseholdMembers.myPhoneNum.Substring(6, 4);
                IWebElement textboxPhoneNum = driver.FindElement(By.Id("__o3id1c"));
                textboxPhoneNum.SendKeys(mysPhone1);
                IWebElement textboxPhoneNum2 = driver.FindElement(By.Id("__o3id1d"));
                textboxPhoneNum2.SendKeys(mysPhone2);
                IWebElement textboxPhoneNum3 = driver.FindElement(By.Id("__o3id1e"));
                textboxPhoneNum3.SendKeys(mysPhone3);

                IWebElement listboxPhoneType = driver.FindElement(By.Id("__o3id1f"));
                listboxPhoneType.SendKeys(myHouseholdMembers.myPhoneType);
                System.Threading.Thread.Sleep(500);

                if (myHouseholdMembers.myAltNum != null && myHouseholdMembers.myAltNum != "")
                {
                    string mysAPhone1 = myHouseholdMembers.myAltNum.Substring(0, 3);
                    string mysAPhone2 = myHouseholdMembers.myAltNum.Substring(3, 3);
                    string mysAPhone3 = myHouseholdMembers.myAltNum.Substring(6, 4);
                    IWebElement textboxAPhoneNum = driver.FindElement(By.Id("__o3id20"));
                    textboxAPhoneNum.SendKeys(mysAPhone1);
                    IWebElement textboxAPhoneNum2 = driver.FindElement(By.Id("__o3id21"));
                    textboxAPhoneNum2.SendKeys(mysAPhone2);
                    IWebElement textboxAPhoneNum3 = driver.FindElement(By.Id("__o3id22"));
                    textboxAPhoneNum3.SendKeys(mysAPhone3);

                    IWebElement listboxAPhoneType = driver.FindElement(By.Id("__o3id23"));
                    listboxAPhoneType.SendKeys(myHouseholdMembers.myAltNumType);
                }

                if (myHouseholdMembers.myEmail != null && myHouseholdMembers.myEmail != "")
                {
                    IWebElement textboxEmail = driver.FindElement(By.Id("__o3id24"));
                    textboxEmail.SendKeys(myHouseholdMembers.myEmail);
                }
                IWebElement listboxVoterCard = driver.FindElement(By.Id("__o3id2a"));
                listboxVoterCard.SendKeys(myHouseholdMembers.myVoterCard);

                IWebElement listboxNotices = driver.FindElement(By.Id("__o3id2b"));
                listboxNotices.SendKeys(myHouseholdMembers.myNotices);

                IWebElement listboxAuthRep = driver.FindElement(By.Id("__o3id2c"));
                listboxAuthRep.SendKeys(myHouseholdMembers.myAuthRep);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoMoreAboutYou(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
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
                DoWaitForElement(myDriver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                myDriver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(myDriver);

                if (myApplication.myHispanic == "No")
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                else
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                IWebElement outsideClick = myDriver.FindElement(By.Id("__o3id6"));
                outsideClick.Click();

                if (myApplication.myRace == "Indian")
                {
                    IWebElement checkboxRace = myDriver.FindElement(By.Id("__o3idc"));
                    checkboxRace.Click();
                }
                else
                {
                    IWebElement checkboxRace = myDriver.FindElement(By.Id("__o3id17"));
                    checkboxRace.Click();
                }

                IWebElement listboxSSN = myDriver.FindElement(By.Id("__o3id1c"));
                listboxSSN.SendKeys(myApplication.mySSN);
                myDriver.FindElement(By.Id("__o3id1b")).Click();

                if (myApplication.mySSN == "Yes")
                {
                    IWebElement listboxSSNNum = myDriver.FindElement(By.Id("__o3id1d"));
                    if (myApplication.myApplyYourself == "No")
                    {
                        int temp = Convert.ToInt32(myApplication.mySSNNum) + 1;
                        listboxSSNNum.SendKeys(Convert.ToString(temp));
                    }
                    else
                    {
                        string hhssn = myApplication.mySSNNum;
                        listboxSSNNum.SendKeys(hhssn);
                    }
                }
                else
                {
                    IWebElement listboxAppliedSSN = myDriver.FindElement(By.Id("__o3id1e"));
                    listboxAppliedSSN.SendKeys(myApplication.myAppliedSSN);
                    // TFR 12-01-2016 sometimes the page is down further and this element is not visible.  It is a small window of opportunity but it does get hit on occasion.
                    // Also most keystrokes like page up are non functional.  The code below calls some Javascript to scroll up and now the element is accessible. Test BHP14 exposed this issue.
                    if (!outsideClick.Displayed)
                    {
                        var bogus = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(("window.scrollBy(0,-250)"));
                    }
                    outsideClick.Click();

                    if (myApplication.myAppliedSSN == "No")
                    {
                        IWebElement listboxWhyNoSSN = myDriver.FindElement(By.Id("__o3id1f"));
                        listboxWhyNoSSN.SendKeys(myApplication.myWhyNoSSN);
                        listboxAppliedSSN.Click();
                    }

                    if (myApplication.myWhyNoSSN == "Other")
                    {
                        IWebElement listboxAssistSSN = myDriver.FindElement(By.Id("__o3id20"));
                        listboxAssistSSN.SendKeys(myApplication.myAssistSSN);
                        listboxAppliedSSN.Click();
                    }
                }
                myDriver.FindElement(By.Id("__o3id1c")).Click();

                IWebElement listboxCitizen = myDriver.FindElement(By.Id("__o3id21"));
                listboxCitizen.SendKeys(myApplication.myCitizen);
                //is this needed?
                //myDriver.FindElement(By.Id("__o3id1b")).Click();

                //This will only appear if age 18-26
                DateTime birth = Convert.ToDateTime(myApplication.myDOB);
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
                if (myApplication.myHouseholdOther == "Yes")
                {
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

                string isPregnant = "No";
                string isFemale = "No";
                string householdMember = "1";
                if (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                {
                    if (myHouseholdMembers.myGender == "Female")
                    {
                        isFemale = "Yes";
                        if (myHouseholdMembers.myIsPregnant == "Yes")
                        {
                            isPregnant = "Yes";
                            householdMember = "2";
                        }
                    }
                }
                else
                {
                    if (myApplication.myGender == "Female" && age.Year - 1 < 65)
                    {
                        isFemale = "Yes";
                        if (myApplication.myIsPregnant == "Yes")
                        {
                            isPregnant = "Yes";
                        }
                    }
                }

                if (isFemale == "Yes")
                {
                    IWebElement listboxPregnant;
                    listboxPregnant = myDriver.FindElement(By.Id("__o3id2c"));
                    listboxPregnant.Clear();
                    listboxPregnant.SendKeys(myApplication.myIsPregnant);
                }

                if (isPregnant == "Yes")
                {
                    string children;
                    string dueDate;
                    string pregnancyEnded;
                    if (householdMember == "1")
                    {
                        children = myApplication.myChildren;
                        dueDate = myApplication.myDueDate;
                        pregnancyEnded = myApplication.myPregnancyEnded;
                    }
                    else
                    {
                        children = myHouseholdMembers.myChildren;
                        dueDate = myHouseholdMembers.myDueDate;
                        pregnancyEnded = myHouseholdMembers.myPregnancyEnded;
                    }
                    myDriver.FindElement(By.Id("__o3id20")).SendKeys(children);
                    myDriver.FindElement(By.Id("__o3id20")).SendKeys(dueDate);
                    myDriver.FindElement(By.Id("__o3id20")).SendKeys(pregnancyEnded);
                }

                string fosterCare = "No";
                //18-26 seems to work most of the time, q5 age 26 fails
                if (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
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
                    if ((myApplication.myApplyYourself == "Yes" && myApplication.myGender == "Female")
                        || (myApplication.myApplyYourself == "No" && myHouseholdMembers.myGender == "Female"))
                    {
                        listboxFosterCare = myDriver.FindElement(By.Id("__o3id30"));
                        listboxFosterCare.SendKeys(myApplication.myFosterCare);
                    }
                    else if ((myApplication.myApplyYourself == "Yes" && myApplication.myGender == "Male")
                        || (myApplication.myApplyYourself == "No" && myHouseholdMembers.myGender == "Male"))
                    {
                        listboxFosterCare = myDriver.FindElement(By.Id("__o3id2f"));
                        listboxFosterCare.SendKeys(myApplication.myFosterCare);
                    }
                }

                new WebDriverWait(myDriver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3btn.next")));
                IWebElement buttonNext = myDriver.FindElement(By.Id("__o3btn.next"));
                buttonNext.Click();

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoMoreSSN(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div/input"), myHistoryInfo);

                IWebElement textboxSSN = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div/input"));
                string hhssn = myApplication.mySSNNum;
                textboxSSN.SendKeys(hhssn);

                string isFemale = "No";
                if (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                {
                    if (myApplication.myGender == "Female")
                    {
                        isFemale = "Yes";
                    }
                }

                if (isFemale == "Yes")
                {
                    IWebElement listboxPregnant = driver.FindElement(By.Id("__o3id1a"));
                    listboxPregnant.Clear();
                    listboxPregnant.SendKeys(myApplication.myIsPregnant);
                }

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3btn.next")));
                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
                buttonNext.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoHouseholdAbout(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (7 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (11 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (7 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (9 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);

                if (myHistoryInfo.myResume == "Yes")
                {
                    driver.SwitchTo().DefaultContent();
                    DoWaitForElement(driver, By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/CitizenWorkspace_resumeMotivationPage.do')]"), myHistoryInfo);
                    var iFrameElement = driver.FindElement(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/CitizenWorkspace_resumeMotivationPage.do')]"));
                    driver.SwitchTo().Frame(iFrameElement);
                }
                else
                {
                    bool isNotdone1 = true;
                    int retries1 = 0;

                    do
                    {
                        String retVal = DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);
                        //String retVal = DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/span/span/span/span[3]"), myHistoryInfo);
                        if (retVal.Equals("true"))
                        {
                            isNotdone1 = false;
                        }
                        else
                        {
                            retries1++;
                        }

                        if (retries1 > 3)
                        {
                            throw new System.Exception("Button never became active");
                        }
                    } while (isNotdone1);

                }

                // TFR 12-7-2016 Fix to QHP Smoke Regr test ID = 7.  This test always failed the first time it was ran and then passed. 
                // See notes under DoWaitForPossibleStaleElement
                DoWaitForPossibleStaleElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);
                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
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

        public int DoHouseholdMembers(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
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
                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                else
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoHouseholdMembersWithoutDiscounts(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (6 + myHistoryInfo.myAppWait) * 1000;//norm 10
                }
                System.Threading.Thread.Sleep(appwait);
                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                if (myApplication.myHouseholdOther == "No")
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                else
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoTaxFiler(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

                if (myApplication.myHouseholdOther == "No")
                {
                    DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td/div[2]/div/div[1]/input"), myHistoryInfo);

                    IWebElement checkboxPerson = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td/div[2]/div/div[1]/input"));
                    checkboxPerson.Click();
                }
                else
                {
                    DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td[1]/div[2]/div/div[1]/input"), myHistoryInfo);

                    IWebElement checkboxPerson = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td[1]/div[2]/div/div[1]/input"));
                    checkboxPerson.Click();

                    IWebElement checkboxPerson2 = driver.FindElement(By.Id("__o3id7"));
                    if (myHouseholdMembers.myTaxFiler == "Yes")
                    {
                        checkboxPerson2.Click();
                    }

                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    if (householdCount == 3)
                    {
                        FillStructures myFillStructures = new FillStructures();
                        int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    }
                    if (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes")
                    {
                        IWebElement checkboxPerson3 = driver.FindElement(By.Id("__o3id8"));
                        checkboxPerson3.Click();
                    }
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next"));
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

        public int DoTaxDependants(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                driver.FindElement(By.XPath("/html/body/form")).Click();

                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);

                if (myApplication.myHouseholdOther == "No")
                {
                    DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[5]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[5]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                }
                else if (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    if (householdCount == 3)
                    {
                        FillStructures myFillStructures = new FillStructures();
                        int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        if (myHouseholdMembers.myHasIncome == "Yes" && myHouseholdMembers.myTaxFiler == "Yes")
                        {
                            DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                            driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                        }
                        else
                        {
                            DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[7]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                            driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[7]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                        }
                    }
                    else
                    {
                        if (myHouseholdMembers.myHasIncome == "No" && myHouseholdMembers.myTaxFiler == "No")
                        {
                            DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[7]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                            driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[7]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                        }
                        else
                        {
                            DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                            driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                        }
                        myHouseholdMembers.myPassCount = "2";//update count to 2 to do the dependant screen another time
                        DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                    }
                }
                else  //pass count = 2
                {
                    DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow

                    myHouseholdMembers.myPassCount = "1";//update count back to 1 to continue on to next screens
                    DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                }
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                string dependant = "No";
                if (householdCount == 3)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    if (myHouseholdMembers.myDependants == "Yes")
                    {
                        dependant = "Yes";
                    }
                }
                else if (householdCount == 2)
                {
                    if (myHouseholdMembers.myDependants == "Yes")
                    {
                        dependant = "Yes";
                    }
                }
                else
                {
                    if (myApplication.myDependants == "Yes")
                    {
                        dependant = "Yes";
                    }
                }
                if (dependant == "No")
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                else
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                if (dependant == "Yes")
                {
                    System.Threading.Thread.Sleep(1000);
                    IWebElement checkboxPerson = driver.FindElement(By.Id("__o3id7"));
                    checkboxPerson.Click();
                }
                if (myApplication.myHouseholdOther == "Yes" && dependant == "No" && householdCount != 1)
                {
                    driver.FindElement(By.Id("__o3id8")).SendKeys("No");
                }
                System.Threading.Thread.Sleep(1000);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoHouseholdSummary(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
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

                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoIncomeAbout(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHouseholdMembers.myPassCount = "1";//reset count back to 1
                DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);

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

        public int DoAnyIncome(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                    System.Threading.Thread.Sleep(1000);
                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);

                    if (myApplication.myIncomeYN == "No")
                    {
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    }
                    else
                    {
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    }
                    action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();
                }
                else
                {
                    DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);
                    driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                    System.Threading.Thread.Sleep(1000);
                    OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    string hasIncome = "No";
                    if (householdCount == 3)
                    {
                        FillStructures myFillStructures = new FillStructures();
                        if (myHouseholdMembers.myPassCount == "2")
                        {
                            int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                        }
                        else
                        {
                            int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        }
                    }
                    if (myHouseholdMembers.myHasIncome == "Yes")
                    {
                        hasIncome = "Yes";
                    }

                    if (hasIncome == "No")
                    {
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    }
                    else
                    {
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    }
                    action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoEnterIncomeDetails(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (16 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (20 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);

                String type = "";
                String employer = "";
                String seasonal = "";
                String amount = "";
                String frequency = "";
                String more = "";
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    type = myApplication.myIncomeType;
                    employer = myApplication.myIncomeEmployer;
                    seasonal = myApplication.myIncomeSeasonal;
                    amount = myApplication.myIncomeAmount;
                    frequency = myApplication.myIncomeFrequency;
                    more = myApplication.myIncomeMore;
                }
                else
                {
                    FillStructures myFillStructures = new FillStructures();
                    if (myHouseholdMembers.myPassCount == "2")
                    {
                        int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    }
                    else
                    {
                        int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    }

                    type = myHouseholdMembers.myIncomeType;
                    employer = myHouseholdMembers.myIncomeEmployer;
                    seasonal = myHouseholdMembers.myIncomeSeasonal;
                    amount = myHouseholdMembers.myIncomeAmount;
                    frequency = myHouseholdMembers.myIncomeFrequency;
                    more = myHouseholdMembers.myIncomeMore;
                }

                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[16]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[16]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);

                if (type == "Wages before taxes")
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                else
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[17]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div/input")));
                IWebElement textboxIncomeEmployer = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[17]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxIncomeEmployer.SendKeys(employer);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id9")));
                IWebElement listboxIncomeSeasonal = driver.FindElement(By.Id("__o3id9"));
                listboxIncomeSeasonal.SendKeys(seasonal);

                IWebElement textboxIncomeAmount = driver.FindElement(By.Id("__o3ida"));
                textboxIncomeAmount.SendKeys(amount);

                IWebElement textboxIncomeFrequency = driver.FindElement(By.Id("__o3idc"));
                textboxIncomeFrequency.SendKeys(frequency);

                IWebElement textboxIncomeMore = driver.FindElement(By.Id("__o3idd"));
                textboxIncomeMore.SendKeys(more);
                if (type == "Wages before taxes")
                {
                    IWebElement textboxEmployerEIN = driver.FindElement(By.Id("__o3ide"));
                    textboxEmployerEIN.SendKeys("123456789");
                    IWebElement textboxEmployerStreet1 = driver.FindElement(By.Id("__o3id10"));
                    textboxEmployerStreet1.SendKeys("123 1st Ave");
                    IWebElement textboxEmployerCity = driver.FindElement(By.Id("__o3id12"));
                    textboxEmployerCity.SendKeys("Minneapolis");
                    IWebElement textboxEmployerCounty = driver.FindElement(By.Id("__o3id13"));
                    textboxEmployerCounty.SendKeys("Hennepin");
                    IWebElement textboxEmployerState = driver.FindElement(By.Id("__o3id14"));
                    textboxEmployerState.SendKeys("Minnesota");
                    IWebElement textboxEmployerZip = driver.FindElement(By.Id("__o3id15"));
                    textboxEmployerZip.SendKeys("55418");
                }
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext3 = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext3.Click();

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

        public int DoAdditionalIncomeDetails(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                String incomeReduced = "";
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    incomeReduced = myApplication.myIncomeReduced;
                }
                else
                {
                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    if (householdCount == 3)
                    {
                        FillStructures myFillStructures = new FillStructures();
                        if (myHouseholdMembers.myPassCount == "2")
                        {
                            int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                        }
                        else
                        {
                            int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        }
                    }
                    incomeReduced = myHouseholdMembers.myIncomeReduced;
                }

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
                driver.FindElement(By.XPath("/html/body/form")).Click();

                DoWaitForElement(driver, By.Id("__o3btn.next"), myHistoryInfo);

                if (incomeReduced == "Yes")
                {
                    IWebElement listboxIncomeReduced = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeReduced.SendKeys(incomeReduced);
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
                buttonNext.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }


        public int DoIncomeAdjustments(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);

                String incomeAdjusted = "";
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    incomeAdjusted = myApplication.myIncomeAdjusted;
                }
                else
                {
                    FillStructures myFillStructures = new FillStructures();
                    if (myHouseholdMembers.myPassCount == "2")
                    {
                        int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    }
                    else
                    {
                        int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                    }
                    incomeAdjusted = myHouseholdMembers.myIncomeAdjusted;
                }

                if (incomeAdjusted != "No")
                {
                    IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeAdjusted.SendKeys(incomeAdjusted);
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoAnnualIncome(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);

                String incomeExpected = "";
                if (myApplication.myHouseholdOther == "No")
                {
                    incomeExpected = myApplication.myIncomeExpected;
                }
                else if (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    incomeExpected = myHouseholdMembers.myIncomeExpected;
                    myHouseholdMembers.myPassCount = "2";//update count to 2 to do the income screens another time
                    DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
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
                            int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                            myHouseholdMembers.myPassCount = "3";//update count to 3 to do the income screens another time
                            DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                        }
                        else
                        {
                            int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
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
                else
                {
                    if (DateTime.Now > Convert.ToDateTime("10/31/2016") &&
                        DateTime.Now < Convert.ToDateTime("1/1/2017"))
                    {
                        IWebElement listboxIncomeNextYear = driver.FindElement(By.Id("__o3id8"));
                        listboxIncomeNextYear.SendKeys(incomeExpected);
                    }
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoAdditionalHouseholdInformation(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoSupportingDocument(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/div/div/input"), myHistoryInfo);

                IWebElement textboxAlien = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/div/div/input"));
                textboxAlien.SendKeys("A1234567");

                IWebElement textboxCard = driver.FindElement(By.Id("__o3id7"));
                textboxCard.SendKeys("ABC7778889991");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoAddInfoAPTC(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);

                IWebElement checkboxMilitary;
                if (myApplication.myHouseholdOther == "No" && myApplication.myMilitary == "Yes")
                {
                    checkboxMilitary = driver.FindElement(By.Id("__o3id9"));
                    checkboxMilitary.Click();
                }
                else if (myApplication.myHouseholdOther == "Yes" && myApplication.myMilitary == "Yes")
                {
                    checkboxMilitary = driver.FindElement(By.Id("__o3idc"));//primary
                    checkboxMilitary.Click();
                }
                else if (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myMilitary == "Yes")
                {
                    checkboxMilitary = driver.FindElement(By.Id("__o3idd"));//2nd member
                    checkboxMilitary.Click();
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoEmployerSponsoredCoverage(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/span[2]/span/span"), myHistoryInfo);

                if (myApplication.myESC == "Yes")
                {
                    IWebElement listboxESC;
                    listboxESC = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/div/table/tbody/tr/td/div[2]/div/div[1]/input"));
                    listboxESC.Click();
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/span[2]/span/span"));
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

        public int DoEmployerSponsoredCoverageMore(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);
                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);

                if (myApplication.myESC == "No")
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                else
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoEmployerDetails(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);

                IWebElement textBoxName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textBoxName.SendKeys("Target");

                IWebElement textBoxId = driver.FindElement(By.Id("__o3id7"));
                textBoxId.SendKeys("12345");

                IWebElement textBoxFulltime = driver.FindElement(By.Id("__o3id8"));
                textBoxFulltime.SendKeys("Yes");

                IWebElement listboxAddress1 = driver.FindElement(By.Id("__o3id9"));
                listboxAddress1.SendKeys("1 Main St");

                IWebElement listboxAddress2 = driver.FindElement(By.Id("__o3ida"));
                listboxAddress2.SendKeys("PO Box 1");

                IWebElement listboxAptSuite = driver.FindElement(By.Id("__o3idb"));
                listboxAptSuite.SendKeys("Apt 2");

                IWebElement listboxCity = driver.FindElement(By.Id("__o3idc"));
                listboxCity.SendKeys("Minneapolis");

                IWebElement listboxCounty = driver.FindElement(By.Id("__o3idd"));
                listboxCounty.SendKeys("Hennepin");

                IWebElement listboxState = driver.FindElement(By.Id("__o3ide"));
                listboxState.SendKeys("Minnesota");

                IWebElement listboxZip = driver.FindElement(By.Id("__o3idf"));
                listboxZip.SendKeys("55418");

                IWebElement textboxPhoneNum = driver.FindElement(By.Id("__o3id10"));
                textboxPhoneNum.SendKeys("612");

                IWebElement textboxPhoneNum2 = driver.FindElement(By.Id("__o3id11"));
                textboxPhoneNum2.SendKeys("222");

                IWebElement textboxPhoneNum3 = driver.FindElement(By.Id("__o3id12"));
                textboxPhoneNum3.SendKeys("4444");

                IWebElement textboxEnrolledOn = driver.FindElement(By.Id("__o3id13"));
                textboxEnrolledOn.SendKeys("01/01/2016");

                IWebElement textboxCoverageEnd = driver.FindElement(By.Id("__o3id14"));
                textboxCoverageEnd.SendKeys(myApplication.myCoverageEnd);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoAdditionalInfoUnassistedInsurance(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
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
                DoWaitForElement(driver, By.Id("__o3btn.next"), myHistoryInfo);

                //This will only appear if age < 19
                DateTime birth = Convert.ToDateTime(myApplication.myDOB);
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
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
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
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
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

                if ((myApplication.myHouseholdOther == "No" && householdCount == 1 && age.Year - 1 < 21) //1 hh
                   || (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && (age.Year - 1 < 21 || age2.Year - 1 < 21)) // 2 hh
                   || (myApplication.myHouseholdOther == "Yes" && householdCount == 3 && (age.Year - 1 < 21 || age2.Year - 1 < 21 || age3.Year - 1 < 21)) // 3 hh
                    )
                {
                    IWebElement listboxOutsideHome;
                    if (householdCount == 2 && myHouseholdMembers.myLiveWithYou == "No")
                    {
                        listboxOutsideHome = driver.FindElement(By.Id("__o3ide"));
                        listboxOutsideHome.SendKeys("No");
                    }
                    else
                    {
                        listboxOutsideHome = driver.FindElement(By.Id("__o3ida"));
                        listboxOutsideHome.SendKeys("No");
                    }
                }

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
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

        public int DoOtherInsurance(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

                driver.FindElement(By.XPath("/html/body/form")).Click();
                DoWaitForElement(driver, By.Id("__o3btn.next"), myHistoryInfo);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[2]/input[1]")));
                IWebElement listboxKindIns = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[2]/input[1]"));
                listboxKindIns.SendKeys(myApplication.myKindIns);

                IWebElement outsideClick = driver.FindElement(By.Id("__o3idb"));
                outsideClick.Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3idb")));
                IWebElement listboxCoverageEnd = driver.FindElement(By.Id("__o3idb"));
                listboxCoverageEnd.SendKeys(myApplication.myCoverageEnd);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3idd")));
                IWebElement listboxAddIns = driver.FindElement(By.Id("__o3idd"));
                listboxAddIns.SendKeys(myApplication.myAddIns);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
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

        public int DoAdditionalInformationForAll(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                IWebElement listboxCondition;
                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    listboxCondition = driver.FindElement(By.Id("__o3id8"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxCondition = driver.FindElement(By.Id("__o3id9"));
                }
                else//3 hh
                {
                    listboxCondition = driver.FindElement(By.Id("__o3ida"));
                }
                listboxCondition.SendKeys("No");

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td[1]/span[1]")));

                IWebElement listboxNative;
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    listboxNative = driver.FindElement(By.Id("__o3ida"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
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
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    if (myHouseholdMembers.myRace == "Indian")
                    {
                        indian = "Yes";
                        indianMember = 2;
                    }
                }
                else if (householdCount == 3)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    if (myHouseholdMembers.myRace == "Indian")
                    {
                        indian = "Yes";
                        indianMember = 2;
                    }
                    else
                    {
                        myFillStructures = new FillStructures();
                        result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        if (myHouseholdMembers.myRace == "Indian")
                        {
                            indian = "Yes";
                            indianMember = 3;
                        }
                    }
                }
                else //1 hh
                {
                    if (myApplication.myRace == "Indian")
                    {
                        indian = "Yes";
                        indianMember = 1;
                    }
                }

                if (indian == "Yes")
                {
                    listboxNative.SendKeys("Yes");
                    listboxCondition.Click();
                    System.Threading.Thread.Sleep(1000);
                    IWebElement listboxNativePerson;
                    if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                    {
                        listboxNativePerson = driver.FindElement(By.Id("__o3idb"));
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
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
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    listboxVisitMN = driver.FindElement(By.Id("__o3idc"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxVisitMN = driver.FindElement(By.Id("__o3idf"));
                }
                else//3 hh
                {
                    listboxVisitMN = driver.FindElement(By.Id("__o3id12"));
                }
                listboxVisitMN.SendKeys("No");

                DateTime birth = Convert.ToDateTime(myApplication.myDOB);
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
                if (myApplication.myHouseholdOther == "Yes")
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
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
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
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
                if (myApplication.myHouseholdOther == "Yes" && householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxChildActiveDuty = driver.FindElement(By.Id("__o3id16"));
                    listboxChildActiveDuty.SendKeys("No");

                    listboxChildCourtOrder = driver.FindElement(By.Id("__o3id18"));
                    listboxChildCourtOrder.SendKeys("No");
                }
                else if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxChildActiveDuty = driver.FindElement(By.Id("__o3ide"));
                    listboxChildActiveDuty.SendKeys("No");

                    listboxChildCourtOrder = driver.FindElement(By.Id("__o3id10"));
                    listboxChildCourtOrder.SendKeys("No");
                }
                else if (householdCount == 2 && age2.Year - 1 < 19)
                {
                    listboxChildActiveDuty = driver.FindElement(By.Id("__o3id12"));
                    listboxChildActiveDuty.SendKeys("No");

                    listboxChildCourtOrder = driver.FindElement(By.Id("__o3id14"));
                    listboxChildCourtOrder.SendKeys("No");
                }

                IWebElement listboxLongTermCare;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id12"));
                }
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3ide"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
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
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id10")));
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id10"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id15")));
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id15"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 19)//2 hh
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id19"));
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
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id12"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id18"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 19)//2 hh
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id1c"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id22"));
                }
                else
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id1e"));
                }

                listboxHaveMedicare.SendKeys(myApplication.myOtherIns);
                listboxResidentialTreatment.Click();
                System.Threading.Thread.Sleep(1000);

                if (myApplication.myOtherIns == "Yes")
                {
                    IWebElement listboxMedicarePerson = driver.FindElement(By.Id("__o3id13"));
                    listboxMedicarePerson.Click();
                }

                IWebElement listboxTorture;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id18"));
                }
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id14"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id1b"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 19)//2 hh
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id1f"));
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
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id16"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id1e"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 19)//2 hh
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id22"));
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
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id18"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id21"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 19)//2 hh
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id25"));
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
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id1a")));
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id1a"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id24")));
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id24"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 19)//2 hh
                {
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id28"));
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

                string isMA = DoIsInTypeRange(myApplication, myHouseholdMembers, myHistoryInfo, "MA");
                string isBHP = DoIsInTypeRange(myApplication, myHouseholdMembers, myHistoryInfo, "BHP");
                string isQHP = DoIsInTypeRange(myApplication, myHouseholdMembers, myHistoryInfo, "QHP");

                if (myApplication.myHomeState == "Minnesota" &&
                    (((householdCount == 1 || householdCount == 2) && (isMA == "True" || isBHP == "True")) //bd bhp1, ma1, bhp2, ma2
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && isQHP == "True" && (age.Year - 1 < 21 || age2.Year - 1 < 21)) // 2 hh
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 3 && (isMA == "True" || isBHP == "True") && (age.Year - 1 < 21 || age2.Year - 1 < 21 || age3.Year - 1 < 21)) // 3 hh
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 3 && isQHP == "True" && (age.Year - 1 < 21 || age2.Year - 1 < 21 || age3.Year - 1 < 21)) // 3 hh
                    ))
                {
                    IWebElement listboxMedicareInjury;
                    if (householdCount == 1 && age.Year - 1 < 19)
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id20"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (householdCount == 1 || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                        || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id1c"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id27"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 19)//2 hh
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id2b"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (householdCount == 3 && ((age3.Year - 1 > 11 && age3.Year - 1 < 19) || age3.Year - 1 == 0))
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id36"));
                        listboxMedicareInjury.SendKeys("No");
                    }
                    else if (householdCount == 3 && age3.Year - 1 < 12 && isBHP == "True") //bhp10, 10 yr only not qhp
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id3a"));
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
                    else if (householdCount == 1 || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                        || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPersonHighlighted == "No"))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 > 18)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2a"));
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 12 && myApplication.myEnrollmentPlanType == "MN Care QHP")//2 hh, renewal 21
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2d"));
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && age2.Year - 1 < 19)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2e"));
                    }
                    else if (householdCount == 3 && ((age3.Year - 1 > 11 && age3.Year - 1 < 15) || age3.Year - 1 == 0 ))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id38"));
                    }
                    else if (indian == "Yes" || (householdCount == 3 && age3.Year - 1 < 12) || (householdCount == 3 && age3.Year - 1 == 20))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id36"));
                    }
                    else if (householdCount == 3 && age3.Year - 1 > 14 && age3.Year - 1 < 19)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id3a"));
                    }
                    else
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id34"));
                    }
                    listboxMAStartDate.SendKeys("No");
                }
                else
                {
                    IWebElement listboxMAStartDate;
                    if (householdCount == 1 && age.Year - 1 < 19)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id22"));
                    }
                    else if (householdCount == 1 || (householdCount == 2 && myApplication.myApplyYourself == "No"))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1c"));
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));//when is this hit
                    }
                    else if (householdCount == 2)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id27"));//2hh married couple qhp
                    }
                    else if (householdCount == 2)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2a")); //when is this hit
                    }
                    else if (householdCount == 3 && age3.Year - 1 < 19)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id36"));
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

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
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

        public int DoAddInfoIndian(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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
                IWebElement listboxFederalTribe;
                System.Threading.Thread.Sleep(appwait);
                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[3]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[2]/input[1]"), myHistoryInfo);
                listboxFederalTribe = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[3]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[2]/input[1]"));

                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                string federalTribe = "No";
                string tribeId = "";
                string liveRes = "No";
                string tribeName = "";
                if (householdCount == 2)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    if (myHouseholdMembers.myFederalTribe == "Yes")
                    {
                        federalTribe = "Yes";
                    }
                    tribeId = myHouseholdMembers.myTribeId;
                    liveRes = myHouseholdMembers.myLiveRes;
                    tribeName = myHouseholdMembers.myTribeName;

                }
                else if (householdCount == 3)
                {
                    FillStructures myFillStructures = new FillStructures();
                    int result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "2");
                    if (myHouseholdMembers.myFederalTribe == "Yes")
                    {
                        federalTribe = "Yes";
                    }
                    else
                    {
                        myFillStructures = new FillStructures();
                        result = myFillStructures.doFillNextHMStructures(ref myApplication, ref myHouseholdMembers, ref myHistoryInfo, "3");
                        if (myHouseholdMembers.myFederalTribe == "Yes")
                        {
                            federalTribe = "Yes";
                        }
                    }
                    tribeId = myHouseholdMembers.myTribeId;
                    liveRes = myHouseholdMembers.myLiveRes;
                    tribeName = myHouseholdMembers.myTribeName;
                }
                else //1 hh
                {
                    if (myApplication.myFederalTribe == "Yes")
                    {
                        federalTribe = "Yes";
                    }
                    tribeId = myApplication.myTribeId;
                    liveRes = myApplication.myLiveRes;
                    tribeName = myApplication.myTribeName;
                }

                listboxFederalTribe.SendKeys(federalTribe);
                listboxFederalTribe.Click();
                IWebElement outsideClick = driver.FindElement(By.Id("__o3ida"));
                outsideClick.Click();
                listboxFederalTribe.Click();

                if (federalTribe == "Yes")
                {
                    IWebElement listboxTribeName = driver.FindElement(By.Id("__o3id7"));
                    listboxTribeName.SendKeys(tribeName);

                    IWebElement listboxLiveRes = driver.FindElement(By.Id("__o3id8"));
                    listboxLiveRes.SendKeys(liveRes);
                }

                IWebElement listboxTribeId = driver.FindElement(By.Id("__o3ida"));
                listboxTribeId.SendKeys(tribeId);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoAddInfoMilitary(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td/div/div[3]/input[1]"), myHistoryInfo);
                IWebElement datepickerMilitaryDate = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td/div/div[3]/input[1]"));
                if (myApplication.myMilitary == "Yes")
                {
                    datepickerMilitaryDate.SendKeys(myApplication.myMilitaryDate);
                }
                else
                {
                    datepickerMilitaryDate.SendKeys(myHouseholdMembers.myMilitaryDate);
                }
                datepickerMilitaryDate.Click();

                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/h3"));
                outsideClick.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoSummary(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

                if (myApplication.myHouseholdOther != "No")
                {
                    myHouseholdMembers.myPassCount = "1";//switch count back to 1 to reset and be ready for next run
                    DoUpdateHMPassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                }

                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo); ;

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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

        public int DoSummaryWithoutDiscounts(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (12 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (14 + myHistoryInfo.myAppWait) * 1000;//norm 12
                }
                System.Threading.Thread.Sleep(appwait);

                DoWaitForElement(driver, By.Id("__o3btn.next_label"), myHistoryInfo);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
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


        public int DoSignature(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (8 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myBrowser == "Chrome")
                    {
                        appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (14 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                System.Threading.Thread.Sleep(appwait);

                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]"), myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                IWebElement checkboxRenewCov;
                if (myApplication.myRenewalCov == "0")
                {
                    checkboxRenewCov = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[3]/td/div/fieldset/div/div[6]/div/input"));
                    checkboxRenewCov.Click();
                }
                else if (myApplication.myRenewalCov == "1")
                {
                    checkboxRenewCov = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[3]/td/div/fieldset/div/div[5]/div/input"));
                    checkboxRenewCov.Click();
                }
                else if (myApplication.myRenewalCov == "2")
                {
                    checkboxRenewCov = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[3]/td/div/fieldset/div/div[4]/div/input"));
                    checkboxRenewCov.Click();
                }
                else if (myApplication.myRenewalCov == "3")
                {
                    checkboxRenewCov = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[3]/td/div/fieldset/div/div[3]/div/input"));
                    checkboxRenewCov.Click();
                }
                else if (myApplication.myRenewalCov == "4")
                {
                    checkboxRenewCov = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[3]/td/div/fieldset/div/div[2]/div/input"));
                    checkboxRenewCov.Click();
                }

                IWebElement checkboxIAgreeNoticeRR = driver.FindElement(By.Id("__o3ida"));
                checkboxIAgreeNoticeRR.Click();

                IWebElement checkboxIAgreeInfoApplication = driver.FindElement(By.Id("__o3idb"));
                checkboxIAgreeInfoApplication.Click();

                IWebElement checkboxIDeclare = driver.FindElement(By.Id("__o3idc"));
                checkboxIDeclare.Click();

                IWebElement checkboxIAgreeStatementsBelow = driver.FindElement(By.Id("__o3idd"));
                checkboxIAgreeStatementsBelow.Click();

                IWebElement textboxFirstName = driver.FindElement(By.Id("__o3ide"));
                textboxFirstName.SendKeys(myApplication.myFirstName);

                IWebElement textboxMiddleName;
                if (myApplication.myMiddleName != null)
                {
                    textboxMiddleName = driver.FindElement(By.Id("__o3idf"));
                    textboxMiddleName.SendKeys(myApplication.myMiddleName);
                }

                IWebElement textboxLastName;
                if (myApplication.myWithDiscounts == "Yes")
                {
                    textboxLastName = driver.FindElement(By.Id("__o3id10"));
                }
                else
                {
                    textboxLastName = driver.FindElement(By.Id("__o3idd"));
                }
                textboxLastName.SendKeys(myApplication.myLastName);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoSignatureWithoutDiscounts(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

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

                DoWaitForElement(driver, By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input"), myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select arrow

                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                IWebElement checkboxAppChanges = driver.FindElement(By.Id("__o3id9"));
                checkboxAppChanges.Click();

                IWebElement checkboxPerjury = driver.FindElement(By.Id("__o3ida"));
                checkboxPerjury.Click();

                IWebElement textboxFirstName = driver.FindElement(By.Id("__o3idb"));
                textboxFirstName.SendKeys(myApplication.myFirstName);

                IWebElement textboxMiddleName = driver.FindElement(By.Id("__o3idc"));
                textboxMiddleName.SendKeys(myApplication.myMiddleName);

                IWebElement textboxLastName = driver.FindElement(By.Id("__o3idd"));
                textboxLastName.SendKeys(myApplication.myLastName);

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoUpdateHMPassCount(mystructHistoryInfo myHistoryInfo, string updateValue)
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
                        myUpdateString = "Update HouseMembers set PassCount = @Passcount where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com2 = new SqlCeCommand(myUpdateString, con))
                        {
                            com2.Parameters.AddWithValue("PassCount", updateValue);
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

        public int DoUpdateAppPassCount(mystructHistoryInfo myHistoryInfo, string updateValue)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;


            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com3 = new SqlCeCommand(
                    "SELECT * FROM Application where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com3.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Application set PassCount = @Passcount where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com4 = new SqlCeCommand(myUpdateString, con))
                        {
                            com4.Parameters.AddWithValue("PassCount", updateValue);
                            com4.ExecuteNonQuery();
                            com4.Dispose();
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

        public String DoWaitForElement(IWebDriver driver, By selector, mystructHistoryInfo myHistoryInfo)
        {
            int wait = 50000;
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
                    return "true";
                }
                int appwait;
                appwait = (1 + myHistoryInfo.myAppWait) * 1000;
                System.Threading.Thread.Sleep(appwait);
            }
            return "false";
        }

        public String DoIsInTypeRange(mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, String type)
        {
            HouseholdMembersDo myHousehold = new HouseholdMembersDo();
            int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
            int temp1 = myHousehold.DoHouseholdTotalIncome(myEnrollment, myHouseholdMembers, myHistoryInfo);
            int temp2 = myHousehold.DoHouseholdTotalIncomeUnrelatedTo(myEnrollment, myHouseholdMembers, myHistoryInfo);

            if (type == "MA")
            {
                if (myEnrollment.myHouseholdOther == "No" && householdCount == 1)
                {
                    if (temp1 < 16514 || (temp2 != 0 && temp2 < 16514))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)
                {
                    if (temp1 < 22268 || (temp2 != 0 && temp2 < 22268))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
                else
                {
                    if (temp1 < 28023 || (temp2 != 0 && temp2 < 28023))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
            }
            else if (type == "BHP")
            {
                if (myEnrollment.myHouseholdOther == "No" && householdCount == 1)
                {
                    if ((temp1 > 16513 && temp1 < 23760) || (temp2 != 0 && temp2 > 16513 && temp2 < 23760))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)
                {
                    if ((temp1 > 22267 && temp1 < 32040) || (temp2 != 0 && temp2 > 22267 && temp2 < 32040))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
                else
                {
                    if ((temp1 > 28022 && temp1 < 40320) || (temp2 != 0 && temp2 > 28022 && temp2 < 40320))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }

            }
            else if (type == "QHP")
            {
                if (myEnrollment.myHouseholdOther == "No" && householdCount == 1)
                {
                    if ((temp1 > 23759 && temp1 < 47520) || (temp2 != 0 && temp2 > 23759 && temp2 < 47520))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)
                {
                    if ((temp1 > 32039 && temp1 < 64080) || (temp2 != 0 && temp2 > 32039 && temp2 < 64080))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
                else
                {
                    if ((temp1 > 40319 && temp1 < 80640) || (temp2 != 0 && temp2 > 40319 && temp2 < 80640))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }

            }
            else
            {
                if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 3)
                {
                    if (temp1 > 80639 || (temp2 != 0 && temp2 > 80639))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
                else if (myEnrollment.myHouseholdOther == "Yes" && householdCount == 2)
                {
                    if (temp1 > 64079 || (temp2 != 0 && temp2 > 64079))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }
                else
                {
                    if (temp1 > 47519 || (temp2 != 0 && temp2 > 47519))
                    {
                        return "True";
                    }
                    else
                    {
                        return "False";
                    }
                }


            }
        }

        public int DoSaveExit(IWebDriver driver, IWebDriver driver5, mystructAccountCreate myAccountCreate, mystructApplication myApplication,
            mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            IWebDriver myDriver = driver;

            try
            {
                if (myHistoryInfo.myAssisterNavigator == "Yes")
                {
                    myDriver = driver5;
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
                DoWaitForElement(myDriver, By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/MNHIXCitizenWorkspace_setupMotivationResolverPage.do')]"), myHistoryInfo);
                var iFrameElement = myDriver.FindElement(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/MNHIXCitizenWorkspace_setupMotivationResolverPage.do')]"));
                myDriver.SwitchTo().Frame(iFrameElement);

                writeLogs.DoGetScreenshot(myDriver, ref myHistoryInfo);

                IWebElement buttonExit = myDriver.FindElement(By.Id("__o3btn.save_and_exit_label"));
                buttonExit.Click();
                System.Threading.Thread.Sleep(6000);
                myHistoryInfo.myAssisterNavigator = "No";

                myDriver.SwitchTo().DefaultContent();
                DoWaitForElement(myDriver, By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/MNHIXCitizenWorkspace_setupMotivationResolverPage.do')]"), myHistoryInfo);
                var iFrameElement2 = myDriver.FindElement(By.XPath("//iframe[contains(@src,'/CitizenPortal/en_US/MNHIXCitizenWorkspace_setupMotivationResolverPage.do')]"));
                myDriver.SwitchTo().Frame(iFrameElement2);

                myDriver.FindElement(By.XPath("/html/body/form/div/div[2]/div[3]/div/div/fieldset/div[1]/div[1]/input")).Click();
                myDriver.FindElement(By.Id("__o3btn.PLAYER_NEXT_label")).Click();

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

        // TFR 12-08-2016 Element ID __o3btn.next_label is used by several pages and it has been found that during a process of rendering a page we can get either two instances 
        // of this element or a stale element exception can occur.  This work around was used in DoHouseholdAbout with success so I am making a generic version of the code
        // This method either throws an exception which will be caught in the try/catch of the caller or simply returns if successful.
        public void DoWaitForPossibleStaleElement(IWebDriver driver, By selector, mystructHistoryInfo myHistoryInfo)
        {
            bool isNotdone = true;
            int staleRetries = 0;
            int duplicateElementRetries = 0;
            const int MAX_RETRIES = 180;

            do
            {
                try
                {
                    var numElements = driver.FindElements(By.Id("__o3btn.next_label"));
                    if (numElements.Count > 1)
                    {
                        if (duplicateElementRetries < MAX_RETRIES)
                        {
                            Thread.Sleep(1000);
                            duplicateElementRetries++;
                            continue;
                        }
                        else
                        {
                            throw new System.Exception("More than one element with the same name - Stale element retry");
                        }
                    }
                    isNotdone = false;
                }
                catch (StaleElementReferenceException e)
                {
                    if (staleRetries < MAX_RETRIES)
                    {
                        Thread.Sleep(1000);
                        staleRetries++;
                        continue;
                    }
                    else
                    {
                        throw e;
                    }
                }
            } while (isNotdone);
        }


    }
}
