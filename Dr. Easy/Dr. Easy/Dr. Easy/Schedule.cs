using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Dr.Easy
{

    class Schedule
    {
        SQL_Settings Fetch = new SQL_Settings();
        public string SQL()
        {
            string newstr = Fetch.SQLConnection();
            return newstr;
        }

        public string Col(int Col)
        {
            string str = "";
            switch (Col)
            {
                case 1: str = "08:00"; break;
                case 2: str = "08:30"; break;
                case 3: str = "09:00"; break;
                case 4: str = "09:30"; break;
                case 5: str = "10:00"; break;
                case 6: str = "10:30"; break;
                case 7: str = "11:00"; break;
                case 8: str = "11:30"; break;
                case 9: str = "12:00"; break;
                case 10: str = "12:30"; break;
                case 11: str = "13:00"; break;
                case 12: str = "13:30"; break;
                case 13: str = "14:00"; break;
                case 14: str = "14:30"; break;
                case 15: str = "15:00"; break;
                case 16: str = "15:30"; break;
                case 17: str = "16:00"; break;
                case 18: str = "16:30"; break;
                case 19: str = "17:00"; break;
                case 20: str = "17:30"; break;
                case 21: str = "18:00"; break;
                case 22: str = "18:30"; break;
                case 23: str = "19:00"; break;
                case 24: str = "19:30"; break;
                case 25: str = "20:00"; break;
            }
            return str;
        }

        public string OnlyDate(string str)
        {
            string NewDate = "";
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || c == '/')
                {
                    NewDate += c;
                }
            }
            return NewDate;
        }



    }
}
