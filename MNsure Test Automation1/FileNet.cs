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
using System.Runtime.InteropServices;
using OpenQA.Selenium.Support.UI;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;

namespace MNsure_Regression_1
{
    class FileNetDo
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoFileNetLogin(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref  mystructAccountCreate myAccountCreate, mystructApplication myEnrollment, mystructAssister myAssister, ref mystructHistoryInfo myHistoryInfo,
            ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string returnICNumber)
        {
            try
            {
                driver.Manage().Window.Maximize();
                
                ApplicationDo myApp = new ApplicationDo();
                myApp.DoWaitForElement(driver, By.Id("j_username"), myHistoryInfo);

                driver.FindElement(By.Id("j_username")).SendKeys("pw47ep01s");
                driver.FindElement(By.Id("j_password")).SendKeys("Engage01#");
                
                writeLogs.DoGetScreenshot(driver, ref myHistoryInfo);

                driver.FindElement(By.XPath("/html/body/form/div/div[2]/table/tbody/tr/td[2]/a")).Click();

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
