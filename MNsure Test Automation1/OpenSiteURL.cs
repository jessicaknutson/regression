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
using System.Data.SqlServerCe;


namespace MNsure_Regression_1
{
    class OpenSiteURL
    {
        WriteLogs writeLogs = new WriteLogs();

        public int DoOpenMNsure(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.Navigate().GoToUrl("https://auth.stst.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver.Navigate().GoToUrl("https://auth.stst2.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }
                else
                {
                    driver.Navigate().GoToUrl("https://auth.atst.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }

                assisterNavigator = "No";

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

        public int DoNavigatorURLOpen(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver5.Navigate().GoToUrl("https://people.stst.mnsure.org/NavigatorS/application.do");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver5.Navigate().GoToUrl("https://people.stst2.mnsure.org/NavigatorS/application.do");
                }
                else
                {
                    driver5.Navigate().GoToUrl("https://people.atst.mnsure.org/NavigatorS/application.do");
                }

                assisterNavigator = "Yes";

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver5, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoOpenMNsureRelogin(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst2.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }
                else
                {
                    driver3.Navigate().GoToUrl("https://auth.atst.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }

                relogin = "Yes";

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoOpenMNsureReloginTimeTravel(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst2.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }
                else
                {
                    driver3.Navigate().GoToUrl("https://auth.atst.mnsure.org/NORIDP/privacy-policy-a.jsp?account_type=Individual");
                }

                relogin = "Yes";

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoCaseWorkerURLOpen(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                // the driver is disposed when done with
                //driver.Close();
                //driver3.Close();

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver2.Navigate().GoToUrl("https://auth.stst.mnsure.org/NORIDP/?account_type=Individual");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver2.Navigate().GoToUrl("https://auth.stst2.mnsure.org/NORIDP/?account_type=Individual");
                }
                else
                {
                    driver2.Navigate().GoToUrl("https://auth.atst.mnsure.org/NORIDP/?account_type=Individual");
                }

                // This checks for the Sign In button
                IWebElement signin = driver2.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"));

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAssisterURLOpen(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst.mnsure.org/NORIDP/?account_type=Individual");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst2.mnsure.org/NORIDP/?account_type=Individual");
                }
                else
                {
                    driver3.Navigate().GoToUrl("https://auth.atst.mnsure.org/NORIDP/?account_type=Individual");
                }

                // This checks for the Sign In button
                IWebElement signin = driver3.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"));

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAssisterTimeTravel(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver3.Navigate().GoToUrl("https://id.stst.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst.mnsure.org/samlsps/Curam&returnurl=https://people.stst.mnsure.org/Curam");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver3.Navigate().GoToUrl("https://id.stst2.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst2.mnsure.org/samlsps/Curam&returnurl=https://people.stst2.mnsure.org/Curam");
                }
                else
                {
                    driver3.Navigate().GoToUrl("https://id.atst.mnsure.org/fed/idp/initiatesso?providerid=https://people.atst.mnsure.org/samlsps/Curam&returnurl=https://people.atst.mnsure.org/Curam");
                }

                // This checks for the Sign In button
                IWebElement signin = driver3.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[3]/td/font/input[1]"));

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAssisterReloginURLOpen(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver4.Navigate().GoToUrl("https://auth.stst.mnsure.org/RIDP/?account_type=Broker");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver4.Navigate().GoToUrl("https://auth.stst2.mnsure.org/RIDP/?account_type=Broker");
                }
                else
                {
                    driver4.Navigate().GoToUrl("https://auth.atst.mnsure.org/RIDP/?account_type=Broker");
                }

                // This checks for the Sign In button
                IWebElement signin = driver4.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"));

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver4, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoAssisterReloginTimeTravel(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    //driver4.Navigate().GoToUrl("https://id.stst.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst.mnsure.org/samlsps/Curam&returnurl=https://people.stst.mnsure.org/NavigatorS");
                    driver4.Navigate().GoToUrl("https://auth.stst.mnsure.org/RIDP/?account_type=Broker");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver4.Navigate().GoToUrl("https://auth.stst2.mnsure.org/RIDP/?account_type=Broker");
                }
                else
                {
                    driver4.Navigate().GoToUrl("https://auth.atst.mnsure.org/RIDP/?account_type=Broker");
                }

                // This checks for the Sign In button              
                //IWebElement signin = driver4.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[1]/td[2]/input"));
                IWebElement signin = driver4.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"));

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver4, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoBrokerURLOpen(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst.mnsure.org/RIDP/?account_type=Broker");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst2.mnsure.org/RIDP/?account_type=Broker");
                }
                else
                {
                    driver3.Navigate().GoToUrl("https://auth.atst.mnsure.org/RIDP/?account_type=Broker");
                }

                // This checks for the Sign In button
                IWebElement signin = driver3.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"));

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoBrokerTimeTravel(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst.mnsure.org/RIDP/?account_type=Broker");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver3.Navigate().GoToUrl("https://auth.stst2.mnsure.org/RIDP/?account_type=Broker");
                }
                else
                {
                    driver3.Navigate().GoToUrl("https://auth.atst.mnsure.org/RIDP/?account_type=Broker");
                }

                // This checks for the Sign In button
                IWebElement signin = driver3.FindElement(By.XPath("/html/body/div/header/div[2]/div[3]/div[1]/a"));

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver3, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }
        
        public int DoCaseWorkerURLOpenTimeTravel(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                // the driver is disposed when done with
                //driver.Close();
                //driver3.Close();

                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver2.Navigate().GoToUrl("https://id.stst.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst.mnsure.org/samlsps/Curam&returnurl=https://people.stst.mnsure.org/Curam");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver2.Navigate().GoToUrl("https://id.stst2.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst2.mnsure.org/samlsps/Curam&returnurl=https://people.stst2.mnsure.org/Curam");
                }
                else
                {
                    driver2.Navigate().GoToUrl("https://id.atst.mnsure.org/fed/idp/initiatesso?providerid=https://people.atst.mnsure.org/samlsps/Curam&returnurl=https://people.atst.mnsure.org/Curam");
                }

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver2, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoOpenMNsureTimeTravel(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.Navigate().GoToUrl("https://id.stst.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst.mnsure.org/samlsps/Curam&returnurl=https://people.stst.mnsure.org/CitizenPortal/application.do");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver.Navigate().GoToUrl("https://id.stst2.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst2.mnsure.org/samlsps/Curam&returnurl=https://people.stst2.mnsure.org/CitizenPortal/application.do");
                }
                else
                {
                    driver.Navigate().GoToUrl("https://id.atst.mnsure.org/fed/idp/initiatesso?providerid=https://people.atst.mnsure.org/samlsps/Curam&returnurl=https://people.atst.mnsure.org/CitizenPortal/application.do");
                }

                assisterNavigator = "No";

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

        public int DoNavigatorTimeTravel(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver5.Navigate().GoToUrl("https://id.stst.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst.mnsure.org/samlsps/Curam&returnurl=https://people.stst.mnsure.org/NavigatorS");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver5.Navigate().GoToUrl("https://id.stst2.mnsure.org/fed/idp/initiatesso?providerid=https://people.stst2.mnsure.org/samlsps/Curam&returnurl=https://people.stst2.mnsure.org/NavigatorS");
                }
                else
                {
                    driver5.Navigate().GoToUrl("https://id.atst.mnsure.org/fed/idp/initiatesso?providerid=https://people.atst.mnsure.org/samlsps/Curam&returnurl=https://people.atst.mnsure.org/NavigatorS");
                }

                assisterNavigator = "Yes";

                returnStatus = "Pass";
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 1;
            }
            catch (Exception e)
            {
                returnException = Convert.ToString(e);
                returnStatus = "Fail";
                myHistoryInfo.myTestStepStatus = "Fail";
                writeLogs.DoGetScreenshot(driver5, ref myHistoryInfo);
                returnScreenshot = myHistoryInfo.myScreenShot;
                return 2;
            }
        }

        public int DoOpenFileNet(IWebDriver driver, IWebDriver driver2, IWebDriver driver3, IWebDriver driver4, IWebDriver driver5,
            ref mystructHistoryInfo myHistoryInfo, ref string returnStatus, ref string returnException, ref string returnScreenshot, ref string relogin, ref string assisterNavigator)
        {
            try
            {
                if (myHistoryInfo.myEnvironment == "STST")
                {
                    driver.Navigate().GoToUrl("http://mmis-hix-stst-fnet-v03.hix.int.state.mn.us:9082/WorkplaceXT");
                }
                else if (myHistoryInfo.myEnvironment == "STST2")
                {
                    driver.Navigate().GoToUrl("http://mmis-hix-stst2-fnet-v03.hix.int.state.mn.us:9082/WorkplaceXT");
                }
                else
                {
                    driver.Navigate().GoToUrl("http://mmis-hix-atst-fnet-v03.hix.int.state.mn.us:9082/WorkplaceXT");
                }

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



