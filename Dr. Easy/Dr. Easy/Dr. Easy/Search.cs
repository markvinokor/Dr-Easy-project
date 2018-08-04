using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Dr.Easy
{
    class Search
    {
        SQL_Settings Fetch = new SQL_Settings();
        public string SQL()
        {
            string newstr = Fetch.SQLConnection();
            return newstr;
        }
        public Boolean ID(string a)
        {
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
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
        public string Name(string a)
        {
            MultiChecks Fix = new MultiChecks();
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["Name"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            strs = Fix.Space(strs);
            return strs;
        }

        public string Last(string a)
        {
            MultiChecks Fix = new MultiChecks();
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["Last"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            strs = Fix.Space(strs);
            return strs;
        }
        public string Gender(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["Gender"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }

        public string BloodType(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["BloodType"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }

        public string Age(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["Age"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }
        public string BirthDate(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["BirthDate"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }
        public string Address(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["Address"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }

        public string City(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["City"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }

        public string BirthPlace(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["BirthPlace"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }

        public string Telephone(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["Telephone"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }


        public string Mobile(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["Mobile"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }

        public string FileID(string a)
        {
            string strs = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Clients] WHERE [ID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                strs += mySqlDataReader["FileID"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return strs;
        }



        public DataTable BuildTable(string a)
        {
            DataTable DT = new DataTable(); ;
            DT.Columns.Add("תאריך יצירה");
            DT.Columns.Add("שעת יצירה");
            DT.Columns.Add("סיבת הביקור");
            DT.Columns.Add("האירוע");
            DT.Columns.Add("תוצאות");
            DT.Columns.Add("הערות");
            DT.Columns.Add("מס' קישור");

            
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Data] WHERE [FileID]='" + a + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                DT.Rows.Add(new object[] { mySqlDataReader["CreateDate"].ToString(), mySqlDataReader["CreateTime"].ToString(), mySqlDataReader["VisitReason"].ToString(), mySqlDataReader["Event"].ToString().ToString(), mySqlDataReader["Result"].ToString().ToString(), mySqlDataReader["Notes"].ToString().ToString(), mySqlDataReader["CaseID"].ToString().ToString() });
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return DT;

        }

    }
}
