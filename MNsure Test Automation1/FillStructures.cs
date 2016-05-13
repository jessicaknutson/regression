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
                        if (!reader.IsDBNull(2)) { myApplication.myFirstName = myAccountCreate.myFirstName; }//auto generated
                        if (!reader.IsDBNull(3)) { myApplication.myMiddleName = myAccountCreate.myMiddleName; }//auto generated
                        if (!reader.IsDBNull(4)) { myApplication.myLastName = myAccountCreate.myLastName; }//auto generated
                        if (!reader.IsDBNull(5)) { myApplication.mySuffix = reader.GetString(5); }
                        if (!reader.IsDBNull(6)) { myApplication.myGender = reader.GetString(6); }
                        if (!reader.IsDBNull(7)) { myApplication.myMaritalStatus = reader.GetString(7); }
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
                        if (!reader.IsDBNull(9)) { myApplication.myLiveMN = reader.GetString(9); }
                        if (!reader.IsDBNull(10)) { myApplication.myPlanLiveMN = reader.GetString(10); }
                        if (!reader.IsDBNull(11)) { myApplication.myPrefContact = reader.GetString(11); }
                        if (!reader.IsDBNull(12)) { myApplication.myPhoneNum = reader.GetString(12); }
                        if (!reader.IsDBNull(13)) { myApplication.myPhoneType = reader.GetString(13); }
                        if (!reader.IsDBNull(14)) { myApplication.myAltNum = reader.GetString(14); }
                        if (!reader.IsDBNull(15)) { myApplication.myAltNumType = reader.GetString(15); }
                        if (!reader.IsDBNull(16)) { myApplication.myEmail = myAccountCreate.myEmail; }//auto generated
                        if (!reader.IsDBNull(17)) { myApplication.myLanguageMost = reader.GetString(17); }
                        if (!reader.IsDBNull(18)) { myApplication.myLanguageWritten = reader.GetString(18); }
                        if (!reader.IsDBNull(19)) { myApplication.myVoterCard = reader.GetString(19); }
                        if (!reader.IsDBNull(20)) { myApplication.myNotices = reader.GetString(20); }
                        if (!reader.IsDBNull(21)) { myApplication.myAuthRep = reader.GetString(21); }
                        if (!reader.IsDBNull(22)) { myApplication.myApplyYourself = reader.GetString(22); }
                        if (!reader.IsDBNull(23)) { myApplication.myHomeless = reader.GetString(23); }
                        if (!reader.IsDBNull(24)) { myApplication.myAddressSame = reader.GetString(24); }
                        if (!reader.IsDBNull(25)) { myApplication.myHispanic = reader.GetString(25); }
                        if (!reader.IsDBNull(26)) { myApplication.myRace = reader.GetString(26); }
                        if (!reader.IsDBNull(27)) { myApplication.mySSN = reader.GetString(27); }                        
                        if (!reader.IsDBNull(28)) { myApplication.myCitizen = reader.GetString(28); }
                        if (myApplication.mySSN == "Yes")
                        {
                            myApplication.mySSNNum = myAccountCreate.mySSN; //auto generated
                        }
                        else
                        {
                            myApplication.mySSNNum = null;
                        }
                        
                        if (!reader.IsDBNull(30)) { myApplication.myHouseholdOther = reader.GetString(30); }
                        if (!reader.IsDBNull(31)) { myApplication.myDependants = reader.GetString(31); }
                        if (!reader.IsDBNull(32)) { myApplication.myIncomeYN = reader.GetString(32); }
                        if (!reader.IsDBNull(33)) { myApplication.myIncomeType = reader.GetString(33); }
                        if (!reader.IsDBNull(34)) { myApplication.myIncomeAmount = reader.GetString(34); }
                        if (!reader.IsDBNull(35)) { myApplication.myIncomeFrequency = reader.GetString(35); }
                        if (!reader.IsDBNull(36)) { myApplication.myIncomeMore = reader.GetString(36); }
                        if (!reader.IsDBNull(37)) { myApplication.myIncomeEmployer = reader.GetString(37); }
                        if (!reader.IsDBNull(38)) { myApplication.myIncomeSeasonal = reader.GetString(38); }
                        if (!reader.IsDBNull(39)) { myApplication.myIncomeReduced = reader.GetString(39); }
                        if (!reader.IsDBNull(40)) { myApplication.myIncomeAdjusted = reader.GetString(40); }
                        if (!reader.IsDBNull(41)) { myApplication.myIncomeExpected = reader.GetString(41); }
                        if (!reader.IsDBNull(42)) { myApplication.myEnrollmentPlanType = reader.GetString(42); }
                        if (!reader.IsDBNull(43)) { myApplication.myFosterCare = reader.GetString(43); }
                        if (!reader.IsDBNull(44)) { myApplication.myMailingAddressYN = reader.GetString(44); }
                        if (!reader.IsDBNull(45)) { myApplication.myTribeName = reader.GetString(45); }
                        if (!reader.IsDBNull(46)) { myApplication.myLiveRes = reader.GetString(46); }
                        if (!reader.IsDBNull(47)) { myApplication.myTribeId = reader.GetString(47); }
                        if (!reader.IsDBNull(48)) { myApplication.myFederalTribe = reader.GetString(48); }
                        if (!reader.IsDBNull(49)) { myApplication.myMilitary = reader.GetString(49); }
                        if (!reader.IsDBNull(50)) { myApplication.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(50)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(51)) { myApplication.myAppliedSSN = reader.GetString(51); }
                        if (!reader.IsDBNull(52)) { myApplication.myWhyNoSSN = reader.GetString(52); }
                        if (!reader.IsDBNull(53)) { myApplication.myAssistSSN = reader.GetString(53); }
                        if (!reader.IsDBNull(54)) { myApplication.myOtherIns = reader.GetString(54); }
                        if (!reader.IsDBNull(55)) { myApplication.myKindIns = reader.GetString(55); }
                        if (!reader.IsDBNull(56)) { myApplication.myCoverageEnd = reader.GetString(56); }
                        if (!reader.IsDBNull(57)) { myApplication.myAddIns = reader.GetString(57); }
                        if (!reader.IsDBNull(58)) { myApplication.myESC = reader.GetString(58); }
                        if (!reader.IsDBNull(59)) { myApplication.myRenewalCov = reader.GetString(59); }
                        if (!reader.IsDBNull(60)) { myApplication.myWithDiscounts = reader.GetString(60); }
                        if (!reader.IsDBNull(61)) { myApplication.myIsPregnant = reader.GetString(61); }
                        if (!reader.IsDBNull(62)) { myApplication.myChildren = reader.GetString(62); }
                        if (!reader.IsDBNull(63)) { myApplication.myDueDate = Convert.ToDateTime(reader.GetDateTime(63)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(64)) { myApplication.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(64)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(65)) { myApplication.myRegDate = Convert.ToDateTime(reader.GetDateTime(65)).ToString("MM/dd/yyyy"); }
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
                        myHouseholdMembers.HouseMembersID = reader.GetInt32(0); 
                        if (!reader.IsDBNull(2)) { myHouseholdMembers.myFirstName = reader.GetString(2); }
                        if (!reader.IsDBNull(3)) { myHouseholdMembers.myMiddleName = reader.GetString(3); }
                        if (!reader.IsDBNull(4)) { myHouseholdMembers.myLastName = reader.GetString(4); }
                        if (!reader.IsDBNull(5)) { myHouseholdMembers.mySuffix = reader.GetString(5); }
                        if (!reader.IsDBNull(6)) { myHouseholdMembers.myGender = reader.GetString(6); }
                        if (!reader.IsDBNull(7)) { myHouseholdMembers.myMaritalStatus = reader.GetString(7); }
                        if (!reader.IsDBNull(8)) { myHouseholdMembers.myDOB = reader.GetString(8); }
                        if (!reader.IsDBNull(9)) { myHouseholdMembers.myLiveWithYou = reader.GetString(9); }
                        if (!reader.IsDBNull(10)) { myHouseholdMembers.myMNHome = reader.GetString(10); }                       
                        if (!reader.IsDBNull(11)) { myHouseholdMembers.myPersonHighlighted = reader.GetString(11); }
                        if (!reader.IsDBNull(12)) { myHouseholdMembers.myLiveInMN = reader.GetString(12); }
                        if (!reader.IsDBNull(13)) { myHouseholdMembers.myTempAbsentMN = reader.GetString(13); }
                        if (!reader.IsDBNull(14)) { myHouseholdMembers.myHomeless = reader.GetString(14); }
                        if (!reader.IsDBNull(15)) { myHouseholdMembers.myPlanMakeMNHome = reader.GetString(15); }
                        if (!reader.IsDBNull(16)) { myHouseholdMembers.mySeekEmplMN = reader.GetString(16); }
                        if (!reader.IsDBNull(17)) { myHouseholdMembers.myHispanic = reader.GetString(17); }
                        if (!reader.IsDBNull(18)) { myHouseholdMembers.myRace = reader.GetString(18); }
                        if (!reader.IsDBNull(19)) { myHouseholdMembers.myHaveSSN = reader.GetString(19); }
                        if (!reader.IsDBNull(20)) { myHouseholdMembers.mySSN = reader.GetString(20); }//auto generated
                        if (!reader.IsDBNull(21)) { myHouseholdMembers.myUSCitizen = reader.GetString(21); }
                        if (!reader.IsDBNull(22)) { myHouseholdMembers.myUSNational = reader.GetString(22); }
                        if (!reader.IsDBNull(23)) { myHouseholdMembers.myIsPregnant = reader.GetString(23); }
                        if (!reader.IsDBNull(24)) { myHouseholdMembers.myBeenInFosterCare = reader.GetString(24); }
                        if (!reader.IsDBNull(25)) { myHouseholdMembers.myRelationship = reader.GetString(25); }
                        if (!reader.IsDBNull(26)) { myHouseholdMembers.myHasIncome = reader.GetString(26); }
                        if (!reader.IsDBNull(27)) { myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(27); }
                        if (!reader.IsDBNull(28)) { myHouseholdMembers.myTribeName = reader.GetString(28); }
                        if (!reader.IsDBNull(29)) { myHouseholdMembers.myLiveRes = reader.GetString(29); }
                        if (!reader.IsDBNull(30)) { myHouseholdMembers.myTribeId = reader.GetString(30); }
                        if (!reader.IsDBNull(31)) { myHouseholdMembers.myFederalTribe = reader.GetString(31); }
                        if (!reader.IsDBNull(32)) { myHouseholdMembers.myFileJointly = reader.GetString(32); }
                        if (!reader.IsDBNull(33)) { myHouseholdMembers.myIncomeType = reader.GetString(33); }
                        if (!reader.IsDBNull(34)) { myHouseholdMembers.myIncomeEmployer = reader.GetString(34); }
                        if (!reader.IsDBNull(35)) { myHouseholdMembers.myIncomeSeasonal = reader.GetString(35); }
                        if (!reader.IsDBNull(36)) { myHouseholdMembers.myIncomeAmount = reader.GetString(36); }
                        if (!reader.IsDBNull(37)) { myHouseholdMembers.myIncomeFrequency = reader.GetString(37); }
                        if (!reader.IsDBNull(38)) { myHouseholdMembers.myIncomeMore = reader.GetString(38); }
                        if (!reader.IsDBNull(39)) { myHouseholdMembers.myIncomeReduced = reader.GetString(39); }
                        if (!reader.IsDBNull(40)) { myHouseholdMembers.myIncomeAdjusted = reader.GetString(40); }
                        if (!reader.IsDBNull(41)) { myHouseholdMembers.myIncomeExpected = reader.GetString(41); }
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); }
                        if (!reader.IsDBNull(43)) { myHouseholdMembers.myMilitary = reader.GetString(43); }
                        if (!reader.IsDBNull(44)) { myHouseholdMembers.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(44)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(45)) { myHouseholdMembers.myPrefContact = reader.GetString(45); }
                        if (!reader.IsDBNull(46)) { myHouseholdMembers.myPhoneNum = reader.GetString(46); }
                        if (!reader.IsDBNull(47)) { myHouseholdMembers.myPhoneType = reader.GetString(47); }
                        if (!reader.IsDBNull(48)) { myHouseholdMembers.myAltNum = reader.GetString(48); }
                        if (!reader.IsDBNull(49)) { myHouseholdMembers.myAltNumType = reader.GetString(49); }
                        if (!reader.IsDBNull(50)) { myHouseholdMembers.myEmail = reader.GetString(50); }
                        if (!reader.IsDBNull(51)) { myHouseholdMembers.myVoterCard = reader.GetString(51); }
                        if (!reader.IsDBNull(52)) { myHouseholdMembers.myNotices = reader.GetString(52); }
                        if (!reader.IsDBNull(53)) { myHouseholdMembers.myAuthRep = reader.GetString(53); }
                        if (!reader.IsDBNull(54)) { myHouseholdMembers.myDependants = reader.GetString(54); }
                        if (!reader.IsDBNull(55)) { myHouseholdMembers.myTaxFiler = reader.GetString(55); }
                        if (!reader.IsDBNull(56)) { myHouseholdMembers.myChildren = reader.GetString(56); }
                        if (!reader.IsDBNull(57)) { myHouseholdMembers.myDueDate = Convert.ToDateTime(reader.GetDateTime(57)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(58)) { myHouseholdMembers.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(58)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(59)) { myHouseholdMembers.myReEnroll = reader.GetString(59); }
                        if (!reader.IsDBNull(60)) { myHouseholdMembers.mySaveExit = reader.GetString(60); }
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
                            if (!reader.IsDBNull(3)) { myApplication.myHomeAddress1 = reader.GetString(3); }
                            if (!reader.IsDBNull(4)) { myApplication.myHomeAddress2 = reader.GetString(4); }
                            if (!reader.IsDBNull(5)) { myApplication.myHomeCity = reader.GetString(5); }
                            if (!reader.IsDBNull(6)) { myApplication.myHomeState = reader.GetString(6); }
                            if (!reader.IsDBNull(7)) { myApplication.myHomeZip = reader.GetString(7); }
                            if (!reader.IsDBNull(8)) { myApplication.myHomeZip4 = reader.GetString(8); }
                            if (!reader.IsDBNull(10)) { myApplication.myHomeCounty = reader.GetString(10); }
                            if (!reader.IsDBNull(11)) { myApplication.myHomeAptSuite = reader.GetString(11); }
                        }
                        else if (reader.GetString(9) == "Household 2")
                        {
                            if (!reader.IsDBNull(3)) { myHouseholdMembers.myMailAddress1 = reader.GetString(3); }
                            if (!reader.IsDBNull(4)) { myHouseholdMembers.myMailAddress2 = reader.GetString(4); }
                            if (!reader.IsDBNull(5)) { myHouseholdMembers.myMailCity = reader.GetString(5); }
                            if (!reader.IsDBNull(6)) { myHouseholdMembers.myMailState = reader.GetString(6); }
                            if (!reader.IsDBNull(7)) { myHouseholdMembers.myMailZip = reader.GetString(7); }
                            if (!reader.IsDBNull(10)) { myHouseholdMembers.myMailCounty = reader.GetString(10); }
                            if (!reader.IsDBNull(11)) { myHouseholdMembers.myMailAptSuite = reader.GetString(11); }
                        }                        
                        else
                        {
                            if (!reader.IsDBNull(3)) { myApplication.myMailAddress1 = reader.GetString(3); }
                            if (!reader.IsDBNull(4)) { myApplication.myMailAddress2 = reader.GetString(4); }
                            if (!reader.IsDBNull(5)) { myApplication.myMailCity = reader.GetString(5); }
                            if (!reader.IsDBNull(6)) { myApplication.myMailState = reader.GetString(6); }
                            if (!reader.IsDBNull(7)) { myApplication.myMailZip = reader.GetString(7); }
                            if (!reader.IsDBNull(8)) { myApplication.myMailZip4 = reader.GetString(8); }
                            if (!reader.IsDBNull(10)) { myApplication.myMailCounty = reader.GetString(10); }
                            if (!reader.IsDBNull(11)) { myApplication.myMailAptSuite = reader.GetString(11); }
                        }
                    }
                }

                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fill all structures didn't work " + e);
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
                        myHouseholdMembers.HouseMembersID = reader.GetInt32(0); 
                        if (!reader.IsDBNull(2)) { myHouseholdMembers.myFirstName = reader.GetString(2); }
                        if (!reader.IsDBNull(3)) { myHouseholdMembers.myMiddleName = reader.GetString(3); }
                        if (!reader.IsDBNull(4)) { myHouseholdMembers.myLastName = reader.GetString(4); }
                        if (!reader.IsDBNull(5)) { myHouseholdMembers.mySuffix = reader.GetString(5); }
                        if (!reader.IsDBNull(6)) { myHouseholdMembers.myGender = reader.GetString(6); }
                        if (!reader.IsDBNull(7)) { myHouseholdMembers.myMaritalStatus = reader.GetString(7); }
                        if (!reader.IsDBNull(8)) { myHouseholdMembers.myDOB = reader.GetString(8); }
                        if (!reader.IsDBNull(9)) { myHouseholdMembers.myLiveWithYou = reader.GetString(9); }
                        if (!reader.IsDBNull(10)) { myHouseholdMembers.myMNHome = reader.GetString(10); }                     
                        if (!reader.IsDBNull(11)) { myHouseholdMembers.myPersonHighlighted = reader.GetString(11); }
                        if (!reader.IsDBNull(12)) { myHouseholdMembers.myLiveInMN = reader.GetString(12); }
                        if (!reader.IsDBNull(13)) { myHouseholdMembers.myTempAbsentMN = reader.GetString(13); }
                        if (!reader.IsDBNull(14)) { myHouseholdMembers.myHomeless = reader.GetString(14); }
                        if (!reader.IsDBNull(15)) { myHouseholdMembers.myPlanMakeMNHome = reader.GetString(15); }
                        if (!reader.IsDBNull(16)) { myHouseholdMembers.mySeekEmplMN = reader.GetString(16); }
                        if (!reader.IsDBNull(17)) { myHouseholdMembers.myHispanic = reader.GetString(17); }
                        if (!reader.IsDBNull(18)) { myHouseholdMembers.myRace = reader.GetString(18); }
                        if (!reader.IsDBNull(19)) { myHouseholdMembers.myHaveSSN = reader.GetString(19); }
                        //if (!reader.IsDBNull(20)) { myHouseholdMembers.mySSN = reader.GetString(20); }//auto generated
                        if (!reader.IsDBNull(21)) { myHouseholdMembers.myUSCitizen = reader.GetString(21); }
                        if (!reader.IsDBNull(22)) { myHouseholdMembers.myUSNational = reader.GetString(22); }
                        if (!reader.IsDBNull(23)) { myHouseholdMembers.myIsPregnant = reader.GetString(23); }
                        if (!reader.IsDBNull(24)) { myHouseholdMembers.myBeenInFosterCare = reader.GetString(24); }
                        if (!reader.IsDBNull(25)) { myHouseholdMembers.myRelationship = reader.GetString(25); }
                        if (!reader.IsDBNull(26)) { myHouseholdMembers.myHasIncome = reader.GetString(26); }
                        if (!reader.IsDBNull(27)) { myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(27); }
                        if (!reader.IsDBNull(28)) { myHouseholdMembers.myTribeName = reader.GetString(28); }
                        if (!reader.IsDBNull(29)) { myHouseholdMembers.myLiveRes = reader.GetString(29); }
                        if (!reader.IsDBNull(30)) { myHouseholdMembers.myTribeId = reader.GetString(30); }
                        if (!reader.IsDBNull(31)) { myHouseholdMembers.myFederalTribe = reader.GetString(31); }
                        if (!reader.IsDBNull(32)) { myHouseholdMembers.myFileJointly = reader.GetString(32); }
                        if (!reader.IsDBNull(33)) { myHouseholdMembers.myIncomeType = reader.GetString(33); }
                        if (!reader.IsDBNull(34)) { myHouseholdMembers.myIncomeEmployer = reader.GetString(34); }
                        if (!reader.IsDBNull(35)) { myHouseholdMembers.myIncomeSeasonal = reader.GetString(35); }
                        if (!reader.IsDBNull(36)) { myHouseholdMembers.myIncomeAmount = reader.GetString(36); }
                        if (!reader.IsDBNull(37)) { myHouseholdMembers.myIncomeFrequency = reader.GetString(37); }
                        if (!reader.IsDBNull(38)) { myHouseholdMembers.myIncomeMore = reader.GetString(38); }
                        if (!reader.IsDBNull(39)) { myHouseholdMembers.myIncomeReduced = reader.GetString(39); }
                        if (!reader.IsDBNull(40)) { myHouseholdMembers.myIncomeAdjusted = reader.GetString(40); }
                        if (!reader.IsDBNull(41)) { myHouseholdMembers.myIncomeExpected = reader.GetString(41); }
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); }
                        if (!reader.IsDBNull(43)) { myHouseholdMembers.myMilitary = reader.GetString(43); }
                        if (!reader.IsDBNull(44)) { myHouseholdMembers.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(44)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(45)) { myHouseholdMembers.myPrefContact = reader.GetString(45); }
                        if (!reader.IsDBNull(46)) { myHouseholdMembers.myPhoneNum = reader.GetString(46); }
                        if (!reader.IsDBNull(47)) { myHouseholdMembers.myPhoneType = reader.GetString(47); }
                        if (!reader.IsDBNull(48)) { myHouseholdMembers.myAltNum = reader.GetString(48); }
                        if (!reader.IsDBNull(49)) { myHouseholdMembers.myAltNumType = reader.GetString(49); }
                        if (!reader.IsDBNull(50)) { myHouseholdMembers.myEmail = reader.GetString(50); }
                        if (!reader.IsDBNull(51)) { myHouseholdMembers.myVoterCard = reader.GetString(51); }
                        if (!reader.IsDBNull(52)) { myHouseholdMembers.myNotices = reader.GetString(52); }
                        if (!reader.IsDBNull(53)) { myHouseholdMembers.myAuthRep = reader.GetString(53); }
                        if (!reader.IsDBNull(54)) { myHouseholdMembers.myDependants = reader.GetString(54); }
                        if (!reader.IsDBNull(55)) { myHouseholdMembers.myTaxFiler = reader.GetString(55); }
                        if (!reader.IsDBNull(56)) { myHouseholdMembers.myChildren = reader.GetString(56); }
                        if (!reader.IsDBNull(57)) { myHouseholdMembers.myDueDate = Convert.ToDateTime(reader.GetDateTime(57)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(58)) { myHouseholdMembers.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(58)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(59)) { myHouseholdMembers.myReEnroll = reader.GetString(59); }
                        if (!reader.IsDBNull(60)) { myHouseholdMembers.mySaveExit = reader.GetString(60); }
                    }
                }

                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fill household members structure didn't work " + e);
                return 1;
            }

        }

        public int doFillNextHMStructures(ref mystructApplication myApplication, ref mystructHouseholdMembers myHouseholdMembers, ref mystructHistoryInfo myHistoryInfo, String myHouseMembersID)
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
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM HouseMembers where TestID = " + myHistoryInfo.myTestId + " and HouseMembersID = " + myHouseMembersID, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        myHouseholdMembers.HouseMembersID = reader.GetInt32(0); 
                        if (!reader.IsDBNull(2)) { myHouseholdMembers.myFirstName = reader.GetString(2); }
                        if (!reader.IsDBNull(3)) { myHouseholdMembers.myMiddleName = reader.GetString(3); }
                        if (!reader.IsDBNull(4)) { myHouseholdMembers.myLastName = reader.GetString(4); }
                        if (!reader.IsDBNull(5)) { myHouseholdMembers.mySuffix = reader.GetString(5); }
                        if (!reader.IsDBNull(6)) { myHouseholdMembers.myGender = reader.GetString(6); }
                        if (!reader.IsDBNull(7)) { myHouseholdMembers.myMaritalStatus = reader.GetString(7); }
                        if (!reader.IsDBNull(8)) { myHouseholdMembers.myDOB = reader.GetString(8); }
                        if (!reader.IsDBNull(9)) { myHouseholdMembers.myLiveWithYou = reader.GetString(9); }
                        if (!reader.IsDBNull(10)) { myHouseholdMembers.myMNHome = reader.GetString(10); }                    
                        if (!reader.IsDBNull(11)) { myHouseholdMembers.myPersonHighlighted = reader.GetString(11); }
                        if (!reader.IsDBNull(12)) { myHouseholdMembers.myLiveInMN = reader.GetString(12); }
                        if (!reader.IsDBNull(13)) { myHouseholdMembers.myTempAbsentMN = reader.GetString(13); }
                        if (!reader.IsDBNull(14)) { myHouseholdMembers.myHomeless = reader.GetString(14); }
                        if (!reader.IsDBNull(15)) { myHouseholdMembers.myPlanMakeMNHome = reader.GetString(15); }
                        if (!reader.IsDBNull(16)) { myHouseholdMembers.mySeekEmplMN = reader.GetString(16); }
                        if (!reader.IsDBNull(17)) { myHouseholdMembers.myHispanic = reader.GetString(17); }
                        if (!reader.IsDBNull(18)) { myHouseholdMembers.myRace = reader.GetString(18); }
                        if (!reader.IsDBNull(19)) { myHouseholdMembers.myHaveSSN = reader.GetString(19); }
                        //if (!reader.IsDBNull(20)) { myHouseholdMembers.mySSN = reader.GetString(20); }//auto generated
                        if (!reader.IsDBNull(21)) { myHouseholdMembers.myUSCitizen = reader.GetString(21); }
                        if (!reader.IsDBNull(22)) { myHouseholdMembers.myUSNational = reader.GetString(22); }
                        if (!reader.IsDBNull(23)) { myHouseholdMembers.myIsPregnant = reader.GetString(23); }
                        if (!reader.IsDBNull(24)) { myHouseholdMembers.myBeenInFosterCare = reader.GetString(24); }
                        if (!reader.IsDBNull(25)) { myHouseholdMembers.myRelationship = reader.GetString(25); }
                        if (!reader.IsDBNull(26)) { myHouseholdMembers.myHasIncome = reader.GetString(26); }
                        if (!reader.IsDBNull(27)) { myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(27); }
                        if (!reader.IsDBNull(28)) { myHouseholdMembers.myTribeName = reader.GetString(28); }
                        if (!reader.IsDBNull(29)) { myHouseholdMembers.myLiveRes = reader.GetString(29); }
                        if (!reader.IsDBNull(30)) { myHouseholdMembers.myTribeId = reader.GetString(30); }
                        if (!reader.IsDBNull(31)) { myHouseholdMembers.myFederalTribe = reader.GetString(31); }
                        if (!reader.IsDBNull(32)) { myHouseholdMembers.myFileJointly = reader.GetString(32); }
                        if (!reader.IsDBNull(33)) { myHouseholdMembers.myIncomeType = reader.GetString(33); }
                        if (!reader.IsDBNull(34)) { myHouseholdMembers.myIncomeEmployer = reader.GetString(34); }
                        if (!reader.IsDBNull(35)) { myHouseholdMembers.myIncomeSeasonal = reader.GetString(35); }
                        if (!reader.IsDBNull(36)) { myHouseholdMembers.myIncomeAmount = reader.GetString(36); }
                        if (!reader.IsDBNull(37)) { myHouseholdMembers.myIncomeFrequency = reader.GetString(37); }
                        if (!reader.IsDBNull(38)) { myHouseholdMembers.myIncomeMore = reader.GetString(38); }
                        if (!reader.IsDBNull(39)) { myHouseholdMembers.myIncomeReduced = reader.GetString(39); }
                        if (!reader.IsDBNull(40)) { myHouseholdMembers.myIncomeAdjusted = reader.GetString(40); }
                        if (!reader.IsDBNull(41)) { myHouseholdMembers.myIncomeExpected = reader.GetString(41); }
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); }
                        if (!reader.IsDBNull(43)) { myHouseholdMembers.myMilitary = reader.GetString(43); }
                        if (!reader.IsDBNull(44)) { myHouseholdMembers.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(44)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(45)) { myHouseholdMembers.myPrefContact = reader.GetString(45); }
                        if (!reader.IsDBNull(46)) { myHouseholdMembers.myPhoneNum = reader.GetString(46); }
                        if (!reader.IsDBNull(47)) { myHouseholdMembers.myPhoneType = reader.GetString(47); }
                        if (!reader.IsDBNull(48)) { myHouseholdMembers.myAltNum = reader.GetString(48); }
                        if (!reader.IsDBNull(49)) { myHouseholdMembers.myAltNumType = reader.GetString(49); }
                        if (!reader.IsDBNull(50)) { myHouseholdMembers.myEmail = reader.GetString(50); }
                        if (!reader.IsDBNull(51)) { myHouseholdMembers.myVoterCard = reader.GetString(51); }
                        if (!reader.IsDBNull(52)) { myHouseholdMembers.myNotices = reader.GetString(52); }
                        if (!reader.IsDBNull(53)) { myHouseholdMembers.myAuthRep = reader.GetString(53); }
                        if (!reader.IsDBNull(54)) { myHouseholdMembers.myDependants = reader.GetString(54); }
                        if (!reader.IsDBNull(55)) { myHouseholdMembers.myTaxFiler = reader.GetString(55); }
                        if (!reader.IsDBNull(56)) { myHouseholdMembers.myChildren = reader.GetString(56); }
                        if (!reader.IsDBNull(57)) { myHouseholdMembers.myDueDate = Convert.ToDateTime(reader.GetDateTime(57)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(58)) { myHouseholdMembers.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(58)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(59)) { myHouseholdMembers.myReEnroll = reader.GetString(59); }
                        if (!reader.IsDBNull(60)) { myHouseholdMembers.mySaveExit = reader.GetString(60); }
                    }
                }

                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fill household members structure didn't work " + e);
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
                MessageBox.Show("Read account didn't work " + e);
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
                        if (myApplication.mySSN == "Yes")
                        {
                            com3.Parameters.AddWithValue("SSN", myAccountCreate.mySSN);
                        } 
                        else
                        {
                            com3.Parameters.AddWithValue("SSN", DBNull.Value);
                        }
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
                catch (Exception e)
                {
                    MessageBox.Show("Add New Account didn't work " + e);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Get next Account_id didn't work " + e);
            }

            return 1;
        }

        public int doGetHouseholdMember(ref mystructHouseholdMembers myHouseholdMembers, ref mystructHistoryInfo myHistoryInfo, String myTestId)
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
                    ("SELECT * FROM HouseMembers where TestId = " + myTestId + " and HouseMembersID = " +
                    myHouseholdMembers.HouseMembersID + ";", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myHouseholdMembers.HouseMembersID = reader.GetInt32(0);
                        if (!reader.IsDBNull(2)) { myHouseholdMembers.myFirstName = reader.GetString(2); }
                        if (!reader.IsDBNull(3)) { myHouseholdMembers.myMiddleName = reader.GetString(3); }
                        if (!reader.IsDBNull(4)) { myHouseholdMembers.myLastName = reader.GetString(4); }
                        if (!reader.IsDBNull(5)) { myHouseholdMembers.mySuffix = reader.GetString(5); }
                        if (!reader.IsDBNull(6)) { myHouseholdMembers.myGender = reader.GetString(6); }
                        if (!reader.IsDBNull(7)) { myHouseholdMembers.myMaritalStatus = reader.GetString(7); }
                        if (!reader.IsDBNull(8)) { myHouseholdMembers.myDOB = reader.GetString(8); }
                        if (!reader.IsDBNull(9)) { myHouseholdMembers.myLiveWithYou = reader.GetString(9); }
                        if (!reader.IsDBNull(10)) { myHouseholdMembers.myMNHome = reader.GetString(10); }                      
                        if (!reader.IsDBNull(11)) { myHouseholdMembers.myPersonHighlighted = reader.GetString(11); }
                        if (!reader.IsDBNull(12)) { myHouseholdMembers.myLiveInMN = reader.GetString(12); }
                        if (!reader.IsDBNull(13)) { myHouseholdMembers.myTempAbsentMN = reader.GetString(13); }
                        if (!reader.IsDBNull(14)) { myHouseholdMembers.myHomeless = reader.GetString(14); }
                        if (!reader.IsDBNull(15)) { myHouseholdMembers.myPlanMakeMNHome = reader.GetString(15); }
                        if (!reader.IsDBNull(16)) { myHouseholdMembers.mySeekEmplMN = reader.GetString(16); }
                        if (!reader.IsDBNull(17)) { myHouseholdMembers.myHispanic = reader.GetString(17); }
                        if (!reader.IsDBNull(18)) { myHouseholdMembers.myRace = reader.GetString(18); }
                        if (!reader.IsDBNull(19)) { myHouseholdMembers.myHaveSSN = reader.GetString(19); }
                        //if (!reader.IsDBNull(20)) { myHouseholdMembers.mySSN = reader.GetString(20); }//auto generated
                        if (!reader.IsDBNull(21)) { myHouseholdMembers.myUSCitizen = reader.GetString(21); }
                        if (!reader.IsDBNull(22)) { myHouseholdMembers.myUSNational = reader.GetString(22); }
                        if (!reader.IsDBNull(23)) { myHouseholdMembers.myIsPregnant = reader.GetString(23); }
                        if (!reader.IsDBNull(24)) { myHouseholdMembers.myBeenInFosterCare = reader.GetString(24); }
                        if (!reader.IsDBNull(25)) { myHouseholdMembers.myRelationship = reader.GetString(25); }
                        if (!reader.IsDBNull(26)) { myHouseholdMembers.myHasIncome = reader.GetString(26); }
                        if (!reader.IsDBNull(27)) { myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(27); }
                        if (!reader.IsDBNull(28)) { myHouseholdMembers.myTribeName = reader.GetString(28); }
                        if (!reader.IsDBNull(29)) { myHouseholdMembers.myLiveRes = reader.GetString(29); }
                        if (!reader.IsDBNull(30)) { myHouseholdMembers.myTribeId = reader.GetString(30); }
                        if (!reader.IsDBNull(31)) { myHouseholdMembers.myFederalTribe = reader.GetString(31); }
                        if (!reader.IsDBNull(32)) { myHouseholdMembers.myFileJointly = reader.GetString(32); }
                        if (!reader.IsDBNull(33)) { myHouseholdMembers.myIncomeType = reader.GetString(33); }
                        if (!reader.IsDBNull(34)) { myHouseholdMembers.myIncomeEmployer = reader.GetString(34); }
                        if (!reader.IsDBNull(35)) { myHouseholdMembers.myIncomeSeasonal = reader.GetString(35); }
                        if (!reader.IsDBNull(36)) { myHouseholdMembers.myIncomeAmount = reader.GetString(36); }
                        if (!reader.IsDBNull(37)) { myHouseholdMembers.myIncomeFrequency = reader.GetString(37); }
                        if (!reader.IsDBNull(38)) { myHouseholdMembers.myIncomeMore = reader.GetString(38); }
                        if (!reader.IsDBNull(39)) { myHouseholdMembers.myIncomeReduced = reader.GetString(39); }
                        if (!reader.IsDBNull(40)) { myHouseholdMembers.myIncomeAdjusted = reader.GetString(40); }
                        if (!reader.IsDBNull(41)) { myHouseholdMembers.myIncomeExpected = reader.GetString(41); }
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); }
                        if (!reader.IsDBNull(43)) { myHouseholdMembers.myMilitary = reader.GetString(43); }
                        if (!reader.IsDBNull(44)) { myHouseholdMembers.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(44)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(45)) { myHouseholdMembers.myPrefContact = reader.GetString(45); }
                        if (!reader.IsDBNull(46)) { myHouseholdMembers.myPhoneNum = reader.GetString(46); }
                        if (!reader.IsDBNull(47)) { myHouseholdMembers.myPhoneType = reader.GetString(47); }
                        if (!reader.IsDBNull(48)) { myHouseholdMembers.myAltNum = reader.GetString(48); }
                        if (!reader.IsDBNull(49)) { myHouseholdMembers.myAltNumType = reader.GetString(49); }
                        if (!reader.IsDBNull(50)) { myHouseholdMembers.myEmail = reader.GetString(50); }
                        if (!reader.IsDBNull(51)) { myHouseholdMembers.myVoterCard = reader.GetString(51); }
                        if (!reader.IsDBNull(52)) { myHouseholdMembers.myNotices = reader.GetString(52); }
                        if (!reader.IsDBNull(53)) { myHouseholdMembers.myAuthRep = reader.GetString(53); }
                        if (!reader.IsDBNull(54)) { myHouseholdMembers.myDependants = reader.GetString(54); }
                        if (!reader.IsDBNull(55)) { myHouseholdMembers.myTaxFiler = reader.GetString(55); }
                        if (!reader.IsDBNull(56)) { myHouseholdMembers.myChildren = reader.GetString(56); }
                        if (!reader.IsDBNull(57)) { myHouseholdMembers.myDueDate = Convert.ToDateTime(reader.GetDateTime(57)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(58)) { myHouseholdMembers.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(58)).ToString("MM/dd/yyyy"); }
                        if (!reader.IsDBNull(59)) { myHouseholdMembers.myReEnroll = reader.GetString(59); }
                        if (!reader.IsDBNull(60)) { myHouseholdMembers.mySaveExit = reader.GetString(60); }
                    }

                    com2.ExecuteNonQuery();
                    com2.Dispose();
                }

                //Read configured rows if exist
                using (SqlCeCommand com3 = new SqlCeCommand("SELECT * FROM Address where TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com3.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(9) == "Household 2")
                        {
                            if (!reader.IsDBNull(3)) { myHouseholdMembers.myMailAddress1 = reader.GetString(3); }
                            if (!reader.IsDBNull(4)) { myHouseholdMembers.myMailAddress2 = reader.GetString(4); }
                            if (!reader.IsDBNull(5)) { myHouseholdMembers.myMailCity = reader.GetString(5); }
                            if (!reader.IsDBNull(6)) { myHouseholdMembers.myMailState = reader.GetString(6); }
                            if (!reader.IsDBNull(7)) { myHouseholdMembers.myMailZip = reader.GetString(7); }
                            if (!reader.IsDBNull(10)) { myHouseholdMembers.myMailCounty = reader.GetString(10); }
                            if (!reader.IsDBNull(11)) { myHouseholdMembers.myMailAptSuite = reader.GetString(11); }
                        }                      
                    }
                }

                return 1;
            }
            catch
            {
                return 2;
            }
        }

    }
}
