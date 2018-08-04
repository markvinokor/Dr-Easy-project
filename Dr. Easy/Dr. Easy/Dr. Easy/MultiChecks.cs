using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Dr.Easy
{
    class MultiChecks
    {
        SQL_Settings Fetch = new SQL_Settings();
        public string SQL()
        {
            string newstr = Fetch.SQLConnection();
            return newstr;
        }

        public Boolean NoLetters(string str)
        {
            foreach (char c in str)
            {
                if (!(c >= '0' && c <= '9'))
                {
                    return false;
                }
            }
            return true;
        }

        public Boolean NoNumbers(string str)
        {
            foreach (char c in str)
            {
                if (!(c >= 'א' && c <= 'ת') && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public Boolean ValidGender(string str)
        {
            if (str == "זכר" || str == "נקבה")
                return true;
            return false;
        }


        public Boolean ValidBloodType(string str)
        {
            if (str == "A+" || str == "B+" || str == "AB+" || str == "O+")
                return true;
            if (str == "A-" || str == "B-" || str == "AB-" || str == "O-")
                return true;
            if (str == "לא ידוע")
                return true;
            return false;
        }

        public Boolean ValidID(string str)
        {
            int count = 0;
            foreach (char c in str)
            {
                if (!(c >= '0' && c <= '9'))
                {
                    return false;
                }
                count++;
            }
            if (count == 9)
                return true;
            else
                return false;
        }

        public string Space(string str)
        {
            string newstr = "";
            foreach (char c in str)
            {
                if (c != ' ' && c != '?')
                {
                    newstr += c;
                }
                if (c == '?')
                {
                    newstr += ' ';
                }
            }
            return newstr;
        }

        public string GetAge(string a, string b, string d)
        {
            int age = 0;
            int counter = 0;
            string year = "", month = "", day = "";
            foreach (char c in a)
            {
                if (counter <= 1) day += c;
                if (counter > 2 && counter <= 4) month += c;
                if (counter > 5) year += c;
                counter++;
            }
            var today = DateTime.Today;
            int now = ((((today.Year * 100) + today.Month) * 100) + today.Day);
            int dob = (((int.Parse(year) * 100 + int.Parse(month)) * 100) + int.Parse(day));
            age = ((now - dob) / 10000);
            Search Seek = new Search();
            if (age != int.Parse(b))
            {
                int n;
                SqlConnection mySqlConnection = new SqlConnection(SQL());
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                mySqlCommand.CommandText = "UPDATE [Clients] SET [Age]='" + age.ToString() + "' WHERE [ID]='" + d.ToString() + "'";
                n = mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            return age.ToString();
        }


        public string Age(string BirthDate)
        {
            if (BirthDate.Length == 10)
            {
                int age = 0;
                int counter = 0;
                string year = "", month = "", day = "";
                foreach (char c in BirthDate)
                {
                    if (counter <= 1) day += c;
                    if (counter > 2 && counter <= 4) month += c;
                    if (counter > 5) year += c;
                    counter++;
                }
                var today = DateTime.Today;
                int now = ((((today.Year * 100) + today.Month) * 100) + today.Day);
                int dob = (((int.Parse(year) * 100 + int.Parse(month)) * 100) + int.Parse(day));
                age = ((now - dob) / 10000);
                return age.ToString();
            }
            else
                return "";
        }

        public string ValidDate(string str)
        {
            string NewDate = "";
            foreach (char c in str)
            {
                if (c != ' ')
                {
                    NewDate += c;
                }
                else
                    return NewDate;
            }
            return NewDate;
        }

        public string FullName(string username)
        {
            string str = "", Name = "", Last = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + username + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Name = mySqlDataReader["Name"].ToString();
                Last = mySqlDataReader["Last"].ToString();
            }
            MultiChecks Remove = new MultiChecks();

            str += Remove.Space(Name) + " ";
            str += Remove.Space(Last);

            mySqlDataReader.Close();
            mySqlConnection.Close();
            return str;
        }

        public string HealthInsurance(string Status, string HealthInsurance)
        {
            string newstr = "";
            if (Status == "Yes")
            {
                newstr = HealthInsurance;
            }
            else
            {
                newstr = Status;
            }
            return newstr;
        }

        public string Address(string Address)
        {
            string newstr = "";
            foreach (char c in Address)
            {
                if ((c >= '0' && c <= '9') || (c >= 'א' && c<='ת') || c ==' ')
                {
                    if (c == ' ')
                        newstr += '?';
                    else
                        newstr += c;
                }
            }
            return newstr;
        }


    }
}
