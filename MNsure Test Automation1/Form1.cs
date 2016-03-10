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

using OpenQA.Selenium.Support.UI; // for dropdown

using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Reflection;

namespace MNsure_Regression_1
{
    public partial class FormMain : Form
    {
        mystructSelectedTest mySelectedTest;
        mystructAccountCreate myAccountCreate;
        mystructHistoryInfo myHistoryInfo;
        mystructApplication myApplication;
        mystructSSN myLastSSN;
        mystructNavHelper myNavHelper;
        mystructReadFileValues myReadFileValues;
        mystructHouseholdMembers myHouseholdMembers;
        mystructEditKey myEditKey;
        private BindingSource bs;

        public FormMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {

            myHistoryInfo.myRequiredScreenshots = new string[30];
            myHistoryInfo.myRequiredStep = new int[30];
            myHistoryInfo.myRequiredStepStatus = new string[30];
            myHistoryInfo.myRequiredScreenshotFile = new string[30];
            myHistoryInfo.myTestStartTime = DateTime.Now;
            this.WindowState = FormWindowState.Minimized;

            string mysTestId;
            int result;
            int rowindex;

            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            int testcount;
            testcount = dataGridViewSelectedTests.RowCount;
            myHistoryInfo.myFirstTime = "Yes";

            WriteLogs writeLogs = new WriteLogs();
            mysTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            mySelectedTest.myTestId = Convert.ToInt32(mysTestId);
            myHistoryInfo.myTestId = mysTestId;
            myHistoryInfo.myTemplateFolder = "C:\\Mnsure Regression 1\\Templates\\";

            int iloop = 1;

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            object reflectResult = null;
            object reflectResultac = null;
            object reflectResultad = null;
            object reflectResulten = null;
            object reflectResultcw = null;
            //This loops through based on the number of tests selected to run
            for (iloop = 1; iloop <= testcount - 1; iloop++)
            {
                //must clear cache first
                FirefoxProfile profile = new FirefoxProfile();
                profile.SetPreference("browser.cache.disk.enable", false);
                profile.SetPreference("browser.cache.memory.enable", false);
                profile.SetPreference("browser.cache.offline.enable", false);
                profile.SetPreference("network.http.use-cache", false);

                //create separate driver for case worker
                FirefoxDriver driver2 = new FirefoxDriver(profile);
                driver2.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));

                FirefoxDriver driver = new FirefoxDriver();
                driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));

                myHistoryInfo.myTestStepStatus = "none";
                mysTestId = dataGridViewSelectedTests.Rows[iloop - 1].Cells[0].Value.ToString();
                mySelectedTest.myTestId = Convert.ToInt32(mysTestId);
                myHistoryInfo.myTestId = mysTestId;

                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com3 = new SqlCeCommand("SELECT TemplateName FROM TestTemplates where TestId = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader2 = com3.ExecuteReader();
                    if (reader2.Read())
                    {
                        myHistoryInfo.myTemplate = reader2.GetString(0);
                    }
                }
                con.Close();

                result = writeLogs.WriteRunHistoryRowStart(ref myHistoryInfo);
                result = writeLogs.WriteTestHistoryRowStart(ref myHistoryInfo);

                try
                {
                    //Fill structures for Test
                    InitializeSSN myInitializeSSN = new InitializeSSN();
                    result = myInitializeSSN.DoReadLines(ref myLastSSN, ref myReadFileValues);
                    int temp1 = Convert.ToInt32(myLastSSN.myLastSSN) + 1;
                    myAccountCreate.mySSN = Convert.ToString(temp1);
                    FillStructures myFillStructures = new FillStructures();
                    result = myFillStructures.doCreateAccount(ref mySelectedTest, ref myAccountCreate, ref myApplication);
                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHistoryInfo);
                    result = writeLogs.DoGetRequiredScreenshots(ref myHistoryInfo);
                    myLastSSN.myLastSSN = myApplication.mySSNNum;
                    InitializeSSN myInitializeSSN2 = new InitializeSSN();
                    result = myInitializeSSN2.DoWriteLines(ref myLastSSN, myReadFileValues);

                    con = new SqlCeConnection(conString);
                    con.Open();
                    string myClass;
                    string myMethod;
                    int myiTestStepId;
                    myiTestStepId = 1;
                    string myWindow;
                    using (SqlCeCommand com2 = new SqlCeCommand("SELECT TestStepId, Class, Method, Window FROM TestSteps where TestId = " + mysTestId, con))
                    {
                        myiTestStepId = myiTestStepId + 1;
                        SqlCeDataReader reader = com2.ExecuteReader();
                        while (reader.Read() && myHistoryInfo.myTestStepStatus != "Fail")
                        {
                            myiTestStepId = reader.GetInt32(0);
                            myClass = reader.GetString(1);
                            myMethod = reader.GetString(2);
                            myWindow = reader.GetString(3);
                            myHistoryInfo.myTestStepId = Convert.ToString(myiTestStepId);
                            myHistoryInfo.myTestStepClass = myClass;
                            myHistoryInfo.myTestStepMethod = myMethod;
                            myHistoryInfo.myTestStepWindow = myWindow;
                            myHistoryInfo.myScreenShot = "";
                            string returnStatus = "";
                            string returnException = "";
                            string returnScreenshot = "";
                            string returnICNumber = "";

                            switch (myClass)
                            {
                                case "OpenSiteURL":
                                    object[] parms = new object[6];
                                    parms[0] = driver;
                                    parms[1] = driver2;
                                    parms[2] = myHistoryInfo;
                                    parms[3] = returnStatus;
                                    parms[4] = returnException;
                                    parms[5] = returnScreenshot;

                                    OpenSiteURL newOpenSiteURL = new OpenSiteURL();
                                    Type reflectTestType = typeof(OpenSiteURL);
                                    MethodInfo reflectMethodToInvoke = reflectTestType.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParameters = reflectMethodToInvoke.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResult = reflectMethodToInvoke.Invoke(new OpenSiteURL(), parms);
                                    myHistoryInfo.myTestStepStatus = parms[3].ToString();
                                    myHistoryInfo.myStepException = parms[4].ToString();
                                    myHistoryInfo.myScreenShot = parms[5].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    break;

                                case "AccountCreation":
                                    object[] parmsac = new object[7];
                                    parmsac[0] = driver;
                                    parmsac[1] = myAccountCreate;
                                    parmsac[2] = myApplication;
                                    parmsac[3] = myHistoryInfo;
                                    parmsac[4] = returnStatus;
                                    parmsac[5] = returnException;
                                    parmsac[6] = returnScreenshot;

                                    AccountCreation newAccount = new AccountCreation();
                                    Type reflectTestTypeac = typeof(AccountCreation);
                                    MethodInfo reflectMethodToInvokeac = reflectTestTypeac.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParametersac = reflectMethodToInvokeac.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResultac = reflectMethodToInvokeac.Invoke(newAccount, parmsac);
                                    myHistoryInfo.myTestStepStatus = parmsac[4].ToString();
                                    myHistoryInfo.myStepException = parmsac[5].ToString();
                                    myHistoryInfo.myScreenShot = parmsac[6].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    break;

                                case "ApplicationDo":
                                    object[] parmsad = new object[8];
                                    parmsad[0] = driver;
                                    parmsad[1] = myAccountCreate;
                                    parmsad[2] = myApplication;
                                    parmsad[3] = myHouseholdMembers;
                                    parmsad[4] = myHistoryInfo;
                                    parmsad[5] = returnStatus;
                                    parmsad[6] = returnException;
                                    parmsad[7] = returnScreenshot;

                                    ApplicationDo myApplicationDo = new ApplicationDo();
                                    Type reflectTestTypead = typeof(ApplicationDo);
                                    MethodInfo reflectMethodToInvokead = reflectTestTypead.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParametersad = reflectMethodToInvokead.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResultad = reflectMethodToInvokead.Invoke(myApplicationDo, parmsad);
                                    myHistoryInfo.myTestStepStatus = parmsad[5].ToString();
                                    myHistoryInfo.myStepException = parmsad[6].ToString();
                                    myHistoryInfo.myScreenShot = parmsad[7].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    break;

                                case "Enrollments":
                                    object[] parmsen = new object[6];
                                    parmsen[0] = driver;
                                    parmsen[1] = myApplication;
                                    parmsen[2] = myHistoryInfo;
                                    parmsen[3] = returnStatus;
                                    parmsen[4] = returnException;
                                    parmsen[5] = returnScreenshot;

                                    Enrollments myEnrollments = new Enrollments();
                                    Type reflectTestTypeen = typeof(Enrollments);
                                    MethodInfo reflectMethodToInvokeen = reflectTestTypeen.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParametersen = reflectMethodToInvokeen.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResulten = reflectMethodToInvokeen.Invoke(myEnrollments, parmsen);
                                    myHistoryInfo.myTestStepStatus = parmsen[3].ToString();
                                    myHistoryInfo.myStepException = parmsen[4].ToString();
                                    myHistoryInfo.myScreenShot = parmsen[5].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    break;

                                case "CaseWorker":
                                    object[] parmscw = new object[8];
                                    parmscw[0] = driver2;
                                    parmscw[1] = myAccountCreate;
                                    parmscw[2] = myApplication;
                                    parmscw[3] = myHistoryInfo;
                                    parmscw[4] = returnStatus;
                                    parmscw[5] = returnException;
                                    parmscw[6] = returnScreenshot;
                                    parmscw[7] = returnICNumber;

                                    CaseWorker myCaseWorker = new CaseWorker();
                                    Type reflectTestTypecw = typeof(CaseWorker);
                                    MethodInfo reflectMethodToInvokecw = reflectTestTypecw.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParameterscw = reflectMethodToInvokecw.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResultcw = reflectMethodToInvokecw.Invoke(new CaseWorker(), parmscw);
                                    myHistoryInfo.myTestStepStatus = parmscw[4].ToString();
                                    myHistoryInfo.myStepException = parmscw[5].ToString();
                                    myHistoryInfo.myScreenShot = parmscw[6].ToString();
                                    if (parmscw[7].ToString() != String.Empty)
                                    {
                                        myHistoryInfo.myIcnumber = parmscw[7].ToString();
                                    }
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    break;

                                default:
                                    MessageBox.Show("End of cases");
                                    break;
                            }
                        }
                    }
                    result = writeLogs.DoWriteTestHistoryEnd(ref myHistoryInfo, myAccountCreate, myApplication);
                    con.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show("Write New Suite Test didn't work, Exception: " + a);
                }
            }
            MessageBox.Show("The test run is complete. For more info see c:\\TemplatesRun\\", "Test Run Complete", MessageBoxButtons.OK, MessageBoxIcon.None,
                MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST
            return;  //exit now
        }

        private void buttonConfigureTest_Click(object sender, EventArgs e)
        {
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string mysRowid;
            mysRowid = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            int myTestId;
            myTestId = Convert.ToInt32(mysRowid);
            mySelectedTest.myTestId = myTestId;
            mySelectedTest.myTestName = dataGridViewSelectedTests.Rows[rowindex].Cells[1].Value.ToString();
            mySelectedTest.myTestDescr = dataGridViewSelectedTests.Rows[rowindex].Cells[3].Value.ToString();
            mySelectedTest.myTestType = dataGridViewSelectedTests.Rows[rowindex].Cells[2].Value.ToString();
            mySelectedTest.myTestRunId = Convert.ToInt32(dataGridViewSelectedTests.Rows[rowindex].Cells[4].Value.ToString());

            if (mySelectedTest.myTestType == "Create Account Single")
            {
                myNavHelper.myConfigureClicked = "Yes";
                tabControlMain.SelectedIndex = 1;
            }
            else if (mySelectedTest.myTestType == "Application")
            {
                myNavHelper.myConfigureClicked = "Yes";
                tabControlMain.SelectedIndex = 2;
            }
            else if (mySelectedTest.myTestType == "Enroll")
            {
                myNavHelper.myConfigureClicked = "Yes";
                tabControlMain.SelectedIndex = 3;
            }

        }

        private void tabPageHistory_Enter(object sender, EventArgs e)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from RunHistory order by RunId desc;";

            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewTestRunHistory.DataSource = dt;

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewTestRunHistory.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewTestHistory.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void dataGridViewSuiteHistory_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex = dataGridViewTestRunHistory.CurrentCell.RowIndex;
            String mysRunid;
            mysRunid = dataGridViewTestRunHistory.Rows[rowindex].Cells[0].Value.ToString();
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from  TestHistory where RunId = " + mysRunid + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewTestHistory.DataSource = dt;

        }

        private void tabPageConfigureEnrollment_Enter(object sender, EventArgs e)
        {
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string mysRowid;
            mysRowid = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            int myTestId;

            myTestId = Convert.ToInt32(mysRowid);
            if (myNavHelper.myConfigureClicked == "No")
            {
                tabControlMain.SelectedIndex = 0;
            }
            else
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
                    using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Application where TestId = " + myTestId, con))
                    {
                        SqlCeDataReader reader = com2.ExecuteReader();
                        if (reader.Read())
                        {
                            myApplication.myFirstName = myAccountCreate.myFirstName;
                            myApplication.myMiddleName = myAccountCreate.myMiddleName;
                            myApplication.myLastName = myAccountCreate.myLastName;
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
                            myApplication.myAddressSame = reader.GetString(24);
                            myApplication.myHispanic = reader.GetString(25);
                            myApplication.myRace = reader.GetString(26);
                            myApplication.mySSN = reader.GetString(27);
                            myApplication.myCitizen = reader.GetString(28);
                            myApplication.mySSNNum = myAccountCreate.mySSN;
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
                                myApplication.myMilitaryDate = Convert.ToString(reader.GetDateTime(50));
                            }
                            else
                            {
                                myApplication.myMilitaryDate = null;
                            }
                        }
                        else
                        {
                            //Could generate these or store as a table default row
                            myApplication.myFirstName = myAccountCreate.myFirstName;
                            myApplication.myMiddleName = myAccountCreate.myMiddleName;
                            myApplication.myLastName = myAccountCreate.myLastName;
                            myApplication.mySuffix = "Senior";
                            myApplication.myGender = "Male";
                            myApplication.myMaritalStatus = "Never Married";
                            myApplication.myDOB = Convert.ToString(myAccountCreate.myDOB);
                            myApplication.myLiveMN = "Yes";
                            myApplication.myHomeless = "No";
                            myApplication.myPlanLiveMN = "Yes";
                            myApplication.myPrefContact = "Phone";
                            myApplication.myPhoneNum = "6128129998";
                            myApplication.myPhoneType = "Mobile";
                            myApplication.myAltNum = "6128129987";
                            myApplication.myAltNumType = "Home";
                            myApplication.myEmail = myAccountCreate.myEmail;
                            myApplication.myLanguageMost = "English";
                            myApplication.myLanguageWritten = "English";
                            myApplication.myVoterCard = "Yes";
                            myApplication.myNotices = "Email";
                            myApplication.myAuthRep = "Yes";
                            myApplication.myApplyYourself = "Yes";
                            myApplication.myHomeless = "No";
                            myApplication.myMailingAddressYN = "No";
                            myApplication.myAddressSame = "Yes";
                            myApplication.myHispanic = "No";
                            myApplication.myLiveRes = "No";
                            myApplication.myFederalTribe = "No";
                            myApplication.myMilitary = "No";
                            myApplication.myRace = "White";
                            myApplication.mySSN = "Yes";
                            myApplication.myCitizen = "Yes";
                            myApplication.mySSNNum = myAccountCreate.mySSN;
                            myApplication.myHouseholdOther = "No";
                            myApplication.myDependants = "No";
                            myApplication.myIncomeYN = "Yes";
                            myApplication.myIncomeType = "Wages before taxes";
                            myApplication.myIncomeAmount = "1000";
                            myApplication.myIncomeFrequency = "Yearly";
                            myApplication.myIncomeMore = "No";
                            myApplication.myIncomeEmployer = "Target";
                            myApplication.myIncomeSeasonal = "No";
                            myApplication.myIncomeReduced = "No";
                            myApplication.myIncomeAdjusted = "No";
                            myApplication.myIncomeExpected = "Yes";
                            myApplication.myEnrollmentPlanType = "MN Care BHP";
                            myApplication.myFosterCare = "No";
                        }
                        com2.ExecuteNonQuery();
                        com2.Dispose();
                    }

                    SqlCeCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;

                    //Read configured rows if exist, otherwise fill with default values
                    using (SqlCeCommand com3 = new SqlCeCommand("SELECT * FROM Address where TestId = " + myTestId, con))
                    {
                        SqlCeDataReader reader = com3.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetString(9) == "Home")
                            {
                                myApplication.myHomeAddress1 = reader.GetString(3);
                                int index = reader.GetOrdinal("Address2");
                                if (!reader.IsDBNull(index))
                                {
                                    myApplication.myHomeAddress2 = reader.GetString(4);
                                }
                                myApplication.myHomeCity = reader.GetString(5);
                                myApplication.myHomeState = reader.GetString(6);
                                myApplication.myHomeZip = reader.GetString(7);
                                index = reader.GetOrdinal("Zip4");
                                if (!reader.IsDBNull(index))
                                {
                                    myApplication.myHomeZip4 = reader.GetString(8);
                                }
                                myApplication.myHomeCounty = reader.GetString(10);                                
                                index = reader.GetOrdinal("AptSuite");
                                if (!reader.IsDBNull(index))
                                {
                                    myApplication.myHomeAptSuite = reader.GetString(11);
                                }
                            }
                            else
                            {
                                myApplication.myMailAddress1 = reader.GetString(3);
                                int index = reader.GetOrdinal("Address2");
                                if (!reader.IsDBNull(index))
                                {
                                    myApplication.myMailAddress2 = reader.GetString(4);
                                }
                                myApplication.myMailCity = reader.GetString(5);
                                myApplication.myMailState = reader.GetString(6);
                                myApplication.myMailZip = reader.GetString(7);
                                index = reader.GetOrdinal("Zip4");
                                if (!reader.IsDBNull(index))
                                {
                                    myApplication.myMailZip4 = reader.GetString(8);
                                }
                                myApplication.myMailCounty = reader.GetString(10);
                                index = reader.GetOrdinal("AptSuite");
                                if (!reader.IsDBNull(index))
                                {
                                    myApplication.myMailAptSuite = reader.GetString(11);
                                }
                            }
                        }
                        if (myApplication.myHomeAddress1 == null)
                        {
                            //Could generate these or store as a table default row
                            myApplication.myHomeAddress1 = "12969 First Ave W";
                            myApplication.myHomeAddress2 = "PO Box 44";
                            myApplication.myHomeCity = "Minneapolis";
                            myApplication.myHomeState = "Minnesota";
                            myApplication.myHomeZip = "55401";
                            myApplication.myHomeZip4 = "1111";
                            myApplication.myHomeCounty = "Hennepin";
                            myApplication.myHomeAptSuite = "Suite 64";

                            myApplication.myMailAddress1 = "";
                            myApplication.myMailAddress2 = "";
                            myApplication.myMailCity = "";
                            myApplication.myMailState = "";
                            myApplication.myMailZip = "";
                            myApplication.myMailZip4 = "";
                            myApplication.myMailCounty = "";
                            myApplication.myMailAptSuite = "";
                        }
                        com3.ExecuteNonQuery();
                        com3.Dispose();
                    }
                }
                catch (Exception f)
                {
                    MessageBox.Show("Did not find test data for enroll " + f);
                }

                textBoxEnrollTest.Text = mySelectedTest.myTestName;
                textBoxEnrollFirstName.Text = myAccountCreate.myFirstName;
                textBoxEnrollMiddleName.Text = myAccountCreate.myMiddleName;
                textBoxEnrollLastName.Text = myAccountCreate.myLastName;
                comboBoxEnrollSuffix.Text = myApplication.mySuffix;
                comboBoxEnrollAddressSame.Text = myApplication.myAddressSame;
                comboBoxHomeCounty.Text = myApplication.myHomeCounty;
                comboBoxEnrollGender.Text = myApplication.myGender;
                comboBoxEnrollMaritalStatus.Text = myApplication.myMaritalStatus;
                if (myApplication.myDOB == null)
                {
                    textBoxEnrollDOB.Text = myAccountCreate.myDOB;
                }
                else
                {
                    textBoxEnrollDOB.Text = myApplication.myDOB;
                }
                textBoxHomeAddr1.Text = myApplication.myHomeAddress1;
                if (myApplication.myHomeAddress2 != null)
                {
                    textBoxHomeAddr2.Text = myApplication.myHomeAddress2;
                }
                textBoxHomeCity.Text = myApplication.myHomeCity;
                comboBoxHomeState.Text = myApplication.myHomeState;
                textBoxHomeZip.Text = myApplication.myHomeZip;
                if (myApplication.myHomeZip4 != null)
                {
                    textBoxHomeZip4.Text = myApplication.myHomeZip4;
                }
                if (myApplication.myHomeAptSuite != null)
                {
                    textBoxHomeAptSuite.Text = myApplication.myHomeAptSuite;
                }
                textBoxMailAddr1.Text = myApplication.myMailAddress1;
                if (myApplication.myMailAddress2 != null)
                {
                    textBoxMailAddr2.Text = myApplication.myMailAddress2;
                }
                textBoxMailCity.Text = myApplication.myMailCity;
                comboBoxMailState.Text = myApplication.myMailState;
                textBoxMailZip.Text = myApplication.myMailZip;
                if (myApplication.myMailZip4 != null)
                {
                    textBoxMailZip4.Text = myApplication.myMailZip4;
                }
                if (myApplication.myMailAptSuite != null)
                {
                    textBoxMailAptSuite.Text = myApplication.myMailAptSuite;
                }
                comboBoxMailCounty.Text = myApplication.myMailCounty;
                comboBoxLiveMN.Text = myApplication.myLiveMN;
                comboBoxMailAddrYN.Text = myApplication.myMailingAddressYN;
                comboBoxPlanLiveMN.Text = myApplication.myPlanLiveMN;
                comboBoxEnrollPrefContact.Text = myApplication.myPrefContact;
                textBoxPhoneNum.Text = myApplication.myPhoneNum;
                comboBoxPhoneType.Text = myApplication.myPhoneType;
                textBoxEnrollAltNum.Text = myApplication.myAltNum;
                comboBoxEnrollAltPhoneType.Text = myApplication.myAltNumType;
                textBoxEnrollEmail.Text = myAccountCreate.myEmail;
                comboBoxEnrollLanguageMost.Text = myApplication.myLanguageMost;
                comboBoxEnrollLanguageWritten.Text = myApplication.myLanguageWritten;
                comboBoxEnrollHomeless.Text = myApplication.myHomeless;
                comboBoxEnrollVoterCard.Text = myApplication.myVoterCard;
                comboBoxEnrollNotices.Text = myApplication.myNotices;
                comboBoxEnrollAuthRep.Text = myApplication.myAuthRep;
                comboBoxEnrollApplyYourself.Text = myApplication.myApplyYourself;
                comboBoxEnrollHispanic.Text = myApplication.myHispanic;
                textBoxTribeName.Text = myApplication.myTribeName;
                textBoxTribeId.Text = myApplication.myTribeId;
                comboBoxRace.Text = myApplication.myRace;
                comboBoxLiveRes.Text = myApplication.myLiveRes;
                comboBoxFederalTribe.Text = myApplication.myFederalTribe;
                comboBoxMilitary.Text = myApplication.myMilitary;
                dateTimeMilitary.Text = myApplication.myMilitaryDate;
                if (myApplication.myMilitaryDate != null)
                {
                    string tempMilitary;
                    tempMilitary = Convert.ToString(myApplication.myMilitaryDate);
                    tempMilitary = DateTime.Parse(tempMilitary).ToString("MM/dd/yyyy");
                    dateTimeMilitary.Format = DateTimePickerFormat.Short;
                    dateTimeMilitary.Value = Convert.ToDateTime(tempMilitary);
                }
                else
                {
                    dateTimeMilitary.CustomFormat = " ";
                    dateTimeMilitary.Format = DateTimePickerFormat.Custom;
                }
                comboBoxEnrollSSN.Text = myApplication.mySSN;
                textBoxEnrollSSNNum.Text = myApplication.mySSNNum;
                comboBoxEnrollCitizen.Text = myApplication.myCitizen;
                comboBoxEnrollHouseholdOther.Text = myApplication.myHouseholdOther;
                comboBoxEnrollDependants.Text = myApplication.myDependants;
                comboBoxEnrollIncomeYN.Text = myApplication.myIncomeYN;
                comboBoxEnrollIncomeType.Text = myApplication.myIncomeType;
                textBoxEnrollAmount.Text = myApplication.myIncomeAmount;
                comboBoxEnrollFrequency.Text = myApplication.myIncomeFrequency;
                comboBoxEnrollMoreIncome.Text = myApplication.myIncomeMore;
                textBoxEnrollIncomeEmployer.Text = myApplication.myIncomeEmployer;
                comboBoxEnrollIncomeSeasonal.Text = myApplication.myIncomeSeasonal;
                comboBoxEnrollIncomeReduced.Text = myApplication.myIncomeReduced;
                comboBoxEnrollIncomeAdjustments.Text = myApplication.myIncomeAdjusted;
                comboBoxEnrollIncomeExpected.Text = myApplication.myIncomeExpected;
                textBoxEnrollFosterCare.Text = myApplication.myFosterCare;

                groupBoxApplicantInformation.Visible = true;
                groupBoxMoreAboutYou.Visible = false;
                groupBoxHouseholdOther.Visible = false;
                groupBoxDependants.Visible = false;
                groupBoxEnrollIncome.Visible = false;
            }
            radioButtonInformation.Checked = true;
            buttonSaveConfiguration.BackColor = Color.Yellow;
        }

        private void buttonSaveConfiguration_Click(object sender, EventArgs e)
        {
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string mysTestId;
            mysTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            myApplication.myFirstName = textBoxEnrollFirstName.Text;
            myApplication.myMiddleName = textBoxEnrollMiddleName.Text;
            myApplication.myLastName = textBoxEnrollLastName.Text;
            myApplication.mySuffix = comboBoxEnrollSuffix.Text;
            myApplication.myHomeAddress1 = textBoxHomeAddr1.Text;
            myApplication.myHomeAddress2 = textBoxHomeAddr2.Text;
            myApplication.myHomeAptSuite = textBoxHomeAptSuite.Text;
            myApplication.myHomeCity = textBoxHomeCity.Text;
            myApplication.myHomeState = comboBoxHomeState.Text;
            myApplication.myHomeZip = textBoxHomeZip.Text;
            myApplication.myHomeZip4 = textBoxHomeZip4.Text;
            myApplication.myHomeCounty = comboBoxHomeCounty.Text;
            myApplication.myMailAddress1 = textBoxMailAddr1.Text;
            myApplication.myMailAddress2 = textBoxMailAddr2.Text;
            myApplication.myMailAptSuite = textBoxMailAptSuite.Text;
            myApplication.myMailCity = textBoxMailCity.Text;
            myApplication.myMailState = comboBoxMailState.Text;
            myApplication.myMailZip = textBoxMailZip.Text;
            myApplication.myMailZip4 = textBoxMailZip4.Text;
            myApplication.myMailCounty = comboBoxMailCounty.Text;
            myApplication.myAddressSame = comboBoxEnrollAddressSame.Text;
            myApplication.myGender = comboBoxEnrollGender.Text;
            myApplication.myMaritalStatus = comboBoxEnrollMaritalStatus.Text;
            myApplication.myDOB = textBoxEnrollDOB.Text;
            myApplication.myMailingAddressYN = comboBoxMailAddrYN.Text;
            myApplication.myLiveMN = comboBoxLiveMN.Text;
            myApplication.myPlanLiveMN = comboBoxPlanLiveMN.Text;
            myApplication.myPrefContact = comboBoxEnrollPrefContact.Text;
            myApplication.myPhoneNum = textBoxPhoneNum.Text;
            myApplication.myPhoneType = comboBoxPhoneType.Text;
            myApplication.myAltNum = textBoxEnrollAltNum.Text;
            myApplication.myAltNumType = comboBoxEnrollAltPhoneType.Text;
            myApplication.myEmail = textBoxEnrollEmail.Text;
            myApplication.myLanguageMost = comboBoxEnrollLanguageMost.Text;
            myApplication.myLanguageWritten = comboBoxEnrollLanguageWritten.Text;
            myApplication.myHomeless = comboBoxEnrollHomeless.Text;
            myApplication.myVoterCard = comboBoxEnrollVoterCard.Text;
            myApplication.myNotices = comboBoxEnrollNotices.Text;
            myApplication.myAuthRep = comboBoxEnrollAuthRep.Text;
            myApplication.myApplyYourself = comboBoxEnrollApplyYourself.Text;
            myApplication.myHispanic = comboBoxEnrollHispanic.Text;
            myApplication.myRace = comboBoxRace.Text;
            myApplication.myTribeName = textBoxTribeName.Text;
            myApplication.myTribeId = textBoxTribeId.Text;
            myApplication.myLiveRes = comboBoxLiveRes.Text;
            myApplication.myFederalTribe = comboBoxFederalTribe.Text;
            myApplication.myMilitary = comboBoxMilitary.Text;
            if (myApplication.myMilitaryDate != null)
            {
                myApplication.myMilitaryDate = dateTimeMilitary.Text;
            }
            myApplication.mySSN = comboBoxEnrollSSN.Text;
            myApplication.mySSNNum = textBoxEnrollSSNNum.Text;
            myApplication.myCitizen = comboBoxEnrollCitizen.Text;
            myApplication.myHouseholdOther = comboBoxEnrollHouseholdOther.Text;
            myApplication.myDependants = comboBoxEnrollDependants.Text;
            myApplication.myIncomeYN = comboBoxEnrollIncomeYN.Text;
            myApplication.myIncomeType = comboBoxEnrollIncomeType.Text;
            myApplication.myIncomeAmount = textBoxEnrollAmount.Text;
            myApplication.myIncomeFrequency = comboBoxEnrollFrequency.Text;
            myApplication.myIncomeMore = comboBoxEnrollMoreIncome.Text;
            myApplication.myIncomeEmployer = textBoxEnrollIncomeEmployer.Text;
            myApplication.myIncomeSeasonal = comboBoxEnrollIncomeSeasonal.Text;
            myApplication.myIncomeReduced = comboBoxEnrollIncomeReduced.Text;
            myApplication.myIncomeAdjusted = comboBoxEnrollIncomeAdjustments.Text;
            myApplication.myIncomeExpected = comboBoxEnrollIncomeExpected.Text;
            myApplication.myFosterCare = textBoxEnrollFosterCare.Text;

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                try
                {
                    cmd2.CommandText = "Delete from Application where TestId = " + mysTestId + ";";
                    cmd2.ExecuteNonQuery();
                }
                catch
                {
                    //fail silently
                }

                //Basic Enrollment stuff
                string myInsertString;
                myInsertString = "Insert into Application values (" + 1 + ", " + mysTestId +
                                    ", @FirstName, @MiddleName, @LastName, @Suffix, @Gender, @MaritalStatus, " +
                                    "@DOB , @LiveMN, @PlanLiveMN, @PrefContact, @PhoneNum, @PhoneType, @AltNum, @AltType, @Email, @LanguageMost," +
                                    "@WrittenLanguage, @VoterCard, @Notices, @AuthRep, @ApplyYourself, @Homeless, @AddressSame, @Hispanic," +
                                    "@Race, @SSN, @Citizen, @SSNNum, @Household, @Dependants, @IncomeYN, @IncomeType, @IncomeAmount, @IncomeFrequency," +
                                    "@IncomeMore, @Employer, @Seasonal, @Reduced, @Adjusted, @Expected, @PlanType, @Foster, @MailAddrYN, @TribeName," +
                                    "@LiveRes, @TribeId, @FederalTribe, @Military, @MilitaryDate );";
                using (SqlCeCommand com2 = new SqlCeCommand(myInsertString, con))
                {
                    com2.Parameters.AddWithValue("FirstName", myApplication.myFirstName);
                    com2.Parameters.AddWithValue("MiddleName", myApplication.myMiddleName);
                    com2.Parameters.AddWithValue("LastName", myApplication.myLastName);
                    com2.Parameters.AddWithValue("Suffix", myApplication.mySuffix);
                    com2.Parameters.AddWithValue("Gender", myApplication.myGender);
                    com2.Parameters.AddWithValue("MaritalStatus", myApplication.myMaritalStatus);
                    if (myApplication.myDOB != "")
                    {
                        com2.Parameters.AddWithValue("DOB", myApplication.myDOB);
                    }
                    else
                    {
                        myApplication.myDOB = "01/01/2011"; // special situation
                        com2.Parameters.AddWithValue("DOB", myApplication.myDOB);
                    }
                    com2.Parameters.AddWithValue("LiveMN", myApplication.myLiveMN);
                    com2.Parameters.AddWithValue("PlanLiveMN", myApplication.myPlanLiveMN);
                    com2.Parameters.AddWithValue("PrefContact", myApplication.myPrefContact);
                    com2.Parameters.AddWithValue("PhoneNum", myApplication.myPhoneNum);
                    com2.Parameters.AddWithValue("PhoneType", myApplication.myPhoneType);
                    com2.Parameters.AddWithValue("AltNum", myApplication.myAltNum);
                    com2.Parameters.AddWithValue("AltType", myApplication.myAltNumType);
                    com2.Parameters.AddWithValue("Email", myApplication.myEmail);
                    com2.Parameters.AddWithValue("LanguageMost", myApplication.myLanguageMost);
                    com2.Parameters.AddWithValue("WrittenLanguage", myApplication.myLanguageWritten);
                    com2.Parameters.AddWithValue("VoterCard", myApplication.myVoterCard);
                    com2.Parameters.AddWithValue("Notices", myApplication.myNotices);
                    com2.Parameters.AddWithValue("AuthRep", myApplication.myAuthRep);
                    com2.Parameters.AddWithValue("ApplyYourself", myApplication.myApplyYourself);
                    com2.Parameters.AddWithValue("Homeless", myApplication.myHomeless);
                    com2.Parameters.AddWithValue("AddressSame", myApplication.myAddressSame);
                    com2.Parameters.AddWithValue("Hispanic", myApplication.myHispanic);
                    com2.Parameters.AddWithValue("FederalTribe", myApplication.myFederalTribe);
                    com2.Parameters.AddWithValue("TribeName", myApplication.myTribeName);
                    com2.Parameters.AddWithValue("LiveRes", myApplication.myLiveRes);
                    com2.Parameters.AddWithValue("TribeId", myApplication.myTribeId);
                    com2.Parameters.AddWithValue("Military", myApplication.myMilitary);
                    if (myApplication.myMilitaryDate != null)
                    {
                        com2.Parameters.AddWithValue("MilitaryDate", myApplication.myMilitaryDate);
                    }
                    else
                    {
                        com2.Parameters.AddWithValue("MilitaryDate", DBNull.Value);
                    }
                    com2.Parameters.AddWithValue("Race", myApplication.myRace);
                    com2.Parameters.AddWithValue("SSN", myApplication.mySSN);
                    com2.Parameters.AddWithValue("Citizen", myApplication.myCitizen);
                    com2.Parameters.AddWithValue("SSNNum", myApplication.mySSNNum);
                    com2.Parameters.AddWithValue("Household", myApplication.myHouseholdOther);
                    com2.Parameters.AddWithValue("Dependants", myApplication.myDependants);
                    com2.Parameters.AddWithValue("IncomeYN", myApplication.myIncomeYN);
                    com2.Parameters.AddWithValue("IncomeType", myApplication.myIncomeType);
                    com2.Parameters.AddWithValue("IncomeAmount", myApplication.myIncomeAmount);
                    com2.Parameters.AddWithValue("IncomeFrequency", myApplication.myIncomeFrequency);
                    com2.Parameters.AddWithValue("IncomeMore", myApplication.myIncomeMore);
                    com2.Parameters.AddWithValue("Employer", myApplication.myIncomeEmployer);
                    com2.Parameters.AddWithValue("Seasonal", myApplication.myIncomeSeasonal);
                    com2.Parameters.AddWithValue("Reduced", myApplication.myIncomeReduced);
                    com2.Parameters.AddWithValue("Adjusted", myApplication.myIncomeAdjusted);
                    com2.Parameters.AddWithValue("Expected", myApplication.myIncomeExpected);
                    com2.Parameters.AddWithValue("PlanType", myApplication.myEnrollmentPlanType);
                    com2.Parameters.AddWithValue("Foster", myApplication.myFosterCare);
                    com2.Parameters.AddWithValue("MailAddrYN", myApplication.myMailingAddressYN);

                    com2.ExecuteNonQuery();
                    com2.Dispose();
                }

                SqlCeCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                try
                {
                    cmd3.CommandText = "Delete from Address where TestId = " + mysTestId + ";";
                    cmd3.ExecuteNonQuery();
                }
                catch
                {
                    //fail silently
                }

                using (SqlCeCommand com3 = new SqlCeCommand("SELECT max(AddressId) FROM Address", con))
                {
                    SqlCeDataReader reader = com3.ExecuteReader();
                    if (reader.Read())
                    {
                        myEditKey.myNextAddressId = Convert.ToString(reader.GetInt32(0) + 1);
                    }
                    else
                    {
                        MessageBox.Show("Did not find Address id");
                    }
                    com3.Dispose();
                }

                SqlCeCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                try
                {
                    cmd4.CommandText = "Delete from Address where TestId = " + mysTestId + ";";
                    cmd4.ExecuteNonQuery();
                }
                catch
                {
                    //fail silently
                }

                //Basic address stuff
                string myInsertString2;
                myInsertString2 = "Insert into Address values (" + 1 + ", " + mysTestId +
                                    ", @AddressId, @Address1, @Address2, @City, @State, @Zip, @Zip4, @Type, @County, @AptSuite );";
                using (SqlCeCommand com4 = new SqlCeCommand(myInsertString2, con))
                {
                    com4.Parameters.AddWithValue("AddressId", myEditKey.myNextAddressId);
                    com4.Parameters.AddWithValue("Address1", myApplication.myHomeAddress1);
                    if (myApplication.myHomeAddress2 != "")
                    {
                        com4.Parameters.AddWithValue("Address2", myApplication.myHomeAddress2);
                    }
                    else
                    {
                        com4.Parameters.AddWithValue("Address2", DBNull.Value);
                    }
                    com4.Parameters.AddWithValue("City", myApplication.myHomeCity);
                    com4.Parameters.AddWithValue("State", myApplication.myHomeState);
                    com4.Parameters.AddWithValue("Zip", myApplication.myHomeZip);
                    if (myApplication.myHomeZip4 != "")
                    {
                        com4.Parameters.AddWithValue("Zip4", myApplication.myHomeZip4);
                    }
                    else
                    {
                        com4.Parameters.AddWithValue("Zip4", DBNull.Value);
                    }
                    com4.Parameters.AddWithValue("County", myApplication.myHomeCounty);
                    if (myApplication.myHomeAptSuite != "")
                    {
                        com4.Parameters.AddWithValue("AptSuite", myApplication.myHomeAptSuite);
                    }
                    else
                    {
                        com4.Parameters.AddWithValue("AptSuite", DBNull.Value);
                    }
                    com4.Parameters.AddWithValue("Type", "Home");

                    com4.ExecuteNonQuery();
                    com4.Dispose();
                }

                if (myApplication.myMailAddress1 != "")
                {
                    string myInsertString3;
                    myInsertString3 = "Insert into Address values (" + 1 + ", " + mysTestId +
                                    ", @AddressId, @Address1, @Address2, @City, @State, @Zip, @Zip4, @Type, @County, @AptSuite );";
                    using (SqlCeCommand com5 = new SqlCeCommand(myInsertString3, con))
                    {
                        myEditKey.myNextAddressId = Convert.ToString(Convert.ToInt32(myEditKey.myNextAddressId) + 1);

                        com5.Parameters.AddWithValue("AddressId", myEditKey.myNextAddressId);
                        com5.Parameters.AddWithValue("Address1", myApplication.myMailAddress1);
                        if (myApplication.myMailAddress2 != "")
                        {
                            com5.Parameters.AddWithValue("Address2", myApplication.myMailAddress2);
                        }
                        else
                        {
                            com5.Parameters.AddWithValue("Address2", DBNull.Value);
                        }
                        com5.Parameters.AddWithValue("City", myApplication.myMailCity);
                        com5.Parameters.AddWithValue("State", myApplication.myMailState);
                        com5.Parameters.AddWithValue("Zip", myApplication.myMailZip);
                        if (myApplication.myMailZip4 != "")
                        {
                            com5.Parameters.AddWithValue("Zip4", myApplication.myMailZip4);
                        }
                        else
                        {
                            com5.Parameters.AddWithValue("Zip4", DBNull.Value);
                        }
                        com5.Parameters.AddWithValue("County", myApplication.myMailCounty);
                        if (myApplication.myMailAptSuite != "")
                        {
                            com5.Parameters.AddWithValue("AptSuite", myApplication.myMailAptSuite);
                        }
                        else
                        {
                            com5.Parameters.AddWithValue("AptSuite", DBNull.Value);
                        }
                        com5.Parameters.AddWithValue("Type", "Mailing");

                        com5.ExecuteNonQuery();
                        com5.Dispose();
                    }
                }

            }
            catch (Exception f)
            {
                MessageBox.Show("Error Exception: " + f);
            }
            dataGridViewSelectedTests.Rows[mySelectedTest.myRowIndex].Cells[1].Style.BackColor = Color.Green;
            buttonSaveConfiguration.BackColor = Color.Green;
        }

        private void radioButtonMore_Click(object sender, EventArgs e)
        {
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Location = new System.Drawing.Point(18, 40);
            groupBoxMoreAboutYou.Visible = true;
            groupBoxDependants.Visible = false;
            groupBoxHouseholdOther.Visible = false;
            groupBoxEnrollIncome.Visible = false;
        }

        private void radioButtonInformation_Click(object sender, EventArgs e)
        {
            groupBoxApplicantInformation.Location = new System.Drawing.Point(18, 40);
            groupBoxApplicantInformation.Visible = true;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxDependants.Visible = false;
            groupBoxHouseholdOther.Visible = false;
            groupBoxEnrollIncome.Visible = false;
        }

        private void radioButtonHouseholdOther_Click(object sender, EventArgs e)
        {
            groupBoxHouseholdOther.Location = new System.Drawing.Point(18, 40);
            groupBoxHouseholdOther.Visible = true;
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxDependants.Visible = false;
            groupBoxEnrollIncome.Visible = false;
        }

        private void radioButtonEnrollDependants_Click(object sender, EventArgs e)
        {
            groupBoxHouseholdOther.Visible = false;
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxDependants.Location = new System.Drawing.Point(18, 40);
            groupBoxDependants.Visible = true;
            groupBoxEnrollIncome.Visible = false;
        }

        private void radioButtonIncome_Click(object sender, EventArgs e)
        {
            groupBoxHouseholdOther.Visible = false;
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxDependants.Visible = false;
            groupBoxEnrollIncome.Location = new System.Drawing.Point(18, 40);
            groupBoxEnrollIncome.Visible = true;
        }

        private void buttonAddTest_Click(object sender, EventArgs e)
        {
            int rowindex;

            rowindex = dataGridViewAvailableTests.CurrentCell.RowIndex;
            string mysTestId;
            mysTestId = dataGridViewAvailableTests.Rows[rowindex].Cells[0].Value.ToString();

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand(
                    "SELECT * FROM Test where TestId = " + mysTestId + " and IsSelected = 'Yes';", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Test Already exists in Regression");
                    }
                    else
                    {
                        string myUpdateString;
                        myUpdateString = "Update Test set IsSelected = 'Yes' where TestId = " + mysTestId + ";";

                        using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                        {
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Add Test didn't work");
            }
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Test where IsSelected = 'No'" + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewAvailableTests.DataSource = dt;

            con = new SqlCeConnection(conString);
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Test where IsSelected = 'Yes'" + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            SqlCeDataAdapter da2 = new SqlCeDataAdapter(cmd);
            da2.Fill(dt2);
            dataGridViewSelectedTests.DataSource = dt2;
            con.Close();
            //myHistoryInfo.myTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            myHistoryInfo.myTestId = dataGridViewSelectedTests.CurrentCell.Value.ToString();

            int rowCount;
            rowCount = dataGridViewAvailableTests.Rows.Count;
            if (rowCount == 1)
            {
                buttonAddTest.Enabled = false;
            }
            else
            {
                buttonAddTest.Enabled = true;
            }
            rowCount = dataGridViewSelectedTests.Rows.Count;
            if (rowCount == 1)
            {
                buttonRemoveTest.Enabled = false;
            }
            else
            {
                buttonRemoveTest.Enabled = true;
            }
        }

        private void buttonRemoveTest_Click(object sender, EventArgs e)
        {
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            string mysTestId;
            mysTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                string myUpdateString;
                myUpdateString = "Update Test set IsSelected = 'No' where TestId = " + mysTestId + ";";
                using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                {
                    com3.ExecuteNonQuery();
                    com3.Dispose();
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Remove Test didn't work");
            }

            int rCount;
            string buttonEnable = "No";
            rCount = dataGridViewSelectedTests.RowCount - 1;
            if (Convert.ToString(dataGridViewSelectedTests.Rows[rCount].Cells[1].Style.BackColor.Name) == "Yellow")
            {
                buttonEnable = "No";
                buttonGo.Enabled = false;
            }
            else
            {
                buttonEnable = "Yes";
            }
            if (buttonEnable == "Yes")
            {
                buttonGo.Enabled = true;
            }

            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Test where IsSelected = 'No'" + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewAvailableTests.DataSource = dt;

            con = new SqlCeConnection(conString);
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Test where IsSelected = 'Yes'" + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            SqlCeDataAdapter da2 = new SqlCeDataAdapter(cmd);
            da2.Fill(dt2);
            dataGridViewSelectedTests.DataSource = dt2;
            con.Close();
            int rowCount;
            rowCount = dataGridViewAvailableTests.Rows.Count;
            if (rowCount == 1)
            {
                buttonAddTest.Enabled = false;
            }
            else
            {
                buttonAddTest.Enabled = true;
            }
            rowCount = dataGridViewSelectedTests.Rows.Count;
            if (rowCount == 1)
            {
                buttonRemoveTest.Enabled = false;
            }
            else
            {
                buttonRemoveTest.Enabled = true;
            }

            if (rowCount - 1 >= 1)
            {
                myHistoryInfo.myTestId = dataGridViewSelectedTests.CurrentCell.Value.ToString();
            }
            else
            {
                myHistoryInfo.myTestId = null;
            }

        }

        private void tabPageEnroll_Enter(object sender, EventArgs e)
        {
        }

        private void tabPageRun_Enter(object sender, EventArgs e)
        {

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Test where IsSelected = 'No'" + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewAvailableTests.DataSource = dt;

            dataGridViewAvailableTests.AutoGenerateColumns = true;
            DataGridViewColumn Id_Column = dataGridViewAvailableTests.Columns[0];
            Id_Column.Width = 60;
            DataGridViewColumn Name_Column = dataGridViewAvailableTests.Columns[1];
            Name_Column.Width = 175;
            DataGridViewColumn Type_Column = dataGridViewAvailableTests.Columns[2];
            Type_Column.Width = 100;
            DataGridViewColumn Desc_Column = dataGridViewAvailableTests.Columns[3];
            Desc_Column.Width = 175;

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewAvailableTests.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            con = new SqlCeConnection(conString);
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Test where IsSelected = 'Yes'" + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            SqlCeDataAdapter da2 = new SqlCeDataAdapter(cmd);
            da2.Fill(dt2);
            dataGridViewSelectedTests.DataSource = dt2;
            con.Close();

            dataGridViewSelectedTests.AutoGenerateColumns = true;
            DataGridViewColumn Id_Column2 = dataGridViewSelectedTests.Columns[0];
            Id_Column2.Width = 60;
            DataGridViewColumn Name_Column2 = dataGridViewSelectedTests.Columns[1];
            Name_Column2.Width = 175;
            DataGridViewColumn Type_Column2 = dataGridViewSelectedTests.Columns[2];
            Type_Column2.Width = 100;
            DataGridViewColumn Desc_Column2 = dataGridViewSelectedTests.Columns[3];
            Desc_Column2.Width = 175;

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewSelectedTests.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            int rowCount;
            rowCount = dataGridViewAvailableTests.Rows.Count;
            if (rowCount == 1)
            {
                buttonAddTest.Enabled = false;
            }
            else
            {
                buttonAddTest.Enabled = true;
            }
            rowCount = dataGridViewSelectedTests.Rows.Count;
            if (rowCount == 1)
            {
                buttonRemoveTest.Enabled = false;
            }
            else
            {
                buttonRemoveTest.Enabled = true;
            }

            myNavHelper.myConfigureClicked = "No";

            string buttonEnable = "No";
            int numRowsCount = dataGridViewSelectedTests.RowCount;
            for (int iloop = 1; iloop < numRowsCount; iloop++)

                if (Convert.ToString(dataGridViewSelectedTests.Rows[iloop].Cells[1].Style.BackColor.Name) == "Yellow")
                {
                    buttonEnable = "No";
                    buttonGo.Enabled = false;
                    break;
                }
                else
                {
                    buttonEnable = "Yes";
                }
            if (buttonEnable == "Yes")
                buttonGo.Enabled = true;

            if (dataGridViewSelectedTests.CurrentCell == null)
            {
                myHistoryInfo.myTestId = null;
            }
            else
            {
                myHistoryInfo.myTestId = dataGridViewSelectedTests.CurrentCell.Value.ToString();
            }

            myHistoryInfo.myAppBuild = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            labelAppBuild.Text = "Application Build #: " + myHistoryInfo.myAppBuild;
            //labelCuramBuild.Text = "Curam Build #: ";
            textBoxMNSureBuild.Text = "16.1.1d";
            myHistoryInfo.myMnsureBuild = textBoxMNSureBuild.Text;
            myHistoryInfo.myCitizenWait = 20;
            myHistoryInfo.myCaseWorkerWait = 20;
        }

        private void tabPageAccountConfigure_Leave(object sender, EventArgs e)
        {
            myNavHelper.myConfigureClicked = "No";
        }

        private void tabPageConfigureEnrollment_Leave(object sender, EventArgs e)
        {
            myNavHelper.myConfigureClicked = "No";
        }

        private void tabPageEnroll_Leave(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dateTimePickerTimeTravel.Format = DateTimePickerFormat.Short;
            dateTimePickerTimeTravel.Value = DateTime.Today;
        }

        private void comboBoxEnrollHouseholdOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            int result;
            if (comboBoxEnrollHouseholdOther.SelectedIndex == 1)
            {

                buttonAddMember.Enabled = false;
                buttonSaveMember.Enabled = false;
                buttonSaveMember.BackColor = Color.Transparent;
                buttonPreviousMember.Enabled = false;
                buttonNextMember.Enabled = false;
                buttonDeleteMember.Enabled = false;
                return;

            }
            else
            {
                buttonAddMember.Enabled = true;
                buttonSaveMember.Enabled = true;
                textBoxCurrentMember.Text = "2";
                SqlCeConnection con;
                string conString = Properties.Settings.Default.Database1ConnectionString;

                try
                {
                    // Open the connection using the connection string.
                    con = new SqlCeConnection(conString);
                    con.Open();
                    using (SqlCeCommand com2 = new SqlCeCommand("SELECT Count(*) FROM HouseMembers where TestId = " + "'" + mySelectedTest.myTestId + "'", con))
                    {
                        SqlCeDataReader reader = com2.ExecuteReader();
                        if (reader.Read())
                        {
                            //   myHouseholdMembers.HouseMembersID = reader.GetInt32(0);
                            myHouseholdMembers.NumMembers = reader.GetInt32(0);

                            textBoxCurrentMember.Text = "2";
                            textBoxTotalMembers.Text = Convert.ToString(myHouseholdMembers.NumMembers + 1);

                        }
                        else
                        {
                            myHouseholdMembers.HouseMembersID = 0;
                            textBoxTotalMembers.Text = "2";
                        }
                        com2.ExecuteNonQuery();
                        com2.Dispose();
                    }
                    using (SqlCeCommand com2 = new SqlCeCommand("SELECT Min(HouseMembersID) FROM HouseMembers where TestId = " + "'" + mySelectedTest.myTestId + "'", con))
                    {
                        SqlCeDataReader reader = com2.ExecuteReader();
                        if (reader.Read())
                        {
                            myHouseholdMembers.HouseMembersID = reader.GetInt32(0);
                            textBoxCurrentMember.Text = "2";
                        }
                        else
                        {
                            myHouseholdMembers.HouseMembersID = 0;

                        }
                        com2.ExecuteNonQuery();
                        com2.Dispose();
                    }
                }
                catch
                {
                    //Fail silently
                    // MessageBox.Show("Did not get rows from household members table.");
                    myHouseholdMembers.HouseMembersID = 0;
                    textBoxTotalMembers.Text = "2";
                }
                if (myHouseholdMembers.HouseMembersID == 0)
                {
                    AccountGeneration myAccountGeneration = new AccountGeneration();
                    result = myAccountGeneration.GenerateHouseholdNames(mySelectedTest, ref  myHouseholdMembers);
                    myHouseholdMembers.HouseMembersID = Convert.ToInt32(textBoxCurrentMember.Text);
                    buttonSaveMember.BackColor = Color.Yellow;
                    textBoxTotalMembers.Text = "2";
                }
                else
                {
                    HouseholdMembers householdMembers = new HouseholdMembers();
                    result = householdMembers.DoGetHouseholdMember(ref myHouseholdMembers, ref myHistoryInfo, mySelectedTest);
                    buttonSaveMember.BackColor = Color.Transparent;

                }

                //  int result;

                //Will generate new SSN every time, as it must
                InitializeSSN myInitializeSSN = new InitializeSSN();
                result = myInitializeSSN.DoReadLines(ref myLastSSN, ref myReadFileValues);
                int tempI;
                tempI = Convert.ToInt32(myLastSSN.myLastSSN) + 1;
                myHouseholdMembers.SSN = Convert.ToString(tempI);

                //The structure should be full now, so populate all the boxes.  
                textBoxHMFirstName.Text = myHouseholdMembers.FirstName;
                textBoxHMMiddleName.Text = myHouseholdMembers.MiddleName;
                textBoxHMLastName.Text = myHouseholdMembers.LastName;
                comboBoxHMSuffix.Text = myHouseholdMembers.Suffix;
                comboBoxHMGender.Text = myHouseholdMembers.Gender;
                comboBoxHMMaritalStatus.Text = myHouseholdMembers.MaritalStatus;
                textBoxHMDOB.Text = myHouseholdMembers.DOB;
                comboBoxHMLiveWithYou.Text = myHouseholdMembers.LiveWithYou;
                comboBoxHMLiveMN.Text = myHouseholdMembers.LiveInMN;
                comboBoxHMTempAbsentMN.Text = myHouseholdMembers.TempAbsentMN;
                comboBoxHMHomeless.Text = myHouseholdMembers.Homeless;
                /*textBoxHMAddress1.Text = myHouseholdMembers.Address1;//move to addr db
                textBoxHMAddress2.Text = myHouseholdMembers.Address2;
                textBoxHMAptSuite.Text = myHouseholdMembers.AptSuite;
                textBoxHMCity.Text = myHouseholdMembers.City;
                textBoxHMState.Text = myHouseholdMembers.State;
                textBoxHMZip.Text = myHouseholdMembers.Zip;
                 * */
                comboBoxHMPlanToLiveInMN.Text = myHouseholdMembers.PlanMakeMNHome;
                comboBoxHMSeekingEmployment.Text = myHouseholdMembers.SeekEmplMN;
                comboBoxHMPersonHighlighted.Text = myHouseholdMembers.PersonHighlighted;
                comboBoxHMHispanic.Text = myHouseholdMembers.Hispanic;
                textBoxTribeName.Text = myHouseholdMembers.TribeName;
                textBoxTribeId.Text = myHouseholdMembers.TribeId;
                comboBoxLiveRes.Text = myHouseholdMembers.LiveRes;
                comboBoxFederalTribe.Text = myHouseholdMembers.FederalTribe;
                comboBoxHMRace.Text = myHouseholdMembers.Race;
                comboBoxHMHaveSSN.Text = myHouseholdMembers.HaveSSN;
                textBoxHMSSN.Text = myHouseholdMembers.SSN;
                comboBoxHMPregnant.Text = myHouseholdMembers.IsPregnant;
                comboBoxHMBeenInFosterCare.Text = myHouseholdMembers.BeenInFosterCare;
                comboBoxHMUSCitizen.Text = myHouseholdMembers.USCitizen;
                comboBoxHMUSNational.Text = myHouseholdMembers.USNational;
                comboBoxHMRelationship.Text = myHouseholdMembers.Relationship;
                comboBoxHasIncome.Text = myHouseholdMembers.HasIncome;
                comboBoxRelToNextMem.Text = myHouseholdMembers.RelationshiptoNextHM;

                textBoxCurrentMember.Text = "2";
            }

            textBoxTotalMembers.Text = Convert.ToString(myHouseholdMembers.NumMembers + 1);
            if (myHouseholdMembers.NumMembers < 2)
            {
                buttonNextMember.Enabled = false;
                buttonPreviousMember.Enabled = false;
                textBoxTotalMembers.Text = "2";
            }
            if (Convert.ToInt32(textBoxTotalMembers.Text) == 2)
            {
                //   buttonSaveMember.BackColor = Color.Yellow;
                //    buttonAddMember.Enabled = false;
                //    buttonDeleteMember.Enabled = false;
            }
            else if (Convert.ToInt32(textBoxTotalMembers.Text) > 2)
            {
                buttonNextMember.Enabled = true;
                buttonPreviousMember.Enabled = true;
            }

            if (textBoxCurrentMember.Text == "2")
            {
                buttonPreviousMember.Enabled = false;
            }
            //textBoxTotalMembers = 
        }

        private void buttonSaveMember_Click(object sender, EventArgs e)
        {
            myHouseholdMembers.FirstName = textBoxHMFirstName.Text;
            myHouseholdMembers.MiddleName = textBoxHMMiddleName.Text;
            myHouseholdMembers.LastName = textBoxHMLastName.Text;
            myHouseholdMembers.Suffix = comboBoxHMSuffix.Text;
            myHouseholdMembers.Gender = comboBoxHMGender.Text;
            myHouseholdMembers.MaritalStatus = comboBoxHMMaritalStatus.Text;
            myHouseholdMembers.DOB = textBoxHMDOB.Text;
            myHouseholdMembers.LiveWithYou = comboBoxHMLiveWithYou.Text;
            myHouseholdMembers.LiveInMN = comboBoxHMLiveMN.Text;
            myHouseholdMembers.TempAbsentMN = comboBoxHMTempAbsentMN.Text;
            myHouseholdMembers.Homeless = comboBoxHMHomeless.Text;
            /*myHouseholdMembers.Address1 = textBoxHMAddress1.Text;//move to addr db
            myHouseholdMembers.Address2 = textBoxHMAddress2.Text;
            myHouseholdMembers.AptSuite = textBoxHMAptSuite.Text;
            myHouseholdMembers.City = textBoxHMCity.Text;
            myHouseholdMembers.State = textBoxHMState.Text;
            myHouseholdMembers.Zip = textBoxHMZip.Text;
              */
            myHouseholdMembers.PlanMakeMNHome = comboBoxHMPlanToLiveInMN.Text;
            myHouseholdMembers.SeekEmplMN = comboBoxHMSeekingEmployment.Text;
            myHouseholdMembers.PersonHighlighted = comboBoxHMPersonHighlighted.Text;
            myHouseholdMembers.Hispanic = comboBoxHMHispanic.Text;
            myHouseholdMembers.TribeName = textBoxTribeName.Text;
            myHouseholdMembers.TribeId = textBoxTribeId.Text;
            myHouseholdMembers.LiveRes = comboBoxLiveRes.Text;
            myHouseholdMembers.FederalTribe = comboBoxFederalTribe.Text;
            myHouseholdMembers.Race = comboBoxHMRace.Text;
            myHouseholdMembers.HaveSSN = comboBoxHMHaveSSN.Text;
            myHouseholdMembers.SSN = textBoxHMSSN.Text;
            myHouseholdMembers.USCitizen = comboBoxHMUSCitizen.Text;
            myHouseholdMembers.USNational = comboBoxHMUSNational.Text;
            myHouseholdMembers.IsPregnant = comboBoxHMPregnant.Text;
            myHouseholdMembers.IsPregnant = comboBoxHMBeenInFosterCare.Text;
            myHouseholdMembers.Relationship = comboBoxHMRelationship.Text;
            myHouseholdMembers.HasIncome = comboBoxHasIncome.Text;
            myHouseholdMembers.RelationshiptoNextHM = comboBoxRelToNextMem.Text;

            myHouseholdMembers.HouseMembersID = Convert.ToInt32(textBoxCurrentMember.Text);
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            string myInsertString;

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();

                //Delete row, then insert a new on based on the currently selected member.
                myHouseholdMembers.HouseMembersID = Convert.ToInt32(textBoxCurrentMember.Text);
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "Delete from HouseMembers where TestID = " + mySelectedTest.myTestId + " and HouseMembersID = " + myHouseholdMembers.HouseMembersID + ";";
                cmd2.ExecuteNonQuery();
                myInsertString = "Insert into HouseMembers values (" + myHouseholdMembers.HouseMembersID + ", " + mySelectedTest.myTestId +
                ", @FirstName, @MiddleName, @LastName, @Suffix, @Gender, @MaritalStatus, " +
                "@DOB , @LiveWithYou, @MNHome, @PersonHighlighted, @LiveMN,   @TempAbsentMN, @Homeless, @Address1, @Address2, @AptSuite, @City, @State, " +
                "@Zip, @PlanMakeMNHome, @SeekingEmployment, @Hispanic, @Race, @HaveSSN, @SSN, " +
                "@USCitizen, @USNational, @Pregnant, @FosterCare, @Relationship, @HasIncome, @RelationshiptoNextHM, @TribeName, @LiveRes, @TribeId, @FederalTribe);";

                using (SqlCeCommand com2 = new SqlCeCommand(myInsertString, con))
                {
                    com2.Parameters.AddWithValue("FirstName", myHouseholdMembers.FirstName);
                    com2.Parameters.AddWithValue("MiddleName", myHouseholdMembers.MiddleName);
                    com2.Parameters.AddWithValue("LastName", myHouseholdMembers.LastName);
                    com2.Parameters.AddWithValue("Suffix", myHouseholdMembers.Suffix);
                    com2.Parameters.AddWithValue("Gender", myHouseholdMembers.Gender);
                    com2.Parameters.AddWithValue("MaritalStatus", myHouseholdMembers.MaritalStatus);
                    com2.Parameters.AddWithValue("DOB", myHouseholdMembers.DOB);
                    com2.Parameters.AddWithValue("LiveWithYou", myHouseholdMembers.LiveWithYou);
                    com2.Parameters.AddWithValue("MNHome", myHouseholdMembers.MNHome);
                    com2.Parameters.AddWithValue("PersonHighlighted", myHouseholdMembers.PersonHighlighted);
                    com2.Parameters.AddWithValue("LiveMN", myHouseholdMembers.LiveInMN);
                    com2.Parameters.AddWithValue("TempAbsentMN", myHouseholdMembers.TempAbsentMN);
                    com2.Parameters.AddWithValue("Homeless", myHouseholdMembers.Homeless);
                    /*com2.Parameters.AddWithValue("Address1", myHouseholdMembers.Address1);//move to addr db
                    com2.Parameters.AddWithValue("Address2", myHouseholdMembers.Address2);
                    com2.Parameters.AddWithValue("AptSuite", myHouseholdMembers.AptSuite);
                    com2.Parameters.AddWithValue("City", myHouseholdMembers.City);
                    com2.Parameters.AddWithValue("State", myHouseholdMembers.State);
                    com2.Parameters.AddWithValue("Zip", myHouseholdMembers.Zip);
                     * */
                    com2.Parameters.AddWithValue("PlanMakeMNHome", myHouseholdMembers.PlanMakeMNHome);
                    com2.Parameters.AddWithValue("SeekingEmployment", myHouseholdMembers.SeekEmplMN);
                    com2.Parameters.AddWithValue("Hispanic", myHouseholdMembers.Hispanic);
                    com2.Parameters.AddWithValue("FederalTribe", myHouseholdMembers.FederalTribe);
                    com2.Parameters.AddWithValue("TribeName", myHouseholdMembers.TribeName);
                    com2.Parameters.AddWithValue("LiveRes", myHouseholdMembers.LiveRes);
                    com2.Parameters.AddWithValue("TribeId", myHouseholdMembers.TribeId);
                    com2.Parameters.AddWithValue("Race", myHouseholdMembers.Race);
                    com2.Parameters.AddWithValue("HaveSSN", myHouseholdMembers.HaveSSN);
                    com2.Parameters.AddWithValue("SSN", myHouseholdMembers.SSN);
                    com2.Parameters.AddWithValue("USCitizen", myHouseholdMembers.USCitizen);
                    com2.Parameters.AddWithValue("USNational", myHouseholdMembers.USNational);
                    com2.Parameters.AddWithValue("Pregnant", myHouseholdMembers.IsPregnant);
                    com2.Parameters.AddWithValue("FosterCare", myHouseholdMembers.BeenInFosterCare);
                    com2.Parameters.AddWithValue("Relationship", myHouseholdMembers.Relationship);
                    com2.Parameters.AddWithValue("HasIncome", myHouseholdMembers.HasIncome);
                    com2.Parameters.AddWithValue("RelationshiptoNextHM", myHouseholdMembers.RelationshiptoNextHM);

                    com2.ExecuteNonQuery();
                    com2.Dispose();
                }
            }
            catch (Exception g)
            {
                MessageBox.Show("Failed to Save HM: " + g);

            }
            myHouseholdMembers.NumMembers = Convert.ToInt32(textBoxTotalMembers.Text);
            buttonSaveMember.BackColor = Color.Transparent;
            buttonDeleteMember.Enabled = true;
            buttonAddMember.Enabled = true;
            if (textBoxCurrentMember.Text == "2")
            {
                buttonPreviousMember.Enabled = false;
            }
            if (textBoxCurrentMember.Text == textBoxTotalMembers.Text)
            {
                buttonNextMember.Enabled = false;
            }
            int result;
            myLastSSN.myLastSSN = myHouseholdMembers.SSN;
            InitializeSSN myInitializeSSN = new InitializeSSN();
            result = myInitializeSSN.DoWriteLines(ref myLastSSN, myReadFileValues);
        }

        private void buttonAddMember_Click(object sender, EventArgs e)
        {
            int result;
            AccountGeneration accountGeneration = new AccountGeneration();
            result = accountGeneration.GenerateHouseholdNames(mySelectedTest, ref myHouseholdMembers);
            myHouseholdMembers.HouseMembersID = myHouseholdMembers.NumMembers + 1;
            //The structure should be full now, so populate all the boxes.  
            textBoxHMFirstName.Text = myHouseholdMembers.FirstName;
            textBoxHMMiddleName.Text = myHouseholdMembers.MiddleName;
            textBoxHMLastName.Text = myHouseholdMembers.LastName;
            comboBoxHMSuffix.Text = myHouseholdMembers.Suffix;
            comboBoxHMGender.Text = myHouseholdMembers.Gender;
            comboBoxHMMaritalStatus.Text = myHouseholdMembers.MaritalStatus;
            textBoxHMDOB.Text = myHouseholdMembers.DOB;
            comboBoxHMLiveWithYou.Text = myHouseholdMembers.LiveWithYou;
            comboBoxHMLiveMN.Text = myHouseholdMembers.LiveInMN;
            comboBoxHMTempAbsentMN.Text = myHouseholdMembers.TempAbsentMN;
            comboBoxHMHomeless.Text = myHouseholdMembers.Homeless;
            /*textBoxHMAddress1.Text = myHouseholdMembers.Address1;//move to addr db
            textBoxHMAddress2.Text = myHouseholdMembers.Address2;
            textBoxHMAptSuite.Text = myHouseholdMembers.AptSuite;
            textBoxHMCity.Text = myHouseholdMembers.City;
            textBoxHMState.Text = myHouseholdMembers.State;
            textBoxHMZip.Text = myHouseholdMembers.Zip;
              */
            comboBoxHMPlanToLiveInMN.Text = myHouseholdMembers.PlanMakeMNHome;
            comboBoxHMSeekingEmployment.Text = myHouseholdMembers.SeekEmplMN;
            comboBoxHMPersonHighlighted.Text = myHouseholdMembers.PersonHighlighted;
            comboBoxHMHispanic.Text = myHouseholdMembers.Hispanic;
            textBoxTribeName.Text = myHouseholdMembers.TribeName;
            textBoxTribeId.Text = myHouseholdMembers.TribeId;
            comboBoxLiveRes.Text = myHouseholdMembers.LiveRes;
            comboBoxFederalTribe.Text = myHouseholdMembers.FederalTribe;
            comboBoxHMRace.Text = myHouseholdMembers.Race;
            comboBoxHMHaveSSN.Text = myHouseholdMembers.HaveSSN;
            textBoxHMSSN.Text = myHouseholdMembers.SSN;
            comboBoxHMUSCitizen.Text = myHouseholdMembers.USCitizen;
            comboBoxHMUSNational.Text = myHouseholdMembers.USNational;
            myHouseholdMembers.IsPregnant = comboBoxHMPregnant.Text;
            myHouseholdMembers.IsPregnant = comboBoxHMBeenInFosterCare.Text;
            myHouseholdMembers.Relationship = comboBoxHMRelationship.Text;
            myHouseholdMembers.HasIncome = comboBoxHasIncome.Text;
            myHouseholdMembers.RelationshiptoNextHM = comboBoxRelToNextMem.Text;

            textBoxCurrentMember.Text = Convert.ToString(Convert.ToInt32(textBoxCurrentMember.Text) + 1);
            textBoxTotalMembers.Text = textBoxCurrentMember.Text;
            buttonSaveMember.BackColor = Color.Yellow;
            if (textBoxCurrentMember.Text == textBoxTotalMembers.Text)
            {
                buttonNextMember.Enabled = false;
            }
            if (Convert.ToInt32(textBoxCurrentMember.Text) < 3)
            {
                buttonPreviousMember.Enabled = false;
            }
            else
            {
                buttonPreviousMember.Enabled = true;
            }
            buttonSaveMember.BackColor = Color.Yellow;

        }

        private void buttonNextMember_Click(object sender, EventArgs e)
        {

            int result;
            myHouseholdMembers.HouseMembersID = myHouseholdMembers.HouseMembersID + 1;
            HouseholdMembers householdMembers = new HouseholdMembers();
            result = householdMembers.DoGetHouseholdMember(ref myHouseholdMembers, ref  myHistoryInfo, mySelectedTest);

            //The structure should be full now, so populate all the boxes.  
            textBoxHMFirstName.Text = myHouseholdMembers.FirstName;
            textBoxHMMiddleName.Text = myHouseholdMembers.MiddleName;
            textBoxHMLastName.Text = myHouseholdMembers.LastName;
            comboBoxHMSuffix.Text = myHouseholdMembers.Suffix;
            comboBoxHMGender.Text = myHouseholdMembers.Gender;
            comboBoxHMMaritalStatus.Text = myHouseholdMembers.MaritalStatus;
            textBoxHMDOB.Text = myHouseholdMembers.DOB;
            comboBoxHMLiveWithYou.Text = myHouseholdMembers.LiveWithYou;
            comboBoxHMLiveMN.Text = myHouseholdMembers.LiveInMN;
            comboBoxHMTempAbsentMN.Text = myHouseholdMembers.TempAbsentMN;
            comboBoxHMHomeless.Text = myHouseholdMembers.Homeless;
            /*textBoxHMAddress1.Text = myHouseholdMembers.Address1;//move to addr db
            textBoxHMAddress2.Text = myHouseholdMembers.Address2;
            textBoxHMAptSuite.Text = myHouseholdMembers.AptSuite;
            textBoxHMCity.Text = myHouseholdMembers.City;
            textBoxHMState.Text = myHouseholdMembers.State;
            textBoxHMZip.Text = myHouseholdMembers.Zip;
             */
            comboBoxHMPlanToLiveInMN.Text = myHouseholdMembers.PlanMakeMNHome;
            comboBoxHMSeekingEmployment.Text = myHouseholdMembers.SeekEmplMN;
            comboBoxHMPersonHighlighted.Text = myHouseholdMembers.PersonHighlighted;
            comboBoxHMHispanic.Text = myHouseholdMembers.Hispanic;
            textBoxTribeName.Text = myHouseholdMembers.TribeName;
            textBoxTribeId.Text = myHouseholdMembers.TribeId;
            comboBoxLiveRes.Text = myHouseholdMembers.LiveRes;
            comboBoxFederalTribe.Text = myHouseholdMembers.FederalTribe;
            comboBoxHMRace.Text = myHouseholdMembers.Race;
            comboBoxHMHaveSSN.Text = myHouseholdMembers.HaveSSN;
            textBoxHMSSN.Text = myHouseholdMembers.SSN;
            comboBoxHMUSCitizen.Text = myHouseholdMembers.USCitizen;
            comboBoxHMUSNational.Text = myHouseholdMembers.USNational;
            myHouseholdMembers.IsPregnant = comboBoxHMPregnant.Text;
            myHouseholdMembers.IsPregnant = comboBoxHMBeenInFosterCare.Text;
            myHouseholdMembers.Relationship = comboBoxHMRelationship.Text;
            myHouseholdMembers.HasIncome = comboBoxHasIncome.Text;
            myHouseholdMembers.RelationshiptoNextHM = comboBoxRelToNextMem.Text;

            textBoxCurrentMember.Text = Convert.ToString(myHouseholdMembers.HouseMembersID);
            //  if (myHouseholdMembers.HouseMembersID == myHouseholdMembers.NumMembers)
            if (textBoxCurrentMember.Text == textBoxTotalMembers.Text)
            {
                buttonNextMember.Enabled = false;
                buttonDeleteMember.Enabled = true;
            }
            if (textBoxCurrentMember.Text == "2")
            {
                buttonPreviousMember.Enabled = false;
            }
            else
            {
                buttonPreviousMember.Enabled = true;
            }


        }

        private void buttonPreviousMember_Click(object sender, EventArgs e)
        {
            int result;
            myHouseholdMembers.HouseMembersID = myHouseholdMembers.HouseMembersID - 1;
            HouseholdMembers householdMembers = new HouseholdMembers();
            result = householdMembers.DoGetHouseholdMember(ref myHouseholdMembers, ref  myHistoryInfo, mySelectedTest);

            //The structure should be full now, so populate all the boxes.  
            textBoxHMFirstName.Text = myHouseholdMembers.FirstName;
            textBoxHMMiddleName.Text = myHouseholdMembers.MiddleName;
            textBoxHMLastName.Text = myHouseholdMembers.LastName;
            comboBoxHMSuffix.Text = myHouseholdMembers.Suffix;
            comboBoxHMGender.Text = myHouseholdMembers.Gender;
            comboBoxHMMaritalStatus.Text = myHouseholdMembers.MaritalStatus;
            textBoxHMDOB.Text = myHouseholdMembers.DOB;
            comboBoxHMLiveWithYou.Text = myHouseholdMembers.LiveWithYou;
            comboBoxHMLiveMN.Text = myHouseholdMembers.LiveInMN;
            comboBoxHMTempAbsentMN.Text = myHouseholdMembers.TempAbsentMN;
            comboBoxHMHomeless.Text = myHouseholdMembers.Homeless;
            /*textBoxHMAddress1.Text = myHouseholdMembers.Address1;//move to addr db
            textBoxHMAddress2.Text = myHouseholdMembers.Address2;
            textBoxHMAptSuite.Text = myHouseholdMembers.AptSuite;
            textBoxHMCity.Text = myHouseholdMembers.City;
            textBoxHMState.Text = myHouseholdMembers.State;
            textBoxHMZip.Text = myHouseholdMembers.Zip;
             * */
            comboBoxHMPlanToLiveInMN.Text = myHouseholdMembers.PlanMakeMNHome;
            comboBoxHMSeekingEmployment.Text = myHouseholdMembers.SeekEmplMN;
            comboBoxHMPersonHighlighted.Text = myHouseholdMembers.PersonHighlighted;
            comboBoxHMHispanic.Text = myHouseholdMembers.Hispanic;
            textBoxTribeName.Text = myHouseholdMembers.TribeName;
            textBoxTribeId.Text = myHouseholdMembers.TribeId;
            comboBoxLiveRes.Text = myHouseholdMembers.LiveRes;
            comboBoxFederalTribe.Text = myHouseholdMembers.FederalTribe;
            comboBoxHMRace.Text = myHouseholdMembers.Race;
            comboBoxHMHaveSSN.Text = myHouseholdMembers.HaveSSN;
            textBoxHMSSN.Text = myHouseholdMembers.SSN;
            comboBoxHMUSCitizen.Text = myHouseholdMembers.USCitizen;
            comboBoxHMUSNational.Text = myHouseholdMembers.USNational;
            myHouseholdMembers.IsPregnant = comboBoxHMPregnant.Text;
            myHouseholdMembers.IsPregnant = comboBoxHMBeenInFosterCare.Text;
            myHouseholdMembers.Relationship = comboBoxHMRelationship.Text;
            myHouseholdMembers.HasIncome = comboBoxHasIncome.Text;
            myHouseholdMembers.RelationshiptoNextHM = comboBoxRelToNextMem.Text;

            textBoxCurrentMember.Text = Convert.ToString(myHouseholdMembers.HouseMembersID);
            if (textBoxCurrentMember.Text == textBoxTotalMembers.Text)
            {
                buttonNextMember.Enabled = false;
                buttonPreviousMember.Enabled = true;
            }
            else
            {
                buttonPreviousMember.Enabled = true;
                buttonNextMember.Enabled = true;
            }
            if (textBoxCurrentMember.Text == "2")
            {
                buttonPreviousMember.Enabled = false;
            }
            else
            {
                buttonNextMember.Enabled = true;
            }

        }

        private void buttonDeleteMember_Click(object sender, EventArgs e)
        {
            myHouseholdMembers.HouseMembersID = Convert.ToInt32(textBoxCurrentMember.Text);
            if (textBoxCurrentMember.Text == textBoxTotalMembers.Text)
            {
                SqlCeConnection con;
                string conString = Properties.Settings.Default.Database1ConnectionString;

                try
                {
                    // Open the connection using the connection string.
                    con = new SqlCeConnection(conString);
                    con.Open();

                    //Delete row, then insert a new on based on the currently selected member.
                    myHouseholdMembers.HouseMembersID = Convert.ToInt32(textBoxCurrentMember.Text);
                    SqlCeCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "Delete from HouseMembers where TestID = " + mySelectedTest.myTestId + " and HouseMembersID = " + myHouseholdMembers.HouseMembersID + ";";
                    cmd2.ExecuteNonQuery();
                }
                catch
                {

                }
                myHouseholdMembers.HouseMembersID = myHouseholdMembers.HouseMembersID - 1;
                textBoxCurrentMember.Text = Convert.ToString(myHouseholdMembers.HouseMembersID);
                textBoxTotalMembers.Text = Convert.ToString(myHouseholdMembers.HouseMembersID);
                if (textBoxCurrentMember.Text == "1")
                {
                    buttonPreviousMember.Enabled = false;
                    buttonNextMember.Enabled = false;
                    buttonDeleteMember.Enabled = false;
                    buttonSaveMember.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please delete the last member first.");
            }
        }


        private void tabPageWindows_Enter(object sender, EventArgs e)
        {
            myEditKey.myWindowsFirstTime = "Yes";
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Windows;";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewWindowsPick.DataSource = dt;
            myEditKey.myWindowsEditKey = "1";
            myEditKey.myWindowsFirstTime = "No";

            dataGridViewWindowsPick.AutoGenerateColumns = true;
            DataGridViewColumn Id_Column = dataGridViewWindowsPick.Columns[0];
            Id_Column.Width = 60;
            DataGridViewColumn Funct_Column = dataGridViewWindowsPick.Columns[1];
            Funct_Column.Width = 125;
            DataGridViewColumn Name_Column = dataGridViewWindowsPick.Columns[2];
            Name_Column.Width = 175;
            DataGridViewColumn Screen_Column = dataGridViewWindowsPick.Columns[3];
            Screen_Column.Width = 175;

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewWindowsPick.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            con.Close();
        }

        private void dataGridViewWindowsPick_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex;

            if (dataGridViewWindowsPick.CurrentCell == null)
            {
                rowindex = 0;
            }
            else
            {
                rowindex = dataGridViewWindowsPick.CurrentCell.RowIndex;
            }

            String mysWindowId;
            if (dataGridViewWindowsPick.Rows[rowindex].Cells[0].Value == null)
            {
                mysWindowId = "1";
            }
            else
            {
                mysWindowId = dataGridViewWindowsPick.Rows[rowindex].Cells[0].Value.ToString();
            }

            int myiWindowId;
            myiWindowId = Convert.ToInt32(mysWindowId);

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            if (myEditKey.myWindowsFirstTime == "Yes" || myEditKey.myWindowsDeletedRow == "Yes")
            {
                myEditKey.myWindowsEditKey = "1";
            }
            else
            {
                myEditKey.myWindowsEditKey = mysWindowId;
            }

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * from Windows where WindowId =  " + myEditKey.myWindowsEditKey, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        textBoxWindowId.Text = Convert.ToString(reader.GetInt32(0));
                        textBoxWindowFunctionalArea.Text = reader.GetString(1);
                        textBoxWindowName.Text = reader.GetString(2);
                        textBoxWindowScreenId.Text = reader.GetString(3);
                        textBoxWindowAction.Text = reader.GetString(4);
                        textBoxWindowModScreenId.Text = reader.GetString(5);
                        textBoxWindowFunctionalYet.Text = reader.GetString(6);
                        textBoxWindowNotes.Text = reader.GetString(7);
                    }
                    else
                    {
                        MessageBox.Show("Did not find window id");
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Add Window ID didn't work");
            }
        }


        private void buttonWindowSave_Click(object sender, EventArgs e)
        {
            string myWindowId;
            string myFunctionalArea;
            string myName;
            string myScreenId;
            string myAction;
            string myModifiedScreenId;
            string myFunctionalYet;
            string myNotes;

            myWindowId = Convert.ToString(textBoxWindowId.Text);
            myFunctionalArea = Convert.ToString(textBoxWindowFunctionalArea.Text);
            myName = Convert.ToString(textBoxWindowName.Text);
            myScreenId = Convert.ToString(textBoxWindowScreenId.Text);
            myAction = Convert.ToString(textBoxWindowAction.Text);
            myModifiedScreenId = Convert.ToString(textBoxWindowModScreenId.Text);
            myFunctionalYet = Convert.ToString(textBoxWindowFunctionalYet.Text);
            myNotes = Convert.ToString(textBoxWindowNotes.Text);

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                int rowindex = dataGridViewWindowsPick.CurrentCell.RowIndex;

                String mysWindowId;
                mysWindowId = dataGridViewWindowsPick.Rows[rowindex].Cells[0].Value.ToString();
                int myiWindowId;
                myiWindowId = Convert.ToInt32(mysWindowId);

                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Windows where WindowId = " + myEditKey.myWindowsEditKey, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Windows set FunctionalArea = @FunctionalArea"
                           + " , Name = @Name, ScreenId = @ScreenId " +
                            ", Action = @Action" +
                            ", ModifiedScreenId = @ModifiedScreenId" +
                            ", FunctionalYet = @FunctionalYet" +
                            ", Notes = @Notes" +
                            " where WindowId = " + myEditKey.myWindowsEditKey + ";";
                        using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                        {
                            com3.Parameters.AddWithValue("FunctionalArea", myFunctionalArea);
                            com3.Parameters.AddWithValue("Name", myName);
                            com3.Parameters.AddWithValue("ScreenId", myScreenId);
                            com3.Parameters.AddWithValue("Action", myAction);
                            com3.Parameters.AddWithValue("ModifiedScreenId", myModifiedScreenId);
                            com3.Parameters.AddWithValue("FunctionalYet", myFunctionalYet);
                            com3.Parameters.AddWithValue("Notes", myNotes);
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                    else
                    {
                        string myInsertString;
                        DateTime now = DateTime.Now;
                        myInsertString = "Insert into Windows Values (" + myEditKey.myWindowsEditKey +
                            ",  @FunctionalArea,  @Name,  @ScreenId,  @Action" +
                            ",  @ModifiedScreenId,  @FunctionalYet,  @Notes  );";
                        using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                        {
                            com3.Parameters.AddWithValue("FunctionalArea", myFunctionalArea);
                            com3.Parameters.AddWithValue("Name", myName);
                            com3.Parameters.AddWithValue("ScreenId", myScreenId);
                            com3.Parameters.AddWithValue("Action", myAction);
                            com3.Parameters.AddWithValue("ModifiedScreenId", myModifiedScreenId);
                            com3.Parameters.AddWithValue("FunctionalYet", myFunctionalYet);
                            com3.Parameters.AddWithValue("Notes", myNotes);
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                }

                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Windows;";
                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewWindowsPick.DataSource = dt;
                myEditKey.myWindowsEditKey = mysWindowId;
                myEditKey.myWindowsFirstTime = "No";

            }
            catch
            {
                MessageBox.Show("Add New Window didn't work");

            }
            myEditKey.myWindowsFirstTime = "No";
        }

        private void buttonWindowAdd_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewWindowsPick.CurrentCell.RowIndex;
            myEditKey.myWindowsFirstTime = "No";
            String mysWindowId;
            mysWindowId = dataGridViewWindowsPick.Rows[rowindex].Cells[0].Value.ToString();
            int myiWindowId;
            myiWindowId = Convert.ToInt32(mysWindowId);
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT max(WindowId) FROM Windows", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myEditKey.myWindowsEditKey = Convert.ToString(reader.GetInt32(0) + 1);
                    }
                    else
                    {
                        MessageBox.Show("Did not find Windows id");
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Add Windows ID didn't work");
            }
            textBoxWindowId.Text = myEditKey.myWindowsEditKey;
            textBoxWindowFunctionalArea.Text = "";
            textBoxWindowName.Text = "";
            textBoxWindowScreenId.Text = "";
            textBoxWindowAction.Text = "";
            textBoxWindowModScreenId.Text = "";
            textBoxWindowFunctionalYet.Text = "";
            textBoxWindowNotes.Text = "";
        }

        private void buttonWindowDelete_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewWindowsPick.CurrentCell.RowIndex;
            myEditKey.myWindowsFirstTime = "No";
            String mysWindowId;
            mysWindowId = dataGridViewWindowsPick.Rows[rowindex].Cells[0].Value.ToString();
            myEditKey.myWindowsEditKey = dataGridViewWindowsPick.Rows[rowindex].Cells[0].Value.ToString();
            int myiWindowId;
            myiWindowId = Convert.ToInt32(mysWindowId);
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Methods where WindowId = " + myEditKey.myWindowsEditKey, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("This Window is part of a Method and cannot be deleted.");
                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("Are you sure you want to delete this Window?", "Delete Window", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {
                            string myDeleteString;
                            myDeleteString = "Delete FROM Windows where WindowId = " + myEditKey.myWindowsEditKey;
                            using (SqlCeCommand com3 = new SqlCeCommand(myDeleteString, con))
                            {
                                com3.ExecuteNonQuery();
                                com3.Dispose();
                                myEditKey.myWindowsFirstTime = "No";
                                myEditKey.myWindowsDeletedRow = "Yes";
                            }
                        }
                    }
                }
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Windows;";

                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewWindowsPick.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Delete Window didn't work");

            }
            //set deleted row back to No when completed
            myEditKey.myWindowsDeletedRow = "No";
        }


        private void dataGridViewMethodsPick_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex;

            if (dataGridViewMethodsPick.CurrentCell == null)
            {
                rowindex = 0;
            }
            else
            {
                rowindex = dataGridViewMethodsPick.CurrentCell.RowIndex;
            }

            String mysMethodId;
            if (dataGridViewMethodsPick.Rows[rowindex].Cells[0].Value == null)
            {
                mysMethodId = "1";
            }
            else
            {
                mysMethodId = dataGridViewMethodsPick.Rows[rowindex].Cells[0].Value.ToString();
            }

            int myiMethodId;
            myiMethodId = Convert.ToInt32(mysMethodId);

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            if (myEditKey.myMethodFirstTime == "Yes" || myEditKey.myMethodDeletedRow == "Yes")
            {
                myEditKey.myMethodEditKey = "1";
            }
            else
            {
                myEditKey.myMethodEditKey = mysMethodId;
            }

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * from Methods where MethodId =  " + myEditKey.myMethodEditKey, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        textBoxMethodMethodId.Text = Convert.ToString(reader.GetInt32(0));
                        textBoxMethodWindowId.Text = Convert.ToString(reader.GetInt32(1));
                        textBoxMethodName.Text = reader.GetString(2);
                        textBoxMethodClassName.Text = reader.GetString(3);
                        textBoxMethodSpecialAction.Text = reader.GetString(4);
                        textBoxMethodFunctionalYet.Text = reader.GetString(5);
                    }
                    else
                    {
                        MessageBox.Show("Did not find Method id");
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Add Method ID didn't work");
            }
        }


        private void tabPageMethods_Enter(object sender, EventArgs e)
        {
            myEditKey.myMethodFirstTime = "Yes";
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText =
            "Select m.MethodId, m.WindowId, w.Name , m.Name, m.ClassName, m.SpecialAction, m.FunctionalYet  from Methods m, Windows w where m.WindowId = w.WindowId order by  MethodId;";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewMethodsPick.DataSource = dt;
            dt.Columns["Name"].ColumnName = "Window";
            dt.Columns["Name1"].ColumnName = "Method";
            dt.Columns["ClassName"].ColumnName = "Class";
            dataGridViewMethodsPick.Columns["Class"].DisplayIndex = 3;
            myEditKey.myMethodEditKey = "1";
            myEditKey.myMethodFirstTime = "No";

            dataGridViewMethodsPick.AutoGenerateColumns = true;
            DataGridViewColumn MethodId_Column = dataGridViewMethodsPick.Columns[0];
            MethodId_Column.Width = 60;
            DataGridViewColumn WindowId_Column = dataGridViewMethodsPick.Columns[1];
            WindowId_Column.Width = 60;
            DataGridViewColumn Window_Column3 = dataGridViewMethodsPick.Columns[2];
            Window_Column3.Width = 175;
            DataGridViewColumn Class_Column3 = dataGridViewMethodsPick.Columns[3];
            Class_Column3.Width = 225;
            DataGridViewColumn Method_Column3 = dataGridViewMethodsPick.Columns[4];
            Method_Column3.Width = 175;

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewMethodsPick.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            con.Close();
        }

        private void buttonMethodAdd_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewMethodsPick.CurrentCell.RowIndex;
            myEditKey.myMethodFirstTime = "No";
            String mysMethodId;
            mysMethodId = dataGridViewMethodsPick.Rows[rowindex].Cells[0].Value.ToString();
            int myiWindowId;
            myiWindowId = Convert.ToInt32(mysMethodId);
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT max(MethodId) FROM Methods", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myEditKey.myMethodEditKey = Convert.ToString(reader.GetInt32(0) + 1);
                    }
                    else
                    {
                        MessageBox.Show("Did not find Method id");
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Add Method ID didn't work");
            }
            textBoxMethodMethodId.Text = myEditKey.myMethodEditKey;
            textBoxMethodWindowId.Text = "";
            textBoxMethodName.Text = "";
            textBoxMethodClassName.Text = "";
            textBoxMethodSpecialAction.Text = "";
            textBoxMethodFunctionalYet.Text = "";

        }

        private void buttonMethodSave_Click(object sender, EventArgs e)
        {

            string myMethodId;
            string myWindowId;
            string myClassName;
            string myName;
            string mySpecialAction;
            string myFunctionalYet;

            myMethodId = Convert.ToString(textBoxMethodMethodId.Text);
            myWindowId = Convert.ToString(textBoxMethodWindowId.Text);
            myName = Convert.ToString(textBoxMethodName.Text);
            mySpecialAction = Convert.ToString(textBoxMethodSpecialAction.Text);
            myClassName = Convert.ToString(textBoxMethodClassName.Text);
            myFunctionalYet = Convert.ToString(textBoxMethodFunctionalYet.Text);

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                int rowindex = dataGridViewMethodsPick.CurrentCell.RowIndex;
                myEditKey.myMethodEditKey = dataGridViewMethodsPick.Rows[rowindex].Cells[0].Value.ToString();

                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Methods where MethodId = " + myMethodId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {

                        string myUpdateString;
                        myUpdateString = "Update Methods set  MethodId = @MethodId, WindowId = @WindowId, Name = @Name, ClassName = @ClassName " +
                            ", SpecialAction = @Action" +
                            ", FunctionalYet = @FunctionalYet" +
                            " where MethodId = " + myEditKey.myMethodEditKey + ";";
                        using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                        {
                            com3.Parameters.AddWithValue("MethodId", myMethodId);
                            com3.Parameters.AddWithValue("WindowId", myWindowId);
                            com3.Parameters.AddWithValue("ClassName", myClassName);
                            com3.Parameters.AddWithValue("Name", myName);
                            com3.Parameters.AddWithValue("Action", mySpecialAction);
                            com3.Parameters.AddWithValue("FunctionalYet", myFunctionalYet);
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                    else
                    {
                        string myInsertString;
                        DateTime now = DateTime.Now;
                        myInsertString = "Insert into Methods Values (" + myMethodId + ", " + myWindowId +
                            ",   @Name, @ClassName,   @Action" +
                            ",    @FunctionalYet );";
                        using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                        {
                            com3.Parameters.AddWithValue("ClassName", myClassName);
                            com3.Parameters.AddWithValue("Name", myName);
                            //    com3.Parameters.AddWithValue("ScreenId", myScreenId);
                            com3.Parameters.AddWithValue("Action", mySpecialAction);
                            com3.Parameters.AddWithValue("FunctionalYet", myFunctionalYet);
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                }
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select m.MethodId, m.WindowId, w.Name , m.Name, m.ClassName, m.SpecialAction, m.FunctionalYet  from Methods m, Windows w where m.WindowId = w.WindowId order by MethodId;";

                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewMethodsPick.DataSource = dt;
                myEditKey.myMethodEditKey = "1";
                myEditKey.myMethodFirstTime = "No";

                dt.Columns["Name"].ColumnName = "Window";
                dt.Columns["Name1"].ColumnName = "Method";
                dt.Columns["ClassName"].ColumnName = "Class";
                dataGridViewMethodsPick.Columns["Class"].DisplayIndex = 3;

                dataGridViewMethodsPick.AutoGenerateColumns = true;
                DataGridViewColumn MethodId_Column = dataGridViewMethodsPick.Columns[0];
                MethodId_Column.Width = 60;
                DataGridViewColumn WindowId_Column = dataGridViewMethodsPick.Columns[1];
                WindowId_Column.Width = 60;
                DataGridViewColumn Window_Column3 = dataGridViewMethodsPick.Columns[2];
                Window_Column3.Width = 175;
                DataGridViewColumn Class_Column3 = dataGridViewMethodsPick.Columns[3];
                Class_Column3.Width = 125;
                DataGridViewColumn Method_Column3 = dataGridViewMethodsPick.Columns[4];
                Method_Column3.Width = 175;

            }
            catch (Exception h)
            {
                MessageBox.Show("Add New Window didn't work, Exception: " + h);

            }
            myEditKey.myWindowsFirstTime = "No";

        }


        private void buttonMethodDelete_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewMethodsPick.CurrentCell.RowIndex;
            myEditKey.myMethodFirstTime = "No";
            String mysMethodId;
            mysMethodId = dataGridViewMethodsPick.Rows[rowindex].Cells[0].Value.ToString();
            int myiMethodId;
            myiMethodId = Convert.ToInt32(mysMethodId);
            string myMethodName;
            myMethodName = dataGridViewMethodsPick.Rows[rowindex].Cells[3].Value.ToString();
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM TestSteps where Method = '" + myMethodName + "';", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("This Method is part of an Test and cannot be deleted.");
                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("Are you sure you want to delete this Method?", "Delete Method", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                        {
                            string myDeleteString;
                            myDeleteString = "Delete  FROM Methods where MethodId = " + mysMethodId;
                            using (SqlCeCommand com3 = new SqlCeCommand(myDeleteString, con))
                            {
                                com3.ExecuteNonQuery();
                                com3.Dispose();
                                myEditKey.myMethodFirstTime = "No";
                                myEditKey.myMethodDeletedRow = "Yes";
                            }
                        }
                    }
                }
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Methods;";

                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewMethodsPick.DataSource = dt;
            }
            catch (Exception w)
            {
                MessageBox.Show("Delete Method didn't work: Exception" + w);

            }
            //set deleted row back to No when completed
            myEditKey.myMethodDeletedRow = "No";
        }

        private void tabPageTest_Enter(object sender, EventArgs e)
        {
            myEditKey.myTestFirstTime = "Yes";
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Test;";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewTestsPick.DataSource = dt;
            myEditKey.myTestEditKey = "1";
            myEditKey.myTestFirstTime = "No";

            dataGridViewTestsPick.AutoGenerateColumns = true;
            DataGridViewColumn TestId_Column = dataGridViewTestsPick.Columns[0];
            TestId_Column.Width = 60;
            DataGridViewColumn TestName_Column = dataGridViewTestsPick.Columns[1];
            TestName_Column.Width = 125;
            DataGridViewColumn Desc_Column = dataGridViewTestsPick.Columns[3];
            Desc_Column.Width = 175;
            DataGridViewColumn Notes_Column = dataGridViewTestsPick.Columns[4];
            Notes_Column.Width = 175;

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewTestsPick.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            cmd.CommandType = CommandType.Text;
            cmd.CommandText =
                "Select  w.Name , m.ClassName, m.Name, m.SpecialAction, m.FunctionalYet, m.MethodId, m.WindowId from Methods m, Windows w where m.WindowId = w.WindowId;";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            SqlCeDataAdapter da1 = new SqlCeDataAdapter(cmd);
            da1.Fill(dt1);
            dataGridViewAvailableMethods.DataSource = dt1;
            dt1.Columns["Name"].ColumnName = "Window";
            dt1.Columns["Name1"].ColumnName = "Method";
            dt1.Columns["ClassName"].ColumnName = "Class";

            dataGridViewAvailableMethods.AutoGenerateColumns = true;
            DataGridViewColumn Window_Column = dataGridViewAvailableMethods.Columns[0];
            Window_Column.Width = 125;
            DataGridViewColumn Method_Column = dataGridViewAvailableMethods.Columns[2];
            Method_Column.Width = 225;

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewAvailableMethods.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void dataGridViewTestsPick_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex;

            if (dataGridViewTestsPick.CurrentCell == null)
            {
                rowindex = 0;
            }
            else
            {
                rowindex = dataGridViewTestsPick.CurrentCell.RowIndex;
            }

            String mysTestId;
            if (dataGridViewTestsPick.Rows[rowindex].Cells[0].Value == null)
            {
                mysTestId = "1";
            }
            else
            {
                mysTestId = dataGridViewTestsPick.Rows[rowindex].Cells[0].Value.ToString();
            }

            int myiTestId;
            myiTestId = Convert.ToInt32(mysTestId);

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            if (myEditKey.myTestFirstTime == "Yes" || myEditKey.myTestDeletedRow == "Yes")
            {
                myEditKey.myTestEditKey = "1";
            }
            else
            {
                myEditKey.myTestEditKey = mysTestId;
            }

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * from Test where TestId =  " + myEditKey.myTestEditKey, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        textBoxTestTestId.Text = Convert.ToString(reader.GetInt32(0));
                        textBoxTestName.Text = reader.GetString(1);
                        textBoxTestType.Text = reader.GetString(2);
                        textBoxTestDescription.Text = reader.GetString(3);
                        textBoxTestNotes.Text = reader.GetString(4);
                        textBoxTestURL.Text = reader.GetString(5);
                        textBoxTestIsSelected.Text = reader.GetString(6);
                    }
                    else
                    {
                        MessageBox.Show("Did not find Test id");
                    }
                    con.Close();
                }
            }
            catch (Exception o)
            {
                MessageBox.Show("Add Test ID didn't work, Exception: " + o);
            }
        }


        private void buttonTestAdd_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewTestsPick.CurrentCell.RowIndex;
            myEditKey.myTestFirstTime = "No";
            String mysTestId;
            mysTestId = dataGridViewTestsPick.Rows[rowindex].Cells[0].Value.ToString();
            int myiWindowId;
            myiWindowId = Convert.ToInt32(mysTestId);
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT max(TestId) FROM Test", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myEditKey.myTestEditKey = Convert.ToString(reader.GetInt32(0) + 1);
                    }
                    else
                    {
                        MessageBox.Show("Did not find Test id");
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Add Test ID didn't work");
            }
            textBoxTestTestId.Text = myEditKey.myTestEditKey;
            textBoxTestName.Text = "";
            textBoxTestName.Text = "";
            textBoxTestType.Text = "";
            textBoxTestDescription.Text = "";
            textBoxTestURL.Text = "";
            textBoxTestIsSelected.Text = "No";
            textBoxTestNotes.Text = "";
        }

        private void buttonTestSave_Click(object sender, EventArgs e)
        {

            string myTestId;
            string myName;
            string myTestType;
            string myDescription;
            string myURL;
            string myIsSelected;
            string myNotes;

            myTestId = Convert.ToString(textBoxTestTestId.Text);
            myName = Convert.ToString(textBoxTestName.Text);
            myTestType = Convert.ToString(textBoxTestType.Text);
            myDescription = Convert.ToString(textBoxTestDescription.Text);
            myURL = Convert.ToString(textBoxTestURL.Text);
            myIsSelected = Convert.ToString(textBoxTestIsSelected.Text);
            myNotes = Convert.ToString(textBoxTestNotes.Text);


            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                int rowindex = dataGridViewTestsPick.CurrentCell.RowIndex;
                myEditKey.myTestEditKey = dataGridViewTestsPick.Rows[rowindex].Cells[0].Value.ToString();

                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Test where TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Test set  Name = @Name, Type = @TestType " +
                            ", Description = @Description" +
                            ", URL = @URL, IsSelected = @IsSelected, Notes = @Notes" +
                            " where TestId = " + myEditKey.myTestEditKey + ";";
                        using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                        {
                            com3.Parameters.AddWithValue("Name", myName);
                            com3.Parameters.AddWithValue("TestType", myTestType);
                            com3.Parameters.AddWithValue("Description", myDescription);
                            com3.Parameters.AddWithValue("URL", myURL);
                            com3.Parameters.AddWithValue("IsSelected", myIsSelected);
                            com3.Parameters.AddWithValue("Notes", myNotes);
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                    else
                    {
                        string myInsertString;
                        DateTime now = DateTime.Now;
                        myInsertString = "Insert into Test Values (" + myTestId +
                            ",   @Name, @Type, @Description, @Notes, @URL" +
                            ",   @IsSelected   );";
                        using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                        {
                            com3.Parameters.AddWithValue("TestId", myTestId);
                            com3.Parameters.AddWithValue("Name", myName);
                            com3.Parameters.AddWithValue("Type", myTestType);
                            com3.Parameters.AddWithValue("Description", myDescription);
                            com3.Parameters.AddWithValue("URL", myURL);
                            com3.Parameters.AddWithValue("IsSelected", myIsSelected);
                            com3.Parameters.AddWithValue("Notes", myNotes);
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                }

                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Test;";

                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewTestsPick.DataSource = dt;
                myEditKey.myTestEditKey = "1";
                myEditKey.myTestFirstTime = "Yes";

            }
            catch (Exception u)
            {
                MessageBox.Show("Add New Test didn't work, Exception: " + u);

            }
            myEditKey.myTestFirstTime = "No";

        }

        private void textBoxTestTestId_TextChanged(object sender, EventArgs e)
        {
            string testId;
            testId = textBoxTestTestId.Text;

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Window, Class, Method, TestId, TestStepId, WindowId, StepURL, StepNotes from TestSteps where TestId = " + testId + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewTestSteps.DataSource = dt;

            dataGridViewTestSteps.AutoGenerateColumns = true;
            DataGridViewColumn Window_Column2 = dataGridViewTestSteps.Columns[0];
            Window_Column2.Width = 125;
            DataGridViewColumn Method_Column2 = dataGridViewTestSteps.Columns[2];
            Method_Column2.Width = 175;

            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dataGridViewTestSteps.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

        }

        private void buttonAddTestStep_Click(object sender, EventArgs e)
        {
            int rowindex;// 
            string mysTestId;
            string myWindow;
            string myWindowId;
            string myClass;
            string myName;
            int rowindexMethod;
            string mysMethodId;

            rowindex = dataGridViewTestsPick.CurrentCell.RowIndex;
            mysTestId = dataGridViewTestsPick.Rows[rowindex].Cells[0].Value.ToString();

            rowindexMethod = dataGridViewAvailableMethods.CurrentCell.RowIndex;
            myWindow = dataGridViewAvailableMethods.Rows[rowindexMethod].Cells[0].Value.ToString();
            myClass = dataGridViewAvailableMethods.Rows[rowindexMethod].Cells[1].Value.ToString();
            myName = dataGridViewAvailableMethods.Rows[rowindexMethod].Cells[2].Value.ToString();
            mysMethodId = dataGridViewAvailableMethods.Rows[rowindexMethod].Cells[5].Value.ToString();
            myWindowId = dataGridViewAvailableMethods.Rows[rowindexMethod].Cells[6].Value.ToString();

            int myTestId;
            int myiTestStepId;
            myTestId = Convert.ToInt32(mysTestId);
            // mysMethodId

            SqlCeConnection con;
            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.Database1ConnectionString;

            int countSelectedTestSteps;
            countSelectedTestSteps = dataGridViewTestSteps.Rows.Count;
            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                using (SqlCeCommand com2 = new SqlCeCommand("Select max(TestStepId) from TestSteps where TestId = " + mysTestId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read() && countSelectedTestSteps != 1)
                    {
                        myiTestStepId = reader.GetInt32(0);
                        myiTestStepId = myiTestStepId + 1;
                    }
                    else
                    {
                        myiTestStepId = 1;
                    }
                }
                using (SqlCeCommand com2 = new SqlCeCommand("Select Name from Windows where WindowId = " + myWindowId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myWindow = reader.GetString(0);

                    }
                }

                myInsertString = "insert into TestSteps values(" + myTestId.ToString() + ", " + myiTestStepId +
                    ", '" + myWindow + "', '" + myWindowId + "', '" + myClass + "', '" + myName + "', '', ''); ";

                using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                {
                    com3.ExecuteNonQuery();
                    com3.Dispose();
                }
                con.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Write New Suite Test didn't work, Exception: " + a);
            }

            string testId;
            testId = textBoxTestTestId.Text;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Window, Class, Method, TestId, TestStepId, WindowId, StepURL, StepNotes from TestSteps where TestId = " + testId + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewTestSteps.DataSource = dt;
        }


        private void buttonConfigureApplication_Click(object sender, EventArgs e)
        {
            myNavHelper.myConfigureClicked = "Yes";
            tabControlMain.SelectedIndex = 1;
        }

        private void buttonRemoveTestStep_Click(object sender, EventArgs e)
        {
            int rowindexTest;
            string mysTestId;

            rowindexTest = dataGridViewTestSteps.CurrentCell.RowIndex;
            mysTestId = dataGridViewTestSteps.Rows[rowindexTest].Cells[3].Value.ToString();
            int rowindex;
            rowindex = dataGridViewTestSteps.CurrentCell.RowIndex;
            string mysTestStepId;
            mysTestStepId = dataGridViewTestSteps.Rows[rowindex].Cells[4].Value.ToString();

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                string myDeleteString;
                myDeleteString = "Delete from TestSteps where TestId = " + mysTestId + " and TestStepId = " + mysTestStepId;

                using (SqlCeCommand com3 = new SqlCeCommand(myDeleteString, con))
                {
                    com3.ExecuteNonQuery();
                    com3.Dispose();
                }

                con.Close();
                string testId;
                testId = textBoxTestTestId.Text;
                con = new SqlCeConnection(conString);
                con.Open();
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select Window, Class, Method, TestId, TestStepId, WindowId, StepURL, StepNotes from TestSteps where TestId = " + testId + ";";
                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewTestSteps.DataSource = dt;
            }

            catch (Exception y)
            {
                MessageBox.Show("Delete Test step didn't work, ecxeption: " + y);
            }


        }

        private void buttonConfigureEnrollment_Click(object sender, EventArgs e)
        {
            myNavHelper.myConfigureClicked = "Yes";
            tabControlMain.SelectedIndex = 3;
        }

        private void textBoxEnrollAmount_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBoxEnrollAmount.Text) < 17000)
            {
                radioButtonApplicationTypeMA.Checked = true;
                myApplication.myEnrollmentPlanType = "MN Care MA";

            }
            else if (Convert.ToInt32(textBoxEnrollAmount.Text) < 24000)
            {
                radioButtonApplicationTypeBHP.Checked = true;
                myApplication.myEnrollmentPlanType = "MN Care BHP";
            }
            else if (Convert.ToInt32(textBoxEnrollAmount.Text) < 50000)
            {
                radioButtonApplicationTypeQHP.Checked = true;
                myApplication.myEnrollmentPlanType = "MN Care QHP";
            }
            else
            {
                radioButtonApplicationTypeUQHP.Checked = true;
                myApplication.myEnrollmentPlanType = "MN Care UQHP";
            }
        }


        private void textBoxMethodWindowId_TextChanged(object sender, EventArgs e)
        {

            string myImage;
            string mysWindowId;
            mysWindowId = textBoxMethodWindowId.Text;
            string myScreenId;
            myScreenId = ""; ;
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            try
            {
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT ScreenId FROM Windows where WindowId = " + mysWindowId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myScreenId = reader.GetString(0);
                    }
                }
            }
            catch (Exception n)
            {
                //   MessageBox.Show("Did not find window image, Exception: " + n);
            }
            try
            {
                myImage = @"C:\MNsure Regression 1\WindowScreenshots\" + myScreenId;
                pictureBoxMethodWindow.Image = System.Drawing.Image.FromFile(myImage);
            }
            catch
            {
                //silent fail
                return;
            }
        }

        private void dataGridViewAvailableMethods_SelectionChanged(object sender, EventArgs e)
        {
            string myImage;
            string mysWindowId;
            int rowindexMethod;

            if (dataGridViewAvailableMethods.CurrentCell == null)
            {
                rowindexMethod = 0;
            }
            else
            {
                rowindexMethod = dataGridViewAvailableMethods.CurrentCell.RowIndex;
            }

            if (dataGridViewAvailableMethods.Rows[rowindexMethod].Cells[0].Value == null)
            {
                mysWindowId = "1";
            }
            else
            {
                mysWindowId = dataGridViewAvailableMethods.Rows[rowindexMethod].Cells[6].Value.ToString();
            }

            string myScreenId;
            myScreenId = ""; ;
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            try
            {
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT ScreenId FROM Windows where WindowId = " + mysWindowId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myScreenId = reader.GetString(0);
                    }
                }
            }
            catch (Exception n)
            {
                MessageBox.Show("Did not find window image, Exception: " + n);
            }

            try
            {
                myImage = @"C:\MNsure Regression 1\WindowScreenshots\" + myScreenId;
                pictureBoxTestMethodWindow.Image = System.Drawing.Image.FromFile(myImage);
            }
            catch
            {

                //fail silently
            }
        }

        private void buttonTemplatesSave_Click(object sender, EventArgs e)
        {
            string myTemplateId;
            string myTestId;
            string myName;

            myTemplateId = textBoxTemplateId.Text;
            myTestId = textBoxTemplatesTestID.Text;
            myName = textBoxTemplatesName.Text;

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                int rowindex = dataGridViewTemplates.CurrentCell.RowIndex;
                myEditKey.myTemplateEditKey = dataGridViewTemplates.Rows[rowindex].Cells[0].Value.ToString();

                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM TestTemplates where TemplateId = " + myTemplateId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update TestTemplates set TemplateName = @Name"
                          + " , TestId  = @TestId " +
                            " where TemplateId = " + myEditKey.myTemplateEditKey + ";";
                        using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                        {
                            com3.Parameters.AddWithValue("Name", myName);
                            com3.Parameters.AddWithValue("TestId", myTestId);
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                    else
                    {
                        string myInsertString;
                        myInsertString = "Insert into TestTemplates Values (" + myTemplateId +
                            ", " + myTestId + ", '" + myName + "' );";
                        using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                        {
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                }

                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from TestTemplates;";
                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewTemplates.DataSource = dt;
                myEditKey.myTemplateEditKey = myTemplateId;
                myEditKey.myTemplateFirstTime = "No";

            }
            catch
            {
                MessageBox.Show("Add New Template didn't work");
            }
            myEditKey.myTemplateFirstTime = "No";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void buttonTemplatesDelete_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewTemplates.CurrentCell.RowIndex;
            myEditKey.myTemplateFirstTime = "No";
            String mysTemplateId;
            mysTemplateId = dataGridViewTemplates.Rows[rowindex].Cells[0].Value.ToString();


            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                con = new SqlCeConnection(conString);
                con.Open();


                DialogResult result1 = MessageBox.Show("Are you sure you want to delete this Template?", "Delete Test", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string myDeleteString;
                    myDeleteString = "Delete  FROM TestTemplates where TemplateId = " + mysTemplateId;
                    using (SqlCeCommand com3 = new SqlCeCommand(myDeleteString, con))
                    {
                        com3.ExecuteNonQuery();
                        com3.Dispose();
                        myEditKey.myTemplateFirstTime = "No";
                        myEditKey.myTemplateDeletedRow = "Yes";
                    }
                }

                dataGridViewTemplates.Rows[1].Selected = true;
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from TestTemplates;";

                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewTemplates.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Delete Template didn't work");
            }
            //set deleted row back to No when completed
            myEditKey.myTemplateDeletedRow = "No";
        }

        private void buttonTemplatesAdd_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewTemplates.CurrentCell.RowIndex;
            myEditKey.myTemplateFirstTime = "No";
            String mysTemplateId;
            mysTemplateId = dataGridViewTemplates.Rows[rowindex].Cells[0].Value.ToString();

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT max(TemplateId) FROM TestTemplates", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myEditKey.myTemplateEditKey = Convert.ToString(reader.GetInt32(0) + 1);

                    }
                    else
                    {
                        MessageBox.Show("Did not find Template Test id");
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Add Test Template ID didn't work");
            }
            textBoxTemplateId.Text = myEditKey.myTemplateEditKey;
            textBoxTemplatesName.Text = "";
            textBoxTemplatesTestID.Text = "0";
        }

        private void textBoxWindowId_TextChanged(object sender, EventArgs e)
        {
            string myImage;
            string mysWindowId;
            mysWindowId = textBoxWindowId.Text;
            string myScreenId;
            myScreenId = ""; ;
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            try
            {
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT ScreenId FROM Windows where WindowId = " + mysWindowId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myScreenId = reader.GetString(0);
                    }
                }
            }
            catch (Exception n)
            {
                return;
            }
            try
            {
                myImage = @"C:\MNsure Regression 1\WindowScreenshots\" + myScreenId;
                pictureBoxWindow.Image = System.Drawing.Image.FromFile(myImage);
            }
            catch
            {
                return;
            }
        }

        private void buttonTestDelete_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewTestsPick.CurrentCell.RowIndex;
            myEditKey.myTestFirstTime = "No";
            String mysTestId;
            mysTestId = dataGridViewTestsPick.Rows[rowindex].Cells[0].Value.ToString();
            int myiTestId;
            myiTestId = Convert.ToInt32(mysTestId);
            string myTestName;
            myTestName = dataGridViewTestsPick.Rows[rowindex].Cells[1].Value.ToString();
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM Test where TestId = '" + mysTestId + "';", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    DialogResult result1 = MessageBox.Show("Are you sure you want to delete this Test?", "Delete Test", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        string myDeleteString;
                        myDeleteString = "Delete FROM Test where TestId = " + mysTestId;
                        using (SqlCeCommand com3 = new SqlCeCommand(myDeleteString, con))
                        {
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                            myEditKey.myTestFirstTime = "No";
                            myEditKey.myTestDeletedRow = "Yes";
                        }
                        string myDeleteString2;
                        myDeleteString2 = "Delete FROM TestSteps where TestId = " + mysTestId;
                        using (SqlCeCommand com3 = new SqlCeCommand(myDeleteString2, con))
                        {
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                            myEditKey.myTestFirstTime = "No";
                            myEditKey.myTestDeletedRow = "Yes";
                        }
                    }
                }

                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Test;";

                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewTestsPick.DataSource = dt;
            }
            catch (Exception w)
            {
                MessageBox.Show("Delete Test didn't work: Exception" + w);

            }
            //set deleted row back to No when completed
            myEditKey.myTestDeletedRow = "No";
        }

        private void tabPageTest_Click(object sender, EventArgs e)
        {

        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            string myTestId = Convert.ToString(textBoxTestTestId.Text);
            string myTestName = Convert.ToString(textBoxTestName.Text);
            string myTestType = Convert.ToString(textBoxTestType.Text);
            string myDescription = Convert.ToString(textBoxTestDescription.Text);
            string myURL = Convert.ToString(textBoxTestURL.Text);
            string myIsSelected = Convert.ToString(textBoxTestIsSelected.Text);
            string myNotes = Convert.ToString(textBoxTestNotes.Text);
            int myiTestStepId;
            int rowindexMethod = dataGridViewTestSteps.CurrentCell.RowIndex;
            string myWindow = dataGridViewTestSteps.Rows[rowindexMethod].Cells[0].Value.ToString();
            string myClass = dataGridViewTestSteps.Rows[rowindexMethod].Cells[1].Value.ToString();
            string myMethodName = dataGridViewTestSteps.Rows[rowindexMethod].Cells[2].Value.ToString();
            string myTestStepNotes = dataGridViewTestSteps.Rows[rowindexMethod].Cells[3].Value.ToString();
            string myTestStepURL = dataGridViewTestSteps.Rows[rowindexMethod].Cells[4].Value.ToString();
            string mysMethodId = dataGridViewTestSteps.Rows[rowindexMethod].Cells[5].Value.ToString();
            string myWindowId = dataGridViewTestSteps.Rows[rowindexMethod].Cells[6].Value.ToString();

            int rowindexTest;

            rowindexTest = dataGridViewTestsPick.CurrentCell.RowIndex;
            myTestId = dataGridViewTestsPick.Rows[rowindexTest].Cells[0].Value.ToString();
            string myNewTestId = myTestId;

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT max(TestId) FROM Test", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myNewTestId = Convert.ToString(reader.GetInt32(0) + 1);
                    }
                    string myInsertString;
                    DateTime now = DateTime.Now;
                    myInsertString = "Insert into Test Values (" + myNewTestId +
                        ",   @Name, @Type, @Description, @Notes, @URL" +
                        ",   @IsSelected   );";
                    using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                    {
                        com3.Parameters.AddWithValue("TestId", myNewTestId);
                        com3.Parameters.AddWithValue("Name", myTestName);
                        com3.Parameters.AddWithValue("Type", myTestType);
                        com3.Parameters.AddWithValue("Description", myDescription);
                        com3.Parameters.AddWithValue("URL", myURL);
                        com3.Parameters.AddWithValue("IsSelected", myIsSelected);
                        com3.Parameters.AddWithValue("Notes", myNotes);
                        com3.ExecuteNonQuery();
                        com3.Dispose();
                    }
                }

                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from Test;";

                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewTestsPick.DataSource = dt;
                myEditKey.myTestEditKey = "1";
                con.Close();
            }
            catch (Exception u)
            {
                MessageBox.Show("Copy New Test didn't work, Exception: " + u);
            }

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                using (SqlCeCommand com2 = new SqlCeCommand("Select * from TestSteps where TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    while (reader.Read())
                    {
                        myiTestStepId = reader.GetInt32(1);
                        myWindow = reader.GetString(2);
                        myWindowId = reader.GetString(3);
                        myClass = reader.GetString(4);
                        myMethodName = reader.GetString(5);
                        myTestStepNotes = reader.GetString(6);
                        myTestStepURL = reader.GetString(7);

                        myInsertString = "insert into TestSteps values(" + myNewTestId.ToString() + ", " + myiTestStepId +
                        ", '" + myWindow + "', '" + myWindowId + "', '" + myClass + "', '" + myMethodName + "', '', ''); ";

                        using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                        {
                            com3.ExecuteNonQuery();
                            com3.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Copy New Test Steps didn't work, Exception: " + a);
            }

            string testId;
            testId = textBoxTestTestId.Text;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "Select * from TestSteps where TestId = " + myNewTestId + ";";
            cmd2.ExecuteNonQuery();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            SqlCeDataAdapter da2 = new SqlCeDataAdapter(cmd2);
            da2.Fill(dt2);
            dataGridViewTestSteps.DataSource = dt2;

        }

        private void comboBoxWaitTime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerTimeTravel_ValueChanged(object sender, EventArgs e)
        {
            labelTimeTravel.BackColor = Color.Green;
            myHistoryInfo.myTimeTravelDate = dateTimePickerTimeTravel.Value;
        }

        private void checkBoxTimeTravel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTimeTravel.Checked == true)
            {
                labelTimeTravel.BackColor = Color.Yellow;
                labelTimeTravel.Visible = true;
                myHistoryInfo.myInTimeTravel = "Yes";
            }
            else
            {
                labelTimeTravel.Visible = false;
                myHistoryInfo.myInTimeTravel = "No";
            }
        }

        private void comboBoxCitizenWait_SelectedValueChanged(object sender, EventArgs e)
        {
            //myHistoryInfo.myCitizenWait = Convert.ToInt32(comboBoxCitizenWait.SelectedValue);
        }

        private void comboBoxCaseWorkerWait_SelectedValueChanged(object sender, EventArgs e)
        {
            //myHistoryInfo.myCaseWorkerWait = Convert.ToInt32(comboBoxCaseWorkerWait.SelectedValue);
        }

        private void textBoxMNSureBuild_TextChanged(object sender, EventArgs e)
        {
            myHistoryInfo.myMnsureBuild = textBoxMNSureBuild.Text;
        }

        private void tabPageTemplate_Enter(object sender, EventArgs e)
        {
            if (myHistoryInfo.myTestId == null)
            {
                tabControlMain.SelectedIndex = 0;
            }
            else
            {
                int rowindex;
                myEditKey.myTemplateFirstTime = "Yes";
                rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
                mySelectedTest.myRowIndex = rowindex;
                string mysTestId;
                mysTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
                myEditKey.myWindowsFirstTime = "Yes";
                SqlCeConnection con;
                string conString = Properties.Settings.Default.Database1ConnectionString;
                con = new SqlCeConnection(conString);
                con.Open();
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from TestTemplates;";
                cmd.ExecuteNonQuery();
                System.Data.DataTable dt = new System.Data.DataTable();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(dt);
                dataGridViewTemplates.DataSource = dt;
                dataGridViewTemplates.Columns["TestId"].DisplayIndex = 2;
                myEditKey.myTemplateEditKey = "1";
                myEditKey.myTemplateFirstTime = "No";

                dataGridViewTemplates.AutoGenerateColumns = true;
                DataGridViewColumn TempId_Column = dataGridViewTemplates.Columns[0];
                TempId_Column.Width = 100;
                DataGridViewColumn TestId_Column2 = dataGridViewTemplates.Columns[1];
                TestId_Column2.Width = 100;
                DataGridViewColumn Name_Column = dataGridViewTemplates.Columns[2];
                Name_Column.Width = 275;

                // Put each of the columns into programmatic sort mode.
                foreach (DataGridViewColumn column in dataGridViewTemplates.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                }
            }
        }

        private void tabPageTemplate_Leave(object sender, EventArgs e)
        {
            myNavHelper.myConfigureClicked = "No";
        }

        private void dataGridViewTemplate_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex;

            if (dataGridViewTemplates.CurrentCell == null)
            {
                rowindex = 0;
            }
            else
            {
                rowindex = dataGridViewTemplates.CurrentCell.RowIndex;
            }

            String mysTemplateId;
            if (dataGridViewTemplates.Rows[rowindex].Cells[0].Value == null)
            {
                mysTemplateId = "1";
            }
            else
            {
                mysTemplateId = dataGridViewTemplates.Rows[rowindex].Cells[0].Value.ToString();
            }

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            if (myEditKey.myTemplateFirstTime == "Yes" || myEditKey.myTemplateDeletedRow == "Yes")
            {
                myEditKey.myTemplateEditKey = "1";
            }
            else
            {
                myEditKey.myTemplateEditKey = mysTemplateId;
            }

            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * from TestTemplates where TemplateId =  " + myEditKey.myTemplateEditKey, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        textBoxTemplateId.Text = Convert.ToString(reader.GetInt32(0));
                        textBoxTemplatesTestID.Text = Convert.ToString(reader.GetInt32(1));
                        textBoxTemplatesName.Text = reader.GetString(2);
                    }
                    else
                    {
                        MessageBox.Show("Did not find id");
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Add ID didn't work");

            }
        }

        private void dataGridViewAvailableTests_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridViewColumn newColumn = dataGridViewAvailableTests.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewAvailableTests.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewAvailableTests.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewAvailableTests.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewSelectedTests_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewSelectedTests.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewSelectedTests.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewSelectedTests.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewSelectedTests.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewTemplates_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewTemplates.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewTemplates.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewTemplates.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewTemplates.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewWindowsPick_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewWindowsPick.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewWindowsPick.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewWindowsPick.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewWindowsPick.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewMethodsPick_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewMethodsPick.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewMethodsPick.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewMethodsPick.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewMethodsPick.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewTestsPick_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewTestsPick.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewTestsPick.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewTestsPick.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewTestsPick.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewTestSteps_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewTestSteps.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewTestSteps.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewTestSteps.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewTestSteps.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewAvailableMethods_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewAvailableMethods.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewAvailableMethods.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewAvailableMethods.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewAvailableMethods.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewTestRunHistory_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewTestRunHistory.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewTestRunHistory.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewTestRunHistory.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewTestRunHistory.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void dataGridViewTestHistory_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridViewTestHistory.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridViewTestHistory.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridViewTestHistory.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            dataGridViewTestHistory.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
            direction == ListSortDirection.Ascending ?
            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
        }

        private void tabPageRun_Leave(object sender, EventArgs e)
        {

        }

        public class MyItem
        {
            public int Id { get; set; }
            public String Name { get; set; }
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from TestSteps where TestId = " + myEditKey.myTestEditKey + ";";
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewTestSteps.DataSource = dt;
            bs = new BindingSource(dt, string.Empty);
            dataGridViewTestSteps.DataSource = bs;

            int position = bs.Position;
            if (position == 0) return;  // already at top

            bs.RaiseListChangedEvents = false;

            MyItem current = (MyItem)bs.Current;
            bs.Remove(current);

            position--;

            bs.Insert(position, current);
            bs.Position = position;

            bs.RaiseListChangedEvents = true;
            bs.ResetBindings(false);
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            int position = bs.Position;
            if (position == bs.Count - 1) return;  // already at bottom

            bs.RaiseListChangedEvents = false;

            MyItem current = (MyItem)bs.Current;
            bs.Remove(current);

            position++;

            bs.Insert(position, current);
            bs.Position = position;

            bs.RaiseListChangedEvents = true;
            bs.ResetBindings(false);
        }

        private void textBoxExecutedBy_TextChanged(object sender, EventArgs e)
        {
            myHistoryInfo.myExecutedBy = textBoxExecutedBy.Text;
        }

        private void groupBoxApplicantInformation_Enter(object sender, EventArgs e)
        {

        }



    }
}
