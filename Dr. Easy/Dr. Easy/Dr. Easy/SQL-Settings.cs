using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dr.Easy
{
    class SQL_Settings
    {
        public string EncryptedPassword(string FakePass1, string FakePass2, string FakePass3)
        {
            string Hidden = "";
            int i = 0;
            foreach (char c in FakePass1)
            {
                if (c == '9' && c != '8' && !(c >= 'A' && c <= 'Z'))
                {
                    if (i % 2 == 0) Hidden += '1'; else Hidden += "";
                    i++;
                }
                else Hidden += "";
            }
            foreach (char f in FakePass3)
            {
                if (f <= '9' && f >= '0') Hidden += '2'; else Hidden += "";
            }
            string StillFake = Hidden;
            foreach (char c in FakePass2)
            {
                if (c >= 'A' && c <= 'Z') StillFake += 'W' + Hidden;
            }
            return StillFake;
        }
        public string SQLConnection()
        {
            string Server = "Server = 25.10.71.237,1433\\SQLEXPRESS;";
            string DataBase = "Database = Dr.Easy;";
            string UserID = "User ID = Avenger;";
            string Password = "Password ="+ EncryptedPassword("B54A8529","AB","realpa5s")+";";
            string SQL = Server+ DataBase+ UserID+ Password;
            return SQL;
        }

    }
}
