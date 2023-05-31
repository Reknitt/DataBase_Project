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
    public partial class AddUniversity : Form
    {
        SqlConnection sqlConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);
        public AddUniversity()
        {
            InitializeComponent();
        }

        private void AddUniversity_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Connection is not open");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Name = textBox1.Text;
                string Rector = textBox2.Text;
                string NumberOfPhone = textBox3.Text;
                string InternetSite = textBox4.Text;

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

                foreach (var chr in Rector)
                {
                    if (!char.IsLetter(chr))
                    {
                        if (chr != ' ')
                        {
                            MessageBox.Show("Поле 'Ректор' содержит цифры или иные запрещенные символы");
                            return;
                        }
                    }
                }

                //foreach (var chr in NumberOfPhone)
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

                if (Name.Length == 0 && Rector.Length == 0 && NumberOfPhone.Length == 0 && InternetSite.Length == 0)
                {
                    MessageBox.Show("Присутствуют пустые поля.");
                    return;
                }

                string sql = string.Format("INSERT INTO University (Name, Rector, NumberOfPhone, InternetSite)" +
                    "VALUES (@Name', @Rector', @NumberOfPhone', @InternetSite')");
                using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Rector", Rector);
                    cmd.Parameters.AddWithValue("@NumberOfPhone", NumberOfPhone);
                    cmd.Parameters.AddWithValue("@InternetSite", InternetSite);
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
                sqlConnection.Dispose();
            }
        }
    }
}
