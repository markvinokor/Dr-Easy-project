using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace Dr.Easy
{
    class LocalConfig
    {
        SQL_Settings Fetch = new SQL_Settings();
        public string SQL()
        {
            string newstr = Fetch.SQLConnection();
            return newstr;
        }
        public void Username(string user)
        {
            FileStream f = new FileStream("Config.inf", FileMode.Create);
            StreamWriter s = new StreamWriter(f);
            s.WriteLine(user);
            s.Close();
            f.Close();
        }

        public void Password(string pass)
        {
            FileStream f = new FileStream("Config.Bin", FileMode.Create);
            StreamWriter s = new StreamWriter(f);
            s.WriteLine(pass);
            s.Close();
            f.Close();
        }

        public string User()
        {
            string str = "";
            StreamReader sr = new StreamReader("Config.inf");
            str = sr.ReadLine();
            sr.Close();
            return str;
        }
        
        public string Pass()
        {
            string str = "";
            StreamReader sr = new StreamReader("Config.Bin");
            str = sr.ReadLine();
            sr.Close();
            return str;
        }

        public void Disable()
        {
            FileStream f = new FileStream("Config.inf", FileMode.Create);
            StreamWriter s = new StreamWriter(f);
            s.WriteLine("0");
            s.Close();
            f.Close();

            FileStream n = new FileStream("Config.Bin", FileMode.Create);
            StreamWriter m = new StreamWriter(n);
            m.WriteLine("0");
            m.Close();
            n.Close();
        }

        public void DisablePass()
        {
            FileStream f = new FileStream("Config.Bin", FileMode.Create);
            StreamWriter s = new StreamWriter(f);
            s.WriteLine("0");
            s.Close();
            f.Close();
        }


        public int CreateFile(string CaseID)
        {
            string date="", time="", dr="", license="", location="", reason="", details="", result="", etc="";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Data] WHERE [CaseID]='" + CaseID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                date = mySqlDataReader["CreateDate"].ToString();
                time = mySqlDataReader["CreateTime"].ToString();
                dr = mySqlDataReader["DoctorName"].ToString();
                license = mySqlDataReader["DrLicense"].ToString();
                location = mySqlDataReader["VisitLocation"].ToString();
                reason = mySqlDataReader["VisitReason"].ToString();
                details = mySqlDataReader["Event"].ToString();
                result = mySqlDataReader["Result"].ToString();
                etc = mySqlDataReader["Notes"].ToString();
            }

            mySqlDataReader.Close();
            mySqlConnection.Close();

            if (date == "")
                return 0;

            FileStream f = new FileStream("FileID.html", FileMode.Create);
            StreamWriter s = new StreamWriter(f);
            s.WriteLine("<html>");
            s.WriteLine("<head>");
            s.WriteLine("<meta charset =\"utf-8\">");
            s.WriteLine("<title>"+ CaseID +"</title>");
            s.WriteLine("<style>");
            s.WriteLine("table {");
            s.WriteLine("font-size: 28px;");
            s.WriteLine("background-color: lightblue;");
            s.WriteLine("}");
            s.WriteLine("</style>");
            s.WriteLine("</head>");
            s.WriteLine("<body dir=\"rtl\" height=\"100%\">");
            s.WriteLine("<table border=\"1\" width=\"100%\" valign=\"top\">");
            s.WriteLine("<center>");
            s.WriteLine("<font>");
            s.WriteLine("<tr>");
            s.WriteLine("<td align=\"center\" colspan=\"5\"><h1>מסמך רפואי</h1></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td align=\"center\"><b>תאריך</td>");
            s.WriteLine("<td align=\"center\"><b>שעה</td>");
            s.WriteLine("<td align=\"center\"><b>רופא מטפל</td>");
            s.WriteLine("<td align=\"center\"><b>מספר רישיון</td>");
            s.WriteLine("<td align=\"center\"><b>מרפאה</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td align=\"center\"><b>" + date + "</td>");
            s.WriteLine("<td align=\"center\"><b>" + time + "</td>");
            s.WriteLine("<td align=\"center\"><b>" + dr + "</td>");
            s.WriteLine("<td align=\"center\"><b>" + license + "</td>");
            s.WriteLine("<td align=\"center\"><b>" + location + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("</table>");
            s.WriteLine("<table border=\"0\" width=\"100%\" height=\"100%\" valign=\"top\">");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" bgcolor=\"#9bcbff\"><br></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" align=\"right\"><h1>סיבת הביקור</h1></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" height=\"100px\" valign=\"top\">" + reason + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" bgcolor=\"#9bcbff\"><br></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td align=\"right\" colspan=\"5\"><h1>אירוע</h1></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" height=\"200px\" valign=\"top\">" + details + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" bgcolor=\"#9bcbff\"><br></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" align=\"right\"><h1>תוצאות</h1></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" height=\"200px\" valign=\"top\">" + result + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" bgcolor=\"#9bcbff\"><br></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" align=\"right\"><h1>הערות</h1></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" height=\"200px\" valign=\"top\">" + etc + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td height=\"100px\" colspan=\"5\" bgcolor=\"#9bcbff\"><br></td>");
            s.WriteLine("</tr>");
            s.WriteLine("</font>");
            s.WriteLine("</table>");
            s.WriteLine("</body>");
            s.WriteLine("</html>");
            s.Close();
            f.Close();

            return 1;
        }
    
    }
}
