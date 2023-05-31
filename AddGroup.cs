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

// TODO: высчитывать атрибут course через год поступления (текущий год будет 2022)


namespace DataBase_Project
{
    public partial class AddGroup : Form
    {
        SqlConnection sqlConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);
        public AddGroup()
        {
            InitializeComponent();
        }

        string TypeOfStudy = "Не выбрано";
        private void AddGroup_Load(object sender, EventArgs e)
        {
            label13.Text = TypeOfStudy;
            SpecNames.DropDownStyle = ComboBoxStyle.DropDownList;
            YearAccepted.DropDownStyle = ComboBoxStyle.DropDownList;
            StudyTime.DropDownStyle = ComboBoxStyle.DropDownList;
            StudyTime.Items.AddRange(new string[] {"Очное", "Заочное"});

            int year = 2022;
            for (int j = 0; j < 20; j++)
            {
                YearAccepted.Items.Add(year);
                year--;
            }
            
            sqlConnection.Open();
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
            sqlConnection.Close();
        }

        private void AddRecord_Click(object sender, EventArgs e)
        {
            int accepted;
            int firstYear;
            int secondYear;
            int thirdYear;
            int fourthYear;
            int fifthYear;
            int ended;

            accepted = Convert.ToInt32(Accepted.Text);
            firstYear = Convert.ToInt32(FirstYear.Text);

            if (!SecondYear.Enabled)
                secondYear = 0;
            else secondYear = Convert.ToInt32(SecondYear.Text);

            if (!ThirdYear.Enabled)
                thirdYear = 0;
            else thirdYear = Convert.ToInt32(ThirdYear.Text);

            if (!FourthYear.Enabled)
                fourthYear = 0;
            else fourthYear = Convert.ToInt32(FourthYear.Text);

            if (!FifthYear.Enabled)
                fifthYear = 0;
            else fifthYear = Convert.ToInt32(FifthYear.Text);

            if (!Ended.Enabled)
                ended = 0;
            else ended = Convert.ToInt32(Ended.Text);

            string name = CodeBox.Text;
            string specName = SpecNames.Text;
            string studyTime = StudyTime.Text;

            int year = Convert.ToInt32(YearAccepted.Text);
            int course = 0;
            if (TypeOfStudy == "Бакалавриат")
                course = (2022 - year) % 4 + 1;
            if (TypeOfStudy == "Магистратура")
                course = (2022 - year) % 2 + 1;
            if (TypeOfStudy == "Специалитет")
                course = (2022 - year) % 5 + 1;

            if (year <= 2018)
            {
                if (TypeOfStudy == "Бакалавриат")
                    course = 4;
                if (TypeOfStudy == "Магистратура")
                    course = 2;
                if (TypeOfStudy == "Специалитет")
                    course = 5;
            }
            bool endStudy = false;

            if (year < 2018)
                endStudy = true;
            label3.Text = course.ToString();

            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Specialization WHERE Name='{specName}'", sqlConnection);
            sqlConnection.Open();

            string UidSpecialization = sqlCommand.ExecuteScalar().ToString();

            string sql = string.Format("INSERT INTO Grouping (Name, Year, StudyTime, Course, Uid, Accepted, firstYear, secondYear, thirdYear, FourthYear, FifthYear, Ended, EndStudy)" +
                    "VALUES (@Name, @Year, @StudyTime, @Course, @Uid, @Accepted, @firstYear, @secondYear, @thirdYear, @FourthYear, @FifthYear, @Ended, @EndStudy)");
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@StudyTime", studyTime);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@Uid", UidSpecialization);
                cmd.Parameters.AddWithValue("@Accepted", accepted);
                cmd.Parameters.AddWithValue("@firstYear", firstYear);
                cmd.Parameters.AddWithValue("@secondYear", secondYear);
                cmd.Parameters.AddWithValue("@thirdYear", thirdYear);
                cmd.Parameters.AddWithValue("@FourthYear", fourthYear);
                cmd.Parameters.AddWithValue("@FifthYear", fifthYear);
                cmd.Parameters.AddWithValue("@Ended", ended);
                cmd.Parameters.AddWithValue("@EndStudy", endStudy);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Запись успешно добавлена");
            this.Close();
        }

        private void SpecNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string specName = SpecNames.Text;
            SqlCommand sqlCommand = new SqlCommand($"SELECT TypeOfStudy FROM Specialization WHERE Name='{specName}'", sqlConnection);
            sqlConnection.Open();

            TypeOfStudy = sqlCommand.ExecuteScalar().ToString();
            sqlConnection.Close();
            label13.Text = TypeOfStudy;


            if (TypeOfStudy == "Специалитет")
            {

            }
            if (TypeOfStudy == "Бакалавриат")
            {
                FifthYear.Enabled = false;
            }
            if (TypeOfStudy == "Магистратура")
            {
                ThirdYear.Enabled = false;
                FourthYear.Enabled = false;
                FifthYear.Enabled = false;
            }

        }

        private void YearAccepted_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = 2022 - Convert.ToInt32(YearAccepted.Text);

            SecondYear.Enabled = true;
            ThirdYear.Enabled = true;
            FourthYear.Enabled = true;
            FifthYear.Enabled = true;
            Ended.Enabled = true;

            switch (value)
            {
                case 0:
                    SecondYear.Enabled = false;
                    ThirdYear.Enabled = false;
                    FourthYear.Enabled = false;
                    FifthYear.Enabled = false;
                    Ended.Enabled = false;
                    break;
                case 1:
                    SecondYear.Enabled = true;
                    ThirdYear.Enabled = false;
                    FourthYear.Enabled = false;
                    FifthYear.Enabled = false;
                    Ended.Enabled = false;
                    break;
                case 2:
                    SecondYear.Enabled = true;
                    if (TypeOfStudy == "Магистратура")
                    {
                        Ended.Enabled = true;
                    }
                    else
                    {
                        ThirdYear.Enabled = true;
                        FourthYear.Enabled = false;
                        FifthYear.Enabled = false;
                        Ended.Enabled = false;
                    }
                    break;
                case 3:
                    SecondYear.Enabled = true;
                    ThirdYear.Enabled = true;
                    FourthYear.Enabled = true;
                    FifthYear.Enabled = false;
                    if (TypeOfStudy == "Магистратура")
                    {
                        Ended.Enabled = true;
                    }
                    else Ended.Enabled = false;
                    break;
                case 4:
                    SecondYear.Enabled = true;
                    ThirdYear.Enabled = true;
                    FourthYear.Enabled = true;
                    if (TypeOfStudy == "Бакалавриат" || TypeOfStudy == "Магистратура")
                    {
                        Ended.Enabled = true;
                    }
                    else
                    {
                        FifthYear.Enabled = true;
                        Ended.Enabled = false;
                    }
                    break;
                
            }

            if (TypeOfStudy == "Специалитет")
            {

            }
            if (TypeOfStudy == "Бакалавриат")
            {
                FifthYear.Enabled = false;
            }
            if (TypeOfStudy == "Магистратура")
            {
                ThirdYear.Enabled = false;
                FourthYear.Enabled = false;
                FifthYear.Enabled = false;
            }

            

        }
    }
}
