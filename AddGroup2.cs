using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase_Project
{
    public partial class AddGroup2 : Form
    {
        string TypeOfStudy = "Не выбрано";
        bool isEnded = false;
        SqlConnection sqlConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);
        public AddGroup2()
        {
            InitializeComponent();
            sqlConnection.Open();

        }

        private void AddGroup2_Load(object sender, EventArgs e)
        {
            SpecNames.DropDownStyle = ComboBoxStyle.DropDownList;
            YearAccepted.DropDownStyle = ComboBoxStyle.DropDownList;
            StudyTime.DropDownStyle = ComboBoxStyle.DropDownList;
            YearReport.DropDownStyle = ComboBoxStyle.DropDownList;
            StudyTime.Items.AddRange(new string[] { "Очное", "Заочное" });
            Ended.Enabled = false;

            int year = 2022;
            for (int j = 0; j < 20; j++)
            {
                YearReport.Items.Add(year);
                year--;
            }
            YearReport.SelectedIndex = 0;

            
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Connection is not open");
                return;
            }

            List<string> SpecializationNames = new List<string>();
            SqlCommand sqlCommand = new SqlCommand("SELECT Name FROM Specialization", sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                SpecializationNames.Add(reader.GetValue(0).ToString());
                SpecNames.Items.Add(SpecializationNames[i]);
                i++;
            }
            reader.Close();
        }

        int Course = 0;
        private void AddRecord_Click(object sender, EventArgs e)
        {
            string name = CodeBox.Text;
            string specName = SpecNames.Text;
            string studyTime = StudyTime.Text;
            int yearAccepted = Convert.ToInt32(YearAccepted.SelectedItem);
            int yearReport = Convert.ToInt32(YearReport.SelectedItem);
            

            

            SqlCommand sqlCommand;
            int peopleAccepted = 0;

            if (!Accepted.Enabled)
            {
                sqlCommand = new SqlCommand($"SELECT PeopleAccepted FROM Group2 WHERE Name='{name}' AND YearReport ='{yearAccepted}'", sqlConnection);
                
                peopleAccepted = Convert.ToInt32(sqlCommand.ExecuteScalar());
                
            }
            else peopleAccepted = Convert.ToInt32(Accepted.Text);

            int numberOfPeopleNow = 0;
            numberOfPeopleNow = Convert.ToInt32(Now.Text);

            

            sqlCommand = new SqlCommand($"SELECT * FROM Specialization WHERE Name='{specName}'", sqlConnection);

            
            string UidSpecialization = sqlCommand.ExecuteScalar().ToString();
            

            
            string sql = string.Format("INSERT INTO Group2 (Name, YearAccepted, StudyTime, Course, Uid, PeopleAccepted, NumberOfPeopleNow, YearReport, isEnded)" +
                    "VALUES (@Name, @YearAccepted, @StudyTime, @Course, @Uid, @PeopleAccepted, @NumberOfPeopleNow, @YearReport, @isEnded)");
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@YearAccepted", yearAccepted);
                cmd.Parameters.AddWithValue("@StudyTime", studyTime);
                cmd.Parameters.AddWithValue("@Course", Course);
                cmd.Parameters.AddWithValue("@Uid", UidSpecialization);
                cmd.Parameters.AddWithValue("@PeopleAccepted", peopleAccepted);
                cmd.Parameters.AddWithValue("@NumberOfPeopleNow", numberOfPeopleNow);
                cmd.Parameters.AddWithValue("@YearReport", yearReport);
                cmd.Parameters.AddWithValue("@isEnded", isEnded);
                cmd.ExecuteNonQuery();
            }
            
            MessageBox.Show("Запись успешно добавлена");
            sqlConnection.Close();
            this.Close();
        }

        private void YearReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(YearReport.SelectedItem);
            YearAccepted.Items.Clear();
            for (int j = 0; j < 20; j++)
            {
                YearAccepted.Items.Add(year);
                year--;
            }
        }

        private void YearAccepted_SelectedIndexChanged(object sender, EventArgs e)
        {
            Accepted.Enabled = true;
            Ended.Enabled = false;
            int yearReport = Convert.ToInt32(YearReport.SelectedItem);
            int yearAccepted = Convert.ToInt32(YearAccepted.SelectedItem);

            if (TypeOfStudy == "Бакалавриат")
                Course = (yearReport - yearAccepted) % 4 + 1;
            if (TypeOfStudy == "Магистратура")
                Course = (yearReport - yearAccepted) % 2 + 1;
            if (TypeOfStudy == "Специалитет")
                Course = (yearReport - yearAccepted) % 5 + 1;


            if (yearReport - yearAccepted >= 1)
            {
                Accepted.Enabled = false;
            }

            if (TypeOfStudy == "Магистратура" && (yearReport - yearAccepted >= 2))
            {
                Course = 2;
                isEnded = true;
                Ended.Enabled = isEnded;
            }

            if (TypeOfStudy == "Бакалавриат" && (yearReport - yearAccepted >= 4))
            {
                Course = 4;
                isEnded = true;
                Ended.Enabled = isEnded;
            }

            if (TypeOfStudy == "Специалитет" && (yearReport - yearAccepted >= 5))
            {
                Course = 5;
                isEnded = true;
                Ended.Enabled = isEnded;
            }


        }


        private void SpecNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string specName = SpecNames.Text;
            SqlCommand sqlCommand = new SqlCommand($"SELECT TypeOfStudy FROM Specialization WHERE Name='{specName}'", sqlConnection);
            
            TypeOfStudy = sqlCommand.ExecuteScalar().ToString();
            
            label13.Text = TypeOfStudy;
        }
    }
}
