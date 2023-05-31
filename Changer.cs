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
    public partial class Changer : Form
    {
        DataSet Dataset;
        
        string TableName;
        string What;
        string Where;

        private SqlConnection sqlConnection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString);

        DataSet dataSet = new DataSet();
        SqlCommand sqlCommand;
        
        public Changer(DataSet dataSet, string tableName, string what, string where)
        {
            InitializeComponent();
            Dataset = dataSet;
            TableName = tableName;
            What = what;
            Where = where;
        }

        private void Changer_Load_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Dataset.Tables[0];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();
            
            string Uid = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();




            string set = $"{What}='{Uid}'";

            string sql = $"UPDATE {TableName} SET {set} WHERE {Where}";

            sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            this.Close();
        }

        private void Changer_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
