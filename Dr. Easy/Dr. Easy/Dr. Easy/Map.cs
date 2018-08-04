using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;


namespace Dr.Easy
{
    class Map
    {
        SQL_Settings Fetch = new SQL_Settings();
        public string SQL()
        {
            string newstr = Fetch.SQLConnection();
            return newstr;
        }

        public Boolean Bed(int Room, int BedID)
        {
            int Occupied = 0;
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Map] WHERE [RoomID]='" + Room + "' AND [BedID]='" + BedID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Occupied = int.Parse(mySqlDataReader["Occupied"].ToString());
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            if(Occupied==1)
                return true;
            return false;
        }

        public string Department(int Room, int BedID)
        {
            MultiChecks Fix = new MultiChecks();
            string Department = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Map] WHERE [RoomID]='" + Room + "' AND [BedID]='" + BedID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Department = mySqlDataReader["Unit"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            Department = Fix.Space(Department);
            return Department;
        }

        public string Room(int Room)
        {
            string RoomLabel = "חדר ";
            RoomLabel += Room.ToString();
            return RoomLabel;
        }

        public string BedNumber(int Bed)
        {
            MultiChecks Fix = new MultiChecks();
            string BedLabel = "מיטה מס' ";
            BedLabel += Bed.ToString();
            return BedLabel;
        }

        public string Occupied(int Room, int BedID)
        {
            int Occupied = 0;
            string StrOccupied = "מיטה בשימוש", StrUnOccupied="מיטה פנויה";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Map] WHERE [RoomID]='" + Room + "' AND [BedID]='" + BedID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Occupied = int.Parse(mySqlDataReader["Occupied"].ToString());
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            if (Occupied == 1)
            {
                return StrOccupied;
            }
            return StrUnOccupied;
        }

        public string ClientFullName(int Room, int BedID)
        {
            MultiChecks Fix = new MultiChecks();
            Search Get = new Search();
            string ClientFullName = "",Temp="שם מלא: ";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Map] WHERE [RoomID]='" + Room + "' AND [BedID]='" + BedID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                ClientFullName = mySqlDataReader["ClientID"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();

            Temp += Get.Name(ClientFullName);
            Temp += " " + Get.Last(ClientFullName);
            ClientFullName = Temp;
            return ClientFullName;
        }

        public string ClientID(int Room, int BedID)
        {
            MultiChecks Fix = new MultiChecks();
            Search Get = new Search();
            string ClientID = "", Temp = "תעודת זהות: ";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Map] WHERE [RoomID]='" + Room + "' AND [BedID]='" + BedID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                ClientID = mySqlDataReader["ClientID"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();

            ClientID = Fix.Space(ClientID);

            Temp += ClientID;
            ClientID = Temp;
            return ClientID;
        }

        public string OnlyClientID(int Room, int BedID)
        {
            MultiChecks Fix = new MultiChecks();
            string ClientID = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Map] WHERE [RoomID]='" + Room + "' AND [BedID]='" + BedID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                ClientID = mySqlDataReader["ClientID"].ToString();
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            ClientID = Fix.Space(ClientID);
            return ClientID;
        }

        public int Occupation(string Department)
        {
            int counter = 0;
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Map] WHERE [Unit]='" + Department + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                counter += int.Parse(mySqlDataReader["Occupied"].ToString());
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return counter;
        }
    }
}
