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
    public partial class AddDepartment : Form
    {
        SqlConnection sqlConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);
        public AddDepartment()
        {
            InitializeComponent();
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void AddRecord_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = textBox1.Text;
                string Director = textBox2.Text;
                string Address = textBox3.Text;
                string Number = textBox4.Text;
                string NameInstitute = comboBox6.Text;
                int UidInstitute;

                foreach (var chr in Name)
                {
                    if (!char.IsLetter(chr))
                    {
                        if (chr != ' ')
                        {
                            MessageBox.Show("Поле 'Название' содержит цифры или иные запрещенные символы");
                            return;
                        }
                    }
                }

                foreach (var chr in Director)
                {
                    if (!char.IsLetter(chr))
                    {
                        if (chr != ' ')
                        {
                            MessageBox.Show("Поле 'Директор' содержит цифры или иные запрещенные символы");
                            return;
                        }
                    }
                }

                //foreach (var chr in Number)
                //{
                //    if (!char.IsDigit(chr))
                //    {
                //        if (!(chr == '(' || chr == ')' || chr == '-' || chr == ' '))
                //        {
                //            MessageBox.Show("Поле 'Номер телефона' содержит буквы или иные запрещенные символы");
                //            return;
                //        }
                //    }
                //}

                if (Name.Length == 0 && Director.Length == 0 && Number.Length == 0 && Address.Length == 0)
                {
                    MessageBox.Show("Присутствуют пустые поля.");
                    return;
                }

                SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Institute WHERE Name='{NameInstitute}'", sqlConnection);
                sqlConnection.Open();

                UidInstitute = Convert.ToInt32(sqlCommand.ExecuteScalar());

                string sql = string.Format("INSERT INTO Department (Name, Director, Number, Address, UidInstitute)" +
                    "VALUES (@Name, @Director, @Number, @Address, @UidInstitute)");
                using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Director", Director);
                    cmd.Parameters.AddWithValue("@Number", Number);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@UidInstitute", UidInstitute);
                    cmd.ExecuteNonQuery();
                }


                MessageBox.Show("Запись успешно добавлена");
                this.Close();
            }
            catch
            {
                throw (new Exception("Что-то пошло не так :("));
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void AddDepartment_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Connection is not open");
                return;
            }

            List<string> InstituteNames = new List<string>();
            SqlCommand sqlCommand = new SqlCommand("SELECT Name FROM Institute", sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                InstituteNames.Add(reader.GetValue(0).ToString());
                comboBox6.Items.Add(InstituteNames[i]);
                i++;
            }
            reader.Close();
            sqlConnection.Close();
        }
    }
}
