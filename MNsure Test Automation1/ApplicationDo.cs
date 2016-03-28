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
                tempDOB = Convert.ToString(myApplication.myDOB);
                tempDOB = tempDOB.Substring(0, 10);
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
                tempDOB = Convert.ToString(myApplication.myDOB);
                tempDOB = tempDOB.Substring(0, 10);
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

                //This will only appear if age 18-27
                /*DateTime birth = Convert.ToDateTime(myApplication.myDOB);
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

                if (age.Year - 1 > 17 && age.Year - 1 < 26)
                {
                    IWebElement listboxFosterCare = driver.FindElement(By.Id("__o3id2f"));
                    listboxFosterCare.SendKeys(myApplication.myFosterCare);
                }*/

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
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[2]/input[1]")));
                
                IWebElement listboxHispanic = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[2]/input[1]"));
                listboxHispanic.SendKeys(myApplication.myHispanic);
                listboxHispanic.Click();

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

                if (age.Year - 1 > 17 && age.Year - 1 < 26)
                {
                    IWebElement listboxFosterCare = driver.FindElement(By.Id("__o3id2f"));
                    listboxFosterCare.SendKeys(myApplication.myFosterCare);
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


        public int DoHouseholdAbout(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                int appwait = (12 + myHistoryInfo.myAppWait) * 1000;//norm 6, could go up to 45
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

                //need the who question here
                if (myApplication.myHouseholdOther == "Yes")
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id7")));
                    IWebElement radioButtonTaxDependant = driver.FindElement(By.Id("__o3id7"));
                    radioButtonTaxDependant.Click();
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

        public int DoHouseholdMembersWithoutDiscounts(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructApplication myApplication, mystructHouseholdMembers myHouseholdMembers,
                                    mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                System.Threading.Thread.Sleep(8000);
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
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td/div[2]/div/div[1]/input")));
                
                IWebElement checkboxPerson = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/table[2]/tbody/tr/td/div[2]/div/div[1]/input"));
                checkboxPerson.Click();

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
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[5]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[1]/input")));
                
                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[5]/table/tbody/tr/td[2]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                if (myApplication.myDependants == "No")
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
              
                /*               if (myApplication.myHouseholdOther == "Yes")
                               { //This is the question of whether anyone outside the household expected to enter xx as a dependant
                                   //The number incements based on the number of household members, so may need to add to this
                                   IWebElement checkboxDependantTax = driver.FindElement(By.Id("__o3id7"));
                                   checkboxDependantTax.Click();
                                   checkboxDependantTax.SendKeys(myApplication.myDependants);
                               }
                 */
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
                int appwait = (10 + myHistoryInfo.myAppWait) * 1000;//norm 6
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
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
                int appwait = (10 + myHistoryInfo.myAppWait) * 1000;//norm 8
                System.Threading.Thread.Sleep(appwait);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[16]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")));
                
                driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[16]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div[2]/div[1]/div[1]/input")).Click();//select else arrow
                System.Threading.Thread.Sleep(1000);
                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
                if (myApplication.myIncomeType == "Wages before taxes")
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                else
                {
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                    action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Build().Perform();
                }
                action.SendKeys(OpenQA.Selenium.Keys.Enter).Build().Perform();
                
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[17]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div/input")));
                IWebElement textboxIncomeEmployer = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[17]/table/tbody/tr/td[2]/table/tbody/tr/td[1]/div/div/input"));
                textboxIncomeEmployer.SendKeys(myApplication.myIncomeEmployer);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id9")));
                IWebElement listboxIncomeSeasonal = driver.FindElement(By.Id("__o3id9"));
                listboxIncomeSeasonal.SendKeys(myApplication.myIncomeSeasonal);

                IWebElement textboxIncomeAmount = driver.FindElement(By.Id("__o3ida"));
                textboxIncomeAmount.SendKeys(myApplication.myIncomeAmount);

                IWebElement textboxIncomeFrequency = driver.FindElement(By.Id("__o3idc"));
                textboxIncomeFrequency.SendKeys(myApplication.myIncomeFrequency);

                IWebElement textboxIncomeMore = driver.FindElement(By.Id("__o3idd"));
                textboxIncomeMore.SendKeys(myApplication.myIncomeMore);

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
                System.Threading.Thread.Sleep(2000); 
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));
                
                if (myApplication.myIncomeReduced == "Yes")
                {
                    IWebElement listboxIncomeReduced = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeReduced.SendKeys(myApplication.myIncomeReduced);
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

                if (myApplication.myIncomeReduced != "No")
                {
                    IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeAdjusted.SendKeys(myApplication.myIncomeAdjusted);
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

                if (myApplication.myIncomeExpected != "Yes")
                {
                    IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeAdjusted.SendKeys(myApplication.myIncomeExpected);
                }

                if (myHistoryInfo.myInTimeTravel == "Yes")
                 {
                     if (myHistoryInfo.myTimeTravelDate > Convert.ToDateTime("10/31/2016") &&
                         myHistoryInfo.myTimeTravelDate < Convert.ToDateTime("1/1/2017"))
                     {
                         IWebElement listboxIncomeNextYear = driver.FindElement(By.Id("__o3id8"));
                         listboxIncomeNextYear.SendKeys(myApplication.myIncomeExpected);
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

                if (myApplication.myMilitary == "Yes")
                {
                    IWebElement checkboxMilitary = driver.FindElement(By.Id("__o3id9"));
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
                System.Threading.Thread.Sleep(2000); 
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.PollingInterval = TimeSpan.FromMilliseconds(100);
                IWebElement element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("__o3btn.next_label")));

                if (myApplication.myESC == "Yes")
                {
                    IWebElement listboxESC = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/fieldset/table/tbody/tr/td/div[2]/div/div[1]/input"));
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

                if (age.Year - 1 < 19)
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
                System.Threading.Thread.Sleep(2000);
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

                IWebElement listboxCondition = driver.FindElement(By.Id("__o3id8"));
                listboxCondition.SendKeys("No");

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id8")));
                IWebElement outsideClick = driver.FindElement(By.Id("__o3id8"));

                IWebElement listboxNative = driver.FindElement(By.Id("__o3ida"));
                if (myApplication.myRace == "Indian")
                {
                    listboxNative.SendKeys("Yes");
                    listboxNative.Click();                    
                    outsideClick.Click();

                    IWebElement listboxNativePerson = driver.FindElement(By.Id("__o3idb"));
                    listboxNativePerson.Click();
                }
                else
                {
                    listboxNative.SendKeys("No");
                }

                IWebElement listboxVisitMN = driver.FindElement(By.Id("__o3idc"));
                listboxVisitMN.SendKeys("No");

                IWebElement listboxLogTermCare = driver.FindElement(By.Id("__o3ide"));
                listboxLogTermCare.SendKeys("No");

                //IWebElement listboxChildCourtOrder = driver.FindElement(By.Id("__o3idf"));
                //listboxChildCourtOrder.SendKeys("No");

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id10")));
                IWebElement listboxLogResidentialTreatment = driver.FindElement(By.Id("__o3id10"));
                listboxLogResidentialTreatment.SendKeys("No");

                IWebElement listboxHaveMedicare = driver.FindElement(By.Id("__o3id12"));
                listboxHaveMedicare.SendKeys(myApplication.myOtherIns);
                outsideClick.Click();

                if (myApplication.myOtherIns == "Yes")
                {
                    IWebElement listboxMedicarePerson = driver.FindElement(By.Id("__o3id13"));
                    listboxMedicarePerson.Click();
                }

                IWebElement listboxTorture = driver.FindElement(By.Id("__o3id14"));
                listboxTorture.SendKeys("No");

                IWebElement listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id16"));
                listboxMedicaidEligibility.SendKeys("No");

                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                IWebElement listboxMedicaidHome = driver.FindElement(By.Id("__o3id18"));
                listboxMedicaidHome.SendKeys("No");

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id1a")));
                IWebElement listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id1a"));
                listboxMedicaidLongTerm.SendKeys("No");

                int temp1;
                temp1 = Convert.ToInt32(myApplication.myIncomeAmount);
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

                if (temp1 < 23540 || age.Year - 1 < 20) //This will only appear if income >23540 or age < 20
                {
                    IWebElement listboxMedicareInjury = driver.FindElement(By.Id("__o3id1c"));
                    listboxMedicareInjury.SendKeys("No");

                    IWebElement listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));
                    listboxMAStartDate.SendKeys("No");
                }
                else
                {
                    IWebElement listboxMAStartDate = driver.FindElement(By.Id("__o3id1c"));
                    listboxMAStartDate.SendKeys("No");
                }

                if (age.Year - 1 < 19) //This will only appear if age < 19
                {
                    IWebElement listboxMedicareInjury = driver.FindElement(By.Id("__o3id20"));
                    listboxMedicareInjury.SendKeys("No");

                    IWebElement listboxMAStartDate = driver.FindElement(By.Id("__o3id22"));
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
                listboxFederalTribe.SendKeys(myApplication.myFederalTribe);
                listboxFederalTribe.Click();

                IWebElement outsideClick = driver.FindElement(By.Id("__o3ida"));
                outsideClick.Click();

                if (myApplication.myFederalTribe == "Yes")
                {
                    IWebElement listboxTribeName = driver.FindElement(By.Id("__o3id7"));
                    listboxTribeName.SendKeys(myApplication.myTribeName);

                    IWebElement listboxLiveRes = driver.FindElement(By.Id("__o3id8"));
                    listboxLiveRes.SendKeys(myApplication.myLiveRes);
                }

                IWebElement listboxTribeId = driver.FindElement(By.Id("__o3ida"));
                listboxTribeId.SendKeys(myApplication.myTribeId);

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
                datepickerMilitaryDate.SendKeys(myApplication.myMilitaryDate);
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
                System.Threading.Thread.Sleep(3000); 
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
                int appwait = (8 + myHistoryInfo.myAppWait) * 1000;//this keeps changing, 4 to 40
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
                int appwait = (4 + myHistoryInfo.myAppWait) * 1000;//this keeps changing, 4 to 40
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

    }
}
