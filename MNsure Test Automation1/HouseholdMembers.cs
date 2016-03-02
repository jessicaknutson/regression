using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data.Sql;

using OpenQA.Selenium.Support.UI; /// for dropdown


using System.Data.SqlClient;
using System.Data.SqlServerCe;
using OpenQA.Selenium;

namespace MNsure_Regression_1
{
    class HouseholdMembers
    {

        public int DoGetHouseholdMember(ref mystructHouseholdMembers myHouseholdMembers, ref mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            int timeOut = myHistoryInfo.myCitizenWait;
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand
                    ("SELECT * FROM HouseMembers where TestId = "  + mySelectedTest.myTestId + " and HouseMembersID = " +
                    myHouseholdMembers.HouseMembersID + ";", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myHouseholdMembers.HouseMembersID = reader.GetInt32(0);
                        myHouseholdMembers.TestId = reader.GetInt32(1);
                        myHouseholdMembers.FirstName = reader.GetString(2);
                        myHouseholdMembers.MiddleName = reader.GetString(3);
                        myHouseholdMembers.LastName = reader.GetString(4);
                        myHouseholdMembers.Suffix = reader.GetString(5);
                        myHouseholdMembers.Gender = reader.GetString(6);
                        myHouseholdMembers.MaritalStatus = reader.GetString(7);
                        myHouseholdMembers.DOB = reader.GetString(8);
                        myHouseholdMembers.LiveWithYou = reader.GetString(9);
                        myHouseholdMembers.MNHome = reader.GetString(10);
                        myHouseholdMembers.PersonHighlighted = reader.GetString(11);
                        myHouseholdMembers.LiveInMN = reader.GetString(12);
                        myHouseholdMembers.TempAbsentMN = reader.GetString(13);
                        myHouseholdMembers.Homeless = reader.GetString(14);
                        myHouseholdMembers.Address1 = reader.GetString(15);
                        myHouseholdMembers.Address2 = reader.GetString(16);
                        myHouseholdMembers.AptSuite = reader.GetString(17);
                        myHouseholdMembers.City = reader.GetString(18);
                        myHouseholdMembers.State = reader.GetString(19);
                        myHouseholdMembers.Zip = reader.GetString(20);
                        myHouseholdMembers.PlanMakeMNHome = reader.GetString(21);
                        myHouseholdMembers.SeekEmplMN = reader.GetString(22);
                        myHouseholdMembers.Hispanic = reader.GetString(23);
                        myHouseholdMembers.Race = reader.GetString(24);
                        myHouseholdMembers.HaveSSN = reader.GetString(25);
                        myHouseholdMembers.SSN = reader.GetString(26);
                        myHouseholdMembers.USCitizen = reader.GetString(27);
                        myHouseholdMembers.USNational = reader.GetString(28);
                        myHouseholdMembers.IsPregnant = reader.GetString(29);
                        myHouseholdMembers.BeenInFosterCare = reader.GetString(30);
                        myHouseholdMembers.Relationship = reader.GetString(31);
                        myHouseholdMembers.HasIncome = reader.GetString(32);
                        myHouseholdMembers.RelationshiptoNextHM = reader.GetString(33);
                    }

                    com2.ExecuteNonQuery();
                    com2.Dispose();
                }

                return 1;
            }
            catch
            {
                return 2;
            }            
        }


        public int DoHouseholdMultipleMembers(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            int timeOut = myHistoryInfo.myCitizenWait;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
            int iloop;
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Started"; // 
                myHistoryInfo.myTestStepName = "Household Members - Multiple Members";
                writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/div/div[2]/div[2]/div[5]"))));

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
                buttonNext.Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id6"))));

             //   for (iloop = 1; iloop < myHouseholdMembers.NumMembers + 1; iloop++)
                for (iloop = 1; iloop < myHouseholdMembers.NumMembers; iloop++)
                {

                    int result;
                    myHouseholdMembers.HouseMembersID = iloop + 1;
                    HouseholdMembers householdMembers = new HouseholdMembers();
                    result = householdMembers.DoGetHouseholdMember(ref myHouseholdMembers, ref myHistoryInfo, mySelectedTest);

                    IWebElement listboxHouseholdOther = driver.FindElement(By.Id("__o3id6"));  // yes no to more members
                    listboxHouseholdOther.Click();
                    listboxHouseholdOther.SendKeys(myEnrollment.myHouseholdOther);

                    IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
                    buttonNext2.Click();

                    //Wait here for Middle name  __o3id6
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id7"))));

                    IWebElement textboxHMFirstName = driver.FindElement(By.Id("__o3id6"));
                    textboxHMFirstName.Click();
                    textboxHMFirstName.SendKeys(myHouseholdMembers.FirstName);

                    IWebElement textboxHMMiddleName = driver.FindElement(By.Id("__o3id7"));
                    textboxHMMiddleName.Click();
                    textboxHMMiddleName.SendKeys(myHouseholdMembers.MiddleName);

                    IWebElement textboxHMLastName = driver.FindElement(By.Id("__o3id8"));
                    textboxHMLastName.Click();
                    textboxHMLastName.SendKeys(myHouseholdMembers.LastName);

                    IWebElement listboxHMSuffix = driver.FindElement(By.Id("__o3id9"));
                    listboxHMSuffix.Click();
                    listboxHMSuffix.SendKeys(myHouseholdMembers.Suffix);

                    IWebElement listboxHMGender = driver.FindElement(By.Id("__o3ida"));
                    listboxHMGender.Click();
                    listboxHMGender.SendKeys(myHouseholdMembers.Gender);

                    IWebElement listboxHMMaritalStatus = driver.FindElement(By.Id("__o3idb"));
                    listboxHMMaritalStatus.Click();
                    listboxHMMaritalStatus.SendKeys(myHouseholdMembers.MaritalStatus);

                    IWebElement calendarboxHMDOB = driver.FindElement(By.Id("__o3idc"));
                    calendarboxHMDOB.Click();
                    calendarboxHMDOB.SendKeys(myHouseholdMembers.DOB);

                    IWebElement listboxHMLiveWithYou = driver.FindElement(By.Id("__o3idd"));
                    listboxHMLiveWithYou.Click();
                    listboxHMLiveWithYou.SendKeys(myHouseholdMembers.LiveWithYou);

                    IWebElement listboxHMPlanMNHome = driver.FindElement(By.Id("__o3id1b"));
                    listboxHMPlanMNHome.Click();
                    listboxHMPlanMNHome.SendKeys(myHouseholdMembers.PlanMakeMNHome);

                    IWebElement listboxHMPersonHighlighted = driver.FindElement(By.Id("__o3id1d"));
                    listboxHMPersonHighlighted.Click();
                    listboxHMPersonHighlighted.SendKeys(myHouseholdMembers.PersonHighlighted);

                    //Click the next button
                    IWebElement buttonNext3 = driver.FindElement(By.Id("__o3btn.next"));
                    buttonNext3.Click();

                    //new window wait for us citizen checkbox, cant use race dropdown, named same as object on previous window

                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id21"))));

                    IWebElement listboxHMRace = driver.FindElement(By.Id("__o3id6"));
                    listboxHMRace.Click();
                    listboxHMRace.SendKeys(myHouseholdMembers.Hispanic);

                    //Other boxes are optional, but would go here for race checkbox

                    IWebElement listboxHMHaveSSN = driver.FindElement(By.Id("__o3id1c"));
                    listboxHMHaveSSN.Click();
                    listboxHMHaveSSN.SendKeys(myHouseholdMembers.HaveSSN);
                    listboxHMHaveSSN.Click();
                    IWebElement listboxHMSUSCitizen = driver.FindElement(By.Id("__o3id21"));
                    listboxHMSUSCitizen.Click();

                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id1d"))));

                    IWebElement listboxHMSSN = driver.FindElement(By.Id("__o3id1d"));
                    listboxHMSSN.Click();
                    listboxHMSSN.SendKeys(myHouseholdMembers.SSN);

                    //IWebElement listboxHMSUSCitizen = driver.FindElement(By.Id("__o3id21"));
                    listboxHMSUSCitizen.Click();
                    listboxHMSUSCitizen.SendKeys(myHouseholdMembers.USCitizen);

                    if (myHouseholdMembers.USCitizen == "No")
                    {
                        new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id22")));
                        IWebElement listboxHMUSNational = driver.FindElement(By.Id("__o3id22"));
                        listboxHMUSNational.Click();
                        listboxHMUSNational.SendKeys(myHouseholdMembers.USNational);
                    }

                    if (myHouseholdMembers.Gender == "Female")
                    {
                        IWebElement listboxHMPregnant = driver.FindElement(By.Id("__o3id2c"));
                        listboxHMPregnant.Click();
                        //     listboxHMPregnant.SendKeys(myHouseholdMembers.IsPregnant);
                        //foster care question, need to find the cut off date

                        string TempSDOB;
                        DateTime tempDTDOB;

                        TempSDOB = myHouseholdMembers.DOB;
                        tempDTDOB = Convert.ToDateTime(TempSDOB);

                        var tempDTDOB2 = DateTime.Parse(TempSDOB).Year;
                        string tempCutoff2 = Convert.ToString(DateTime.Now);
                        int intCutoff2 = DateTime.Parse(tempCutoff2).Year;
                        intCutoff2 = intCutoff2 - 27;


                        if (tempDTDOB2 > intCutoff2)
                        {
                            IWebElement listboxHMFosterCare = driver.FindElement(By.Id("__o3id30"));
                            listboxHMFosterCare.Click();
                            listboxHMFosterCare.SendKeys(myHouseholdMembers.BeenInFosterCare);
                        }
                    }

                    //Click the next button
                    IWebElement buttonNext4 = driver.FindElement(By.Id("__o3btn.next_label"));
                    buttonNext4.Click();

                    //Add more people?
                    //Wait for text asking if more people
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[5]/table/tbody/tr/td[1]/span[1]"))));
                    // This is where to loop if more than one.

                }

                IWebElement listboxHMMorePeopleNo = driver.FindElement(By.Id("__o3id6"));
                listboxHMMorePeopleNo.Click();
                listboxHMMorePeopleNo.SendKeys("No");

                IWebElement buttonNext5 = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext5.Click();

                // relationship __o3id6

                //Wait for the person icon on the left - applicant
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[1]/div[2]/table/tbody/tr/td[1]/img"))));

                int result2;
                myHouseholdMembers.HouseMembersID = 2;
                HouseholdMembers householdMembers2 = new HouseholdMembers();
                result2 = householdMembers2.DoGetHouseholdMember(ref myHouseholdMembers, ref  myHistoryInfo, mySelectedTest);

             //   for (iloop = 1; iloop < myHouseholdMembers.NumMembers + 1; iloop++)
                    for (iloop = 1; iloop < myHouseholdMembers.NumMembers; iloop++)
                {

                    if (iloop == 1)
                    {
                        //Relationship
                        IWebElement listboxHMRelationship = driver.FindElement(By.Id("__o3id6"));
                        listboxHMRelationship.Click();
                        listboxHMRelationship.SendKeys(myHouseholdMembers.Relationship); //change
                    }
                    if (iloop == 2)
                    {
                        //Relationship
                        IWebElement listboxHMRelationship = driver.FindElement(By.Id("__o3id7"));
                        listboxHMRelationship.Click();
                        listboxHMRelationship.SendKeys(myHouseholdMembers.RelationshiptoNextHM); //change
                    }
                    if (iloop == 3)
                    {
                        //Relationship
                        IWebElement listboxHMRelationship = driver.FindElement(By.Id("__o3id8"));
                        listboxHMRelationship.Click();
                        listboxHMRelationship.SendKeys(myHouseholdMembers.RelationshiptoNextHM); //change
                    }
                    if (iloop == 4)
                    {
                        //Relationship
                        IWebElement listboxHMRelationship = driver.FindElement(By.Id("__o3id9"));
                        listboxHMRelationship.Click();
                        listboxHMRelationship.SendKeys(myHouseholdMembers.Relationship); //change
                    }
                    if (iloop == 5)  // this will be the max for now: 5 = 1 applicant, 4 additional members
                    {
                        //Relationship
                        IWebElement listboxHMRelationship = driver.FindElement(By.Id("__o3id10"));
                        listboxHMRelationship.Click();
                        listboxHMRelationship.SendKeys(myHouseholdMembers.Relationship); //change
                    }
                    //__o3id6
                }
                //Click the next button
                IWebElement buttonNext6 = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext6.Click();

                for (iloop = 1; iloop < myHouseholdMembers.NumMembers + 1; iloop++)
                {
                    int result;
                    myHouseholdMembers.HouseMembersID = iloop + 1;
                    HouseholdMembers householdMembers = new HouseholdMembers();
                    result = householdMembers.DoGetHouseholdMember(ref myHouseholdMembers, ref myHistoryInfo, mySelectedTest);

                    if (iloop == 1)
                    {
                     //   if (myHouseholdMembers.HouseMembersID <= myHouseholdMembers.NumMembers + 1)
                            if (myHouseholdMembers.HouseMembersID < myHouseholdMembers.NumMembers)
                        {

                            //Relationship
                            IWebElement listboxHMRelationshiptoNext = driver.FindElement(By.Id("__o3id6"));
                            listboxHMRelationshiptoNext.Click();
                            listboxHMRelationshiptoNext.SendKeys(myHouseholdMembers.RelationshiptoNextHM); //change
                        }
                        else
                        {
                            //do nothing, window wont even appear
                        }
                    }
                    if (iloop == 2)
                    {
                        if (myHouseholdMembers.HouseMembersID < myHouseholdMembers.NumMembers + 1)
                        {

                            IWebElement buttonNext9 = driver.FindElement(By.Id("__o3btn.next_label"));
                            buttonNext9.Click();
                        }
                        else
                        {
                            //do nothing, window wont even appear
                        }
                    }
                }


                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Pass"; // 
                myHistoryInfo.myTestStepName = "Household Members - Multiple Members";
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Fail"; // 
                myHistoryInfo.myTestStepName = "Household Members";
                myHistoryInfo.myStepNotes = "Failed to complete Household Members - Multiple Members screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoHouseholdHM(IWebDriver driver, mystructApplication myEnrollment, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            //not sure if this is ever used
            WriteLogs writeLogs = new WriteLogs();
            int timeOut = myHistoryInfo.myCitizenWait;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Started"; // 
                myHistoryInfo.myTestStepName = "Household Members";
                writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[1]/table/tbody/tr/td/div/div[2]/div[2]/div[6]"))));

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next"));
                buttonNext.Click();

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id6"))));

                /*
                IWebElement listboxFirstName = driver.FindElement(By.Id("__o3id6"));
                listboxFirstName.Click();
                listboxFirstName.SendKeys(myEnrollment.myFirstName);

                IWebElement listboxHouseholdOther = driver.FindElement(By.Id("__o3id7"));
                listboxHouseholdOther.Click();
                listboxHouseholdOther.SendKeys(myEnrollment.myMiddleName);
                */
                IWebElement listboxHouseholdOther = driver.FindElement(By.Id("__o3id6"));
                listboxHouseholdOther.Click();
                listboxHouseholdOther.SendKeys(myEnrollment.myHouseholdOther);

                //need the who question here
                if (myEnrollment.myHouseholdOther == "Yes")
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id7")));
                    IWebElement radioButtonTaxDependant = driver.FindElement(By.Id("__o3id7"));
                    radioButtonTaxDependant.Click();
                }

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext2.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Pass"; // 
                myHistoryInfo.myTestStepName = "Household Members";
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Fail"; // 
                myHistoryInfo.myTestStepName = "Household Members";
                myHistoryInfo.myStepNotes = "Failed to complete Household Members screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoTaxDependantsHM(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            int timeOut = myHistoryInfo.myCitizenWait;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Started"; // 
                myHistoryInfo.myTestStepName = "Tax Dependents";
                writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id7"))));

                //This may change if multiple house hold members
                if (myEnrollment.myHouseholdOther == "Yes")
                {
                    IWebElement checkboxDependant = driver.FindElement(By.Id("__o3id6"));
                    checkboxDependant.Click();
                    checkboxDependant.SendKeys(myEnrollment.myDependants);
                }
                else
                {
                    IWebElement checkboxDependant = driver.FindElement(By.Id("__o3id7"));
                    checkboxDependant.Click();
                    checkboxDependant.SendKeys(myEnrollment.myDependants);
                }

                IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext2.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Pass";  
                myHistoryInfo.myTestStepName = "Tax Dependents";
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Fail"; // 
                myHistoryInfo.myTestStepName = "Tax Dependents";
                myHistoryInfo.myStepNotes = "Failed to complete Tax Dependents screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoIncomeForHM(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            int timeOut = myHistoryInfo.myCitizenWait;
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Started"; // 
                myHistoryInfo.myTestStepName = "Income for Household members";
                writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                //This waits for the Household Summary title
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[4]/div/div/table/tbody/tr/td/div[1]/h2"))));

        //        IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
       //         buttonNext.Click(); //remove this

                //look for title on Income Section page
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[4]/div/div/table/tbody/tr/td/div[1]/h2"))));
                //        new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div[2]/table/tbody/tr/td[1]/span[1]"))));

                if (myEnrollment.myHouseholdOther == "No")
                {
                    IWebElement checkboxIncomeYN = driver.FindElement(By.Id("__o3id6"));
                    checkboxIncomeYN.Click();
                    checkboxIncomeYN.SendKeys(myEnrollment.myIncomeYN);
                }

        //        IWebElement buttonNext2 = driver.FindElement(By.Id("__o3btn.next_label"));
        //        buttonNext2.Click(); //Remove this

                if (myEnrollment.myHouseholdOther == "Yes")
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.Id("__o3id6"))));
                    IWebElement checkboxIncomeYN = driver.FindElement(By.Id("__o3id6"));
                    checkboxIncomeYN.Click();
                    checkboxIncomeYN.SendKeys(myHouseholdMembers.HasIncome);
                    IWebElement buttonNext3 = driver.FindElement(By.Id("__o3btn.next_label"));
                    buttonNext3.Click();
                }

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Pass"; // 
                myHistoryInfo.myTestStepName = "Income";
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Fail"; // 
                myHistoryInfo.myTestStepName = "Income for Household members";
                myHistoryInfo.myStepNotes = "Failed to complete Income screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 2;
            }
        }

        
        public int DoEnterIncomeDetailsHM(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            int timeOut = myHistoryInfo.myCitizenWait;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Started"; // 
                myHistoryInfo.myTestStepName = "Enter Income Details";
                writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                //This checks for the any more income box at the bottom, it is unique
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id6")));

                IWebElement listboxIncomeType = driver.FindElement(By.Id("__o3id6"));
                listboxIncomeType.Click();
                listboxIncomeType.SendKeys(myEnrollment.myIncomeType);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id7")));
                IWebElement textboxIncomeEmployer = driver.FindElement(By.Id("__o3id7"));
                textboxIncomeEmployer.SendKeys(myEnrollment.myIncomeEmployer);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id("__o3id9")));
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
   
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Pass"; 
                myHistoryInfo.myTestStepName = "Enter Income Details";
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Fail"; // 
                myHistoryInfo.myTestStepName = "Enter Income Details";
                myHistoryInfo.myStepNotes = "Failed to complete Enter Income Details.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 2;
            }

        }
        
        public int DoAdditionalIncomeDetailsHM(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            int timeOut = myHistoryInfo.myCitizenWait;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Started"; // 
                myHistoryInfo.myTestStepName = "Additional Income Details for Household members";
                writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                //This checks for the text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[2]/table/tbody/tr/td[1]/span[1]"))));

                if (myEnrollment.myIncomeReduced != "No")
                {
                    IWebElement listboxIncomeReduced = driver.FindElement(By.Id("__o3id6"));
                    listboxIncomeReduced.SendKeys(myEnrollment.myIncomeReduced);
                }
                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Pass"; // 
                myHistoryInfo.myTestStepName = "Additional Income Details for Household members";
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Fail"; // 
                myHistoryInfo.myTestStepName = "Additional Income Details for Household members";
                myHistoryInfo.myStepNotes = "Failed to complete Additional Income Details for Household members screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 2;
            }
        }

        public int DoIncomeAdjustmentsHM(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
                {
                    WriteLogs writeLogs = new WriteLogs();
                    int timeOut = myHistoryInfo.myCitizenWait;
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
                    try
                    {
                        myHistoryInfo.myStepStartTime = DateTime.Now;
                        myHistoryInfo.myStepEndTime = DateTime.Now;
                        myHistoryInfo.myTestStepStatus = "Started"; // 
                        myHistoryInfo.myTestStepName = "Income Adjustments for Household Members";
                        writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                        //This checks for the text at the bottom
                        new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[2]/table/tbody/tr/td[1]/span"))));

                        if (myEnrollment.myIncomeReduced != "No")
                        {
                            IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                            listboxIncomeAdjusted.SendKeys(myEnrollment.myIncomeAdjusted);
                        }

                        IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                        buttonNext.Click();

                        myHistoryInfo.myStepEndTime = DateTime.Now;
                        myHistoryInfo.myTestStepStatus = "Pass"; // 
                        myHistoryInfo.myTestStepName = "Income Adjustments for Household Members";
                        writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                        return 1;
                    }

                    catch (Exception e)
                    {
                        myHistoryInfo.myStepEndTime = DateTime.Now;
                        myHistoryInfo.myTestStepStatus = "Fail"; // 
                        myHistoryInfo.myTestStepName = "IIncome Adjustments for Household Members";
                        myHistoryInfo.myStepNotes = "Failed to complete Income Adjustments for Household Members screen.";
                        myHistoryInfo.myStepException = Convert.ToString(e);
                        writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                        writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                        return 2;
                    }

                }

        public int DoAnnualIncomeHM(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
                {
                    WriteLogs writeLogs = new WriteLogs();
                    int timeOut = myHistoryInfo.myCitizenWait;
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
                    try
                    {
                        myHistoryInfo.myStepStartTime = DateTime.Now;
                        myHistoryInfo.myStepEndTime = DateTime.Now;
                        myHistoryInfo.myTestStepStatus = "Started"; // 
                        myHistoryInfo.myTestStepName = "Annual Income for Household Members";
                        writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                        //This checks for the text at the bottom
                        new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div[2]/div/div/div/div[3]/table/tbody/tr/td[1]/span[1]"))));

                        if (myEnrollment.myIncomeExpected != "Yes")
                        {
                            IWebElement listboxIncomeAdjusted = driver.FindElement(By.Id("__o3id6"));
                            listboxIncomeAdjusted.SendKeys(myEnrollment.myIncomeExpected);
                        }

                        //From 10/31 - 1/1 additional income verification is asked
                        //
                        if (myHistoryInfo.myInTimeTravel == "Yes")
                        {
                            if (myHistoryInfo.myTimeTravelDate > Convert.ToDateTime("10/31/2016") &&
                                myHistoryInfo.myTimeTravelDate < Convert.ToDateTime("1/1/2017"))
                            {
                                IWebElement listboxIncomeNextYear = driver.FindElement(By.Id("__o3id8"));
                                listboxIncomeNextYear.SendKeys("Yes");
                            }
                        }
                        else if (DateTime.Now > Convert.ToDateTime("10/31/2016") &&
                            DateTime.Now < Convert.ToDateTime("1/1/2017"))
                        {
                            IWebElement listboxIncomeNextYear = driver.FindElement(By.Id("__o3id8"));
                            listboxIncomeNextYear.SendKeys("Yes");
                        }

                        IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                        buttonNext.Click();

                        myHistoryInfo.myStepEndTime = DateTime.Now;
                        myHistoryInfo.myTestStepStatus = "Pass"; // 
                        myHistoryInfo.myTestStepName = "Annual Income for Household Members";
                        writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                        return 1;
                    }

                    catch (Exception e)
                    {
                        myHistoryInfo.myStepEndTime = DateTime.Now;
                        myHistoryInfo.myTestStepStatus = "Fail"; // 
                        myHistoryInfo.myTestStepName = "AAnnual Income for Household Members";
                        myHistoryInfo.myStepNotes = "Failed to complete Annual Income for Household Memberse screen.";
                        myHistoryInfo.myStepException = Convert.ToString(e);
                        writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                        writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                        return 2;
                    }

                }

        public int DoAdditionalInformationForAllHM(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            //setting all values to no, explicitely.  Not through the database.
            // if anything but no is required, additional logic, and table columns will be needed.
            WriteLogs writeLogs = new WriteLogs();
            int timeOut = myHistoryInfo.myCitizenWait;

            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Started"; // 
                myHistoryInfo.myTestStepName = "Additional Information for all Applicants";
                writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[9]/table/tbody/tr/td[1]/span[1]"))));

                IWebElement listboxBlind = driver.FindElement(By.Id("__o3id6"));
                listboxBlind.Click();
                listboxBlind.SendKeys("No");

                IWebElement listboxCondition = driver.FindElement(By.Id("__o3id9"));
                listboxCondition.SendKeys("No");

                IWebElement listboxNative = driver.FindElement(By.Id("__o3idc"));
                listboxNative.SendKeys("No");

                IWebElement listboxVisitMN = driver.FindElement(By.Id("__o3idf"));
                listboxVisitMN.SendKeys("No");

                IWebElement listboxLogTermCare = driver.FindElement(By.Id("__o3id12"));
                listboxLogTermCare.SendKeys("No");

                //                IWebElement listboxChildCourtOrder = driver.FindElement(By.Id("__o3idf"));
                //               listboxChildCourtOrder.SendKeys("No");

                IWebElement listboxLogResidentialTreatment = driver.FindElement(By.Id("__o3id15"));
                listboxLogResidentialTreatment.SendKeys("No");

                IWebElement listboxHaveMedicare = driver.FindElement(By.Id("__o3id18"));
                listboxHaveMedicare.SendKeys("No");

                IWebElement listboxTorture = driver.FindElement(By.Id("__o3id1b"));
                listboxTorture.SendKeys("No");

                IWebElement listboxMedicaidEligibility = driver.FindElement(By.Id("__o3id1e"));
                listboxMedicaidEligibility.SendKeys("No");


                IWebElement listboxMedicaidHome = driver.FindElement(By.Id("__o3id21"));
                listboxMedicaidHome.SendKeys("No");

                IWebElement listboxMedicaidLongTerm = driver.FindElement(By.Id("__o3id24"));
                listboxMedicaidLongTerm.SendKeys("No");

                //This will only appear if income >24000
                int temp1;
                temp1 = Convert.ToInt32(myEnrollment.myIncomeAmount);
                if (temp1 < 24000)
                {
                    IWebElement listboxMedicareInjury = driver.FindElement(By.Id("__o3id27"));
                    listboxMedicareInjury.SendKeys("No");

                    IWebElement listboxMAStartDate = driver.FindElement(By.Id("__o3id2a"));
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
                myHistoryInfo.myTestStepStatus = "Pass"; // 
                myHistoryInfo.myTestStepName = "Additional Information for all Applicants";
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Fail"; // 
                myHistoryInfo.myTestStepName = "Additional Information for all Applicants";
                myHistoryInfo.myStepNotes = "Failed to complete Additional Information for all Applicants screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 2;
            }

        }

        public int DoTaxFilerSpouseHM(IWebDriver driver, mystructApplication myEnrollment, mystructHouseholdMembers myHouseholdMembers, mystructHistoryInfo myHistoryInfo, mystructSelectedTest mySelectedTest)
        {
            WriteLogs writeLogs = new WriteLogs();
            int timeOut = myHistoryInfo.myCitizenWait;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeOut));
            try
            {
                myHistoryInfo.myStepStartTime = DateTime.Now;
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Started"; // 
                myHistoryInfo.myTestStepName = "Married Couple Tax Filer";
                writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);

                //This checks for the text at the bottom
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists((By.XPath("/html/body/form/div/div[3]/div[5]/div/div/div/div/div[3]/table/tbody/tr/td[1]/span[1]"))));

                IWebElement listboxFilingJointly = driver.FindElement(By.Id("__o3id6"));
                listboxFilingJointly.Click();
                listboxFilingJointly.SendKeys("Yes");

                IWebElement buttonNext = driver.FindElement(By.Id("__o3btn.next_label"));
                buttonNext.Click();

                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Pass"; // 
                myHistoryInfo.myTestStepName = "Married Couple Tax Filer";
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 1;
            }

            catch (Exception e)
            {
                myHistoryInfo.myStepEndTime = DateTime.Now;
                myHistoryInfo.myTestStepStatus = "Fail"; // 
                myHistoryInfo.myTestStepName = "IIncome Adjustments for Household Members";
                myHistoryInfo.myStepNotes = "Failed to complete Income Adjustments for Household Members screen.";
                myHistoryInfo.myStepException = Convert.ToString(e);
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                return 2;
            }

        }
    }
}
