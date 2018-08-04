using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Dr.Easy
{
    class User
    {
        SQL_Settings Fetch = new SQL_Settings();
        public string SQL()
        {
            string newstr = Fetch.SQLConnection();
            return newstr;
        }
        public string FullName(string a)
        {
            string str = "",name="",last="";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                last = mySqlDataReader["Last"].ToString();
                name = mySqlDataReader["Name"].ToString();
            }
            MultiChecks Remove = new MultiChecks();

            str += Remove.Space(last)+" ";
            str += Remove.Space(name);

            mySqlDataReader.Close();
            mySqlConnection.Close();
            return str;
        }

        public string Department(string a)
        {
            string str = "", department = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                department = mySqlDataReader["Department"].ToString();
            }
            MultiChecks Remove = new MultiChecks();

            str += Remove.Space(department);

            mySqlDataReader.Close();
            mySqlConnection.Close();
            return str;
        }


        public string WorkID(string a)
        {
            string strs = "מס' עובד\\ת: ";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["LicenseNumber"].ToString();
            }

            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }

        public int PicID(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs = mySqlDataReader["ImageID"].ToString();
            }
            int PicID = int.Parse(strs);
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return PicID;
        }

                public string License(string a)
        {
            string strs = "מס' רישיון: ";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["LicenseNumber"].ToString();
            }

            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }
    }
}
