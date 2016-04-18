using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class WriteLogs
    {
        public int WriteRunHistoryRowStart(ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            myHistoryInfo.myStepStartTime = DateTime.Now;
            try
            {
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT max(RunId) FROM RunHistory", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    if (reader.Read())
                    {
                        myHistoryInfo.myRunId = reader.GetInt32(0);
                        //   MessageBox.Show(num.ToString());
                    }
                    else
                    {
                        myHistoryInfo.myRunId = 0;
                    }
                    com2.ExecuteNonQuery();
                    com2.Dispose();
                }

                string myInsertString;
                if (myHistoryInfo.myFirstTime == "Yes")
                {
                    myHistoryInfo.myRunId = myHistoryInfo.myRunId + 1;
                    myHistoryInfo.myFirstTime = "No";
                }
                myInsertString = "insert into RunHistory values(" + myHistoryInfo.myRunId + ", '" + myHistoryInfo.myTestId +
                    "', '" + myHistoryInfo.myTestStartTime + "','" + myHistoryInfo.myTestStartTime + "', " + "'Fail'" + ", " + "'See Run Step History'" + "); ";

                using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                {
                    com3.ExecuteNonQuery();
                    com3.Dispose();
                }

            }

            catch (Exception e)
            {
                MessageBox.Show("Write History didn't work " + e);
            }
            con.Close();

            return 1;
        }

        public int WriteTestHistoryRowStart(ref mystructHistoryInfo myHistoryInfo)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();

            myHistoryInfo.myStepStartTime = DateTime.Now;
            try
            {
                string myUpdateString;
                myUpdateString = "Update RunHistory set TestStartTime = '" + myHistoryInfo.myTestStartTime +
                    "', TestStatus = 'none'" +
                     " where RunId = " + myHistoryInfo.myRunId + " and TestId = " + myHistoryInfo.myTestId + "  ;";
                using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                {
                    com3.ExecuteNonQuery();
                    com3.Dispose();
                }
            }

            catch (Exception e)
            {
                MessageBox.Show("Write Run History didn't work " + e);
            }
            con.Close();

            return 1;
        }

        public int DoWriteTestHistoryEnd(ref mystructHistoryInfo myHistoryInfo, mystructAccountCreate myAccountCreate, mystructApplication myApplication)
        {
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            myHistoryInfo.myTestEndTime = DateTime.Now;
            string myRunStatus;
            myHistoryInfo.myRunStatus = "Pass";
            try
            {
                using (SqlCeCommand com4 = new SqlCeCommand("SELECT Status FROM TestHistory where RunId = "
                    + myHistoryInfo.myRunId + " and TestId = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com4.ExecuteReader();
                    while (reader.Read())
                    {
                        myRunStatus = reader.GetString(0);
                        if (myRunStatus == "Fail")
                        {
                            myHistoryInfo.myRunStatus = "Fail";
                        }
                        else if (myRunStatus == null)
                        {
                            myHistoryInfo.myRunStatus = "null";
                        }
                    }
                    com4.ExecuteNonQuery();
                    com4.Dispose();
                }

                string myUpdateString;
                myUpdateString = "Update RunHistory set TestEndTime = '" + myHistoryInfo.myTestEndTime +
                    "', TestStatus = '" + myHistoryInfo.myRunStatus + "' " +
                     " where RunId = " + myHistoryInfo.myRunId + " and TestId = " + myHistoryInfo.myTestId + "  ;";

                using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                {
                    com3.ExecuteNonQuery();
                    com3.Dispose();
                }
            }

            catch (Exception e)
            {

                MessageBox.Show("Write History pass method didn't work " + e);
                return 2;
            }

            try
            {
                int myRunId;
                int myLogTestId;
                string myLogSuiteName;
                string myLogTestStepClass;
                string myLogTestStepMethod;
                string myLogTestStepWindow;
                string myLogTestStepStatus;
                DateTime myLogTestStepStartTime;
                DateTime myLogTestStepEndTime;
                string myLogStepSS;
                string myLogFileLocation;
                DateTime myExecutionDate = DateTime.Now;
                string mystringExecutionDate = Convert.ToString(DateTime.Now);
                string fileName = @"C:\Logs\Log" + myHistoryInfo.myRunId + "_Test" + myHistoryInfo.myTestId + ".docx";

                // Create a document in memory:
                var doc = DocX.Create(fileName);

                doc.InsertParagraph("Test Results:").Bold().FontSize(18);
                doc.InsertParagraph(" ");
                myLogSuiteName = "Test Name: " + myHistoryInfo.myTestName;
                doc.InsertParagraph(myLogSuiteName).Bold().FontSize(14);
                doc.InsertParagraph(mystringExecutionDate).Bold().FontSize(14);
                doc.InsertParagraph("Result: " + myHistoryInfo.myTestStepStatus).Bold().FontSize(14);
                doc.InsertParagraph(" ");
                doc.InsertParagraph("Account Created, User Name: " + myAccountCreate.myUsername);
                doc.InsertParagraph("Account Created, Name: " + myApplication.myFirstName + " " + myApplication.myLastName);
                doc.InsertParagraph("Account Created, SSN: " + myApplication.mySSNNum);
                doc.InsertParagraph("Enrollment, Enrollment Plan Type: " + myApplication.myEnrollmentPlanType);
                doc.InsertParagraph("Case Worker Login Id: " + myAccountCreate.myCaseWorkerLoginId);
                doc.InsertParagraph("IC Number: " + myHistoryInfo.myIcnumber);
                doc.InsertParagraph("App Build: " + myHistoryInfo.myAppBuild);
                doc.InsertParagraph("MNsure Build: " + myHistoryInfo.myMnsureBuild);
                doc.InsertParagraph(" ");
                doc.InsertParagraph("Start Time: " + myHistoryInfo.myTestStartTime);
                doc.InsertParagraph("End Time: " + myHistoryInfo.myTestEndTime);
                doc.InsertParagraph(" ");
                doc.InsertParagraph("Test Steps Executed ").Bold().FontSize(13);
                doc.InsertParagraph(" ");

                SqlCeCommand cmd2 = con.CreateCommand();
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT * FROM TestHistory where RunId = " + myHistoryInfo.myRunId +
                    " and TestId = " + myHistoryInfo.myTestId, con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    while (reader.Read())
                    {
                        myRunId = reader.GetInt32(1);
                        myLogTestId = reader.GetInt32(2);
                        myLogTestStepClass = reader.GetString(3);
                        myLogTestStepWindow = reader.GetString(4);
                        myLogTestStepMethod = reader.GetString(5);
                        myLogTestStepStatus = reader.GetString(6);
                        myLogTestStepStartTime = reader.GetDateTime(7);
                        myLogTestStepEndTime = reader.GetDateTime(8);
                        myLogStepSS = reader.GetString(11);
                        doc.InsertParagraph("Test Window: " + myLogTestStepWindow).Bold().FontSize(12);
                        doc.InsertParagraph("Start Time: " + myLogTestStepStartTime).FontSize(12);
                        doc.InsertParagraph("End Time: " + myLogTestStepEndTime).FontSize(12);
                        doc.InsertParagraph("Class: " + myLogTestStepClass).FontSize(12);
                        doc.InsertParagraph("Method: " + myLogTestStepMethod).FontSize(12);
                        doc.InsertParagraph("Result: " + myLogTestStepStatus);

                        if (myLogStepSS != "" & myLogStepSS != "none")
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                //    System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"C:\Logs\Screenshot1.jpg");
                                System.Drawing.Image myImg = System.Drawing.Image.FromFile(myLogStepSS);
                                myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                                ms.Seek(0, SeekOrigin.Begin);
                                Novacode.Image img = doc.AddImage(ms); // Create image.
                                Paragraph p = doc.InsertParagraph("Image", false);
                                Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                                pic1.SetPictureShape(BasicShapes.cube); // Set picture shape (if needed)
                                p.InsertPicture(pic1, 0); // Insert picture into paragraph.
                            }
                        }

                        doc.InsertParagraph("");
                    }

                    com2.ExecuteNonQuery();
                    com2.Dispose();
                }
                doc.InsertParagraph(" ");
                doc.InsertParagraph("Application Data, UserName: " + myAccountCreate.myUsername);
                doc.InsertParagraph("Application Data, First Name: " + myApplication.myFirstName);
                doc.InsertParagraph("Application Data, Middle Name: " + myApplication.myMiddleName);
                doc.InsertParagraph("Application Data, Last Name: " + myApplication.myLastName);
                doc.InsertParagraph("Application Data, Suffix: " + myApplication.mySuffix);
                doc.InsertParagraph("Application Data, Gender: " + myApplication.myGender);
                doc.InsertParagraph("Application Data, Marital Status: " + myApplication.myMaritalStatus);
                doc.InsertParagraph("Application Data, DOB: " + myApplication.myDOB);
                doc.InsertParagraph("Application Data, Live in MN:  " + myApplication.myLiveMN);
                doc.InsertParagraph("Application Data, Plan to Live in MN: " + myApplication.myPlanLiveMN);
                doc.InsertParagraph("Application Data, Preferred Contact: " + myApplication.myPrefContact);
                doc.InsertParagraph("Application Data, Phone Number: " + myApplication.myPhoneNum);
                doc.InsertParagraph("Application Data, Phone Type: " + myApplication.myPhoneType);
                doc.InsertParagraph("Application Data, Alt Humber: " + myApplication.myAltNum);
                doc.InsertParagraph("Application Data, Alt Num Type: " + myApplication.myAltNumType);
                doc.InsertParagraph("Application Data, Email: " + myApplication.myEmail);
                doc.InsertParagraph("Application Data, Language most used: " + myApplication.myLanguageMost);
                doc.InsertParagraph("Application Data, Language written: " + myApplication.myLanguageWritten);
                doc.InsertParagraph("Application Data, Voter Card: " + myApplication.myVoterCard);
                doc.InsertParagraph("Application Data, Notices: " + myApplication.myNotices);
                doc.InsertParagraph("Application Data, Authorized Representative: " + myApplication.myAuthRep);
                doc.InsertParagraph("Application Data, Applying Yourself: " + myApplication.myApplyYourself);
                doc.InsertParagraph("Application Data, Are you homeless: " + myApplication.myHomeless);
                doc.InsertParagraph("Application Data, Home Address line 1: " + myApplication.myHomeAddress1);
                doc.InsertParagraph("Application Data, Address line 2: " + myApplication.myHomeAddress2);
                doc.InsertParagraph("Application Data, City: " + myApplication.myHomeCity);
                doc.InsertParagraph("Application Data, State: " + myApplication.myHomeState);
                doc.InsertParagraph("Application Data, Zip: " + myApplication.myHomeZip);
                doc.InsertParagraph("Application Data, Zip + 4: " + myApplication.myHomeZip4);
                doc.InsertParagraph("Application Data, County: " + myApplication.myHomeCounty);
                doc.InsertParagraph("Application Data, Is your address same: " + myApplication.myAddressSame);                
                doc.InsertParagraph("Application Data, Mailing Address line 1: " + myApplication.myMailAddress1);
                doc.InsertParagraph("Application Data, Address line 2: " + myApplication.myMailAddress2);
                doc.InsertParagraph("Application Data, City: " + myApplication.myMailCity);
                doc.InsertParagraph("Application Data, State: " + myApplication.myMailState);
                doc.InsertParagraph("Application Data, Zip: " + myApplication.myMailZip);
                doc.InsertParagraph("Application Data, Zip + 4: " + myApplication.myMailZip4);
                doc.InsertParagraph("Application Data, County: " + myApplication.myMailCounty);
                doc.InsertParagraph("Application Data, Apt or Suite: " + myApplication.myHomeAptSuite);
                doc.InsertParagraph("Application Data, Hispanic: " + myApplication.myHispanic);
                doc.InsertParagraph("Application Data, Race: " + myApplication.myRace);
                doc.InsertParagraph("Application Data, Have an SSN: " + myApplication.mySSN);
                doc.InsertParagraph("Application Data, SSN Number: " + myApplication.mySSNNum).Bold();
                doc.InsertParagraph("Application Data, Applied for SSN: " + myApplication.myAppliedSSN);
                doc.InsertParagraph("Application Data, Why No SSN: " + myApplication.myWhyNoSSN);
                doc.InsertParagraph("Application Data, Asssistance with SSN: " + myApplication.myAssistSSN);     
                doc.InsertParagraph("Application Data, Citizen: " + myApplication.myCitizen);
                //    myEnrollment.mySSNNum = reader.GetString(37);
                doc.InsertParagraph("Application Data, Household Other: " + myApplication.myHouseholdOther);
                doc.InsertParagraph("Application Data, Dependents: " + myApplication.myDependants);
                doc.InsertParagraph("Application Data, Have Income: " + myApplication.myIncomeYN);
                doc.InsertParagraph("Application Data, Income Type: " + myApplication.myIncomeType);
                doc.InsertParagraph("Application Data, Income Amount: " + myApplication.myIncomeAmount).Bold();
                doc.InsertParagraph("Application Data, Income Frequency: " + myApplication.myIncomeFrequency);
                doc.InsertParagraph("Application Data, More Income: " + myApplication.myIncomeMore);
                doc.InsertParagraph("Application Data, Employer: " + myApplication.myIncomeEmployer);
                doc.InsertParagraph("Application Data, Income Seasonal: " + myApplication.myIncomeSeasonal);
                doc.InsertParagraph("Application Data, Reduced Income: " + myApplication.myIncomeReduced);
                doc.InsertParagraph("Application Data, Income Adjusted: " + myApplication.myIncomeAdjusted);
                doc.InsertParagraph("Application Data, Income Expected: " + myApplication.myIncomeExpected);
                doc.InsertParagraph("Application Data, Enrollment Plan Type: " + myApplication.myEnrollmentPlanType).Bold();                             
                   
                // Save to the output directory:
                doc.Save();

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

                app = new Microsoft.Office.Interop.Excel.Application();

                string workbookPath = "C:\\Mnsure Regression 1\\Templates\\" + myHistoryInfo.myTemplate;
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = app.Workbooks.Open(workbookPath,
                        0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);
                app.Visible = true;
                Microsoft.Office.Interop.Excel.Sheets xcelSheets = excelWorkbook.Worksheets;
                string currentSheet = myHistoryInfo.myTemplate;
                Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xcelSheets.get_Item(1);

                Range excelRange = excelWorksheet.UsedRange;

                //get an object array of all of the cells in the worksheet (their values)
                object[,] valueArray = (object[,])excelRange.get_Value(
                            XlRangeValueDataType.xlRangeValueDefault);

                excelWorksheet.Cells[4, 2] = myHistoryInfo.myIcnumber;
                excelWorksheet.Cells[3, 5] = myHistoryInfo.myExecutedBy;
                excelWorksheet.Cells[3, 7] = mystringExecutionDate;
                excelWorksheet.Cells[6, 5] = "Account Created, User Name: " + myAccountCreate.myUsername + ", Password:"
                    + myAccountCreate.myPassword + ", Name: " + myApplication.myFirstName + " " + myApplication.myLastName
                    + ", SSN: " + myApplication.mySSNNum + ", Enrollment Plan Type: " + myApplication.myEnrollmentPlanType
                    + ", App Build: " + myHistoryInfo.myAppBuild + ", MNSure Build: " + myHistoryInfo.myMnsureBuild;

                int i = 0; //offset for header rows
                foreach (string s in myHistoryInfo.myRequiredScreenshots)
                {
                    if (myHistoryInfo.myRequiredScreenshots[i] != null && myHistoryInfo.myRequiredStepStatus[i] != null)
                    {
                        excelWorksheet.Cells[myHistoryInfo.myRequiredStep[i], 6] = myHistoryInfo.myRequiredStepStatus[i];
                    }
                    else
                    {
                        //do nothing
                    }
                    i = i + 1;
                }

                currentSheet = "Screenshots";
                Microsoft.Office.Interop.Excel.Worksheet excelWorksheet2 = (Microsoft.Office.Interop.Excel.Worksheet)xcelSheets.get_Item(currentSheet);
                excelRange = excelWorksheet.UsedRange;
                excelWorksheet2.Cells[1, 1] = "Window";
                excelWorksheet2.Cells[1, 2] = "Step Status";
                excelWorksheet2.Cells[1, 3] = "Exception";

                i = 0;
                int j = 2;
                int topImagePosition = 250;
                int leftImagePosition = 0;

                foreach (string s in myHistoryInfo.myRequiredScreenshots)
                {
                    if (myHistoryInfo.myRequiredScreenshots[i] != null && myHistoryInfo.myRequiredScreenshotFile[i] != null)
                    {
                        if (myHistoryInfo.myRequiredStepStatus[i] == "Fail")
                        {
                            excelWorksheet2.Cells[j, 1] = myHistoryInfo.myRequiredScreenshots[i];
                            excelWorksheet2.Cells[j, 2] = myHistoryInfo.myRequiredStepStatus[i];
                            excelWorksheet2.Cells[j, 3] = "Failed on: " + myHistoryInfo.myTestStepWindow;
                            j = j + 1;
                        }
                        else
                        {
                            excelWorksheet2.Cells[j, 1] = myHistoryInfo.myRequiredScreenshots[i];
                            excelWorksheet2.Cells[j, 2] = myHistoryInfo.myRequiredStepStatus[i];
                            excelWorksheet2.Cells[j, 3] = "N/A";
                            j = j + 1;
                        }

                        if (myHistoryInfo.myRequiredScreenshotFile[i].ToString().Contains(", ")) //multiple images for the same screen
                        {
                            List<String> allImages; 
                            allImages = myHistoryInfo.myRequiredScreenshotFile[i].ToString().Split(',').ToList();
                            foreach (string image in allImages)
                            {
                                excelWorksheet2.Shapes.AddPicture(image.Trim(), MsoTriState.msoFalse, MsoTriState.msoCTrue, leftImagePosition, topImagePosition, 900, 600);
                                topImagePosition = topImagePosition + 600;
                            }
                        }
                        else //only 1 image for a screen
                        {
                            excelWorksheet2.Shapes.AddPicture(myHistoryInfo.myRequiredScreenshotFile[i], MsoTriState.msoFalse, MsoTriState.msoCTrue, leftImagePosition, topImagePosition, 900, 600);
                            topImagePosition = topImagePosition + 600;
                        }                        
                    }
                    i = i + 1;
                }

                string workbookSavePath = "C:\\TemplatesRun\\" + "RunId_" + myHistoryInfo.myRunId + "_" + myHistoryInfo.myTemplate + ".xlsx";
                excelWorkbook.SaveAs(workbookSavePath,
                Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                app.Quit();

                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(app);

                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Didn't write results " + e);
                //   excelWorkbook.Close(true, Type.Missing, Type.Missing);

                //  app.Quit();

                //  Marshal.ReleaseComObject(excelWorkbook);
                //   Marshal.ReleaseComObject(app);

                return 2;
            }
        }

        public int DoWriteHistoryTestStepEnd(ref mystructHistoryInfo myHistoryInfo)
        {
            myHistoryInfo.myTestStepName = myHistoryInfo.myTestStepMethod;
            myHistoryInfo.myStepEndTime = DateTime.Now;
            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            con = new SqlCeConnection(conString);
            con.Open();
            myHistoryInfo.myStepEndTime = DateTime.Now;

            try
            {
                string myUpdateString;

                if (myHistoryInfo.myStepException != null)
                {
                    if (myHistoryInfo.myStepException.Length > 44)
                    {
                        myHistoryInfo.myStepException = myHistoryInfo.myStepException.Substring(0, 44);
                    }
                }
                myUpdateString = "Update TestHistory set EndTime = '" + myHistoryInfo.myStepEndTime +
                    "', Status = '" + myHistoryInfo.myTestStepStatus +
                       "', Note = '" + myHistoryInfo.myStepNotes +
                       "', Exception = '" + myHistoryInfo.myStepException +
                       "', ScreenshotLocation = '" + myHistoryInfo.myScreenShot + "'" +
                     " where RunId = " + myHistoryInfo.myRunId + " and TestId = " + myHistoryInfo.myTestId +
                     " and StepId = " + myHistoryInfo.myTestStepId + ";";

                using (SqlCeCommand com3 = new SqlCeCommand(myUpdateString, con))
                {
                    com3.ExecuteNonQuery();
                    com3.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Write History step end method didn't work " + e);
            }
            return 1;
        }


        public int DoWriteHistoryTestStepStart(ref mystructHistoryInfo myHistoryInfo)
        {
            myHistoryInfo.myStepStartTime = DateTime.Now;
            myHistoryInfo.myStepEndTime = DateTime.Now;
            myHistoryInfo.myTestStepStatus = "Started"; // 
            string conString = Properties.Settings.Default.Database1ConnectionString;
            SqlCeConnection con;
            con = new SqlCeConnection(conString);
            con.Open();

            try
            {
                int num;
                num = 1;
                using (SqlCeCommand com2 = new SqlCeCommand("SELECT max(TestId) FROM TestHistory", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    while (reader.Read())
                    {
                        num = reader.GetInt32(0);
                        myHistoryInfo.myTestHistoryId = num + 1;
                        //   MessageBox.Show(num.ToString());
                    }

                    com2.ExecuteNonQuery();
                    com2.Dispose();
                }

                string myInsertString;
                myHistoryInfo.myStepException = "";
                myHistoryInfo.myStepNotes = "";
                myHistoryInfo.myScreenShot = "none";
                myInsertString = "insert into TestHistory values(" + myHistoryInfo.myRunId.ToString() + ", "
                    + myHistoryInfo.myTestId + ", " + myHistoryInfo.myTestStepId + ", '" +
                    myHistoryInfo.myTestStepClass + "', '" + myHistoryInfo.myTestStepWindow + "', '" +
                    myHistoryInfo.myTestStepMethod + "', '" + myHistoryInfo.myTestStepStatus + "', '" +
                    myHistoryInfo.myStepStartTime + "', '" + myHistoryInfo.myStepEndTime + "', '" +
                    myHistoryInfo.myStepException + "','" + myHistoryInfo.myStepNotes + "','" + myHistoryInfo.myScreenShot + "');";

                using (SqlCeCommand com3 = new SqlCeCommand(myInsertString, con))
                {
                    com3.ExecuteNonQuery();
                    com3.Dispose();
                }

            }

            catch (Exception e)
            {
                MessageBox.Show("Write History Start Step method didn't work " + e);
            }

            con.Close();
            return 1;

        }

        public int DoGetScreenshot(IWebDriver driver, ref mystructHistoryInfo myHistoryInfo)
        {
            int i = 0;
            try
            {
                if (myHistoryInfo.myTestStepStatus == "Pass")
                {
                    foreach (string s in myHistoryInfo.myRequiredScreenshots)
                    {
                        if (s == myHistoryInfo.myTestStepWindow)
                        {
                            driver.Manage().Window.Maximize();
                            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                            SqlCeConnection con;
                            string conString = Properties.Settings.Default.Database1ConnectionString;
                            int windowCount = 1;
                            try
                            {
                                // Open the connection using the connection string.
                                con = new SqlCeConnection(conString);
                                con.Open();
                                using (SqlCeCommand com = new SqlCeCommand("SELECT Count(*) FROM TestSteps where TestId = " + "'" + myHistoryInfo.myTestId + "'" + " and Window = " + "'" + myHistoryInfo.myTestStepWindow + "'", con))
                                {
                                    SqlCeDataReader reader = com.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        windowCount = reader.GetInt32(0);
                                    }
                                    else
                                    {
                                        windowCount = 1;
                                    }
                                }
                            }
                            catch
                            {
                                windowCount = 1;
                            }

                            for (int iLoop = 1; iLoop <= windowCount; iLoop++)
                            {
                                myHistoryInfo.myScreenShot = @"C:\Logs\SS_" + myHistoryInfo.myRunId + "_" + myHistoryInfo.myTestId + "_" + myHistoryInfo.myTestStepWindow + "_" + iLoop + ".jpg";
                            }
                            
                            ss.SaveAsFile(myHistoryInfo.myScreenShot, System.Drawing.Imaging.ImageFormat.Jpeg);
                            myHistoryInfo.myRequiredStepStatus[i] = myHistoryInfo.myTestStepStatus;
                            //you can hit the same window multiple times so capture all screenshots
                            if (myHistoryInfo.myRequiredScreenshotFile[i] == null)
                            {
                                myHistoryInfo.myRequiredScreenshotFile[i] = myHistoryInfo.myScreenShot;
                            }
                            else
                            {
                                myHistoryInfo.myRequiredScreenshotFile[i] = myHistoryInfo.myRequiredScreenshotFile[i] + ", " + myHistoryInfo.myScreenShot;
                            }
                        }
                        i = i + 1;
                    }
                }
                else if (myHistoryInfo.myTestStepStatus == "Fail")
                {
                    foreach (string s in myHistoryInfo.myRequiredScreenshots)
                    {
                        if (myHistoryInfo.myRequiredStepStatus[i] == null && myHistoryInfo.myRequiredScreenshots[i] == null)
                        {
                            driver.Manage().Window.Maximize();
                            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                            myHistoryInfo.myScreenShot = @"C:\Logs\SS_" + myHistoryInfo.myRunId + "_" + myHistoryInfo.myTestId + "_" + myHistoryInfo.myTestStepName + ".jpg";
                            ss.SaveAsFile(myHistoryInfo.myScreenShot, System.Drawing.Imaging.ImageFormat.Jpeg);
                            //must go to next available required step
                            foreach (string t in myHistoryInfo.myRequiredStepStatus)
                            {
                                if (myHistoryInfo.myRequiredStepStatus[i] == null && myHistoryInfo.myRequiredScreenshots[i] != null)
                                {
                                    myHistoryInfo.myRequiredStepStatus[i] = myHistoryInfo.myTestStepStatus;
                                    myHistoryInfo.myRequiredScreenshotFile[i] = myHistoryInfo.myScreenShot;
                                    break;
                                }
                                i = i + 1;
                            }
                            break;
                        }
                        else if (myHistoryInfo.myRequiredStepStatus[i] == null && myHistoryInfo.myRequiredScreenshots[i].Length > 0)
                        {
                            driver.Manage().Window.Maximize();
                            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                            myHistoryInfo.myScreenShot = @"C:\Logs\SS_" + myHistoryInfo.myRunId + "_" + myHistoryInfo.myTestId + "_" + myHistoryInfo.myTestStepName + ".jpg";
                            ss.SaveAsFile(myHistoryInfo.myScreenShot, System.Drawing.Imaging.ImageFormat.Jpeg);

                            myHistoryInfo.myRequiredStepStatus[i] = myHistoryInfo.myTestStepStatus;
                            myHistoryInfo.myRequiredScreenshotFile[i] = myHistoryInfo.myScreenShot;
                            break;
                        } else if (s == null)
                        {
                            //do nothing
                        }
                        i = i + 1;
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Did not finish screenshot capture, Exception: " + e);
                return 2;
            }
            return 1;
        }

        public int DoGetRequiredScreenshots(ref mystructHistoryInfo myHistoryInfo)
        {
            //create the Application object we can use in the member functions.
            Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();
            _excelApp.Visible = true;

            if (myHistoryInfo.myTemplate == null)
            {
                myHistoryInfo.myTemplate = "SmokeMA";
            }

            //open the workbook   
            string tempFullName = myHistoryInfo.myTemplateFolder + myHistoryInfo.myTemplate;
            Workbook workbook = _excelApp.Workbooks.Open(tempFullName,
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

            string myWindow;
            int i = 0;
            for (int row = 6; row < worksheet.UsedRange.Rows.Count; ++row)
            {

                // IsFound = "No";
                //access each cell
                myWindow = Convert.ToString(valueArray[row, 8]);
                if (myWindow != "")
                {
                    myHistoryInfo.myRequiredScreenshots[i] = myWindow;
                    myHistoryInfo.myRequiredStep[i] = row;
                }
                i = i + 1;
            }
            workbook.Close(true, Type.Missing, Type.Missing);

            _excelApp.Quit();

            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(_excelApp);

            return 1;

        }

    }
}

