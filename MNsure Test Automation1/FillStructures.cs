using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Data.Sql;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using Novacode;
using System.Diagnostics;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;

namespace MNsure_Regression_1
{
    class FillStructures
    {
        public int doFillStructures(mystructSelectedTest mySelectedTest, ref mystructAccountCreate myAccountCreate, ref mystructApplication myApplication, ref mystructHouseholdMembers myHouseholdMembers, ref mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                SqlCeCommand cmd6 = con.CreateCommand();
                cmd6.CommandType = CommandType.Text;

                //Read configured rows if exist
                using (SqlCeCommand com6 = new SqlCeCommand("SELECT * FROM Account where TestID = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader = com6.ExecuteReader();
                    if (reader.Read())
                    {
                        myAccountCreate.myAccountID = reader.GetInt32(0);
                        if (!reader.IsDBNull(2)) { myAccountCreate.myFirstName = reader.GetString(2); } else { myAccountCreate.myFirstName = null; }
                        if (!reader.IsDBNull(3)) { myAccountCreate.myMiddleName = reader.GetString(3); } else { myAccountCreate.myMiddleName = null; }
                        if (!reader.IsDBNull(4)) { myAccountCreate.myLastName = reader.GetString(4); } else { myAccountCreate.myLastName = null; }
                        if (!reader.IsDBNull(5)) { myAccountCreate.mySuffix = reader.GetString(5); } else { myAccountCreate.mySuffix = null; }
                        if (!reader.IsDBNull(6)) { myAccountCreate.myEmail = reader.GetString(6); } else { myAccountCreate.myEmail = null; }
                        if (!reader.IsDBNull(7)) { myAccountCreate.myPhone = reader.GetString(7); } else { myAccountCreate.myPhone = null; }
                        if (!reader.IsDBNull(8)) { myAccountCreate.mySSN = reader.GetString(8); } else { myAccountCreate.mySSN = null; }
                        if (!reader.IsDBNull(9)) { myAccountCreate.myDOB = Convert.ToDateTime(reader.GetDateTime(9)).ToString("MM/dd/yyyy"); } else { myAccountCreate.myDOB = null; }
                        if (!reader.IsDBNull(10)) { myAccountCreate.myUsername = reader.GetString(10); } else { myAccountCreate.myUsername = null; }
                        if (!reader.IsDBNull(11)) { myAccountCreate.myCaseWorkerLoginId = reader.GetString(11); } else { myAccountCreate.myCaseWorkerLoginId = null; }
                    }
                }

                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;

                //Read configured rows if exist
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Application where TestId = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myApplication.myFirstName = myAccountCreate.myFirstName;  //auto generated
                        myApplication.myMiddleName = myAccountCreate.myMiddleName;  //auto generated
                        myApplication.myLastName = myAccountCreate.myLastName;  //auto generated
                        myApplication.mySuffix = myAccountCreate.mySuffix;  //auto generated
                        myApplication.myGender = reader.GetString(6);  //auto generated and updated earlier

                        /*if (!reader.IsDBNull(8))
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
                        {*/
                        myApplication.myDOB = myAccountCreate.myDOB;
                        //}
                        myApplication.myEmail = myAccountCreate.myEmail; //auto generated

                        if (!reader.IsDBNull(7)) { myApplication.myMaritalStatus = reader.GetString(7); } else { myApplication.myMaritalStatus = null; }
                        if (!reader.IsDBNull(9)) { myApplication.myLiveMN = reader.GetString(9); } else { myApplication.myLiveMN = null; }
                        if (!reader.IsDBNull(10)) { myApplication.myPlanLiveMN = reader.GetString(10); } else { myApplication.myPlanLiveMN = null; }
                        if (!reader.IsDBNull(11)) { myApplication.myPrefContact = reader.GetString(11); } else { myApplication.myPrefContact = null; }
                        if (!reader.IsDBNull(12)) { myApplication.myPhoneNum = reader.GetString(12); } else { myApplication.myPhoneNum = null; }
                        if (!reader.IsDBNull(13)) { myApplication.myPhoneType = reader.GetString(13); } else { myApplication.myPhoneType = null; }
                        if (!reader.IsDBNull(14)) { myApplication.myAltNum = reader.GetString(14); } else { myApplication.myAltNum = null; }
                        if (!reader.IsDBNull(15)) { myApplication.myAltNumType = reader.GetString(15); } else { myApplication.myAltNumType = null; }
                        if (!reader.IsDBNull(17)) { myApplication.myLanguageMost = reader.GetString(17); } else { myApplication.myLanguageMost = null; }
                        if (!reader.IsDBNull(18)) { myApplication.myLanguageWritten = reader.GetString(18); } else { myApplication.myLanguageWritten = null; }
                        if (!reader.IsDBNull(19)) { myApplication.myVoterCard = reader.GetString(19); } else { myApplication.myVoterCard = null; }
                        if (!reader.IsDBNull(20)) { myApplication.myNotices = reader.GetString(20); } else { myApplication.myNotices = null; }
                        if (!reader.IsDBNull(21)) { myApplication.myAuthRep = reader.GetString(21); } else { myApplication.myAuthRep = null; }
                        if (!reader.IsDBNull(22)) { myApplication.myApplyYourself = reader.GetString(22); } else { myApplication.myApplyYourself = null; }
                        if (!reader.IsDBNull(23)) { myApplication.myHomeless = reader.GetString(23); } else { myApplication.myHomeless = null; }
                        if (!reader.IsDBNull(24)) { myApplication.myAddressSame = reader.GetString(24); } else { myApplication.myAddressSame = null; }
                        if (!reader.IsDBNull(25)) { myApplication.myHispanic = reader.GetString(25); } else { myApplication.myHispanic = null; }
                        if (!reader.IsDBNull(26)) { myApplication.myRace = reader.GetString(26); } else { myApplication.myRace = null; }
                        if (!reader.IsDBNull(27)) { myApplication.mySSN = reader.GetString(27); } else { myApplication.mySSN = null; }
                        if (!reader.IsDBNull(28)) { myApplication.myCitizen = reader.GetString(28); } else { myApplication.myCitizen = null; }
                        if (myApplication.mySSN == "Yes")
                        {
                            myApplication.mySSNNum = myAccountCreate.mySSN; //auto generated
                        }
                        else
                        {
                            myApplication.mySSNNum = null;
                        }

                        if (!reader.IsDBNull(30)) { myApplication.myHouseholdOther = reader.GetString(30); } else { myApplication.myHouseholdOther = null; }
                        if (!reader.IsDBNull(31)) { myApplication.myDependants = reader.GetString(31); } else { myApplication.myDependants = null; }
                        if (!reader.IsDBNull(32)) { myApplication.myIncomeYN = reader.GetString(32); } else { myApplication.myIncomeYN = null; }
                        if (!reader.IsDBNull(33)) { myApplication.myIncomeType = reader.GetString(33); } else { myApplication.myIncomeType = null; }
                        if (!reader.IsDBNull(34)) { myApplication.myIncomeAmount = reader.GetString(34); } else { myApplication.myIncomeAmount = null; }
                        if (!reader.IsDBNull(35)) { myApplication.myIncomeFrequency = reader.GetString(35); } else { myApplication.myIncomeFrequency = null; }
                        if (!reader.IsDBNull(36)) { myApplication.myIncomeMore = reader.GetString(36); } else { myApplication.myIncomeMore = null; }
                        if (!reader.IsDBNull(37)) { myApplication.myIncomeEmployer = reader.GetString(37); } else { myApplication.myIncomeEmployer = null; }
                        if (!reader.IsDBNull(38)) { myApplication.myIncomeSeasonal = reader.GetString(38); } else { myApplication.myIncomeSeasonal = null; }
                        if (!reader.IsDBNull(39)) { myApplication.myIncomeReduced = reader.GetString(39); } else { myApplication.myIncomeReduced = null; }
                        if (!reader.IsDBNull(40)) { myApplication.myIncomeAdjusted = reader.GetString(40); } else { myApplication.myIncomeAdjusted = null; }
                        if (!reader.IsDBNull(41)) { myApplication.myIncomeExpected = reader.GetString(41); } else { myApplication.myIncomeExpected = null; }
                        if (!reader.IsDBNull(42)) { myApplication.myEnrollmentPlanType = reader.GetString(42); } else { myApplication.myEnrollmentPlanType = null; }
                        if (!reader.IsDBNull(43)) { myApplication.myFosterCare = reader.GetString(43); } else { myApplication.myFosterCare = null; }
                        if (!reader.IsDBNull(44)) { myApplication.myMailingAddressYN = reader.GetString(44); } else { myApplication.myMailingAddressYN = null; }
                        if (!reader.IsDBNull(45)) { myApplication.myTribeName = reader.GetString(45); } else { myApplication.myTribeName = null; }
                        if (!reader.IsDBNull(46)) { myApplication.myLiveRes = reader.GetString(46); } else { myApplication.myLiveRes = null; }
                        if (!reader.IsDBNull(47)) { myApplication.myTribeId = reader.GetString(47); } else { myApplication.myTribeId = null; }
                        if (!reader.IsDBNull(48)) { myApplication.myFederalTribe = reader.GetString(48); } else { myApplication.myFederalTribe = null; }
                        if (!reader.IsDBNull(49)) { myApplication.myMilitary = reader.GetString(49); } else { myApplication.myMilitary = null; }
                        if (!reader.IsDBNull(50)) { myApplication.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(50)).ToString("MM/dd/yyyy"); } else { myApplication.myMilitaryDate = null; }
                        if (!reader.IsDBNull(51)) { myApplication.myAppliedSSN = reader.GetString(51); } else { myApplication.myAppliedSSN = null; }
                        if (!reader.IsDBNull(52)) { myApplication.myWhyNoSSN = reader.GetString(52); } else { myApplication.myWhyNoSSN = null; }
                        if (!reader.IsDBNull(53)) { myApplication.myAssistSSN = reader.GetString(53); } else { myApplication.myAssistSSN = null; }
                        if (!reader.IsDBNull(54)) { myApplication.myOtherIns = reader.GetString(54); } else { myApplication.myOtherIns = null; }
                        if (!reader.IsDBNull(55)) { myApplication.myKindIns = reader.GetString(55); } else { myApplication.myKindIns = null; }
                        if (!reader.IsDBNull(56)) { myApplication.myCoverageEnd = reader.GetString(56); } else { myApplication.myCoverageEnd = null; }
                        if (!reader.IsDBNull(57)) { myApplication.myAddIns = reader.GetString(57); } else { myApplication.myAddIns = null; }
                        if (!reader.IsDBNull(58)) { myApplication.myESC = reader.GetString(58); } else { myApplication.myESC = null; }
                        if (!reader.IsDBNull(59)) { myApplication.myRenewalCov = reader.GetString(59); } else { myApplication.myRenewalCov = null; }
                        if (!reader.IsDBNull(60)) { myApplication.myWithDiscounts = reader.GetString(60); } else { myApplication.myWithDiscounts = null; }
                        if (!reader.IsDBNull(61)) { myApplication.myIsPregnant = reader.GetString(61); } else { myApplication.myIsPregnant = null; }
                        if (!reader.IsDBNull(62)) { myApplication.myChildren = reader.GetString(62); } else { myApplication.myChildren = null; }
                        if (!reader.IsDBNull(63)) { myApplication.myDueDate = Convert.ToDateTime(reader.GetDateTime(63)).ToString("MM/dd/yyyy"); } else { myApplication.myDueDate = null; }
                        if (!reader.IsDBNull(64)) { myApplication.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(64)).ToString("MM/dd/yyyy"); } else { myApplication.myPregnancyEnded = null; }
                        if (!reader.IsDBNull(65)) { myApplication.myRegDate = Convert.ToDateTime(reader.GetDateTime(65)).ToString("MM/dd/yyyy"); } else { myApplication.myRegDate = null; }
                        if (!reader.IsDBNull(66)) { myApplication.myDay2TestId = reader.GetString(66); } else { myApplication.myDay2TestId = null; }
                        if (!reader.IsDBNull(67)) { myApplication.myPassCount = reader.GetString(67); } else { myApplication.myPassCount = null; }
                        if (!reader.IsDBNull(68)) { myApplication.myTobacco = reader.GetString(68); } else { myApplication.myTobacco = null; }
                        if (!reader.IsDBNull(69)) { myApplication.myTobaccoLast = Convert.ToDateTime(reader.GetDateTime(69)).ToString("MM/dd/yyyy"); } else { myApplication.myTobaccoLast = null; }
                        if (!reader.IsDBNull(70)) { myApplication.myRandom = reader.GetString(70); } else { myApplication.myRandom = null; }
                        if (!reader.IsDBNull(71)) { myApplication.myHcrPassCount = reader.GetString(71); } else { myApplication.myHcrPassCount = null; }
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
                        if (!reader.IsDBNull(2)) { myHouseholdMembers.myFirstName = reader.GetString(2); } else { myHouseholdMembers.myFirstName = null; }
                        if (!reader.IsDBNull(3)) { myHouseholdMembers.myMiddleName = reader.GetString(3); } else { myHouseholdMembers.myMiddleName = null; }
                        if (!reader.IsDBNull(4)) { myHouseholdMembers.myLastName = reader.GetString(4); } else { myHouseholdMembers.myLastName = null; }
                        if (!reader.IsDBNull(5)) { myHouseholdMembers.mySuffix = reader.GetString(5); } else { myHouseholdMembers.mySuffix = null; }
                        if (!reader.IsDBNull(6)) { myHouseholdMembers.myGender = reader.GetString(6); } else { myHouseholdMembers.myGender = null; }
                        if (!reader.IsDBNull(7)) { myHouseholdMembers.myMaritalStatus = reader.GetString(7); } else { myHouseholdMembers.myMaritalStatus = null; }
                        if (!reader.IsDBNull(8)) { myHouseholdMembers.myDOB = reader.GetString(8); } else { myHouseholdMembers.myDOB = null; }
                        if (!reader.IsDBNull(9)) { myHouseholdMembers.myLiveWithYou = reader.GetString(9); } else { myHouseholdMembers.myLiveWithYou = null; }
                        if (!reader.IsDBNull(10)) { myHouseholdMembers.myMNHome = reader.GetString(10); } else { myHouseholdMembers.myMNHome = null; }
                        if (!reader.IsDBNull(11)) { myHouseholdMembers.myPersonHighlighted = reader.GetString(11); } else { myHouseholdMembers.myPersonHighlighted = null; }
                        if (!reader.IsDBNull(12)) { myHouseholdMembers.myLiveInMN = reader.GetString(12); } else { myHouseholdMembers.myLiveInMN = null; }
                        if (!reader.IsDBNull(13)) { myHouseholdMembers.myTempAbsentMN = reader.GetString(13); } else { myHouseholdMembers.myTempAbsentMN = null; }
                        if (!reader.IsDBNull(14)) { myHouseholdMembers.myHomeless = reader.GetString(14); } else { myHouseholdMembers.myHomeless = null; }
                        if (!reader.IsDBNull(15)) { myHouseholdMembers.myPlanMakeMNHome = reader.GetString(15); } else { myHouseholdMembers.myPlanMakeMNHome = null; }
                        if (!reader.IsDBNull(16)) { myHouseholdMembers.mySeekEmplMN = reader.GetString(16); } else { myHouseholdMembers.mySeekEmplMN = null; }
                        if (!reader.IsDBNull(17)) { myHouseholdMembers.myHispanic = reader.GetString(17); } else { myHouseholdMembers.myHispanic = null; }
                        if (!reader.IsDBNull(18)) { myHouseholdMembers.myRace = reader.GetString(18); } else { myHouseholdMembers.myRace = null; }
                        if (!reader.IsDBNull(19)) { myHouseholdMembers.myHaveSSN = reader.GetString(19); } else { myHouseholdMembers.myHaveSSN = null; }
                        if (!reader.IsDBNull(20)) { myHouseholdMembers.mySSN = reader.GetString(20); } else { myHouseholdMembers.mySSN = null; }
                        if (!reader.IsDBNull(21)) { myHouseholdMembers.myUSCitizen = reader.GetString(21); } else { myHouseholdMembers.myUSCitizen = null; }
                        if (!reader.IsDBNull(22)) { myHouseholdMembers.myUSNational = reader.GetString(22); } else { myHouseholdMembers.myUSNational = null; }
                        if (!reader.IsDBNull(23)) { myHouseholdMembers.myIsPregnant = reader.GetString(23); } else { myHouseholdMembers.myIsPregnant = null; }
                        if (!reader.IsDBNull(24)) { myHouseholdMembers.myBeenInFosterCare = reader.GetString(24); } else { myHouseholdMembers.myBeenInFosterCare = null; }
                        if (!reader.IsDBNull(25)) { myHouseholdMembers.myRelationship = reader.GetString(25); } else { myHouseholdMembers.myRelationship = null; }
                        if (!reader.IsDBNull(26)) { myHouseholdMembers.myHasIncome = reader.GetString(26); } else { myHouseholdMembers.myHasIncome = null; }
                        if (!reader.IsDBNull(27)) { myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(27); } else { myHouseholdMembers.myRelationshiptoNextHM = null; }
                        if (!reader.IsDBNull(28)) { myHouseholdMembers.myTribeName = reader.GetString(28); } else { myHouseholdMembers.myTribeName = null; }
                        if (!reader.IsDBNull(29)) { myHouseholdMembers.myLiveRes = reader.GetString(29); } else { myHouseholdMembers.myLiveRes = null; }
                        if (!reader.IsDBNull(30)) { myHouseholdMembers.myTribeId = reader.GetString(30); } else { myHouseholdMembers.myTribeId = null; }
                        if (!reader.IsDBNull(31)) { myHouseholdMembers.myFederalTribe = reader.GetString(31); } else { myHouseholdMembers.myFederalTribe = null; }
                        if (!reader.IsDBNull(32)) { myHouseholdMembers.myFileJointly = reader.GetString(32); } else { myHouseholdMembers.myFileJointly = null; }
                        if (!reader.IsDBNull(33)) { myHouseholdMembers.myIncomeType = reader.GetString(33); } else { myHouseholdMembers.myIncomeType = null; }
                        if (!reader.IsDBNull(34)) { myHouseholdMembers.myIncomeEmployer = reader.GetString(34); } else { myHouseholdMembers.myIncomeEmployer = null; }
                        if (!reader.IsDBNull(35)) { myHouseholdMembers.myIncomeSeasonal = reader.GetString(35); } else { myHouseholdMembers.myIncomeSeasonal = null; }
                        if (!reader.IsDBNull(36)) { myHouseholdMembers.myIncomeAmount = reader.GetString(36); } else { myHouseholdMembers.myIncomeAmount = null; }
                        if (!reader.IsDBNull(37)) { myHouseholdMembers.myIncomeFrequency = reader.GetString(37); } else { myHouseholdMembers.myIncomeFrequency = null; }
                        if (!reader.IsDBNull(38)) { myHouseholdMembers.myIncomeMore = reader.GetString(38); } else { myHouseholdMembers.myIncomeMore = null; }
                        if (!reader.IsDBNull(39)) { myHouseholdMembers.myIncomeReduced = reader.GetString(39); } else { myHouseholdMembers.myIncomeReduced = null; }
                        if (!reader.IsDBNull(40)) { myHouseholdMembers.myIncomeAdjusted = reader.GetString(40); } else { myHouseholdMembers.myIncomeAdjusted = null; }
                        if (!reader.IsDBNull(41)) { myHouseholdMembers.myIncomeExpected = reader.GetString(41); } else { myHouseholdMembers.myIncomeExpected = null; }
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); } else { myHouseholdMembers.myPassCount = null; }
                        if (!reader.IsDBNull(43)) { myHouseholdMembers.myMilitary = reader.GetString(43); } else { myHouseholdMembers.myMilitary = null; }
                        if (!reader.IsDBNull(44)) { myHouseholdMembers.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(44)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myMilitaryDate = null; }
                        if (!reader.IsDBNull(45)) { myHouseholdMembers.myPrefContact = reader.GetString(45); } else { myHouseholdMembers.myPrefContact = null; }
                        if (!reader.IsDBNull(46)) { myHouseholdMembers.myPhoneNum = reader.GetString(46); } else { myHouseholdMembers.myPhoneNum = null; }
                        if (!reader.IsDBNull(47)) { myHouseholdMembers.myPhoneType = reader.GetString(47); } else { myHouseholdMembers.myPhoneType = null; }
                        if (!reader.IsDBNull(48)) { myHouseholdMembers.myAltNum = reader.GetString(48); } else { myHouseholdMembers.myAltNum = null; }
                        if (!reader.IsDBNull(49)) { myHouseholdMembers.myAltNumType = reader.GetString(49); } else { myHouseholdMembers.myAltNumType = null; }
                        if (!reader.IsDBNull(50)) { myHouseholdMembers.myEmail = reader.GetString(50); } else { myHouseholdMembers.myEmail = null; }
                        if (!reader.IsDBNull(51)) { myHouseholdMembers.myVoterCard = reader.GetString(51); } else { myHouseholdMembers.myVoterCard = null; }
                        if (!reader.IsDBNull(52)) { myHouseholdMembers.myNotices = reader.GetString(52); } else { myHouseholdMembers.myNotices = null; }
                        if (!reader.IsDBNull(53)) { myHouseholdMembers.myAuthRep = reader.GetString(53); } else { myHouseholdMembers.myAuthRep = null; }
                        if (!reader.IsDBNull(54)) { myHouseholdMembers.myDependants = reader.GetString(54); } else { myHouseholdMembers.myDependants = null; }
                        if (!reader.IsDBNull(55)) { myHouseholdMembers.myTaxFiler = reader.GetString(55); } else { myHouseholdMembers.myTaxFiler = null; }
                        if (!reader.IsDBNull(56)) { myHouseholdMembers.myChildren = reader.GetString(56); } else { myHouseholdMembers.myChildren = null; }
                        if (!reader.IsDBNull(57)) { myHouseholdMembers.myDueDate = Convert.ToDateTime(reader.GetDateTime(57)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myDueDate = null; }
                        if (!reader.IsDBNull(58)) { myHouseholdMembers.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(58)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myPregnancyEnded = null; }
                        if (!reader.IsDBNull(59)) { myHouseholdMembers.myReEnroll = reader.GetString(59); } else { myHouseholdMembers.myReEnroll = null; }
                        if (!reader.IsDBNull(60)) { myHouseholdMembers.mySaveExit = reader.GetString(60); } else { myHouseholdMembers.mySaveExit = null; }
                        if (!reader.IsDBNull(61)) { myHouseholdMembers.myRandom = reader.GetString(61); } else { myHouseholdMembers.myRandom = null; }
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
                            if (!reader.IsDBNull(3)) { myApplication.myHomeAddress1 = reader.GetString(3); } else { myApplication.myHomeAddress1 = null; }
                            if (!reader.IsDBNull(4)) { myApplication.myHomeAddress2 = reader.GetString(4); } else { myApplication.myHomeAddress2 = null; }
                            if (!reader.IsDBNull(5)) { myApplication.myHomeCity = reader.GetString(5); } else { myApplication.myHomeCity = null; }
                            if (!reader.IsDBNull(6)) { myApplication.myHomeState = reader.GetString(6); } else { myApplication.myHomeState = null; }
                            if (!reader.IsDBNull(7)) { myApplication.myHomeZip = reader.GetString(7); } else { myApplication.myHomeZip = null; }
                            if (!reader.IsDBNull(8)) { myApplication.myHomeZip4 = reader.GetString(8); } else { myApplication.myHomeZip4 = null; }
                            if (!reader.IsDBNull(10)) { myApplication.myHomeCounty = reader.GetString(10); } else { myApplication.myHomeCounty = null; }
                            if (!reader.IsDBNull(11)) { myApplication.myHomeAptSuite = reader.GetString(11); } else { myApplication.myHomeAptSuite = null; }
                        }
                        else if (reader.GetString(9) == "Household 2")
                        {
                            if (!reader.IsDBNull(3)) { myHouseholdMembers.myMailAddress1 = reader.GetString(3); } else { myHouseholdMembers.myMailAddress1 = null; }
                            if (!reader.IsDBNull(4)) { myHouseholdMembers.myMailAddress2 = reader.GetString(4); } else { myHouseholdMembers.myMailAddress2 = null; }
                            if (!reader.IsDBNull(5)) { myHouseholdMembers.myMailCity = reader.GetString(5); } else { myHouseholdMembers.myMailCity = null; }
                            if (!reader.IsDBNull(6)) { myHouseholdMembers.myMailState = reader.GetString(6); } else { myHouseholdMembers.myMailState = null; }
                            if (!reader.IsDBNull(7)) { myHouseholdMembers.myMailZip = reader.GetString(7); } else { myHouseholdMembers.myMailZip = null; }
                            if (!reader.IsDBNull(10)) { myHouseholdMembers.myMailCounty = reader.GetString(10); } else { myHouseholdMembers.myMailCounty = null; }
                            if (!reader.IsDBNull(11)) { myHouseholdMembers.myMailAptSuite = reader.GetString(11); } else { myHouseholdMembers.myMailAptSuite = null; }
                        }
                        else if (reader.GetString(9) == "Assister")
                        {
                            if (!reader.IsDBNull(3)) { myAssister.myAddress1 = reader.GetString(3); } else { myAssister.myAddress1 = null; }
                            if (!reader.IsDBNull(4)) { myAssister.myAddress2 = reader.GetString(4); } else { myAssister.myAddress2 = null; }
                            if (!reader.IsDBNull(5)) { myAssister.myCity = reader.GetString(5); } else { myAssister.myCity = null; }
                            if (!reader.IsDBNull(6)) { myAssister.myState = reader.GetString(6); } else { myAssister.myState = null; }
                            if (!reader.IsDBNull(7)) { myAssister.myZip = reader.GetString(7); } else { myAssister.myZip = null; }
                            if (!reader.IsDBNull(10)) { myAssister.myCounty = reader.GetString(10); } else { myAssister.myCounty = null; }
                            if (!reader.IsDBNull(11)) { myAssister.myAptSuite = reader.GetString(11); } else { myAssister.myAptSuite = null; }
                        }
                        else
                        {
                            if (!reader.IsDBNull(3)) { myApplication.myMailAddress1 = reader.GetString(3); } else { myApplication.myMailAddress1 = null; }
                            if (!reader.IsDBNull(4)) { myApplication.myMailAddress2 = reader.GetString(4); } else { myApplication.myMailAddress2 = null; }
                            if (!reader.IsDBNull(5)) { myApplication.myMailCity = reader.GetString(5); } else { myApplication.myMailCity = null; }
                            if (!reader.IsDBNull(6)) { myApplication.myMailState = reader.GetString(6); } else { myApplication.myMailState = null; }
                            if (!reader.IsDBNull(7)) { myApplication.myMailZip = reader.GetString(7); } else { myApplication.myMailZip = null; }
                            if (!reader.IsDBNull(8)) { myApplication.myMailZip4 = reader.GetString(8); } else { myApplication.myMailZip4 = null; }
                            if (!reader.IsDBNull(10)) { myApplication.myMailCounty = reader.GetString(10); } else { myApplication.myMailCounty = null; }
                            if (!reader.IsDBNull(11)) { myApplication.myMailAptSuite = reader.GetString(11); } else { myApplication.myMailAptSuite = null; }
                        }
                    }

                    SqlCeCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;

                    //Read configured rows if exist
                    using (SqlCeCommand com4 = new SqlCeCommand("SELECT * FROM Assister where TestId = " + mySelectedTest.myTestId, con))
                    {
                        SqlCeDataReader reader2 = com4.ExecuteReader();
                        if (reader2.Read())
                        {
                            if (!reader2.IsDBNull(2)) { myAssister.AssisterId = reader2.GetString(2); }
                            if (!reader2.IsDBNull(3)) { myAssister.myCommunication = reader2.GetString(3); }
                            if (!reader2.IsDBNull(4)) { myAssister.myLanguage = reader2.GetString(4); }
                            if (!reader2.IsDBNull(5)) { myAssister.myMethod = reader2.GetString(5); }
                            if (!reader2.IsDBNull(6)) { myAssister.myPhoneType = reader2.GetString(6); }
                            if (!reader2.IsDBNull(7)) { myAssister.myPhoneNum = reader2.GetString(7); }
                            if (!reader2.IsDBNull(8)) { myAssister.myCategory = reader2.GetString(8); }
                            if (!reader2.IsDBNull(9)) { myAssister.myType = reader2.GetString(9); }
                            if (!reader2.IsDBNull(10)) { myAssister.myEmail = reader2.GetString(10); }
                            if (!reader2.IsDBNull(11)) { myAssister.myLastName = reader2.GetString(11); }
                            if (!reader2.IsDBNull(12)) { myAssister.myFirstName = reader2.GetString(12); }
                            if (!reader2.IsDBNull(13)) { myAssister.myRefNumber = reader2.GetString(13); }
                            if (!reader2.IsDBNull(14)) { myAssister.mySSN = reader2.GetString(14); }
                            if (!reader2.IsDBNull(15)) { myAssister.myDOB = reader2.GetDateTime(15).ToShortDateString(); }
                            if (!reader2.IsDBNull(16)) { myAssister.myRegNumber = reader2.GetString(16); }
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

        public int doFillAppCountStructures(ref mystructApplication myApplication, ref mystructHistoryInfo myHistoryInfo)
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
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Application where TestId = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(67)) { myApplication.myPassCount = reader.GetString(67); }
                    }
                }

                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fill app pass count didn't work " + e);
                return 1;
            }

        }

        public int doFillHouseholdCountStructures(ref mystructHouseholdMembers myHouseholdMembers, ref mystructHistoryInfo myHistoryInfo)
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
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM HouseMembers where TestId = " + myHistoryInfo.myTestId + " and HouseMembersID = 2", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); }
                    }
                }

                //Read configured rows if exist
                using (SqlCeCommand com3 = new SqlCeCommand("SELECT * FROM HouseMembers where TestId = " + myHistoryInfo.myTestId + " and HouseMembersID = 3", con))
                {
                    SqlCeDataReader reader = com3.ExecuteReader();
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); }
                    }
                }
                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fill household pass count didn't work " + e);
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
                        if (!reader.IsDBNull(2)) { myHouseholdMembers.myFirstName = reader.GetString(2); } else { myHouseholdMembers.myFirstName = null; }
                        if (!reader.IsDBNull(3)) { myHouseholdMembers.myMiddleName = reader.GetString(3); } else { myHouseholdMembers.myMiddleName = null; }
                        if (!reader.IsDBNull(4)) { myHouseholdMembers.myLastName = reader.GetString(4); } else { myHouseholdMembers.myLastName = null; }
                        if (!reader.IsDBNull(5)) { myHouseholdMembers.mySuffix = reader.GetString(5); } else { myHouseholdMembers.mySuffix = null; }
                        if (!reader.IsDBNull(6)) { myHouseholdMembers.myGender = reader.GetString(6); } else { myHouseholdMembers.myGender = null; }
                        if (!reader.IsDBNull(7)) { myHouseholdMembers.myMaritalStatus = reader.GetString(7); } else { myHouseholdMembers.myMaritalStatus = null; }
                        if (!reader.IsDBNull(8)) { myHouseholdMembers.myDOB = reader.GetString(8); } else { myHouseholdMembers.myDOB = null; }
                        if (!reader.IsDBNull(9)) { myHouseholdMembers.myLiveWithYou = reader.GetString(9); } else { myHouseholdMembers.myLiveWithYou = null; }
                        if (!reader.IsDBNull(10)) { myHouseholdMembers.myMNHome = reader.GetString(10); } else { myHouseholdMembers.myMNHome = null; }
                        if (!reader.IsDBNull(11)) { myHouseholdMembers.myPersonHighlighted = reader.GetString(11); } else { myHouseholdMembers.myPersonHighlighted = null; }
                        if (!reader.IsDBNull(12)) { myHouseholdMembers.myLiveInMN = reader.GetString(12); } else { myHouseholdMembers.myLiveInMN = null; }
                        if (!reader.IsDBNull(13)) { myHouseholdMembers.myTempAbsentMN = reader.GetString(13); } else { myHouseholdMembers.myTempAbsentMN = null; }
                        if (!reader.IsDBNull(14)) { myHouseholdMembers.myHomeless = reader.GetString(14); } else { myHouseholdMembers.myHomeless = null; }
                        if (!reader.IsDBNull(15)) { myHouseholdMembers.myPlanMakeMNHome = reader.GetString(15); } else { myHouseholdMembers.myPlanMakeMNHome = null; }
                        if (!reader.IsDBNull(16)) { myHouseholdMembers.mySeekEmplMN = reader.GetString(16); } else { myHouseholdMembers.mySeekEmplMN = null; }
                        if (!reader.IsDBNull(17)) { myHouseholdMembers.myHispanic = reader.GetString(17); } else { myHouseholdMembers.myHispanic = null; }
                        if (!reader.IsDBNull(18)) { myHouseholdMembers.myRace = reader.GetString(18); } else { myHouseholdMembers.myRace = null; }
                        if (!reader.IsDBNull(19)) { myHouseholdMembers.myHaveSSN = reader.GetString(19); } else { myHouseholdMembers.myHaveSSN = null; }
                        if (!reader.IsDBNull(20)) { myHouseholdMembers.mySSN = reader.GetString(20); } else { myHouseholdMembers.mySSN = null; }//auto generated
                        if (!reader.IsDBNull(21)) { myHouseholdMembers.myUSCitizen = reader.GetString(21); } else { myHouseholdMembers.myUSCitizen = null; }
                        if (!reader.IsDBNull(22)) { myHouseholdMembers.myUSNational = reader.GetString(22); } else { myHouseholdMembers.myUSNational = null; }
                        if (!reader.IsDBNull(23)) { myHouseholdMembers.myIsPregnant = reader.GetString(23); } else { myHouseholdMembers.myIsPregnant = null; }
                        if (!reader.IsDBNull(24)) { myHouseholdMembers.myBeenInFosterCare = reader.GetString(24); } else { myHouseholdMembers.myBeenInFosterCare = null; }
                        if (!reader.IsDBNull(25)) { myHouseholdMembers.myRelationship = reader.GetString(25); } else { myHouseholdMembers.myRelationship = null; }
                        if (!reader.IsDBNull(26)) { myHouseholdMembers.myHasIncome = reader.GetString(26); } else { myHouseholdMembers.myHasIncome = null; }
                        if (!reader.IsDBNull(27)) { myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(27); } else { myHouseholdMembers.myRelationshiptoNextHM = null; }
                        if (!reader.IsDBNull(28)) { myHouseholdMembers.myTribeName = reader.GetString(28); } else { myHouseholdMembers.myTribeName = null; }
                        if (!reader.IsDBNull(29)) { myHouseholdMembers.myLiveRes = reader.GetString(29); } else { myHouseholdMembers.myLiveRes = null; }
                        if (!reader.IsDBNull(30)) { myHouseholdMembers.myTribeId = reader.GetString(30); } else { myHouseholdMembers.myTribeId = null; }
                        if (!reader.IsDBNull(31)) { myHouseholdMembers.myFederalTribe = reader.GetString(31); } else { myHouseholdMembers.myFederalTribe = null; }
                        if (!reader.IsDBNull(32)) { myHouseholdMembers.myFileJointly = reader.GetString(32); } else { myHouseholdMembers.myFileJointly = null; }
                        if (!reader.IsDBNull(33)) { myHouseholdMembers.myIncomeType = reader.GetString(33); } else { myHouseholdMembers.myIncomeType = null; }
                        if (!reader.IsDBNull(34)) { myHouseholdMembers.myIncomeEmployer = reader.GetString(34); } else { myHouseholdMembers.myIncomeEmployer = null; }
                        if (!reader.IsDBNull(35)) { myHouseholdMembers.myIncomeSeasonal = reader.GetString(35); } else { myHouseholdMembers.myIncomeSeasonal = null; }
                        if (!reader.IsDBNull(36)) { myHouseholdMembers.myIncomeAmount = reader.GetString(36); } else { myHouseholdMembers.myIncomeAmount = null; }
                        if (!reader.IsDBNull(37)) { myHouseholdMembers.myIncomeFrequency = reader.GetString(37); } else { myHouseholdMembers.myIncomeFrequency = null; }
                        if (!reader.IsDBNull(38)) { myHouseholdMembers.myIncomeMore = reader.GetString(38); } else { myHouseholdMembers.myIncomeMore = null; }
                        if (!reader.IsDBNull(39)) { myHouseholdMembers.myIncomeReduced = reader.GetString(39); } else { myHouseholdMembers.myIncomeReduced = null; }
                        if (!reader.IsDBNull(40)) { myHouseholdMembers.myIncomeAdjusted = reader.GetString(40); } else { myHouseholdMembers.myIncomeAdjusted = null; }
                        if (!reader.IsDBNull(41)) { myHouseholdMembers.myIncomeExpected = reader.GetString(41); } else { myHouseholdMembers.myIncomeExpected = null; }
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); } else { myHouseholdMembers.myPassCount = null; }
                        if (!reader.IsDBNull(43)) { myHouseholdMembers.myMilitary = reader.GetString(43); } else { myHouseholdMembers.myMilitary = null; }
                        if (!reader.IsDBNull(44)) { myHouseholdMembers.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(44)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myMilitaryDate = null; }
                        if (!reader.IsDBNull(45)) { myHouseholdMembers.myPrefContact = reader.GetString(45); } else { myHouseholdMembers.myPrefContact = null; }
                        if (!reader.IsDBNull(46)) { myHouseholdMembers.myPhoneNum = reader.GetString(46); } else { myHouseholdMembers.myPhoneNum = null; }
                        if (!reader.IsDBNull(47)) { myHouseholdMembers.myPhoneType = reader.GetString(47); } else { myHouseholdMembers.myPhoneType = null; }
                        if (!reader.IsDBNull(48)) { myHouseholdMembers.myAltNum = reader.GetString(48); } else { myHouseholdMembers.myAltNum = null; }
                        if (!reader.IsDBNull(49)) { myHouseholdMembers.myAltNumType = reader.GetString(49); } else { myHouseholdMembers.myAltNumType = null; }
                        if (!reader.IsDBNull(50)) { myHouseholdMembers.myEmail = reader.GetString(50); } else { myHouseholdMembers.myEmail = null; }
                        if (!reader.IsDBNull(51)) { myHouseholdMembers.myVoterCard = reader.GetString(51); } else { myHouseholdMembers.myVoterCard = null; }
                        if (!reader.IsDBNull(52)) { myHouseholdMembers.myNotices = reader.GetString(52); } else { myHouseholdMembers.myNotices = null; }
                        if (!reader.IsDBNull(53)) { myHouseholdMembers.myAuthRep = reader.GetString(53); } else { myHouseholdMembers.myAuthRep = null; }
                        if (!reader.IsDBNull(54)) { myHouseholdMembers.myDependants = reader.GetString(54); } else { myHouseholdMembers.myDependants = null; }
                        if (!reader.IsDBNull(55)) { myHouseholdMembers.myTaxFiler = reader.GetString(55); } else { myHouseholdMembers.myTaxFiler = null; }
                        if (!reader.IsDBNull(56)) { myHouseholdMembers.myChildren = reader.GetString(56); } else { myHouseholdMembers.myChildren = null; }
                        if (!reader.IsDBNull(57)) { myHouseholdMembers.myDueDate = Convert.ToDateTime(reader.GetDateTime(57)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myDueDate = null; }
                        if (!reader.IsDBNull(58)) { myHouseholdMembers.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(58)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myPregnancyEnded = null; }
                        if (!reader.IsDBNull(59)) { myHouseholdMembers.myReEnroll = reader.GetString(59); } else { myHouseholdMembers.myReEnroll = null; }
                        if (!reader.IsDBNull(60)) { myHouseholdMembers.mySaveExit = reader.GetString(60); } else { myHouseholdMembers.mySaveExit = null; }
                        if (!reader.IsDBNull(61)) { myHouseholdMembers.myRandom = reader.GetString(61); } else { myHouseholdMembers.myRandom = null; }
                    }
                }

                SqlCeCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;

                //Read configured rows if exist
                using (SqlCeCommand com3 = new SqlCeCommand("SELECT * FROM Address where TestId = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com3.ExecuteReader();
                    while (reader.Read())
                    {
                        if (myHouseMembersID == "2" && reader.GetString(9) == "Household 2")
                        {
                            if (!reader.IsDBNull(3)) { myHouseholdMembers.myMailAddress1 = reader.GetString(3); } else { myHouseholdMembers.myMailAddress1 = null; }
                            if (!reader.IsDBNull(4)) { myHouseholdMembers.myMailAddress2 = reader.GetString(4); } else { myHouseholdMembers.myMailAddress2 = null; }
                            if (!reader.IsDBNull(5)) { myHouseholdMembers.myMailCity = reader.GetString(5); } else { myHouseholdMembers.myMailCity = null; }
                            if (!reader.IsDBNull(6)) { myHouseholdMembers.myMailState = reader.GetString(6); } else { myHouseholdMembers.myMailState = null; }
                            if (!reader.IsDBNull(7)) { myHouseholdMembers.myMailZip = reader.GetString(7); } else { myHouseholdMembers.myMailZip = null; }
                            if (!reader.IsDBNull(10)) { myHouseholdMembers.myMailCounty = reader.GetString(10); } else { myHouseholdMembers.myMailCounty = null; }
                            if (!reader.IsDBNull(11)) { myHouseholdMembers.myMailAptSuite = reader.GetString(11); } else { myHouseholdMembers.myMailAptSuite = null; }
                        }
                        else if (myHouseMembersID == "3" && reader.GetString(9) == "Household 3")
                        {
                            if (!reader.IsDBNull(3)) { myHouseholdMembers.myMailAddress1 = reader.GetString(3); } else { myHouseholdMembers.myMailAddress1 = null; }
                            if (!reader.IsDBNull(4)) { myHouseholdMembers.myMailAddress2 = reader.GetString(4); } else { myHouseholdMembers.myMailAddress2 = null; }
                            if (!reader.IsDBNull(5)) { myHouseholdMembers.myMailCity = reader.GetString(5); } else { myHouseholdMembers.myMailCity = null; }
                            if (!reader.IsDBNull(6)) { myHouseholdMembers.myMailState = reader.GetString(6); } else { myHouseholdMembers.myMailState = null; }
                            if (!reader.IsDBNull(7)) { myHouseholdMembers.myMailZip = reader.GetString(7); } else { myHouseholdMembers.myMailZip = null; }
                            if (!reader.IsDBNull(10)) { myHouseholdMembers.myMailCounty = reader.GetString(10); } else { myHouseholdMembers.myMailCounty = null; }
                            if (!reader.IsDBNull(11)) { myHouseholdMembers.myMailAptSuite = reader.GetString(11); } else { myHouseholdMembers.myMailAptSuite = null; }
                        }
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

        public int doCreateAccount(ref mystructSelectedTest mySelectedTest, ref mystructAccountCreate myAccountCreate, ref mystructApplication myApplication, ref mystructHistoryInfo myHistoryInfo)
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
                result = myAccountGeneration.GenerateNames(mySelectedTest, ref myAccountCreate, ref myApplication, ref myHistoryInfo);

                try
                {
                    con = new SqlCeConnection(conString);
                    con.Open();
                    string myInsertString;
                    myInsertString = "Insert into Account values (" + myAccountCreate.myAccountID + ", " + mySelectedTest.myTestId +
                       ", @FirstName, @MiddleName, @LastName, @Suffix, @Email, @Phone, @SSN, @DOB, @Username, @CWUsername );";
                    using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                    {
                        com3.Parameters.AddWithValue("FirstName", myAccountCreate.myFirstName);
                        if (myAccountCreate.myMiddleName != "")
                        {
                            com3.Parameters.AddWithValue("MiddleName", myAccountCreate.myMiddleName);
                        }
                        else
                        {
                            com3.Parameters.AddWithValue("MiddleName", DBNull.Value);
                        }
                        com3.Parameters.AddWithValue("LastName", myAccountCreate.myLastName);
                        com3.Parameters.AddWithValue("Suffix", DBNull.Value);//myAccountCreate.mySuffix);
                        com3.Parameters.AddWithValue("Email", myAccountCreate.myEmail);
                        com3.Parameters.AddWithValue("Phone", myAccountCreate.myPhone);
                       // if (myApplication.mySSN == "Yes")
                        //{
                            com3.Parameters.AddWithValue("SSN", myAccountCreate.mySSN);
                        /*}
                        else
                        {
                            com3.Parameters.AddWithValue("SSN", DBNull.Value);
                        }*/
                        //if (myApplication.myDOB == null)
                        //{
                        com3.Parameters.AddWithValue("DOB", myAccountCreate.myDOB);
                        /*}
                        else
                        {
                            com3.Parameters.AddWithValue("DOB", myApplication.myDOB);//this is wrong
                        }*/
                        com3.Parameters.AddWithValue("Username", myAccountCreate.myUsername);
                        if (myAccountCreate.myCaseWorkerLoginId != "" && myAccountCreate.myCaseWorkerLoginId != null)
                        {
                            com3.Parameters.AddWithValue("CWUsername", myAccountCreate.myCaseWorkerLoginId);
                        }
                        else
                        {
                            com3.Parameters.AddWithValue("CWUsername", DBNull.Value);
                        }
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

        public int doCreateAssisterAccount(ref mystructSelectedTest mySelectedTest, ref mystructAccountCreate myAccountCreate, ref mystructApplication myApplication, ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.CommandText = "Delete from Account where TestId = " + mySelectedTest.myTestId + " and AccountId = 2;";
                cmd2.ExecuteNonQuery();
                myAccountCreate.myAccountID = 2;
                int result;
                AccountGeneration myAccountGeneration = new AccountGeneration();
                result = myAccountGeneration.GenerateNames(mySelectedTest, ref myAccountCreate, ref myApplication, ref myHistoryInfo);

                try
                {
                    con = new SqlCeConnection(conString);
                    con.Open();
                    string myInsertString;
                    myInsertString = "Insert into Account values (" + myAccountCreate.myAccountID + ", " + mySelectedTest.myTestId +
                       ", @FirstName, @MiddleName, @LastName, @Suffix, @Email, @Phone, @SSN, @DOB, @Username, @CWUsername );";
                    using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                    {
                        com3.Parameters.AddWithValue("FirstName", myAccountCreate.myFirstName);
                        if (myAccountCreate.myMiddleName != "" && myAccountCreate.myMiddleName != null)
                        {
                            com3.Parameters.AddWithValue("MiddleName", myAccountCreate.myMiddleName);
                        }
                        else
                        {
                            com3.Parameters.AddWithValue("MiddleName", DBNull.Value);
                        }
                        com3.Parameters.AddWithValue("LastName", myAccountCreate.myLastName);
                        com3.Parameters.AddWithValue("Suffix", DBNull.Value);//myAccountCreate.mySuffix);
                        com3.Parameters.AddWithValue("Email", myAccountCreate.myEmail);
                        com3.Parameters.AddWithValue("Phone", myAccountCreate.myPhone);
                        if (myApplication.mySSN == "Yes")
                        {
                            com3.Parameters.AddWithValue("SSN", Convert.ToString((Convert.ToInt32(myAccountCreate.mySSN) + 1)));
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
                        if (myAccountCreate.myCaseWorkerLoginId != "" && myAccountCreate.myCaseWorkerLoginId != null)
                        {
                            com3.Parameters.AddWithValue("CWUsername", myAccountCreate.myCaseWorkerLoginId);
                        }
                        else
                        {
                            com3.Parameters.AddWithValue("CWUsername", DBNull.Value);
                        }

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
                        if (!reader.IsDBNull(2)) { myHouseholdMembers.myFirstName = reader.GetString(2); } else { myHouseholdMembers.myFirstName = null; }
                        if (!reader.IsDBNull(3)) { myHouseholdMembers.myMiddleName = reader.GetString(3); } else { myHouseholdMembers.myMiddleName = null; }
                        if (!reader.IsDBNull(4)) { myHouseholdMembers.myLastName = reader.GetString(4); } else { myHouseholdMembers.myLastName = null; }
                        if (!reader.IsDBNull(5)) { myHouseholdMembers.mySuffix = reader.GetString(5); } else { myHouseholdMembers.mySuffix = null; }
                        if (!reader.IsDBNull(6)) { myHouseholdMembers.myGender = reader.GetString(6); } else { myHouseholdMembers.myGender = null; }
                        if (!reader.IsDBNull(7)) { myHouseholdMembers.myMaritalStatus = reader.GetString(7); } else { myHouseholdMembers.myMaritalStatus = null; }
                        if (!reader.IsDBNull(8)) { myHouseholdMembers.myDOB = reader.GetString(8); } else { myHouseholdMembers.myDOB = null; }
                        if (!reader.IsDBNull(9)) { myHouseholdMembers.myLiveWithYou = reader.GetString(9); } else { myHouseholdMembers.myLiveWithYou = null; }
                        if (!reader.IsDBNull(10)) { myHouseholdMembers.myMNHome = reader.GetString(10); } else { myHouseholdMembers.myMNHome = null; }
                        if (!reader.IsDBNull(11)) { myHouseholdMembers.myPersonHighlighted = reader.GetString(11); } else { myHouseholdMembers.myPersonHighlighted = null; }
                        if (!reader.IsDBNull(12)) { myHouseholdMembers.myLiveInMN = reader.GetString(12); } else { myHouseholdMembers.myLiveInMN = null; }
                        if (!reader.IsDBNull(13)) { myHouseholdMembers.myTempAbsentMN = reader.GetString(13); } else { myHouseholdMembers.myTempAbsentMN = null; }
                        if (!reader.IsDBNull(14)) { myHouseholdMembers.myHomeless = reader.GetString(14); } else { myHouseholdMembers.myHomeless = null; }
                        if (!reader.IsDBNull(15)) { myHouseholdMembers.myPlanMakeMNHome = reader.GetString(15); } else { myHouseholdMembers.myPlanMakeMNHome = null; }
                        if (!reader.IsDBNull(16)) { myHouseholdMembers.mySeekEmplMN = reader.GetString(16); } else { myHouseholdMembers.mySeekEmplMN = null; }
                        if (!reader.IsDBNull(17)) { myHouseholdMembers.myHispanic = reader.GetString(17); } else { myHouseholdMembers.myHispanic = null; }
                        if (!reader.IsDBNull(18)) { myHouseholdMembers.myRace = reader.GetString(18); } else { myHouseholdMembers.myRace = null; }
                        if (!reader.IsDBNull(19)) { myHouseholdMembers.myHaveSSN = reader.GetString(19); } else { myHouseholdMembers.myHaveSSN = null; }
                        if (!reader.IsDBNull(20)) { myHouseholdMembers.mySSN = reader.GetString(20); } else { myHouseholdMembers.mySSN = null; }
                        if (!reader.IsDBNull(21)) { myHouseholdMembers.myUSCitizen = reader.GetString(21); } else { myHouseholdMembers.myUSCitizen = null; }
                        if (!reader.IsDBNull(22)) { myHouseholdMembers.myUSNational = reader.GetString(22); } else { myHouseholdMembers.myUSNational = null; }
                        if (!reader.IsDBNull(23)) { myHouseholdMembers.myIsPregnant = reader.GetString(23); } else { myHouseholdMembers.myIsPregnant = null; }
                        if (!reader.IsDBNull(24)) { myHouseholdMembers.myBeenInFosterCare = reader.GetString(24); } else { myHouseholdMembers.myBeenInFosterCare = null; }
                        if (!reader.IsDBNull(25)) { myHouseholdMembers.myRelationship = reader.GetString(25); } else { myHouseholdMembers.myRelationship = null; }
                        if (!reader.IsDBNull(26)) { myHouseholdMembers.myHasIncome = reader.GetString(26); } else { myHouseholdMembers.myHasIncome = null; }
                        if (!reader.IsDBNull(27)) { myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(27); } else { myHouseholdMembers.myRelationshiptoNextHM = null; }
                        if (!reader.IsDBNull(28)) { myHouseholdMembers.myTribeName = reader.GetString(28); } else { myHouseholdMembers.myTribeName = null; }
                        if (!reader.IsDBNull(29)) { myHouseholdMembers.myLiveRes = reader.GetString(29); } else { myHouseholdMembers.myLiveRes = null; }
                        if (!reader.IsDBNull(30)) { myHouseholdMembers.myTribeId = reader.GetString(30); } else { myHouseholdMembers.myTribeId = null; }
                        if (!reader.IsDBNull(31)) { myHouseholdMembers.myFederalTribe = reader.GetString(31); } else { myHouseholdMembers.myFederalTribe = null; }
                        if (!reader.IsDBNull(32)) { myHouseholdMembers.myFileJointly = reader.GetString(32); } else { myHouseholdMembers.myFileJointly = null; }
                        if (!reader.IsDBNull(33)) { myHouseholdMembers.myIncomeType = reader.GetString(33); } else { myHouseholdMembers.myIncomeType = null; }
                        if (!reader.IsDBNull(34)) { myHouseholdMembers.myIncomeEmployer = reader.GetString(34); } else { myHouseholdMembers.myIncomeEmployer = null; }
                        if (!reader.IsDBNull(35)) { myHouseholdMembers.myIncomeSeasonal = reader.GetString(35); } else { myHouseholdMembers.myIncomeSeasonal = null; }
                        if (!reader.IsDBNull(36)) { myHouseholdMembers.myIncomeAmount = reader.GetString(36); } else { myHouseholdMembers.myIncomeAmount = null; }
                        if (!reader.IsDBNull(37)) { myHouseholdMembers.myIncomeFrequency = reader.GetString(37); } else { myHouseholdMembers.myIncomeFrequency = null; }
                        if (!reader.IsDBNull(38)) { myHouseholdMembers.myIncomeMore = reader.GetString(38); } else { myHouseholdMembers.myIncomeMore = null; }
                        if (!reader.IsDBNull(39)) { myHouseholdMembers.myIncomeReduced = reader.GetString(39); } else { myHouseholdMembers.myIncomeReduced = null; }
                        if (!reader.IsDBNull(40)) { myHouseholdMembers.myIncomeAdjusted = reader.GetString(40); } else { myHouseholdMembers.myIncomeAdjusted = null; }
                        if (!reader.IsDBNull(41)) { myHouseholdMembers.myIncomeExpected = reader.GetString(41); } else { myHouseholdMembers.myIncomeExpected = null; }
                        if (!reader.IsDBNull(42)) { myHouseholdMembers.myPassCount = reader.GetString(42); } else { myHouseholdMembers.myPassCount = null; }
                        if (!reader.IsDBNull(43)) { myHouseholdMembers.myMilitary = reader.GetString(43); } else { myHouseholdMembers.myMilitary = null; }
                        if (!reader.IsDBNull(44)) { myHouseholdMembers.myMilitaryDate = Convert.ToDateTime(reader.GetDateTime(44)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myMilitaryDate = null; }
                        if (!reader.IsDBNull(45)) { myHouseholdMembers.myPrefContact = reader.GetString(45); } else { myHouseholdMembers.myPrefContact = null; }
                        if (!reader.IsDBNull(46)) { myHouseholdMembers.myPhoneNum = reader.GetString(46); } else { myHouseholdMembers.myPhoneNum = null; }
                        if (!reader.IsDBNull(47)) { myHouseholdMembers.myPhoneType = reader.GetString(47); } else { myHouseholdMembers.myPhoneType = null; }
                        if (!reader.IsDBNull(48)) { myHouseholdMembers.myAltNum = reader.GetString(48); } else { myHouseholdMembers.myAltNum = null; }
                        if (!reader.IsDBNull(49)) { myHouseholdMembers.myAltNumType = reader.GetString(49); } else { myHouseholdMembers.myAltNumType = null; }
                        if (!reader.IsDBNull(50)) { myHouseholdMembers.myEmail = reader.GetString(50); } else { myHouseholdMembers.myEmail = null; }
                        if (!reader.IsDBNull(51)) { myHouseholdMembers.myVoterCard = reader.GetString(51); } else { myHouseholdMembers.myVoterCard = null; }
                        if (!reader.IsDBNull(52)) { myHouseholdMembers.myNotices = reader.GetString(52); } else { myHouseholdMembers.myNotices = null; }
                        if (!reader.IsDBNull(53)) { myHouseholdMembers.myAuthRep = reader.GetString(53); } else { myHouseholdMembers.myAuthRep = null; }
                        if (!reader.IsDBNull(54)) { myHouseholdMembers.myDependants = reader.GetString(54); } else { myHouseholdMembers.myDependants = null; }
                        if (!reader.IsDBNull(55)) { myHouseholdMembers.myTaxFiler = reader.GetString(55); } else { myHouseholdMembers.myTaxFiler = null; }
                        if (!reader.IsDBNull(56)) { myHouseholdMembers.myChildren = reader.GetString(56); } else { myHouseholdMembers.myChildren = null; }
                        if (!reader.IsDBNull(57)) { myHouseholdMembers.myDueDate = Convert.ToDateTime(reader.GetDateTime(57)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myDueDate = null; }
                        if (!reader.IsDBNull(58)) { myHouseholdMembers.myPregnancyEnded = Convert.ToDateTime(reader.GetDateTime(58)).ToString("MM/dd/yyyy"); } else { myHouseholdMembers.myPregnancyEnded = null; }
                        if (!reader.IsDBNull(59)) { myHouseholdMembers.myReEnroll = reader.GetString(59); } else { myHouseholdMembers.myReEnroll = null; }
                        if (!reader.IsDBNull(60)) { myHouseholdMembers.mySaveExit = reader.GetString(60); } else { myHouseholdMembers.mySaveExit = null; }
                        if (!reader.IsDBNull(61)) { myHouseholdMembers.myRandom = reader.GetString(61); } else { myHouseholdMembers.myRandom = null; }
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
                        if (myHouseholdMembers.HouseMembersID == 2 && reader.GetString(9) == "Household 2")
                        {
                            if (!reader.IsDBNull(3)) { myHouseholdMembers.myMailAddress1 = reader.GetString(3); } else { myHouseholdMembers.myMailAddress1 = null; }
                            if (!reader.IsDBNull(4)) { myHouseholdMembers.myMailAddress2 = reader.GetString(4); } else { myHouseholdMembers.myMailAddress2 = null; }
                            if (!reader.IsDBNull(5)) { myHouseholdMembers.myMailCity = reader.GetString(5); } else { myHouseholdMembers.myMailCity = null; }
                            if (!reader.IsDBNull(6)) { myHouseholdMembers.myMailState = reader.GetString(6); } else { myHouseholdMembers.myMailState = null; }
                            if (!reader.IsDBNull(7)) { myHouseholdMembers.myMailZip = reader.GetString(7); } else { myHouseholdMembers.myMailZip = null; }
                            if (!reader.IsDBNull(10)) { myHouseholdMembers.myMailCounty = reader.GetString(10); } else { myHouseholdMembers.myMailCounty = null; }
                            if (!reader.IsDBNull(11)) { myHouseholdMembers.myMailAptSuite = reader.GetString(11); } else { myHouseholdMembers.myMailAptSuite = null; }
                        }
                        else if (myHouseholdMembers.HouseMembersID == 3 && reader.GetString(9) == "Household 3")
                        {
                            if (!reader.IsDBNull(3)) { myHouseholdMembers.myMailAddress1 = reader.GetString(3); } else { myHouseholdMembers.myMailAddress1 = null; }
                            if (!reader.IsDBNull(4)) { myHouseholdMembers.myMailAddress2 = reader.GetString(4); } else { myHouseholdMembers.myMailAddress2 = null; }
                            if (!reader.IsDBNull(5)) { myHouseholdMembers.myMailCity = reader.GetString(5); } else { myHouseholdMembers.myMailCity = null; }
                            if (!reader.IsDBNull(6)) { myHouseholdMembers.myMailState = reader.GetString(6); } else { myHouseholdMembers.myMailState = null; }
                            if (!reader.IsDBNull(7)) { myHouseholdMembers.myMailZip = reader.GetString(7); } else { myHouseholdMembers.myMailZip = null; }
                            if (!reader.IsDBNull(10)) { myHouseholdMembers.myMailCounty = reader.GetString(10); } else { myHouseholdMembers.myMailCounty = null; }
                            if (!reader.IsDBNull(11)) { myHouseholdMembers.myMailAptSuite = reader.GetString(11); } else { myHouseholdMembers.myMailAptSuite = null; }
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

        public int doGetAccount(ref mystructAccountCreate myAccountCreate, ref mystructHistoryInfo myHistoryInfo, String myTestId, String myAccountId)
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
                    ("SELECT * FROM Account where TestId = " + myTestId + " and AccountID = " +
                    myAccountId + ";", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myAccountCreate.myAccountID = reader.GetInt32(0);
                        if (!reader.IsDBNull(2)) { myAccountCreate.myFirstName = reader.GetString(2); } else { myAccountCreate.myFirstName = null; }
                        if (!reader.IsDBNull(3)) { myAccountCreate.myMiddleName = reader.GetString(3); } else { myAccountCreate.myMiddleName = null; }
                        if (!reader.IsDBNull(4)) { myAccountCreate.myLastName = reader.GetString(4); } else { myAccountCreate.myLastName = null; }
                        if (!reader.IsDBNull(5)) { myAccountCreate.mySuffix = reader.GetString(5); } else { myAccountCreate.mySuffix = null; }
                        if (!reader.IsDBNull(6)) { myAccountCreate.myEmail = reader.GetString(6); } else { myAccountCreate.myEmail = null; }
                        if (!reader.IsDBNull(7)) { myAccountCreate.myPhone = reader.GetString(7); } else { myAccountCreate.myPhone = null; }
                        if (!reader.IsDBNull(8)) { myAccountCreate.mySSN = reader.GetString(8); } else { myAccountCreate.mySSN = null; }
                        if (!reader.IsDBNull(9)) { myAccountCreate.myDOB = Convert.ToString(reader.GetDateTime(9)); } else { myAccountCreate.myDOB = null; }
                        if (!reader.IsDBNull(10)) { myAccountCreate.myUsername = reader.GetString(10); } else { myAccountCreate.myUsername = null; }
                        if (!reader.IsDBNull(11)) { myAccountCreate.myCaseWorkerLoginId = reader.GetString(11); } else { myAccountCreate.myCaseWorkerLoginId = null; }
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

        public int doUpdateHouseholdSSN(ref mystructHistoryInfo myHistoryInfo, string updateValue, string memberId)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;


            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com4 = new SqlCeCommand(
                    "SELECT * FROM HouseMembers where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com4.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update HouseMembers set SSN = @mySSN where TestID = " + myHistoryInfo.myTestId + " and HouseMembersID = " + memberId;

                        using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                        {
                            com5.Parameters.AddWithValue("mySSN", updateValue);
                            com5.ExecuteNonQuery();
                            com5.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update Household SSN didn't work");
            }
            return 1;
        }

        public int doUpdateApplicationSSN(ref mystructHistoryInfo myHistoryInfo, string updateValue)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;


            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com4 = new SqlCeCommand(
                    "SELECT * FROM Application where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com4.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Application set SSNNum = @mySSNNum where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                        {
                            com5.Parameters.AddWithValue("mySSNNum", updateValue);
                            com5.ExecuteNonQuery();
                            com5.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update Application SSN didn't work");
            }
            return 1;
        }

        public int doUpdateAccountSSN(ref mystructHistoryInfo myHistoryInfo, string updateValue)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com4 = new SqlCeCommand(
                    "SELECT * FROM Account where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com4.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Account set SSN = @mySSN where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                        {
                            com5.Parameters.AddWithValue("mySSN", updateValue);
                            com5.ExecuteNonQuery();
                            com5.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update Account SSN didn't work");
            }
            return 1;
        }

        public int doUpdateAccountUsername(ref mystructHistoryInfo myHistoryInfo, string updateValue)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;


            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com4 = new SqlCeCommand(
                    "SELECT * FROM Account where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com4.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Account set Username = @myUsername where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                        {
                            com5.Parameters.AddWithValue("myUsername", updateValue);
                            com5.ExecuteNonQuery();
                            com5.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update Account Username didn't work");
            }
            return 1;
        }

        public int doUpdateAssisterSSN(ref mystructHistoryInfo myHistoryInfo, string updateValue)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;


            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com4 = new SqlCeCommand(
                    "SELECT * FROM Assister where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com4.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Assister set SSN = @mySSN where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                        {
                            com5.Parameters.AddWithValue("mySSN", updateValue);
                            com5.ExecuteNonQuery();
                            com5.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update Assister SSN didn't work");
            }
            return 1;
        }

        public string DoGetAppRandom(ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM Application where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetString(70);
                    }
                    else
                    {
                        return "Error locating app random";
                    }
                }
            }
            catch
            {
                return "Error locating app random";
            }
        }

        public string DoGetAppGender(ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM Application where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetString(6);
                    }
                    else
                    {
                        return "Error locating app gender";
                    }
                }
            }
            catch
            {
                return "Error locating app gender";
            }
        }

        public string DoGetAppDay2(ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT * FROM Application where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(66))
                        {
                            return reader.GetString(66);
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "Error locating app day2";
                    }
                }
            }
            catch (Exception e)
            {
                return "Error locating app day2 " + e;
            }
        }

        public int DoGetExistingAccounts(ref mystructHistoryInfo myHistoryInfo, ref mystructExistingAccounts myExistingAccountInfo, ref mystructAccountCreate myAccountCreate,
            ref mystructApplication myApplication)
        {
            for (int j = 0; j < 250; ++j)//must clear first before next test
            {
                if (myExistingAccountInfo.myExistingAccountFirstName[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountFirstName[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountMiddleName[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountMiddleName[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountLastName[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountLastName[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountSuffix[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountSuffix[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountAddress1[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountAddress1[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountAddress2[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountAddress2[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountCity[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountCity[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountState[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountState[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountZip[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountZip[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountZip4[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountZip4[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountEmail[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountEmail[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountPhone[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountPhone[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountSSN[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountSSN[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountDOB[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountDOB[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountUserName[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountUserName[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountPassword[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountPassword[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountSecret[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountSecret[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountQuestion1[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountQuestion1[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountAnswer1[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountAnswer1[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountQuestion2[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountQuestion2[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountAnswer2[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountAnswer2[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountQuestion3[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountQuestion3[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountAnswer3[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountAnswer3[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountQuestion4[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountQuestion4[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountAnswer4[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountAnswer4[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountQuestion5[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountQuestion5[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountAnswer5[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountAnswer5[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountConfirmation[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountConfirmation[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountEnvironment[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountEnvironment[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountGender[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountGender[j] = null;
                }
                if (myExistingAccountInfo.myExistingAccountUsed[j] != null)
                {
                    myExistingAccountInfo.myExistingAccountUsed[j] = null;
                }
            }

            //open the workbook 
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app = new Microsoft.Office.Interop.Excel.Application();

            string fullPathName;
            if (myHistoryInfo.myEnvironment == "STST2")
            {
                fullPathName = "S:\\GPesall backup\\ExistingAccounts\\STST2AccountCreate1.xls";
            }
            else
            {
                fullPathName = "S:\\GPesall backup\\ExistingAccounts\\STST1AccountCreate1.xls";
            }
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = app.Workbooks.Open(fullPathName,
                    0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
            app.Visible = true;
            Microsoft.Office.Interop.Excel.Sheets xcelSheets = excelWorkbook.Worksheets;

            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xcelSheets.get_Item(1);
            Range excelRange = excelWorksheet.UsedRange;

            //get an object array of all of the cells in the worksheet (their values)
            object[,] valueArray = (object[,])excelRange.get_Value(
                        XlRangeValueDataType.xlRangeValueDefault);

            string myFirstName, myMiddleName, myLastName, mySuffix, myAddress1, myAddress2, myCity, myState, myZip, myZip4, myEmail, myPhone, mySSN, myDOB,
                myUserName, myPassword, mySecret, myQuestion1, myAnswer1, myQuestion2, myAnswer2, myQuestion3, myAnswer3, myQuestion4, myAnswer4, myQuestion5,
                myAnswer5, myConfirmation, myEnvironment, myGender, myUsed;
            int i = 0;
            for (int row = 2; row < excelWorksheet.UsedRange.Rows.Count; ++row)
            {
                //access each cell
                myFirstName = Convert.ToString(valueArray[row, 1]);
                if (myFirstName != "")
                {
                    myExistingAccountInfo.myExistingAccountFirstName[i] = myFirstName;
                }
                myMiddleName = Convert.ToString(valueArray[row, 2]);
                if (myMiddleName != "")
                {
                    myExistingAccountInfo.myExistingAccountMiddleName[i] = myMiddleName;
                }
                myLastName = Convert.ToString(valueArray[row, 3]);
                if (myLastName != "")
                {
                    myExistingAccountInfo.myExistingAccountLastName[i] = myLastName;
                }
                mySuffix = Convert.ToString(valueArray[row, 4]);
                if (mySuffix != "")
                {
                    myExistingAccountInfo.myExistingAccountSuffix[i] = mySuffix;
                }
                myAddress1 = Convert.ToString(valueArray[row, 5]);
                if (myAddress1 != "")
                {
                    myExistingAccountInfo.myExistingAccountAddress1[i] = myAddress1;
                }
                myAddress2 = Convert.ToString(valueArray[row, 6]);
                if (myAddress2 != "")
                {
                    myExistingAccountInfo.myExistingAccountAddress2[i] = myAddress2;
                }
                myCity = Convert.ToString(valueArray[row, 7]);
                if (myCity != "")
                {
                    myExistingAccountInfo.myExistingAccountCity[i] = myCity;
                }
                myState = Convert.ToString(valueArray[row, 8]);
                if (myState != "")
                {
                    myExistingAccountInfo.myExistingAccountState[i] = myState;
                }
                myZip = Convert.ToString(valueArray[row, 9]);
                if (myZip != "")
                {
                    myExistingAccountInfo.myExistingAccountZip[i] = myZip;
                }
                myZip4 = Convert.ToString(valueArray[row, 10]);
                if (myZip4 != "")
                {
                    myExistingAccountInfo.myExistingAccountZip4[i] = myZip4;
                }
                myEmail = Convert.ToString(valueArray[row, 11]);
                if (myEmail != "")
                {
                    myExistingAccountInfo.myExistingAccountEmail[i] = myEmail;
                }
                myPhone = Convert.ToString(valueArray[row, 12]);
                if (myPhone != "")
                {
                    myExistingAccountInfo.myExistingAccountPhone[i] = myPhone;
                }
                mySSN = Convert.ToString(valueArray[row, 13]);
                if (mySSN != "")
                {
                    myExistingAccountInfo.myExistingAccountSSN[i] = mySSN;
                }
                myDOB = Convert.ToString(valueArray[row, 14]);
                if (myDOB != "")
                {
                    myExistingAccountInfo.myExistingAccountDOB[i] = myDOB;
                }
                myUserName = Convert.ToString(valueArray[row, 15]);
                if (myUserName != "")
                {
                    myExistingAccountInfo.myExistingAccountUserName[i] = myUserName;
                }
                myPassword = Convert.ToString(valueArray[row, 16]);
                if (myPassword != "")
                {
                    myExistingAccountInfo.myExistingAccountPassword[i] = myPassword;
                }
                mySecret = Convert.ToString(valueArray[row, 17]);
                if (mySecret != "")
                {
                    myExistingAccountInfo.myExistingAccountSecret[i] = mySecret;
                }
                myQuestion1 = Convert.ToString(valueArray[row, 18]);
                if (myQuestion1 != "")
                {
                    myExistingAccountInfo.myExistingAccountQuestion1[i] = myQuestion1;
                }
                myAnswer1 = Convert.ToString(valueArray[row, 19]);
                if (myAnswer1 != "")
                {
                    myExistingAccountInfo.myExistingAccountAnswer1[i] = myAnswer1;
                }
                myQuestion2 = Convert.ToString(valueArray[row, 20]);
                if (myQuestion2 != "")
                {
                    myExistingAccountInfo.myExistingAccountQuestion2[i] = myQuestion2;
                }
                myAnswer2 = Convert.ToString(valueArray[row, 21]);
                if (myAnswer2 != "")
                {
                    myExistingAccountInfo.myExistingAccountAnswer2[i] = myAnswer2;
                }
                myQuestion3 = Convert.ToString(valueArray[row, 22]);
                if (myQuestion3 != "")
                {
                    myExistingAccountInfo.myExistingAccountQuestion3[i] = myQuestion3;
                }
                myAnswer3 = Convert.ToString(valueArray[row, 23]);
                if (myAnswer3 != "")
                {
                    myExistingAccountInfo.myExistingAccountAnswer3[i] = myAnswer3;
                }
                myQuestion4 = Convert.ToString(valueArray[row, 24]);
                if (myQuestion4 != "")
                {
                    myExistingAccountInfo.myExistingAccountQuestion4[i] = myQuestion4;
                }
                myAnswer4 = Convert.ToString(valueArray[row, 25]);
                if (myAnswer4 != "")
                {
                    myExistingAccountInfo.myExistingAccountAnswer4[i] = myAnswer4;
                }
                myQuestion5 = Convert.ToString(valueArray[row, 26]);
                if (myQuestion5 != "")
                {
                    myExistingAccountInfo.myExistingAccountQuestion5[i] = myQuestion5;
                }
                myAnswer5 = Convert.ToString(valueArray[row, 27]);
                if (myAnswer5 != "")
                {
                    myExistingAccountInfo.myExistingAccountAnswer5[i] = myAnswer5;
                }
                myConfirmation = Convert.ToString(valueArray[row, 28]);
                if (myConfirmation != "")
                {
                    myExistingAccountInfo.myExistingAccountConfirmation[i] = myConfirmation;
                }
                myEnvironment = Convert.ToString(valueArray[row, 29]);
                if (myEnvironment != "")
                {
                    myExistingAccountInfo.myExistingAccountEnvironment[i] = myEnvironment;
                }
                myGender = Convert.ToString(valueArray[row, 30]);
                if (myGender != "")
                {
                    myExistingAccountInfo.myExistingAccountGender[i] = myGender;
                }
                myUsed = Convert.ToString(valueArray[row, 31]);
                if (myUsed != "")
                {
                    myExistingAccountInfo.myExistingAccountUsed[i] = myUsed;
                }
                i = i + 1;
            }

            //locate first not used
            int k = 0;
            for (int row = 2; row < excelWorksheet.UsedRange.Rows.Count; ++row)
            {
                myUsed = Convert.ToString(valueArray[row, 31]);
                if (myUsed == "N")
                {
                    myAccountCreate.myFirstName = myExistingAccountInfo.myExistingAccountFirstName[k];
                    myAccountCreate.myMiddleName = myExistingAccountInfo.myExistingAccountMiddleName[k];
                    myAccountCreate.myLastName = myExistingAccountInfo.myExistingAccountLastName[k];
                    //myAccountCreate.mySuffix = myExistingAccountInfo.myExistingAccountSuffix[k];
                    myAccountCreate.myEmail = myExistingAccountInfo.myExistingAccountEmail[k];
                    myAccountCreate.myPhone = myExistingAccountInfo.myExistingAccountPhone[k];
                    myAccountCreate.mySSN = myExistingAccountInfo.myExistingAccountSSN[k];
                    myAccountCreate.myDOB = myExistingAccountInfo.myExistingAccountDOB[k];
                    myAccountCreate.myUsername = myExistingAccountInfo.myExistingAccountUserName[k];
                    myAccountCreate.myPassword = "Welcome1#";
                    myAccountCreate.myQuestion1 = myExistingAccountInfo.myExistingAccountQuestion1[k];
                    myAccountCreate.myAnswer1 = myExistingAccountInfo.myExistingAccountAnswer1[k];
                    myAccountCreate.myQuestion2 = myExistingAccountInfo.myExistingAccountQuestion2[k];
                    myAccountCreate.myAnswer2 = myExistingAccountInfo.myExistingAccountAnswer2[k];
                    myAccountCreate.myQuestion3 = myExistingAccountInfo.myExistingAccountQuestion3[k];
                    myAccountCreate.myAnswer3 = myExistingAccountInfo.myExistingAccountAnswer3[k];
                    myAccountCreate.myQuestion4 = myExistingAccountInfo.myExistingAccountQuestion4[k];
                    myAccountCreate.myAnswer4 = myExistingAccountInfo.myExistingAccountAnswer4[k];
                    myAccountCreate.myQuestion5 = myExistingAccountInfo.myExistingAccountQuestion5[k];
                    myAccountCreate.myAnswer5 = myExistingAccountInfo.myExistingAccountAnswer5[k];

                    SqlCeConnection con;
                    string conString = Properties.Settings.Default.Database1ConnectionString;

                    try
                    {
                        con = new SqlCeConnection(conString);
                        con.Open();
                        using (SqlCeCommand com4 = new SqlCeCommand(
                            "SELECT * FROM Account where TestID = " + myHistoryInfo.myTestId, con))
                        {
                            SqlCeDataReader reader = com4.ExecuteReader();
                            if (reader.Read())
                            {
                                string myUpdateString;
                                myUpdateString = "Update Account set SSN = @mySSN, FirstName = @myFirstName, MiddleName = @myMiddleName, LastName = @myLastName, "
                                   + "Suffix = @mySuffix, Email = @myEmail, Phone = @myPhone, DOB = @myDOB, UserName = @myUsername where TestID = " + myHistoryInfo.myTestId;

                                using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                                {
                                    com5.Parameters.AddWithValue("myFirstName", myAccountCreate.myFirstName);
                                    com5.Parameters.AddWithValue("myMiddleName", myAccountCreate.myMiddleName);
                                    com5.Parameters.AddWithValue("myLastName", myAccountCreate.myLastName);
                                    com5.Parameters.AddWithValue("mySuffix", DBNull.Value);
                                    com5.Parameters.AddWithValue("myEmail", myAccountCreate.myEmail);
                                    com5.Parameters.AddWithValue("myPhone", myAccountCreate.myPhone);
                                    com5.Parameters.AddWithValue("mySSN", myAccountCreate.mySSN);
                                    com5.Parameters.AddWithValue("myDOB", myAccountCreate.myDOB);
                                    com5.Parameters.AddWithValue("myUsername", myAccountCreate.myUsername);
                                    com5.ExecuteNonQuery();
                                    com5.Dispose();
                                }
                            }
                        }
                        con.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Update Account didn't work");
                    }

                    myApplication.myFirstName = myAccountCreate.myFirstName;
                    myApplication.myMiddleName = myAccountCreate.myMiddleName;
                    myApplication.myLastName = myAccountCreate.myLastName;
                    myApplication.myEmail = myAccountCreate.myEmail;
                    myApplication.myPhoneNum = myAccountCreate.myPhone;
                    myApplication.myPhoneNum = myAccountCreate.myPhone.Substring(1, 3) + myAccountCreate.myPhone.Substring(5, 3) + myAccountCreate.myPhone.Substring(9, 4);
                    myApplication.mySSNNum = myAccountCreate.mySSN;
                    myApplication.myDOB = myAccountCreate.myDOB;
                    myApplication.myGender = myExistingAccountInfo.myExistingAccountGender[k];

                    try
                    {
                        con = new SqlCeConnection(conString);
                        con.Open();
                        using (SqlCeCommand com4 = new SqlCeCommand(
                            "SELECT * FROM Application where TestID = " + myHistoryInfo.myTestId, con))
                        {
                            SqlCeDataReader reader = com4.ExecuteReader();
                            if (reader.Read())
                            {
                                string myUpdateString;
                                myUpdateString = "Update Application set SSNNum = @mySSN, FirstName = @myFirstName, MiddleName = @myMiddleName, LastName = @myLastName, "
                                   + "Email = @myEmail, PhoneNum = @myPhone, DOB = @myDOB, Gender = @myGender where TestID = " + myHistoryInfo.myTestId;

                                using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                                {
                                    com5.Parameters.AddWithValue("myFirstName", myApplication.myFirstName);
                                    com5.Parameters.AddWithValue("myMiddleName", myApplication.myMiddleName);
                                    com5.Parameters.AddWithValue("myLastName", myApplication.myLastName);
                                    com5.Parameters.AddWithValue("myEmail", myApplication.myEmail);
                                    com5.Parameters.AddWithValue("myPhone", myApplication.myPhoneNum);
                                    com5.Parameters.AddWithValue("mySSN", myApplication.mySSNNum);
                                    com5.Parameters.AddWithValue("myDOB", myApplication.myDOB);
                                    com5.Parameters.AddWithValue("myGender", myApplication.myGender);
                                    com5.ExecuteNonQuery();
                                    com5.Dispose();
                                }
                            }
                        }
                        con.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Update Application didn't work");
                    }

                    myApplication.myHomeAddress1 = myExistingAccountInfo.myExistingAccountAddress1[k];
                    myApplication.myHomeAddress2 = myExistingAccountInfo.myExistingAccountAddress2[k];
                    myApplication.myHomeCity = myExistingAccountInfo.myExistingAccountCity[k];
                    myApplication.myHomeState = myExistingAccountInfo.myExistingAccountState[k];
                    myApplication.myHomeZip = myExistingAccountInfo.myExistingAccountZip[k];
                    myApplication.myHomeZip4 = myExistingAccountInfo.myExistingAccountZip4[k];

                    try
                    {
                        con = new SqlCeConnection(conString);
                        con.Open();
                        using (SqlCeCommand com6 = new SqlCeCommand(
                            "SELECT * FROM Address where TestID = " + myHistoryInfo.myTestId + " and Type = 'Home'", con))
                        {
                            SqlCeDataReader reader = com6.ExecuteReader();
                            if (reader.Read())
                            {
                                string myUpdateString;
                                myUpdateString = "Update Address set Address1 = @myAddress1, Address2 = @myAddress2, City = @myCity, State = @myState, Zip = @myZip, Zip4 = @myZip4, "
                                + "Type = @myType, County = @myCounty where TestID = " + myHistoryInfo.myTestId;

                                using (SqlCeCommand com7 = new SqlCeCommand(myUpdateString, con))
                                {
                                    com7.Parameters.AddWithValue("myAddress1", myApplication.myHomeAddress1);
                                    com7.Parameters.AddWithValue("myAddress2", myApplication.myHomeAddress2);
                                    com7.Parameters.AddWithValue("myCity", myApplication.myHomeCity);
                                    com7.Parameters.AddWithValue("myState", myApplication.myHomeState);
                                    com7.Parameters.AddWithValue("myZip", myApplication.myHomeZip);
                                    com7.Parameters.AddWithValue("myZip4", myApplication.myHomeZip4);
                                    com7.Parameters.AddWithValue("myCounty", "Hennepin");
                                    com7.Parameters.AddWithValue("myType", "Home");
                                    com7.ExecuteNonQuery();
                                    com7.Dispose();
                                }
                            }
                        }
                        con.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Update Address didn't work");
                    }

                    myExistingAccountInfo.myExistingAccountUsed[k] = "Y";
                    try
                    {
                        excelWorksheet.Cells[k + 2, 31] = "Y";//update used in worksheet
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("Spreadsheet update didn't work" + a);
                    }
                    break;
                }
                k = k + 1;
            }

            if (myAccountCreate.myFirstName == null)
            {
                MessageBox.Show("All existing accounts have been used. A new list must be created.");
            }

            excelWorkbook.Save();
            excelWorkbook.Close(true, Type.Missing, Type.Missing);

            app.Quit();

            Marshal.ReleaseComObject(excelWorkbook);
            Marshal.ReleaseComObject(app);

            return 1;
        }

        public int doGetMyAddresses(ref mystructMyAddresses myAddressInfo)
        {
            Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();
            _excelApp.Visible = true;
                        
            //open the workbook   
            String fullPathName = "C:\\Logs\\MyAddresses.xls";
            Workbook workbook = _excelApp.Workbooks.Open(fullPathName,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

            //select the first sheet        
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

            //find the used range in worksheet
            Range excelRange = worksheet.UsedRange;

            //get an object array of all of the cells in the worksheet (their values)
            object[,] valueArray = (object[,])excelRange.get_Value(
                        XlRangeValueDataType.xlRangeValueDefault);

            string myFirstName;
            string myMiddleName;
            string myLastName;
            string mySuffix;
            string myAddress1;
            string myAddress2;
            string mySuite;
            string myCity;
            string myState;
            string myZip;
            string myZip4;
            string myCounty;
            int i = 0;
            for (int row = 2; row < worksheet.UsedRange.Rows.Count; ++row)
            {
                //access each cell
                myFirstName = Convert.ToString(valueArray[row, 1]);
                if (myFirstName != "")
                {
                    myAddressInfo.myAddressFirstName[i] = myFirstName;
                }
                myMiddleName = Convert.ToString(valueArray[row, 2]);
                if (myMiddleName != "")
                {
                    myAddressInfo.myAddressMiddleName[i] = myMiddleName;
                }
                myLastName = Convert.ToString(valueArray[row, 3]);
                if (myLastName != "")
                {
                    myAddressInfo.myAddressLastName[i] = myLastName;
                }
                mySuffix = Convert.ToString(valueArray[row, 4]);
                if (mySuffix != "")
                {
                    myAddressInfo.myAddressSuffix[i] = mySuffix;
                }
                myAddress1 = Convert.ToString(valueArray[row, 5]);
                if (myAddress1 != "")
                {
                    myAddressInfo.myAddressAddress1[i] = myAddress1;
                }
                myAddress2 = Convert.ToString(valueArray[row, 6]);
                if (myAddress2 != "")
                {
                    myAddressInfo.myAddressAddress2[i] = myAddress2;
                }
                mySuite = Convert.ToString(valueArray[row, 7]);
                if (mySuite != "")
                {
                    myAddressInfo.myAddressSuite[i] = mySuite;
                }
                myCity = Convert.ToString(valueArray[row, 8]);
                if (myCity != "")
                {
                    myAddressInfo.myAddressCity[i] = myCity;
                }
                myState = Convert.ToString(valueArray[row, 9]);
                if (myState != "")
                {
                    myAddressInfo.myAddressState[i] = myState;
                }
                myZip = Convert.ToString(valueArray[row, 10]);
                if (myZip != "")
                {
                    myAddressInfo.myAddressZip[i] = myZip;
                }
                myZip4 = Convert.ToString(valueArray[row, 11]);
                if (myZip4 != "")
                {
                    myAddressInfo.myAddressZip4[i] = myZip4;
                }
                myCounty = Convert.ToString(valueArray[row, 12]);
                if (myCounty != "")
                {
                    myAddressInfo.myAddressCounty[i] = myCounty;
                }
                i = i + 1;
            }
            workbook.Close(true, Type.Missing, Type.Missing);

            _excelApp.Quit();

            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(_excelApp);

            return 1;
        }

        public int doUpdateWithMyAddress(ref mystructAccountCreate myAccountCreate, ref mystructApplication myApplication, ref mystructMyAddresses myAddressInfo, ref mystructHistoryInfo myHistoryInfo, int iloop2)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com4 = new SqlCeCommand("SELECT * FROM Account where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com4.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Account set FirstName = @myFirstName, MiddleName = @myMiddleName, "
                         + "LastName = @myLastName where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                        {
                            com5.Parameters.AddWithValue("myFirstName", myAddressInfo.myAddressFirstName[iloop2-1]);
                            if (myAddressInfo.myAddressMiddleName[iloop2 - 1] != "" && myAddressInfo.myAddressMiddleName[iloop2 - 1] != null)
                            {
                                com5.Parameters.AddWithValue("myMiddleName", myAddressInfo.myAddressMiddleName[iloop2 - 1]);
                            }
                            else
                            {
                                com5.Parameters.AddWithValue("myMiddleName", DBNull.Value);
                            }                            
                            com5.Parameters.AddWithValue("myLastName", myAddressInfo.myAddressLastName[iloop2-1]);
                            com5.ExecuteNonQuery();
                            com5.Dispose();
                        }
                    }
                }
                using (SqlCeCommand com4 = new SqlCeCommand("SELECT * FROM Application where TestID = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com4.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Application set FirstName = @myFirstName, MiddleName = @myMiddleName, "
                           + "LastName = @myLastName where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com5 = new SqlCeCommand(myUpdateString, con))
                        {
                            com5.Parameters.AddWithValue("myFirstName", myAddressInfo.myAddressFirstName[iloop2-1]);
                            if (myAddressInfo.myAddressMiddleName[iloop2 - 1] != "" && myAddressInfo.myAddressMiddleName[iloop2 - 1] != null)
                            {
                                com5.Parameters.AddWithValue("myMiddleName", myAddressInfo.myAddressMiddleName[iloop2 - 1]);
                            }
                            else
                            {
                                com5.Parameters.AddWithValue("myMiddleName", DBNull.Value);
                            }  
                            com5.Parameters.AddWithValue("myLastName", myAddressInfo.myAddressLastName[iloop2-1]);
                            com5.ExecuteNonQuery();
                            com5.Dispose();
                        }
                    }
                }
                using (SqlCeCommand com6 = new SqlCeCommand(
                            "SELECT * FROM Address where TestID = " + myHistoryInfo.myTestId + " and Type = 'Home'", con))
                {
                    SqlCeDataReader reader = com6.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Address set Address1 = @myAddress1, Address2 = @myAddress2, City = @myCity, State = @myState, Zip = @myZip, "
                        + "Zip4 = @myZip4, County = @myCounty, AptSuite = @mySuite where TestID = " + myHistoryInfo.myTestId;

                        using (SqlCeCommand com7 = new SqlCeCommand(myUpdateString, con))
                        {
                            com7.Parameters.AddWithValue("myAddress1", myAddressInfo.myAddressAddress1[iloop2 - 1]);
                            if (myAddressInfo.myAddressAddress2[iloop2 - 1] != "" && myAddressInfo.myAddressAddress2[iloop2 - 1] != null)
                            {
                                com7.Parameters.AddWithValue("myAddress2", myAddressInfo.myAddressAddress2[iloop2 - 1]);
                            }
                            else
                            {
                                com7.Parameters.AddWithValue("myAddress2", DBNull.Value);
                            } 
                            com7.Parameters.AddWithValue("myCity", myAddressInfo.myAddressCity[iloop2 - 1]);
                            com7.Parameters.AddWithValue("myState", myAddressInfo.myAddressState[iloop2 - 1]);
                            com7.Parameters.AddWithValue("myZip", myAddressInfo.myAddressZip[iloop2 - 1]);
                            if (myAddressInfo.myAddressZip4[iloop2 - 1] != "" && myAddressInfo.myAddressZip4[iloop2 - 1] != null)
                            {
                                com7.Parameters.AddWithValue("myZip4", myAddressInfo.myAddressZip4[iloop2 - 1]);
                            }
                            else
                            {
                                com7.Parameters.AddWithValue("myZip4", DBNull.Value);
                            }
                            com7.Parameters.AddWithValue("myCounty", myAddressInfo.myAddressCounty[iloop2 - 1]);
                            if (myAddressInfo.myAddressSuite[iloop2 - 1] != "" && myAddressInfo.myAddressSuite[iloop2 - 1] != null)
                            {
                                com7.Parameters.AddWithValue("mySuite", myAddressInfo.myAddressSuite[iloop2 - 1]);
                            }
                            else
                            {
                                com7.Parameters.AddWithValue("mySuite", DBNull.Value);
                            } 
                            com7.ExecuteNonQuery();
                            com7.Dispose();
                        }
                    }
                }
                con.Close();

                myAccountCreate.myFirstName = myAddressInfo.myAddressFirstName[iloop2-1];
                myAccountCreate.myMiddleName = myAddressInfo.myAddressMiddleName[iloop2 - 1];
                myAccountCreate.myLastName = myAddressInfo.myAddressLastName[iloop2 - 1];

                myApplication.myFirstName = myAddressInfo.myAddressFirstName[iloop2 - 1];
                myApplication.myMiddleName = myAddressInfo.myAddressMiddleName[iloop2 - 1];
                myApplication.myLastName = myAddressInfo.myAddressLastName[iloop2 - 1];                

                myApplication.myHomeAddress1 = myAddressInfo.myAddressAddress1[iloop2 - 1];                
                myApplication.myHomeAddress2 = myAddressInfo.myAddressAddress2[iloop2 - 1];
                myApplication.myHomeAptSuite = myAddressInfo.myAddressSuite[iloop2 - 1];
                myApplication.myHomeCity = myAddressInfo.myAddressCity[iloop2 - 1];
                myApplication.myHomeState = myAddressInfo.myAddressState[iloop2 - 1];
                myApplication.myHomeZip = myAddressInfo.myAddressZip[iloop2 - 1];
                myApplication.myHomeZip4 = myAddressInfo.myAddressZip4[iloop2 - 1];
                myApplication.myHomeCounty = myAddressInfo.myAddressCounty[iloop2 - 1];

            }
            catch
            {
                MessageBox.Show("Update With My Address Info didn't work");
            }
            return 1;
        }


    }
}
