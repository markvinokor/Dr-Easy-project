using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Dr.Easy
{
    class AddData
    {
        SQL_Settings Fetch = new SQL_Settings();
        public string SQL()
        {
            string newstr = Fetch.SQLConnection();
            return newstr;
        }
        public Boolean Client(string Name, string Last, string ID, string Gender, string BloodType, string Age, string Address, string City, string Telephone, string Mobile, string BirthPlace, string BirthDate, string HealthInsurance)
        {
            int n;
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlConnection.Open();
            mySqlCommand.CommandText = "INSERT INTO  Clients (Name,Last,ID,Gender,BloodType,Age,BirthDate,Address,City,BirthPlace,Telephone,Mobile,HealthInsurance) VALUES  ('" + Name + "','" + Last + "', '" + ID + "', '" + Gender + "', '" + BloodType + "', '" + Age + "', '" + BirthDate + "', '" + Address + "', '" + City + "', '" + BirthPlace + "', '" + Telephone + "', '" + Mobile + "', '" + HealthInsurance + "');";
            n = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (n > 0)
                return true;
            return false;
        }
    }
}
