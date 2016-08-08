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
using OpenQA.Selenium.Support.UI; 
using System.Net;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using OpenQA.Selenium.Chrome;

namespace MNsure_Regression_1
{
    class AccountGeneration
    {
        public int GenerateNames(mystructSelectedTest myTest, ref mystructAccountCreate myAccountCreate, ref mystructApplication myApplication, ref mystructHistoryInfo myHistoryInfo)
        {
            int result;

            string[] maleNames = { "Aaron", "Adrien", "Bob", "Chuck", "Charles", "Dean", "Eric", "Frank", "Gregory", "Harry", "Hank", "Indiana", "James", "Joseph", "Karl", "Larry", "Mark", "Martin", "Neal", "Nick", "Olie", "Patrick", "Robert", "Steven", "Stuart", "Ted", "Thomas", "Tim", "Ulrick", "Vern", "William", "Yary", "Zowie" };
            string[] femaleNames = { "Abby", "Barb", "Betty", "Cathy", "Darla", "Debby", "Ellen", "Francis", "Grace", "Helen", "Ilean", "Jean", "Martha", "Nancy", "Nena", "Nora", "Patty", "Reena", "Stephanie", "Tammy", "Teresa", "Tina", "Thelma", "Trinity", "Vickie" };
            string[] maleMiddleNames = { "J", "K", "R", "S", "T" };
            string[] femaleMiddleNames = { "A", "B", "C", "L", "R", "W", "Z" };
            string[] lastNames = { "Able", "Adams", "Andle", "Adkin", "Back", "Balk", "Belt", "Bing", "Bend", "Baker", "Burns", "Calk", "Chart", "Chang", "Chong", "Dallas", "Dalt", "Decks", "Dills", "Dons", "Els", "Frat", "Gets", "Hark", "Jans", "Jons", "Kipp", "Lark", "Lefs", "Mack", "Nell", "Olla", "Peck", "Rass", "Stark", "Sims", "Stend", "Seps", "Toll", "Wats", "Welch", "Wills", "Whit", "Zena" };
            //string[] suffix = { "JR", "SR", "2", "3", "4", "II", "III", "IV" };
            //string[] suffix = { "Junior", "Senior", "Second", "Third", "Fourth" };//current bug, turn off for now

            Random rand = new Random();

            if (myApplication.myGender != null && myApplication.myGender != "")
            {
                if (myApplication.myGender == "Male")
                {
                    Random rand2 = new Random();
                    myAccountCreate.myFirstName = maleNames[rand2.Next(1, maleNames.Count())];
                    myAccountCreate.myMiddleName = maleMiddleNames[rand2.Next(1, maleMiddleNames.Count())];
                    myAccountCreate.myLastName = lastNames[rand2.Next(1, lastNames.Count())];
                    myAccountCreate.mySuffix = null;//suffix[rand2.Next(1, 4)];
                    myAccountCreate.myDOB = rand2.Next(10, 12) + "/" + rand2.Next(10, 28) + "/" + rand2.Next(1951, 1996);
                    myApplication.myGender = "Male";

                    result = 1;
                }
                else
                {
                    Random rand3 = new Random();
                    myAccountCreate.myFirstName = femaleNames[rand3.Next(1, femaleNames.Count())];
                    myAccountCreate.myMiddleName = femaleMiddleNames[rand3.Next(1, femaleMiddleNames.Count())];
                    myAccountCreate.myLastName = lastNames[rand3.Next(1, lastNames.Count())];
                    myAccountCreate.mySuffix = null;//suffix[rand3.Next(1, 4)];
                    myAccountCreate.myDOB = rand3.Next(10, 12) + "/" + rand3.Next(10, 28) + "/" + rand3.Next(1951, 1996);
                    myApplication.myGender = "Female";

                    string temp1;
                    temp1 = myAccountCreate.myDOB;
                    result = 1;
                }
            }
            else
            {
                //<65 and >18 years old, otherwise need logic to handle other scenarios
                if (rand.Next(1, 3) == 1)
                {
                    Random rand4 = new Random();
                    myAccountCreate.myFirstName = maleNames[rand4.Next(1, maleNames.Count())];
                    myAccountCreate.myMiddleName = maleMiddleNames[rand4.Next(1, maleMiddleNames.Count())];
                    myAccountCreate.myLastName = lastNames[rand4.Next(1, lastNames.Count())];
                    myAccountCreate.mySuffix = null;// suffix[rand4.Next(1, 4)];
                    myAccountCreate.myDOB = rand4.Next(10, 12) + "/" + rand4.Next(10, 28) + "/" + rand4.Next(1951, 1996);
                    myApplication.myGender = "Male";

                    result = 1;
                }
                else
                {
                    Random rand5 = new Random();
                    myAccountCreate.myFirstName = femaleNames[rand5.Next(1, femaleNames.Count())];
                    myAccountCreate.myMiddleName = femaleMiddleNames[rand5.Next(1, femaleMiddleNames.Count())];
                    myAccountCreate.myLastName = lastNames[rand5.Next(1, lastNames.Count())];
                    myAccountCreate.mySuffix = null;// suffix[rand5.Next(1, 4)];
                    myAccountCreate.myDOB = rand5.Next(10, 12) + "/" + rand5.Next(10, 28) + "/" + rand5.Next(1951, 1996);
                    myApplication.myGender = "Female";

                    string temp1;
                    temp1 = myAccountCreate.myDOB;
                    result = 1;
                }
            }

            if (myHistoryInfo.myEnvironment == "STST2")
            {
                myAccountCreate.myFirstName = "SS" + myAccountCreate.myFirstName;
                myAccountCreate.myLastName = "SS" + myAccountCreate.myLastName;
            }
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

            string conString = Properties.Settings.Default.Database1ConnectionString;
            SqlCeConnection con;
            try
            {
                con = new SqlCeConnection(conString);
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand(
                    "SELECT * FROM Application where TestID = " + myTest.myTestId, con))
                {
                    SqlCeDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        string myUpdateString;
                        myUpdateString = "Update Application set Gender = @Gender where TestID = " + myTest.myTestId;

                        using (SqlCeCommand com2 = new SqlCeCommand(myUpdateString, con))
                        {
                            com2.Parameters.AddWithValue("Gender", myApplication.myGender);
                            com2.ExecuteNonQuery();
                            com2.Dispose();
                        }
                    }
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Update gender didn't work");
            }

            return result;
        }

        public int GenerateHouseholdNames(ref mystructHouseholdMembers myHouseholdMembers, int testId, string householdMember, ref mystructHistoryInfo myHistoryInfo)
        {
            int result;

            //household 2
            string[] maleNames2 = { "Abdul", "Abel", "Bart", "Calvin", "Carter", "Darren", "Ed", "Felix", "Gary", "Harvey", "Heath", "Isaac", "Jack", "Jake", "Keith", "Lee", "Mac", "Max", "Nate", "Noah", "Otto", "Paul", "Ray", "Sam", "Scott", "Taylor", "Theo", "Tanner", "Ulysses", "Victor", "Wade", "Yong", "Zack" };
            string[] femaleNames2 = { "Allison", "Bebe", "Becky", "Cami", "Daisy", "Dani", "Estelle", "Fay", "Gayle", "Hanna", "Ida", "Jennifer", "Kali", "Lacie", "Mable", "Melissa", "Naomi", "Natalia", "Olivia", "Pam", "Rachel", "Sabrina", "Taisha", "Tabitha", "Tamara", "Teri", "Toni", "Ula", "Val", "Wendy", "Yoko", "Zelda" };
            string[] lastNames2 = { "Abbott", "Abrams", "Albert", "Adler", "Babcock", "Backman", "Bacon", "Bailey", "Ballard", "Best", "Bradley", "Cable", "Cade", "Callaway", "Callahan", "Daley", "Damon", "Danner", "Darby", "Daniels", "Eagle", "Fairbanks", "Gage", "Hackney", "Ireland", "Johnson", "Jackson", "Kane", "Ladd", "Landers", "Miller", "Nelson", "Oconnor", "Peterson", "Rafferty", "Sadler", "Salmon", "Salter", "Samson", "Taber", "Tang", "Underhill", "Vale", "Winters", "York", "Zimmermann" };

            //household 3
            string[] maleNames3 = { "Abe", "Abraham", "Ben", "Carl", "Chad", "Dale", "Elliot", "Fred", "Glen", "Herb", "Homer", "Ivan", "Jason", "John", "Ken", "Logan", "Michael", "Mitch", "Norman", "Nigel", "Owen", "Peter", "Randy", "Sean", "Simon", "Toby", "Todd", "Tony", "Ulys", "Vince", "Wyatt", "Yang", "Zane" };
            string[] femaleNames3 = { "April", "Beth", "Billi", "Candy", "Dolly", "Dixie", "Edith", "Faith", "Gloria", "Hazel", "Iris", "Jessica", "Katy", "Leigh", "Martha", "Marisa", "Nita", "Nichole", "Odell", "Perl", "Rikki", "Shawn", "Tora", "Terra", "Tonya", "Tonette", "Tomika", "Ursula", "Vanessa", "Wynona", "Ying", "Zula" };
            string[] lastNames3 = { "Albright", "Andrews", "Ashford", "Arthur", "Bradford", "Bernard", "Branch", "Branson", "Braxton", "Brady", "Brandt", "Cameron", "Cambell", "Corbin", "Cope", "Devlin", "Dexter", "Dicksin", "Drummond", "Driscoll", "East", "Farmer", "Galvan", "Hall", "Irish", "Jamison", "Jay", "Keeler", "Landis", "Lemon", "Madson", "Neal", "Oleary", "Pepper", "Ramey", "Shay", "Sherwood", "Shields", "Shipmen", "Tanner", "Tetter", "Upton", "Vance", "Wingate", "Young", "Zhang" };


            Random rand6 = new Random();
            if (householdMember == "2")
            {
                if (myHouseholdMembers.myGender != null && myHouseholdMembers.myGender != "")
                {
                    if (myHouseholdMembers.myGender == "Male")
                    {
                        Random rand7 = new Random();
                        myHouseholdMembers.myFirstName = maleNames2[rand7.Next(1, maleNames2.Count())];
                        myHouseholdMembers.myLastName = lastNames2[rand7.Next(1, lastNames2.Count())];
                        myHouseholdMembers.myGender = "Male";
                        result = 1;
                    }
                    else
                    {
                        Random rand8 = new Random();
                        myHouseholdMembers.myFirstName = femaleNames2[rand8.Next(1, femaleNames2.Count())];
                        myHouseholdMembers.myLastName = lastNames2[rand8.Next(1, lastNames2.Count())];
                        myHouseholdMembers.myGender = "Female";
                        result = 1;
                    }
                }
                else
                {
                    if (rand6.Next(1, 3) == 1)
                    {
                        Random rand9 = new Random();
                        myHouseholdMembers.myFirstName = maleNames2[rand9.Next(1, maleNames2.Count())];
                        myHouseholdMembers.myLastName = lastNames2[rand9.Next(1, lastNames2.Count())];
                        myHouseholdMembers.myGender = "Male";
                        result = 1;
                    }
                    else
                    {
                        Random rand10 = new Random();
                        myHouseholdMembers.myFirstName = femaleNames2[rand10.Next(1, femaleNames2.Count())];
                        myHouseholdMembers.myLastName = lastNames2[rand10.Next(1, lastNames2.Count())];
                        myHouseholdMembers.myGender = "Female";
                        result = 1;
                    }
                }
            }
            else
            {
                if (myHouseholdMembers.myGender != null && myHouseholdMembers.myGender != "")
                {
                    if (myHouseholdMembers.myGender == "Male")
                    {
                        Random rand11 = new Random();
                        myHouseholdMembers.myFirstName = maleNames3[rand11.Next(1, maleNames3.Count())];
                        myHouseholdMembers.myLastName = lastNames3[rand11.Next(1, lastNames3.Count())];
                        myHouseholdMembers.myGender = "Male";
                        result = 1;
                    }
                    else
                    {
                        Random rand12 = new Random();
                        myHouseholdMembers.myFirstName = femaleNames3[rand12.Next(1, femaleNames3.Count())];
                        myHouseholdMembers.myLastName = lastNames3[rand12.Next(1, lastNames3.Count())];
                        myHouseholdMembers.myGender = "Female";
                        result = 1;
                    }
                }
                else
                {
                    if (rand6.Next(1, 3) == 1)
                    {
                        Random rand13 = new Random();
                        myHouseholdMembers.myFirstName = maleNames3[rand13.Next(1, maleNames3.Count())];
                        myHouseholdMembers.myLastName = lastNames3[rand13.Next(1, lastNames3.Count())];
                        myHouseholdMembers.myGender = "Male";
                        result = 1;
                    }
                    else
                    {
                        Random rand14 = new Random();
                        myHouseholdMembers.myFirstName = femaleNames3[rand14.Next(1, femaleNames3.Count())];
                        myHouseholdMembers.myLastName = lastNames3[rand14.Next(1, lastNames3.Count())];
                        myHouseholdMembers.myGender = "Female";
                        result = 1;
                    }
                }
            }

            if (myHistoryInfo.myEnvironment == "STST2")
            {
                myHouseholdMembers.myFirstName = "SS" + myHouseholdMembers.myFirstName;
                myHouseholdMembers.myLastName = "SS" + myHouseholdMembers.myLastName;
            }

            SqlCeConnection con;
            string conString = Properties.Settings.Default.Database1ConnectionString;
            if (householdMember == "2")
            {
                try
                {
                    con = new SqlCeConnection(conString);
                    con.Open();
                    using (SqlCeCommand com3 = new SqlCeCommand(
                        "SELECT * FROM Housemembers where TestID = " + testId + " and HouseMembersID = 2", con))
                    {
                        SqlCeDataReader reader = com3.ExecuteReader();
                        if (reader.Read())
                        {
                            string myUpdateString;
                            myUpdateString = "Update HouseMembers set Gender = @Gender, FirstName = @First, LastName = @Last where TestID = " + testId + " and HouseMembersID = 2";

                            using (SqlCeCommand com4 = new SqlCeCommand(myUpdateString, con))
                            {
                                com4.Parameters.AddWithValue("Gender", myHouseholdMembers.myGender);
                                com4.Parameters.AddWithValue("First", myHouseholdMembers.myFirstName);
                                com4.Parameters.AddWithValue("Last", myHouseholdMembers.myLastName);
                                com4.ExecuteNonQuery();
                                com4.Dispose();
                            }
                        }
                    }
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Update house member 2 names didn't work");
                }
            }
            else
            {
                try
                {
                    con = new SqlCeConnection(conString);
                    con.Open();
                    using (SqlCeCommand com5 = new SqlCeCommand(
                        "SELECT * FROM Housemembers where TestID = " + testId + " and HouseMembersID = 3", con))
                    {
                        SqlCeDataReader reader = com5.ExecuteReader();
                        if (reader.Read())
                        {
                            string myUpdateString;
                            myUpdateString = "Update HouseMembers set Gender = @Gender, FirstName = @First, LastName = @Last where TestID = " + testId + " and HouseMembersID = 3";

                            using (SqlCeCommand com6 = new SqlCeCommand(myUpdateString, con))
                            {
                                com6.Parameters.AddWithValue("Gender", myHouseholdMembers.myGender);
                                com6.Parameters.AddWithValue("First", myHouseholdMembers.myFirstName);
                                com6.Parameters.AddWithValue("Last", myHouseholdMembers.myLastName);
                                com6.ExecuteNonQuery();
                                com6.Dispose();
                            }
                        }
                    }
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Update house member 3 names didn't work");
                }
            }

            return result;
        }

    }
}
