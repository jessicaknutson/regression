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
using System.Net;
using System.Data.Sql;

using System.Data.SqlClient;
using System.Data.SqlServerCe;//


namespace MNsure_Regression_1
{
    class OpenSiteURL
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoOpenMNsure(IWebDriver driver, IWebDriver driver2, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.Navigate().GoToUrl("https://auth.stst.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; 
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoCaseWorkerURLOpen(IWebDriver driver, IWebDriver driver2, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                // the driver is disposed when done with
                driver.Close();

                driver2.Navigate().GoToUrl("https://auth.stst.mnsure.org/NORIDP/?account_type=Individual");
                // This checks for the Sign In button
                IWebElement myfield = driver2.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"));

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; 
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoCaseWorkerURLOpenTimeTravel(IWebDriver driver, IWebDriver driver2, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                // the driver is disposed when done with
                driver.Close();

                driver2.Navigate().GoToUrl("https://id.stst.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst.mnsure.org/samlsps/Curam&returnurl=https://people.stst.mnsure.org/Curam");

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; 
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoOpenMNsureTimeTravel(IWebDriver driver, IWebDriver driver2, ref mystructHistoryInfo myHistoryInfo,
           ref string returnStatus, ref string returnException, ref string returnScreenshot)
        {
            try
            {
                driver.Navigate().GoToUrl("https://id.stst.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst.mnsure.org/samlsps/Curam&returnurl=https://people.stst.mnsure.org/CitizenPortal/application.do");

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail"; 
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }


    }
}



