using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DataBase_Project
{
  
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = 
            new SqlConnection(ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);
        public Form1()
        {
            InitializeComponent();
            sqlConnection.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.AddRange(new string[] { "Очное", "Заочное" });
            

            //dataGridView2.ReadOnly = true;
            int year = 2022;
            for (int j = 0; j < 20; j++)
            {
                comboBox4.Items.Add(year);
                YearReport.Items.Add(year);
                year--;
            }
            YearReport.SelectedIndex = 0;
            YearReport.DropDownStyle = ComboBoxStyle.DropDownList;

            label14.Visible = false;
            YearReport.Visible = false;

            comboBox1.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;

            

            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Connection is open");
            }

            List<string> DepartmentNames = new List<string>();
            SqlCommand sqlCommand = new SqlCommand("SELECT Name FROM Department", sqlConnection);
            
            SqlDataReader reader = sqlCommand.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                DepartmentNames.Add(reader.GetValue(0).ToString());
                //comboBox6.Items.Add(DepartmentNames[i]);
                i++;
            }
            reader.Close();

            List<string> SpecializationNames = new List<string>();
            sqlCommand = new SqlCommand("SELECT Name FROM Specialization", sqlConnection);
            reader = sqlCommand.ExecuteReader();
            
            i = 0;
            while (reader.Read())
            {
                SpecializationNames.Add(reader.GetValue(0).ToString());
                comboBox5.Items.Add(SpecializationNames[i]);
                i++;
            }
            reader.Close();

            

            RefreshTable(tableLayer);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            checkBox4.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            comboBox5.Enabled = false;
            comboBox4.Enabled = checkBox3.Checked;
            comboBox4.SelectedIndex = 0;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            comboBox5.Enabled = checkBox2.Checked;
            comboBox4.Enabled = false;
            comboBox5.SelectedIndex = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox4.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            comboBox5.Enabled = false;
            comboBox4.Enabled = checkBox1.Checked;
            comboBox4.SelectedIndex = 0;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox4.Checked;
            comboBox1.SelectedIndex = 0;
        }

        private void SaveExcel(DataGridView dataGridView) // Сохранить в Excel
        {
            try
            {
                Excel.Application excelapp = new Excel.Application();
                    
                Excel.Workbook workbook = excelapp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                for (int i = 1; i < dataGridView.ColumnCount + 1; i++)
                {
                    worksheet.Rows[1].Columns[i] = dataGridView.Columns[i - 1].HeaderCell.Value;
                }
                for (int i = 2; i < dataGridView.RowCount + 2; i++)
                {
                    for (int j = 1; j < dataGridView.ColumnCount + 1; j++)
                    {
                        worksheet.Rows[i].Columns[j] = dataGridView.Rows[i - 2].Cells[j - 1].Value;
                    }
                }

                excelapp.AlertBeforeOverwriting = false;

                excelapp.Visible = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveExcel(dataGridView1);
        }

        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }



        /*
Выходная информация на печать:
   - сводная ведомость для заданного отделения и года
  отчетности:
  специальность, факультет, обучается на 1-м курсе,
  обучается на 2-м курсе, обучается на 3-м курсе,
  обучается на 4-м курсе, окончили ВУЗ;
   - ведомость для заданного факультета:
  год отчетности, специальность, отделение, принято на 1
  курс, окончили ВУЗ.

Выходная информация на экран:
   - для заданной специальности:
  год отчетности, отделение, принято на 1 курс, окончили
  ВУЗ;
   - для заданного года отчетности:
  специальность, отделение, факультет, общее количество
  (1-5 курс) обучающихся по специальности;
   - для заданного года отчетности:
  факультет, отделение, обучается на 1-м курсе, 2-м курсе,
  3-м курсе, 4-м курсе, 5-м курсе.
  */


        private void Select_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked && checkBox4.Checked)
            {
                string studyTime = comboBox1.Text.Trim();
                //string yearReport = comboBox4.SelectedItem.ToString();
                string nameSpec = comboBox5.Text.Trim();
                SqlCommand sqlCommand = new SqlCommand($"SELECT Uid FROM Specialization WHERE Name='{nameSpec}'", sqlConnection);

                //sqlConnection.Open();
                string uidSpec = sqlCommand.ExecuteScalar().ToString();
                //sqlConnection.Close();

                string sql = $@"
                                SELECT Name, Course, Uid, PeopleAccepted, NumberOfPeopleNow, YearReport, isEnded
                                FROM Group2
                                WHERE Uid = '{uidSpec}' AND StudyTime = N'{studyTime}'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }
            // По заданной специальности
            else if (checkBox2.Checked)
            {
                //string yearReport = comboBox4.SelectedItem.ToString();
                string nameSpec = comboBox5.Text.Trim();
                SqlCommand sqlCommand = new SqlCommand($"SELECT Uid FROM Specialization WHERE Name='{nameSpec}'", sqlConnection);

                //sqlConnection.Open();
                string uidSpec = sqlCommand.ExecuteScalar().ToString();
                //sqlConnection.Close();

                string sql = $"SELECT Name, YearAccepted, StudyTime, Course, Uid, PeopleAccepted, NumberOfPeopleNow, YearReport, isEnded FROM Group2 WHERE Uid = '{uidSpec}'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }

            else if (checkBox3.Checked && checkBox4.Checked)
            {
                string studyTime = comboBox1.Text.Trim();
                string yearReport = comboBox4.SelectedItem.ToString();
                string sql = $@"
                SELECT Specialization.Name, Summ.Uid, SumPeople 
                FROM 
	                (SELECT Group2.Uid, Group2.StudyTime, sum(Group2.NumberOfPeopleNow) 
	                AS SumPeople 
	                FROM Group2 
	                WHERE Group2.YearReport = '{yearReport}' 
	                GROUP BY Group2.Uid, Group2.StudyTime) 
                AS Summ, Specialization WHERE Summ.Uid = Specialization.Uid AND Summ.StudyTime = N'{studyTime}'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }
            // для заданного года отчетности кол-во людей на специальности со всех курсов
            else if (checkBox3.Checked)
            {
                string yearReport = comboBox4.SelectedItem.ToString();
                
                string sql = $"SELECT Specialization.Name, Summ.Uid, SumPeople FROM " +
                                        $"(SELECT Group2.Uid, sum(Group2.NumberOfPeopleNow) AS SumPeople " +
                                        $"FROM Group2 " +
                                        $"WHERE Group2.YearReport = '{yearReport}' " +
                                        $"GROUP BY Group2.Uid) " +
                            $"AS Summ, Specialization " +
                            $"WHERE Summ.Uid = Specialization.Uid";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }

            else if (checkBox1.Checked && checkBox4.Checked)
            {
                string studyTime = comboBox1.Text.Trim();
                string yearReport = comboBox4.SelectedItem.ToString();

                string sql = $@"
                SELECT Specialization.Name, p.Uid, [1], [2], [3], [4], [5] 
                FROM Specialization, (SELECT Group2.Uid, Group2.YearReport, Group2.Course, Group2.NumberOfPeopleNow 
	                AS SumPeople 
	                FROM Group2
	                WHERE Group2.YearReport = '{yearReport}' AND Group2.StudyTime = N'{studyTime}'
	                GROUP BY Group2.Uid, Group2.YearReport, Group2.Course, Group2.NumberOfPeopleNow) as T
	                PIVOT (Sum(SumPeople) FOR T.Course IN ([1], [2], [3], [4], [5])) AS p
	                WHERE Specialization.Uid = p.Uid";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }

            else if (checkBox1.Checked)
            {
                string yearReport = comboBox4.SelectedItem.ToString();

                string sql = $@"SELECT Specialization.Name, p.Uid, [1], [2], [3], [4], [5] 
                            FROM Specialization, (SELECT Group2.Uid, Group2.YearReport, Group2.Course, Group2.NumberOfPeopleNow
                            AS SumPeople
                            FROM Group2
                            WHERE Group2.YearReport = '{yearReport}'
                            GROUP BY Group2.Uid, Group2.YearReport, Group2.Course, Group2.NumberOfPeopleNow) as T
                            PIVOT(Sum(SumPeople) FOR T.Course IN([1], [2], [3], [4], [5])) AS p
                            WHERE Specialization.Uid = p.Uid";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, sqlConnection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].HeaderText = translatedColumns[dataGridView1.Columns[i].Name];
        }


        // Код вкладки База данных

        /*
         * tableLayer:
         * 1 - University
         * 2 - Institute
         * 3 - Depatment
         * 4 - Specialization
         * 5 - Grouping
         */

        int tableLayer = 1;
        Dictionary<string, string> translatedColumns = new Dictionary<string, string>()
        {
            {"Name", "Название"},
            {"Name1", "Название"},
            {"Rector", "Ректор"},
            {"NumberOfPhone", "Номер Телефона"},
            {"InternetSite", "Сайт"},
            {"Director", "Директор"},
            {"UidUniversity", "Код Университета"},
            {"Uid", "Код"},
            {"Address", "Адрес"},
            {"UidInstitute", "Код Института"},
            {"UidDepartment", "Код Кафедры"},
            {"StudyCost", "Цена Обучения"},
            {"TotalYearsStudy", "Кол-во лет обучения"},
            {"TypeOfStudy", "Тип обучения"},
            {"Year", "Год поступления"},
            {"StudyTime", "Отделение"},
            {"Course", "Курс"},
            {"UidSpecialization", "Код Специализации"},
            {"Accepted", "Принято"},
            {"firstYear", "Первый курс"},
            {"secondYear", "Второй курс"},
            {"thirdYear", "Третий курс"},
            {"FourthYear", "Четвертый курс"},
            {"FifthYear", "Пятый курс"},
            {"isEnded", "Окончили"},
            {"UID", "Код"},
            {"EndStudy", "Закончили обучение"},
            {"YearAccepted", "Год поступления"},
            {"PeopleAccepted", "Зачислено"},
            {"NumberOfPeopleNow", "Обучается / Закончили"},
            {"YearReport", "Год отчетности"},
            {"SumPeople", "Кол-во студентов"},
            {"Id", "Id"},
            {"1", "1 курс"},
            {"2", "2 курс"},
            {"3", "3 курс"},
            {"4", "4 курс"},
            {"5", "5 курс"}
            
        };
        private void RefreshTable(int tableLayer)
        {
            SqlDataAdapter dataAdapter = null;

            label14.Visible = false;
            YearReport.Visible = false;

            // TODO: После добавления атрибутов в БД вписать их сюда
            switch (tableLayer)
            {
                case 1:
                    dataAdapter = new SqlDataAdapter(
                "SELECT Uid, Name, Rector, NumberOfPhone, InternetSite FROM University", sqlConnection);
                    NameTable.Text = "Университеты";
                    break;
                case 2:
                    dataAdapter = new SqlDataAdapter(
                "SELECT Institute.Uid, Institute.Name, Institute.Director, Institute.Address, Institute.NumberOfPhone, University.Name " +
                "FROM Institute, University WHERE Institute.UidUniversity = University.UID", sqlConnection);
                    NameTable.Text = "Институты";
                    break;
                case 3:
                    dataAdapter = new SqlDataAdapter(
                "SELECT Department.Uid, Department.Name, Department.NumberOfPhone, Department.Director, Department.Address, Institute.Name " +
                "FROM Department, Institute WHERE Department.UidInstitute = Institute.Uid", sqlConnection);
                    NameTable.Text = "Кафедры";
                    break;
                case 4:
                    dataAdapter = new SqlDataAdapter(
                "SELECT Specialization.Name, Specialization.Uid, Specialization.StudyCost, Specialization.TotalYearsStudy, Specialization.TypeOfStudy, Department.Name" +
                " FROM Specialization, Department WHERE Specialization.UidDepartment = Department.Uid", sqlConnection);
                    NameTable.Text = "Специальности";
                    break;
                case 5:
                    label14.Visible = true;
                    YearReport.Visible = true;
                    // Добавить атрибут typeOfstudy
                    string sql = $"SELECT Specialization.TypeOfStudy, Group2.* FROM Specialization, Group2 WHERE YearReport = {YearReport.SelectedItem} AND Group2.Uid = Specialization.Uid";
                    dataAdapter = new SqlDataAdapter(sql, sqlConnection);
                    NameTable.Text = "Группы";

                    break;
            }

            if (dataAdapter == null)
                return;

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            

            dataGridView2.DataSource = dataSet.Tables[0];

            dataGridView2.Columns["Name"].ReadOnly = false;

            if (tableLayer == 4 || tableLayer == 5)
            {
                dataGridView2.Columns["Uid"].Visible = true;
                if (tableLayer == 5)
                    dataGridView2.Columns["Id"].Visible = false;
            }
            else dataGridView2.Columns["Uid"].Visible = false;

            

            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                dataGridView2.Columns[i].HeaderText = translatedColumns[dataGridView2.Columns[i].Name];

        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshTable(tableLayer);
        }

        private void toLeft_Click(object sender, EventArgs e)
        {
            if (tableLayer == 1)
            {
                return;
            }
            else
            {
                tableLayer--;
                RefreshTable(tableLayer);
            }
        }

        private void toRight_Click(object sender, EventArgs e)
        {
            if (tableLayer == 5)
            {
                return;
            }
            else
            {
                tableLayer++;
                RefreshTable(tableLayer);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            switch (tableLayer)
            {
                case 1:
                    new AddUniversity().Show();
                    break;
                case 2:
                    new AddInstitute().Show();
                    break;
                case 3:
                    new AddDepartment().Show();
                    break;
                case 4:
                    new AddSpecialization().Show();
                    break;
                case 5:
                    new AddGroup2().Show();
                    break;
            }
            RefreshTable(tableLayer);
        }
        /*
         * tableLayer:
         * 1 - University
         * 2 - Institute
         * 3 - Depatment
         * 4 - Specialization
         * 5 - Grouping
         */
        private void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            string[] Names = new string[] { "University", "Institute", "Department", "Specialization", "Group2" };
            string tableName = Names[tableLayer - 1];

            if (tableName == "Group2")
            {
                if (MessageBox.Show("Вы действительно хотите удалить группу?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
                int Id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Id"].Value);
                //label1.Text = Id.ToString();
                sql = $"DELETE {tableName} WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
                RefreshTable(tableLayer);
                return;
            }

            object Uid;

            if (tableName == "Specialization")
            {
                if (MessageBox.Show("Вы действительно хотите удалить специализацию? Все связанные группы будут каскадно удалены.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
                Uid = dataGridView2.SelectedRows[0].Cells["Uid"].Value.ToString();
            }
            else Uid = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Uid"].Value);

            if (tableName == "Department")
            {
                if (MessageBox.Show("Вы действительно хотите удалить кафедру? Все связанные записи будут каскадно удалены.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }

            if (tableName == "Institute")
            {
                if (MessageBox.Show("Вы действительно хотите удалить институт? Все связанные записи будут каскадно удалены.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }

            if (tableName == "University")
            {
                if (MessageBox.Show("Вы действительно хотите удалить университет? Все связанные записи будут каскадно удалены.", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }

            //label1.Text = Uid.ToString();
            sql = $"DELETE {tableName} WHERE Uid = @pId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@pId", Uid);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Строка не может быть удалена из-за существующих связей.");
            }

            RefreshTable(tableLayer);
        }

        private void YearReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sqlConnection.State == ConnectionState.Open)
                RefreshTable(tableLayer);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sqlConnection.Close();
        }

        bool isChanged = false;
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            isChanged = !isChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"
Выходная информация на экран:
   - сводная ведомость для заданного отделения и года
  отчетности:
  специальность, обучается на 1-м курсе,
  обучается на 2-м курсе, обучается на 3-м курсе,
  обучается на 4-м курсе, окончили ВУЗ;
   - ведомость для заданной специальности:
  год отчетности, специальность, отделение, принято на 1
  курс, окончили ВУЗ.
   - для заданной специальности:
  год отчетности, отделение, принято на 1 курс, окончили
  ВУЗ;
   - для заданного года отчетности:
  специальность, отделение, факультет, общее количество
  (1-5 курс) обучающихся по специальности;
   - для заданного года отчетности:
  факультет, отделение, обучается на 1-м курсе, 2-м курсе,
  3-м курсе, 4-м курсе, 5-м курсе.
"
);
        }

        Bitmap bmp;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(bmp, 0, 0);
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            Print(dataGridView1);
        }

        private void Print(DataGridView dataGridView)
        {
            var pd = new PrintDocument();
            // Альбомная ориентация
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += (s, q) =>
            {
                var bmp = new Bitmap(dataGridView.Width, dataGridView.Height);
                dataGridView.DrawToBitmap(bmp, dataGridView.ClientRectangle);
                q.Graphics.DrawImage(bmp, new Point(100, 100));
            };

            printPreviewDialog1.Document = pd;
            printPreviewDialog1.ShowDialog();
        }

        private void printDialog1_HelpRequest(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        /*
         * tableLayer:
         * 1 - University
         * 2 - Institute
         * 3 - Depatment
         * 4 - Specialization
         * 5 - Grouping
         */
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDataAdapter;
            string sql = "";

            switch (tableLayer)
            {
                case 2:
                    if (e.ColumnIndex == 5)
                    {
                        int Uid = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                        MessageBox.Show(Uid.ToString());
                        sql = "SELECT Uid, Name FROM University";
                        sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                        sqlDataAdapter.Fill(dataSet);
                        string where = $"Uid='{Uid}'";
                        string tableName = "Institute";
                        string what = "UidUniversity";
                        new Changer(dataSet, tableName, what, where).ShowDialog();
                        
                    }
                    break;
                case 3:
                    if (e.ColumnIndex == 5)
                    {
                        int Uid = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                        sql = "SELECT Uid, Name FROM Institute";
                        sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                        sqlDataAdapter.Fill(dataSet);
                        string where = $"Uid='{Uid}'";
                        string tableName = "Department";
                        string what = "UidInstitute";
                        new Changer(dataSet, tableName, what, where).ShowDialog();
                    }
                    break;
                case 4:

                    if (e.ColumnIndex == 4)
                    {
                        dataSet = new DataSet();

                        DataTable dt = new DataTable("Тип обучения");
                        
                        dt.Columns.Add(new DataColumn("type", typeof(string)));

                        dt.Rows.Add("Бакалавриат");
                        dt.Rows.Add("Специалитет");
                        dt.Rows.Add("Магистратура");
                        dataSet.Tables.Add(dt);

                        string Uid = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string where = $"Uid='{Uid}'";
                        string tableName = "Specialization";
                        string what = "TypeOfStudy";
                        new Changer(dataSet, tableName, what, where).ShowDialog();
                    }

                    if (e.ColumnIndex == 5)
                    {
                        string Uid = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                        sql = "SELECT Uid, Name FROM Department";
                        sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                        sqlDataAdapter.Fill(dataSet);
                        string where = $"Uid='{Uid}'";
                        string tableName = "Specialization";
                        string what = "UidDepartment";
                        new Changer(dataSet, tableName, what, where).ShowDialog();
                    }
                    break;
                case 5:
                    
                    if (e.ColumnIndex == 4)
                    {
                        dataSet = new DataSet();

                        DataTable dt = new DataTable("Время обучения");

                        dt.Columns.Add(new DataColumn("type", typeof(string)));

                        dt.Rows.Add("Очное");
                        dt.Rows.Add("Заочное");
                        dataSet.Tables.Add(dt);

                        string name = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                        string where = $"Name='{name}'";
                        string tableName = "Group2";
                        string what = "StudyTime";
                        new Changer(dataSet, tableName, what, where).ShowDialog();
                    }

                    if (e.ColumnIndex == 6)
                    {
                        string name = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                        
                        sql = "SELECT Uid, Name FROM Specialization";
                        sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                        sqlDataAdapter.Fill(dataSet);
                        string where = $"Name='{name}'";
                        string tableName = "Group2";
                        string what = "Uid";
                        new Changer(dataSet, tableName, what, where).ShowDialog();
                    }
                    break;
            }
            RefreshTable(tableLayer);
        }
        string def;
        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            def = dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString();
        }


        /*
         * tableLayer:
         * 1 - University
         * 2 - Institute
         * 3 - Depatment
         * 4 - Specialization
         * 5 - Grouping
         */
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string content = dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString();
            string tableName = "";
            string what = "";
            string where = "";
            string uid = "";
            SqlCommand sqlCommand;
            if (content != def)
            {

                if (content.Length == 0)
                {
                    MessageBox.Show("Поле не может быть пустым!");
                    return;
                }

                what = dataGridView2.Columns[e.ColumnIndex].Name.ToString();
                uid = dataGridView2[0, e.RowIndex].Value.ToString();

                switch (tableLayer)
                {
                    case 1:
                        tableName = "University";
                        where = $"Uid='{uid}'";

                        if (e.ColumnIndex == 2)
                        {
                            foreach (var symbol in content.Trim())
                                if (!char.IsLetter(symbol) && symbol != ' ')
                                {
                                    MessageBox.Show("Поле 'Ректор' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }
                        }

                        if (e.ColumnIndex == 3)
                        {
                            foreach (var symbol in content)
                                if (!char.IsDigit(symbol))
                                {
                                    MessageBox.Show("Поле 'Номер телефона' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }
                        }
                        break;
                    case 2:
                        tableName = "Institute";
                        where = $"Uid='{uid}'";

                        if (e.ColumnIndex == 2)
                        {
                            foreach (var symbol in content.Trim())
                                if (!char.IsLetter(symbol) && symbol != ' ')
                                {
                                    MessageBox.Show("Поле 'Директор' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }
                        }

                        if (e.ColumnIndex == 3)
                        {
                            foreach (var symbol in content)
                                if (!char.IsDigit(symbol))
                                {
                                    MessageBox.Show("Поле 'Номер телефона' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }
                        }

                        break;
                    case 3:
                        tableName = "Department";
                        where = $"Uid='{uid}'";
                        break;
                    case 4:
                        tableName = "Specialization";
                        if (e.ColumnIndex == 1)
                        {
                            foreach (var symbol in content.Trim())
                                if (!char.IsDigit(symbol) && symbol != '.')
                                {
                                    MessageBox.Show("Поле 'Код' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }

                            where = $"Name='{uid}'";
                            break;
                        }

                        if (e.ColumnIndex == 0)
                        {
                            foreach (var symbol in content.Trim())
                                if (!char.IsLetter(symbol) && symbol != ' ')
                                {
                                    MessageBox.Show("Поле 'Название' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }

                            
                        }

                        if (e.ColumnIndex == 2)
                        {
                            foreach (var symbol in content.Trim())
                                if (!char.IsDigit(symbol))
                                {
                                    MessageBox.Show("Поле 'Цена обучения' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }
                        }


                        if (e.ColumnIndex == 3)
                        {
                            MessageBox.Show("Данное поле изменить нельзя");
                            RefreshTable(tableLayer);
                            return;
                        }

                        string _uid = dataGridView2[1, e.RowIndex].Value.ToString();
                        
                        where = $"Uid='{_uid}'";
                        break;
                    case 5:

                        if (e.ColumnIndex == 0)
                        {
                            MessageBox.Show("Данное поле изменить нельзя");
                            RefreshTable(tableLayer);
                            return;
                        }

                        if (e.ColumnIndex == 3)
                        {
                            MessageBox.Show("Данное поле изменить нельзя");
                            RefreshTable(tableLayer);
                            return;
                        }

                        if (e.ColumnIndex == 5)
                        {
                            MessageBox.Show("Данное поле изменить нельзя");
                            RefreshTable(tableLayer);
                            return;
                        }

                        if (e.ColumnIndex == 9)
                        {
                            MessageBox.Show("Данное поле изменить нельзя");
                            RefreshTable(tableLayer);
                            return;
                        }

                        if (e.ColumnIndex == 10)
                        {
                            MessageBox.Show("Данное поле изменить нельзя");
                            RefreshTable(tableLayer);
                            return;
                        }

                        if (e.ColumnIndex == 7)
                        {
                            foreach (var symbol in content)
                                if (!char.IsDigit(symbol))
                                {
                                    MessageBox.Show("Поле 'Зачислено' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }
                        }
                        string name = dataGridView2[2, e.RowIndex].Value.ToString();
                        where = $"Name='{name}'";

                        if (e.ColumnIndex == 8)
                        {
                            where = $"Name='{name}' AND YearReport='{YearReport.Text}'";
                            foreach (var symbol in content)
                                if (!char.IsDigit(symbol))
                                {
                                    MessageBox.Show("Поле 'Обучается \\ Закончили' содержит запрещенные символы");
                                    RefreshTable(tableLayer);
                                    return;
                                }
                        }

                        tableName = "Group2";
                        break;
                }
                
                
                string set = $"{what}='{content}'";

                string sql = $"UPDATE {tableName} SET {set} WHERE {where}";

                sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            else return;
        }

        private void dataGridView2_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Редактируемое поле содержит запрещенные символы.");
            return;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveExcel(dataGridView2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Print(dataGridView2);
        }
    }
}
