using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Dr.Easy
{
    class Login
    {
        SQL_Settings Set = new SQL_Settings();
        
        public Boolean Authorization(string a,string b)
        {
            string SQL = Set.SQLConnection();
            SqlConnection mySqlConnection = new SqlConnection(SQL);
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM Login WHERE [Username]='"+ a + "' AND [Pass]='"+b+"';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                mySqlDataReader.Close();
                mySqlConnection.Close();
                return true;
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return false;
        }

        public int Level(string a)
        {
            string SQL = Set.SQLConnection();
            string LevelID = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL);
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                LevelID = mySqlDataReader["Level"].ToString();

            }
            mySqlDataReader.Close();
            mySqlConnection.Close();

            return (int.Parse(LevelID));

        }

        public string GetUserInfo(string Username)
        {
            string SQL = Set.SQLConnection();
            User Get = new User();
            MultiChecks Fetch = new MultiChecks();
            string newstr = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL);
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + Username + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                newstr = mySqlDataReader["Title"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();

            newstr = Fetch.Space(newstr);

            newstr += " " + Get.FullName(Username);
            return newstr;
        }

    }
}
