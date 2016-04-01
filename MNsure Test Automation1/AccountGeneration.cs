using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Windows.Forms;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI; /// for dropdown

using System.Net;
using System.Data.Sql;

using System.Data.SqlClient;
using System.Data.SqlServerCe;
using OpenQA.Selenium.Chrome;

namespace MNsure_Regression_1
{
    class AccountGeneration
    {
        public int GenerateNames(mystructSelectedTest myTest, ref mystructAccountCreate myAccountCreate)
        {
            string conString = Properties.Settings.Default.Database1ConnectionString;
            SqlCeConnection con;
            con = new SqlCeConnection(conString);
            con.Open();
            int result;

            string[] maleNames = { "Aaron", "Adrien", "Bob", "Chuck", "Charles", "Dean", "Eric", "Frank", "Gregory", "Harry", "Hank", "Indiana", "James", "Joseph", "Karl", "Larry", "Mark", "Martin", "Neal", "Nick", "Olie", "Patrick", "Robert", "Steven", "Stuart", "Ted", "Thomas", "Tim", "Ulrick", "Vern", "William", "Yary", "Zowie" };
            string[] femaleNames = { "Abby", "Barb", "Betty", "Cathy", "Darla", "Debby", "Ellen", "Francis", "Grace", "Helen", "Ilean", "Jean", "Martha", "Nancy", "Nena", "Nora", "Patty", "Reena", "Stephanie", "Tammy", "Teresa", "Tina", "Thelma", "Trinity", "Vickie" };
            string[] maleMiddleNames = { "Joseph", "R", "Thomas", "Randy", "Rick" };
            string[] femaleMiddleNames = { "A", "B", "Candy", "R", "Lisa", "Wendy", "Z" };
            string[] lastNames = { "Able", "Adams", "Andle", "Adkin", "Back", "Balk", "Belt", "Bing", "Bend", "Baker", "Burns", "Calk", "Chart", "Chang", "Chong", "Dallas", "Dalt", "Decks", "Dills", "Dons", "Els", "Frat", "Gets", "Hark", "Jans", "Jons", "Kipp", "Lark", "Lefs", "Mack", "Nell", "Olla", "Peck", "Rass", "Stark", "Sims", "Stend", "Seps", "Toll", "Wats", "Welch", "Wills", "Whit", "Zena" };
            string[] suffix = { "JR", "SR", "2", "3", "4", "II", "III", "IV" };

            Random rand = new Random();
            //<65 and >18 years old, otherwise need logic to handle other scenarios
            if (rand.Next(1, 3) == 1)
            {
                Random rand2 = new Random();
                myAccountCreate.myFirstName = maleNames[rand2.Next(1, maleNames.Count())];
                myAccountCreate.myMiddleName = maleMiddleNames[rand2.Next(1, maleMiddleNames.Count())];
                myAccountCreate.myLastName = lastNames[rand2.Next(1, lastNames.Count())];
                myAccountCreate.mySuffix = suffix[rand2.Next(1, 7)];
                myAccountCreate.myDOB = rand.Next(10, 12) + "/" + rand.Next(10, 28) + "/" + rand.Next(1951, 1996);       
                result = 1;
            }
            else
            {
                Random rand2 = new Random();
                myAccountCreate.myFirstName = femaleNames[rand2.Next(1, femaleNames.Count())];
                myAccountCreate.myMiddleName = femaleMiddleNames[rand2.Next(1, femaleMiddleNames.Count())];
                myAccountCreate.myLastName = lastNames[rand2.Next(1, lastNames.Count())];
                myAccountCreate.mySuffix = suffix[rand2.Next(1, 7)];
                myAccountCreate.myDOB = rand.Next(10, 12) + "/" + rand.Next(10, 28) + "/" + rand.Next(1951, 1996);                   
                string temp1;
                temp1 = myAccountCreate.myDOB;
                result = 1;
            }
            System.Threading.Thread.Sleep(100);

            myAccountCreate.myEmail = "Test@Gmail.com";
            myAccountCreate.myPhone = "(612)812-9996";
            myAccountCreate.myUsername = "st" + myAccountCreate.myFirstName.Substring(0, 1) +
            myAccountCreate.myLastName + rand.Next(0, 9) + rand.Next(0, 9) + rand.Next(0, 9) + rand.Next(0, 9);
            myAccountCreate.myPassword = "Welcome1#";
            myAccountCreate.mySecret = "password";
            myAccountCreate.myQuestion1 = "What was the model of your first car?";
            myAccountCreate.myAnswer1 = "Pontiac";
            myAccountCreate.myQuestion2 = "What is/was your grandfather's occupation?";
            myAccountCreate.myAnswer2 = "MNIT";
            myAccountCreate.myQuestion3 = "In what city or town did your mother and father meet?";
            myAccountCreate.myAnswer3 = "Minneapolis";
            myAccountCreate.myQuestion4 = "What is the name of your favorite childhood friend?";
            myAccountCreate.myAnswer4 = "Mark";
            myAccountCreate.myQuestion5 = "What city would you like to retire to?";
            myAccountCreate.myAnswer5 = "Duluth";

            return result;
        }

        public int GenerateHouseholdNames(mystructSelectedTest mySelectedTest, ref mystructHouseholdMembers myHouseholdMembers)
        {
            string conString = Properties.Settings.Default.Database1ConnectionString;
            SqlCeConnection con;
            con = new SqlCeConnection(conString);
            con.Open();
            int result;

            string[] maleNames = { "Aaron", "Adrien", "Bob", "Chuck", "Charles", "Dean", "Eric", "Frank", "Gregory", "Harry", "Hank", "Indiana", "James", "Joseph", "Karl", "Larry", "Mark", "Martin", "Neal", "Nick", "Olie", "Patrick", "Robert", "Steven", "Stuart", "Ted", "Thomas", "Tim", "Ulrick", "Vern", "William", "Yary", "Zowie" };
            string[] femaleNames = { "Abby", "Barb", "Betty", "Cathy", "Darla", "Debby", "Ellen", "Francis", "Grace", "Helen", "Ilean", "Jean", "Martha", "Nancy", "Nena", "Nora", "Patty", "Reena", "Stephanie", "Tammy", "Teresa", "Tina", "Thelma", "Trinity", "Vickie" };
            string[] maleMiddleNames = { "Joseph", "R", "Thomas", "Randy", "Rick" };
            string[] femaleMiddleNames = { "A", "B", "Candy", "R", "Lisa", "Wendy", "Z" };
            string[] lastNames = { "Able", "Adams", "Andle", "Adkin", "Back", "Balk", "Belt", "Bing", "Bend", "Baker", "Burns", "Calk", "Chart", "Chang", "Chong", "Dallas", "Dalt", "Decks", "Dills", "Dons", "Els", "Frat", "Gets", "Hark", "Jans", "Jons", "Kipp", "Lark", "Lefs", "Mack", "Nell", "Olla", "Peck", "Rass", "Stark", "Sims", "Stend", "Seps", "Toll", "Wats", "Welch", "Wills", "Whit", "Zena" };
            // string[] suffix = { "JR", "SR", "2", "3", "4", "II", "III", "IV" };
            string[] suffix = { "Junior", "Senior", "Second", "Third", "Fourth" };

            Random rand = new Random();
            //<65 and >18 years old, otherwise need logic to handle other scenarios
            if (rand.Next(1, 3) == 1)
            {
                Random rand2 = new Random();
                myHouseholdMembers.myFirstName = maleNames[rand2.Next(1, maleNames.Count())];
                myHouseholdMembers.myMiddleName = maleMiddleNames[rand2.Next(1, maleMiddleNames.Count())];
                myHouseholdMembers.myLastName = lastNames[rand2.Next(1, lastNames.Count())];
                myHouseholdMembers.mySuffix = suffix[rand2.Next(1, suffix.Count())];
                myHouseholdMembers.myDOB = rand.Next(10, 12) + "/" + rand.Next(10, 28) + "/" + rand.Next(1951, 1996);
                myHouseholdMembers.myGender = "Male";
                result = 1;
            }
            else
            {
                Random rand2 = new Random();
                myHouseholdMembers.myFirstName = femaleNames[rand2.Next(1, femaleNames.Count())];
                myHouseholdMembers.myMiddleName = femaleMiddleNames[rand2.Next(1, femaleMiddleNames.Count())];
                myHouseholdMembers.myLastName = lastNames[rand2.Next(1, lastNames.Count())];
                myHouseholdMembers.mySuffix = suffix[rand2.Next(1, suffix.Count())];
                myHouseholdMembers.myDOB = rand.Next(10, 12) + "/" + rand.Next(10, 28) + "/" + rand.Next(1951, 1996);
                myHouseholdMembers.myGender = "Female";
                string temp1;
                temp1 = myHouseholdMembers.myDOB;
                result = 1;
            }
            System.Threading.Thread.Sleep(129);

            //Get the next SSN in sequence          
            /*
            try
            {
                SqlCeCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                //Fill Parties Datagrid

                using (SqlCeCommand com2 = new SqlCeCommand("SELECT max(Account_SSN) FROM Test_Account", con))
                {
                    SqlCeDataReader reader = com2.ExecuteReader();
                    while (reader.Read())
                    {
                        myAccountCreate.mySSN = reader.GetString(0);
                        int temp1;
                        temp1 = Convert.ToInt32(myAccountCreate.mySSN);
                        temp1 = temp1 + 1;
                        myAccountCreate.mySSN = Convert.ToString(temp1);
                    }

                    com2.ExecuteNonQuery();
                    com2.Dispose();
                }
            }
            catch
            {
                MessageBox.Show("Did not create ssn");
            }          
             */

            myHouseholdMembers.myMaritalStatus = "Married";
            myHouseholdMembers.myLiveWithYou = "Yes";
            myHouseholdMembers.myMNHome = "Yes";
            myHouseholdMembers.myPersonHighlighted = "Yes";
            myHouseholdMembers.myLiveInMN = "Yes";
            myHouseholdMembers.myTempAbsentMN = "Yes";
            myHouseholdMembers.myHomeless = "Yes";

            /*myHouseholdMembers.Address1 = "12969 First Ave W";
            myHouseholdMembers.Address2 = "PO 44";
            myHouseholdMembers.AptSuite = "Suite 64";
            myHouseholdMembers.City = "Minneapolis";
            myHouseholdMembers.State = "Minnesota";
            myHouseholdMembers.Zip = "55440";*/
            //   myHouseholdMembers.Zip4 = "";
            //   myHouseholdMembers.Email = "Test@Gmail.com";
            //   myHouseholdMembers.Phone = "6128129996";

            myHouseholdMembers.myPlanMakeMNHome = "Yes";
            myHouseholdMembers.mySeekEmplMN = "Yes";
            myHouseholdMembers.myHispanic = "No";
            myHouseholdMembers.myRace = "White";
            myHouseholdMembers.myHaveSSN = "Yes";
            myHouseholdMembers.mySSN = "123456789";
            myHouseholdMembers.myUSCitizen = "Yes";
            myHouseholdMembers.myUSNational = "Yes";
            myHouseholdMembers.myIsPregnant = "No";
            myHouseholdMembers.myBeenInFosterCare = "No";
            myHouseholdMembers.myRelationship = "Is the Spouse of";
            myHouseholdMembers.myHasIncome = "No";
            myHouseholdMembers.myRelationshiptoNextHM = "Is the Parent of";
            return result;
        }

    }
}
