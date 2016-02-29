using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

struct mystructHistoryInfo
{
    public int myTestHistoryId;
    public string myURL;
    public int myRunId;
    public string myRunStatus;
    public int myHistoryTestId;
    public string mySuiteName;
    public string myTestName;
    public string myTestId;
    public string myTestStepId;
    public string myTestStepName;
    public string myTestStepClass;
    public string myTestStepMethod;
    public string myTestStepWindow;
    public string myTestStepStatus;
    public string myStepException;
    public string myStepNotes;
    public string myBuildVersion;
    public DateTime myTestStartTime;
    public DateTime myTestEndTime;
    public DateTime myStepStartTime;
    public DateTime myStepEndTime;
    public string myScreenShot;
    public string myFirstTime;
    public string myTemplateFolder;
    public string myTemplate;
    public string[] myRequiredScreenshots;
    public int[] myRequiredStep;
    public string[] myRequiredStepStatus;
    public string[] myRequiredScreenshotFile;
    public string myAppBuild;
    public string myMnsureBuild;
    public string myCuramBuild;
    public string myIcnumber;
    public int myCaseWorkerWait;
    public int myCitizenWait;
    public string myInTimeTravel;
    public DateTime myTimeTravelDate;
    public string myExecutedBy;
}

struct mystructSelectedTest
{
    public int myTestId;
    public string myTestName;
    public string myTestDescr;
    public int myTestRunId;
    public string myTestType;
    public string myURL;
    public string mySpecialCase1;
    public int myRowIndex;

}

    struct mystructAccountCreate
    {
        public int myAccountID;
        public string myFirstName;
        public string myMiddleName;
        public string myLastName;
        public string mySuffix;
        public string myAddress1;
        public string myAddress2;
        public string myCity;
        public string myState;
        public string myZip;
        public string myZip4;
        public string myEmail;
        public string myPhone;
        public string mySSN;
        public string myDOB;
        public string myUsername;
        public string myPassword;
        public string mySecret;
        public string myQuestion1;
        public string myQuestion2;
        public string myQuestion3;
        public string myQuestion4;
        public string myQuestion5;
        public string myAnswer1;
        public string myAnswer2;
        public string myAnswer3;
        public string myAnswer4;
        public string myAnswer5;
        public string myCaseWorkerLoginId;
    }

    struct mystructApplication
    {
        public int myEnrollmentId;
        public string myFirstName;
        public string myMiddleName;
        public string myLastName;
        public string mySuffix;
        public string myAddress1;
        public string myAddress2;
        public string myAptSuite;
        public string myCity;
        public string myState;
        public string myZip;
        public string myZip4;
        public string myCounty;
        public string myAddressSame;
        public string myGender;
        public string myMaritalStatus;
        public string myDOB;
        public string myLiveMN;
        public string myPlanLiveMN;
        public string myPrefContact;
        public string myPhoneNum;
        public string myPhoneType;
        public string myAltNum;
        public string myAltNumType;
        public string myEmail;
        public string myLanguageMost;
        public string myLanguageWritten;
        public string myHomeless;
        public string myVoterCard;
        public string myNotices;
        public string myAuthRep;
        public string myApplyYourself;
        public string myHispanic;
        public string myFederalTribe;
        public string myTribeId;
        public string myTribeName;
        public string myLiveRes;
        public string myRace;
        public string mySSN;
        public string myCitizen;
        public string mySSNNum;
        public string myHouseholdOther;
        public string myDependants;
        public string myIncomeYN;
        public string myIncomeType;
        public string myIncomeAmount;
        public string myIncomeFrequency;
        public string myIncomeMore;
        public string myIncomeEmployer;
        public string myIncomeSeasonal;
        public string myIncomeReduced;
        public string myIncomeAdjusted;
        public string myIncomeExpected;
        public string myEnrollmentPlanType;
        public string myFosterCare;
        public string myMailingAddressYN;
    }
    struct mystructSSN
    {
        public string myLastSSN;
    }

    struct mystructNavHelper
    {
        public string myConfigureClicked;
    }

    struct mystructReadFileValues
    {
        public string mySSN;
        public string myAddress1;
        public string myAddress2;
        public string myCity;
        public string myState;
        public string myZip;
        public string myZip4;
        public string myEmail;
        public string myPhone;
        public int myAccountSaveFileNum;
        public string myAccountSaveFileName;

    }


    struct mystructHouseholdMembers
    {
        public int HouseMembersID;
        public int TestId;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public string Suffix;
        public string Gender;
        public string MaritalStatus;
        public string DOB;
        public string LiveWithYou;
        public string MNHome;
        public string PersonHighlighted;
        public string LiveInMN;
        public string TempAbsentMN;
        public string Homeless;
        public string Address1;
        public string Address2;
        public string AptSuite;
        public string City;
        public string State;
        public string Zip;
        public string PlanMakeMNHome;
        public string SeekEmplMN;
        public string Hispanic;
        public string FederalTribe;
        public string TribeId;
        public string TribeName;
        public string LiveRes;
        public string Race;
        public string HaveSSN; 
        public string SSN;
        public string USCitizen;
        public string USNational;
        public string IsPregnant;
        public string BeenInFosterCare;
        public string Relationship;
        public string HasIncome;
        public string RelationshiptoNextHM;
        public int NumMembers;
    }

    struct mystructEditKey
    {
        public string myWindowsFirstTime;
        public string myWindowsEditKey;
        public string myWindowsDeletedRow;
        public string myMethodFirstTime;
        public string myMethodDeletedRow;
        public string myMethodEditKey;
        public string myTestFirstTime;
        public string myTestDeletedRow;
        public string myTestEditKey;
        public string myTemplateFirstTime;
        public string myTemplateDeletedRow;
        public string myTemplateEditKey;
    }


namespace MNsure_Regression_1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Mnsure Regression 1\");

            Application.Run(new FormMain());

        }
    }
}
