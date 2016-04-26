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
using OpenQA.Selenium.Support.UI; // for dropdown


using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace MNsure_Regression_1
{
    class ApplicationDo
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoGettingStarted(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iFrame")));
                var iFrameElement = driver.FindElement(By.TagName("iFrame"));
                driver.SwitchTo().Frame(iFrameElement);

                //wait for link
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/div[1]/span[1]/b"))));
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //Check the I agree box
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id6")));
                IWebElement checkboxAgree = driver.FindElement(By.Id("__o3id6"));
                checkboxAgree.Click();

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                //Click the Next button
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3btn.next_label")));
                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHouseholdMembers.myPassCount = "1";//reset count back to 1 on start in case an error happened during previous run
                DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);

                Enrollments myEnrollment = new Enrollments();
                myHouseholdMembers.myReEnroll = "No"; //reset reenroll on start in case an error happened during previous run
                myEnrollment.DoUpdateReEnroll(myHistoryInfo, myHouseholdMembers.myReEnroll);

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

        public int DoApplicantDetailsAbout(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next")));

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

        public int DoApplicantDetails(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input")));

                IWebElement textboxFirstName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxFirstName.SendKeys(myApplication.myFirstName);

                IWebElement textboxMiddleName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[4]/table/tbody/tr/td[1]/div/div/input"));
                if (myApplication.myMiddleName != null)
                {
                    textboxMiddleName.SendKeys(myApplication.myMiddleName);
                }
                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id8"));
                textboxLastName.SendKeys(myApplication.myLastName);

                IWebElement textboxSuffix = driver.FindElement(By.Id("__o3id9"));
                if (myApplication.mySuffix != null)
                {
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
                listboxLiveMN.Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[1]/td[1]/span[1]")));
                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[1]/td[1]/span[1]"));
                outsideClick.Click();

                if (myApplication.myLiveMN == "Yes")
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3ide")));
                    IWebElement listboxHomeless = driver.FindElement(By.Id("__o3ide"));
                    listboxHomeless.SendKeys(myApplication.myHomeless);
                    listboxHomeless.Click();

                    outsideClick.Click();
                }

                if (myApplication.myHomeless == "No")
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id11")));
                    IWebElement listboxAddress1 = driver.FindElement(By.Id("__o3id11"));
                    listboxAddress1.SendKeys(myApplication.myHomeAddress1);

                    if (myApplication.myHomeAddress2 != null)
                    {
                        IWebElement listboxAddress2 = driver.FindElement(By.Id("__o3id12"));
                        listboxAddress2.SendKeys(myApplication.myHomeAddress2);
                    }
                    if (myApplication.myHomeAptSuite != null)
                    {
                        IWebElement listboxAptSuite = driver.FindElement(By.Id("__o3id13"));
                        listboxAptSuite.SendKeys(myApplication.myHomeAptSuite);
                    }
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
                    IWebElement element2 = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iframe")));
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

                IWebElement listboxPreferedContact = driver.FindElement(By.Id("__o3id23"));
                listboxPreferedContact.SendKeys(myApplication.myPrefContact);

                string mysPhone1 = myApplication.myPhoneNum.Substring(0, 3);
                string mysPhone2 = myApplication.myPhoneNum.Substring(3, 3);
                string mysPhone3 = myApplication.myPhoneNum.Substring(6, 4);
                IWebElement textboxPhoneNum = driver.FindElement(By.Id("__o3id24"));
                textboxPhoneNum.SendKeys(mysPhone1);
                IWebElement textboxPhoneNum2 = driver.FindElement(By.Id("__o3id25"));
                textboxPhoneNum2.SendKeys(mysPhone2);
                IWebElement textboxPhoneNum3 = driver.FindElement(By.Id("__o3id26"));
                textboxPhoneNum3.SendKeys(mysPhone3);

                IWebElement listboxPhoneType = driver.FindElement(By.Id("__o3id27"));
                listboxPhoneType.SendKeys(myApplication.myPhoneType);

                string mysAPhone1 = myApplication.myAltNum.Substring(0, 3);
                string mysAPhone2 = myApplication.myAltNum.Substring(3, 3);
                string mysAPhone3 = myApplication.myAltNum.Substring(6, 4);
                IWebElement textboxAPhoneNum = driver.FindElement(By.Id("__o3id28"));
                textboxAPhoneNum.SendKeys(mysAPhone1);
                IWebElement textboxAPhoneNum2 = driver.FindElement(By.Id("__o3id29"));
                textboxAPhoneNum2.SendKeys(mysAPhone2);
                IWebElement textboxAPhoneNum3 = driver.FindElement(By.Id("__o3id2a"));
                textboxAPhoneNum3.SendKeys(mysAPhone3);

                IWebElement listboxAPhoneType = driver.FindElement(By.Id("__o3id2b"));
                listboxAPhoneType.SendKeys(myApplication.myAltNumType);

                IWebElement textboxEmail = driver.FindElement(By.Id("__o3id2c"));
                textboxEmail.SendKeys(myApplication.myEmail);

                IWebElement listboxVoterCard = driver.FindElement(By.Id("__o3id32"));
                listboxVoterCard.SendKeys(myApplication.myVoterCard);

                IWebElement listboxNotices = driver.FindElement(By.Id("__o3id33"));
                listboxNotices.SendKeys(myApplication.myNotices);

                IWebElement listboxAuthRep = driver.FindElement(By.Id("__o3id34"));
                listboxAuthRep.SendKeys(myApplication.myAuthRep);

                IWebElement listboxApplyYouself = driver.FindElement(By.Id("__o3id35"));
                listboxApplyYouself.SendKeys(myApplication.myApplyYourself);

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

        public int DoApplicantDetailsWithoutDiscounts(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input")));

                IWebElement textboxFirstName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxFirstName.SendKeys(myApplication.myFirstName);

                IWebElement textboxMiddleName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr[1]/td[4]/table/tbody/tr/td[1]/div/div/input"));
                textboxMiddleName.SendKeys(myApplication.myMiddleName);

                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id8"));
                textboxLastName.SendKeys(myApplication.myLastName);

                IWebElement textboxSuffix = driver.FindElement(By.Id("__o3id9"));
                textboxSuffix.SendKeys(myApplication.mySuffix);

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
                listboxLiveMN.Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input")));
                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"));
                outsideClick.Click();

                if (myApplication.myLiveMN == "Yes")
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3ide")));
                    IWebElement listboxHomeless = driver.FindElement(By.Id("__o3ide"));
                    listboxHomeless.SendKeys(myApplication.myHomeless);
                    outsideClick.Click();
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
                    IWebElement element2 = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.TagName("iframe")));
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
                outsideClick.Click();

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
                listboxHispanic.Click();

                if (myApplication.mySSN == "Yes")
                {
                    IWebElement listboxSSNNum = driver.FindElement(By.Id("__o3id3a"));
                    listboxSSNNum.SendKeys(myApplication.mySSNNum);
                    listboxSSNNum.Click();
                }
                else
                {
                    IWebElement listboxAppliedSSN = driver.FindElement(By.Id("__o3id3b"));
                    listboxAppliedSSN.SendKeys(myApplication.myAppliedSSN);
                    listboxSSN.Click();
                    if (myApplication.myAppliedSSN == "No")
                    {
                        IWebElement listboxWhyNoSSN = driver.FindElement(By.Id("__o3id3c"));
                        listboxWhyNoSSN.SendKeys(myApplication.myWhyNoSSN);
                        listboxSSN.Click();
                    }

                    if (myApplication.myWhyNoSSN == "Other")
                    {
                        IWebElement listboxAssistSSN = driver.FindElement(By.Id("__o3id3d"));
                        listboxAssistSSN.SendKeys(myApplication.myAssistSSN);
                        listboxSSN.Click();
                    }

                }

                IWebElement listboxApplyYouself = driver.FindElement(By.Id("__o3id3e"));
                listboxApplyYouself.SendKeys(myApplication.myApplyYourself);
                listboxSSN.Click();

                IWebElement listboxCitizen = driver.FindElement(By.Id("__o3id3f"));
                listboxCitizen.SendKeys(myApplication.myCitizen);
                listboxApplyYouself.Click();

                IWebElement listboxInJail = driver.FindElement(By.Id("__o3id45"));
                listboxInJail.SendKeys("No");

                IWebElement listboxMedicalIns = driver.FindElement(By.Id("__o3id47"));
                listboxMedicalIns.SendKeys("No");

                IWebElement listboxUseTobacco = driver.FindElement(By.Id("__o3id49"));
                listboxUseTobacco.SendKeys("No");

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

                IWebElement textboxEmail = driver.FindElement(By.Id("__o3id58"));
                textboxEmail.SendKeys(myApplication.myEmail);

                //  These values default to English, so not needed to interact, leaving code in case
                //  need to change languages.
                //            IWebElement listboxLangaugeMost = driver.FindElement(By.Id("__o3id2d"));
                //                var selectlistboxLangaugeMost = new SelectElement(listboxLangaugeMost);
                //                selectlistboxLangaugeMost.SelectByValue(myApplication.myLanguageMost);
                //    listboxLangaugeMost.SendKeys(myApplication.myLanguageMost);

                //            IWebElement listboxWrittenLangauge = driver.FindElement(By.Id("__o3id30"));
                //            var selectlistboxWrittenLangauge = new SelectElement(listboxWrittenLangauge);
                //            selectlistboxLangaugeMost.SelectByValue(myApplication.myLanguageMost);
                // listboxWrittenLangauge.SendKeys(myApplication.myLanguageWritten);

                IWebElement listboxVoterCard = driver.FindElement(By.Id("__o3id5e"));
                listboxVoterCard.SendKeys(myApplication.myVoterCard);

                IWebElement listboxNotices = driver.FindElement(By.Id("__o3id5f"));
                listboxNotices.SendKeys(myApplication.myNotices);

                /*IWebElement listboxAuthRep = driver.FindElement(By.Id("__o3id34"));
                listboxAuthRep.SendKeys(myApplication.myAuthRep);*/

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

        public int DoApplicantDetailsPrimary(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(4000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input")));

                IWebElement textboxFirstName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxFirstName.SendKeys(myHouseholdMembers.myFirstName);

                IWebElement textboxMiddleName = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr[1]/td[4]/table/tbody/tr/td[1]/div/div/input"));
                textboxMiddleName.SendKeys(myHouseholdMembers.myMiddleName);

                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id8"));
                textboxLastName.SendKeys(myHouseholdMembers.myLastName);

                IWebElement textboxSuffix = driver.FindElement(By.Id("__o3id9"));
                textboxSuffix.SendKeys(myHouseholdMembers.mySuffix);

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

                IWebElement textboxEmail = driver.FindElement(By.Id("__o3id24"));
                textboxEmail.SendKeys(myHouseholdMembers.myEmail);

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

        public int DoMoreAboutYou(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));
                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);

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

                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[6]/table/tbody/tr/td[1]"));
                outsideClick.Click();

                if (myApplication.myRace == "Indian")
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
                listboxSSN.SendKeys(myApplication.mySSN);
                outsideClick.Click();

                if (myApplication.mySSN == "Yes")
                {
                    IWebElement listboxSSNNum = driver.FindElement(By.Id("__o3id1d"));
                    listboxSSNNum.SendKeys(myApplication.mySSNNum);
                }
                else
                {
                    IWebElement listboxAppliedSSN = driver.FindElement(By.Id("__o3id1e"));
                    listboxAppliedSSN.SendKeys(myApplication.myAppliedSSN);
                    outsideClick.Click();
                    if (myApplication.myAppliedSSN == "No")
                    {
                        IWebElement listboxWhyNoSSN = driver.FindElement(By.Id("__o3id1f"));
                        listboxWhyNoSSN.SendKeys(myApplication.myWhyNoSSN);
                        outsideClick.Click();
                    }

                    if (myApplication.myWhyNoSSN == "Other")
                    {
                        IWebElement listboxAssistSSN = driver.FindElement(By.Id("__o3id20"));
                        listboxAssistSSN.SendKeys(myApplication.myAssistSSN);
                        outsideClick.Click();
                    }
                }
                outsideClick.Click();

                IWebElement listboxCitizen = driver.FindElement(By.Id("__o3id21"));
                listboxCitizen.SendKeys(myApplication.myCitizen);
                outsideClick.Click();

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
                    if (myApplication.myGender == "Female")
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
                    /*if (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No") //this is not the correct logic, something else triggers this, foster care maybe
                    {
                        listboxPregnant = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[23]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[1]/input"));
                    }
                    else
                    {*/
                        listboxPregnant = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[22]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[1]/input"));
                    //}
                    listboxPregnant.Click();
                    OpenQA.Selenium.Interactions.Actions action2 = new OpenQA.Selenium.Interactions.Actions(driver);
                    if (isPregnant == "No")
                    {
                        action2.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action2.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    }
                    else
                    {
                        action2.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action2.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                        action2.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    }
                    //action2.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();
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
                    driver.FindElement(By.Id("__o3id20")).SendKeys(children);
                    driver.FindElement(By.Id("__o3id20")).SendKeys(dueDate);
                    driver.FindElement(By.Id("__o3id20")).SendKeys(pregnancyEnded);
                }

                //This will only appear if age 18-27
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
                /* DateTime age3 = DateTime.MinValue;
                 HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                 int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                 if (householdCount == 3)
                 {
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
                 }*/

                string fosterCare = "No";
                if (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                {
                    if (age2.Year - 1 > 17 && age2.Year - 1 < 26) //2 hh
                    {
                        fosterCare = "Yes";
                    }
                }
                /*else if (myApplication.myHouseholdOther == "Yes" && householdCount == 3) //3 hh, is this needed?
                { 

                }*/
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
                    if (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    {
                        listboxFosterCare = driver.FindElement(By.Id("__o3id30"));
                    }
                    else
                    {
                        listboxFosterCare = driver.FindElement(By.Id("__o3id2f"));
                    }
                    listboxFosterCare.SendKeys(myApplication.myFosterCare);
                    System.Threading.Thread.Sleep(1000);
                    //outsideClick.Click();
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

        public int DoMoreSSN(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(4000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div/input")));

                IWebElement textboxSSN = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxSSN.SendKeys(myApplication.mySSNNum);

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

        public int DoHouseholdAbout(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (36 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (22 + myHistoryInfo.myAppWait) * 1000;//norm 6
                }
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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

        public int DoHouseholdMembers(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));

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

        public int DoHouseholdMembersWithoutDiscounts(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (20 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (14 + myHistoryInfo.myAppWait) * 1000;//norm 10
                }
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));

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

        public int DoTaxFiler(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);

                if (myApplication.myHouseholdOther == "No")
                {
                    IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td/div[2]/div/div[1]/input")));

                    IWebElement checkboxPerson = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td/div[2]/div/div[1]/input"));
                    checkboxPerson.Click();
                }
                else
                {
                    IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td[1]/div[2]/div/div[1]/input")));

                    IWebElement checkboxPerson = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td[1]/div[2]/div/div[1]/input"));
                    checkboxPerson.Click();

                    IWebElement checkboxPerson2 = driver.FindElement(By.Id("__o3id7"));
                    checkboxPerson2.Click();

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

        public int DoTaxDependants(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait = (2 + myHistoryInfo.myAppWait) * 1000;//could go up to 6
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);

                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);

                if (myApplication.myHouseholdOther == "No")
                {
                    IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[5]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[1]/input")));
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
                            IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")));
                            driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                        }
                        else
                        {
                            IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[7]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")));
                            driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[7]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                        }
                    }
                    else
                    {
                        IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")));
                        driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow

                        myHouseholdMembers.myPassCount = "2";//update count to 2 to do the dependant screen another time
                        DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                    }
                }
                else  //pass count = 2
                {
                    IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")));
                    driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[4]/table/tbody/tr[1]/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow

                    myHouseholdMembers.myPassCount = "1";//update count back to 1 to continue on to next screens
                    DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
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
                if (dependant == "No" && householdCount != 1)
                {
                    driver.FindElement(By.Id("__o3id8")).SendKeys("No");
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

        public int DoHouseholdSummary(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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

        public int DoIncomeAbout(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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

        public int DoAnyIncome(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myApplication.myHouseholdOther == "Yes" && (myHouseholdMembers.myPassCount == "2" || myHouseholdMembers.myPassCount == "3"))
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (2 + myHistoryInfo.myAppWait) * 1000;
                    }
                }
                else
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (70 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (25 + myHistoryInfo.myAppWait) * 1000;
                    }
                }

                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);

                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1"))
                {
                    IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));
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
                    IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));
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

        public int DoEnterIncomeDetails(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
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

                int appwait;

                if (myApplication.myHouseholdOther == "No")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (25 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (18 + myHistoryInfo.myAppWait) * 1000; //1 hh
                    }
                }
                else if (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")//&& myHouseholdMembers.myHasIncome == "No")
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (115 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (24 + myHistoryInfo.myAppWait) * 1000;//norm 8
                    }
                }
                else if (myApplication.myHouseholdOther == "Yes" && (myHouseholdMembers.myHasIncome == "Yes" || myApplication.myIncomeYN == "Yes"))
                {
                    if (myHistoryInfo.myInTimeTravel == "Yes")
                    {
                        appwait = (155 + myHistoryInfo.myAppWait) * 1000;
                    }
                    else
                    {
                        appwait = (20 + myHistoryInfo.myAppWait) * 1000;//was 16
                    }
                }
                else if (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "3" && myHouseholdMembers.myHasIncome == "Yes")
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
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[16]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));

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

        public int DoAdditionalIncomeDetails(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
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
                if ((myApplication.myIncomeYN == "Yes" && myHouseholdMembers.myPassCount == "1") || (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myHasIncome == "Yes"))
                {
                    appwait = (5 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    if (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "3")
                    {
                        if (myHistoryInfo.myInTimeTravel == "Yes")
                        {
                            appwait = (35 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (14 + myHistoryInfo.myAppWait) * 1000;
                        }
                    }                    
                    else//hh2
                    {
                        if (myHistoryInfo.myInTimeTravel == "Yes")
                        {
                            appwait = (35 + myHistoryInfo.myAppWait) * 1000;
                        }
                        else
                        {
                            appwait = (18 + myHistoryInfo.myAppWait) * 1000;
                        }
                    }
                }
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

                if (incomeReduced == "Yes")
                {
                    IWebElement listboxIncomeReduced = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeReduced.SendKeys(incomeReduced);
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
                returnStatus = "Fail"; myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }


        public int DoIncomeAdjustments(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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

        public int DoAnnualIncome(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

                String incomeExpected = "";
                if (myApplication.myHouseholdOther == "No")
                {
                    incomeExpected = myApplication.myIncomeExpected;
                }
                else if (myApplication.myHouseholdOther == "Yes" && myHouseholdMembers.myPassCount == "1")
                {
                    incomeExpected = myHouseholdMembers.myIncomeExpected;
                    myHouseholdMembers.myPassCount = "2";//update count to 2 to do the income screens another time
                    DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
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
                            DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
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

        public int DoAdditionalHouseholdInformation(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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

        public int DoAddInfoAPTC(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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

        public int DoEmployerSponsoredCoverage(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(3000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

                if (myApplication.myESC == "Yes")
                {
                    IWebElement listboxESC;
                    if (myApplication.myHouseholdOther == "No")
                    {
                        listboxESC = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/fieldset/table/tbody/tr/td/div[2]/div/div[1]/input"));
                    }
                    else
                    {
                        listboxESC = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/fieldset/table/tbody/tr/td[1]/div[2]/div/div[1]/input"));
                    }
                    listboxESC.Click();
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

        public int DoEmployerSponsoredCoverageMore(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

                IWebElement listboxESC = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[2]/input[1]"));
                listboxESC.SendKeys(myApplication.myESC);

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

        public int DoEmployerDetails(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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

        public int DoAdditionalInfoUnassistedInsurance(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next")));

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

                if ((myApplication.myHouseholdOther == "No" && householdCount == 1 && age.Year - 1 < 19) //1 hh
                    //what about hh 2?
                  || (myApplication.myHouseholdOther == "Yes" && householdCount == 3 && age3.Year - 1 < 19)) // 3 hh
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[2]/input[1]")));
                    IWebElement listboxOutsideHome = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[2]/input[1]"));
                    listboxOutsideHome.SendKeys("No");
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

        public int DoOtherInsurance(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next")));

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

        public int DoAdditionalInformationForAll(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(4000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));

                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                IWebElement listboxCondition;
                HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
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
                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td[1]/span[1]"));

                //this changes ids on all below boxes so moving to bottom of code
                IWebElement listboxNative;
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
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
                    listboxNative.Click();
                    outsideClick.Click();
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
                if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
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

                IWebElement listboxLongTermCare;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id12"));
                }
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3ide"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id12"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id1a"));//b10
                }
                else /*if (indian == "Yes" || 
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") ||//b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes")//q03
                    )*/
                {
                    listboxLongTermCare = driver.FindElement(By.Id("__o3id16"));
                }                
                listboxLongTermCare.SendKeys("No");

                IWebElement listboxResidentialTreatment;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id14"));
                }
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id10")));
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id10"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id15")));
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id15"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id1e"));
                }
                else /*if (indian == "Yes" || 
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") || //b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03
                    )*/
                {
                    listboxResidentialTreatment = driver.FindElement(By.Id("__o3id1a"));
                }
                                                  
                listboxResidentialTreatment.SendKeys("No");

                IWebElement listboxHaveMedicare;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id16"));
                }
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id12"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id18"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id22"));
                }
                else /*if (indian == "Yes" ||
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") || //b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03
                    )*/
                {
                    listboxHaveMedicare = driver.FindElement(By.Id("__o3id1e"));
                }

                listboxHaveMedicare.SendKeys(myApplication.myOtherIns);
                outsideClick.Click();

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
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id14"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id1b"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id26"));
                }
                else /*if (indian == "Yes" ||
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") || //b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03
                    )*/
                {
                    listboxTorture = driver.FindElement(By.Id("__o3id22"));
                }
               
                listboxTorture.SendKeys("No");

                IWebElement listboxMedicaidEligibility;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id1a"));
                }
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id16"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id1e"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                {
                    listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id2a"));
                }
                else /*if (indian == "Yes" ||
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") || //b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03
                    )*/
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
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id18"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id21"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                { 
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id2e"));
                }
                else /*if (indian == "Yes" ||
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") || //b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03
                    )*/
                {
                    listboxMedicaidHome = driver.FindElement(By.Id("__o3id2a"));
                }
                listboxMedicaidHome.SendKeys("No");

                IWebElement listboxMedicaidLongTerm;
                if (householdCount == 1 && age.Year - 1 < 19)
                {
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id1e"));
                }
                else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id1a")));
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id1a"));
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id24")));
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id24"));
                }
                else if (householdCount == 3 && age3.Year - 1 < 19)
                { 
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id32"));
                }
                else /*if (indian == "Yes" ||
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") || //b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03
                    )*/
                {
                    listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id2e"));
                }
                listboxMedicaidLongTerm.SendKeys("No");

                int temp1 = 0; //only 1 plan
                int temp2 = 0; //2 separate plans
                if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)
                {
                    String household2Income = "0";
                    household2Income = myHousehold.DoHouseholdIncome(myHistoryInfo, "2");
                    if (household2Income == null || household2Income == "")
                    {
                        household2Income = "0";
                    }
                    if (myHouseholdMembers.myRelationship == "Is Unrelated to")
                    {
                        temp1 = Convert.ToInt32(myApplication.myIncomeAmount);
                        temp2 = Convert.ToInt32(household2Income);
                    }
                    else
                    {
                        temp1 = Convert.ToInt32(myApplication.myIncomeAmount) + Convert.ToInt32(household2Income);//2 hh
                    }
                }
                else if (myApplication.myHouseholdOther == "Yes" && householdCount == 3)
                {
                    String household2Income = "0";
                    String household3Income = "0";
                    household2Income = myHousehold.DoHouseholdIncome(myHistoryInfo, "2");
                    household3Income = myHousehold.DoHouseholdIncome(myHistoryInfo, "3");
                    if (household2Income == null || household2Income == "")
                    {
                        household2Income = "0";
                    }
                    if (household3Income == null || household3Income == "")
                    {
                        household3Income = "0";
                    }
                    temp1 = Convert.ToInt32(myApplication.myIncomeAmount) + Convert.ToInt32(household2Income) + Convert.ToInt32(household3Income);//3 hh
                }
                else
                {
                    temp1 = Convert.ToInt32(myApplication.myIncomeAmount);//1 hh
                }

                if ((myApplication.myHouseholdOther == "No" && householdCount == 1 && (temp1 < 23541 || age.Year - 1 < 20)) //1 hh
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && myHouseholdMembers.myRelationship == "Is Unrelated to" && ((temp1 < 32041 && temp2 < 32041) || age.Year - 1 < 20 || age2.Year - 1 < 20)) // 2 hh unrelated
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && (temp1 < 32041 || age.Year - 1 < 20 || age2.Year - 1 < 20)) // 2 hh related
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 3 && (temp1 < 40321 || age.Year - 1 < 20 || age2.Year - 1 < 20 || age3.Year - 1 < 20)) // 3 hh
                    )
                {
                    IWebElement listboxMedicareInjury;
                    if (householdCount == 1 && age.Year - 1 < 19)
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id20"));
                    }
                    else if (householdCount == 1 || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id1c"));
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id27"));
                    }
                    else if (householdCount == 3 && age3.Year - 1 < 19)
                    { 
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id36"));
                    }
                    else /*if (indian == "Yes" ||
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") || //b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03
                    )*/
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id32"));
                    }
                    listboxMedicareInjury.SendKeys("No");


                    IWebElement listboxMAStartDate;
                    if (householdCount == 1 && age.Year - 1 < 19)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id22"));
                    }
                    else if (householdCount == 1 || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2a"));
                    }
                    else if (indian == "Yes")
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id36"));
                    }
                    else if (householdCount == 3 && age3.Year - 1 < 19)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id3a"));
                    }
                    else /*if ( (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") || //b09
                    (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03
                    )*/
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id34"));
                    }
                    listboxMAStartDate.SendKeys("No");
                }

                /*if ((myApplication.myHouseholdOther == "No" && householdCount == 1 && age.Year - 1 < 19) //1 hh
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && (age.Year - 1 < 19 || age2.Year - 1 < 19)) // 2 hh
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 3 && (age.Year - 1 < 19 || age2.Year - 1 < 19 || age3.Year - 1 < 19)) // 3 hh
                    ) //This will only appear if age < 19, this is already covered previously, why again????
                {
                    IWebElement listboxMedicareInjury;
                    if (householdCount == 1)
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id20"));
                    }
                    else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id1c"));
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 2)//2 hh
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id27"));
                    }
                    else if (indian == "Yes")
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id32"));
                    }
                    else//3 hh
                    {
                        listboxMedicareInjury = driver.FindElement(By.Id("__o3id36"));
                    }
                    listboxMedicareInjury.SendKeys("No");


                    IWebElement listboxMAStartDate;
                    if (householdCount == 1)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id22"));
                    }
                    else if (myApplication.myHouseholdOther == "No" || (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No"))
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));
                    }
                    else if (householdCount == 2)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2a"));
                    }
                    else if (indian == "Yes")
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id36"));
                    }
                    else//3 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id3a"));
                    }
                    listboxMAStartDate.SendKeys("No");
                }*/

                if ((myApplication.myHouseholdOther == "No" && householdCount == 1 && temp1 > 23540 && age.Year - 1 > 19) //1 hh
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && myHouseholdMembers.myRelationship == "Is Unrelated to" && ((temp1 > 32040 || temp2 > 32040) && age.Year - 1 > 19 && age2.Year - 1 > 19)) // 2 hh unrelated
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 2 && temp1 > 32040 && age.Year - 1 > 19 && age2.Year - 1 > 19) // 2 hh
                    || (myApplication.myHouseholdOther == "Yes" && householdCount == 3 && temp1 > 40320 && age.Year - 1 > 19 && age2.Year - 1 > 19 && age3.Year - 1 > 19) // 3 hh
                    )//not sure what the rule is here??????
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
                    else if (myApplication.myHouseholdOther == "Yes" && myApplication.myApplyYourself == "No")
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));
                    }
                    else if (householdCount == 2)//2 hh
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id2a"));
                    }                     
                    else if (householdCount == 3 && age3.Year - 1 < 19)
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id3a"));
                    }
                    else if (indian == "Yes" || (householdCount == 3 && myHouseholdMembers.myTaxFiler == "Yes" && myHouseholdMembers.myDependants == "No") )
                    {
                        listboxMAStartDate = driver.FindElement(By.Id("__o3id34"));
                    }
                    else /*if (householdCount == 3 && myHouseholdMembers.myTaxFiler == "No" && myHouseholdMembers.myDependants == "Yes") //q03*/
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

        public int DoAddInfoIndian(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[2]/input[1]")));
                IWebElement listboxFederalTribe = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[2]/input[1]"));

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

        public int DoAddInfoMilitary(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(2000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td/div/div[3]/input[1]")));
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

        public int DoSummary(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                if (myApplication.myHouseholdOther == "No")
                {
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    System.Threading.Thread.Sleep(4000);
                    myHouseholdMembers.myPassCount = "1";//switch count back to 1 to reset and be ready for next run
                    DoUpdatePassCount(myHistoryInfo, myHouseholdMembers.myPassCount);
                }
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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

        public int DoSummaryWithoutDiscounts(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                            mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (20 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (12 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

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


        public int DoSignature(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (65 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (25 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]")));

                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[2]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();

                if (myApplication.myRenewalCov == "0")
                {
                    IWebElement checkboxRenewCov = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[3]/td/div/fieldset/div/div[6]/div/input"));
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

                IWebElement textboxMiddleName = driver.FindElement(By.Id("__o3idf"));
                textboxMiddleName.SendKeys(myApplication.myMiddleName);

                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id10"));
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

        public int DoSignatureWithoutDiscounts(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait;
                if (myHistoryInfo.myInTimeTravel == "Yes")
                {
                    appwait = (14 + myHistoryInfo.myAppWait) * 1000;
                }
                else
                {
                    appwait = (4 + myHistoryInfo.myAppWait) * 1000;
                }
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));
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

        public int DoUpdatePassCount(mystructHistoryInfo myHistoryInfo, string updateValue)
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

    }
}
