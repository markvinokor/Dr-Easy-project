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
    class Messages
    {
        SQL_Settings Fetch = new SQL_Settings();
        public string SQL()
        {
            string newstr = Fetch.SQLConnection();
            return newstr;
        }
        public DataTable BuildReadTable(string a)
        {
            DataTable DT = new DataTable(); ;
            DT.Columns.Add("שם השולח");
            DT.Columns.Add("נושא ההודעה");
            DT.Columns.Add("תוכן ההודעה");
            DT.Columns.Add("התקבל בתאריך");
            DT.Columns.Add("מס' קישור להודעה");


            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Messages] WHERE [Username]='" + a + "' AND [Deleted]='0';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                DT.Rows.Add(new object[] { mySqlDataReader["SentFrom"].ToString(), mySqlDataReader["TitleMessage"].ToString(), mySqlDataReader["Message"].ToString(), mySqlDataReader["SentDate"].ToString().ToString(), mySqlDataReader["MessageID"].ToString().ToString() });
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return DT;

        }

        public DataTable BuildSentTable(string a)
        {
            DataTable DT = new DataTable(); ;
            DT.Columns.Add("נשלח אל");
            DT.Columns.Add("נושא ההודעה");
            DT.Columns.Add("תוכן ההודעה");
            DT.Columns.Add("נשלח בתאריך");
            DT.Columns.Add("מס' קישור להודעה");


            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Messages] WHERE [SentFrom]='" + a + "' AND [Deleted]='0';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                DT.Rows.Add(new object[] { mySqlDataReader["Username"].ToString(), mySqlDataReader["TitleMessage"].ToString(), mySqlDataReader["Message"].ToString(), mySqlDataReader["SentDate"].ToString().ToString(), mySqlDataReader["MessageID"].ToString().ToString() });
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return DT;

        }

        public DataTable BuildTrashTable(string a)
        {
            DataTable DT = new DataTable(); ;
            DT.Columns.Add("שם השולח");
            DT.Columns.Add("נושא ההודעה");
            DT.Columns.Add("תוכן ההודעה");
            DT.Columns.Add("התקבל בתאריך");
            DT.Columns.Add("מס' קישור להודעה");


            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Messages] WHERE [Username]='" + a + "' AND [Deleted]='1';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                DT.Rows.Add(new object[] { mySqlDataReader["SentFrom"].ToString(), mySqlDataReader["TitleMessage"].ToString(), mySqlDataReader["Message"].ToString(), mySqlDataReader["SentDate"].ToString().ToString(), mySqlDataReader["MessageID"].ToString().ToString() });
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return DT;

        }

        public Boolean NewMessage(string SentFrom, string SentTo, string Title, string Message)
        {
            int n;
            string[] DatePlusTime = DateTime.Now.ToString().Split(' ');
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlConnection.Open();
            mySqlCommand.CommandText = "INSERT INTO [Messages] (Username, SentDate, SentTime, Message, TitleMessage, SentFrom) VALUES ('" + SentTo + "', '" + DatePlusTime[0] + "', '" + DatePlusTime[1] + "', '" + Message + "', '" + Title + "', '" + SentFrom + "');";
            n = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            if (n > 0)
                return true;
            return false;
        }


        public string ReadCheck(int MessageID)
        {
            string Unread = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Messages] WHERE [MessageID]='" + MessageID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Unread = mySqlDataReader["Unread"].ToString();
            }
            MultiChecks Check = new MultiChecks();
            Unread = Check.Space(Unread);
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return Unread;
        }


        public string Respond(int MessageID)
        {
            string SentForm = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Messages] WHERE [MessageID]='" + MessageID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                SentForm = mySqlDataReader["SentFrom"].ToString();
            }
            MultiChecks Check = new MultiChecks();
            SentForm = Check.Space(SentForm);
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return SentForm;
        }

        public int ViewMessage(int MessageID)
        {
            MultiChecks Get = new MultiChecks();
            string SentDate = "", SentTime = "", SentFrom = "", Message = "", Title = "", FullName = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Messages] WHERE [MessageID]='" + MessageID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                SentFrom = mySqlDataReader["SentFrom"].ToString();
                SentDate = mySqlDataReader["SentDate"].ToString();
                SentTime = mySqlDataReader["SentTime"].ToString();
                Message = mySqlDataReader["Message"].ToString();
                Title = mySqlDataReader["TitleMessage"].ToString();
            }
            FullName = Get.FullName(SentFrom);
            mySqlDataReader.Close();
            mySqlConnection.Close();

            SentFrom = Get.Space(SentFrom);

            if (SentFrom == "")
                return 0;

            FileStream f = new FileStream("Message.html", FileMode.Create);
            StreamWriter s = new StreamWriter(f);
            s.WriteLine("<html>");
            s.WriteLine("<head>");
            s.WriteLine("<meta charset =\"utf-8\">");
            s.WriteLine("<title>" + MessageID + "</title>");
            s.WriteLine("<style>");
            s.WriteLine("table {");
            s.WriteLine("font-size: 24px;");
            s.WriteLine("background-color: white;");
            s.WriteLine("}");
            s.WriteLine("</style>");
            s.WriteLine("</head>");
            s.WriteLine("<body dir=\"rtl\" height=\"100%\">");
            s.WriteLine("<table border=\"1\" width=\"100%\" valign=\"top\">");
            s.WriteLine("<center>");
            s.WriteLine("<font>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" bgcolor=\"#9bcbff\"><br></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td align=\"right\" colspan=\"5\"><h1>" + Title + "</h1></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td width=\"30%\" align=\"right\"><table width=\"100%\" border=\"1\">");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"2\" bgcolor=\"#9bcbff\">נשלח מאת:</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td width=\"1%\" align=\"right\"><b><img src=\"../../Resources/" + SentFrom + ".png\" width=\"100px\" height=\"100px\"></td>");
            s.WriteLine("<td align=\"center\">" + FullName + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("</table></td>");
            s.WriteLine("<td colspan=\"3\" align=\"left\" valign=\"top\">");
            s.WriteLine("<table border=\"0\" valign=\"top\" width=\"100%\">");
            s.WriteLine("<tr valign=\"top\">");
            s.WriteLine("<td valign=\"top\" colspan=\"4\" bgcolor=\"#9bcbff\"><br></td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td align=\"left\">התקבל בתאריך:</td>");
            s.WriteLine("<td align=\"left\">" + SentDate + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td align=\"left\" width=\"77%\">התקבל בשעה:</td>");
            s.WriteLine("<td align=\"left\" width=\"1%\">" + SentTime + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("</table>");
            s.WriteLine("</td>");
            s.WriteLine("</tr>");
            s.WriteLine("</table>");
            s.WriteLine("<table border=\"1\" width=\"100%\" height=\"100%\" valign=\"top\">");
            s.WriteLine("<tr>");
            s.WriteLine("<td height=\"5%\" colspan=\"5\" bgcolor=\"#9bcbff\"><b>תוכן ההודעה</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td colspan=\"5\" valign=\"top\"><br>" + Message + "</td>");
            s.WriteLine("</tr>");
            s.WriteLine("<tr>");
            s.WriteLine("<td height=\"5%\" colspan=\"5\" bgcolor=\"#9bcbff\"><br></td>");
            s.WriteLine("</tr>");
            s.WriteLine("</font>");
            s.WriteLine("</table>");
            s.WriteLine("</body>");
            s.WriteLine("</html>");
            s.Close();
            f.Close();
            return 1;
        }

        public string DeleteCheck(int MessageID)
        {
            string Delete = "";
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Messages] WHERE [MessageID]='" + MessageID + "';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Delete = mySqlDataReader["Deleted"].ToString();
            }
            MultiChecks Check = new MultiChecks();
            Delete = Check.Space(Delete);
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return Delete;
        }


        public void SetDelete(int MessageID)
        {
            int n;
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlConnection.Open();
            mySqlCommand.CommandText = "UPDATE [Messages] SET [Deleted]='1' WHERE [MessageID]='" + MessageID + "';";
            n = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }


        public void Remove(int MessageID)
        {
            int n;
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlConnection.Open();
            mySqlCommand.CommandText = "DELETE FROM [Messages] WHERE [MessageID]='" + MessageID + "';";
            n = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }


        public void SetUnRead(int MessageID)
        {
            int n;
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlConnection.Open();
            mySqlCommand.CommandText = "UPDATE [Messages] SET [Unread]='0' WHERE [MessageID]='" + MessageID + "';";
            n = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public Boolean CheckDestination(string Destination)
        {

            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Login] WHERE [Username]='" + Destination + "';";
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


        public int UnreadMessages(string Username)
        {
            int counter = 0;
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM [Messages] WHERE [Username]='" + Username + "' AND [Unread]='1';";
            mySqlConnection.Open();
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                counter++;
            }
            mySqlDataReader.Close();
            mySqlConnection.Close();
            return counter;
        }

        public void UnDelete(int MessageID)
        {
            int n;
            SqlConnection mySqlConnection = new SqlConnection(SQL());
            SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlConnection.Open();
            mySqlCommand.CommandText = "UPDATE [Messages] SET [Deleted]='0' WHERE [MessageID]='" + MessageID + "';";
            n = mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

    }
}
