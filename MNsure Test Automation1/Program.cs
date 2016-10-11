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
    public int myAppWait;
    public string myInTimeTravel;
    public DateTime myTimeTravelDate;
    public string myExecutedBy;
    public string myRelogin;
    public string myEnvironment;
    public string myAssisterNavigator;
    public string myBrowser;
    public string myResume;
    public int myMultiples;
    public DateTime myScheduleRunTime;
    public string myMNSureID;
}

struct mystructExistingAccounts
{
    public string[] myExistingAccountFirstName;
    public string[] myExistingAccountMiddleName;
    public string[] myExistingAccountLastName;
    public string[] myExistingAccountSuffix;
    public string[] myExistingAccountAddress1;
    public string[] myExistingAccountAddress2;
    public string[] myExistingAccountCity;
    public string[] myExistingAccountState;
    public string[] myExistingAccountZip;
    public string[] myExistingAccountZip4;
    public string[] myExistingAccountEmail;
    public string[] myExistingAccountPhone;
    public string[] myExistingAccountSSN;
    public string[] myExistingAccountDOB;
    public string[] myExistingAccountUserName;
    public string[] myExistingAccountPassword;
    public string[] myExistingAccountSecret;
    public string[] myExistingAccountQuestion1;
    public string[] myExistingAccountAnswer1;
    public string[] myExistingAccountQuestion2;
    public string[] myExistingAccountAnswer2;
    public string[] myExistingAccountQuestion3;
    public string[] myExistingAccountAnswer3;
    public string[] myExistingAccountQuestion4;
    public string[] myExistingAccountAnswer4;
    public string[] myExistingAccountQuestion5;
    public string[] myExistingAccountAnswer5;
    public string[] myExistingAccountConfirmation;
    public string[] myExistingAccountEnvironment;
    public string[] myExistingAccountGender;
    public string[] myExistingAccountUsed;
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
        public string myHomeAddress1;
        public string myHomeAddress2;
        public string myHomeAptSuite;
        public string myHomeCity;
        public string myHomeState;
        public string myHomeZip;
        public string myHomeZip4;
        public string myHomeCounty;
        public string myMailAddress1;
        public string myMailAddress2;
        public string myMailAptSuite;
        public string myMailCity;
        public string myMailState;
        public string myMailZip;
        public string myMailZip4;
        public string myMailCounty;
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
        public string myMilitary;
        public string myMilitaryDate;
        public string myLiveRes;
        public string myRace;
        public string mySSN;
        public string myCitizen;
        public string mySSNNum;
        public string myAppliedSSN;
        public string myWhyNoSSN;
        public string myAssistSSN;
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
        public string myOtherIns;
        public string myKindIns;
        public string myCoverageEnd;
        public string myAddIns;
        public string myESC;
        public string myRenewalCov;
        public string myWithDiscounts;
        public string myIsPregnant;
        public string myChildren;
        public string myDueDate;
        public string myPregnancyEnded;
        public string myRegDate;
        public string myDay2TestId;
        public string myPassCount;
        public string myTobacco;
        public string myTobaccoLast;
        public string myRandom;
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
        public string myHomeAddress1;
        public string myHomeAddress2;
        public string myHomeCity;
        public string myHomeState;
        public string myHomeZip;
        public string myHomeZip4;
        public string myEmail;
        public string myPhone;
        public int myAccountSaveFileNum;
        public string myAccountSaveFileName;
    }

    struct mystructHouseholdMembers
    {
        public int HouseMembersID;
        public int TestId;
        public string myFirstName;
        public string myMiddleName;
        public string myLastName;
        public string mySuffix;
        public string myMailAddress1;
        public string myMailAddress2;
        public string myMailAptSuite;
        public string myMailCity;
        public string myMailState;
        public string myMailZip;
        //public string myHomeZip4;
        public string myMailCounty;        
        public string myGender;
        public string myMaritalStatus;
        public string myDOB;
        public string myFileJointly;
        public string myLiveWithYou;
        public string myMNHome;
        public string myPersonHighlighted;
        public string myLiveInMN;
        public string myTempAbsentMN;
        public string myHomeless;
        public string myPlanMakeMNHome;
        public string mySeekEmplMN;
        public string myHispanic;
        public string myFederalTribe;
        public string myTribeId;
        public string myTribeName;
        public string myMilitary;
        public string myMilitaryDate;
        public string myLiveRes;
        public string myRace;
        public string myHaveSSN;
        public string mySSN;
        public string myUSCitizen;
        public string myUSNational;
        public string myIsPregnant;
        public string myBeenInFosterCare;
        public string myRelationship;
        public string myHasIncome;
        public string myRelationshiptoNextHM;
        public int NumMembers;
        public string myIncomeType;
        public string myIncomeAmount;
        public string myIncomeFrequency;
        public string myIncomeMore;
        public string myIncomeEmployer;
        public string myIncomeSeasonal;
        public string myIncomeReduced;
        public string myIncomeAdjusted;
        public string myIncomeExpected;
        public string myPrefContact;
        public string myPhoneNum;
        public string myPhoneType;
        public string myAltNum;
        public string myAltNumType;
        public string myEmail;
        public string myVoterCard;
        public string myNotices;
        public string myAuthRep;
        public string myDependants;
        public string myTaxFiler;
        public string myChildren;
        public string myDueDate;
        public string myPregnancyEnded;
        public string myReEnroll;
        public string mySaveExit;
        public string myPassCount;
        public string myRandom;
    }
    
    struct mystructAssister
    {
        public string AssisterId;
        public int TestId;
        public string myFirstName;
        public string myLastName;
        public string myCommunication;
        public string myLanguage;
        public string myMethod;
        public string myPhoneType;
        public string myPhoneNum;
        public string myCategory;
        public string myType;
        public string myAddress1;
        public string myAddress2;
        public string myAptSuite;
        public string myCity;
        public string myState;
        public string myZip;
        public string myCounty; 
        public string myEmail;
        public string myRefNumber;
        public string mySSN;
        public string myDOB;
        public string myRegNumber;
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
        public string myNextAddressId;
        public string myNextAssisterId;
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
