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

namespace MNsure_Test_Automation1
{
    class Applications
    {
        int timeOut = 30;
        public int DoGettingStarted(IWebDriver driver, mystructAccountCreate myAccountCreate, mystructHistoryInfo myHistoryInfo)
        {
            WriteLogs writeLogs = new WriteLogs();
            myHistoryInfo.myStepStartTime = DateTime.Now;
            myHistoryInfo.myStepEndTime = DateTime.Now;
            myHistoryInfo.myStepStatus = "Started"; // 
            myHistoryInfo.myStepName = "Getting Started";
            writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));


            try
            {
                System.Threading.Thread.Sleep(2000);
            //Click the link to enroll families or individuals with discounts
            IWebElement linkEnrollSingleDiscounts = driver.FindElement(By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div/div/div[1]/div/div/h1[1]/ul/li/a"));
            linkEnrollSingleDiscounts.Click();
            
            //Check the I agree box
            driver.SwitchTo().Frame("curamUAIframe");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            
            System.Threading.Thread.Sleep(5000);
            IWebElement checkboxAgree = driver.FindElement(By.Id("__o3id6"));       

            checkboxAgree.Click();

            //Click the Next button
            System.Threading.Thread.Sleep(1000);
            IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label")); 
            //IWebElement buttonContinue = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/div[2]/div/div/div[1]/input"));
            buttonNext.Click();

            //Click the Next button, again
            System.Threading.Thread.Sleep(1000);
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementExists((By.Id("__o3btn.next"))));
            System.Threading.Thread.Sleep(1000);

            IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next"));
            buttonNext2.Click();

            myHistoryInfo.myStepEndTime = DateTime.Now;
            myHistoryInfo.myStepStatus = "Pass"; // 
            myHistoryInfo.myStepName = "Getting Started";
            writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
            return 1;

            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Getting Started";
                myHistoryInfo.myStepNotes = "Failed to complete Getting Started page.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo); 
                 return 2;
            }
        }

        public int DoApplicantDetails(IWebDriver driver, ref mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
       {
           WriteLogs writeLogs = new WriteLogs();
           driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
       
            myHistoryInfo.myStepStartTime = DateTime.Now;
            myHistoryInfo.myStepEndTime = DateTime.Now;
            myHistoryInfo.myStepStatus = "Started"; // 
            myHistoryInfo.myStepName = "Applicant Information";
            writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            try
            {
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;

                //Read configured rows if exist, otherwise fill with default values
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Enrollment where Test_Id = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myEnrollment.myFirstName = reader.GetString(2);
                        myEnrollment.myMiddleName = reader.GetString(3);
                        myEnrollment.myLastName = reader.GetString(4);
                        myEnrollment.mySuffix = reader.GetString(5);
                        myEnrollment.myGender = reader.GetString(6);
                        myEnrollment.myMaritalStatus = reader.GetString(7);
                        myEnrollment.myDOB = Convert.ToString(reader.GetDateTime(8));
                        myEnrollment.myLiveMN = reader.GetString(9);
                        myEnrollment.myPlanLiveMN = reader.GetString(10);
                        myEnrollment.myPrefContact = reader.GetString(11);
                        myEnrollment.myPhoneNum = reader.GetString(12);
                        myEnrollment.myPhoneType = reader.GetString(13);
                        myEnrollment.myAltNum = reader.GetString(14);
                        myEnrollment.myAltNumType = reader.GetString(15);
                        myEnrollment.myEmail = reader.GetString(16);
                        myEnrollment.myLanguageMost = reader.GetString(17);
                        myEnrollment.myLanguageWritten = reader.GetString(18);
                        myEnrollment.myVoterCard = reader.GetString(19);
                        myEnrollment.myNotices = reader.GetString(20);
                        myEnrollment.myAuthRep = reader.GetString(21);
                        myEnrollment.myApplyYourself = reader.GetString(22);
                        myEnrollment.myHomeless = reader.GetString(23);
                        myEnrollment.myAddress1 = reader.GetString(24);
                        myEnrollment.myAddress2 = reader.GetString(25);
                        myEnrollment.myCity = reader.GetString(26);
                        myEnrollment.myState = reader.GetString(27);
                        myEnrollment.myZip = reader.GetString(28);
                        myEnrollment.myZip4 = reader.GetString(29);
                        myEnrollment.myAddressSame = reader.GetString(30);
                        myEnrollment.myCounty = reader.GetString(31);
                        myEnrollment.myAptSuite = reader.GetString(32);
                        myEnrollment.myHispanic = reader.GetString(33);
                        myEnrollment.myRace = reader.GetString(34);
                        myEnrollment.mySSN = reader.GetString(35);
                        myEnrollment.myCitizen = reader.GetString(36);
                        myEnrollment.mySSNNum = reader.GetString(37);
                        myEnrollment.myHouseholdOther = reader.GetString(38);
                        myEnrollment.myDependants = reader.GetString(39);
                        myEnrollment.myIncomeYN = reader.GetString(40);
                        myEnrollment.myIncomeType = reader.GetString(41);
                        myEnrollment.myIncomeAmount = reader.GetString(42);
                        myEnrollment.myIncomeFrequency = reader.GetString(43);
                        myEnrollment.myIncomeMore = reader.GetString(44);
                        myEnrollment.myIncomeEmployer = reader.GetString(45);
                        myEnrollment.myIncomeSeasonal = reader.GetString(46);
                        myEnrollment.myIncomeReduced = reader.GetString(47);
                        myEnrollment.myIncomeAdjusted = reader.GetString(48);

                    }
                    else
                    {
                        myHistoryInfo.myStepEndTime = DateTime.Now;
                        myHistoryInfo.myStepStatus = "Fail"; // 
                        myHistoryInfo.myStepName = "Applicant Information";
                        myHistoryInfo.myStepNotes = "Failed to read Enrollment row from db.";
                        writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                        return 2;
                    }
                 }
            }
            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Applicant Information";
                myHistoryInfo.myStepNotes = "Failed to read Enrollment row from db.";
                myHistoryInfo.myStepException = Convert.ToString(e);

                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }
         

            System.Threading.Thread.Sleep(1000);
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementExists((By.Id("__o3id6"))));
            System.Threading.Thread.Sleep(1000);

            IWebElement textboxFirstName = driver.FindElement(By.Id("__o3id6"));
            textboxFirstName.SendKeys(myEnrollment.myFirstName);

            IWebElement textboxMiddleName = driver.FindElement(By.Id("__o3id7"));
            textboxMiddleName.SendKeys(myEnrollment.myMiddleName);

            IWebElement textboxLastName = driver.FindElement(By.Id("__o3id8"));
            textboxLastName.SendKeys(myEnrollment.myLastName);

            IWebElement textboxSuffix = driver.FindElement(By.Id("__o3id9"));
            textboxSuffix.SendKeys(myEnrollment.mySuffix);

            IWebElement textboxGender = driver.FindElement(By.Id("__o3ida"));
            textboxGender.SendKeys(myEnrollment.myGender);

            IWebElement textboxMaritalStatus = driver.FindElement(By.Id("__o3idb"));
            textboxMaritalStatus.SendKeys(myEnrollment.myMaritalStatus);

            string tempDOB;
            tempDOB = Convert.ToString(myEnrollment.myDOB);
            tempDOB = tempDOB.Substring(0, 10);
            IWebElement textboxDOB = driver.FindElement(By.Id("__o3idc"));
            textboxDOB.SendKeys(tempDOB);

            System.Threading.Thread.Sleep(2000);

            IWebElement listboxLiveMN = driver.FindElement(By.Id("__o3idd"));
            listboxLiveMN.SendKeys(myEnrollment.myLiveMN);

            IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[2]/td[2]"));
            outsideClick.Click();

            System.Threading.Thread.Sleep(2000);

            IWebElement listboxHomeless = driver.FindElement(By.Id("__o3ide"));
            listboxHomeless.SendKeys(myEnrollment.myHomeless);

            outsideClick.Click();

            System.Threading.Thread.Sleep(2000);


            if (myEnrollment.myHomeless == "No")
            {
                IWebElement listboxAddress1 = driver.FindElement(By.Id("__o3id11"));
                listboxAddress1.SendKeys(myEnrollment.myAddress1);

                IWebElement listboxAddress2 = driver.FindElement(By.Id("__o3id12"));
                listboxAddress2.SendKeys(myEnrollment.myAddress2);

                IWebElement listboxAptSuite = driver.FindElement(By.Id("__o3id13"));
                listboxAptSuite.SendKeys(myEnrollment.myAptSuite);

                IWebElement listboxCity = driver.FindElement(By.Id("__o3id14"));
                listboxCity.SendKeys(myEnrollment.myCity);

                IWebElement listboxCounty = driver.FindElement(By.Id("__o3id15"));
                listboxCounty.SendKeys(myEnrollment.myCounty);

                IWebElement listboxState = driver.FindElement(By.Id("__o3id16"));
                listboxState.SendKeys(myEnrollment.myState);

                IWebElement listboxZip = driver.FindElement(By.Id("__o3id17"));
                listboxZip.SendKeys(myEnrollment.myZip);

                IWebElement listboxAddressSame = driver.FindElement(By.Id("__o3id18"));
                listboxAddressSame.SendKeys(myEnrollment.myAddressSame);
            }

            IWebElement listboxPlanLiveMN = driver.FindElement(By.Id("__o3id21"));
            listboxPlanLiveMN.SendKeys(myEnrollment.myPlanLiveMN);

            IWebElement listboxPreferedContact = driver.FindElement(By.Id("__o3id23"));
            listboxPreferedContact.SendKeys(myEnrollment.myPrefContact);

            string mysPhone1 = myEnrollment.myPhoneNum.Substring(0, 3);
            string mysPhone2 = myEnrollment.myPhoneNum.Substring(3, 3);
            string mysPhone3 = myEnrollment.myPhoneNum.Substring(6, 4);
            IWebElement textboxPhoneNum = driver.FindElement(By.Id("__o3id24"));
            textboxPhoneNum.SendKeys(mysPhone1);
            IWebElement textboxPhoneNum2 = driver.FindElement(By.Id("__o3id25"));
            textboxPhoneNum2.SendKeys(mysPhone2);
            IWebElement textboxPhoneNum3 = driver.FindElement(By.Id("__o3id26"));
            textboxPhoneNum3.SendKeys(mysPhone3);

            IWebElement listboxPhoneType = driver.FindElement(By.Id("__o3id27"));
            listboxPhoneType.SendKeys(myEnrollment.myPhoneType);

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

//  These values default to English, so not needed to interact, leaving code in case
//  need to change languages.
//            IWebElement listboxLangaugeMost = driver.FindElement(By.Id("__o3id2d"));
//                var selectlistboxLangaugeMost = new SelectElement(listboxLangaugeMost);
//                selectlistboxLangaugeMost.SelectByValue(myEnrollment.myLanguageMost);
        //    listboxLangaugeMost.SendKeys(myEnrollment.myLanguageMost);

//            IWebElement listboxWrittenLangauge = driver.FindElement(By.Id("__o3id30"));
//            var selectlistboxWrittenLangauge = new SelectElement(listboxWrittenLangauge);
//            selectlistboxLangaugeMost.SelectByValue(myEnrollment.myLanguageMost);
           // listboxWrittenLangauge.SendKeys(myEnrollment.myLanguageWritten);

            IWebElement listboxVoterCard = driver.FindElement(By.Id("__o3id32"));
            listboxVoterCard.SendKeys(myEnrollment.myVoterCard);

            IWebElement listboxNotices = driver.FindElement(By.Id("__o3id33"));
            listboxNotices.SendKeys(myEnrollment.myNotices);

            IWebElement listboxAuthRep = driver.FindElement(By.Id("__o3id34"));
            listboxAuthRep.SendKeys(myEnrollment.myAuthRep);

            IWebElement listboxApplyYouself = driver.FindElement(By.Id("__o3id35"));
            listboxApplyYouself.SendKeys(myEnrollment.myApplyYourself);

            IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
            buttonNext.Click();

            myHistoryInfo.myStepEndTime = DateTime.Now;
            myHistoryInfo.myStepStatus = "Pass"; // 
            myHistoryInfo.myStepName = "Applicant Information";
            writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
            return 1;
            }
            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Applicant Information";
                myHistoryInfo.myStepNotes = "Failed to complete the Information About You page.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }
        
        }


        public int DoMoreAboutYou(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {

                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "More About You";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(2000);

                new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementExists((By.Id("__o3id6"))));
                System.Threading.Thread.Sleep(2000);

                IWebElement listboxHispanic = driver.FindElement(By.Id("__o3id6"));
                listboxHispanic.SendKeys(myEnrollment.myHispanic);

                IWebElement checkboxRace = driver.FindElement(By.Id("__o3id17"));
                checkboxRace.Click();

                IWebElement listboxSSN = driver.FindElement(By.Id("__o3id1c"));
                listboxSSN.SendKeys(myEnrollment.mySSN);

                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[6]/table/tbody/tr/td[1]"));
                outsideClick.Click();
                System.Threading.Thread.Sleep(1000);

                if (myEnrollment.mySSN == "Yes")
                {
                    IWebElement listboxSSNNum = driver.FindElement(By.Id("__o3id1d")); 
                    listboxSSNNum.SendKeys(myEnrollment.mySSNNum);
                }
                System.Threading.Thread.Sleep(2000);
                outsideClick.Click();

                IWebElement listboxCitizen = driver.FindElement(By.Id("__o3id21"));
                listboxCitizen.SendKeys(myEnrollment.myCitizen);
                outsideClick.Click();

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
                buttonNext.Click();
                System.Threading.Thread.Sleep(2000);

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "More About You";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "More About You";
                myHistoryInfo.myStepNotes = "Failed to complete More About You screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }
        }


        public int DoHousehold(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Household Members";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(2000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/div/div[2]/div[2]/div[5]"))));
                System.Threading.Thread.Sleep(2000);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
                buttonNext.Click();

                System.Threading.Thread.Sleep(2000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id6"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement listboxHouseholdOther = driver.FindElement(By.Id("__o3id6"));
                listboxHouseholdOther.Click();
                listboxHouseholdOther.SendKeys(myEnrollment.myHouseholdOther);

                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[6]/table/tbody/tr/td[1]"));
                outsideClick.Click();

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext2.Click();
                

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Household Members";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Household Members";
                myHistoryInfo.myStepNotes = "Failed to complete Household Members screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }


        public int DoTaxFiler(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Tax Filer";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(2000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id6"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement checkboxPerson = driver.FindElement(By.Id("__o3id6"));
                checkboxPerson.Click();

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next"));
                buttonNext2.Click();
                System.Threading.Thread.Sleep(1000);

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Tax Filer";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Tax Filer";
                myHistoryInfo.myStepNotes = "Failed to complete Tax Filer screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

       }

        public int DoTaxDependants(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Tax Dependents";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                    System.Threading.Thread.Sleep(1000);
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id7"))));
                    System.Threading.Thread.Sleep(1000);

                    IWebElement checkboxDependant = driver.FindElement(By.Id("__o3id7"));
                    checkboxDependant.Click();
                    checkboxDependant.SendKeys(myEnrollment.myDependants);

                    IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
                    buttonNext2.Click();

                    myHistoryInfo.myStepEndTime = DateTime.Now;
                    myHistoryInfo.myStepStatus = "Pass"; // 
                    myHistoryInfo.myStepName = "Tax Dependents";
                    writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                    return 1;
                }

                catch (Exception e)
                {
                    myHistoryInfo.myStepEndTime = DateTime.Now;
                    myHistoryInfo.myStepStatus = "Fail"; // 
                    myHistoryInfo.myStepName = "Tax Dependents";
                    myHistoryInfo.myStepNotes = "Failed to complete Tax Dependents screen.";
                    myHistoryInfo.myStepException = Convert.ToString(e);
                    writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                    writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                    return 2;
                }

            }

        public int DoHouseholdSummary(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
          driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Household Summary";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                //Look for the header near the bottom: Tax Filer Information
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[7]/h3"))));
                System.Threading.Thread.Sleep(1000);


                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
               // IWebElement buttonNext = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td/span/span/span/span[3]"));

                buttonNext.Click();
                System.Threading.Thread.Sleep(1000);

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Household Summary";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Household Summary";
                myHistoryInfo.myStepNotes = "Failed to complete Household Summary screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoIncome(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Income";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/div/div[2]/div[2]/div[6]"))));


                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                System.Threading.Thread.Sleep(1000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[1]/span[1]"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement checkboxIncomeYN = driver.FindElement(By.Id("__o3id6"));
                checkboxIncomeYN.Click();
                checkboxIncomeYN.SendKeys(myEnrollment.myIncomeYN);

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext2.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Income";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Income";
                myHistoryInfo.myStepNotes = "Failed to complete Income screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }
        }


  public int DoEnterIncomeDetails(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Enter Income Details";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);
                
                System.Threading.Thread.Sleep(1000);
                //This checks for the any more income box at the bottom, it is unique
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3idd"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement listboxIncomeType = driver.FindElement(By.Id("__o3id6"));
                listboxIncomeType.Click();
                listboxIncomeType.SendKeys(myEnrollment.myIncomeType);

                IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[16]/table/tbody/tr/td[3]"));
                outsideClick.Click();

                System.Threading.Thread.Sleep(1000);

                IWebElement textboxIncomeEmployer = driver.FindElement(By.Id("__o3id7"));
                textboxIncomeEmployer.SendKeys(myEnrollment.myIncomeEmployer);

                System.Threading.Thread.Sleep(2000);

                IWebElement listboxIncomeSeasonal = driver.FindElement(By.Id("__o3id9"));
                listboxIncomeSeasonal.SendKeys(myEnrollment.myIncomeSeasonal);

                IWebElement textboxIncomeAmount = driver.FindElement(By.Id("__o3ida"));
                textboxIncomeAmount.SendKeys(myEnrollment.myIncomeAmount);

                IWebElement textboxIncomeFrequency = driver.FindElement(By.Id("__o3idc"));
                textboxIncomeFrequency.SendKeys(myEnrollment.myIncomeFrequency);

                IWebElement textboxIncomeMore = driver.FindElement(By.Id("__o3idd"));
                textboxIncomeMore.SendKeys(myEnrollment.myIncomeMore);

                IWebElement buttonNext3 = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext3.Click();
                System.Threading.Thread.Sleep(1000);
                

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Enter Income Details";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Enter Income Details";
                myHistoryInfo.myStepNotes = "Failed to complete Enter Income Details.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }
    //    DoHouseholdIncomeAdjusted
        public int DoAdditionalIncomeDetails(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Additional Income Details";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                //This checks for the text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[2]/table/tbody/tr/td[1]/span[1]"))));
                System.Threading.Thread.Sleep(1000);


                if (myEnrollment.myIncomeReduced != "No")
                {
                    IWebElement listboxIncomeReduced = driver.FindElement(By.Id("__o3id6"));
                    //                listboxIncomeReduced.Click();
                    listboxIncomeReduced.SendKeys(myEnrollment.myIncomeReduced);
                }

        //        IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div"));
        //        outsideClick.Click();

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Additional Income Details";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Additional Income Details";
                myHistoryInfo.myStepNotes = "Failed to complete Additional Income Details screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }


        public int DoIncomeAdjustments(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Income Adjustments";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                //This checks for the text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[2]/table/tbody/tr/td[1]/span"))));
                System.Threading.Thread.Sleep(1000);

                if (myEnrollment.myIncomeReduced != "No")
                {
                    IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeAdjusted.SendKeys(myEnrollment.myIncomeAdjusted);
                }

       //         IWebElement outsideClick = driver.FindElement(By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div"));
      //          outsideClick.Click();

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Income Adjustments";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Income Adjustments";
                myHistoryInfo.myStepNotes = "Failed to complete Income Adjustments screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoAnnualIncome(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Annual Income";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                //This checks for the text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[3]/table/tbody/tr/td[1]/span[1]"))));

                System.Threading.Thread.Sleep(2000);

                if (myEnrollment.myIncomeExpected != "Yes")
                {
                    IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeAdjusted.SendKeys(myEnrollment.myIncomeExpected);
                }

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Annual Income";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Annual Income";
                myHistoryInfo.myStepNotes = "Failed to complete Annual Income screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoAdditionalHouseholdInformation(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Additional Household Information Section";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/div/div[2]/div[2]/div[6]"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Additional Household Information Section";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Annual Income";
                myHistoryInfo.myStepNotes = "Failed to complete Additional Household Information Section screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoAdditionalAPTCProgarmInformation(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Additional APTC Program Information";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[11]/table/tbody/tr/td/fieldset/legend/span[1]"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Additional APTC Program Information";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Additional APTC Program Information";
                myHistoryInfo.myStepNotes = "Failed to complete Additional APTC Program Information screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoEmployerSponsoredCoverage(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Employer Sponsored Coverage Information";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/fieldset/legend/span"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();
                System.Threading.Thread.Sleep(5000);


                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Employer Sponsored Coverage Information";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Employer Sponsored Coverage Information";
                myHistoryInfo.myStepNotes = "Failed to complete Employer Sponsored Coverage Information screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoAdditionalInformationForAll(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            //setting all values to no, explicitely.  Not through the database.
            // if anything but no is required, additional logic, and table columns will be needed.
            WriteLogs writeLogs = new WriteLogs();

            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Additional Information for all Applicants";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[9]/table/tbody/tr/td[1]/span[1]"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement listboxBlind = driver.FindElement(By.Id("__o3id6"));
                listboxBlind.SendKeys("No");

                IWebElement listboxCondition = driver.FindElement(By.Id("__o3id8"));
                listboxCondition.SendKeys("No");

                IWebElement listboxNative = driver.FindElement(By.Id("__o3ida"));
                listboxNative.SendKeys("No");

                IWebElement listboxVisitMN = driver.FindElement(By.Id("__o3idc"));
                listboxVisitMN.SendKeys("No");

                IWebElement listboxLogTermCare = driver.FindElement(By.Id("__o3ide"));
                listboxLogTermCare.SendKeys("No");

                IWebElement listboxLogResidentialTreatment = driver.FindElement(By.Id("__o3id10"));
                listboxLogResidentialTreatment.SendKeys("No");

                IWebElement listboxHaveMedicare = driver.FindElement(By.Id("__o3id12"));
                listboxHaveMedicare.SendKeys("No");

                IWebElement listboxTorture = driver.FindElement(By.Id("__o3id14"));
                listboxTorture.SendKeys("No");

                IWebElement listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id16"));
                listboxMedicaidEligibility.SendKeys("No");


                IWebElement listboxMedicaidHome = driver.FindElement(By.Id("__o3id18"));
                listboxMedicaidHome.SendKeys("No");

                IWebElement listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id1a"));
                listboxMedicaidLongTerm.SendKeys("No");

                //This will only appear if income >24000
                int temp1;
                    temp1 = Convert.ToInt32(myEnrollment.myIncomeAmount);
                if (temp1 < 24000)
                {
                    IWebElement listboxMedicareInjury = driver.FindElement(By.Id("__o3id1c"));
                    listboxMedicareInjury.SendKeys("No");
                //    listboxMedicareInjury.Click();

                    IWebElement listboxMAStartDate = driver.FindElement(By.Id("__o3id1e"));
                    listboxMAStartDate.SendKeys("No");
                }
                else
                {
                    IWebElement listboxMAStartDate = driver.FindElement(By.Id("__o3id1c"));
                    listboxMAStartDate.SendKeys("No");
                }
                
                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Additional Information for all Applicants";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Additional Information for all Applicants";
                myHistoryInfo.myStepNotes = "Failed to complete Additional Information for all Applicants screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

      public int DoSummary(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();

            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Summary";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                //check for text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[29]/h3"))));
                System.Threading.Thread.Sleep(1000);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Summary";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Summary";
                myHistoryInfo.myStepNotes = "Failed to complete Summary screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoSignature(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            //setting all values manually, explicitely.  Not through the database.
            // if anything is required, additional logic, and table columns will be needed.
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Signature";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                System.Threading.Thread.Sleep(1000);
                //check for assister text
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[4]/table/tbody/tr[2]/td/p"))));
                
                System.Threading.Thread.Sleep(1000);

                IWebElement listboxAssister = driver.FindElement(By.Id("__o3id6"));
                listboxAssister.Click();
                listboxAssister.SendKeys("No");

                IWebElement checkboxIAgreeNoticeRR = driver.FindElement(By.Id("__o3ida"));
                checkboxIAgreeNoticeRR.Click();

                IWebElement checkboxIAgreeInfoApplication = driver.FindElement(By.Id("__o3idb"));
                checkboxIAgreeInfoApplication.Click();

                IWebElement checkboxIDeclare = driver.FindElement(By.Id("__o3idc"));
                checkboxIDeclare.Click();

                IWebElement checkboxIAgreeStatementsBelow = driver.FindElement(By.Id("__o3idd"));
                checkboxIAgreeStatementsBelow.Click();

                IWebElement textboxFirstName = driver.FindElement(By.Id("__o3ide"));
                textboxFirstName.SendKeys(myEnrollment.myFirstName);

                IWebElement textboxMiddleName = driver.FindElement(By.Id("__o3idf"));
                textboxMiddleName.SendKeys(myEnrollment.myMiddleName);

                IWebElement textboxLastName = driver.FindElement(By.Id("__o3id10"));
                textboxLastName.SendKeys(myEnrollment.myLastName);

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();
                System.Threading.Thread.Sleep(3000);


                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Signature";
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Signature";
                myHistoryInfo.myStepNotes = "Failed to complete Signature screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }


        public int DoEnrollMNsure(IWebDriver driver, mystructEnrollment myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Started"; // 
                myHistoryInfo.myStepName = "Your Healthcare Options";
                writeLogs.DoWriteHistoryStepStart(ref myHistoryInfo);

                
                driver.SwitchTo().DefaultContent();

              
                int tempI;
                    tempI = Convert.ToInt32(myEnrollment.myIncomeAmount);
                    if (tempI <= 24500)
                {
                    System.Threading.Thread.Sleep(1000);
                    //check for text at the bottom
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath(".//*[substring(@id, 1, 25)='program-action-button-SBP']"))));
                    System.Threading.Thread.Sleep(1000);

                    IWebElement buttonContinue = driver.FindElement(By.XPath(".//*[substring(@id, 1, 25)='program-action-button-SBP']"));
                    buttonContinue.Click();

                    System.Threading.Thread.Sleep(1000);
                    //check for text at the bottom
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_2"))));
                    System.Threading.Thread.Sleep(1000);


                    IWebElement buttonContinue2 = driver.FindElement(By.Id("dijit_form_Button_2"));
                    buttonContinue2.Click();

                    System.Threading.Thread.Sleep(1000);
                    //check for text at the bottom
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/div[3]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div[1]/div[1]/div[1]/div"))));
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    //check for text at the bottom
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath(".//*[substring(@id, 1, 25)='program-action-button-SBP']"))));
                    System.Threading.Thread.Sleep(1000);

                    IWebElement buttonEnroll = driver.FindElement(By.XPath(".//*[substring(@id, 1, 25)='program-action-button-IA']"));
                    buttonEnroll.Click();

                    System.Threading.Thread.Sleep(1000);
                    //check for text at the bottom
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("dijit_form_Button_2"))));
                    System.Threading.Thread.Sleep(1000);

                    IWebElement buttonContinue2 = driver.FindElement(By.Id("dijit_form_Button_2"));
                    buttonContinue2.Click();

                    System.Threading.Thread.Sleep(1000);
                    //check for text at the bottom
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("q9"))));
                    System.Threading.Thread.Sleep(1000);

                }
                //Done

                 
               
                
                

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Pass"; // 
                myHistoryInfo.myStepName = "Your Healthcare Options";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myStepStatus = "Fail"; // 
                myHistoryInfo.myStepName = "Your Healthcare Options";
                myHistoryInfo.myStepNotes = "Failed to complete Your Healthcare Options screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryStepEnd(ref myHistoryInfo);
                return 2;
            }

        }
     }
   }
