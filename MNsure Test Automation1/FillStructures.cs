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
        public int doFillStructures(mystructSelectedTest mySelectedTest, mystructAccountCreate myAccountCreate, ref mystructApplication myApplication, ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;

                //Read configured rows if exist, otherwise fill with default values
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
                        if (reader.GetString(45) != null)
                        {
                            myApplication.myTribeName = reader.GetString(45);
                        }
                        if (reader.GetString(46) != null)
                        {
                            myApplication.myLiveRes = reader.GetString(46);
                        }

                        if (reader.GetString(47) != null)
                        {
                            myApplication.myTribeId = reader.GetString(47);
                        }

                        if (reader.GetString(48) != null)
                        {
                            myApplication.myFederalTribe = reader.GetString(48);
                        }
                        if (reader.GetString(49) != null)
                        {
                            myApplication.myMilitary = reader.GetString(49);
                        }
                        if (!reader.IsDBNull(50))
                        {
                            myApplication.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(50)).ToString("MM/dd/yyyy");
                        }
                    }
                }

                SqlCeCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;

                //Read configured rows if exist, otherwise fill with default values
                using (SqlCeCommand com3 = new SqlCeCommand("SELECT * FROM Address where TestId = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader = com3.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(9) == "Home")
                        {
                            myApplication.myHomeAddress1 = reader.GetString(3);
                            if (reader.GetString(4) != null)
                            {
                                myApplication.myHomeAddress2 = reader.GetString(4);
                            }
                            myApplication.myHomeCity = reader.GetString(5);
                            myApplication.myHomeState = reader.GetString(6);
                            myApplication.myHomeZip = reader.GetString(7);
                            if (reader.GetString(8) != null)
                            {
                                myApplication.myHomeZip4 = reader.GetString(8);
                            }
                            if (reader.GetString(10) != null)
                            {
                                myApplication.myCounty = reader.GetString(10);
                            }
                            if (reader.GetString(11) != null)
                            {
                                myApplication.myHomeAptSuite = reader.GetString(11);
                            }
                        }
                        else
                        {
                            myApplication.myMailAddress1 = reader.GetString(3);
                            if (reader.GetString(4) != null)
                            {
                                myApplication.myMailAddress2 = reader.GetString(4);
                            }
                            myApplication.myMailCity = reader.GetString(5);
                            myApplication.myMailState = reader.GetString(6);
                            myApplication.myMailZip = reader.GetString(7);
                            if (reader.GetString(8) != null)
                            {
                                myApplication.myMailZip4 = reader.GetString(8);
                            }                            
                            if (reader.GetString(11) != null)
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
                    //myInsertString = "Insert into Account values (" + myAccountCreate.myAccountID + ", " + mySelectedTest.myTestId +
                    //    ", @FirstName, @MiddleName, @LastName, @Suffix, @Address1 , @Address2 , @City , @State, @Zip, @Zip4, @Email, @Phone, @SSN, @DOB, @Username );";
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
