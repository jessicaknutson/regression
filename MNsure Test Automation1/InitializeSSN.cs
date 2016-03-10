
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace MNsure_Regression_1
{
    class InitializeSSN
    {
        public int DoReadLines(ref mystructSSN myLastSSN, ref mystructReadFileValues myReadFileValues)
        {
            // Read all values from the text file if there.
            try
            {
                using (StreamReader sr = new StreamReader("C:\\Logs\\SSN.txt"))
                {
                    string line;
                    int icount = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (icount == 1)
                        {
                            myLastSSN.myLastSSN = line;
                        }
                        if (icount == 2)
                        {
                            myReadFileValues.myHomeAddress1 = line;
                        }
                        if (icount == 3)
                        {
                            myReadFileValues.myHomeAddress2 = line;
                        }
                        if (icount == 4)
                        {
                            myReadFileValues.myHomeCity = line;
                        }
                        if (icount == 5)
                        {
                            myReadFileValues.myHomeState = line;
                        }
                        if (icount == 6)
                        {
                            myReadFileValues.myHomeZip = line;
                        }
                        if (icount == 7)
                        {
                            myReadFileValues.myHomeZip4 = line;
                        }
                        if (icount == 8)
                        {
                            myReadFileValues.myEmail = line;
                        }
                        if (icount == 9)
                        {
                            myReadFileValues.myPhone = line;
                        }
                        if (icount == 10)
                        {
                            myReadFileValues.myAccountSaveFileName = line;
                        }

                        icount = icount + 1;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Problem creating SSN");

            }
            return 1;
        }


        public int DoWriteLines(ref mystructSSN myLastSSN, mystructReadFileValues myReadFileValues)
        {
            try
            {

                string[] SSNToWrite = new string[] { myLastSSN.myLastSSN, myReadFileValues.myHomeAddress1, 
                    myReadFileValues.myHomeAddress2, myReadFileValues.myHomeCity, myReadFileValues.myHomeState, 
                    myReadFileValues.myHomeZip, myReadFileValues.myHomeZip4, myReadFileValues.myEmail, 
                    myReadFileValues.myPhone, myReadFileValues.myAccountSaveFileName};
                    using (StreamWriter sw = new StreamWriter("C:\\Logs\\SSN.txt"))
                    {
                        foreach (string s in SSNToWrite)
                        {
                            sw.WriteLine(s);
                        }
                    }
                }
            
            catch (Exception e)
            {
                MessageBox.Show("Error on Save SSN file: " + e);
                return 2;
            }

            return 1;

        }

    }
    
}
