using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dr.Easy.Properties;
using System.IO;

namespace Dr.Easy
{
    public partial class Form1 : Form
    {
        int Config = 0, Message = 0, Count, Timer = 0, UnreadMessages = 0, Max, RoomID = 0;
        int ScreenX= Screen.PrimaryScreen.Bounds.Width, ScreenY= Screen.PrimaryScreen.Bounds.Height;
        string WindowID = "",Gender="", HealthInsurance="";
        Image myImage;
        public Form1()
        {
            InitializeComponent();
            MessageTimer.Start();
            MainMenuPanel.Left = 0;
            MainMenuPanel.Top = 0;
            MainPanel.Left = 0;
            MainPanel.Top = 0;
            OfficePanel.Left = 0;
            OfficePanel.Top = 0;
            MessagesPanel.Left = 170;
            MessagesPanel.Top = 150;
            NotificationPanel.Left = 0;
            NotificationPanel.Top = 0;
            LoginPanel.Left = 0;
            LoginPanel.Top = 0;
            Room980.Top = (ScreenY - UserPanel.Size.Height - Room980.Size.Height) /2;
            Room980.Left = (ScreenX - Room980.Size.Width)/2;
            AddEventButton.Top += 100;
            CloseFileButton.Top += 100;
            HospitalizationPanel.Top = (ScreenY - UserPanel.Size.Height - HospitalizationPanel.Size.Height);
            HospitalizationPanel.Left = (ScreenX - HospitalizationPanel.Size.Width) / 2;
            DepSlidePanel.Left = 0;
            MapBackground.Dock = DockStyle.Fill;
            MainPanel.Dock = DockStyle.Fill;
            OfficePanel.Dock = DockStyle.Fill;
            MainMenuPanel.Dock = DockStyle.Fill;
            SearchPanel.Dock = DockStyle.Right;
            DataFilesGridView.Dock = DockStyle.Fill;
            Panel_AddClient.Dock = DockStyle.Fill;
            Panel_Schedule.Dock = DockStyle.Fill;
            var SlideButtons = DepSlidePanel.Controls.OfType<Button>();
            foreach (Button EveryButton in SlideButtons)
            {
                EveryButton.Top = 33;
            }
            BackFromMap.Top = ScreenY - (UserPanel.Height) - (BackFromMap.Height) +38;
            BackFromMap.Left = ScreenX - (BackFromMap.Width) -10;
            LocalConfig Save = new LocalConfig();
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Config.inf"))
            {
                string user = Save.User();
                if (user != "0") { Config = 1; UsernameTB.Text = user; SaveUser.Checked = true; }
            }
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Config.Bin")){
                string pass = Save.Pass();
                if (pass != "0") { Config = 1; PassTB.Text = pass; SavePass.Checked = true; }
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            Login Access = new Login();
            User Get = new User();

            if (Access.Authorization(UsernameTB.Text, PassTB.Text) == true)
            {
                LogoutButton.Visible = true;
                MessageButton.Visible = true;
                LoginPanel.Visible = false;
                DetailA.Text = Access.GetUserInfo(UsernameTB.Text);
                DetailB.Text = Get.Department(UsernameTB.Text);
                switch (Get.PicID(UsernameTB.Text))
                {
                    case 0: myImage = Resources.Shlomo; break;
                    case 1: myImage = Resources.Mark; break;
                    case 2: myImage = Resources.Doctor; break;
                    case 3: myImage = Resources.Nurse; break;
                    case 4: myImage = Resources.Secretary; break;
                }
                UserPicture.BackgroundImage = myImage;

                if (Access.Level(UsernameTB.Text) == 3)
                {
                    MainMenuPanel.Visible = true;
                    LicenseLabel.Text = (Get.License(UsernameTB.Text).ToString());
                }
                if (Access.Level(UsernameTB.Text) == 0)
                {
                    OfficePanel.Visible = true;
                    LicenseLabel.Text = (Get.WorkID(UsernameTB.Text).ToString());
                }
            }
            else
            {
                MessageBox.Show("                   Login Falied                     ");
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            MapPanel.Visible = false;
            UnreadCounterLabel.Visible = false;
            MessagesPanel.Visible = false;
            OfficePanel.Visible = false;
            MainPanel.Visible = false;
            LoginPanel.Visible = true;
            UserPicture.BackgroundImage = Resources.User;
            DetailA.Text = "";
            DetailB.Text = "";
            LicenseLabel.Text = "";
            LogoutButton.Visible = false; 
            MainMenuPanel.Visible = false;
            IDSearch.Text = "";
            DataFilesGridView.DataSource = "";
            DataFilesGridView.Visible = true;
            WebBrowser.Visible = false;
            FirstNameA.Text = "";
            LastNameA.Text = "";
            AgeA.Text = "";
            AddressA.Text = "";
            CityA.Text = "";
            BloodTypeA.Text = "";
            GenderA.Text = "";
            BirthDateA.Text = "";
            BirthPlaceA.Text = "";
            TelephoneA.Text = "";
            MobileA.Text = "";
            IDA.Text = "";
            AddEventButton.Visible = false;
            CloseFileButton.Visible = false;
            MessageButton.Visible = false;

        }

        private void SearchAButton_Click(object sender, EventArgs e)
        {
            MultiChecks Check = new MultiChecks();
            Search Seek = new Search();
            if (IDSearch.Text != String.Empty)
            {
                if(Check.NoLetters(IDSearch.Text) == true)
                {
                    Boolean Result=Seek.ID(IDSearch.Text);
                    if (Result == true){
                        LogPanel.Visible = true;
                        ClientDetailsPanel.Visible = true;
                        AddEventButton.Visible = true;
                        AddEventButton.BringToFront();
                        FirstNameA.Text = Seek.Name(IDSearch.Text);
                        LastNameA.Text = Seek.Last(IDSearch.Text);
                        AgeA.Text = Check.GetAge(Seek.BirthDate(IDSearch.Text), Seek.Age(IDSearch.Text), IDSearch.Text);
                        AddressA.Text = Check.Space(Seek.Address(IDSearch.Text));
                        CityA.Text = Seek.City(IDSearch.Text);
                        BloodTypeA.Text = Seek.BloodType(IDSearch.Text);
                        GenderA.Text = Seek.Gender(IDSearch.Text);
                        BirthDateA.Text = Seek.BirthDate(IDSearch.Text);
                        BirthPlaceA.Text= Seek.BirthPlace(IDSearch.Text);
                        TelephoneA.Text = Seek.Telephone(IDSearch.Text);
                        MobileA.Text = Seek.Mobile(IDSearch.Text);
                        IDA.Text = IDSearch.Text;

                        DataFilesGridView.DataSource = Seek.BuildTable(Seek.FileID(IDSearch.Text));
                        DataFilesGridView.Columns[3].Width = 550;
                        DataFilesGridView.Columns[4].Width = 400;

                        DataFilesGridView.Sort(DataFilesGridView.Columns["תאריך יצירה"], ListSortDirection.Descending);
                        DataFilesGridView.Dock = DockStyle.Fill;

                        
                    }
                    else
                    {
                        var AllLabels = ClientDetailsPanel.Controls.OfType<Label>();
                        foreach (Label Label in AllLabels)
                            if (Label.BackColor == Color.Gray)
                                Label.Text = "";
                        DataFilesGridView.DataSource = "";
                        DataFilesGridView.Visible = true;
                        WebBrowser.Visible = false;
                        CloseFileButton.Visible = false;
                        AddEventButton.Visible = false;
                        MessageBox.Show("לא נמצאו תוצאות");
                    }
                }
                else
                {
                    MessageBox.Show("אנא רשום מספרים בלבד בשדה תעודת זהות");
                }
            }
            else
            {
                MessageBox.Show("אנא מלא את השדות הנחוצים לתחילת החיפוש");
            }
        }

        private void SaveUser_CheckedChanged(object sender, EventArgs e)
        {
            LocalConfig Save = new LocalConfig();
            if (SaveUser.Checked == true)
            {
                if(UsernameTB.Text != String.Empty)
                {
                    Save.Username(UsernameTB.Text);
                }
                else
                {
                    MessageBox.Show("שם משתמש ריק, אנא מלא ונסה שוב");
                    SaveUser.Checked = false;
                }
            }
            else
            {
                Save.Disable();
                SaveUser.Checked = false;
                SavePass.Checked = false;
                Config = 0;
            }
        }

        private void SavePass_CheckedChanged(object sender, EventArgs e)
        {
            LocalConfig Save = new LocalConfig();
            if (SavePass.Checked == true)
            {
                if (PassTB.Text != String.Empty)
                {
                    if(SaveUser.Checked == true)
                    {
                        Save.Password(PassTB.Text);
                        if(Config==0)
                            MessageBox.Show("הסיסמה נשמרה, אך מומלץ שלא להשתמש באפשרות זו");
                    }
                    else
                    {
                        SavePass.Checked = false;
                        if (Config == 0)
                            MessageBox.Show("חייב לשמור קודם שם משתמש כדי שתוכל לשמור סיסמא");
                    }
                }
                else
                {
                    SavePass.Checked = false;
                    if (Config == 0)
                        MessageBox.Show("שדה הסיסמא ריק, אנא מלא ונסה שוב");
                }
            }
            else
            {
                Save.DisablePass();
                Config = 0;
            }
        }

        private void DataFilesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Result;
            if (e.ColumnIndex == 6)
            {
                LocalConfig Do = new LocalConfig();
                Result = Do.CreateFile(DataFilesGridView[e.ColumnIndex, e.RowIndex].Value.ToString());
                if (Result == 1)
                {
                    WebBrowser.Navigate(Directory.GetCurrentDirectory() + @"\FileID.html");
                    WebBrowser.Visible = true;
                    DataFilesGridView.Visible = false;
                    AddEventButton.Visible = false;
                    CloseFileButton.Visible = true;
                }
                else
                {
                    MessageBox.Show("קובץ לא נמצא, אנא צור קשר עם התמיכה");
                }
            }
        }

        private void CloseFileButton_Click(object sender, EventArgs e)
        {
            WebBrowser.Visible = false;
            CloseFileButton.Visible = false;
            DataFilesGridView.Visible = true;
            AddEventButton.Visible = true;
        }

        private void ClientsButton_Click(object sender, EventArgs e)
        {
            MainMenuPanel.Visible = false;
            MainPanel.Visible = true;
            MainPanel.BringToFront();
        }

        private void ClientsBackButton_Click(object sender, EventArgs e)
        {
            MainMenuPanel.Visible = true;
            MainPanel.Visible = false;
            IDSearch.Text = "";
            DataFilesGridView.DataSource = "";
            DataFilesGridView.Visible = true;
            WebBrowser.Visible = false;
            var AllLabels = ClientDetailsPanel.Controls.OfType<Label>();
            foreach (Label Label in AllLabels)
            {
                if(Label.BackColor==Color.Gray)
                    Label.Text = "";
            }
            AddEventButton.Visible = false;
            CloseFileButton.Visible = false;
        }

        private void IDSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchAButton.Focus();
                SearchAButton_Click(null, EventArgs.Empty);
            }
        }

        private void AddClientButton_Click(object sender, EventArgs e)
        {
            Panel_AddClient.Visible = true;
            Panel_AddClient.BringToFront();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MultiChecks Check = new MultiChecks();
            AddData Add = new AddData();
            if(NameAdd.Text!=String.Empty && LastAdd.Text != String.Empty  && IDAdd.Text != String.Empty && BloodTypeAdd.Text != "" && BirthDateAdd.Text != String.Empty && AddressAdd.Text != String.Empty && CityAdd.Text != String.Empty && BirthPlaceAdd.Text != String.Empty && MobileAdd.Text != String.Empty)
            {
                if(Check.NoNumbers(NameAdd.Text) == true)
                {
                    if(Check.NoNumbers(LastAdd.Text) == true)
                    {
                        if(Check.NoLetters(AgeAdd.Text)== true)
                        {
                            if(Check.ValidID(IDAdd.Text) == true)
                            {
                                if((MaleCB.Checked == true || FemaleCB.Checked == true))
                                {
                                    if(Check.ValidBloodType(BloodTypeAdd.Text) == true)
                                    {
                                        if (Check.NoNumbers(CityAdd.Text) == true)
                                        {
                                            if (Check.NoLetters(TelephoneAdd.Text) == true)
                                            {
                                                if (Check.NoLetters(MobileAdd.Text) == true)
                                                {
                                                    if (Check.HealthInsurance(HealthInsurance, InsuranceTB.Text) != "")
                                                    {
                                                        HealthInsurance = Check.HealthInsurance(HealthInsurance, InsuranceTB.Text);
                                                        string Address = Check.Address(AddressAdd.Text);
                                                        if (Add.Client(NameAdd.Text, LastAdd.Text, IDAdd.Text, Gender, BloodTypeAdd.Text, AgeAdd.Text, Address, CityAdd.Text, TelephoneAdd.Text, MobileAdd.Text, BirthPlaceAdd.Text, BirthDateAdd.Text, HealthInsurance) == true)
                                                        {
                                                            
                                                            var AllTextBoxs = Panel_AddClient.Controls.OfType<TextBox>();
                                                            foreach (TextBox Text in AllTextBoxs)
                                                            {
                                                                Text.Text = String.Empty;
                                                            }
                                                            var AllBoxes = Panel_AddClient.Controls.OfType<CheckBox>();
                                                            foreach (CheckBox Box in AllBoxes)
                                                            {
                                                                Box.Checked = false;
                                                            }
                                                            InsuranceTB.Visible = false;
                                                            InsuranceNameLabel.Visible = false;
                                                            Panel_AddClient.Visible = false;
                                                            BloodTypeAdd.SelectedIndex = 0;
                                                            BloodTypeAdd.Text = "";
                                                            MessageBox.Show("לקוח הוסף למאגר בהצלחה");
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("שגיאה בהוספת הלקוח");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("אנא מלא את פרטי קופת החולים");
                                                    }
                                                    
                                                }
                                                else
                                                    MessageBox.Show("שדה פלאפון לא תקין אנא בדוק שוב יכול להכיל מספרים בלבד");
                                            }
                                            else
                                                MessageBox.Show("שדה טלפון לא תקין אנא בדוק שוב יכול להכיל מספרים בלבד");
                                        }
                                        else
                                            MessageBox.Show("שדה יישוב לא תקין אנא בדוק שוב");
                                    }
                                    else
                                        MessageBox.Show("שדה סוג דם ריק אנא מלא בהתאם");
                                }
                                else
                                    MessageBox.Show("אנא בחר זכר או נקבה בשדה מין");
                            }
                            else
                                MessageBox.Show("שדה תעודת זהות לא תקינה אנא בדוק שוב");
                        }
                        else
                            MessageBox.Show("שדה גיל יכול להכיל מספרים בלבד");
                    }
                    else
                        MessageBox.Show("שדה שם משפחה יכול להכיל אותיות בעברית בלבד");
                }
                else
                    MessageBox.Show("שדה שם פרטי יכול להכיל אותיות בעברית בלבד");
            }
            else
                MessageBox.Show("אנא מלא את כל השדות הנחוצים");
        }

        private void BirthDatePicker_ValueChanged(object sender, EventArgs e)
        {
            MultiChecks Check = new MultiChecks();
            BirthDateAdd.Text = Check.ValidDate(BirthDatePicker.Value.ToString());
        }




        private void MessageButton_Click(object sender, EventArgs e)
        {
            Messages Get = new Messages();

            MessageTable.DataSource = (Get.BuildReadTable(UsernameTB.Text));
            MessageTable.Sort(MessageTable.Columns["מס' קישור להודעה"], ListSortDirection.Descending);
            MessageTable.Columns[1].Width = 300;
            MessageTable.Columns[2].Width = 320;
            MessagesPanel.Visible = true;
            MessagesPanel.BringToFront();
            ViewMessagePanel.Visible = false;
            NewMessagePanel.Visible = false;
            foreach (DataGridViewRow r in MessageTable.Rows)
            {
                if ((Get.ReadCheck(Convert.ToInt32(r.Cells[4].Value)) == "1"))
                {
                    r.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                }
            }
            

        }

        private void MessagesSent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Messages Get = new Messages();
            MessageTable.DataSource = (Get.BuildSentTable(UsernameTB.Text));
            MessageLabel.Text = "הודעות שנשלחו";
            MessageTable.Sort(MessageTable.Columns["מס' קישור להודעה"], ListSortDirection.Descending);
            MessageTable.Visible = true;
            NewMessagePanel.Visible = false;
            ViewMessagePanel.Visible = false;
        }

        private void InBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Messages Get = new Messages();
            MessageTable.DataSource = (Get.BuildReadTable(UsernameTB.Text));
            MessageLabel.Text = "הודעות שהתקבלו";
            MessageTable.Sort(MessageTable.Columns["מס' קישור להודעה"], ListSortDirection.Descending);
            NewMessagePanel.Visible = false;
            ViewMessagePanel.Visible = false;
            MessageTable.Visible = true;
            foreach (DataGridViewRow r in MessageTable.Rows)
            {
                if ((Get.ReadCheck(Convert.ToInt32(r.Cells[4].Value)) == "1"))
                {
                    r.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                }
            }
        }

        private void ExitMessage_Click(object sender, EventArgs e)
        {
            MessagesPanel.Visible = false;
            MessageTable.DataSource = "";
            MessageLabel.Text = "הודעות שהתקבלו";
        }


        private void MessagesTrash_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Messages Get = new Messages();
            MessageTable.DataSource = (Get.BuildTrashTable(UsernameTB.Text));
            MessageLabel.Text = "הודעות שנמחקו";
            MessageTable.Sort(MessageTable.Columns["מס' קישור להודעה"], ListSortDirection.Descending);
            MessageTable.Visible = true;
            NewMessagePanel.Visible = false;
            ViewMessagePanel.Visible = false;
        }

        private void SendMail_Click(object sender, EventArgs e)
        {
            NewMessagePanel.Visible = true;
            MessageTable.Visible = false;
            TitleTB.Text = String.Empty;
            MessageTB.Text = String.Empty;
            SentToTB.Text = String.Empty;
        }

        private void CancelNewMessage_Click(object sender, EventArgs e)
        {
            Messages Get = new Messages();
            NewMessagePanel.Visible = false;
            MessageTable.Visible = true;
            foreach (DataGridViewRow r in MessageTable.Rows)
            {
                if ((Get.ReadCheck(Convert.ToInt32(r.Cells[4].Value)) == "1"))
                {
                    r.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                }
            }
        }

        private void SendMailButton_Click(object sender, EventArgs e)
        {
            Messages Get = new Messages();

            if (Get.CheckDestination(SentToTB.Text) == true)
            {
                if(Get.NewMessage(UsernameTB.Text, SentToTB.Text, TitleTB.Text, MessageTB.Text) == true)
            {
                    NewMessagePanel.Visible = false;
                    MessageTable.Visible = true;
                    MessageBox.Show("ההודעה נשלחה");
                }
            else
            {
                    MessageBox.Show("השליחה נכשלה");
                }
            }
            else
            {
                MessageBox.Show("שליחה נכשלה, שם משתמש לא קיים במערכת");
            }
            
            
        }

        private void MessageTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Result;
            if (e.ColumnIndex == 4)
            {
                Messages Show = new Messages();
                Result = Show.ViewMessage(Convert.ToInt32(MessageTable[e.ColumnIndex, e.RowIndex].Value));
                if (Result == 1)
                {
                    Message = Convert.ToInt32(MessageTable[e.ColumnIndex, e.RowIndex].Value);
                    if (Show.DeleteCheck(Message) == "1")
                        RestoreButton.Visible = true;
                    else
                        RestoreButton.Visible = false;
                    ViewMessageWeb.Navigate(Directory.GetCurrentDirectory() + @"\Message.html");
                    ViewMessagePanel.Visible = true;
                    MessageTable.Visible = false;
                    NewMessagePanel.Visible = false;
                    Show.SetUnRead(Message);
                }
                else
                {
                    MessageBox.Show("קובץ לא נמצא, אנא צור קשר עם התמיכה");
                }
            }
        }

        private void MoveButton_MouseDown(object sender, MouseEventArgs e)
        {
            WindowMove.Start();
            WindowMove.Interval = 10;
            WindowID = "Messages";
            MessagesPanel.BringToFront();
        }

        private void WindowMove_Tick(object sender, EventArgs e)
        {
            if(WindowID == "Messages")
            {
                MessagesPanel.Top = MousePosition.Y - 15;
                MessagesPanel.Left = MousePosition.X - 15;
            }
            if (WindowID == "Room980")
            {
                Room980.Top = MousePosition.Y - 25;
                Room980.Left = MousePosition.X - 75;
            }
            if (WindowID == "Hospitalization")
            {
                HospitalizationPanel.Top = MousePosition.Y - 25;
                HospitalizationPanel.Left = MousePosition.X - 75;
            }

            if (WindowID == "Slider")
            {
                DepSlidePanel.Left = -Slider.Left;
                if (MousePosition.X <= ScreenX - 200)
                    Slider.Left = MousePosition.X;
            }
           
        }

        private void MoveButton_MouseUp(object sender, MouseEventArgs e)
        {
            WindowMove.Stop();
        }

        private void CloseNote_Click(object sender, EventArgs e)
        {
            NotificationPanel.Visible = false;
        }

        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MapPanel.Visible = true;
            MapPanel.Dock = DockStyle.Fill;
            MapPanel.BringToFront();
        }

        private void BackFromMap_Click(object sender, EventArgs e)
        {
            MapPanel.Visible = false;
            MainMenuPanel.Visible = true;
            MapBackground.Visible = false;
        }

        private void Move980_MouseUp(object sender, MouseEventArgs e)
        {
            WindowMove.Stop();
        }

        private void Move980_MouseDown(object sender, MouseEventArgs e)
        {
            WindowMove.Start();
            WindowMove.Interval = 10;
            WindowID = "Room980";
            Room980.BringToFront();
        }

        private void Close980_Click(object sender, EventArgs e)
        {
            Room980.Visible = false;
        }

        private void MoveHospitalization_MouseDown(object sender, MouseEventArgs e)
        {
            WindowMove.Start();
            WindowMove.Interval = 10;
            WindowID = "Hospitalization";
            HospitalizationPanel.BringToFront();
        }

        private void MoveHospitalization_MouseUp(object sender, MouseEventArgs e)
        {
            WindowMove.Stop();
        }

        private void CloseHospitalization_Click(object sender, EventArgs e)
        {
            HospitalizationPanel.Visible = false;
            MapBackground.Visible = false;
        }

        private void Room980Button_Click(object sender, EventArgs e)
        {
            Map Check = new Map();
            int BedID = 1;
            RoomID = 980;
            TitleRoom.Text = Check.Room(RoomID);

            Room980.BringToFront();
            Image EmptyBed = Resources.EmptyBed;
            Image OccupiedBed = Resources.FullBed;
            if (Check.Bed(RoomID, BedID) == true) BedID1.BackgroundImage = OccupiedBed; else BedID1.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID2.BackgroundImage = OccupiedBed; else BedID2.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID3.BackgroundImage = OccupiedBed; else BedID3.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID4.BackgroundImage = OccupiedBed; else BedID4.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID5.BackgroundImage = OccupiedBed; else BedID5.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID6.BackgroundImage = OccupiedBed; else BedID6.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID7.BackgroundImage = OccupiedBed; else BedID7.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID8.BackgroundImage = OccupiedBed; else BedID8.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID9.BackgroundImage = OccupiedBed; else BedID9.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID10.BackgroundImage = OccupiedBed; else BedID10.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID11.BackgroundImage = OccupiedBed; else BedID11.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID12.BackgroundImage = OccupiedBed; else BedID12.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID13.BackgroundImage = OccupiedBed; else BedID13.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID14.BackgroundImage = OccupiedBed; else BedID14.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID15.BackgroundImage = OccupiedBed; else BedID15.BackgroundImage = EmptyBed; BedID++;
            Room980.Visible = true;
        }

        private void HospitalizationButton_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            HospitalizationPanel.Visible = true;
            MapBackground.Visible = true;
            HospitalizationPanel.BringToFront();
            OccupationTaken.Size = new Size(Get.Occupation("מחלקת?אשפוז") * 10, 55);
            //OccupationFree.Size = new Size(1000 - (Get.Occupation("מחלקת?אשפוז") * 10), 82);
            OccupationTaken.Text = Get.Occupation("מחלקת?אשפוז").ToString();
            OccupationFree.Text = ((15*5)-Get.Occupation("מחלקת?אשפוז")).ToString();
            TotalLabel.Text = (15 * 5).ToString();
            HospitalizationPanel.Top = (ScreenY - 50 - UserPanel.Size.Height - HospitalizationPanel.Size.Height);
            HospitalizationPanel.Left = (ScreenX - HospitalizationPanel.Size.Width) / 2;
        }

        private void TitleRoom980_Click(object sender, EventArgs e)
        {
            Room980.BringToFront();
        }

        private void TitleHospitalization_Click(object sender, EventArgs e)
        {
            HospitalizationPanel.BringToFront();
        }

        private void TitleHospitalization_MouseDown(object sender, MouseEventArgs e)
        {
            WindowMove.Start();
            WindowMove.Interval = 10;
            WindowID = "Hospitalization";
            HospitalizationPanel.BringToFront();
        }

        private void TitleHospitalization_MouseUp(object sender, MouseEventArgs e)
        {
            WindowMove.Stop();
        }

        private void TitleRoom980_MouseDown(object sender, MouseEventArgs e)
        {
            WindowMove.Start();
            WindowMove.Interval = 10;
            WindowID = "Room980";
            Room980.BringToFront();
        }

        private void TitleRoom980_MouseUp(object sender, MouseEventArgs e)
        {
            WindowMove.Stop();
        }

        private void BedID1_MouseHover(object sender, EventArgs e)
        {
        }

        private void BedID1_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 1;
            if (RoomToolTip.Visible == false) {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID2_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 2;
            if (RoomToolTip.Visible == false)
            {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID2_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID3_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 3;
            if (RoomToolTip.Visible == false)
            {
                
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID3_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID8_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 8;
            if (RoomToolTip.Visible == false)
            {
           
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID8_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID13_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 13;
            if (RoomToolTip.Visible == false)
            {
              
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID13_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID1_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 1;
            if(OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID8_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 8;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID13_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 13;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID2_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 2;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID3_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 3;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID4_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 4;
            if (RoomToolTip.Visible == false)
            {
    
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID4_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID5_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID6_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID7_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 7;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID9_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID10_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID11_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 11;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID12_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID14_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 14;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID14_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID15_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID4_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 4;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID5_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 5;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID6_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 6;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID15_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 15;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID12_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 12;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID11_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID10_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 10;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID9_Click(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 9;
            if (OccupiedLabel.Text == "מיטה בשימוש")
            {
                IDSearch.Text = Get.OnlyClientID(RoomID, BedID);
                Room980.Visible = false;
                HospitalizationPanel.Visible = false;
                MapPanel.Visible = false;
                MainMenuPanel.Visible = false;
                MainPanel.Visible = true;
                SearchAButton_Click(null, EventArgs.Empty);
                MapBackground.Visible = false;
            }
            else
            {
                MessageBox.Show("מיטה לא בשימוש");
            }
        }

        private void BedID7_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void BedID5_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 5;
            if (RoomToolTip.Visible == false)
            {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID6_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 6;
            if (RoomToolTip.Visible == false)
            {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID7_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 7;
            if (RoomToolTip.Visible == false)
            {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID9_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 9;
            if (RoomToolTip.Visible == false)
            {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID10_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 10;
            if (RoomToolTip.Visible == false)
            {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID11_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 11;
            if (RoomToolTip.Visible == false)
            {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID12_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 12;
            if (RoomToolTip.Visible == false)
            {
          
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID14_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 14;
            if (RoomToolTip.Visible == false)
            {
     
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void BedID15_MouseEnter(object sender, EventArgs e)
        {
            Map Get = new Map();
            int BedID = 15;
            if (RoomToolTip.Visible == false)
            {
                DepLabel.Text = Get.Department(RoomID, BedID);
                RoomIDLabel.Text = Get.Room(RoomID);
                BedIDLabel.Text = Get.BedNumber(BedID);
                OccupiedLabel.Text = Get.Occupied(RoomID, BedID);
                if (OccupiedLabel.Text == "מיטה בשימוש")
                {
                    ToolTipClientName.Text = Get.ClientFullName(RoomID, BedID);
                    ClientIDLabel.Text = Get.ClientID(RoomID, BedID);
                    ToolTipClientName.Visible = true;
                    ClientIDLabel.Visible = true;
                    DetailsLabel.Visible = true;
                }
                else
                {
                    ToolTipClientName.Visible = false;
                    ClientIDLabel.Visible = false;
                    DetailsLabel.Visible = false;
                }
                RoomToolTip.Visible = true;
                RoomToolTip.BringToFront();
            }
        }

        private void Room981Button_Click(object sender, EventArgs e)
        {
            Map Check = new Map();
            int BedID = 1;
            RoomID = 981;
            TitleRoom.Text = Check.Room(RoomID);
            Room980.BringToFront();
            Image EmptyBed = Resources.EmptyBed;
            Image OccupiedBed = Resources.FullBed;
            if (Check.Bed(RoomID, BedID) == true) BedID1.BackgroundImage = OccupiedBed; else BedID1.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID2.BackgroundImage = OccupiedBed; else BedID2.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID3.BackgroundImage = OccupiedBed; else BedID3.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID4.BackgroundImage = OccupiedBed; else BedID4.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID5.BackgroundImage = OccupiedBed; else BedID5.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID6.BackgroundImage = OccupiedBed; else BedID6.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID7.BackgroundImage = OccupiedBed; else BedID7.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID8.BackgroundImage = OccupiedBed; else BedID8.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID9.BackgroundImage = OccupiedBed; else BedID9.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID10.BackgroundImage = OccupiedBed; else BedID10.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID11.BackgroundImage = OccupiedBed; else BedID11.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID12.BackgroundImage = OccupiedBed; else BedID12.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID13.BackgroundImage = OccupiedBed; else BedID13.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID14.BackgroundImage = OccupiedBed; else BedID14.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID15.BackgroundImage = OccupiedBed; else BedID15.BackgroundImage = EmptyBed; BedID++;
            Room980.Visible = true;
        }

        private void Room982Button_Click(object sender, EventArgs e)
        {
            Map Check = new Map();
            int BedID = 1;
            RoomID = 982;
            TitleRoom.Text = Check.Room(RoomID);
            Room980.BringToFront();
            Image EmptyBed = Resources.EmptyBed;
            Image OccupiedBed = Resources.FullBed;
            if (Check.Bed(RoomID, BedID) == true) BedID1.BackgroundImage = OccupiedBed; else BedID1.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID2.BackgroundImage = OccupiedBed; else BedID2.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID3.BackgroundImage = OccupiedBed; else BedID3.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID4.BackgroundImage = OccupiedBed; else BedID4.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID5.BackgroundImage = OccupiedBed; else BedID5.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID6.BackgroundImage = OccupiedBed; else BedID6.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID7.BackgroundImage = OccupiedBed; else BedID7.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID8.BackgroundImage = OccupiedBed; else BedID8.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID9.BackgroundImage = OccupiedBed; else BedID9.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID10.BackgroundImage = OccupiedBed; else BedID10.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID11.BackgroundImage = OccupiedBed; else BedID11.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID12.BackgroundImage = OccupiedBed; else BedID12.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID13.BackgroundImage = OccupiedBed; else BedID13.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID14.BackgroundImage = OccupiedBed; else BedID14.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID15.BackgroundImage = OccupiedBed; else BedID15.BackgroundImage = EmptyBed; BedID++;
            Room980.Visible = true;
        }

        private void Room983Button_Click(object sender, EventArgs e)
        {
            Map Check = new Map();
            int BedID = 1;
            RoomID = 983;
            TitleRoom.Text = Check.Room(RoomID);
            Room980.BringToFront();
            Image EmptyBed = Resources.EmptyBed;
            Image OccupiedBed = Resources.FullBed;
            if (Check.Bed(RoomID, BedID) == true) BedID1.BackgroundImage = OccupiedBed; else BedID1.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID2.BackgroundImage = OccupiedBed; else BedID2.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID3.BackgroundImage = OccupiedBed; else BedID3.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID4.BackgroundImage = OccupiedBed; else BedID4.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID5.BackgroundImage = OccupiedBed; else BedID5.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID6.BackgroundImage = OccupiedBed; else BedID6.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID7.BackgroundImage = OccupiedBed; else BedID7.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID8.BackgroundImage = OccupiedBed; else BedID8.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID9.BackgroundImage = OccupiedBed; else BedID9.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID10.BackgroundImage = OccupiedBed; else BedID10.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID11.BackgroundImage = OccupiedBed; else BedID11.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID12.BackgroundImage = OccupiedBed; else BedID12.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID13.BackgroundImage = OccupiedBed; else BedID13.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID14.BackgroundImage = OccupiedBed; else BedID14.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID15.BackgroundImage = OccupiedBed; else BedID15.BackgroundImage = EmptyBed; BedID++;
            Room980.Visible = true;
        }

        private void Room984Button_Click(object sender, EventArgs e)
        {
            Map Check = new Map();
            int BedID = 1;
            RoomID = 984;
            TitleRoom.Text = Check.Room(RoomID);
            Room980.BringToFront();
            Image EmptyBed = Resources.EmptyBed;
            Image OccupiedBed = Resources.FullBed;
            if (Check.Bed(RoomID, BedID) == true) BedID1.BackgroundImage = OccupiedBed; else BedID1.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID2.BackgroundImage = OccupiedBed; else BedID2.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID3.BackgroundImage = OccupiedBed; else BedID3.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID4.BackgroundImage = OccupiedBed; else BedID4.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID5.BackgroundImage = OccupiedBed; else BedID5.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID6.BackgroundImage = OccupiedBed; else BedID6.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate180FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID7.BackgroundImage = OccupiedBed; else BedID7.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID8.BackgroundImage = OccupiedBed; else BedID8.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID9.BackgroundImage = OccupiedBed; else BedID9.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID10.BackgroundImage = OccupiedBed; else BedID10.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID11.BackgroundImage = OccupiedBed; else BedID11.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID12.BackgroundImage = OccupiedBed; else BedID12.BackgroundImage = EmptyBed; BedID++;
            OccupiedBed = Resources.FullBed; OccupiedBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            EmptyBed = Resources.EmptyBed; EmptyBed.RotateFlip((RotateFlipType.Rotate90FlipX));
            if (Check.Bed(RoomID, BedID) == true) BedID13.BackgroundImage = OccupiedBed; else BedID13.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID14.BackgroundImage = OccupiedBed; else BedID14.BackgroundImage = EmptyBed; BedID++;
            if (Check.Bed(RoomID, BedID) == true) BedID15.BackgroundImage = OccupiedBed; else BedID15.BackgroundImage = EmptyBed; BedID++;
            Room980.Visible = true;
        }

        private void OccupationFree_Click(object sender, EventArgs e)
        {

        }

        private void Slider_MouseDown(object sender, MouseEventArgs e)
        {
            WindowMove.Start();
            WindowMove.Interval = 10;
            WindowID = "Slider";
        }

        private void SurgeryDepButton_MouseEnter(object sender, EventArgs e)
        {
            Surgery_Button_Back.BackColor = Color.White;
        }

        private void SurgeryDepButton_MouseLeave(object sender, EventArgs e)
        {
            Surgery_Button_Back.BackColor = Color.Black;
        }

        private void HospitalizationButton_MouseEnter(object sender, EventArgs e)
        {
            Hospitalization_Button_Back.BackColor = Color.White;
        }

        private void HospitalizationButton_MouseLeave(object sender, EventArgs e)
        {
            Hospitalization_Button_Back.BackColor = Color.Black;
        }

        private void WomanDepButton_MouseEnter(object sender, EventArgs e)
        {
            Woman_Dep_Button_Back.BackColor = Color.White;
        }

        private void WomanDepButton_MouseLeave(object sender, EventArgs e)
        {
            Woman_Dep_Button_Back.BackColor = Color.Black;
        }

        private void ChildClinicButton_MouseEnter(object sender, EventArgs e)
        {
            Kids_Button_Back.BackColor = Color.White;
        }

        private void ChildClinicButton_MouseLeave(object sender, EventArgs e)
        {
            Kids_Button_Back.BackColor = Color.Black;
        }

        private void OtolaryngologyDepButton_MouseEnter(object sender, EventArgs e)
        {
            Otolaryngology_Back.BackColor = Color.White;
        }

        private void OtolaryngologyDepButton_MouseLeave(object sender, EventArgs e)
        {
            Otolaryngology_Back.BackColor = Color.Black;
        }

        private void MaleCB_CheckedChanged(object sender, EventArgs e)
        {
            if(MaleCB.Checked == true)
            {
                FemaleCB.Checked = false;
                Gender = "זכר";
            }
            else
            {
                FemaleCB.Checked = true;
                Gender = "נקבה";
            }
        }

        private void FemaleCB_CheckedChanged(object sender, EventArgs e)
        {
            if (FemaleCB.Checked == true)
            {
                MaleCB.Checked = false;
                Gender = "נקבה";
            }
            else
            {
                MaleCB.Checked = true;
                Gender = "זכר";
            }
        }

        private void BirthDateAdd_TextChanged(object sender, EventArgs e)
        {
            MultiChecks Get = new MultiChecks();
            AgeAdd.Text = Get.Age(BirthDateAdd.Text);
        }

        private void CancelAddClient_Click(object sender, EventArgs e)
        {
            Panel_AddClient.Visible = false;
            var AllTextBoxs = Panel_AddClient.Controls.OfType<TextBox>();
            foreach (TextBox Text in AllTextBoxs)
            {
                Text.Text = String.Empty;
            }
            var AllBoxes = Panel_AddClient.Controls.OfType<CheckBox>();
            foreach (CheckBox Box in AllBoxes)
            {
                Box.Checked = false;
            }
            InsuranceTB.Visible = false;
            InsuranceNameLabel.Visible = false;
            BloodTypeAdd.SelectedIndex = 0;
            BloodTypeAdd.Text = "";
        }

        private void OfficeButton_Click(object sender, EventArgs e)
        {
            MainMenuPanel.Visible = false;
            OfficePanel.Visible = true;
            OfficePanel.BringToFront();
            BackFromOffice.Visible = true;

        }

        private void BackFromOffice_Click(object sender, EventArgs e)
        {
            BackFromOffice.Visible = false;
            MainMenuPanel.Visible = true;
            OfficePanel.Visible = false;
        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void ScheduleTable_MouseClick(object sender, MouseEventArgs e)
        {
            string Col = "", Row = "";

            label97.Text = Col;
            label96.Text = Row;

            label68.Text = "" + MousePosition.X.ToString() + " " + MousePosition.Y.ToString() + "";
            if (ClientFoundLabel.Text != "")
            {
                
            }
        }

        private void SetDoctorAppointment_Click(object sender, EventArgs e)
        {
            Schedule Get = new Schedule();
            MultiChecks Add = new MultiChecks();
            Panel_Schedule.Visible = true;
            Panel_Schedule.BringToFront();
            DateTime localDate = DateTime.Now;
            label68.Text = localDate.ToString("dddd");
            label97.Text = localDate.ToString();
            int day = 0;

            switch (localDate.ToString("dddd"))
            {
                case "Sunday": day = 1; break;
                case "Monday":   day = 2; break;
                case "Tuesday": day = 3; break;
                case "Wendsday": day = 4; break;
                case "Thursday": day = 5; break;
                case "Friday":  day = 6; break;
                case "Saterday":   day = 7; break;
            }

            label97.Text = day.ToString();
            int Temp = day - 1, i=day-1;



            Day1.Text = "יום א " + Add.ValidDate(localDate.AddDays(-4).ToString()) + ""; i++;
            Day2.Text = "יום ב " + Add.ValidDate(localDate.AddDays(-3).ToString()) + ""; i++;
            Day3.Text = "יום ג " + Add.ValidDate(localDate.AddDays(-2).ToString()) + ""; i++;
            Day4.Text = "יום ד " + Add.ValidDate(localDate.AddDays(-1).ToString()) + ""; i++;
            Day5.Text = "יום ה " + Add.ValidDate(localDate.AddDays(0).ToString()) + ""; i++;
            Day6.Text = "יום ו " + Add.ValidDate(localDate.AddDays(1).ToString()) + ""; i++;
            Day7.Text = "יום ש " + Add.ValidDate(localDate.AddDays(2).ToString()) + "";



        }

        private void BackFromSchedule_Click(object sender, EventArgs e)
        {
            Panel_Schedule.Visible = false;
            OfficePanel.BringToFront();
        }

        private void ScheduleTable_Paint(object sender, PaintEventArgs e)
        {
            ScheduleTable.BackColor =Color.Gray;
        }

        private void label101_Click(object sender, EventArgs e)
        {
 
            
        }


        public string SetRow(int Row)
        {
            string row = "";
            switch (Row)
            {
                case 1: row=Day1.Text; break;
                case 2: row = Day2.Text; break;
                case 3: row = Day3.Text; break;
                case 4: row = Day4.Text; break;
                case 5: row = Day5.Text; break;
                case 6: row = Day6.Text; break;
                case 7: row = Day7.Text; break;
            }
            return row;
        }

        private void R101_Click(object sender, EventArgs e)
        {
            Schedule Get = new Schedule();
            string Text = "";
            string Row ="", Col = "";
            Text = this.Text;
            Row = SetRow(int.Parse(Text) / 100);
            Row = Get.OnlyDate(Row);
            Col = Get.Col(int.Parse(Text) % 100);
            
            
        }

        private void HealthInsuranceYes_CheckedChanged(object sender, EventArgs e)
        {
            if (HealthInsuranceYes.Checked == true)
            {
                HealthInsurance = "Yes";
                HealthInsuranceNo.Checked = false;
                InsuranceNameLabel.Visible = true;
                InsuranceTB.Visible = true;
            }
            else
            {
                InsuranceNameLabel.Visible = false;
                InsuranceTB.Visible = false;
            }
        }

        private void HealthInsuranceNo_CheckedChanged(object sender, EventArgs e)
        {
            if (HealthInsuranceNo.Checked == true)
            {
                HealthInsurance = "None";
                HealthInsuranceYes.Checked = false;
                InsuranceNameLabel.Visible = false;
                InsuranceTB.Visible = false;
            }
        }

        private void Slider_MouseUp(object sender, MouseEventArgs e)
        {
            if (Slider.Left == 870)
                WindowMove.Stop();
            else
                WindowID = "ReturnSlider";
        }

        private void BedID1_MouseLeave(object sender, EventArgs e)
        {
            if (RoomToolTip.Visible == true)
            {
                RoomToolTip.Visible = false;
                DepLabel.Text = "";
                RoomIDLabel.Text = "";
                BedIDLabel.Text = "";
                OccupiedLabel.Text = "";
                ToolTipClientName.Text = "";
                ClientIDLabel.Text = "";
            }
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            Messages Set = new Messages();
            Set.UnDelete(Message);
            RestoreButton.Visible = false;
        }

        private void CloseView_Click(object sender, EventArgs e)
        {
            ViewMessagePanel.Visible = false;
            MessageTable.Visible = true;
            MessageButton_Click(null, EventArgs.Empty);
        }

        private void RespondButton_Click(object sender, EventArgs e)
        {
            Messages Get = new Messages();
            ViewMessagePanel.Visible = false;
            NewMessagePanel.Visible = true;
            SentToTB.Text = Get.Respond(Message);
            TitleTB.Focus();
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            if (Timer % 5 == 0)
            {
                Count = UnreadMessages;
            }
            if (Max >= 0)
            {
                Max--;
            }
            if(Max == -1)
            {
                NotificationPanel.Visible = false;
            }
            Login Access = new Login();
            User Get = new User();
            Messages Check = new Messages();
            
            if (Access.Authorization(UsernameTB.Text, PassTB.Text) == true && UsernameTB.Visible==false)
            {
                UnreadMessages = Check.UnreadMessages(UsernameTB.Text);
                if (UnreadMessages >= 0)
                {
                    MessageButton.BackgroundImage = Resources.MessagesNote;
                    UnreadCounterLabel.Visible = true;
                    UnreadCounterLabel.Text = UnreadMessages.ToString();
                    UnreadCounterLabel.BringToFront();
                    if (UnreadMessages > Count)
                    {
                        NotificationPanel.Visible = true;
                        Max = 35;
                    }

                }
                if (UnreadMessages==0)
                {
                    UnreadCounterLabel.Visible = false;
                    MessageButton.BackgroundImage = Resources.Messages;
                }
            }

            Timer++;
            if (Timer > 10)
            {
                Timer = 1;
            }
        }

        private void DeleteMessage_Click(object sender, EventArgs e)
        {
            Messages Get = new Messages();
            int Delete = int.Parse(Get.DeleteCheck(Message));
            if(Delete == 0)
            {
                Get.SetDelete(Message);
                ViewMessagePanel.Visible = false;
                NewMessagePanel.Visible = false;
                MessageTable.Visible = true;
                MessageBox.Show("ההודעה הועבר לסל האשפה");
                MessageTable.DataSource = (Get.BuildReadTable(UsernameTB.Text));
                MessageTable.Sort(MessageTable.Columns["מס' קישור להודעה"], ListSortDirection.Descending);
                foreach (DataGridViewRow r in MessageTable.Rows)
                {
                    if ((Get.ReadCheck(Convert.ToInt32(r.Cells[4].Value)) == "1"))
                    {
                        r.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                    }
                }

            }
            else
            {
                Get.Remove(Message);
                ViewMessagePanel.Visible = false;
                NewMessagePanel.Visible = false;
                MessageTable.Visible = true;
                MessageBox.Show("ההודעה נמחקה לצמיתות");
                MessageTable.DataSource = (Get.BuildTrashTable(UsernameTB.Text));
            }
        }
    }
}
