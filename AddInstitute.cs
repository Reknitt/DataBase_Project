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
    public partial class AddInstitute : Form
    {
        SqlConnection sqlConnection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);
        public AddInstitute()
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
                string NumberOfPhone = textBox4.Text;
                string NameUniversity = comboBox6.Text.Trim(' ');
                int UidUniversity;

                foreach (var chr in Name)
                {
                    if (!char.IsLetter(chr))
                    {
                        if (!char.IsPunctuation(chr))
                        {
                            if (chr != ' ')
                            {
                                MessageBox.Show("Поле 'Название' содержит цифры или иные запрещенные символы");
                                return;
                            }
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

                if (Name.Length == 0 && Director.Length == 0 && NumberOfPhone.Length == 0 && Address.Length == 0)
                {
                    MessageBox.Show("Присутствуют пустые поля.");
                    return;
                }

                SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM University WHERE Name='{NameUniversity}'", sqlConnection);
                sqlConnection.Open();

                UidUniversity = Convert.ToInt32(sqlCommand.ExecuteScalar());

                string sql = string.Format("INSERT INTO Institute (Name, Director, UidUniversity, Address, NumberOfPhone)" +
                    "VALUES (@Name, @Director, @UidUniversity, @Address, @NumberOfPhone)");
                using(SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Director", Director);
                    cmd.Parameters.AddWithValue("@UidUniversity", UidUniversity);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@NumberOfPhone", NumberOfPhone);
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

        private void AddInstitute_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Connection is not open");
                return;
            }

            List<string> UniversityNames = new List<string>();
            SqlCommand sqlCommand = new SqlCommand("SELECT Name FROM University", sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                UniversityNames.Add(reader.GetValue(0).ToString());
                comboBox6.Items.Add(UniversityNames[i]);
                i++;
            }
            reader.Close();
            sqlConnection.Close();
        }
    }
}
