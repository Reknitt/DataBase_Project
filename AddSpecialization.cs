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
    public partial class AddSpecialization : Form
    {
        SqlConnection sqlConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);
        public AddSpecialization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = NameBox.Text;
            string Uid = UidBox.Text;
            string TypeOfStudy = TypeBox.Text;
            string DepartmentName = DepartmentBox.Text;
            int TotalYearsStudy = 5;

            if (TypeOfStudy == "Специалитет")
                TotalYearsStudy = 5;
            if (TypeOfStudy == "Бакалавриат")
                TotalYearsStudy = 4;
            if (TypeOfStudy == "Магистратура")
                TotalYearsStudy = 2;

            foreach (var chr in CostBox.Text) 
            {
                if (!char.IsDigit(chr))
                    MessageBox.Show("В поле 'Цена обучения' присутствуют запрещенные символы.");
            }
            int StudyCost = Convert.ToInt32(CostBox.Text);

            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Department WHERE Name='{DepartmentName}'", sqlConnection);
            sqlConnection.Open();

            int UidDepartment = Convert.ToInt32(sqlCommand.ExecuteScalar());

            string sql = string.Format("INSERT INTO Specialization (Uid, Name, UidDepartment, StudyCost, TotalYearsStudy, TypeOfStudy)" +
                    "VALUES (@Uid, @Name, @UidDepartment, @StudyCost, @TotalYearsStudy, @TypeOfStudy)");
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@Uid", Uid);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@UidDepartment", UidDepartment);
                cmd.Parameters.AddWithValue("@StudyCost", StudyCost);
                cmd.Parameters.AddWithValue("@TotalYearsStudy", TotalYearsStudy);
                cmd.Parameters.AddWithValue("@TypeOfStudy", TypeOfStudy);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Запись успешно добавлена");
            this.Close();
        }

        private void AddSpecialization_Load(object sender, EventArgs e)
        {
            DepartmentBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TypeBox.Items.AddRange(new string[] { "Специалитет", "Бакалавриат", "Магистратура" });

            sqlConnection.Open();
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Connection is not open");
                return;
            }

            List<string> DepartmentNames = new List<string>();
            SqlCommand sqlCommand = new SqlCommand("SELECT Name FROM Department", sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                DepartmentNames.Add(reader.GetValue(0).ToString());
                DepartmentBox.Items.Add(DepartmentNames[i]);
                i++;
            }
            reader.Close();
            sqlConnection.Close();
        }
    }
}
