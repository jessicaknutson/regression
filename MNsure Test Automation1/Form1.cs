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
//using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using System.Data.Sql;
using OpenQA.Selenium.Support.UI;
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
        mystructAssister myAssister;
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
            myHistoryInfo.myIcnumber = null;
            myApplication.myDay2TestId = null;
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
            object reflectResultcwad = null;
            object reflectResulten = null;
            object reflectResultcw = null;
            object reflectResulthm = null;
            object reflectResultwad = null;
            object reflectResulta = null;
            //This loops through based on the number of tests selected to run
            for (iloop = 1; iloop <= testcount - 1; iloop++)
            {
                myHistoryInfo.myTestStepStatus = "none";
                mysTestId = dataGridViewSelectedTests.Rows[iloop - 1].Cells[0].Value.ToString();
                mySelectedTest.myTestId = Convert.ToInt32(mysTestId);
                myHistoryInfo.myTestId = mysTestId;

                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT TemplateName FROM TestTemplates where TestId = " + mySelectedTest.myTestId, con))
                {
                    SqlCeDataReader reader2 = com.ExecuteReader();
                    if (reader2.Read())
                    {
                        myHistoryInfo.myTemplate = reader2.GetString(0);
                    }
                }
                con.Close();

                FirefoxDriver driver = null;
                FirefoxDriver driver2 = null;
                FirefoxDriver driver3 = null;
                FirefoxDriver driver4 = null;
                FirefoxDriver driver5 = null;               
                IWebDriver driver6 = null;
                IWebDriver driver7 = null;
                IWebDriver driver8 = null;
                IWebDriver driver9 = null;
                IWebDriver driver10 = null;

                //must clear cache first
                if (myHistoryInfo.myBrowser == "Firefox")
                {             
                    //main driver for citizen portal
                    driver = new FirefoxDriver();
                    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));                    
                } 
                else
                {
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("-incognito");

                    driver6 = new ChromeDriver("C:\\MNsure Regression 1", options);//chrome version must be 51
                    driver6.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));                    
                }               

                result = writeLogs.WriteRunHistoryRowStart(ref myHistoryInfo);
                result = writeLogs.WriteTestHistoryRowStart(ref myHistoryInfo);                

                try
                {
                    //Fill structures for Test
                    InitializeSSN myInitializeSSN = new InitializeSSN();
                    result = myInitializeSSN.DoReadLines(ref myLastSSN, ref myReadFileValues);
                    int temp1 = Convert.ToInt32(myLastSSN.myLastSSN) + 1;
                    myAccountCreate.mySSN = Convert.ToString(temp1);
                    if (myHistoryInfo.myEnvironment == "STST2")
                    {
                        myAccountCreate.mySSN = myAccountCreate.mySSN.Remove(0, 3).Insert(0, "444");
                    }
                    if (myHistoryInfo.myEnvironment == "STST")
                    {
                        string beginning = myAccountCreate.mySSN.Substring(0, 3);
                        if (beginning == "444")
                        {
                            myAccountCreate.mySSN = myAccountCreate.mySSN.Remove(0, 3).Insert(0, "144");
                        }
                    }

                    FillStructures myFillStructures = new FillStructures();
                    result = myFillStructures.doCreateAccount(ref mySelectedTest, ref myAccountCreate, ref myApplication, ref myHistoryInfo);
                    
                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    AccountGeneration myAccountGeneration = new AccountGeneration();
                    if (householdCount > 1)
                    {
                        result = myAccountGeneration.GenerateHouseholdNames(ref myHouseholdMembers, mySelectedTest.myTestId, "2", ref myHistoryInfo);
                    }
                    if (householdCount == 3)
                    {
                        result = myAccountGeneration.GenerateHouseholdNames(ref myHouseholdMembers, mySelectedTest.myTestId, "3", ref myHistoryInfo);
                    }
                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHouseholdMembers, ref myAssister, ref myHistoryInfo);
                    
                    if (myAssister.myFirstName!= null)//must create a second account for assister
                    {
                        result = myFillStructures.doCreateAssisterAccount(ref mySelectedTest, ref myAccountCreate, ref myApplication, ref myHistoryInfo);
                    }

                    result = writeLogs.DoGetRequiredScreenshots(ref myHistoryInfo);

                    if (myApplication.myHouseholdOther == "Yes" && householdCount == 2) //for 2nd member in household
                    {
                        int temp2 = temp1 + 1;
                        myHouseholdMembers.mySSN = Convert.ToString(temp2);
                        if (myHistoryInfo.myEnvironment == "STST2")
                        {
                            myHouseholdMembers.mySSN = myHouseholdMembers.mySSN.Remove(0, 3).Insert(0, "444");
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            string beginning = myHouseholdMembers.mySSN.Substring(0, 3);
                            if (beginning == "444")
                            {
                                myHouseholdMembers.mySSN = myHouseholdMembers.mySSN.Remove(0, 3).Insert(0, "144");
                            }
                        }
                        myLastSSN.myLastSSN = myHouseholdMembers.mySSN;

                        result = myFillStructures.doUpdateHouseholdSSN(ref myHistoryInfo, myHouseholdMembers.mySSN, "2");
                    }
                    else if (myApplication.myHouseholdOther == "Yes" && householdCount == 3) //for 3rd member in household
                    {
                        int temp3 = temp1 + 2;
                        myLastSSN.myLastSSN = Convert.ToString(temp3);
                        if (myHistoryInfo.myEnvironment == "STST2")
                        {
                            myLastSSN.myLastSSN = myLastSSN.myLastSSN.Remove(0, 3).Insert(0, "444");
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            string beginning = myLastSSN.myLastSSN.Substring(0, 3);
                            if (beginning == "444")
                            {
                                myLastSSN.myLastSSN = myLastSSN.myLastSSN.Remove(0, 3).Insert(0, "144");
                            }
                        }
                    }
                    else if (myAssister.myFirstName != null) //for assister
                    {
                        int temp2 = temp1 + 1;
                        myAssister.mySSN = Convert.ToString(temp2);
                        if (myHistoryInfo.myEnvironment == "STST2")
                        {
                            myAssister.mySSN = myAssister.mySSN.Remove(0, 3).Insert(0, "444");
                        }
                        if (myHistoryInfo.myEnvironment == "STST")
                        {
                            string beginning = myAssister.mySSN.Substring(0, 3);
                            if (beginning == "444")
                            {
                                myAssister.mySSN = myAssister.mySSN.Remove(0, 3).Insert(0, "144");
                            }
                        }
                        myLastSSN.myLastSSN = myAssister.mySSN;

                        result = myFillStructures.doUpdateAssisterSSN(ref myHistoryInfo, myAssister.mySSN);
                    }
                    else
                    {
                        myLastSSN.myLastSSN = myAccountCreate.mySSN;
                    }

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
                            string relogin = "";
                            string resume = "";
                            string assisterNavigator = "";                            

                            switch (myClass)
                            {
                                case "OpenSiteURL":

                                    if (myMethod == "DoCaseWorkerURLOpen" || myMethod == "DoCaseWorkerURLOpenTimeTravel")
                                    {                                        
                                        if (myHistoryInfo.myBrowser == "Firefox")
                                        {
                                            driver.Dispose();

                                            FirefoxProfile profile2 = new FirefoxProfile();

                                            profile2.SetPreference("browser.cache.disk.enable", false);
                                            profile2.SetPreference("browser.cache.memory.enable", false);
                                            profile2.SetPreference("browser.cache.offline.enable", false);
                                            profile2.SetPreference("network.http.use-cache", false);

                                            //create separate driver for case worker
                                            driver2 = new FirefoxDriver(profile2);
                                            driver2.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                                        }
                                        else
                                        {
                                            driver6.Quit();

                                            ChromeOptions options = new ChromeOptions();
                                            options.AddArguments("-incognito");

                                            //create separate driver for case worker
                                            driver7 = new ChromeDriver("C:\\MNsure Regression 1", options);
                                            driver7.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                                        }
                                    }

                                    else if (myMethod == "DoOpenMNsureRelogin" || myMethod == "DoOpenMNsureReloginTimeTravel" || myMethod == "DoAssisterURLOpen" || myMethod == "DoAssisterTimeTravel")
                                    {                                        
                                        if (myHistoryInfo.myBrowser == "Firefox")
                                        {                                                                                    
                                            //must clear cache first
                                            FirefoxProfile profile3 = new FirefoxProfile();
                                            profile3.SetPreference("browser.cache.disk.enable", false);
                                            profile3.SetPreference("browser.cache.memory.enable", false);
                                            profile3.SetPreference("browser.cache.offline.enable", false);
                                            profile3.SetPreference("network.http.use-cache", false);

                                            //create separate driver for logout and relogin to citizen portal, also assister manager
                                            driver3 = new FirefoxDriver(profile3);
                                            driver3.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                                        }
                                        else
                                        {
                                            driver8 = new ChromeDriver("C:\\MNsure Regression 1");
                                            driver8.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                                        }
                                    }
                                    else if (myMethod == "DoAssisterReloginURLOpen" || myMethod == "DoAssisterReloginTimeTravel")
                                    {
                                        driver3.Dispose();
                                        if (myHistoryInfo.myBrowser == "Firefox")
                                        {                                                                                 
                                            //must clear cache first
                                            FirefoxProfile profile4 = new FirefoxProfile();
                                            profile4.SetPreference("browser.cache.disk.enable", false);
                                            profile4.SetPreference("browser.cache.memory.enable", false);
                                            profile4.SetPreference("browser.cache.offline.enable", false);
                                            profile4.SetPreference("network.http.use-cache", false);

                                            //create separate driver for logout and relogin to citizen portal, also assister manager
                                            driver4 = new FirefoxDriver(profile4);
                                            driver4.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                                        }
                                        else
                                        {
                                            driver9 = new ChromeDriver("C:\\MNsure Regression 1");
                                            driver9.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                                        }
                                    }
                                    else if (myMethod == "DoNavigatorURLOpen" || myMethod == "DoNavigatorTimeTravel")
                                    {
                                        //driver4.Dispose();
                                        if (myHistoryInfo.myBrowser == "Firefox")
                                        {
                                            //must clear cache first
                                            FirefoxProfile profile5 = new FirefoxProfile();
                                            profile5.SetPreference("browser.cache.disk.enable", false);
                                            profile5.SetPreference("browser.cache.memory.enable", false);
                                            profile5.SetPreference("browser.cache.offline.enable", false);
                                            profile5.SetPreference("network.http.use-cache", false);

                                            //create separate driver for logout and relogin to citizen portal, also assister manager
                                            driver5 = new FirefoxDriver(profile5);
                                            driver5.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                                        }
                                        else
                                        {
                                            driver10 = new ChromeDriver("C:\\MNsure Regression 1");
                                            driver10.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                                        }

                                        myFillStructures.doGetAccount(ref myAccountCreate, ref myHistoryInfo, mysTestId, "1");
                                    }
                                    
                                    object[] parms = new object[11];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parms[0] = driver;
                                        parms[1] = driver2;
                                        parms[2] = driver3;
                                        parms[3] = driver4;
                                        parms[4] = driver5;
                                    }
                                    else
                                    {
                                        parms[0] = driver6;
                                        parms[1] = driver7;
                                        parms[2] = driver8;
                                        parms[3] = driver9;
                                        parms[4] = driver10;
                                    }
                                    parms[5] = myHistoryInfo;
                                    parms[6] = returnStatus;
                                    parms[7] = returnException;
                                    parms[8] = returnScreenshot;
                                    parms[9] = relogin;
                                    parms[10] = assisterNavigator;

                                    OpenSiteURL newOpenSiteURL = new OpenSiteURL();
                                    Type reflectTestType = typeof(OpenSiteURL);
                                    MethodInfo reflectMethodToInvoke = reflectTestType.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParameters = reflectMethodToInvoke.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResult = reflectMethodToInvoke.Invoke(new OpenSiteURL(), parms);
                                    myHistoryInfo.myTestStepStatus = parms[6].ToString();
                                    myHistoryInfo.myStepException = parms[7].ToString();
                                    myHistoryInfo.myScreenShot = parms[8].ToString();
                                    myHistoryInfo.myRelogin = parms[9].ToString();
                                    myHistoryInfo.myAssisterNavigator = parms[10].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    break;

                                case "AccountCreation":                                    
                                    object[] parmsac = new object[10];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parmsac[0] = driver;
                                        parmsac[1] = driver3;
                                        parmsac[2] = driver5;
                                    }
                                    else
                                    {
                                        parmsac[0] = driver6;
                                        parmsac[1] = driver8;
                                        parmsac[2] = driver10;
                                    }
                                    parmsac[3] = myAccountCreate;
                                    parmsac[4] = myApplication;
                                    parmsac[5] = myHistoryInfo;
                                    parmsac[6] = returnStatus;
                                    parmsac[7] = returnException;
                                    parmsac[8] = returnScreenshot;
                                    parmsac[9] = resume;

                                    AccountCreation newAccount = new AccountCreation();
                                    Type reflectTestTypeac = typeof(AccountCreation);
                                    MethodInfo reflectMethodToInvokeac = reflectTestTypeac.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParametersac = reflectMethodToInvokeac.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResultac = reflectMethodToInvokeac.Invoke(newAccount, parmsac);
                                    myHistoryInfo.myTestStepStatus = parmsac[6].ToString();
                                    myHistoryInfo.myStepException = parmsac[7].ToString();
                                    myHistoryInfo.myScreenShot = parmsac[8].ToString();
                                    myHistoryInfo.myResume = parmsac[9].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    if (myAssister.myFirstName != null) //for assister only
                                    {
                                        myFillStructures.doGetAccount(ref myAccountCreate, ref myHistoryInfo, mysTestId, "1");
                                    }
                                    break;

                                case "ApplicationDo":
                                    
                                    object[] parmsad = new object[9];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parmsad[0] = driver;
                                        parmsad[1] = driver5;
                                    }
                                    else
                                    {
                                        parmsad[0] = driver6;
                                        parmsad[1] = driver10;
                                    }
                                    parmsad[2] = myAccountCreate;
                                    parmsad[3] = myApplication;
                                    parmsad[4] = myHouseholdMembers;
                                    parmsad[5] = myHistoryInfo;
                                    parmsad[6] = returnStatus;
                                    parmsad[7] = returnException;
                                    parmsad[8] = returnScreenshot;

                                    ApplicationDo myApplicationDo = new ApplicationDo();
                                    Type reflectTestTypead = typeof(ApplicationDo);
                                    MethodInfo reflectMethodToInvokead = reflectTestTypead.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParametersad = reflectMethodToInvokead.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResultad = reflectMethodToInvokead.Invoke(myApplicationDo, parmsad);
                                    myHistoryInfo.myTestStepStatus = parmsad[6].ToString();
                                    myHistoryInfo.myStepException = parmsad[7].ToString();
                                    myHistoryInfo.myScreenShot = parmsad[8].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    //must fill structures again after updating pass count
                                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHouseholdMembers, ref myAssister, ref myHistoryInfo);
                                    break;

                                case "CWApplicationDo":
                                    object[] parmscwad = new object[8];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parmscwad[0] = driver2;
                                    }
                                    else
                                    {
                                        parmscwad[0] = driver7;
                                    }
                                    parmscwad[1] = myAccountCreate;
                                    parmscwad[2] = myApplication;
                                    parmscwad[3] = myHouseholdMembers;
                                    parmscwad[4] = myHistoryInfo;
                                    parmscwad[5] = returnStatus;
                                    parmscwad[6] = returnException;
                                    parmscwad[7] = returnScreenshot;

                                    CWApplicationDo myCWApplicationDo = new CWApplicationDo();
                                    Type reflectTestTypecwad = typeof(CWApplicationDo);
                                    MethodInfo reflectMethodToInvokecwad = reflectTestTypecwad.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParameterscwad = reflectMethodToInvokecwad.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResultcwad = reflectMethodToInvokecwad.Invoke(myCWApplicationDo, parmscwad);
                                    myHistoryInfo.myTestStepStatus = parmscwad[5].ToString();
                                    myHistoryInfo.myStepException = parmscwad[6].ToString();
                                    myHistoryInfo.myScreenShot = parmscwad[7].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    //must fill structures again after updating pass count
                                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHouseholdMembers, ref myAssister, ref myHistoryInfo);
                                    break;

                                case "WizardApplicationDo":
                                    object[] parmswad = new object[8];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parmswad[0] = driver2;
                                    }
                                    else
                                    {
                                        parmswad[0] = driver7;
                                    }
                                    parmswad[1] = myAccountCreate;
                                    parmswad[2] = myApplication;
                                    parmswad[3] = myHouseholdMembers;
                                    parmswad[4] = myHistoryInfo;
                                    parmswad[5] = returnStatus;
                                    parmswad[6] = returnException;
                                    parmswad[7] = returnScreenshot;

                                    WizardApplicationDo myWizardApplicationDo = new WizardApplicationDo();
                                    Type reflectTestTypewad = typeof(WizardApplicationDo);
                                    MethodInfo reflectMethodToInvokewad = reflectTestTypewad.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParameterswad = reflectMethodToInvokewad.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResultwad = reflectMethodToInvokewad.Invoke(myWizardApplicationDo, parmswad);
                                    myHistoryInfo.myTestStepStatus = parmswad[5].ToString();
                                    myHistoryInfo.myStepException = parmswad[6].ToString();
                                    myHistoryInfo.myScreenShot = parmswad[7].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    //must fill structures again after updating pass count
                                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHouseholdMembers, ref myAssister, ref myHistoryInfo);
                                    break;

                                case "HouseholdMembersDo":
                                    object[] parmshm = new object[9];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parmshm[0] = driver;
                                        parmshm[1] = driver2;
                                    }
                                    else
                                    {
                                        parmshm[0] = driver6;
                                        parmshm[1] = driver7;
                                    }
                                    parmshm[2] = myAccountCreate;
                                    parmshm[3] = myApplication;
                                    parmshm[4] = myHouseholdMembers;
                                    parmshm[5] = myHistoryInfo;
                                    parmshm[6] = returnStatus;
                                    parmshm[7] = returnException;
                                    parmshm[8] = returnScreenshot;

                                    HouseholdMembersDo myHouseholdMembersDo = new HouseholdMembersDo();
                                    Type reflectTestTypehm = typeof(HouseholdMembersDo);
                                    MethodInfo reflectMethodToInvokehm = reflectTestTypehm.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParametershm = reflectMethodToInvokehm.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResulthm = reflectMethodToInvokehm.Invoke(myHouseholdMembersDo, parmshm);
                                    myHistoryInfo.myTestStepStatus = parmshm[6].ToString();
                                    myHistoryInfo.myStepException = parmshm[7].ToString();
                                    myHistoryInfo.myScreenShot = parmshm[8].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    //must fill structures again after updating pass count
                                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHouseholdMembers, ref myAssister, ref myHistoryInfo);
                                    break;

                                case "Enrollments":
                                    object[] parmsen = new object[8];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parmsen[0] = driver;
                                        parmsen[1] = driver3;
                                    }
                                    else
                                    {
                                        parmsen[0] = driver6;
                                        parmsen[1] = driver8;
                                    }
                                    parmsen[2] = myApplication;
                                    parmsen[3] = myHistoryInfo;
                                    parmsen[4] = returnStatus;
                                    parmsen[5] = returnException;
                                    parmsen[6] = returnScreenshot;
                                    parmsen[7] = myHouseholdMembers;

                                    Enrollments myEnrollments = new Enrollments();
                                    Type reflectTestTypeen = typeof(Enrollments);
                                    MethodInfo reflectMethodToInvokeen = reflectTestTypeen.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParametersen = reflectMethodToInvokeen.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResulten = reflectMethodToInvokeen.Invoke(myEnrollments, parmsen);
                                    myHistoryInfo.myTestStepStatus = parmsen[4].ToString();
                                    myHistoryInfo.myStepException = parmsen[5].ToString();
                                    myHistoryInfo.myScreenShot = parmsen[6].ToString();
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);
                                    //must fill structures again after updating pass count
                                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHouseholdMembers, ref myAssister, ref myHistoryInfo);
                                    break;

                                case "CaseWorker":
                                    object[] parmscw = new object[8];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parmscw[0] = driver2;
                                    }
                                    else
                                    {
                                        parmscw[0] = driver7;
                                    }
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
                                    //must fill structures again after updating pass count
                                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHouseholdMembers, ref myAssister, ref myHistoryInfo);
                                    break;

                                case "Assister":
                                    
                                    object[] parmsa = new object[13];
                                    if (myHistoryInfo.myBrowser == "Firefox")
                                    {
                                        parmsa[0] = driver;
                                        parmsa[1] = driver2;
                                        parmsa[2] = driver3;
                                        parmsa[3] = driver4;
                                        parmsa[4] = driver5;
                                    }
                                    else
                                    {
                                        parmsa[0] = driver6;
                                        parmsa[1] = driver7;
                                        parmsa[2] = driver8;
                                        parmsa[3] = driver9;
                                        parmsa[4] = driver10;
                                    }
                                    parmsa[5] = myAccountCreate;
                                    parmsa[6] = myApplication;
                                    parmsa[7] = myAssister;
                                    parmsa[8] = myHistoryInfo;
                                    parmsa[9] = returnStatus;
                                    parmsa[10] = returnException;
                                    parmsa[11] = returnScreenshot;
                                    parmsa[12] = returnICNumber;

                                    AssisterDo myAssisterDo = new AssisterDo();
                                    Type reflectTestTypea = typeof(AssisterDo);
                                    MethodInfo reflectMethodToInvokea = reflectTestTypea.GetMethod(myMethod);
                                    ParameterInfo[] reflectMethodParametersa = reflectMethodToInvokea.GetParameters();
                                    result = writeLogs.DoWriteHistoryTestStepStart(ref myHistoryInfo);
                                    reflectResulta = reflectMethodToInvokea.Invoke(new AssisterDo(), parmsa);
                                    myHistoryInfo.myTestStepStatus = parmsa[9].ToString();
                                    myHistoryInfo.myStepException = parmsa[10].ToString();
                                    myHistoryInfo.myScreenShot = parmsa[11].ToString();
                                    if (parmsa[12].ToString() != String.Empty)
                                    {
                                        myHistoryInfo.myIcnumber = parmsa[12].ToString();
                                    }
                                    result = writeLogs.DoWriteHistoryTestStepEnd(ref myHistoryInfo);

                                    myFillStructures.doGetAccount(ref myAccountCreate, ref myHistoryInfo, mysTestId, "2");

                                    //must fill structures again after updating pass count
                                    result = myFillStructures.doFillStructures(mySelectedTest, myAccountCreate, ref myApplication, ref myHouseholdMembers, ref myAssister, ref myHistoryInfo);
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

                //driver.Dispose();
                //driver2.Dispose();
                //driver3.Dispose();
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
            String mysRunid = dataGridViewTestRunHistory.Rows[rowindex].Cells[0].Value.ToString();
            String mysTestid = dataGridViewTestRunHistory.Rows[rowindex].Cells[1].Value.ToString();
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from TestHistory where RunId = " + mysRunid + " and TestId = " + mysTestid + ";";
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
                    using (SqlCeCommand com3 = new SqlCeCommand("SELECT * FROM Application where TestId = " + myTestId, con))
                    {
                        SqlCeDataReader reader = com3.ExecuteReader();
                        if (reader.Read())
                        {
                            myApplication.myFirstName = reader.GetString(2);
                            myApplication.myMiddleName = reader.GetString(3);
                            myApplication.myLastName = reader.GetString(4);
                            if (!reader.IsDBNull(5))
                            {
                                myApplication.mySuffix = reader.GetString(5);
                            }
                            myApplication.myGender = reader.GetString(6);
                            myApplication.myMaritalStatus = reader.GetString(7);
                            if (!reader.IsDBNull(8))
                            {
                                string tempDOB;
                                tempDOB = Convert.ToString(reader.GetDateTime(8));
                                tempDOB = DateTime.Parse(tempDOB).ToString("M/d/yyyy");
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
                            if (!reader.IsDBNull(29))
                            {
                                myApplication.mySSNNum = reader.GetString(29);
                            }
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
                            myApplication.myWithDiscounts = reader.GetString(60);
                            myApplication.myIsPregnant = reader.GetString(61);
                            if (!reader.IsDBNull(62)) { myApplication.myChildren = reader.GetString(62); }
                            if (!reader.IsDBNull(63)) { myApplication.myDueDate = Convert.ToString(reader.GetDateTime(63)); }
                            if (!reader.IsDBNull(64)) { myApplication.myPregnancyEnded = Convert.ToString(reader.GetDateTime(64)); }
                            if (!reader.IsDBNull(65))
                            {
                                myApplication.myRegDate = Convert.ToString(reader.GetDateTime(65));
                                myApplication.myRegDate = DateTime.Parse(myApplication.myRegDate).ToString("M/d/yyyy");
                            }
                            else
                            {
                                myApplication.myRegDate = null;
                            }
                            if (!reader.IsDBNull(66)) { myApplication.myDay2TestId = reader.GetString(66); }
                            if (!reader.IsDBNull(67)) { myApplication.myPassCount = reader.GetString(67); }
                        }
                        else
                        {
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
                            myApplication.myTribeName = "";
                            myApplication.myTribeId = "";
                            myApplication.myMilitary = "No";
                            myApplication.myRace = "White";
                            myApplication.mySSN = "Yes";
                            myApplication.myCitizen = "Yes";
                            myApplication.myAppliedSSN = "No";
                            myApplication.myAssistSSN = "No";
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
                            myApplication.myOtherIns = "No";
                            myApplication.myCoverageEnd = "No";
                            myApplication.myAddIns = "No";
                            myApplication.myESC = "No";
                            myApplication.myRenewalCov = "5";
                            myApplication.myWithDiscounts = "Yes";
                            myApplication.myIsPregnant = "No";
                        }
                        com3.ExecuteNonQuery();
                        com3.Dispose();
                    }
                    //reset address values before continuing
                    if (myApplication.myHouseholdOther == "Yes")
                    {
                        myHouseholdMembers.myMailAddress1 = "";
                        myHouseholdMembers.myMailAddress2 = "";
                        myHouseholdMembers.myMailAptSuite = "";
                        myHouseholdMembers.myMailCity = "";
                        myHouseholdMembers.myMailState = "";
                        myHouseholdMembers.myMailZip = "";
                        myHouseholdMembers.myMailCounty = "";
                    }
                    myAssister.myAddress1 = "";
                    myAssister.myAddress2 = "";
                    myAssister.myAptSuite = "";
                    myAssister.myCity = "";
                    myAssister.myState = "";
                    myAssister.myZip = "";
                    myAssister.myCounty = "";

                    myApplication.myHomeAddress1 = "";
                    myApplication.myHomeAddress2 = "";
                    myApplication.myHomeCity = "";
                    myApplication.myHomeState = "";
                    myApplication.myHomeZip = "";
                    myApplication.myHomeZip4 = "";
                    myApplication.myHomeCounty = "";
                    myApplication.myHomeAptSuite = "";

                    myApplication.myMailAddress1 = "";
                    myApplication.myMailAddress2 = "";
                    myApplication.myMailCity = "";
                    myApplication.myMailState = "";
                    myApplication.myMailZip = "";
                    myApplication.myMailZip4 = "";
                    myApplication.myMailCounty = "";
                    myApplication.myMailAptSuite = "";

                    //reset assister values before continuing
                    myAssister.myFirstName = "";
                    myAssister.myLastName = "";
                    myAssister.myCommunication = "";
                    myAssister.myLanguage = "";
                    myAssister.myMethod = "";
                    myAssister.AssisterId = "";
                    myAssister.myPhoneType = "";
                    myAssister.myPhoneNum = "";
                    myAssister.myCategory = "";
                    myAssister.myType = "";
                    myAssister.myEmail = "";

                    SqlCeCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;

                    //Read configured rows if exist, otherwise fill with default values
                    using (SqlCeCommand com4 = new SqlCeCommand("SELECT * FROM Address where TestId = " + myTestId, con))
                    {
                        SqlCeDataReader reader = com4.ExecuteReader();
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
                            else if (reader.GetString(9) == "Household 2")
                            {
                                myHouseholdMembers.myMailAddress1 = reader.GetString(3);
                                if (!reader.IsDBNull(4))
                                {
                                    myHouseholdMembers.myMailAddress2 = reader.GetString(4);
                                }
                                myHouseholdMembers.myMailCity = reader.GetString(5);
                                myHouseholdMembers.myMailState = reader.GetString(6);
                                myHouseholdMembers.myMailZip = reader.GetString(7);
                                myHouseholdMembers.myMailCounty = reader.GetString(10);
                                if (!reader.IsDBNull(11))
                                {
                                    myHouseholdMembers.myMailAptSuite = reader.GetString(11);
                                }
                            }
                            else if (reader.GetString(9) == "Assister")
                            {
                                myAssister.myAddress1 = reader.GetString(3);
                                if (!reader.IsDBNull(4))
                                {
                                    myAssister.myAddress2 = reader.GetString(4);
                                }
                                myAssister.myCity = reader.GetString(5);
                                myAssister.myState = reader.GetString(6);
                                myAssister.myZip = reader.GetString(7);
                                myAssister.myCounty = reader.GetString(10);
                                if (!reader.IsDBNull(11))
                                {
                                    myAssister.myAptSuite = reader.GetString(11);
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

                        com4.ExecuteNonQuery();
                        com4.Dispose();
                    }

                    if (myApplication.myHouseholdOther == "Yes")
                    {
                        SqlCeCommand cmd4 = con.CreateCommand();
                        cmd4.CommandType = CommandType.Text;

                        //Read configured rows if exist, otherwise fill with default values
                        using (SqlCeCommand com5 = new SqlCeCommand("SELECT * FROM HouseMembers where TestID = " + myTestId + " and HouseMembersID = 2", con))
                        {
                            SqlCeDataReader reader = com5.ExecuteReader();
                            while (reader.Read())
                            {
                                myHouseholdMembers.myFirstName = reader.GetString(2);
                                if (!reader.IsDBNull(3))
                                {
                                    myHouseholdMembers.myMiddleName = reader.GetString(3);
                                }
                                myHouseholdMembers.myLastName = reader.GetString(4);
                                if (!reader.IsDBNull(5))
                                {
                                    myHouseholdMembers.mySuffix = reader.GetString(5);
                                }
                                myHouseholdMembers.myGender = reader.GetString(6);
                                myHouseholdMembers.myMaritalStatus = reader.GetString(7);
                                myHouseholdMembers.myDOB = reader.GetString(8);
                                myHouseholdMembers.myLiveWithYou = reader.GetString(9);
                                myHouseholdMembers.myMNHome = reader.GetString(10); //is this the same mnhome and planmakemnhome????                       
                                myHouseholdMembers.myPersonHighlighted = reader.GetString(11);
                                myHouseholdMembers.myLiveInMN = reader.GetString(12);
                                myHouseholdMembers.myTempAbsentMN = reader.GetString(13);
                                myHouseholdMembers.myHomeless = reader.GetString(14);
                                myHouseholdMembers.myPlanMakeMNHome = reader.GetString(15);
                                myHouseholdMembers.mySeekEmplMN = reader.GetString(16);
                                myHouseholdMembers.myHispanic = reader.GetString(17);
                                myHouseholdMembers.myRace = reader.GetString(18);
                                myHouseholdMembers.myHaveSSN = reader.GetString(19);
                                //myHouseholdMembers.mySSN = reader.GetString(26);//auto generated
                                myHouseholdMembers.myUSCitizen = reader.GetString(21);
                                myHouseholdMembers.myUSNational = reader.GetString(22);
                                myHouseholdMembers.myIsPregnant = reader.GetString(23);
                                myHouseholdMembers.myBeenInFosterCare = reader.GetString(24);
                                myHouseholdMembers.myRelationship = reader.GetString(25);
                                myHouseholdMembers.myHasIncome = reader.GetString(26);
                                if (!reader.IsDBNull(27))
                                {
                                    myHouseholdMembers.myRelationshiptoNextHM = reader.GetString(27);
                                }
                                if (!reader.IsDBNull(28))
                                {
                                    myHouseholdMembers.myTribeName = reader.GetString(28);
                                }
                                myHouseholdMembers.myLiveRes = reader.GetString(29);
                                if (!reader.IsDBNull(30))
                                {
                                    myHouseholdMembers.myTribeId = reader.GetString(30);
                                }
                                myHouseholdMembers.myFederalTribe = reader.GetString(31);
                                myHouseholdMembers.myFileJointly = reader.GetString(32);
                                if (!reader.IsDBNull(3))
                                {
                                    myHouseholdMembers.myIncomeType = reader.GetString(33);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    myHouseholdMembers.myIncomeEmployer = reader.GetString(34);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    myHouseholdMembers.myIncomeSeasonal = reader.GetString(35);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    myHouseholdMembers.myIncomeAmount = reader.GetString(36);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    myHouseholdMembers.myIncomeFrequency = reader.GetString(37);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    myHouseholdMembers.myIncomeMore = reader.GetString(38);
                                }
                                myHouseholdMembers.myIncomeReduced = reader.GetString(39);
                                if (!reader.IsDBNull(3))
                                {
                                    myHouseholdMembers.myIncomeAdjusted = reader.GetString(40);
                                }
                                myHouseholdMembers.myIncomeExpected = reader.GetString(41);
                                myHouseholdMembers.myPassCount = reader.GetString(42);
                                myHouseholdMembers.myMilitary = reader.GetString(43);
                                if (!reader.IsDBNull(44))
                                {
                                    myHouseholdMembers.myMilitaryDate = Convert.ToString(reader.GetDateTime(44));
                                }
                                myHouseholdMembers.myPrefContact = reader.GetString(45);
                                myHouseholdMembers.myPhoneNum = reader.GetString(46);
                                myHouseholdMembers.myPhoneType = reader.GetString(47);
                                myHouseholdMembers.myAltNum = reader.GetString(48);
                                myHouseholdMembers.myAltNumType = reader.GetString(49);
                                myHouseholdMembers.myEmail = reader.GetString(50);
                                myHouseholdMembers.myVoterCard = reader.GetString(51);
                                myHouseholdMembers.myNotices = reader.GetString(52);
                                myHouseholdMembers.myAuthRep = reader.GetString(53);
                                myHouseholdMembers.myDependants = reader.GetString(54);
                                myHouseholdMembers.myTaxFiler = reader.GetString(55);
                                if (!reader.IsDBNull(56)) { myHouseholdMembers.myChildren = reader.GetString(56); }
                                if (!reader.IsDBNull(57)) { myHouseholdMembers.myDueDate = Convert.ToString(reader.GetDateTime(57)); }
                                if (!reader.IsDBNull(58)) { myHouseholdMembers.myPregnancyEnded = Convert.ToString(reader.GetDateTime(58)); }
                                if (!reader.IsDBNull(59)) { myHouseholdMembers.myReEnroll = reader.GetString(59); }
                                if (!reader.IsDBNull(60)) { myHouseholdMembers.mySaveExit = reader.GetString(60); }
                            }
                            com5.ExecuteNonQuery();
                            com5.Dispose();
                        }
                    }

                    SqlCeCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;

                    //Read configured rows if exist
                    using (SqlCeCommand com6 = new SqlCeCommand("SELECT * FROM Assister where TestID = " + myTestId, con))
                    {
                        SqlCeDataReader reader = com6.ExecuteReader();
                        while (reader.Read())
                        {
                            myAssister.AssisterId = reader.GetString(2);
                            myAssister.myCommunication = reader.GetString(3);
                            myAssister.myLanguage = reader.GetString(4);
                            myAssister.myMethod = reader.GetString(5);
                            if (!reader.IsDBNull(6))
                            {
                                myAssister.myPhoneType = reader.GetString(6);
                            }
                            if (!reader.IsDBNull(7))
                            {
                                myAssister.myPhoneNum = reader.GetString(7);
                            }
                            myAssister.myCategory = reader.GetString(8);
                            myAssister.myType = reader.GetString(9);
                            if (!reader.IsDBNull(10))
                            {
                                myAssister.myEmail = reader.GetString(10);
                            }
                            if (!reader.IsDBNull(11))
                            {
                                myAssister.myLastName = reader.GetString(11);
                            }
                            if (!reader.IsDBNull(12))
                            {
                                myAssister.myFirstName = reader.GetString(12);
                            }
                            if (!reader.IsDBNull(13))
                            {
                                myAssister.myRefNumber = reader.GetString(13);
                            }
                            if (!reader.IsDBNull(14))
                            {
                                myAssister.mySSN = reader.GetString(14);
                            }
                            if (!reader.IsDBNull(15))
                            {
                                myAssister.myDOB = reader.GetDateTime(15).ToShortDateString();
                            }
                            if (!reader.IsDBNull(16))
                            {
                                myAssister.myRegNumber = reader.GetString(16);
                            }
                        }
                        com6.ExecuteNonQuery();
                        com6.Dispose();
                    }

                }
                catch (Exception f)
                {
                    MessageBox.Show("Did not find data for enroll " + f);
                }

                textBoxEnrollTest.Text = mySelectedTest.myTestName;
                textBoxEnrollFirstName.Text = myApplication.myFirstName;
                textBoxEnrollMiddleName.Text = myApplication.myMiddleName;
                textBoxEnrollLastName.Text = myApplication.myLastName;
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
                if (myApplication.myMilitary == "Yes")
                {
                    dateTimeMilitary.Enabled = true;
                    dateTimeMilitary.Format = DateTimePickerFormat.Short;
                }
                else
                {
                    dateTimeMilitary.Enabled = false;
                    dateTimeMilitary.Format = DateTimePickerFormat.Custom;
                    dateTimeMilitary.CustomFormat = " ";
                }
                dateTimeMilitary.Text = myApplication.myMilitaryDate;
                if (myApplication.myMilitaryDate != null && myApplication.myMilitaryDate != "")
                {
                    string tempMilitary;
                    tempMilitary = Convert.ToString(myApplication.myMilitaryDate);
                    tempMilitary = DateTime.Parse(tempMilitary).ToString("MM/dd/yyyy");
                    dateTimeMilitary.Format = DateTimePickerFormat.Short;
                    dateTimeMilitary.Value = Convert.ToDateTime(tempMilitary);
                }

                comboBoxEnrollSSN.Text = myApplication.mySSN;
                textBoxEnrollSSNNum.Text = myApplication.mySSNNum;
                comboBoxAppliedSSN.Text = myApplication.myAppliedSSN;
                comboBoxWhyNoSSN.Text = myApplication.myWhyNoSSN;
                comboBoxAssistSSN.Text = myApplication.myAssistSSN;
                comboBoxEnrollCitizen.Text = myApplication.myCitizen;
                comboBoxEnrollHouseholdOther.Text = myApplication.myHouseholdOther;
                comboBoxEnrollDependants.Text = myApplication.myDependants;
                comboBoxEnrollIncomeYN.Text = myApplication.myIncomeYN;
                comboBoxEnrollIncomeType.Text = myApplication.myIncomeType;
                textBoxEnrollIncomeEmployer.Text = myApplication.myIncomeEmployer;
                comboBoxEnrollIncomeSeasonal.Text = myApplication.myIncomeSeasonal;
                textBoxEnrollAmount.Text = myApplication.myIncomeAmount;
                comboBoxEnrollFrequency.Text = myApplication.myIncomeFrequency;
                comboBoxEnrollMoreIncome.Text = myApplication.myIncomeMore;
                comboBoxEnrollIncomeReduced.Text = myApplication.myIncomeReduced;
                comboBoxEnrollIncomeAdjustments.Text = myApplication.myIncomeAdjusted;
                comboBoxEnrollIncomeExpected.Text = myApplication.myIncomeExpected;
                textBoxEnrollFosterCare.Text = myApplication.myFosterCare;
                comboBoxOtherIns.Text = myApplication.myOtherIns;
                comboBoxKindIns.Text = myApplication.myKindIns;
                comboBoxCoverageEnd.Text = myApplication.myCoverageEnd;
                comboBoxAddIns.Text = myApplication.myAddIns;
                comboBoxESC.Text = myApplication.myESC;
                comboBoxRenewalCov.Text = myApplication.myRenewalCov;
                comboBoxWithDiscounts.Text = myApplication.myWithDiscounts;
                comboBoxPregnant.Text = myApplication.myIsPregnant;
                comboBoxChildren.Text = myApplication.myChildren;
                if (myApplication.myIsPregnant == "Yes")
                {
                    dateTimeDueDate.Enabled = true;
                    dateTimeDueDate.Format = DateTimePickerFormat.Short;
                }
                else
                {
                    dateTimeDueDate.Enabled = false;
                    dateTimeDueDate.Format = DateTimePickerFormat.Custom;
                    dateTimeDueDate.CustomFormat = " ";
                }
                if (comboBoxPregnancyDone.Text == "Yes")
                {
                    dateTimePregnancyEnded.Enabled = true;
                    dateTimePregnancyEnded.Format = DateTimePickerFormat.Short;
                }
                else
                {
                    dateTimePregnancyEnded.Enabled = false;
                    dateTimePregnancyEnded.Format = DateTimePickerFormat.Custom;
                    dateTimePregnancyEnded.CustomFormat = " ";
                }
                if (myApplication.myDueDate != null && myApplication.myDueDate != "")
                {
                    string tempDueDate;
                    tempDueDate = Convert.ToString(myApplication.myDueDate);
                    tempDueDate = DateTime.Parse(tempDueDate).ToString("MM/dd/yyyy");
                    dateTimeDueDate.Format = DateTimePickerFormat.Short;
                    dateTimeDueDate.Value = Convert.ToDateTime(tempDueDate);
                }
                if (myApplication.myPregnancyEnded != null && myApplication.myPregnancyEnded != "")
                {
                    string tempPregnancyEnded;
                    tempPregnancyEnded = Convert.ToString(myApplication.myPregnancyEnded);
                    tempPregnancyEnded = DateTime.Parse(tempPregnancyEnded).ToString("MM/dd/yyyy");
                    dateTimePregnancyEnded.Format = DateTimePickerFormat.Short;
                    dateTimePregnancyEnded.Value = Convert.ToDateTime(tempPregnancyEnded);
                    comboBoxPregnancyDone.Text = "Yes";
                }
                else
                {
                    comboBoxPregnancyDone.Text = "No";
                }
                textBoxRegDate.Text = myApplication.myRegDate;
                textBoxDay2TestId.Text = myApplication.myDay2TestId;

                if (myApplication.myHouseholdOther == "Yes")
                {
                    if (checkBoxHMRandom.Checked == false)
                    {
                        textBoxHMFirstName.Text = myHouseholdMembers.myFirstName;
                        textBoxHMMiddleName.Text = myHouseholdMembers.myMiddleName;
                        textBoxHMLastName.Text = myHouseholdMembers.myLastName;
                        comboBoxHMSuffix.Text = myHouseholdMembers.mySuffix;
                    }
                    else
                    {
                        textBoxHMFirstName.Text = "";
                        textBoxHMMiddleName.Text = "";
                        textBoxHMLastName.Text = "";
                        comboBoxHMSuffix.Text = "";
                    }
                    comboBoxHMGender.Text = myHouseholdMembers.myGender;
                    comboBoxHMMaritalStatus.Text = myHouseholdMembers.myMaritalStatus;
                    textBoxHMDOB.Text = myHouseholdMembers.myDOB;
                    comboBoxHMLiveWithYou.Text = myHouseholdMembers.myLiveWithYou;
                    comboBoxHMLiveMN.Text = myHouseholdMembers.myLiveInMN;
                    comboBoxHMTempAbsentMN.Text = myHouseholdMembers.myTempAbsentMN;
                    comboBoxHMHomeless.Text = myHouseholdMembers.myHomeless;
                    textBoxHMAddress1.Text = myHouseholdMembers.myMailAddress1;
                    textBoxHMAddress2.Text = myHouseholdMembers.myMailAddress2;
                    textBoxHMAptSuite.Text = myHouseholdMembers.myMailAptSuite;
                    textBoxHMCity.Text = myHouseholdMembers.myMailCity;
                    comboBoxHMState.Text = myHouseholdMembers.myMailState;
                    textBoxHMZip.Text = myHouseholdMembers.myMailZip;
                    comboBoxHMCounty.Text = myHouseholdMembers.myMailCounty;
                    comboBoxHMPlanToLiveInMN.Text = myHouseholdMembers.myPlanMakeMNHome;
                    comboBoxHMSeekingEmployment.Text = myHouseholdMembers.mySeekEmplMN;
                    comboBoxHMPersonHighlighted.Text = myHouseholdMembers.myPersonHighlighted;
                    comboBoxHMHispanic.Text = myHouseholdMembers.myHispanic;
                    textBoxHMTribeName.Text = myHouseholdMembers.myTribeName;
                    textBoxHMTribeId.Text = myHouseholdMembers.myTribeId;
                    comboBoxHMLiveRes.Text = myHouseholdMembers.myLiveRes;
                    comboBoxHMFederalTribe.Text = myHouseholdMembers.myFederalTribe;
                    comboBoxHMRace.Text = myHouseholdMembers.myRace;
                    comboBoxHMHaveSSN.Text = myHouseholdMembers.myHaveSSN;
                    //textBoxHMSSN.Text = myHouseholdMembers.mySSN;//auto generated
                    comboBoxHMUSCitizen.Text = myHouseholdMembers.myUSCitizen;
                    comboBoxHMUSNational.Text = myHouseholdMembers.myUSNational;
                    comboBoxHMPregnant.Text = myHouseholdMembers.myIsPregnant;
                    comboBoxHMBeenInFosterCare.Text = myHouseholdMembers.myBeenInFosterCare;
                    comboBoxHMRelationship.Text = myHouseholdMembers.myRelationship;
                    comboBoxHasIncome.Text = myHouseholdMembers.myHasIncome;
                    comboBoxHMRelationship2.Text = myHouseholdMembers.myRelationshiptoNextHM;
                    comboBoxHMFileJointly.Text = myHouseholdMembers.myFileJointly;
                    comboBoxHMIncomeType.Text = myHouseholdMembers.myIncomeType;
                    textBoxHMEmployerName.Text = myHouseholdMembers.myIncomeEmployer;
                    comboBoxHMSeasonal.Text = myHouseholdMembers.myIncomeSeasonal;
                    textBoxHMAmount.Text = myHouseholdMembers.myIncomeAmount;
                    comboBoxHMFrequency.Text = myHouseholdMembers.myIncomeFrequency;
                    comboBoxHMMoreIncome.Text = myHouseholdMembers.myIncomeMore;
                    comboBoxHMIncomeReduced.Text = myHouseholdMembers.myIncomeReduced;
                    comboBoxHMIncomeAdjustments.Text = myHouseholdMembers.myIncomeAdjusted;
                    comboBoxHMAnnualIncome.Text = myHouseholdMembers.myIncomeExpected;
                    comboBoxHMMilitary.Text = myHouseholdMembers.myMilitary;
                    if (myHouseholdMembers.myMilitary == "Yes")
                    {
                        dateTimeHMMilitary.Enabled = true;
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                    }
                    else
                    {
                        dateTimeHMMilitary.Enabled = false;
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                        dateTimeHMMilitary.CustomFormat = " ";
                    }
                    dateTimeHMMilitary.Text = myHouseholdMembers.myMilitaryDate;
                    if (myHouseholdMembers.myMilitaryDate != null && myHouseholdMembers.myMilitaryDate != " ")
                    {
                        string tempMilitary;
                        tempMilitary = Convert.ToString(myHouseholdMembers.myMilitaryDate);
                        tempMilitary = DateTime.Parse(tempMilitary).ToString("MM/dd/yyyy");
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                        dateTimeHMMilitary.Value = Convert.ToDateTime(tempMilitary);
                    }
                    comboBoxHMPrefContact.Text = myHouseholdMembers.myPrefContact;
                    textBoxHMPhoneNum.Text = myHouseholdMembers.myPhoneNum;
                    comboBoxHMPhoneType.Text = myHouseholdMembers.myPhoneType;
                    textBoxHMAltNum.Text = myHouseholdMembers.myAltNum;
                    comboBoxHMAltType.Text = myHouseholdMembers.myAltNumType;
                    textBoxHMEmail.Text = myHouseholdMembers.myEmail;
                    comboBoxHMVoterCard.Text = myHouseholdMembers.myVoterCard;
                    comboBoxHMNotices.Text = myHouseholdMembers.myNotices;
                    comboBoxHMAuthRep.Text = myHouseholdMembers.myAuthRep;
                    comboBoxHMDependant.Text = myHouseholdMembers.myDependants;
                    comboBoxHMTaxFiler.Text = myHouseholdMembers.myTaxFiler;
                    comboBoxHMChildren.Text = myHouseholdMembers.myChildren;
                    if (myHouseholdMembers.myIsPregnant == "Yes")
                    {
                        dateTimeHMDueDate.Enabled = true;
                        dateTimeHMDueDate.Format = DateTimePickerFormat.Short;
                    }
                    else
                    {
                        dateTimeHMDueDate.Enabled = false;
                        dateTimeHMDueDate.Format = DateTimePickerFormat.Custom;
                        dateTimeHMDueDate.CustomFormat = " ";
                    }
                    if (comboBoxHMPregnancyDone.Text == "Yes")
                    {
                        dateTimeHMPregnancyEnded.Enabled = true;
                        dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Short;
                    }
                    else
                    {
                        dateTimeHMPregnancyEnded.Enabled = false;
                        dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Custom;
                        dateTimeHMPregnancyEnded.CustomFormat = " ";
                    }
                    if (myHouseholdMembers.myDueDate != null && myHouseholdMembers.myDueDate != " ")
                    {
                        string tempDueDate;
                        tempDueDate = Convert.ToString(myHouseholdMembers.myDueDate);
                        tempDueDate = DateTime.Parse(tempDueDate).ToString("MM/dd/yyyy");
                        dateTimeHMDueDate.Format = DateTimePickerFormat.Short;
                        dateTimeHMDueDate.Value = Convert.ToDateTime(tempDueDate);
                    }
                    if (myHouseholdMembers.myPregnancyEnded != null && myHouseholdMembers.myPregnancyEnded != " ")
                    {
                        string tempPregnancyEnded;
                        tempPregnancyEnded = Convert.ToString(myHouseholdMembers.myPregnancyEnded);
                        tempPregnancyEnded = DateTime.Parse(tempPregnancyEnded).ToString("MM/dd/yyyy");
                        dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Short;
                        dateTimeHMPregnancyEnded.Value = Convert.ToDateTime(tempPregnancyEnded);
                    }

                    textBoxCurrentMember.Text = "2";
                    HouseholdMembersDo myHousehold = new HouseholdMembersDo();
                    int householdCount = myHousehold.DoHouseholdCount(myHistoryInfo);
                    textBoxTotalMembers.Text = Convert.ToString(householdCount);
                }
                else
                {
                    textBoxHMFirstName.Text = "";
                    textBoxHMMiddleName.Text = "";
                    textBoxHMLastName.Text = "";
                    comboBoxHMSuffix.Text = "";
                    comboBoxHMGender.Text = "";
                    comboBoxHMMaritalStatus.Text = "";
                    textBoxHMDOB.Text = "";
                    comboBoxHMLiveWithYou.Text = "";
                    comboBoxHMLiveMN.Text = "";
                    comboBoxHMTempAbsentMN.Text = "";
                    comboBoxHMHomeless.Text = "";
                    textBoxHMAddress1.Text = "";
                    textBoxHMAddress2.Text = "";
                    textBoxHMAptSuite.Text = "";
                    textBoxHMCity.Text = "";
                    comboBoxHMState.Text = "";
                    textBoxHMZip.Text = "";
                    comboBoxHMCounty.Text = "";
                    comboBoxHMPlanToLiveInMN.Text = "";
                    comboBoxHMSeekingEmployment.Text = "";
                    comboBoxHMPersonHighlighted.Text = "";
                    comboBoxHMHispanic.Text = "";
                    textBoxHMTribeName.Text = "";
                    textBoxHMTribeId.Text = "";
                    comboBoxHMLiveRes.Text = "";
                    comboBoxHMFederalTribe.Text = "";
                    comboBoxHMRace.Text = "";
                    comboBoxHMHaveSSN.Text = "";
                    comboBoxHMUSCitizen.Text = "";
                    comboBoxHMUSNational.Text = "";
                    comboBoxHMPregnant.Text = "";
                    comboBoxHMBeenInFosterCare.Text = "";
                    comboBoxHMRelationship.Text = "";
                    comboBoxHasIncome.Text = "";
                    comboBoxHMRelationship2.Text = "";
                    comboBoxHMFileJointly.Text = "";
                    comboBoxHMIncomeType.Text = "";
                    textBoxHMEmployerName.Text = "";
                    comboBoxHMSeasonal.Text = "";
                    textBoxHMAmount.Text = "";
                    comboBoxHMFrequency.Text = "";
                    comboBoxHMMoreIncome.Text = "";
                    comboBoxHMIncomeReduced.Text = "";
                    comboBoxHMIncomeAdjustments.Text = "";
                    comboBoxHMAnnualIncome.Text = "";
                    comboBoxHMMilitary.Text = "";
                    dateTimeHMMilitary.Enabled = false;
                    dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                    dateTimeHMMilitary.CustomFormat = " ";
                    comboBoxHMPrefContact.Text = "";
                    textBoxHMPhoneNum.Text = "";
                    comboBoxHMPhoneType.Text = "";
                    textBoxHMAltNum.Text = "";
                    comboBoxHMAltType.Text = "";
                    textBoxHMEmail.Text = "";
                    comboBoxHMVoterCard.Text = "";
                    comboBoxHMNotices.Text = "";
                    comboBoxHMAuthRep.Text = "";
                    comboBoxHMDependant.Text = "";
                    comboBoxHMTaxFiler.Text = "";
                    comboBoxHMChildren.Text = "";
                    dateTimeHMDueDate.Enabled = false;
                    dateTimeHMDueDate.Format = DateTimePickerFormat.Custom;
                    dateTimeHMDueDate.CustomFormat = " ";
                    dateTimeHMPregnancyEnded.Enabled = false;
                    dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Custom;
                    dateTimeHMPregnancyEnded.CustomFormat = " ";
                    textBoxCurrentMember.Text = "1";
                    textBoxTotalMembers.Text = "1";
                }

                if (myAssister.myLastName != null)
                {
                    textBoxAssisterFirstName.Text = myAssister.myFirstName;
                    textBoxAssisterLastName.Text = myAssister.myLastName;
                    textBoxAssisterDOB.Text = myAssister.myDOB;
                    comboBoxAssisterCommunication.Text = myAssister.myCommunication;
                    comboBoxAssisterLanguage.Text = myAssister.myLanguage;
                    comboBoxAssisterMethod.Text = myAssister.myMethod;
                    textBoxAssisterId.Text = Convert.ToString(myAssister.AssisterId);
                    comboBoxAssisterPhoneType.Text = myAssister.myPhoneType;
                    textBoxAssisterPhoneNumber.Text = myAssister.myPhoneNum;
                    comboBoxAssisterCategory.Text = myAssister.myCategory;
                    comboBoxAssisterType.Text = myAssister.myType;
                    textBoxAssisterStreet1.Text = myAssister.myAddress1;
                    textBoxAssisterStreet2.Text = myAssister.myAddress2;
                    textBoxAssisterAptSuite.Text = myAssister.myAptSuite;
                    textBoxAssisterCity.Text = myAssister.myCity;
                    comboBoxAssisterState.Text = myAssister.myState;
                    textBoxAssisterZip.Text = myAssister.myZip;
                    comboBoxAssisterCounty.Text = myAssister.myCounty;
                    textBoxAssisterEmail.Text = myAssister.myEmail;
                }

                groupBoxApplicantInformation.Visible = true;
                groupBoxMoreAboutYou.Visible = false;
                groupBoxHouseholdOther.Visible = false;
                groupBoxAssister.Visible = false;
                groupBoxDependants.Visible = false;
                groupBoxEnrollIncome.Visible = false;
            }
            radioButtonInformation.Checked = true;
            buttonSaveConfiguration.BackColor = Color.Yellow;
            HouseholdMembersDo myHouseholdCount = new HouseholdMembersDo();
            int householdCount2 = myHouseholdCount.DoHouseholdCount(myHistoryInfo);
            textBoxTotalMembers.Text = Convert.ToString(householdCount2);
            if (householdCount2 < 2)
            {
                buttonNextMember.Enabled = false;
                buttonPreviousMember.Enabled = false;
                textBoxCurrentMember.Text = "1";
            }
            else if (householdCount2 == 2)
            {
                buttonNextMember.Enabled = false;
                buttonPreviousMember.Enabled = false;
                textBoxCurrentMember.Text = "2";
            }
            else
            {
                buttonNextMember.Enabled = true;
                buttonPreviousMember.Enabled = false;
                textBoxCurrentMember.Text = "2";
            }
        }

        private void buttonSaveConfiguration_Click(object sender, EventArgs e)
        {
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string mysTestId;
            mysTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            if (checkBoxRandom.Checked == true)
            {
                myApplication.myFirstName = "";
                myApplication.myMiddleName = "";
                myApplication.myLastName = "";
                myApplication.mySuffix = "";
            }
            else
            {
                myApplication.myFirstName = textBoxEnrollFirstName.Text;
                myApplication.myMiddleName = textBoxEnrollMiddleName.Text;
                myApplication.myLastName = textBoxEnrollLastName.Text;
                myApplication.mySuffix = comboBoxEnrollSuffix.Text;
            }
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
            if (checkBoxRandom.Checked == true)
            {
                myApplication.myDOB = "";
            }
            else
            {
                myApplication.myDOB = textBoxEnrollDOB.Text;
            }
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
            if (dateTimeMilitary.Text != " ")
            {
                myApplication.myMilitaryDate = dateTimeMilitary.Text;
            }
            myApplication.mySSN = comboBoxEnrollSSN.Text;
            if (checkBoxRandom.Checked == true)
            {
                myApplication.mySSNNum = "";
            }
            else
            {
                myApplication.mySSNNum = textBoxEnrollSSNNum.Text;
            }
            myApplication.myAppliedSSN = comboBoxAppliedSSN.Text;
            myApplication.myWhyNoSSN = comboBoxWhyNoSSN.Text;
            myApplication.myAssistSSN = comboBoxAssistSSN.Text;
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
            myApplication.myOtherIns = comboBoxOtherIns.Text;
            myApplication.myKindIns = comboBoxKindIns.Text;
            myApplication.myCoverageEnd = comboBoxCoverageEnd.Text;
            myApplication.myAddIns = comboBoxAddIns.Text;
            myApplication.myESC = comboBoxESC.Text;
            myApplication.myRenewalCov = comboBoxRenewalCov.Text;
            myApplication.myWithDiscounts = comboBoxWithDiscounts.Text;
            myApplication.myIsPregnant = comboBoxPregnant.Text;
            myApplication.myChildren = comboBoxChildren.Text;
            if (dateTimeDueDate.Text != " ")
            {
                myApplication.myDueDate = dateTimeDueDate.Text;
            }
            if (dateTimePregnancyEnded.Text != " ")
            {
                myApplication.myPregnancyEnded = dateTimePregnancyEnded.Text;
            }
            myApplication.myRegDate = textBoxRegDate.Text;
            myApplication.myDay2TestId = textBoxDay2TestId.Text;
            myApplication.myPassCount = "1";

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
                                    "@LiveRes, @TribeId, @FederalTribe, @Military, @MilitaryDate, @AppliedSSN, @WhyNoSSN, @AssistSSN, @OtherIns," +
                                    "@KindIns, @CoverageEnd, @AddIns, @ESC, @RenewalCov, @WithDiscounts, @Pregnant, @Children, @DueDate, @PregnancyEnded, @RegDate, @Day2TestId, @PassCount );";
                using (SqlCeCommand com6 = new SqlCeCommand(myInsertString, con))
                {
                    com6.Parameters.AddWithValue("FirstName", myApplication.myFirstName);
                    com6.Parameters.AddWithValue("MiddleName", myApplication.myMiddleName);
                    com6.Parameters.AddWithValue("LastName", myApplication.myLastName);
                    com6.Parameters.AddWithValue("Suffix", myApplication.mySuffix);
                    com6.Parameters.AddWithValue("Gender", myApplication.myGender);
                    com6.Parameters.AddWithValue("MaritalStatus", myApplication.myMaritalStatus);
                    if (myApplication.myDOB != "")
                    {
                        com6.Parameters.AddWithValue("DOB", myApplication.myDOB);
                    }
                    else
                    {
                        myApplication.myDOB = "01/01/2011"; // special situation
                        com6.Parameters.AddWithValue("DOB", myApplication.myDOB);
                    }
                    com6.Parameters.AddWithValue("LiveMN", myApplication.myLiveMN);
                    com6.Parameters.AddWithValue("PlanLiveMN", myApplication.myPlanLiveMN);
                    com6.Parameters.AddWithValue("PrefContact", myApplication.myPrefContact);
                    com6.Parameters.AddWithValue("PhoneNum", myApplication.myPhoneNum);
                    com6.Parameters.AddWithValue("PhoneType", myApplication.myPhoneType);
                    com6.Parameters.AddWithValue("AltNum", myApplication.myAltNum);
                    com6.Parameters.AddWithValue("AltType", myApplication.myAltNumType);
                    com6.Parameters.AddWithValue("Email", myApplication.myEmail);
                    com6.Parameters.AddWithValue("LanguageMost", myApplication.myLanguageMost);
                    com6.Parameters.AddWithValue("WrittenLanguage", myApplication.myLanguageWritten);
                    com6.Parameters.AddWithValue("VoterCard", myApplication.myVoterCard);
                    com6.Parameters.AddWithValue("Notices", myApplication.myNotices);
                    com6.Parameters.AddWithValue("AuthRep", myApplication.myAuthRep);
                    com6.Parameters.AddWithValue("ApplyYourself", myApplication.myApplyYourself);
                    com6.Parameters.AddWithValue("Homeless", myApplication.myHomeless);
                    com6.Parameters.AddWithValue("AddressSame", myApplication.myAddressSame);
                    com6.Parameters.AddWithValue("Hispanic", myApplication.myHispanic);
                    com6.Parameters.AddWithValue("Race", myApplication.myRace);
                    com6.Parameters.AddWithValue("SSN", myApplication.mySSN);
                    com6.Parameters.AddWithValue("Citizen", myApplication.myCitizen);
                    com6.Parameters.AddWithValue("SSNNum", myApplication.mySSNNum);
                    com6.Parameters.AddWithValue("AppliedSSN", myApplication.myAppliedSSN);
                    if (myApplication.myWhyNoSSN != null)
                    {
                        com6.Parameters.AddWithValue("WhyNoSSN", myApplication.myWhyNoSSN);
                    }
                    else
                    {
                        com6.Parameters.AddWithValue("WhyNoSSN", DBNull.Value);
                    }
                    com6.Parameters.AddWithValue("AssistSSN", myApplication.myAssistSSN);
                    com6.Parameters.AddWithValue("Household", myApplication.myHouseholdOther);
                    com6.Parameters.AddWithValue("Dependants", myApplication.myDependants);
                    com6.Parameters.AddWithValue("IncomeYN", myApplication.myIncomeYN);
                    com6.Parameters.AddWithValue("IncomeType", myApplication.myIncomeType);
                    com6.Parameters.AddWithValue("IncomeAmount", myApplication.myIncomeAmount);
                    com6.Parameters.AddWithValue("IncomeFrequency", myApplication.myIncomeFrequency);
                    com6.Parameters.AddWithValue("IncomeMore", myApplication.myIncomeMore);
                    com6.Parameters.AddWithValue("Employer", myApplication.myIncomeEmployer);
                    com6.Parameters.AddWithValue("Seasonal", myApplication.myIncomeSeasonal);
                    com6.Parameters.AddWithValue("Reduced", myApplication.myIncomeReduced);
                    com6.Parameters.AddWithValue("Adjusted", myApplication.myIncomeAdjusted);
                    com6.Parameters.AddWithValue("Expected", myApplication.myIncomeExpected);
                    com6.Parameters.AddWithValue("PlanType", myApplication.myEnrollmentPlanType);
                    com6.Parameters.AddWithValue("Foster", myApplication.myFosterCare);
                    com6.Parameters.AddWithValue("MailAddrYN", myApplication.myMailingAddressYN);
                    com6.Parameters.AddWithValue("TribeName", myApplication.myTribeName);
                    com6.Parameters.AddWithValue("LiveRes", myApplication.myLiveRes);
                    com6.Parameters.AddWithValue("TribeId", myApplication.myTribeId);
                    com6.Parameters.AddWithValue("FederalTribe", myApplication.myFederalTribe);
                    com6.Parameters.AddWithValue("Military", myApplication.myMilitary);
                    if (myApplication.myMilitaryDate != "" && myApplication.myMilitaryDate != null)
                    {
                        com6.Parameters.AddWithValue("MilitaryDate", myApplication.myMilitaryDate);
                    }
                    else
                    {
                        com6.Parameters.AddWithValue("MilitaryDate", DBNull.Value);
                    }
                    com6.Parameters.AddWithValue("OtherIns", myApplication.myOtherIns);
                    com6.Parameters.AddWithValue("KindIns", myApplication.myKindIns);
                    com6.Parameters.AddWithValue("CoverageEnd", myApplication.myCoverageEnd);
                    com6.Parameters.AddWithValue("AddIns", myApplication.myAddIns);
                    com6.Parameters.AddWithValue("ESC", myApplication.myESC);
                    com6.Parameters.AddWithValue("RenewalCov", myApplication.myRenewalCov);
                    com6.Parameters.AddWithValue("WithDiscounts", myApplication.myWithDiscounts);
                    com6.Parameters.AddWithValue("Pregnant", myApplication.myIsPregnant);
                    com6.Parameters.AddWithValue("Children", myApplication.myChildren);
                    if (myApplication.myDueDate != "" && myApplication.myDueDate != null)
                    {
                        com6.Parameters.AddWithValue("DueDate", myApplication.myDueDate);
                    }
                    else
                    {
                        com6.Parameters.AddWithValue("DueDate", DBNull.Value);
                    }
                    if (myApplication.myPregnancyEnded != "" && myApplication.myPregnancyEnded != null)
                    {
                        com6.Parameters.AddWithValue("PregnancyEnded", myApplication.myPregnancyEnded);
                    }
                    else
                    {
                        com6.Parameters.AddWithValue("PregnancyEnded", DBNull.Value);
                    }
                    if (myApplication.myRegDate != "" && myApplication.myRegDate != null)
                    {
                        com6.Parameters.AddWithValue("RegDate", myApplication.myRegDate);
                    }
                    else
                    {
                        com6.Parameters.AddWithValue("RegDate", DBNull.Value);
                    }
                    if (myApplication.myDay2TestId != "" && myApplication.myDay2TestId != null)
                    {
                        com6.Parameters.AddWithValue("Day2TestId", myApplication.myDay2TestId);
                    }
                    else
                    {
                        com6.Parameters.AddWithValue("Day2TestId", DBNull.Value);
                    }
                    com6.Parameters.AddWithValue("PassCount", myApplication.myPassCount);

                    com6.ExecuteNonQuery();
                    com6.Dispose();
                }

                using (SqlCeCommand com7 = new SqlCeCommand("SELECT max(AddressId) FROM Address", con))
                {
                    SqlCeDataReader reader = com7.ExecuteReader();
                    if (reader.Read())
                    {
                        myEditKey.myNextAddressId = Convert.ToString(reader.GetInt32(0) + 1);
                    }
                    else
                    {
                        MessageBox.Show("Did not find Address id");
                    }
                    com7.Dispose();
                }

                SqlCeCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                try
                {
                    cmd4.CommandText = "Delete from Address where TestId = " + mysTestId + " and (Type = 'Home' or Type = 'Mailing' or Type = 'Assister')" + ";";
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
                using (SqlCeCommand com8 = new SqlCeCommand(myInsertString2, con))
                {
                    com8.Parameters.AddWithValue("AddressId", myEditKey.myNextAddressId);
                    com8.Parameters.AddWithValue("Address1", myApplication.myHomeAddress1);
                    if (myApplication.myHomeAddress2 != "")
                    {
                        com8.Parameters.AddWithValue("Address2", myApplication.myHomeAddress2);
                    }
                    else
                    {
                        com8.Parameters.AddWithValue("Address2", DBNull.Value);
                    }
                    com8.Parameters.AddWithValue("City", myApplication.myHomeCity);
                    com8.Parameters.AddWithValue("State", myApplication.myHomeState);
                    com8.Parameters.AddWithValue("Zip", myApplication.myHomeZip);
                    if (myApplication.myHomeZip4 != "")
                    {
                        com8.Parameters.AddWithValue("Zip4", myApplication.myHomeZip4);
                    }
                    else
                    {
                        com8.Parameters.AddWithValue("Zip4", DBNull.Value);
                    }
                    com8.Parameters.AddWithValue("Type", "Home");
                    com8.Parameters.AddWithValue("County", myApplication.myHomeCounty);
                    if (myApplication.myHomeAptSuite != "")
                    {
                        com8.Parameters.AddWithValue("AptSuite", myApplication.myHomeAptSuite);
                    }
                    else
                    {
                        com8.Parameters.AddWithValue("AptSuite", DBNull.Value);
                    }

                    com8.ExecuteNonQuery();
                    com8.Dispose();
                }

                if (myApplication.myMailAddress1 != "")
                {
                    string myInsertString3;
                    myInsertString3 = "Insert into Address values (" + 1 + ", " + mysTestId +
                                    ", @AddressId, @Address1, @Address2, @City, @State, @Zip, @Zip4, @Type, @County, @AptSuite );";
                    using (SqlCeCommand com9 = new SqlCeCommand(myInsertString3, con))
                    {
                        myEditKey.myNextAddressId = Convert.ToString(Convert.ToInt32(myEditKey.myNextAddressId) + 1);

                        com9.Parameters.AddWithValue("AddressId", myEditKey.myNextAddressId);
                        com9.Parameters.AddWithValue("Address1", myApplication.myMailAddress1);
                        if (myApplication.myMailAddress2 != "")
                        {
                            com9.Parameters.AddWithValue("Address2", myApplication.myMailAddress2);
                        }
                        else
                        {
                            com9.Parameters.AddWithValue("Address2", DBNull.Value);
                        }
                        com9.Parameters.AddWithValue("City", myApplication.myMailCity);
                        com9.Parameters.AddWithValue("State", myApplication.myMailState);
                        com9.Parameters.AddWithValue("Zip", myApplication.myMailZip);
                        if (myApplication.myMailZip4 != "")
                        {
                            com9.Parameters.AddWithValue("Zip4", myApplication.myMailZip4);
                        }
                        else
                        {
                            com9.Parameters.AddWithValue("Zip4", DBNull.Value);
                        }
                        com9.Parameters.AddWithValue("Type", "Mailing");
                        com9.Parameters.AddWithValue("County", myApplication.myMailCounty);
                        if (myApplication.myMailAptSuite != "")
                        {
                            com9.Parameters.AddWithValue("AptSuite", myApplication.myMailAptSuite);
                        }
                        else
                        {
                            com9.Parameters.AddWithValue("AptSuite", DBNull.Value);
                        }

                        com9.ExecuteNonQuery();
                        com9.Dispose();
                    }
                }

                if (textBoxAssisterLastName.Text != null && textBoxAssisterLastName.Text != "")
                {
                    myAssister.myFirstName = textBoxAssisterFirstName.Text;
                    myAssister.myLastName = textBoxAssisterLastName.Text;
                    myAssister.myDOB = textBoxAssisterDOB.Text;
                    myAssister.myCommunication = comboBoxAssisterCommunication.Text;
                    myAssister.myLanguage = comboBoxAssisterLanguage.Text;
                    myAssister.myMethod = comboBoxAssisterMethod.Text;
                    myAssister.AssisterId = textBoxAssisterId.Text;
                    myAssister.myPhoneType = comboBoxAssisterPhoneType.Text;
                    myAssister.myPhoneNum = textBoxAssisterPhoneNumber.Text;
                    myAssister.myCategory = comboBoxAssisterCategory.Text;
                    myAssister.myType = comboBoxAssisterType.Text;
                    myAssister.myAddress1 = textBoxAssisterStreet1.Text;
                    myAssister.myAddress2 = textBoxAssisterStreet2.Text;
                    myAssister.myAptSuite = textBoxAssisterAptSuite.Text;
                    myAssister.myCity = textBoxAssisterCity.Text;
                    myAssister.myState = comboBoxAssisterState.Text;
                    myAssister.myZip = textBoxAssisterZip.Text;
                    myAssister.myCounty = comboBoxAssisterCounty.Text;
                    myAssister.myEmail = textBoxAssisterEmail.Text;

                    SqlCeCommand cmd5 = con.CreateCommand();
                    cmd5.CommandType = CommandType.Text;
                    try
                    {
                        cmd5.CommandText = "Delete from Assister where TestId = " + mysTestId + ";";
                        cmd5.ExecuteNonQuery();
                    }
                    catch
                    {
                        //fail silently
                    }

                    using (SqlCeCommand com8 = new SqlCeCommand("SELECT max(Id) FROM Assister", con))
                    {
                        SqlCeDataReader reader = com8.ExecuteReader();
                        if (reader.Read())
                        {
                            myEditKey.myNextAssisterId = Convert.ToString(reader.GetInt32(0) + 1);
                        }
                        else
                        {
                            MessageBox.Show("Did not find Address id");
                        }
                        com8.Dispose();
                    }

                    string myInsertString4;
                    myInsertString4 = "Insert into Assister values (@Id," + mysTestId +
                                    ", @AssisterId, @Communication, @Language, @Method, @PhoneType, @PhoneNum, @Category, @Type, @Email, @LastName, @FirstName, @RefNumber, @SSN, @DOB, @RegNumber );";
                    using (SqlCeCommand com10 = new SqlCeCommand(myInsertString4, con))
                    {
                        com10.Parameters.AddWithValue("Id", myEditKey.myNextAssisterId);
                        com10.Parameters.AddWithValue("AssisterId", myAssister.AssisterId);
                        com10.Parameters.AddWithValue("Communication", myAssister.myCommunication);
                        com10.Parameters.AddWithValue("Language", myAssister.myLanguage);
                        com10.Parameters.AddWithValue("Method", myAssister.myMethod);
                        if (myAssister.myPhoneType != "")
                        {
                            com10.Parameters.AddWithValue("PhoneType", myAssister.myPhoneType);
                        }
                        else
                        {
                            com10.Parameters.AddWithValue("PhoneType", DBNull.Value);
                        }
                        if (myAssister.myPhoneNum != "")
                        {
                            com10.Parameters.AddWithValue("PhoneNum", myAssister.myPhoneNum);
                        }
                        else
                        {
                            com10.Parameters.AddWithValue("PhoneNum", DBNull.Value);
                        }
                        com10.Parameters.AddWithValue("Category", myAssister.myCategory);
                        com10.Parameters.AddWithValue("Type", myAssister.myType);
                        com10.Parameters.AddWithValue("Email", myAssister.myEmail);
                        com10.Parameters.AddWithValue("LastName", myAssister.myLastName);
                        com10.Parameters.AddWithValue("FirstName", myAssister.myFirstName);
                        com10.Parameters.AddWithValue("RefNumber", DBNull.Value);
                        com10.Parameters.AddWithValue("SSN", DBNull.Value);
                        com10.Parameters.AddWithValue("DOB", myAssister.myDOB);
                        com10.Parameters.AddWithValue("RegNumber", DBNull.Value);

                        com10.ExecuteNonQuery();
                        com10.Dispose();
                    }

                    string myInsertString5;
                    myInsertString5 = "Insert into Address values (" + 1 + ", " + mysTestId +
                                    ", @AddressId, @Address1, @Address2, @City, @State, @Zip, @Zip4, @Type, @County, @AptSuite );";
                    using (SqlCeCommand com11 = new SqlCeCommand(myInsertString5, con))
                    {
                        myEditKey.myNextAddressId = Convert.ToString(Convert.ToInt32(myEditKey.myNextAddressId) + 1);

                        com11.Parameters.AddWithValue("AddressId", myEditKey.myNextAddressId);
                        com11.Parameters.AddWithValue("Address1", myAssister.myAddress1);
                        if (myAssister.myAddress2 != "")
                        {
                            com11.Parameters.AddWithValue("Address2", myAssister.myAddress2);
                        }
                        else
                        {
                            com11.Parameters.AddWithValue("Address2", DBNull.Value);
                        }
                        com11.Parameters.AddWithValue("City", myAssister.myCity);
                        com11.Parameters.AddWithValue("State", myAssister.myState);
                        com11.Parameters.AddWithValue("Zip", myAssister.myZip);
                        com11.Parameters.AddWithValue("Zip4", DBNull.Value);
                        com11.Parameters.AddWithValue("Type", "Assister");
                        com11.Parameters.AddWithValue("County", myAssister.myCounty);
                        if (myAssister.myAptSuite != "")
                        {
                            com11.Parameters.AddWithValue("AptSuite", myAssister.myAptSuite);
                        }
                        else
                        {
                            com11.Parameters.AddWithValue("AptSuite", DBNull.Value);
                        }

                        com11.ExecuteNonQuery();
                        com11.Dispose();
                    }
                }
                else
                {
                    textBoxAssisterFirstName.Text = "";
                    comboBoxAssisterCommunication.Text = "";
                    comboBoxAssisterLanguage.Text = "";
                    comboBoxAssisterMethod.Text = "";
                    textBoxAssisterId.Text = "";
                    comboBoxAssisterPhoneType.Text = "";
                    textBoxAssisterPhoneNumber.Text = "";
                    comboBoxAssisterCategory.Text = "";
                    comboBoxAssisterType.Text = "";
                    textBoxAssisterStreet1.Text = "";
                    textBoxAssisterStreet2.Text = "";
                    textBoxAssisterAptSuite.Text = "";
                    textBoxAssisterCity.Text = "";
                    comboBoxAssisterState.Text = "";
                    textBoxAssisterZip.Text = "";
                    comboBoxAssisterCounty.Text = "";
                    textBoxAssisterEmail.Text = "";
                }

            }
            catch (Exception f)
            {
                MessageBox.Show("Error Exception: " + f);
            }

            dataGridViewSelectedTests.Rows[mySelectedTest.myRowIndex].Cells[1].Style.BackColor = Color.Beige;
            buttonSaveConfiguration.BackColor = Color.Beige;
        }

        private void radioButtonMore_Click(object sender, EventArgs e)
        {
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Location = new System.Drawing.Point(18, 40);
            groupBoxMoreAboutYou.Visible = true;
            groupBoxDependants.Visible = false;
            groupBoxHouseholdOther.Visible = false;
            groupBoxEnrollIncome.Visible = false;
            groupBoxAssister.Visible = false;
        }

        private void radioButtonInformation_Click(object sender, EventArgs e)
        {
            groupBoxApplicantInformation.Location = new System.Drawing.Point(18, 40);
            groupBoxApplicantInformation.Visible = true;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxDependants.Visible = false;
            groupBoxHouseholdOther.Visible = false;
            groupBoxEnrollIncome.Visible = false;
            groupBoxAssister.Visible = false;
        }

        private void radioButtonHouseholdOther_Click(object sender, EventArgs e)
        {
            groupBoxHouseholdOther.Location = new System.Drawing.Point(18, 40);
            groupBoxHouseholdOther.Visible = true;
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxDependants.Visible = false;
            groupBoxEnrollIncome.Visible = false;
            groupBoxAssister.Visible = false;
        }

        private void radioButtonEnrollDependants_Click(object sender, EventArgs e)
        {
            groupBoxHouseholdOther.Visible = false;
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxDependants.Location = new System.Drawing.Point(18, 40);
            groupBoxDependants.Visible = true;
            groupBoxEnrollIncome.Visible = false;
            groupBoxAssister.Visible = false;
        }

        private void radioButtonIncome_Click(object sender, EventArgs e)
        {
            groupBoxHouseholdOther.Visible = false;
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxDependants.Visible = false;
            groupBoxEnrollIncome.Location = new System.Drawing.Point(18, 40);
            groupBoxEnrollIncome.Visible = true;
            groupBoxAssister.Visible = false;

        }

        private void radioButtonAssister_Click(object sender, EventArgs e)
        {
            groupBoxHouseholdOther.Visible = false;
            groupBoxApplicantInformation.Visible = false;
            groupBoxMoreAboutYou.Visible = false;
            groupBoxAssister.Location = new System.Drawing.Point(18, 40);
            groupBoxAssister.Visible = true;
            groupBoxEnrollIncome.Visible = false;
            groupBoxDependants.Visible = false;
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
                using (SqlCeCommand com10 = new SqlCeCommand(
                    "SELECT * FROM Test where TestId = " + mysTestId + " and IsSelected = 'Yes';", con))
                {
                    SqlCeDataReader reader = com10.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Test Already exists in Regression");
                    }
                    else
                    {
                        string myUpdateString;
                        myUpdateString = "Update Test set IsSelected = 'Yes' where TestId = " + mysTestId + ";";

                        using (SqlCeCommand com11 = new SqlCeCommand(myUpdateString, con))
                        {
                            com11.ExecuteNonQuery();
                            com11.Dispose();
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
            buttonGo.Enabled = true;
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
                using (SqlCeCommand com12 = new SqlCeCommand(myUpdateString, con))
                {
                    com12.ExecuteNonQuery();
                    com12.Dispose();
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Remove Test didn't work");
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
            int numRowsCount = dataGridViewSelectedTests.RowCount;
            if (numRowsCount == 1)
            {
                buttonGo.Enabled = false;
            }
            else
            {
                buttonGo.Enabled = true;
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

            int numRowsCount = dataGridViewSelectedTests.RowCount;
            if (numRowsCount == 1)
            {
                buttonGo.Enabled = false;
            }
            else
            {
                buttonGo.Enabled = true;
            }

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
            textBoxMNSureBuild.Text = "16.3";
            myHistoryInfo.myMnsureBuild = textBoxMNSureBuild.Text;
            myHistoryInfo.myCitizenWait = 20;
            myHistoryInfo.myCaseWorkerWait = 20;
            myHistoryInfo.myAppWait = 0;
            comboBoxAppWait.Text = "0";
            myHistoryInfo.myAppWait = Convert.ToInt32(comboBoxAppWait.Text);
            myHistoryInfo.myEnvironment = "STST";
            comboBoxEnvironment.Text = "STST";
            myHistoryInfo.myBrowser = "Firefox";
            comboBoxBrowser.Text = "Firefox";
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
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string myTestId;
            myTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
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
                    using (SqlCeCommand com13 = new SqlCeCommand("SELECT Count(*) FROM HouseMembers where TestId = " + "'" + myTestId + "'", con))
                    {
                        SqlCeDataReader reader = com13.ExecuteReader();
                        if (reader.Read())
                        {
                            myHouseholdMembers.NumMembers = reader.GetInt32(0);
                            textBoxCurrentMember.Text = "2";
                            textBoxTotalMembers.Text = Convert.ToString(myHouseholdMembers.NumMembers + 1);
                        }
                        else
                        {
                            myHouseholdMembers.HouseMembersID = 0;
                            textBoxTotalMembers.Text = "2";
                        }
                        com13.ExecuteNonQuery();
                        com13.Dispose();
                    }
                    using (SqlCeCommand com14 = new SqlCeCommand("SELECT Min(HouseMembersID) FROM HouseMembers where TestId = " + "'" + myTestId + "'", con))
                    {
                        SqlCeDataReader reader = com14.ExecuteReader();
                        if (reader.Read())
                        {
                            myHouseholdMembers.HouseMembersID = reader.GetInt32(0);
                            textBoxCurrentMember.Text = "2";
                        }
                        else
                        {
                            myHouseholdMembers.HouseMembersID = 0;
                        }
                        com14.ExecuteNonQuery();
                        com14.Dispose();
                    }
                }
                catch
                {
                    //Fail silently
                    myHouseholdMembers.HouseMembersID = 0;
                    textBoxTotalMembers.Text = "2";
                }
                if (myHouseholdMembers.HouseMembersID == 0)
                {
                    buttonSaveMember.BackColor = Color.Yellow;
                    textBoxTotalMembers.Text = "2";
                }
                else
                {
                    FillStructures householdMembers = new FillStructures();
                    result = householdMembers.doGetHouseholdMember(ref myHouseholdMembers, ref myHistoryInfo, myTestId);
                    buttonSaveMember.BackColor = Color.Transparent;
                }

                if (myHouseholdMembers.HouseMembersID > 0)
                {
                    textBoxHMFirstName.Text = myHouseholdMembers.myFirstName;
                    textBoxHMMiddleName.Text = myHouseholdMembers.myMiddleName;
                    textBoxHMLastName.Text = myHouseholdMembers.myLastName;
                    comboBoxHMSuffix.Text = myHouseholdMembers.mySuffix;
                    comboBoxHMGender.Text = myHouseholdMembers.myGender;
                    comboBoxHMMaritalStatus.Text = myHouseholdMembers.myMaritalStatus;
                    textBoxHMDOB.Text = myHouseholdMembers.myDOB;
                    comboBoxHMLiveWithYou.Text = myHouseholdMembers.myLiveWithYou;
                    comboBoxHMLiveMN.Text = myHouseholdMembers.myLiveInMN;
                    comboBoxHMTempAbsentMN.Text = myHouseholdMembers.myTempAbsentMN;
                    comboBoxHMHomeless.Text = myHouseholdMembers.myHomeless;
                    textBoxHMAddress1.Text = myHouseholdMembers.myMailAddress1;
                    textBoxHMAddress2.Text = myHouseholdMembers.myMailAddress2;
                    textBoxHMAptSuite.Text = myHouseholdMembers.myMailAptSuite;
                    textBoxHMCity.Text = myHouseholdMembers.myMailCity;
                    comboBoxHMState.Text = myHouseholdMembers.myMailState;
                    textBoxHMZip.Text = myHouseholdMembers.myMailZip;
                    comboBoxHMCounty.Text = myHouseholdMembers.myMailCounty;
                    comboBoxHMPlanToLiveInMN.Text = myHouseholdMembers.myPlanMakeMNHome;
                    comboBoxHMSeekingEmployment.Text = myHouseholdMembers.mySeekEmplMN;
                    comboBoxHMPersonHighlighted.Text = myHouseholdMembers.myPersonHighlighted;
                    comboBoxHMHispanic.Text = myHouseholdMembers.myHispanic;
                    textBoxHMTribeName.Text = myHouseholdMembers.myTribeName;
                    textBoxHMTribeId.Text = myHouseholdMembers.myTribeId;
                    comboBoxHMLiveRes.Text = myHouseholdMembers.myLiveRes;
                    comboBoxHMFederalTribe.Text = myHouseholdMembers.myFederalTribe;
                    comboBoxHMRace.Text = myHouseholdMembers.myRace;
                    comboBoxHMHaveSSN.Text = myHouseholdMembers.myHaveSSN;
                    //textBoxHMSSN.Text = myHouseholdMembers.mySSN;//auto generated
                    comboBoxHMUSCitizen.Text = myHouseholdMembers.myUSCitizen;
                    comboBoxHMUSNational.Text = myHouseholdMembers.myUSNational;
                    comboBoxHMPregnant.Text = myHouseholdMembers.myIsPregnant;
                    comboBoxHMBeenInFosterCare.Text = myHouseholdMembers.myBeenInFosterCare;
                    comboBoxHMRelationship.Text = myHouseholdMembers.myRelationship;
                    comboBoxHasIncome.Text = myHouseholdMembers.myHasIncome;
                    comboBoxHMRelationship2.Text = myHouseholdMembers.myRelationshiptoNextHM;
                    comboBoxHMFileJointly.Text = myHouseholdMembers.myFileJointly;
                    comboBoxHMIncomeType.Text = myHouseholdMembers.myIncomeType;
                    textBoxHMEmployerName.Text = myHouseholdMembers.myIncomeEmployer;
                    comboBoxHMSeasonal.Text = myHouseholdMembers.myIncomeSeasonal;
                    textBoxHMAmount.Text = myHouseholdMembers.myIncomeAmount;
                    comboBoxHMFrequency.Text = myHouseholdMembers.myIncomeFrequency;
                    comboBoxHMMoreIncome.Text = myHouseholdMembers.myIncomeMore;
                    comboBoxHMIncomeReduced.Text = myHouseholdMembers.myIncomeReduced;
                    comboBoxHMIncomeAdjustments.Text = myHouseholdMembers.myIncomeAdjusted;
                    comboBoxHMAnnualIncome.Text = myHouseholdMembers.myIncomeExpected;
                    comboBoxHMMilitary.Text = myHouseholdMembers.myMilitary;
                    if (myHouseholdMembers.myMilitary == "Yes")
                    {
                        dateTimeHMMilitary.Enabled = true;
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                    }
                    else
                    {
                        dateTimeHMMilitary.Enabled = false;
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                        dateTimeHMMilitary.CustomFormat = " ";
                    }
                    dateTimeHMMilitary.Text = myHouseholdMembers.myMilitaryDate;
                    if (myHouseholdMembers.myMilitaryDate != null && myHouseholdMembers.myMilitaryDate != " ")
                    {
                        string tempMilitary;
                        tempMilitary = Convert.ToString(myHouseholdMembers.myMilitaryDate);
                        tempMilitary = DateTime.Parse(tempMilitary).ToString("MM/dd/yyyy");
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                        dateTimeHMMilitary.Value = Convert.ToDateTime(tempMilitary);
                    }
                    comboBoxHMPrefContact.Text = myHouseholdMembers.myPrefContact;
                    textBoxHMPhoneNum.Text = myHouseholdMembers.myPhoneNum;
                    comboBoxHMPhoneType.Text = myHouseholdMembers.myPhoneType;
                    textBoxHMAltNum.Text = myHouseholdMembers.myAltNum;
                    comboBoxHMAltType.Text = myHouseholdMembers.myAltNumType;
                    textBoxHMEmail.Text = myHouseholdMembers.myEmail;
                    comboBoxHMVoterCard.Text = myHouseholdMembers.myVoterCard;
                    comboBoxHMNotices.Text = myHouseholdMembers.myNotices;
                    comboBoxHMAuthRep.Text = myHouseholdMembers.myAuthRep;
                    comboBoxHMDependant.Text = myHouseholdMembers.myDependants;
                    comboBoxHMTaxFiler.Text = myHouseholdMembers.myTaxFiler;
                    comboBoxHMChildren.Text = myHouseholdMembers.myChildren;
                    if (myHouseholdMembers.myDueDate != null && myHouseholdMembers.myDueDate != " ")
                    {
                        string tempDueDate;
                        tempDueDate = Convert.ToString(myHouseholdMembers.myDueDate);
                        tempDueDate = DateTime.Parse(tempDueDate).ToString("MM/dd/yyyy");
                        dateTimeHMDueDate.Format = DateTimePickerFormat.Short;
                        dateTimeHMDueDate.Value = Convert.ToDateTime(tempDueDate);
                    }
                    if (myHouseholdMembers.myPregnancyEnded != null && myHouseholdMembers.myPregnancyEnded != " ")
                    {
                        string tempPregnancyEnded;
                        tempPregnancyEnded = Convert.ToString(myHouseholdMembers.myPregnancyEnded);
                        tempPregnancyEnded = DateTime.Parse(tempPregnancyEnded).ToString("MM/dd/yyyy");
                        dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Short;
                        dateTimeHMPregnancyEnded.Value = Convert.ToDateTime(tempPregnancyEnded);
                    }

                    textBoxCurrentMember.Text = "2";
                }
                else
                {
                    //default values
                    checkBoxHMRandom.Checked = true;
                    textBoxHMFirstName.Text = "";
                    textBoxHMMiddleName.Text = "";
                    textBoxHMLastName.Text = "";
                    comboBoxHMSuffix.Text = "";
                    comboBoxHMGender.Text = "";
                    textBoxHMDOB.Text = "";
                    comboBoxHMMaritalStatus.Text = "Married";
                    comboBoxHMLiveWithYou.Text = "Yes";
                    comboBoxHMLiveMN.Text = "Yes";
                    comboBoxHMTempAbsentMN.Text = "No";
                    comboBoxHMHomeless.Text = "No";
                    textBoxHMAddress1.Text = "";
                    textBoxHMAddress2.Text = "";
                    textBoxHMAptSuite.Text = "";
                    textBoxHMCity.Text = "";
                    comboBoxHMState.Text = "";
                    textBoxHMZip.Text = "";
                    comboBoxHMCounty.Text = "";
                    comboBoxHMPlanToLiveInMN.Text = "Yes";
                    comboBoxHMSeekingEmployment.Text = "No";
                    comboBoxHMPersonHighlighted.Text = "Yes";
                    comboBoxHMHispanic.Text = "No";
                    textBoxHMTribeName.Text = "";
                    textBoxHMTribeId.Text = "";
                    comboBoxHMLiveRes.Text = "No";
                    comboBoxHMFederalTribe.Text = "No";
                    comboBoxHMRace.Text = "White";
                    comboBoxHMHaveSSN.Text = "Yes";
                    //textBoxHMSSN.Text = myHouseholdMembers.mySSN;
                    comboBoxHMUSCitizen.Text = "Yes";
                    comboBoxHMUSNational.Text = "No";
                    comboBoxHMPregnant.Text = "No";
                    comboBoxHMBeenInFosterCare.Text = "No";
                    comboBoxHMRelationship.Text = "Is the Spouse of";
                    comboBoxHasIncome.Text = "No";
                    comboBoxHMRelationship2.Text = "";
                    comboBoxHMFileJointly.Text = "Yes";
                    comboBoxHMIncomeType.Text = "";
                    textBoxHMEmployerName.Text = "";
                    comboBoxHMSeasonal.Text = "No";
                    textBoxHMAmount.Text = "";
                    comboBoxHMFrequency.Text = "";
                    comboBoxHMMoreIncome.Text = "No";
                    comboBoxHMIncomeReduced.Text = "No";
                    comboBoxHMIncomeAdjustments.Text = "No";
                    comboBoxHMAnnualIncome.Text = "Yes";
                    comboBoxHMMilitary.Text = "No";
                    if (myHouseholdMembers.myMilitary == "Yes")
                    {
                        dateTimeHMMilitary.Enabled = true;
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                    }
                    else
                    {
                        dateTimeHMMilitary.Enabled = false;
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                        dateTimeHMMilitary.CustomFormat = " ";
                    }
                    if (myHouseholdMembers.myMilitaryDate != null && myHouseholdMembers.myMilitaryDate != " ")
                    {
                        string tempMilitary;
                        tempMilitary = Convert.ToString(myHouseholdMembers.myMilitaryDate);
                        tempMilitary = DateTime.Parse(tempMilitary).ToString("MM/dd/yyyy");
                        dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                        dateTimeHMMilitary.Value = Convert.ToDateTime(tempMilitary);
                    }
                    comboBoxHMPrefContact.Text = "Email";
                    textBoxHMPhoneNum.Text = "";
                    comboBoxHMPhoneType.Text = "Mobile";
                    textBoxHMAltNum.Text = "";
                    comboBoxHMAltType.Text = "";
                    textBoxHMEmail.Text = "test2@gmail.com";
                    comboBoxHMVoterCard.Text = "No";
                    comboBoxHMNotices.Text = "Email";
                    comboBoxHMAuthRep.Text = "No";
                    comboBoxHMDependant.Text = "No";
                    comboBoxHMTaxFiler.Text = "No";
                }
            }

            textBoxTotalMembers.Text = Convert.ToString(myHouseholdMembers.NumMembers + 1);
            if (myHouseholdMembers.NumMembers < 3)
            {
                buttonNextMember.Enabled = false;
                buttonPreviousMember.Enabled = false;
                textBoxTotalMembers.Text = "2";
            }
            else if (Convert.ToInt32(textBoxTotalMembers.Text) > 2)
            {
                buttonNextMember.Enabled = true;
                buttonPreviousMember.Enabled = true;
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
                using (SqlCeCommand com15 = new SqlCeCommand("SELECT * from Windows where WindowId =  " + myEditKey.myWindowsEditKey, con))
                {
                    SqlCeDataReader reader = com15.ExecuteReader();
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
                using (SqlCeCommand com16 = new SqlCeCommand("SELECT * FROM Windows where WindowId = " + myEditKey.myWindowsEditKey, con))
                {
                    SqlCeDataReader reader = com16.ExecuteReader();
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
                        using (SqlCeCommand com17 = new SqlCeCommand(myUpdateString, con))
                        {
                            com17.Parameters.AddWithValue("FunctionalArea", myFunctionalArea);
                            com17.Parameters.AddWithValue("Name", myName);
                            com17.Parameters.AddWithValue("ScreenId", myScreenId);
                            com17.Parameters.AddWithValue("Action", myAction);
                            com17.Parameters.AddWithValue("ModifiedScreenId", myModifiedScreenId);
                            com17.Parameters.AddWithValue("FunctionalYet", myFunctionalYet);
                            com17.Parameters.AddWithValue("Notes", myNotes);
                            com17.ExecuteNonQuery();
                            com17.Dispose();
                        }
                    }
                    else
                    {
                        string myInsertString;
                        DateTime now = DateTime.Now;
                        myInsertString = "Insert into Windows Values (" + myEditKey.myWindowsEditKey +
                            ",  @FunctionalArea,  @Name,  @ScreenId,  @Action" +
                            ",  @ModifiedScreenId,  @FunctionalYet,  @Notes  );";
                        using (SqlCeCommand com18 = new SqlCeCommand(myInsertString, con))
                        {
                            com18.Parameters.AddWithValue("FunctionalArea", myFunctionalArea);
                            com18.Parameters.AddWithValue("Name", myName);
                            com18.Parameters.AddWithValue("ScreenId", myScreenId);
                            com18.Parameters.AddWithValue("Action", myAction);
                            com18.Parameters.AddWithValue("ModifiedScreenId", myModifiedScreenId);
                            com18.Parameters.AddWithValue("FunctionalYet", myFunctionalYet);
                            com18.Parameters.AddWithValue("Notes", myNotes);
                            com18.ExecuteNonQuery();
                            com18.Dispose();
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
                using (SqlCeCommand com19 = new SqlCeCommand("SELECT max(WindowId) FROM Windows", con))
                {
                    SqlCeDataReader reader = com19.ExecuteReader();
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
                using (SqlCeCommand com20 = new SqlCeCommand("SELECT * FROM Methods where WindowId = " + myEditKey.myWindowsEditKey, con))
                {
                    SqlCeDataReader reader = com20.ExecuteReader();
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
                            using (SqlCeCommand com21 = new SqlCeCommand(myDeleteString, con))
                            {
                                com21.ExecuteNonQuery();
                                com21.Dispose();
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
                using (SqlCeCommand com22 = new SqlCeCommand("SELECT * from Methods where MethodId =  " + myEditKey.myMethodEditKey, con))
                {
                    SqlCeDataReader reader = com22.ExecuteReader();
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
                using (SqlCeCommand com23 = new SqlCeCommand("SELECT max(MethodId) FROM Methods", con))
                {
                    SqlCeDataReader reader = com23.ExecuteReader();
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
                using (SqlCeCommand com24 = new SqlCeCommand("SELECT * FROM Methods where MethodId = " + myMethodId, con))
                {
                    SqlCeDataReader reader = com24.ExecuteReader();
                    if (reader.Read())
                    {

                        string myUpdateString;
                        myUpdateString = "Update Methods set  MethodId = @MethodId, WindowId = @WindowId, Name = @Name, ClassName = @ClassName " +
                            ", SpecialAction = @Action" +
                            ", FunctionalYet = @FunctionalYet" +
                            " where MethodId = " + myEditKey.myMethodEditKey + ";";
                        using (SqlCeCommand com25 = new SqlCeCommand(myUpdateString, con))
                        {
                            com25.Parameters.AddWithValue("MethodId", myMethodId);
                            com25.Parameters.AddWithValue("WindowId", myWindowId);
                            com25.Parameters.AddWithValue("ClassName", myClassName);
                            com25.Parameters.AddWithValue("Name", myName);
                            com25.Parameters.AddWithValue("Action", mySpecialAction);
                            com25.Parameters.AddWithValue("FunctionalYet", myFunctionalYet);
                            com25.ExecuteNonQuery();
                            com25.Dispose();
                        }
                    }
                    else
                    {
                        string myInsertString;
                        DateTime now = DateTime.Now;
                        myInsertString = "Insert into Methods Values (" + myMethodId + ", " + myWindowId +
                            ",   @Name, @ClassName,   @Action" +
                            ",    @FunctionalYet );";
                        using (SqlCeCommand com26 = new SqlCeCommand(myInsertString, con))
                        {
                            com26.Parameters.AddWithValue("ClassName", myClassName);
                            com26.Parameters.AddWithValue("Name", myName);
                            com26.Parameters.AddWithValue("Action", mySpecialAction);
                            com26.Parameters.AddWithValue("FunctionalYet", myFunctionalYet);
                            com26.ExecuteNonQuery();
                            com26.Dispose();
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
                using (SqlCeCommand com27 = new SqlCeCommand("SELECT * FROM TestSteps where Method = '" + myMethodName + "';", con))
                {
                    SqlCeDataReader reader = com27.ExecuteReader();
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
                            using (SqlCeCommand com28 = new SqlCeCommand(myDeleteString, con))
                            {
                                com28.ExecuteNonQuery();
                                com28.Dispose();
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
                using (SqlCeCommand com29 = new SqlCeCommand("SELECT * from Test where TestId =  " + myEditKey.myTestEditKey, con))
                {
                    SqlCeDataReader reader = com29.ExecuteReader();
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
                using (SqlCeCommand com30 = new SqlCeCommand("SELECT max(TestId) FROM Test", con))
                {
                    SqlCeDataReader reader = com30.ExecuteReader();
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
                using (SqlCeCommand com31 = new SqlCeCommand("SELECT * FROM Test where TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com31.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Test set  Name = @Name, Type = @TestType " +
                            ", Description = @Description" +
                            ", URL = @URL, IsSelected = @IsSelected, Notes = @Notes" +
                            " where TestId = " + myEditKey.myTestEditKey + ";";
                        using (SqlCeCommand com32 = new SqlCeCommand(myUpdateString, con))
                        {
                            com32.Parameters.AddWithValue("Name", myName);
                            com32.Parameters.AddWithValue("TestType", myTestType);
                            com32.Parameters.AddWithValue("Description", myDescription);
                            com32.Parameters.AddWithValue("URL", myURL);
                            com32.Parameters.AddWithValue("IsSelected", myIsSelected);
                            com32.Parameters.AddWithValue("Notes", myNotes);
                            com32.ExecuteNonQuery();
                            com32.Dispose();
                        }
                    }
                    else
                    {
                        string myInsertString;
                        DateTime now = DateTime.Now;
                        myInsertString = "Insert into Test Values (" + myTestId +
                            ",   @Name, @Type, @Description, @Notes, @URL" +
                            ",   @IsSelected   );";
                        using (SqlCeCommand com33 = new SqlCeCommand(myInsertString, con))
                        {
                            com33.Parameters.AddWithValue("TestId", myTestId);
                            com33.Parameters.AddWithValue("Name", myName);
                            com33.Parameters.AddWithValue("Type", myTestType);
                            com33.Parameters.AddWithValue("Description", myDescription);
                            com33.Parameters.AddWithValue("URL", myURL);
                            com33.Parameters.AddWithValue("IsSelected", myIsSelected);
                            com33.Parameters.AddWithValue("Notes", myNotes);
                            com33.ExecuteNonQuery();
                            com33.Dispose();
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
            int rowindex;
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

            SqlCeConnection con;
            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.Database1ConnectionString;

           /* if (Convert.ToString(myTestId) == "1")
            {
                string myTestType;
                string myDescription;
                string myURL;
                string myIsSelected;
                string myNotes;

                myName = Convert.ToString(textBoxTestName.Text);
                myTestType = Convert.ToString(textBoxTestType.Text);
                myDescription = Convert.ToString(textBoxTestDescription.Text);
                myURL = Convert.ToString(textBoxTestURL.Text);
                myIsSelected = Convert.ToString(textBoxTestIsSelected.Text);
                myNotes = Convert.ToString(textBoxTestNotes.Text);

                con = new SqlCeConnection(conString);
                con.Open();

                string myInsertString;
                DateTime now = DateTime.Now;
                myInsertString = "Insert into Test Values (" + myTestId +
                    ",   @Name, @Type, @Description, @Notes, @URL" +
                    ",   @IsSelected   );";
                using (SqlCeCommand com33 = new SqlCeCommand(myInsertString, con))
                {
                    com33.Parameters.AddWithValue("TestId", myTestId);
                    com33.Parameters.AddWithValue("Name", myName);
                    com33.Parameters.AddWithValue("Type", myTestType);
                    com33.Parameters.AddWithValue("Description", myDescription);
                    com33.Parameters.AddWithValue("URL", myURL);
                    com33.Parameters.AddWithValue("IsSelected", myIsSelected);
                    com33.Parameters.AddWithValue("Notes", myNotes);
                    com33.ExecuteNonQuery();
                    com33.Dispose();
                }
            }*/

            int countSelectedTestSteps;
            countSelectedTestSteps = dataGridViewTestSteps.Rows.Count;
            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                using (SqlCeCommand com34 = new SqlCeCommand("Select max(TestStepId) from TestSteps where TestId = " + mysTestId, con))
                {
                    SqlCeDataReader reader = com34.ExecuteReader();
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
                using (SqlCeCommand com35 = new SqlCeCommand("Select Name from Windows where WindowId = " + myWindowId, con))
                {
                    SqlCeDataReader reader = com35.ExecuteReader();
                    if (reader.Read())
                    {
                        myWindow = reader.GetString(0);

                    }
                }

                myInsertString = "insert into TestSteps values(" + myTestId.ToString() + ", " + myiTestStepId +
                    ", '" + myWindow + "', '" + myWindowId + "', '" + myClass + "', '" + myName + "', '', ''); ";

                using (SqlCeCommand com36 = new SqlCeCommand(myInsertString, con))
                {
                    com36.ExecuteNonQuery();
                    com36.Dispose();
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

                using (SqlCeCommand com37 = new SqlCeCommand(myDeleteString, con))
                {
                    com37.ExecuteNonQuery();
                    com37.Dispose();
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
                MessageBox.Show("Delete Test step didn't work, exception: " + y);
            }
        }

        private void buttonConfigureEnrollment_Click(object sender, EventArgs e)
        {
            myNavHelper.myConfigureClicked = "Yes";
            tabControlMain.SelectedIndex = 3;
        }

        private void textBoxEnrollAmount_TextChanged(object sender, EventArgs e)
        {
            if (myApplication.myHouseholdOther == "No") //1 HH
            {
                if (Convert.ToInt32(textBoxEnrollAmount.Text) < 16395)
                {
                    //radioButtonApplicationTypeMA.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care MA";
                }
                else if (Convert.ToInt32(textBoxEnrollAmount.Text) < 23761)
                {
                    //radioButtonApplicationTypeBHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care BHP";
                }
                else if (Convert.ToInt32(textBoxEnrollAmount.Text) < 47521)
                {
                    //radioButtonApplicationTypeQHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care QHP";
                }
                else
                {
                    //radioButtonApplicationTypeUQHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care UQHP";
                }
            }
            else if (textBoxTotalMembers.Text == "2")
            {
                if (textBoxHMAmount.Text == "")
                {
                    textBoxHMAmount.Text = "0";
                }
                if ((Convert.ToInt32(textBoxEnrollAmount.Text) + Convert.ToInt32(textBoxHMAmount.Text)) < 22108)
                {
                    //radioButtonApplicationTypeMA.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care MA";
                }
                else if ((Convert.ToInt32(textBoxEnrollAmount.Text) + Convert.ToInt32(textBoxHMAmount.Text)) < 32041)
                {
                    //radioButtonApplicationTypeBHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care BHP";
                }
                else if ((Convert.ToInt32(textBoxEnrollAmount.Text) + Convert.ToInt32(textBoxHMAmount.Text)) < 64081)
                {
                    //radioButtonApplicationTypeQHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care QHP";
                }
                else
                {
                    //radioButtonApplicationTypeUQHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care UQHP";
                }
            }
            else if (textBoxTotalMembers.Text == "3")
            {
                if (textBoxHMAmount.Text == "")
                {
                    textBoxHMAmount.Text = "0";
                }
                if ((Convert.ToInt32(textBoxEnrollAmount.Text) + Convert.ToInt32(textBoxHMAmount.Text)) < 27821)
                {
                    //radioButtonApplicationTypeMA.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care MA";
                }
                else if ((Convert.ToInt32(textBoxEnrollAmount.Text) + Convert.ToInt32(textBoxHMAmount.Text)) < 40321)
                {
                    //radioButtonApplicationTypeBHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care BHP";
                }
                else if ((Convert.ToInt32(textBoxEnrollAmount.Text) + Convert.ToInt32(textBoxHMAmount.Text)) < 80641)
                {
                    //radioButtonApplicationTypeQHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care QHP";
                }
                else
                {
                    //radioButtonApplicationTypeUQHP.Checked = true;
                    myApplication.myEnrollmentPlanType = "MN Care UQHP";
                }
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
                using (SqlCeCommand com38 = new SqlCeCommand("SELECT ScreenId FROM Windows where WindowId = " + mysWindowId, con))
                {
                    SqlCeDataReader reader = com38.ExecuteReader();
                    if (reader.Read())
                    {
                        myScreenId = reader.GetString(0);
                    }
                }
            }
            catch (Exception n)
            {
                //silent fail
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
                using (SqlCeCommand com39 = new SqlCeCommand("SELECT ScreenId FROM Windows where WindowId = " + mysWindowId, con))
                {
                    SqlCeDataReader reader = com39.ExecuteReader();
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
                using (SqlCeCommand com40 = new SqlCeCommand("SELECT * FROM TestTemplates where TemplateId = " + myTemplateId, con))
                {
                    SqlCeDataReader reader = com40.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update TestTemplates set TemplateName = @Name"
                          + " , TestId  = @TestId " +
                            " where TemplateId = " + myEditKey.myTemplateEditKey + ";";
                        using (SqlCeCommand com41 = new SqlCeCommand(myUpdateString, con))
                        {
                            com41.Parameters.AddWithValue("Name", myName);
                            com41.Parameters.AddWithValue("TestId", myTestId);
                            com41.ExecuteNonQuery();
                            com41.Dispose();
                        }
                    }
                    else
                    {
                        string myInsertString;
                        myInsertString = "Insert into TestTemplates Values (" + myTemplateId +
                            ", " + myTestId + ", '" + myName + "' );";
                        using (SqlCeCommand com42 = new SqlCeCommand(myInsertString, con))
                        {
                            com42.ExecuteNonQuery();
                            com42.Dispose();
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
                    using (SqlCeCommand com43 = new SqlCeCommand(myDeleteString, con))
                    {
                        com43.ExecuteNonQuery();
                        com43.Dispose();
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
                using (SqlCeCommand com44 = new SqlCeCommand("SELECT max(TemplateId) FROM TestTemplates", con))
                {
                    SqlCeDataReader reader = com44.ExecuteReader();
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
                using (SqlCeCommand com45 = new SqlCeCommand("SELECT ScreenId FROM Windows where WindowId = " + mysWindowId, con))
                {
                    SqlCeDataReader reader = com45.ExecuteReader();
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
                using (SqlCeCommand com46 = new SqlCeCommand("SELECT * FROM Test where TestId = '" + mysTestId + "';", con))
                {
                    SqlCeDataReader reader = com46.ExecuteReader();
                    DialogResult result1 = MessageBox.Show("Are you sure you want to delete this Test?", "Delete Test", MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        string myDeleteString;
                        myDeleteString = "Delete FROM Test where TestId = " + mysTestId;
                        using (SqlCeCommand com47 = new SqlCeCommand(myDeleteString, con))
                        {
                            com47.ExecuteNonQuery();
                            com47.Dispose();
                            myEditKey.myTestFirstTime = "No";
                            myEditKey.myTestDeletedRow = "Yes";
                        }
                        string myDeleteString2;
                        myDeleteString2 = "Delete FROM TestSteps where TestId = " + mysTestId;
                        using (SqlCeCommand com48 = new SqlCeCommand(myDeleteString2, con))
                        {
                            com48.ExecuteNonQuery();
                            com48.Dispose();
                            myEditKey.myTestFirstTime = "No";
                            myEditKey.myTestDeletedRow = "Yes";
                        }

                        string myDeleteString3;
                        myDeleteString3 = "Delete FROM TestTemplates where TestId = " + mysTestId;
                        using (SqlCeCommand com74 = new SqlCeCommand(myDeleteString3, con))
                        {
                            com74.ExecuteNonQuery();
                            com74.Dispose();
                        }

                        string myDeleteString4;
                        myDeleteString4 = "Delete FROM Account where TestId = " + mysTestId;
                        using (SqlCeCommand com75 = new SqlCeCommand(myDeleteString4, con))
                        {
                            com75.ExecuteNonQuery();
                            com75.Dispose();
                        }

                        string myDeleteString5;
                        myDeleteString5 = "Delete FROM Address where TestId = " + mysTestId;
                        using (SqlCeCommand com76 = new SqlCeCommand(myDeleteString5, con))
                        {
                            com76.ExecuteNonQuery();
                            com76.Dispose();
                        }

                        string myDeleteString6;
                        myDeleteString6 = "Delete FROM Application where TestId = " + mysTestId;
                        using (SqlCeCommand com77 = new SqlCeCommand(myDeleteString6, con))
                        {
                            com77.ExecuteNonQuery();
                            com77.Dispose();
                        }

                        string myDeleteString7;
                        myDeleteString7 = "Delete FROM HouseMembers where TestId = " + mysTestId;
                        using (SqlCeCommand com78 = new SqlCeCommand(myDeleteString7, con))
                        {
                            com78.ExecuteNonQuery();
                            com78.Dispose();
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
                using (SqlCeCommand com49 = new SqlCeCommand("SELECT max(TestId) FROM Test", con))
                {
                    SqlCeDataReader reader = com49.ExecuteReader();
                    if (reader.Read())
                    {
                        myNewTestId = Convert.ToString(reader.GetInt32(0) + 1);
                    }
                    string myInsertString;
                    DateTime now = DateTime.Now;
                    myInsertString = "Insert into Test Values (" + myNewTestId +
                        ",   @Name, @Type, @Description, @Notes, @URL" +
                        ",   @IsSelected   );";
                    using (SqlCeCommand com50 = new SqlCeCommand(myInsertString, con))
                    {
                        com50.Parameters.AddWithValue("TestId", myNewTestId);
                        com50.Parameters.AddWithValue("Name", myTestName);
                        com50.Parameters.AddWithValue("Type", myTestType);
                        com50.Parameters.AddWithValue("Description", myDescription);
                        com50.Parameters.AddWithValue("URL", myURL);
                        com50.Parameters.AddWithValue("IsSelected", myIsSelected);
                        com50.Parameters.AddWithValue("Notes", myNotes);
                        com50.ExecuteNonQuery();
                        com50.Dispose();
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
                using (SqlCeCommand com51 = new SqlCeCommand("Select * from TestSteps where TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com51.ExecuteReader();
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

                        using (SqlCeCommand com52 = new SqlCeCommand(myInsertString, con))
                        {
                            com52.ExecuteNonQuery();
                            com52.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Copy New Test Steps didn't work, Exception: " + a);
            }

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                using (SqlCeCommand com53 = new SqlCeCommand("Select * from Application where TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com53.ExecuteReader();
                    while (reader.Read())
                    {
                        myInsertString = "Insert into Application values (1, " + Convert.ToInt32(myNewTestId) +
                                    ", @FirstName, @MiddleName, @LastName, @Suffix, @Gender, @MaritalStatus, " +
                                    "@DOB , @LiveMN, @PlanLiveMN, @PrefContact, @PhoneNum, @PhoneType, @AltNum, @AltType, @Email, @LanguageMost," +
                                    "@WrittenLanguage, @VoterCard, @Notices, @AuthRep, @ApplyYourself, @Homeless, @AddressSame, @Hispanic," +
                                    "@Race, @SSN, @Citizen, @SSNNum, @Household, @Dependants, @IncomeYN, @IncomeType, @IncomeAmount, @IncomeFrequency," +
                                    "@IncomeMore, @Employer, @Seasonal, @Reduced, @Adjusted, @Expected, @PlanType, @Foster, @MailAddrYN, @TribeName," +
                                    "@LiveRes, @TribeId, @FederalTribe, @Military, @MilitaryDate, @AppliedSSN, @WhyNoSSN, @AssistSSN, @OtherIns," +
                                    "@KindIns, @CoverageEnd, @AddIns, @ESC, @RenewalCov, @WithDiscounts, @Pregnant, @Children, @DueDate, @PregnancyEnded, @RegDate, @Day2TestId, @PassCount );";
                        using (SqlCeCommand com54 = new SqlCeCommand(myInsertString, con))
                        {
                            com54.Parameters.AddWithValue("FirstName", reader.GetString(2));
                            com54.Parameters.AddWithValue("MiddleName", reader.GetString(3));
                            com54.Parameters.AddWithValue("LastName", reader.GetString(4));
                            com54.Parameters.AddWithValue("Suffix", reader.GetString(5));
                            com54.Parameters.AddWithValue("Gender", reader.GetString(6));
                            com54.Parameters.AddWithValue("MaritalStatus", reader.GetString(7));
                            if (!reader.IsDBNull(8))
                            {
                                com54.Parameters.AddWithValue("DOB", reader.GetDateTime(8));
                            }
                            else
                            {
                                myApplication.myDOB = "01/01/2011"; // special situation
                                com54.Parameters.AddWithValue("DOB", reader.GetDateTime(8));
                            }
                            com54.Parameters.AddWithValue("LiveMN", reader.GetString(9));
                            com54.Parameters.AddWithValue("PlanLiveMN", reader.GetString(10));
                            com54.Parameters.AddWithValue("PrefContact", reader.GetString(11));
                            com54.Parameters.AddWithValue("PhoneNum", reader.GetString(12));
                            com54.Parameters.AddWithValue("PhoneType", reader.GetString(13));
                            com54.Parameters.AddWithValue("AltNum", reader.GetString(14));
                            com54.Parameters.AddWithValue("AltType", reader.GetString(15));
                            com54.Parameters.AddWithValue("Email", reader.GetString(16));
                            com54.Parameters.AddWithValue("LanguageMost", reader.GetString(17));
                            com54.Parameters.AddWithValue("WrittenLanguage", reader.GetString(18));
                            com54.Parameters.AddWithValue("VoterCard", reader.GetString(19));
                            com54.Parameters.AddWithValue("Notices", reader.GetString(20));
                            com54.Parameters.AddWithValue("AuthRep", reader.GetString(21));
                            com54.Parameters.AddWithValue("ApplyYourself", reader.GetString(22));
                            com54.Parameters.AddWithValue("Homeless", reader.GetString(23));
                            com54.Parameters.AddWithValue("AddressSame", reader.GetString(24));
                            com54.Parameters.AddWithValue("Hispanic", reader.GetString(25));
                            com54.Parameters.AddWithValue("Race", reader.GetString(26));
                            com54.Parameters.AddWithValue("SSN", reader.GetString(27));
                            com54.Parameters.AddWithValue("Citizen", reader.GetString(28));
                            com54.Parameters.AddWithValue("SSNNum", DBNull.Value);
                            com54.Parameters.AddWithValue("Household", reader.GetString(30));
                            com54.Parameters.AddWithValue("Dependants", reader.GetString(31));
                            com54.Parameters.AddWithValue("IncomeYN", reader.GetString(32));
                            com54.Parameters.AddWithValue("IncomeType", reader.GetString(33));
                            com54.Parameters.AddWithValue("IncomeAmount", reader.GetString(34));
                            com54.Parameters.AddWithValue("IncomeFrequency", reader.GetString(35));
                            com54.Parameters.AddWithValue("IncomeMore", reader.GetString(36));
                            com54.Parameters.AddWithValue("Employer", reader.GetString(37));
                            com54.Parameters.AddWithValue("Seasonal", reader.GetString(38));
                            com54.Parameters.AddWithValue("Reduced", reader.GetString(39));
                            com54.Parameters.AddWithValue("Adjusted", reader.GetString(40));
                            com54.Parameters.AddWithValue("Expected", reader.GetString(41));
                            com54.Parameters.AddWithValue("PlanType", reader.GetString(42));
                            com54.Parameters.AddWithValue("Foster", reader.GetString(43));
                            com54.Parameters.AddWithValue("MailAddrYN", reader.GetString(44));
                            com54.Parameters.AddWithValue("TribeName", reader.GetString(45));
                            com54.Parameters.AddWithValue("LiveRes", reader.GetString(46));
                            if (!reader.IsDBNull(47))
                            {
                                com54.Parameters.AddWithValue("TribeId", reader.GetString(47));
                            }
                            else
                            {
                                com54.Parameters.AddWithValue("TribeId", DBNull.Value);
                            }
                            com54.Parameters.AddWithValue("FederalTribe", reader.GetString(48));
                            com54.Parameters.AddWithValue("Military", reader.GetString(49));
                            if (!reader.IsDBNull(50))
                            {
                                com54.Parameters.AddWithValue("MilitaryDate", reader.GetDateTime(50));
                            }
                            else
                            {
                                com54.Parameters.AddWithValue("MilitaryDate", DBNull.Value);
                            }
                            com54.Parameters.AddWithValue("AppliedSSN", reader.GetString(51));
                            if (!reader.IsDBNull(52))
                            {
                                com54.Parameters.AddWithValue("WhyNoSSN", reader.GetString(52));
                            }
                            else
                            {
                                com54.Parameters.AddWithValue("WhyNoSSN", DBNull.Value);
                            }
                            com54.Parameters.AddWithValue("AssistSSN", reader.GetString(53));
                            com54.Parameters.AddWithValue("OtherIns", reader.GetString(54));
                            com54.Parameters.AddWithValue("KindIns", reader.GetString(55));
                            com54.Parameters.AddWithValue("CoverageEnd", reader.GetString(56));
                            com54.Parameters.AddWithValue("AddIns", reader.GetString(57));
                            com54.Parameters.AddWithValue("ESC", reader.GetString(58));
                            com54.Parameters.AddWithValue("RenewalCov", reader.GetString(59));
                            com54.Parameters.AddWithValue("WithDiscounts", reader.GetString(60));
                            com54.Parameters.AddWithValue("Pregnant", reader.GetString(61));
                            com54.Parameters.AddWithValue("Children", reader.GetString(62));
                            if (!reader.IsDBNull(63))
                            {
                                com54.Parameters.AddWithValue("DueDate", reader.GetDateTime(63));
                            }
                            else
                            {
                                com54.Parameters.AddWithValue("DueDate", DBNull.Value);
                            }
                            if (!reader.IsDBNull(64))
                            {
                                com54.Parameters.AddWithValue("PregnancyEnded", reader.GetString(64));
                            }
                            else
                            {
                                com54.Parameters.AddWithValue("PregnancyEnded", DBNull.Value);
                            }
                            if (!reader.IsDBNull(65))
                            {
                                com54.Parameters.AddWithValue("RegDate", reader.GetDateTime(65));
                            }
                            else
                            {
                                com54.Parameters.AddWithValue("RegDate", DBNull.Value);
                            }
                            if (!reader.IsDBNull(66))
                            {
                                com54.Parameters.AddWithValue("Day2TestId", reader.GetString(66));
                            }
                            else
                            {
                                com54.Parameters.AddWithValue("Day2TestId", DBNull.Value);
                            }
                            com54.Parameters.AddWithValue("PassCount", reader.GetString(67));

                            com54.ExecuteNonQuery();
                            com54.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Copy New Application didn't work, Exception: " + a);
            }

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                int myNewAddressId = 0;
                using (SqlCeCommand com55 = new SqlCeCommand("SELECT max(AddressId) FROM Address", con))
                {
                    SqlCeDataReader reader = com55.ExecuteReader();
                    if (reader.Read())
                    {
                        myNewAddressId = reader.GetInt32(0) + 1;
                    }
                }

                using (SqlCeCommand com56 = new SqlCeCommand("Select * from Address where Type = 'Home' and TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com56.ExecuteReader();
                    while (reader.Read())
                    {
                        myInsertString = "Insert into Address values (" + 1 + ", " + Convert.ToInt32(myNewTestId) +
                                    ", @AddressId, @Address1, @Address2, @City, @State, @Zip, @Zip4, @Type, @County, @AptSuite );";

                        using (SqlCeCommand com78 = new SqlCeCommand(myInsertString, con))
                        {
                            com78.Parameters.AddWithValue("AddressId", myNewAddressId);
                            com78.Parameters.AddWithValue("Address1", reader.GetString(3));
                            if (!reader.IsDBNull(4))
                            {
                                com78.Parameters.AddWithValue("Address2", reader.GetString(4));
                            }
                            else
                            {
                                com78.Parameters.AddWithValue("Address2", DBNull.Value);
                            }
                            com78.Parameters.AddWithValue("City", reader.GetString(5));
                            com78.Parameters.AddWithValue("State", reader.GetString(6));
                            com78.Parameters.AddWithValue("Zip", reader.GetString(7));
                            if (!reader.IsDBNull(8))
                            {
                                com78.Parameters.AddWithValue("Zip4", reader.GetString(8));
                            }
                            else
                            {
                                com78.Parameters.AddWithValue("Zip4", DBNull.Value);
                            }
                            com78.Parameters.AddWithValue("Type", "Home");
                            com78.Parameters.AddWithValue("County", reader.GetString(10));
                            if (!reader.IsDBNull(11))
                            {
                                com78.Parameters.AddWithValue("AptSuite", reader.GetString(11));
                            }
                            else
                            {
                                com78.Parameters.AddWithValue("AptSuite", DBNull.Value);
                            }

                            com78.ExecuteNonQuery();
                            com78.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Copy New Home Address didn't work, Exception: " + a);
            }

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                int myNewAddressId = 0;
                using (SqlCeCommand com58 = new SqlCeCommand("SELECT max(AddressId) FROM Address", con))
                {
                    SqlCeDataReader reader = com58.ExecuteReader();
                    if (reader.Read())
                    {
                        myNewAddressId = reader.GetInt32(0) + 1;
                    }
                }
                using (SqlCeCommand com59 = new SqlCeCommand("Select * from Address where Type = Mailing and TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com59.ExecuteReader();
                    while (reader.Read())
                    {
                        myInsertString = "Insert into Address values (1, " + Convert.ToInt32(myNewTestId) +
                                    ", @AddressId, @Address1, @Address2, @City, @State, @Zip, @Zip4, @Type, @County, @AptSuite );";

                        using (SqlCeCommand com79 = new SqlCeCommand(myInsertString, con))
                        {
                            com79.Parameters.AddWithValue("AddressId", myNewAddressId);
                            com79.Parameters.AddWithValue("Address1", reader.GetString(3));
                            if (!reader.IsDBNull(4))
                            {
                                com79.Parameters.AddWithValue("Address2", reader.GetString(4));
                            }
                            else
                            {
                                com79.Parameters.AddWithValue("Address2", DBNull.Value);
                            }
                            com79.Parameters.AddWithValue("City", reader.GetString(5));
                            com79.Parameters.AddWithValue("State", reader.GetString(6));
                            com79.Parameters.AddWithValue("Zip", reader.GetString(7));
                            if (!reader.IsDBNull(8))
                            {
                                com79.Parameters.AddWithValue("Zip4", reader.GetString(8));
                            }
                            else
                            {
                                com79.Parameters.AddWithValue("Zip4", DBNull.Value);
                            }
                            com79.Parameters.AddWithValue("Type", "Mailing");
                            com79.Parameters.AddWithValue("County", reader.GetString(10));
                            if (!reader.IsDBNull(11))
                            {
                                com79.Parameters.AddWithValue("AptSuite", reader.GetString(11));
                            }
                            else
                            {
                                com79.Parameters.AddWithValue("AptSuite", DBNull.Value);
                            }

                            com79.ExecuteNonQuery();
                            com79.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception a)
            {
                //do nothing
            }

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                int myNewAddressId = 0;
                using (SqlCeCommand com61 = new SqlCeCommand("SELECT max(AddressId) FROM Address", con))
                {
                    SqlCeDataReader reader = com61.ExecuteReader();
                    if (reader.Read())
                    {
                        myNewAddressId = reader.GetInt32(0) + 1;
                    }
                }
                using (SqlCeCommand com62 = new SqlCeCommand("Select * from Address where Type = Household 2 and TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com62.ExecuteReader();
                    while (reader.Read())
                    {
                        myInsertString = "Insert into Address values (1, " + Convert.ToInt32(myNewTestId) +
                                    ", @AddressId, @Address1, @Address2, @City, @State, @Zip, @Zip4, @Type, @County, @AptSuite );";

                        using (SqlCeCommand com80 = new SqlCeCommand(myInsertString, con))
                        {
                            com80.Parameters.AddWithValue("AddressId", myNewAddressId);
                            com80.Parameters.AddWithValue("Address1", reader.GetString(3));
                            if (!reader.IsDBNull(4))
                            {
                                com80.Parameters.AddWithValue("Address2", reader.GetString(4));
                            }
                            else
                            {
                                com80.Parameters.AddWithValue("Address2", DBNull.Value);
                            }
                            com80.Parameters.AddWithValue("City", reader.GetString(5));
                            com80.Parameters.AddWithValue("State", reader.GetString(6));
                            com80.Parameters.AddWithValue("Zip", reader.GetString(7));
                            if (!reader.IsDBNull(8))
                            {
                                com80.Parameters.AddWithValue("Zip4", reader.GetString(8));
                            }
                            else
                            {
                                com80.Parameters.AddWithValue("Zip4", DBNull.Value);
                            }
                            com80.Parameters.AddWithValue("Type", "Household 2");
                            com80.Parameters.AddWithValue("County", reader.GetString(10));
                            if (!reader.IsDBNull(11))
                            {
                                com80.Parameters.AddWithValue("AptSuite", reader.GetString(11));
                            }
                            else
                            {
                                com80.Parameters.AddWithValue("AptSuite", DBNull.Value);
                            }

                            com80.ExecuteNonQuery();
                            com80.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception a)
            {
                //do nothing
            }

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                using (SqlCeCommand com64 = new SqlCeCommand("Select * from HouseMembers where HouseMembersID = 2 and TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com64.ExecuteReader();
                    while (reader.Read())
                    {
                        myInsertString = "Insert into HouseMembers values (2, " + Convert.ToInt32(myNewTestId) +
                    ", @FirstName, @MiddleName, @LastName, @Suffix, @Gender, @MaritalStatus, " +
                    "@DOB , @LiveWithYou, @MNHome, @PersonHighlighted, @LiveMN, @TempAbsentMN, @Homeless, @PlanMakeMNHome, @SeekingEmployment, @Hispanic, @Race, @HaveSSN, @SSN, " +
                    "@USCitizen, @USNational, @Pregnant, @FosterCare, @Relationship, @HasIncome, @RelationshiptoNextHM, @TribeName, @LiveRes, @TribeId, @FederalTribe, @FileJointly, " +
                    "@IncomeType, @Employer, @Seasonal, @IncomeAmount, @IncomeFrequency, @IncomeMore, @Reduced, @Adjusted, @Expected, @PassCount, @Military, @MilitaryDate, " +
                    "@PrefContact, @PhoneNum, @PhoneType, @AltNum, @AltType, @Email, @VoterCard, @Notices, @AuthRep, @Dependant, @TaxFiler, @Children, @DueDate, @PregnancyEnded, @Reenroll, @SaveExit );";

                        using (SqlCeCommand com65 = new SqlCeCommand(myInsertString, con))
                        {
                            com65.Parameters.AddWithValue("FirstName", reader.GetString(2));
                            com65.Parameters.AddWithValue("MiddleName", reader.GetString(3));
                            com65.Parameters.AddWithValue("LastName", reader.GetString(4));
                            com65.Parameters.AddWithValue("Suffix", reader.GetString(5));
                            com65.Parameters.AddWithValue("Gender", reader.GetString(6));
                            com65.Parameters.AddWithValue("MaritalStatus", reader.GetString(7));
                            com65.Parameters.AddWithValue("DOB", reader.GetString(8));
                            com65.Parameters.AddWithValue("LiveWithYou", reader.GetString(9));
                            com65.Parameters.AddWithValue("MNHome", reader.GetString(10));
                            com65.Parameters.AddWithValue("PersonHighlighted", reader.GetString(11));
                            com65.Parameters.AddWithValue("LiveMN", reader.GetString(12));
                            com65.Parameters.AddWithValue("TempAbsentMN", reader.GetString(13));
                            com65.Parameters.AddWithValue("Homeless", reader.GetString(14));
                            com65.Parameters.AddWithValue("PlanMakeMNHome", reader.GetString(15));
                            com65.Parameters.AddWithValue("SeekingEmployment", reader.GetString(16));
                            com65.Parameters.AddWithValue("Hispanic", reader.GetString(17));
                            com65.Parameters.AddWithValue("Race", reader.GetString(18));
                            com65.Parameters.AddWithValue("HaveSSN", reader.GetString(19));
                            com65.Parameters.AddWithValue("SSN", DBNull.Value);
                            com65.Parameters.AddWithValue("USCitizen", reader.GetString(21));
                            com65.Parameters.AddWithValue("USNational", reader.GetString(22));
                            com65.Parameters.AddWithValue("Pregnant", reader.GetString(23));
                            com65.Parameters.AddWithValue("FosterCare", reader.GetString(24));
                            com65.Parameters.AddWithValue("Relationship", reader.GetString(25));
                            com65.Parameters.AddWithValue("HasIncome", reader.GetString(26));
                            com65.Parameters.AddWithValue("RelationshiptoNextHM", reader.GetString(27));
                            com65.Parameters.AddWithValue("TribeName", reader.GetString(28));
                            com65.Parameters.AddWithValue("LiveRes", reader.GetString(29));
                            com65.Parameters.AddWithValue("TribeId", reader.GetString(30));
                            com65.Parameters.AddWithValue("FederalTribe", reader.GetString(31));
                            com65.Parameters.AddWithValue("FileJointly", reader.GetString(32));
                            com65.Parameters.AddWithValue("IncomeType", reader.GetString(33));
                            com65.Parameters.AddWithValue("Employer", reader.GetString(34));
                            com65.Parameters.AddWithValue("Seasonal", reader.GetString(35));
                            com65.Parameters.AddWithValue("IncomeAmount", reader.GetString(36));
                            com65.Parameters.AddWithValue("IncomeFrequency", reader.GetString(37));
                            com65.Parameters.AddWithValue("IncomeMore", reader.GetString(38));
                            com65.Parameters.AddWithValue("Reduced", reader.GetString(39));
                            com65.Parameters.AddWithValue("Adjusted", reader.GetString(40));
                            com65.Parameters.AddWithValue("Expected", reader.GetString(41));
                            com65.Parameters.AddWithValue("PassCount", "1");
                            com65.Parameters.AddWithValue("Military", reader.GetString(43));
                            if (!reader.IsDBNull(44))
                            {
                                com65.Parameters.AddWithValue("MilitaryDate", reader.GetDateTime(44));
                            }
                            else
                            {
                                com65.Parameters.AddWithValue("MilitaryDate", DBNull.Value);
                            }
                            com65.Parameters.AddWithValue("PrefContact", reader.GetString(45));
                            com65.Parameters.AddWithValue("PhoneNum", reader.GetString(46));
                            com65.Parameters.AddWithValue("PhoneType", reader.GetString(47));
                            com65.Parameters.AddWithValue("AltNum", reader.GetString(48));
                            com65.Parameters.AddWithValue("AltType", reader.GetString(49));
                            if (!reader.IsDBNull(50))
                            {
                                com65.Parameters.AddWithValue("Email", reader.GetString(50));
                            }
                            else
                            {
                                com65.Parameters.AddWithValue("Email", DBNull.Value);
                            }
                            com65.Parameters.AddWithValue("VoterCard", reader.GetString(51));
                            com65.Parameters.AddWithValue("Notices", reader.GetString(52));
                            com65.Parameters.AddWithValue("AuthRep", reader.GetString(53));
                            com65.Parameters.AddWithValue("Dependant", reader.GetString(54));
                            com65.Parameters.AddWithValue("TaxFiler", reader.GetString(55));
                            com65.Parameters.AddWithValue("Children", reader.GetString(56));
                            if (!reader.IsDBNull(57))
                            {
                                com65.Parameters.AddWithValue("DueDate", reader.GetDateTime(57));
                            }
                            else
                            {
                                com65.Parameters.AddWithValue("DueDate", DBNull.Value);
                            }
                            if (!reader.IsDBNull(58))
                            {
                                com65.Parameters.AddWithValue("PregnancyEnded", reader.GetDateTime(58));
                            }
                            else
                            {
                                com65.Parameters.AddWithValue("PregnancyEnded", DBNull.Value);
                            }
                            com65.Parameters.AddWithValue("Reenroll", reader.GetString(59));
                            com65.Parameters.AddWithValue("SaveExit", reader.GetString(60));

                            com65.ExecuteNonQuery();
                            com65.Dispose();
                        }
                        /*using (SqlCeCommand com65 = new SqlCeCommand(myInsertString, con))
                        {
                            com65.ExecuteNonQuery();
                            com65.Dispose();
                        }*/
                    }
                }
                con.Close();
            }
            catch (Exception a)
            {
                //do nothing
            }

            try
            {
                // Open the connection using the connection string.
                con = new SqlCeConnection(conString);
                con.Open();
                string myInsertString;
                using (SqlCeCommand com66 = new SqlCeCommand("Select * from HouseMembers where HouseMembersID = 3 and TestId = " + myTestId, con))
                {
                    SqlCeDataReader reader = com66.ExecuteReader();
                    while (reader.Read())
                    {
                        myInsertString = "Insert into HouseMembers values (3, " + Convert.ToInt32(myNewTestId) +
                    ", @FirstName, @MiddleName, @LastName, @Suffix, @Gender, @MaritalStatus, " +
                    "@DOB , @LiveWithYou, @MNHome, @PersonHighlighted, @LiveMN, @TempAbsentMN, @Homeless, @PlanMakeMNHome, @SeekingEmployment, @Hispanic, @Race, @HaveSSN, @SSN, " +
                    "@USCitizen, @USNational, @Pregnant, @FosterCare, @Relationship, @HasIncome, @RelationshiptoNextHM, @TribeName, @LiveRes, @TribeId, @FederalTribe, @FileJointly, " +
                    "@IncomeType, @Employer, @Seasonal, @IncomeAmount, @IncomeFrequency, @IncomeMore, @Reduced, @Adjusted, @Expected, @PassCount, @Military, @MilitaryDate, " +
                    "@PrefContact, @PhoneNum, @PhoneType, @AltNum, @AltType, @Email, @VoterCard, @Notices, @AuthRep, @Dependant, @TaxFiler, @Children, @DueDate, @PregnancyEnded, @Reenroll, @SaveExit );";

                        using (SqlCeCommand com67 = new SqlCeCommand(myInsertString, con))
                        {
                            com67.Parameters.AddWithValue("FirstName", reader.GetString(2));
                            com67.Parameters.AddWithValue("MiddleName", reader.GetString(3));
                            com67.Parameters.AddWithValue("LastName", reader.GetString(4));
                            com67.Parameters.AddWithValue("Suffix", reader.GetString(5));
                            com67.Parameters.AddWithValue("Gender", reader.GetString(6));
                            com67.Parameters.AddWithValue("MaritalStatus", reader.GetString(7));
                            com67.Parameters.AddWithValue("DOB", reader.GetString(8));
                            com67.Parameters.AddWithValue("LiveWithYou", reader.GetString(9));
                            com67.Parameters.AddWithValue("MNHome", reader.GetString(10));
                            com67.Parameters.AddWithValue("PersonHighlighted", reader.GetString(11));
                            com67.Parameters.AddWithValue("LiveMN", reader.GetString(12));
                            com67.Parameters.AddWithValue("TempAbsentMN", reader.GetString(13));
                            com67.Parameters.AddWithValue("Homeless", reader.GetString(14));
                            com67.Parameters.AddWithValue("PlanMakeMNHome", reader.GetString(15));
                            com67.Parameters.AddWithValue("SeekingEmployment", reader.GetString(16));
                            com67.Parameters.AddWithValue("Hispanic", reader.GetString(17));
                            com67.Parameters.AddWithValue("Race", reader.GetString(18));
                            com67.Parameters.AddWithValue("HaveSSN", reader.GetString(19));
                            com67.Parameters.AddWithValue("SSN", DBNull.Value);
                            com67.Parameters.AddWithValue("USCitizen", reader.GetString(21));
                            com67.Parameters.AddWithValue("USNational", reader.GetString(22));
                            com67.Parameters.AddWithValue("Pregnant", reader.GetString(23));
                            com67.Parameters.AddWithValue("FosterCare", reader.GetString(24));
                            com67.Parameters.AddWithValue("Relationship", reader.GetString(25));
                            com67.Parameters.AddWithValue("HasIncome", reader.GetString(26));
                            com67.Parameters.AddWithValue("RelationshiptoNextHM", reader.GetString(27));
                            com67.Parameters.AddWithValue("TribeName", reader.GetString(28));
                            com67.Parameters.AddWithValue("LiveRes", reader.GetString(29));
                            com67.Parameters.AddWithValue("TribeId", reader.GetString(30));
                            com67.Parameters.AddWithValue("FederalTribe", reader.GetString(31));
                            com67.Parameters.AddWithValue("FileJointly", reader.GetString(32));
                            com67.Parameters.AddWithValue("IncomeType", reader.GetString(33));
                            com67.Parameters.AddWithValue("Employer", reader.GetString(34));
                            com67.Parameters.AddWithValue("Seasonal", reader.GetString(35));
                            com67.Parameters.AddWithValue("IncomeAmount", reader.GetString(36));
                            com67.Parameters.AddWithValue("IncomeFrequency", reader.GetString(37));
                            com67.Parameters.AddWithValue("IncomeMore", reader.GetString(38));
                            com67.Parameters.AddWithValue("Reduced", reader.GetString(39));
                            com67.Parameters.AddWithValue("Adjusted", reader.GetString(40));
                            com67.Parameters.AddWithValue("Expected", reader.GetString(41));
                            com67.Parameters.AddWithValue("PassCount", "1");
                            com67.Parameters.AddWithValue("Military", reader.GetString(43));
                            if (!reader.IsDBNull(44))
                            {
                                com67.Parameters.AddWithValue("MilitaryDate", reader.GetDateTime(44));
                            }
                            else
                            {
                                com67.Parameters.AddWithValue("MilitaryDate", DBNull.Value);
                            }
                            com67.Parameters.AddWithValue("PrefContact", reader.GetString(45));
                            com67.Parameters.AddWithValue("PhoneNum", reader.GetString(46));
                            com67.Parameters.AddWithValue("PhoneType", reader.GetString(47));
                            com67.Parameters.AddWithValue("AltNum", reader.GetString(48));
                            com67.Parameters.AddWithValue("AltType", reader.GetString(49));
                            if (!reader.IsDBNull(50))
                            {
                                com67.Parameters.AddWithValue("Email", reader.GetString(50));
                            }
                            else
                            {
                                com67.Parameters.AddWithValue("Email", DBNull.Value);
                            }
                            com67.Parameters.AddWithValue("VoterCard", reader.GetString(51));
                            com67.Parameters.AddWithValue("Notices", reader.GetString(52));
                            com67.Parameters.AddWithValue("AuthRep", reader.GetString(53));
                            com67.Parameters.AddWithValue("Dependant", reader.GetString(54));
                            com67.Parameters.AddWithValue("TaxFiler", reader.GetString(55));
                            com67.Parameters.AddWithValue("Children", reader.GetString(56));
                            if (!reader.IsDBNull(57))
                            {
                                com67.Parameters.AddWithValue("DueDate", reader.GetDateTime(57));
                            }
                            else
                            {
                                com67.Parameters.AddWithValue("DueDate", DBNull.Value);
                            }
                            if (!reader.IsDBNull(58))
                            {
                                com67.Parameters.AddWithValue("PregnancyEnded", reader.GetDateTime(58));
                            }
                            else
                            {
                                com67.Parameters.AddWithValue("PregnancyEnded", DBNull.Value);
                            }
                            com67.Parameters.AddWithValue("Reenroll", reader.GetString(59));
                            com67.Parameters.AddWithValue("SaveExit", reader.GetString(60));

                            com67.ExecuteNonQuery();
                            com67.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception a)
            {
                //do nothing
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
            labelTimeTravel.BackColor = Color.Beige;
            myHistoryInfo.myTimeTravelDate = dateTimePickerTimeTravel.Value;
        }

        private void checkBoxTimeTravel_CheckedChanged(object sender, EventArgs e)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            SqlCeCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (checkBoxTimeTravel.Checked == true)
            {
                labelTimeTravel.BackColor = Color.Yellow;
                labelTimeTravel.Visible = true;
                myHistoryInfo.myInTimeTravel = "Yes";
                cmd.CommandText = "Select * from Test where IsSelected = 'No' and Name like '% in TT'" + ";";
            }
            else
            {
                labelTimeTravel.Visible = false;
                myHistoryInfo.myInTimeTravel = "No";
                cmd.CommandText = "Select * from Test where IsSelected = 'No'" + ";";
            }
            cmd.ExecuteNonQuery();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewAvailableTests.DataSource = dt;
        }

        private void comboBoxCitizenWait_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxCaseWorkerWait_SelectedValueChanged(object sender, EventArgs e)
        {

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
                using (SqlCeCommand com68 = new SqlCeCommand("SELECT * from TestTemplates where TemplateId =  " + myEditKey.myTemplateEditKey, con))
                {
                    SqlCeDataReader reader = com68.ExecuteReader();
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

        private void comboBoxMailAddrYN_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxMailAddrYN.Text == "No")
            {
                myApplication.myMailAddress1 = "";
                myApplication.myMailAddress2 = "";
                myApplication.myMailCity = "";
                myApplication.myMailState = null;
                myApplication.myMailZip = "";
                myApplication.myMailZip4 = "";
                myApplication.myMailCounty = null;
                myApplication.myMailAptSuite = "";

                textBoxMailAddr1.Text = myApplication.myMailAddress1;
                textBoxMailAddr2.Text = myApplication.myMailAddress2;
                textBoxMailCity.Text = myApplication.myMailCity;
                comboBoxMailState.SelectedIndex = -1;
                textBoxMailZip.Text = myApplication.myMailZip;
                textBoxMailZip4.Text = myApplication.myMailZip4;
                textBoxMailAptSuite.Text = myApplication.myMailAptSuite;
                comboBoxMailCounty.SelectedIndex = -1;

                SqlCeConnection con;
                string conString = Properties.Settings.Default.Database1ConnectionString;
                try
                {
                    con = new SqlCeConnection(conString);
                    con.Open();
                    using (SqlCeCommand com69 = new SqlCeCommand("SELECT * FROM Address where TestId = " + myHistoryInfo.myTestId + "and Type = 'Mailing'", con))
                    {
                        SqlCeDataReader reader = com69.ExecuteReader();
                        string myDeleteString;
                        myDeleteString = "Delete FROM Address where TestId = " + myHistoryInfo.myTestId + "and Type = 'Mailing'";
                        using (SqlCeCommand com70 = new SqlCeCommand(myDeleteString, con))
                        {
                            com70.ExecuteNonQuery();
                            com70.Dispose();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Delete Mailing Address didn't work");

                }
            }
        }

        private void comboBoxMilitary_SelectedValueChanged(object sender, EventArgs e)
        {
            myApplication.myMilitaryDate = null;
            if (comboBoxMilitary.Text == "No")
            {
                dateTimeMilitary.Enabled = false;
                dateTimeMilitary.Format = DateTimePickerFormat.Custom;
                dateTimeMilitary.CustomFormat = " ";
            }
            else
            {
                dateTimeMilitary.Enabled = true;
                dateTimeMilitary.Format = DateTimePickerFormat.Short;
            }
        }

        private void comboBoxAppWait_SelectedValueChanged(object sender, EventArgs e)
        {
            myHistoryInfo.myAppWait = Convert.ToInt32(comboBoxAppWait.Text);
        }

        private void comboBoxOtherIns_SelectedValueChanged(object sender, EventArgs e)
        {
            myApplication.myOtherIns = comboBoxOtherIns.Text;
        }

        private void comboBoxKindIns_SelectedValueChanged(object sender, EventArgs e)
        {
            myApplication.myKindIns = comboBoxKindIns.Text;
        }

        private void comboBoxCoverageEnd_SelectedValueChanged(object sender, EventArgs e)
        {
            myApplication.myCoverageEnd = comboBoxCoverageEnd.Text;
        }

        private void comboBoxAddIns_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxAddIns_SelectedValueChanged(object sender, EventArgs e)
        {
            myApplication.myAddIns = comboBoxAddIns.Text;
        }

        private void comboBoxESC_SelectedValueChanged(object sender, EventArgs e)
        {
            myApplication.myESC = comboBoxESC.Text;
        }

        private void comboBoxRenewalCov_SelectedValueChanged(object sender, EventArgs e)
        {
            myApplication.myRenewalCov = comboBoxRenewalCov.Text;
        }

        private void comboBoxWithDiscounts_SelectedValueChanged(object sender, EventArgs e)
        {
            myApplication.myWithDiscounts = comboBoxWithDiscounts.Text;
        }

        private void dateTimeHMMilitary_ValueChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myMilitaryDate = null;
            if (comboBoxHMMilitary.Text == "No")
            {
                dateTimeHMMilitary.Enabled = false;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                dateTimeHMMilitary.CustomFormat = " ";
            }
            else
            {
                dateTimeHMMilitary.Enabled = true;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
            }
        }

        private void textBoxHMAmount_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxHMIncomeReduced_SelectedValueChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myIncomeReduced = comboBoxHMIncomeReduced.Text;
        }

        private void textBoxHMFirstName_TextChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myFirstName = textBoxHMFirstName.Text;
        }

        private void textBoxHMMiddleName_TextChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myMiddleName = textBoxHMMiddleName.Text;
        }

        private void textBoxHMLastName_TextChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myLastName = textBoxHMLastName.Text;
        }

        private void comboBoxHMSuffix_SelectedValueChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.mySuffix = comboBoxHMSuffix.Text;
        }

        private void comboBoxHMGender_SelectedValueChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myGender = comboBoxHMGender.Text;
        }

        private void comboBoxHMMaritalStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myMaritalStatus = comboBoxHMMaritalStatus.Text;
        }

        private void textBoxHMDOB_TextChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myDOB = textBoxHMDOB.Text;
        }

        private void buttonPreviousMember_Click(object sender, EventArgs e)
        {
            int result;
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string myTestId;
            myTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            myHouseholdMembers.HouseMembersID = myHouseholdMembers.HouseMembersID - 1;
            FillStructures householdMembers = new FillStructures();
            result = householdMembers.doGetHouseholdMember(ref myHouseholdMembers, ref  myHistoryInfo, myTestId);

            //The structure should be full now, so populate all the boxes.  
            textBoxHMFirstName.Text = myHouseholdMembers.myFirstName;
            textBoxHMMiddleName.Text = myHouseholdMembers.myMiddleName;
            textBoxHMLastName.Text = myHouseholdMembers.myLastName;
            comboBoxHMSuffix.Text = myHouseholdMembers.mySuffix;
            comboBoxHMGender.Text = myHouseholdMembers.myGender;
            comboBoxHMMaritalStatus.Text = myHouseholdMembers.myMaritalStatus;
            textBoxHMDOB.Text = myHouseholdMembers.myDOB;
            comboBoxHMLiveWithYou.Text = myHouseholdMembers.myLiveWithYou;
            comboBoxHMLiveMN.Text = myHouseholdMembers.myLiveInMN;
            comboBoxHMTempAbsentMN.Text = myHouseholdMembers.myTempAbsentMN;
            comboBoxHMHomeless.Text = myHouseholdMembers.myHomeless;
            textBoxHMAddress1.Text = myHouseholdMembers.myMailAddress1;
            textBoxHMAddress2.Text = myHouseholdMembers.myMailAddress2;
            textBoxHMAptSuite.Text = myHouseholdMembers.myMailAptSuite;
            textBoxHMCity.Text = myHouseholdMembers.myMailCity;
            comboBoxHMState.Text = myHouseholdMembers.myMailState;
            textBoxHMZip.Text = myHouseholdMembers.myMailZip;
            comboBoxHMCounty.Text = myHouseholdMembers.myMailCounty;
            comboBoxHMPlanToLiveInMN.Text = myHouseholdMembers.myPlanMakeMNHome;
            comboBoxHMSeekingEmployment.Text = myHouseholdMembers.mySeekEmplMN;
            comboBoxHMPersonHighlighted.Text = myHouseholdMembers.myPersonHighlighted;
            comboBoxHMHispanic.Text = myHouseholdMembers.myHispanic;
            textBoxHMTribeName.Text = myHouseholdMembers.myTribeName;
            textBoxHMTribeId.Text = myHouseholdMembers.myTribeId;
            comboBoxHMLiveRes.Text = myHouseholdMembers.myLiveRes;
            comboBoxHMFederalTribe.Text = myHouseholdMembers.myFederalTribe;
            comboBoxHMRace.Text = myHouseholdMembers.myRace;
            comboBoxHMHaveSSN.Text = myHouseholdMembers.myHaveSSN;
            //textBoxHMSSN.Text = myHouseholdMembers.mySSN;//auto generated
            comboBoxHMUSCitizen.Text = myHouseholdMembers.myUSCitizen;
            comboBoxHMUSNational.Text = myHouseholdMembers.myUSNational;
            comboBoxHMPregnant.Text = myHouseholdMembers.myIsPregnant;
            comboBoxHMBeenInFosterCare.Text = myHouseholdMembers.myBeenInFosterCare;
            comboBoxHMRelationship.Text = myHouseholdMembers.myRelationship;
            comboBoxHasIncome.Text = myHouseholdMembers.myHasIncome;
            comboBoxHMRelationship2.Text = myHouseholdMembers.myRelationshiptoNextHM;
            comboBoxHMFileJointly.Text = myHouseholdMembers.myFileJointly;
            comboBoxHMIncomeType.Text = myHouseholdMembers.myIncomeType;
            textBoxHMEmployerName.Text = myHouseholdMembers.myIncomeEmployer;
            comboBoxHMSeasonal.Text = myHouseholdMembers.myIncomeSeasonal;
            textBoxHMAmount.Text = myHouseholdMembers.myIncomeAmount;
            comboBoxHMFrequency.Text = myHouseholdMembers.myIncomeFrequency;
            comboBoxHMMoreIncome.Text = myHouseholdMembers.myIncomeMore;
            comboBoxHMIncomeReduced.Text = myHouseholdMembers.myIncomeReduced;
            comboBoxHMIncomeAdjustments.Text = myHouseholdMembers.myIncomeAdjusted;
            comboBoxHMAnnualIncome.Text = myHouseholdMembers.myIncomeExpected;
            comboBoxHMMilitary.Text = myHouseholdMembers.myMilitary;
            if (myHouseholdMembers.myMilitary == "Yes")
            {
                dateTimeHMMilitary.Enabled = true;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
            }
            else
            {
                dateTimeHMMilitary.Enabled = false;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                dateTimeHMMilitary.CustomFormat = " ";
            }
            dateTimeHMMilitary.Text = myHouseholdMembers.myMilitaryDate;
            if (myHouseholdMembers.myMilitaryDate != null && myHouseholdMembers.myMilitaryDate != " ")
            {
                string tempMilitary;
                tempMilitary = Convert.ToString(myHouseholdMembers.myMilitaryDate);
                tempMilitary = DateTime.Parse(tempMilitary).ToString("MM/dd/yyyy");
                dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                dateTimeHMMilitary.Value = Convert.ToDateTime(tempMilitary);
            }
            comboBoxHMPrefContact.Text = myHouseholdMembers.myPrefContact;
            textBoxHMPhoneNum.Text = myHouseholdMembers.myPhoneNum;
            comboBoxHMPhoneType.Text = myHouseholdMembers.myPhoneType;
            textBoxHMAltNum.Text = myHouseholdMembers.myAltNum;
            comboBoxHMAltType.Text = myHouseholdMembers.myAltNumType;
            textBoxHMEmail.Text = myHouseholdMembers.myEmail;
            comboBoxHMVoterCard.Text = myHouseholdMembers.myVoterCard;
            comboBoxHMNotices.Text = myHouseholdMembers.myNotices;
            comboBoxHMAuthRep.Text = myHouseholdMembers.myAuthRep;
            comboBoxHMDependant.Text = myHouseholdMembers.myDependants;
            comboBoxHMTaxFiler.Text = myHouseholdMembers.myTaxFiler;
            comboBoxHMChildren.Text = myHouseholdMembers.myChildren;
            if (myHouseholdMembers.myDueDate != null && myHouseholdMembers.myDueDate != " ")
            {
                string tempDueDate;
                tempDueDate = Convert.ToString(myHouseholdMembers.myDueDate);
                tempDueDate = DateTime.Parse(tempDueDate).ToString("MM/dd/yyyy");
                dateTimeHMDueDate.Format = DateTimePickerFormat.Short;
                dateTimeHMDueDate.Value = Convert.ToDateTime(tempDueDate);
            }
            if (myHouseholdMembers.myPregnancyEnded != null && myHouseholdMembers.myPregnancyEnded != " ")
            {
                string tempPregnancyEnded;
                tempPregnancyEnded = Convert.ToString(myHouseholdMembers.myPregnancyEnded);
                tempPregnancyEnded = DateTime.Parse(tempPregnancyEnded).ToString("MM/dd/yyyy");
                dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Short;
                dateTimeHMPregnancyEnded.Value = Convert.ToDateTime(tempPregnancyEnded);
            }

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
            buttonSaveMember.Enabled = true;
        }

        private void buttonNextMember_Click(object sender, EventArgs e)
        {
            int result;
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string myTestId;
            myTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();
            myHouseholdMembers.HouseMembersID = myHouseholdMembers.HouseMembersID + 1;
            FillStructures householdMembers = new FillStructures();
            result = householdMembers.doGetHouseholdMember(ref myHouseholdMembers, ref  myHistoryInfo, myTestId);

            //The structure should be full now, so populate all the boxes.  
            textBoxHMFirstName.Text = myHouseholdMembers.myFirstName;
            textBoxHMMiddleName.Text = myHouseholdMembers.myMiddleName;
            textBoxHMLastName.Text = myHouseholdMembers.myLastName;
            comboBoxHMSuffix.Text = myHouseholdMembers.mySuffix;
            comboBoxHMGender.Text = myHouseholdMembers.myGender;
            comboBoxHMMaritalStatus.Text = myHouseholdMembers.myMaritalStatus;
            textBoxHMDOB.Text = myHouseholdMembers.myDOB;
            comboBoxHMLiveWithYou.Text = myHouseholdMembers.myLiveWithYou;
            comboBoxHMLiveMN.Text = myHouseholdMembers.myLiveInMN;
            comboBoxHMTempAbsentMN.Text = myHouseholdMembers.myTempAbsentMN;
            comboBoxHMHomeless.Text = myHouseholdMembers.myHomeless;
            textBoxHMAddress1.Text = myHouseholdMembers.myMailAddress1;
            textBoxHMAddress2.Text = myHouseholdMembers.myMailAddress2;
            textBoxHMAptSuite.Text = myHouseholdMembers.myMailAptSuite;
            textBoxHMCity.Text = myHouseholdMembers.myMailCity;
            comboBoxHMState.Text = myHouseholdMembers.myMailState;
            textBoxHMZip.Text = myHouseholdMembers.myMailZip;
            comboBoxHMCounty.Text = myHouseholdMembers.myMailCounty;
            comboBoxHMPlanToLiveInMN.Text = myHouseholdMembers.myPlanMakeMNHome;
            comboBoxHMSeekingEmployment.Text = myHouseholdMembers.mySeekEmplMN;
            comboBoxHMPersonHighlighted.Text = myHouseholdMembers.myPersonHighlighted;
            comboBoxHMHispanic.Text = myHouseholdMembers.myHispanic;
            textBoxHMTribeName.Text = myHouseholdMembers.myTribeName;
            textBoxHMTribeId.Text = myHouseholdMembers.myTribeId;
            comboBoxHMLiveRes.Text = myHouseholdMembers.myLiveRes;
            comboBoxHMFederalTribe.Text = myHouseholdMembers.myFederalTribe;
            comboBoxHMRace.Text = myHouseholdMembers.myRace;
            comboBoxHMHaveSSN.Text = myHouseholdMembers.myHaveSSN;
            //textBoxHMSSN.Text = myHouseholdMembers.mySSN;//auto generated
            comboBoxHMUSCitizen.Text = myHouseholdMembers.myUSCitizen;
            comboBoxHMUSNational.Text = myHouseholdMembers.myUSNational;
            comboBoxHMPregnant.Text = myHouseholdMembers.myIsPregnant;
            comboBoxHMBeenInFosterCare.Text = myHouseholdMembers.myBeenInFosterCare;
            comboBoxHMRelationship.Text = myHouseholdMembers.myRelationship;
            comboBoxHasIncome.Text = myHouseholdMembers.myHasIncome;
            comboBoxHMRelationship2.Text = myHouseholdMembers.myRelationshiptoNextHM;
            comboBoxHMFileJointly.Text = myHouseholdMembers.myFileJointly;
            comboBoxHMIncomeType.Text = myHouseholdMembers.myIncomeType;
            textBoxHMEmployerName.Text = myHouseholdMembers.myIncomeEmployer;
            comboBoxHMSeasonal.Text = myHouseholdMembers.myIncomeSeasonal;
            textBoxHMAmount.Text = myHouseholdMembers.myIncomeAmount;
            comboBoxHMFrequency.Text = myHouseholdMembers.myIncomeFrequency;
            comboBoxHMMoreIncome.Text = myHouseholdMembers.myIncomeMore;
            comboBoxHMIncomeReduced.Text = myHouseholdMembers.myIncomeReduced;
            comboBoxHMIncomeAdjustments.Text = myHouseholdMembers.myIncomeAdjusted;
            comboBoxHMAnnualIncome.Text = myHouseholdMembers.myIncomeExpected;
            comboBoxHMMilitary.Text = myHouseholdMembers.myMilitary;
            if (myHouseholdMembers.myMilitary == "Yes")
            {
                dateTimeHMMilitary.Enabled = true;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
            }
            else
            {
                dateTimeHMMilitary.Enabled = false;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                dateTimeHMMilitary.CustomFormat = " ";
            }
            dateTimeHMMilitary.Text = myHouseholdMembers.myMilitaryDate;
            if (myHouseholdMembers.myMilitaryDate != null && myHouseholdMembers.myMilitaryDate != " ")
            {
                string tempMilitary;
                tempMilitary = Convert.ToString(myHouseholdMembers.myMilitaryDate);
                tempMilitary = DateTime.Parse(tempMilitary).ToString("MM/dd/yyyy");
                dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                dateTimeHMMilitary.Value = Convert.ToDateTime(tempMilitary);
            }
            comboBoxHMPrefContact.Text = myHouseholdMembers.myPrefContact;
            textBoxHMPhoneNum.Text = myHouseholdMembers.myPhoneNum;
            comboBoxHMPhoneType.Text = myHouseholdMembers.myPhoneType;
            textBoxHMAltNum.Text = myHouseholdMembers.myAltNum;
            comboBoxHMAltType.Text = myHouseholdMembers.myAltNumType;
            textBoxHMEmail.Text = myHouseholdMembers.myEmail;
            comboBoxHMVoterCard.Text = myHouseholdMembers.myVoterCard;
            comboBoxHMNotices.Text = myHouseholdMembers.myNotices;
            comboBoxHMAuthRep.Text = myHouseholdMembers.myAuthRep;
            comboBoxHMDependant.Text = myHouseholdMembers.myDependants;
            comboBoxHMTaxFiler.Text = myHouseholdMembers.myTaxFiler;
            comboBoxHMChildren.Text = myHouseholdMembers.myChildren;
            if (myHouseholdMembers.myDueDate != null && myHouseholdMembers.myDueDate != " ")
            {
                string tempDueDate;
                tempDueDate = Convert.ToString(myHouseholdMembers.myDueDate);
                tempDueDate = DateTime.Parse(tempDueDate).ToString("MM/dd/yyyy");
                dateTimeHMDueDate.Format = DateTimePickerFormat.Short;
                dateTimeHMDueDate.Value = Convert.ToDateTime(tempDueDate);
            }
            if (myHouseholdMembers.myPregnancyEnded != null && myHouseholdMembers.myPregnancyEnded != " ")
            {
                string tempPregnancyEnded;
                tempPregnancyEnded = Convert.ToString(myHouseholdMembers.myPregnancyEnded);
                tempPregnancyEnded = DateTime.Parse(tempPregnancyEnded).ToString("MM/dd/yyyy");
                dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Short;
                dateTimeHMPregnancyEnded.Value = Convert.ToDateTime(tempPregnancyEnded);
            }

            textBoxCurrentMember.Text = Convert.ToString(myHouseholdMembers.HouseMembersID);
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
            buttonSaveMember.Enabled = true;
        }

        private void buttonAddMember_Click(object sender, EventArgs e)
        {
            checkBoxHMRandom.Checked = true;
            textBoxHMFirstName.Text = "";
            textBoxHMMiddleName.Text = "";
            textBoxHMLastName.Text = "";
            comboBoxHMSuffix.Text = "";
            comboBoxHMGender.Text = "";
            textBoxHMDOB.Text = "";
            comboBoxHMMaritalStatus.Text = "Married";
            comboBoxHMLiveWithYou.Text = "Yes";
            comboBoxHMLiveMN.Text = "Yes";
            comboBoxHMTempAbsentMN.Text = "No";
            comboBoxHMHomeless.Text = "No";
            textBoxHMAddress1.Text = "";
            textBoxHMAddress2.Text = "";
            textBoxHMAptSuite.Text = "";
            textBoxHMCity.Text = "";
            comboBoxHMState.Text = "";
            textBoxHMZip.Text = "";
            comboBoxHMCounty.Text = "";
            comboBoxHMPlanToLiveInMN.Text = "Yes";
            comboBoxHMSeekingEmployment.Text = "No";
            comboBoxHMPersonHighlighted.Text = "Yes";
            comboBoxHMHispanic.Text = "No";
            textBoxHMTribeName.Text = "";
            textBoxHMTribeId.Text = "";
            comboBoxHMLiveRes.Text = "No";
            comboBoxHMFederalTribe.Text = "No";
            comboBoxHMRace.Text = "White";
            comboBoxHMHaveSSN.Text = "Yes";
            //textBoxHMSSN.Text = myHouseholdMembers.mySSN;
            comboBoxHMUSCitizen.Text = "Yes";
            comboBoxHMUSNational.Text = "No";
            comboBoxHMPregnant.Text = "No";
            comboBoxHMBeenInFosterCare.Text = "No";
            comboBoxHMRelationship.Text = "Is the Spouse of";
            comboBoxHasIncome.Text = "No";
            comboBoxHMRelationship2.Text = "";
            comboBoxHMFileJointly.Text = "Yes";
            comboBoxHMIncomeType.Text = "";
            textBoxHMEmployerName.Text = "";
            comboBoxHMSeasonal.Text = "No";
            textBoxHMAmount.Text = "";
            comboBoxHMFrequency.Text = "";
            comboBoxHMMoreIncome.Text = "No";
            comboBoxHMIncomeReduced.Text = "No";
            comboBoxHMIncomeAdjustments.Text = "No";
            comboBoxHMAnnualIncome.Text = "Yes";
            comboBoxHMMilitary.Text = "No";
            if (myHouseholdMembers.myMilitary == "Yes")
            {
                dateTimeHMMilitary.Enabled = true;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
            }
            else
            {
                dateTimeHMMilitary.Enabled = false;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                dateTimeHMMilitary.CustomFormat = " ";
            }
            if (myHouseholdMembers.myMilitaryDate != null && myHouseholdMembers.myMilitaryDate != " ")
            {
                string tempMilitary;
                tempMilitary = Convert.ToString(myHouseholdMembers.myMilitaryDate);
                tempMilitary = DateTime.Parse(tempMilitary).ToString("MM/dd/yyyy");
                dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
                dateTimeHMMilitary.Value = Convert.ToDateTime(tempMilitary);
            }
            comboBoxHMPrefContact.Text = "Email";
            textBoxHMPhoneNum.Text = "";
            comboBoxHMPhoneType.Text = "Mobile";
            textBoxHMAltNum.Text = "";
            comboBoxHMAltType.Text = "";
            textBoxHMEmail.Text = "test2@gmail.com";
            comboBoxHMVoterCard.Text = "No";
            comboBoxHMNotices.Text = "Email";
            comboBoxHMAuthRep.Text = "No";
            comboBoxHMDependant.Text = "No";
            comboBoxHMTaxFiler.Text = "No";

            textBoxCurrentMember.Text = Convert.ToString(Convert.ToInt32(textBoxTotalMembers.Text) + 1);
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

        private void buttonDeleteMember_Click(object sender, EventArgs e)
        {
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string myTestId;
            myTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();

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
                    cmd2.CommandText = "Delete from HouseMembers where TestID = " + myTestId + " and HouseMembersID = " + myHouseholdMembers.HouseMembersID + ";";
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

        private void buttonSaveMember_Click(object sender, EventArgs e)
        {
            int rowindex;
            rowindex = dataGridViewSelectedTests.CurrentCell.RowIndex;
            mySelectedTest.myRowIndex = rowindex;
            string myTestId;
            myTestId = dataGridViewSelectedTests.Rows[rowindex].Cells[0].Value.ToString();

            if (checkBoxHMRandom.Checked == true)
            {
                myHouseholdMembers.myFirstName = "";
                myHouseholdMembers.myLastName = "";
            }
            else
            {
                myHouseholdMembers.myFirstName = textBoxHMFirstName.Text;
                myHouseholdMembers.myLastName = textBoxHMLastName.Text;
            }
            myHouseholdMembers.myMiddleName = textBoxHMMiddleName.Text;
            myHouseholdMembers.mySuffix = comboBoxHMSuffix.Text;
            myHouseholdMembers.myGender = comboBoxHMGender.Text;
            myHouseholdMembers.myMaritalStatus = comboBoxHMMaritalStatus.Text;
            myHouseholdMembers.myDOB = textBoxHMDOB.Text;
            myHouseholdMembers.myLiveWithYou = comboBoxHMLiveWithYou.Text;
            myHouseholdMembers.myLiveInMN = comboBoxHMLiveMN.Text;
            myHouseholdMembers.myTempAbsentMN = comboBoxHMTempAbsentMN.Text;
            myHouseholdMembers.myHomeless = comboBoxHMHomeless.Text;
            myHouseholdMembers.myMailAddress1 = textBoxHMAddress1.Text;
            myHouseholdMembers.myMailAddress2 = textBoxHMAddress2.Text;
            myHouseholdMembers.myMailAptSuite = textBoxHMAptSuite.Text;
            myHouseholdMembers.myMailCity = textBoxHMCity.Text;
            myHouseholdMembers.myMailState = comboBoxHMState.Text;
            myHouseholdMembers.myMailZip = textBoxHMZip.Text;
            myHouseholdMembers.myMailCounty = comboBoxHMCounty.Text;
            myHouseholdMembers.myPlanMakeMNHome = comboBoxHMPlanToLiveInMN.Text;
            myHouseholdMembers.mySeekEmplMN = comboBoxHMSeekingEmployment.Text;
            myHouseholdMembers.myPersonHighlighted = comboBoxHMPersonHighlighted.Text;
            myHouseholdMembers.myHispanic = comboBoxHMHispanic.Text;
            myHouseholdMembers.myTribeName = textBoxHMTribeName.Text;
            myHouseholdMembers.myTribeId = textBoxHMTribeId.Text;
            myHouseholdMembers.myLiveRes = comboBoxHMLiveRes.Text;
            myHouseholdMembers.myFederalTribe = comboBoxHMFederalTribe.Text;
            myHouseholdMembers.myRace = comboBoxHMRace.Text;
            myHouseholdMembers.myHaveSSN = comboBoxHMHaveSSN.Text;
            //myHouseholdMembers.mySSN = textBoxHMSSN.Text; //auto generated
            myHouseholdMembers.myUSCitizen = comboBoxHMUSCitizen.Text;
            myHouseholdMembers.myUSNational = comboBoxHMUSNational.Text;
            myHouseholdMembers.myIsPregnant = comboBoxHMPregnant.Text;
            myHouseholdMembers.myBeenInFosterCare = comboBoxHMBeenInFosterCare.Text;
            myHouseholdMembers.myRelationship = comboBoxHMRelationship.Text;
            myHouseholdMembers.myHasIncome = comboBoxHasIncome.Text;
            myHouseholdMembers.myRelationshiptoNextHM = comboBoxHMRelationship2.Text;
            myHouseholdMembers.myFileJointly = comboBoxHMFileJointly.Text;
            myHouseholdMembers.myIncomeType = comboBoxHMIncomeType.Text;
            myHouseholdMembers.myIncomeEmployer = textBoxHMEmployerName.Text;
            myHouseholdMembers.myIncomeSeasonal = comboBoxHMSeasonal.Text;
            myHouseholdMembers.myIncomeAmount = textBoxHMAmount.Text;
            myHouseholdMembers.myIncomeFrequency = comboBoxHMFrequency.Text;
            myHouseholdMembers.myIncomeMore = comboBoxHMMoreIncome.Text;
            myHouseholdMembers.myIncomeReduced = comboBoxHMIncomeReduced.Text;
            myHouseholdMembers.myIncomeAdjusted = comboBoxHMIncomeAdjustments.Text;
            myHouseholdMembers.myIncomeExpected = comboBoxHMAnnualIncome.Text;
            myHouseholdMembers.myMilitary = comboBoxHMMilitary.Text;
            if (dateTimeHMMilitary.Text != " ")
            {
                myHouseholdMembers.myMilitaryDate = dateTimeHMMilitary.Text;
            }
            myHouseholdMembers.myPrefContact = comboBoxHMPrefContact.Text;
            myHouseholdMembers.myPhoneNum = textBoxHMPhoneNum.Text;
            myHouseholdMembers.myPhoneType = comboBoxHMPhoneType.Text;
            myHouseholdMembers.myAltNum = textBoxHMAltNum.Text;
            myHouseholdMembers.myAltNumType = comboBoxHMAltType.Text;
            myHouseholdMembers.myEmail = textBoxHMEmail.Text;
            myHouseholdMembers.myVoterCard = comboBoxHMVoterCard.Text;
            myHouseholdMembers.myNotices = comboBoxHMNotices.Text;
            myHouseholdMembers.myAuthRep = comboBoxHMAuthRep.Text;
            myHouseholdMembers.myDependants = comboBoxHMDependant.Text;
            myHouseholdMembers.myTaxFiler = comboBoxHMTaxFiler.Text;
            myHouseholdMembers.myChildren = comboBoxHMChildren.Text;
            if (dateTimeHMDueDate.Text != " ")
            {
                myHouseholdMembers.myDueDate = dateTimeHMDueDate.Text;
            }
            if (dateTimeHMPregnancyEnded.Text != " ")
            {
                myHouseholdMembers.myPregnancyEnded = dateTimeHMPregnancyEnded.Text;
            }

            myHouseholdMembers.HouseMembersID = Convert.ToInt32(textBoxCurrentMember.Text);
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            string myInsertString;
            // Open the connection using the connection string.
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                //Delete row, then insert a new on based on the currently selected member.
                myHouseholdMembers.HouseMembersID = Convert.ToInt32(textBoxCurrentMember.Text);
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "Delete from HouseMembers where TestID = " + myTestId + " and HouseMembersID = " + myHouseholdMembers.HouseMembersID + ";";
                cmd2.ExecuteNonQuery();
                myInsertString = "Insert into HouseMembers values (" + myHouseholdMembers.HouseMembersID + ", " + myTestId +
                    ", @FirstName, @MiddleName, @LastName, @Suffix, @Gender, @MaritalStatus, " +
                    "@DOB , @LiveWithYou, @MNHome, @PersonHighlighted, @LiveMN, @TempAbsentMN, @Homeless, @PlanMakeMNHome, @SeekingEmployment, @Hispanic, @Race, @HaveSSN, @SSN, " +
                    "@USCitizen, @USNational, @Pregnant, @FosterCare, @Relationship, @HasIncome, @RelationshiptoNextHM, @TribeName, @LiveRes, @TribeId, @FederalTribe, @FileJointly, " +
                    "@IncomeType, @Employer, @Seasonal, @IncomeAmount, @IncomeFrequency, @IncomeMore, @Reduced, @Adjusted, @Expected, @PassCount, @Military, @MilitaryDate, " +
                    "@PrefContact, @PhoneNum, @PhoneType, @AltNum, @AltType, @Email, @VoterCard, @Notices, @AuthRep, @Dependant, @TaxFiler, @Children, @DueDate, @PregnancyEnded, @Reenroll, @SaveExit );";

                using (SqlCeCommand com71 = new SqlCeCommand(myInsertString, con))
                {
                    com71.Parameters.AddWithValue("FirstName", myHouseholdMembers.myFirstName);
                    com71.Parameters.AddWithValue("MiddleName", myHouseholdMembers.myMiddleName);
                    com71.Parameters.AddWithValue("LastName", myHouseholdMembers.myLastName);
                    com71.Parameters.AddWithValue("Suffix", myHouseholdMembers.mySuffix);
                    com71.Parameters.AddWithValue("Gender", myHouseholdMembers.myGender);
                    com71.Parameters.AddWithValue("MaritalStatus", myHouseholdMembers.myMaritalStatus);
                    com71.Parameters.AddWithValue("DOB", myHouseholdMembers.myDOB);
                    com71.Parameters.AddWithValue("LiveWithYou", myHouseholdMembers.myLiveWithYou);
                    com71.Parameters.AddWithValue("MNHome", myHouseholdMembers.myPlanMakeMNHome); //is mnhome the same as planmakemnhome?
                    com71.Parameters.AddWithValue("PersonHighlighted", myHouseholdMembers.myPersonHighlighted);
                    com71.Parameters.AddWithValue("LiveMN", myHouseholdMembers.myLiveInMN);
                    com71.Parameters.AddWithValue("TempAbsentMN", myHouseholdMembers.myTempAbsentMN);
                    com71.Parameters.AddWithValue("Homeless", myHouseholdMembers.myHomeless);
                    com71.Parameters.AddWithValue("PlanMakeMNHome", myHouseholdMembers.myPlanMakeMNHome);
                    com71.Parameters.AddWithValue("SeekingEmployment", myHouseholdMembers.mySeekEmplMN);
                    com71.Parameters.AddWithValue("Hispanic", myHouseholdMembers.myHispanic);
                    com71.Parameters.AddWithValue("Race", myHouseholdMembers.myRace);
                    com71.Parameters.AddWithValue("HaveSSN", myHouseholdMembers.myHaveSSN);
                    com71.Parameters.AddWithValue("SSN", DBNull.Value);
                    com71.Parameters.AddWithValue("USCitizen", myHouseholdMembers.myUSCitizen);
                    com71.Parameters.AddWithValue("USNational", myHouseholdMembers.myUSNational);
                    com71.Parameters.AddWithValue("Pregnant", myHouseholdMembers.myIsPregnant);
                    com71.Parameters.AddWithValue("FosterCare", myHouseholdMembers.myBeenInFosterCare);
                    com71.Parameters.AddWithValue("Relationship", myHouseholdMembers.myRelationship);
                    com71.Parameters.AddWithValue("HasIncome", myHouseholdMembers.myHasIncome);
                    com71.Parameters.AddWithValue("RelationshiptoNextHM", myHouseholdMembers.myRelationshiptoNextHM);
                    com71.Parameters.AddWithValue("TribeName", myHouseholdMembers.myTribeName);
                    com71.Parameters.AddWithValue("LiveRes", myHouseholdMembers.myLiveRes);
                    com71.Parameters.AddWithValue("TribeId", myHouseholdMembers.myTribeId);
                    com71.Parameters.AddWithValue("FederalTribe", myHouseholdMembers.myFederalTribe);
                    com71.Parameters.AddWithValue("FileJointly", myHouseholdMembers.myFileJointly);
                    com71.Parameters.AddWithValue("IncomeType", myHouseholdMembers.myIncomeType);
                    com71.Parameters.AddWithValue("Employer", myHouseholdMembers.myIncomeEmployer);
                    com71.Parameters.AddWithValue("Seasonal", myHouseholdMembers.myIncomeSeasonal);
                    com71.Parameters.AddWithValue("IncomeAmount", myHouseholdMembers.myIncomeAmount);
                    com71.Parameters.AddWithValue("IncomeFrequency", myHouseholdMembers.myIncomeFrequency);
                    com71.Parameters.AddWithValue("IncomeMore", myHouseholdMembers.myIncomeMore);
                    com71.Parameters.AddWithValue("Reduced", myHouseholdMembers.myIncomeReduced);
                    com71.Parameters.AddWithValue("Adjusted", myHouseholdMembers.myIncomeAdjusted);
                    com71.Parameters.AddWithValue("Expected", myHouseholdMembers.myIncomeExpected);
                    com71.Parameters.AddWithValue("PassCount", "1");
                    com71.Parameters.AddWithValue("Military", myHouseholdMembers.myMilitary);
                    if (myHouseholdMembers.myMilitaryDate != "" && myHouseholdMembers.myMilitaryDate != null)
                    {
                        com71.Parameters.AddWithValue("MilitaryDate", myHouseholdMembers.myMilitaryDate);
                    }
                    else
                    {
                        com71.Parameters.AddWithValue("MilitaryDate", DBNull.Value);
                    }
                    com71.Parameters.AddWithValue("PrefContact", myHouseholdMembers.myPrefContact);
                    com71.Parameters.AddWithValue("PhoneNum", myHouseholdMembers.myPhoneNum);
                    com71.Parameters.AddWithValue("PhoneType", myHouseholdMembers.myPhoneType);
                    com71.Parameters.AddWithValue("AltNum", myHouseholdMembers.myAltNum);
                    com71.Parameters.AddWithValue("AltType", myHouseholdMembers.myAltNumType);
                    com71.Parameters.AddWithValue("Email", myHouseholdMembers.myEmail);
                    com71.Parameters.AddWithValue("VoterCard", myHouseholdMembers.myVoterCard);
                    com71.Parameters.AddWithValue("Notices", myHouseholdMembers.myNotices);
                    com71.Parameters.AddWithValue("AuthRep", myHouseholdMembers.myAuthRep);
                    com71.Parameters.AddWithValue("Dependant", myHouseholdMembers.myDependants);
                    com71.Parameters.AddWithValue("TaxFiler", myHouseholdMembers.myTaxFiler);
                    com71.Parameters.AddWithValue("Children", myHouseholdMembers.myChildren);
                    if (myHouseholdMembers.myDueDate != "" && myHouseholdMembers.myDueDate != null)
                    {
                        com71.Parameters.AddWithValue("DueDate", myHouseholdMembers.myDueDate);
                    }
                    else
                    {
                        com71.Parameters.AddWithValue("DueDate", DBNull.Value);
                    }
                    if (myHouseholdMembers.myPregnancyEnded != "" && myHouseholdMembers.myPregnancyEnded != null)
                    {
                        com71.Parameters.AddWithValue("PregnancyEnded", myHouseholdMembers.myPregnancyEnded);
                    }
                    else
                    {
                        com71.Parameters.AddWithValue("PregnancyEnded", DBNull.Value);
                    }
                    com71.Parameters.AddWithValue("Reenroll", "No");
                    com71.Parameters.AddWithValue("SaveExit", "No");

                    com71.ExecuteNonQuery();
                    com71.Dispose();
                }

                SqlCeCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                try
                {
                    cmd3.CommandText = "Delete from Address where TestId = " + myTestId + " and Type = Household 2;";
                    cmd3.ExecuteNonQuery();
                }
                catch
                {
                    //fail silently
                }

                using (SqlCeCommand com72 = new SqlCeCommand("SELECT max(AddressId) FROM Address", con))
                {
                    SqlCeDataReader reader = com72.ExecuteReader();
                    if (reader.Read())
                    {
                        myEditKey.myNextAddressId = Convert.ToString(reader.GetInt32(0) + 1);
                    }
                    else
                    {
                        MessageBox.Show("Did not find Address id");
                    }
                    com72.Dispose();
                }

                //Basic address stuff
                if (myHouseholdMembers.myMailAddress1 != "")
                {
                    string myInsertString3;
                    myInsertString3 = "Insert into Address values (" + 1 + ", " + myTestId +
                                    ", @AddressId, @Address1, @Address2, @City, @State, @Zip, @Zip4, @Type, @County, @AptSuite );";
                    using (SqlCeCommand com73 = new SqlCeCommand(myInsertString3, con))
                    {
                        myEditKey.myNextAddressId = Convert.ToString(Convert.ToInt32(myEditKey.myNextAddressId) + 1);

                        com73.Parameters.AddWithValue("AddressId", myEditKey.myNextAddressId);
                        com73.Parameters.AddWithValue("Address1", myHouseholdMembers.myMailAddress1);
                        if (myHouseholdMembers.myMailAddress2 != "")
                        {
                            com73.Parameters.AddWithValue("Address2", myHouseholdMembers.myMailAddress2);
                        }
                        else
                        {
                            com73.Parameters.AddWithValue("Address2", DBNull.Value);
                        }
                        com73.Parameters.AddWithValue("City", myHouseholdMembers.myMailCity);
                        com73.Parameters.AddWithValue("State", myHouseholdMembers.myMailState);
                        com73.Parameters.AddWithValue("Zip", myHouseholdMembers.myMailZip);
                        com73.Parameters.AddWithValue("Zip4", DBNull.Value);
                        com73.Parameters.AddWithValue("Type", "Household 2");
                        com73.Parameters.AddWithValue("County", myHouseholdMembers.myMailCounty);
                        if (myHouseholdMembers.myMailAptSuite != "")
                        {
                            com73.Parameters.AddWithValue("AptSuite", myHouseholdMembers.myMailAptSuite);
                        }
                        else
                        {
                            com73.Parameters.AddWithValue("AptSuite", DBNull.Value);
                        }

                        com73.ExecuteNonQuery();
                        com73.Dispose();
                    }
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
            /*int result;
            myLastSSN.myLastSSN = myHouseholdMembers.mySSN;
            InitializeSSN myInitializeSSN = new InitializeSSN();
            result = myInitializeSSN.DoWriteLines(ref myLastSSN, myReadFileValues);*/
        }

        private void comboBoxHMMilitary_SelectedValueChanged(object sender, EventArgs e)
        {
            myHouseholdMembers.myMilitaryDate = null;
            if (comboBoxHMMilitary.Text == "No")
            {
                dateTimeHMMilitary.Enabled = false;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Custom;
                dateTimeHMMilitary.CustomFormat = " ";
            }
            else
            {
                dateTimeHMMilitary.Enabled = true;
                dateTimeHMMilitary.Format = DateTimePickerFormat.Short;
            }
        }

        private void comboBoxPregnant_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxPregnant.Text == "No")
            {
                myApplication.myDueDate = null;
                dateTimeDueDate.Enabled = false;
                dateTimeDueDate.Format = DateTimePickerFormat.Custom;
                dateTimeDueDate.CustomFormat = " ";
            }
            else
            {
                dateTimeDueDate.Enabled = true;
                dateTimeDueDate.Format = DateTimePickerFormat.Short;
            }
        }

        private void comboBoxHMPregnant_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxHMPregnant.Text == "No")
            {
                myHouseholdMembers.myDueDate = null;
                dateTimeHMDueDate.Enabled = false;
                dateTimeHMDueDate.Format = DateTimePickerFormat.Custom;
                dateTimeHMDueDate.CustomFormat = " ";
            }
            else
            {
                dateTimeHMDueDate.Enabled = true;
                dateTimeHMDueDate.Format = DateTimePickerFormat.Short;
            }
        }

        private void comboBoxPregnancyDone_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxPregnancyDone.Text == "No")
            {
                myApplication.myPregnancyEnded = null;
                dateTimePregnancyEnded.Enabled = false;
                dateTimePregnancyEnded.Format = DateTimePickerFormat.Custom;
                dateTimePregnancyEnded.CustomFormat = " ";
            }
            else
            {
                dateTimePregnancyEnded.Enabled = true;
                dateTimePregnancyEnded.Format = DateTimePickerFormat.Short;
            }
        }

        private void comboBoxHMPregnancyDone_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxHMPregnancyDone.Text == "No")
            {
                myHouseholdMembers.myPregnancyEnded = null;
                dateTimeHMPregnancyEnded.Enabled = false;
                dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Custom;
                dateTimeHMPregnancyEnded.CustomFormat = " ";
            }
            else
            {
                dateTimeHMPregnancyEnded.Enabled = true;
                dateTimeHMPregnancyEnded.Format = DateTimePickerFormat.Short;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxAppWait_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEnvironment_SelectedValueChanged(object sender, EventArgs e)
        {
            myHistoryInfo.myEnvironment = comboBoxEnvironment.Text;
        }

        private void comboBoxBrowser_SelectedValueChanged(object sender, EventArgs e)
        {
            myHistoryInfo.myBrowser = comboBoxBrowser.Text;
        }



    }
}
