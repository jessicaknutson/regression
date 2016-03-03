﻿using System;
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
                        myApplication.myEmail = reader.GetString(16);
                        myApplication.myLanguageMost = reader.GetString(17);
                        myApplication.myLanguageWritten = reader.GetString(18);
                        myApplication.myVoterCard = reader.GetString(19);
                        myApplication.myNotices = reader.GetString(20);
                        myApplication.myAuthRep = reader.GetString(21);
                        myApplication.myApplyYourself = reader.GetString(22);
                        myApplication.myHomeless = reader.GetString(23);
                        myApplication.myAddress1 = myAccountCreate.myAddress1;//auto generated
                        myApplication.myAddress2 = myAccountCreate.myAddress2;//auto generated
                        myApplication.myCity = myAccountCreate.myCity;//auto generated
                        myApplication.myState = myAccountCreate.myState;//auto generated
                        myApplication.myZip = myAccountCreate.myZip;//auto generated
                        myApplication.myZip4 = myAccountCreate.myZip4;//auto generated
                        myApplication.myAddressSame = reader.GetString(30);
                        myApplication.myCounty = reader.GetString(31);
                        myApplication.myAptSuite = reader.GetString(32);
                        myApplication.myHispanic = reader.GetString(33);
                        myApplication.myRace = reader.GetString(34);
                        myApplication.mySSN = reader.GetString(35);
                        myApplication.myCitizen = reader.GetString(36);
                        myApplication.mySSNNum = myAccountCreate.mySSN;//auto generated
                        myApplication.myHouseholdOther = reader.GetString(38);
                        myApplication.myDependants = reader.GetString(39);
                        myApplication.myIncomeYN = reader.GetString(40);
                        myApplication.myIncomeType = reader.GetString(41);
                        myApplication.myIncomeAmount = reader.GetString(42);
                        myApplication.myIncomeFrequency = reader.GetString(43);
                        myApplication.myIncomeMore = reader.GetString(44);
                        myApplication.myIncomeEmployer = reader.GetString(45);
                        myApplication.myIncomeSeasonal = reader.GetString(46);
                        myApplication.myIncomeReduced = reader.GetString(47);
                        myApplication.myIncomeAdjusted = reader.GetString(48);
                        myApplication.myIncomeExpected = reader.GetString(49);
                        myApplication.myEnrollmentPlanType = reader.GetString(50);
                        myApplication.myFosterCare = reader.GetString(51);
                        myApplication.myMailingAddressYN = reader.GetString(52);
                        if (reader.GetString(53) != null)
                        {
                            myApplication.myTribeName = reader.GetString(53);
                        }
                        if (reader.GetString(54) != null)
                        {
                            myApplication.myLiveRes = reader.GetString(54);
                        }

                        if (reader.GetString(55) != null)
                        {
                            myApplication.myTribeId = reader.GetString(55);
                        }

                        if (reader.GetString(56) != null)
                        {
                            myApplication.myFederalTribe = reader.GetString(56);
                        }
                        if (reader.GetString(57) != null)
                        {
                            myApplication.myMilitary = reader.GetString(57);
                        }
                        if (reader.GetDateTime(58) != null)
                        {
                            myApplication.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(58)).ToString("MM/dd/yyyy");
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
                    myInsertString = "Insert into Account values (" + myAccountCreate.myAccountID + ", " + mySelectedTest.myTestId +
                        ", @FirstName, @MiddleName, @LastName, @Suffix, @Address1 , @Address2 , @City , @State, @Zip, @Zip4, @Email, @Phone, @SSN, @DOB, @Username );";
                    using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                    {
                        com3.Parameters.AddWithValue("FirstName", myAccountCreate.myFirstName);
                        com3.Parameters.AddWithValue("MiddleName", myAccountCreate.myMiddleName);
                        com3.Parameters.AddWithValue("LastName", myAccountCreate.myLastName);
                        com3.Parameters.AddWithValue("Suffix", myAccountCreate.mySuffix);
                        com3.Parameters.AddWithValue("Address1", myAccountCreate.myAddress1);
                        com3.Parameters.AddWithValue("Address2", myAccountCreate.myAddress2);
                        com3.Parameters.AddWithValue("City", myAccountCreate.myCity);
                        com3.Parameters.AddWithValue("State", myAccountCreate.myState);
                        com3.Parameters.AddWithValue("Zip", myAccountCreate.myZip);
                        com3.Parameters.AddWithValue("Zip4", myAccountCreate.myZip4);
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
