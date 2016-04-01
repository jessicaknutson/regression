using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNsure_Regression_1
{
    class FillStructures
    {
        public int doFillStructures(mystructSelectedTest mySelectedTest, mystructAccountCreate myAccountCreate, ref mystructApplication myApplication, ref mystructHouseholdMembers myHouseholdMembers, ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;

                //Read configured rows if exist
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Application where TestId = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myApplication.myFirstName = myAccountCreate.myFirstName;//auto generated
                        myApplication.myMiddleName = myAccountCreate.myMiddleName;//auto generated
                        myApplication.myLastName = myAccountCreate.myLastName;//auto generated
                        myApplication.mySuffix = reader.GetString(5);
                        myApplication.myGender = reader.GetString(6);
                        myApplication.myMaritalStatus = reader.GetString(7);
                        if (!reader.IsDBNull(8))
                        {
                            string tempDOB;
                            tempDOB = Convert.ToString(reader.GetDateTime(8));
                            tempDOB = DateTime.Parse(tempDOB).ToString("MM/dd/yyyy");
                            if (tempDOB != "01/01/2011")
                            {
                                myApplication.myDOB = tempDOB;
                            }
                            else
                            {
                                myApplication.myDOB = myAccountCreate.myDOB;
                            }
                        }
                        else
                        {
                            myApplication.myDOB = myAccountCreate.myDOB;
                        }

                        myApplication.myLiveMN = reader.GetString(9);
                        myApplication.myPlanLiveMN = reader.GetString(10);
                        myApplication.myPrefContact = reader.GetString(11);
                        myApplication.myPhoneNum = reader.GetString(12);
                        myApplication.myPhoneType = reader.GetString(13);
                        myApplication.myAltNum = reader.GetString(14);
                        myApplication.myAltNumType = reader.GetString(15);
                        myApplication.myEmail = myAccountCreate.myEmail;//auto generated
                        myApplication.myLanguageMost = reader.GetString(17);
                        myApplication.myLanguageWritten = reader.GetString(18);
                        myApplication.myVoterCard = reader.GetString(19);
                        myApplication.myNotices = reader.GetString(20);
                        myApplication.myAuthRep = reader.GetString(21);
                        myApplication.myApplyYourself = reader.GetString(22);
                        myApplication.myHomeless = reader.GetString(23);                        
                        myApplication.myAddressSame = reader.GetString(24);                        
                        myApplication.myHispanic = reader.GetString(25);
                        myApplication.myRace = reader.GetString(26);
                        myApplication.mySSN = reader.GetString(27);
                        myApplication.myCitizen = reader.GetString(28);
                        myApplication.mySSNNum = myAccountCreate.mySSN;//auto generated
                        myApplication.myHouseholdOther = reader.GetString(30);
                        myApplication.myDependants = reader.GetString(31);
                        myApplication.myIncomeYN = reader.GetString(32);
                        myApplication.myIncomeType = reader.GetString(33);
                        myApplication.myIncomeAmount = reader.GetString(34);
                        myApplication.myIncomeFrequency = reader.GetString(35);
                        myApplication.myIncomeMore = reader.GetString(36);
                        myApplication.myIncomeEmployer = reader.GetString(37);
                        myApplication.myIncomeSeasonal = reader.GetString(38);
                        myApplication.myIncomeReduced = reader.GetString(39);
                        myApplication.myIncomeAdjusted = reader.GetString(40);
                        myApplication.myIncomeExpected = reader.GetString(41);
                        myApplication.myEnrollmentPlanType = reader.GetString(42);
                        myApplication.myFosterCare = reader.GetString(43);
                        myApplication.myMailingAddressYN = reader.GetString(44);
                        if (!reader.IsDBNull(45))
                        {
                            myApplication.myTribeName = reader.GetString(45);
                        }
                        if (!reader.IsDBNull(46))
                        {
                            myApplication.myLiveRes = reader.GetString(46);
                        }

                        if (!reader.IsDBNull(47))
                        {
                            myApplication.myTribeId = reader.GetString(47);
                        }

                        if (!reader.IsDBNull(48))
                        {
                            myApplication.myFederalTribe = reader.GetString(48);
                        }
                        if (!reader.IsDBNull(49))
                        {
                            myApplication.myMilitary = reader.GetString(49);
                        }
                        if (!reader.IsDBNull(50))
                        {
                            myApplication.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(50)).ToString("MM/dd/yyyy");
                        }
                        myApplication.myAppliedSSN = reader.GetString(51);
                        if (!reader.IsDBNull(52))
                        {
                            myApplication.myWhyNoSSN = reader.GetString(52);
                        }
                        myApplication.myAssistSSN = reader.GetString(53);
                        myApplication.myOtherIns = reader.GetString(54);
                        if (!reader.IsDBNull(55))
                        {
                            myApplication.myKindIns = reader.GetString(55);
                        }
                        myApplication.myCoverageEnd = reader.GetString(56);
                        myApplication.myAddIns = reader.GetString(57);
                        myApplication.myESC = reader.GetString(58);
                        myApplication.myRenewalCov = reader.GetString(59);
                    }
                }

                SqlCeCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;

                //Read configured rows if exist
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM HouseMembers where TestID = " + mySelectedTest.myTestId + " and HouseMembersID = 2", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myHouseholdMembers.myFirstName = reader.GetString(2);
                        myHouseholdMembers.myMiddleName = reader.GetString(3);
                        myHouseholdMembers.myLastName = reader.GetString(4);
                        myHouseholdMembers.mySuffix = reader.GetString(5);
                        myHouseholdMembers.myGender = reader.GetString(6);
                        myHouseholdMembers.myMaritalStatus = reader.GetString(7);
                        myHouseholdMembers.myDOB = reader.GetString(8);
                        myHouseholdMembers.myLiveWithYou = reader.GetString(9);
                        myHouseholdMembers.myMNHome = reader.GetString(10); //is this the same mnhome and planmakemnhome????                       
                        myHouseholdMembers.myPersonHighlighted = reader.GetString(11);
                        myHouseholdMembers.myLiveInMN = reader.GetString(12);
                        myHouseholdMembers.myTempAbsentMN = reader.GetString(13);
                        myHouseholdMembers.myHomeless = reader.GetString(14);
                        myHouseholdMembers.myHomeAddress1 = reader.GetString(15);//move to addr db
                        myHouseholdMembers.myHomeAddress2 = reader.GetString(16);
                        myHouseholdMembers.myHomeAptSuite = reader.GetString(17);
                        myHouseholdMembers.myHomeCity = reader.GetString(18);
                        myHouseholdMembers.myHomeState = reader.GetString(19);
                        myHouseholdMembers.myHomeZip = reader.GetString(20);
                        myHouseholdMembers.myPlanMakeMNHome = reader.GetString(21); 
                        myHouseholdMembers.mySeekEmplMN = reader.GetString(22);                        
                        myHouseholdMembers.myHispanic = reader.GetString(23);
                        myHouseholdMembers.myRace = reader.GetString(24);
                        myHouseholdMembers.myHaveSSN = reader.GetString(25);
                        //myHouseholdMembers.mySSN = reader.GetString(26);//auto generated
                        myHouseholdMembers.myUSCitizen = reader.GetString(27);
                        myHouseholdMembers.myUSNational = reader.GetString(28);
                        myHouseholdMembers.myIsPregnant = reader.GetString(29);
                        myHouseholdMembers.myBeenInFosterCare = reader.GetString(30);
                        myHouseholdMembers.myRelationship = reader.GetString(31);
                        myHouseholdMembers.myHasIncome = reader.GetString(32);
                        myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(33);
                        myHouseholdMembers.myTribeName = reader.GetString(34);
                        myHouseholdMembers.myTribeId = reader.GetString(35);
                        myHouseholdMembers.myLiveRes = reader.GetString(36);
                        myHouseholdMembers.myFederalTribe = reader.GetString(37);
                        myHouseholdMembers.myFileJointly = reader.GetString(38);
                        myHouseholdMembers.myIncomeType = reader.GetString(39);
                        myHouseholdMembers.myIncomeEmployer = reader.GetString(40);
                        myHouseholdMembers.myIncomeSeasonal = reader.GetString(41);
                        myHouseholdMembers.myIncomeAmount = reader.GetString(42);
                        myHouseholdMembers.myIncomeFrequency = reader.GetString(43);
                        myHouseholdMembers.myIncomeMore = reader.GetString(44);                        
                        myHouseholdMembers.myIncomeReduced = reader.GetString(45);
                        myHouseholdMembers.myIncomeAdjusted = reader.GetString(46);
                        myHouseholdMembers.myIncomeExpected = reader.GetString(47);
                        myHouseholdMembers.myPassCount = reader.GetString(48);
                    }
                }

                SqlCeCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;

                //Read configured rows if exist
                using (SqlCeCommand com3 = new SqlCeCommand("SELECT * FROM Address where TestId = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader = com3.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(9) == "Home")
                        {
                            myApplication.myHomeAddress1 = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                            {
                                myApplication.myHomeAddress2 = reader.GetString(4);
                            }
                            myApplication.myHomeCity = reader.GetString(5);
                            myApplication.myHomeState = reader.GetString(6);
                            myApplication.myHomeZip = reader.GetString(7);
                            if (!reader.IsDBNull(8))
                            {
                                myApplication.myHomeZip4 = reader.GetString(8);
                            }
                            myApplication.myHomeCounty = reader.GetString(10);
                            if (!reader.IsDBNull(11))
                            {
                                myApplication.myHomeAptSuite = reader.GetString(11);
                            }
                        }
                        else
                        {
                            myApplication.myMailAddress1 = reader.GetString(3);
                            if (!reader.IsDBNull(4))
                            {
                                myApplication.myMailAddress2 = reader.GetString(4);
                            }
                            myApplication.myMailCity = reader.GetString(5);
                            myApplication.myMailState = reader.GetString(6);
                            myApplication.myMailZip = reader.GetString(7);
                            if (!reader.IsDBNull(8))
                            {
                                myApplication.myMailZip4 = reader.GetString(8);
                            }
                            myApplication.myMailCounty = reader.GetString(10);
                            if (!reader.IsDBNull(11))
                            {
                                myApplication.myMailAptSuite = reader.GetString(11);
                            }
                        }
                    }
                }

                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fill all structures didn't work");
                return 1;
            }
            
        }

        public int doFillHMStructures(mystructSelectedTest mySelectedTest, mystructAccountCreate myAccountCreate, ref mystructApplication myApplication, ref mystructHouseholdMembers myHouseholdMembers, ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                //Read configured rows if exist
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM HouseMembers where TestID = " + mySelectedTest.myTestId + " and HouseMembersID = 2", con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        myHouseholdMembers.myFirstName = reader.GetString(2);
                        myHouseholdMembers.myMiddleName = reader.GetString(3);
                        myHouseholdMembers.myLastName = reader.GetString(4);
                        myHouseholdMembers.mySuffix = reader.GetString(5);
                        myHouseholdMembers.myGender = reader.GetString(6);
                        myHouseholdMembers.myMaritalStatus = reader.GetString(7);
                        myHouseholdMembers.myDOB = reader.GetString(8);
                        myHouseholdMembers.myLiveWithYou = reader.GetString(9);
                        myHouseholdMembers.myMNHome = reader.GetString(10);                       
                        myHouseholdMembers.myPersonHighlighted = reader.GetString(11);
                        myHouseholdMembers.myLiveInMN = reader.GetString(12);
                        myHouseholdMembers.myTempAbsentMN = reader.GetString(13);
                        myHouseholdMembers.myHomeless = reader.GetString(14);
                        myHouseholdMembers.myHomeAddress1 = reader.GetString(15);//move to addr db
                        myHouseholdMembers.myHomeAddress2 = reader.GetString(16);
                        myHouseholdMembers.myHomeAptSuite = reader.GetString(17);
                        myHouseholdMembers.myHomeCity = reader.GetString(18);
                        myHouseholdMembers.myHomeState = reader.GetString(19);
                        myHouseholdMembers.myHomeZip = reader.GetString(20);
                        myHouseholdMembers.myPlanMakeMNHome = reader.GetString(21);
                        myHouseholdMembers.mySeekEmplMN = reader.GetString(22);
                        myHouseholdMembers.myHispanic = reader.GetString(23);
                        myHouseholdMembers.myRace = reader.GetString(24);
                        myHouseholdMembers.myHaveSSN = reader.GetString(25);
                        //myHouseholdMembers.mySSN = reader.GetString(26);//auto generated
                        myHouseholdMembers.myUSCitizen = reader.GetString(27);
                        myHouseholdMembers.myUSNational = reader.GetString(28);
                        myHouseholdMembers.myIsPregnant = reader.GetString(29);
                        myHouseholdMembers.myBeenInFosterCare = reader.GetString(30);
                        myHouseholdMembers.myRelationship = reader.GetString(31);
                        myHouseholdMembers.myHasIncome = reader.GetString(32);
                        myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(33);
                        myHouseholdMembers.myTribeName = reader.GetString(34);
                        myHouseholdMembers.myTribeId = reader.GetString(35);
                        myHouseholdMembers.myLiveRes = reader.GetString(36);
                        myHouseholdMembers.myFederalTribe = reader.GetString(37);
                        myHouseholdMembers.myFileJointly = reader.GetString(38);
                        myHouseholdMembers.myIncomeType = reader.GetString(39);
                        myHouseholdMembers.myIncomeEmployer = reader.GetString(40);
                        myHouseholdMembers.myIncomeSeasonal = reader.GetString(41);
                        myHouseholdMembers.myIncomeAmount = reader.GetString(42);
                        myHouseholdMembers.myIncomeFrequency = reader.GetString(43);
                        myHouseholdMembers.myIncomeMore = reader.GetString(44);
                        myHouseholdMembers.myIncomeReduced = reader.GetString(45);
                        myHouseholdMembers.myIncomeAdjusted = reader.GetString(46);
                        myHouseholdMembers.myIncomeExpected = reader.GetString(47);
                        myHouseholdMembers.myPassCount = reader.GetString(48);
                    }
                }

                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fill household members structure didn't work");
                return 1;
            }
            
        }

        public int doCreateAccount(ref mystructSelectedTest mySelectedTest, ref mystructAccountCreate myAccountCreate, ref mystructApplication myApplication)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                //Read configured rows if exist, otherwise fill with default values
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM Application where TestId = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(8))
                        {
                            string tempDOB;
                            tempDOB = Convert.ToString(reader.GetDateTime(8));
                            tempDOB = DateTime.Parse(tempDOB).ToString("MM/dd/yyyy");
                            if (tempDOB != "01/01/2011")
                            {
                                myApplication.myDOB = tempDOB;
                            }
                            else
                            {
                                myApplication.myDOB = myAccountCreate.myDOB;
                            }
                        }
                        else
                        {
                            myApplication.myDOB = myAccountCreate.myDOB;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Read account didn't work");
                return 1;
            }
            

            try
            {
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.CommandText = "Delete from Account where TestId = " + mySelectedTest.myTestId + ";";
                cmd2.ExecuteNonQuery();
                myAccountCreate.myAccountID = 1;
                int result;
                AccountGeneration myAccountGeneration = new AccountGeneration();
                result = myAccountGeneration.GenerateNames(mySelectedTest, ref myAccountCreate);

                try
                {
                    con = new SqlCeConnection(conString);
                    con.Open();
                    string myInsertString;
                    myInsertString = "Insert into Account values (" + myAccountCreate.myAccountID + ", " + mySelectedTest.myTestId +
                       ", @FirstName, @MiddleName, @LastName, @Suffix, @Email, @Phone, @SSN, @DOB, @Username );";
                    using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                    {
                        com3.Parameters.AddWithValue("FirstName", myAccountCreate.myFirstName);
                        com3.Parameters.AddWithValue("MiddleName", myAccountCreate.myMiddleName);
                        com3.Parameters.AddWithValue("LastName", myAccountCreate.myLastName);
                        com3.Parameters.AddWithValue("Suffix", myAccountCreate.mySuffix);
                        com3.Parameters.AddWithValue("Email", myAccountCreate.myEmail);
                        com3.Parameters.AddWithValue("Phone", myAccountCreate.myPhone);
                        com3.Parameters.AddWithValue("SSN", myAccountCreate.mySSN);
                        if (myApplication.myDOB == null)
                        {
                            com3.Parameters.AddWithValue("DOB", myAccountCreate.myDOB);
                        }
                        else
                        {
                            com3.Parameters.AddWithValue("DOB", myApplication.myDOB);
                        }
                        com3.Parameters.AddWithValue("Username", myAccountCreate.myUsername);

                        com3.ExecuteNonQuery();
                        com3.Dispose();
                    }
                }
                catch
                {
                    MessageBox.Show("Add New Account didn't work");
                }
            }
            catch
            {
                MessageBox.Show("Get next Account_id didn't work");
            }

            return 1;
        }
    }
}
