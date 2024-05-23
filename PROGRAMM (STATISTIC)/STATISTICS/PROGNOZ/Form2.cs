using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PROGNOZ
{
    public partial class Form2 : Form
    {
        public static string ConnectString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = baseCollege.mdb";
        private OleDbConnection myConnection;
        public Form2()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(ConnectString);
            myConnection.Open();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baseCollegeDataSet.history' table. You can move, or remove it, as needed.
            this.historyTableAdapter.Fill(this.baseCollegeDataSet.history);
            // TODO: This line of code loads data into the 'baseCollegeDataSet.special' table. You can move, or remove it, as needed.
            this.specialTableAdapter.Fill(this.baseCollegeDataSet.special);
            // TODO: This line of code loads data into the 'baseCollegeDataSet.zvazok' table. You can move, or remove it, as needed.
            this.zvazokTableAdapter.Fill(this.baseCollegeDataSet.zvazok);
            // TODO: This line of code loads data into the 'baseCollegeDataSet.zaclad' table. You can move, or remove it, as needed.
            this.zacladTableAdapter.Fill(this.baseCollegeDataSet.zaclad);

        }
        // ВИДАЛЕННЯ закладу
        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox13.Text);
            string query = " DELETE FROM zaclad WHERE id_za=" + n + "";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
            MessageBox.Show("Дані про навчальний заклад видалено");
            this.zacladTableAdapter.Fill(this.baseCollegeDataSet.zaclad);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Додавання навчального закладу
        private void button3_Click(object sender, EventArgs e)
        {
            int nomer = Convert.ToInt32(textBox1.Text);
            string name = textBox2.Text;
            string misto = textBox3.Text;
            string query = "INSERT INTO zaclad(id_za,namecol,city) VALUES(" + nomer + ",'" + name + "','" + misto + "')";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
            MessageBox.Show("Дані про навчальний заклад додано");
            this.zacladTableAdapter.Fill(this.baseCollegeDataSet.zaclad);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Додавання нової спеціальності
        private void button5_Click(object sender, EventArgs e)
        {
            int nomersp = Convert.ToInt32(textBox4.Text);
            string namesp = textBox5.Text;
            string query1 = "INSERT INTO special(kod_sp,name_sp) VALUES(" + nomersp + ",'" + namesp + "')";
            OleDbCommand command = new OleDbCommand(query1, myConnection);
            command.ExecuteNonQuery();
            MessageBox.Show("Інформацію про спеціальність додано");
            this.specialTableAdapter.Fill(this.baseCollegeDataSet.special);
            textBox4.Text = " ";
            textBox5.Text = " ";
        }
        // Видалення спеціальності
        private void button6_Click(object sender, EventArgs e)
        {
            int nomersp = Convert.ToInt32(textBox6.Text);
            string query = " DELETE FROM special WHERE kod_sp=" + nomersp + "";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
            MessageBox.Show("Дані про спеціальність видалено");
            this.specialTableAdapter.Fill(this.baseCollegeDataSet.special);
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.historyTableAdapter.FillBy(this.baseCollegeDataSet.history);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy2ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.historyTableAdapter.FillBy2(this.baseCollegeDataSet.history);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Додавання даних про кількість вступників у таблицю history
           
            int k=0;
                int nom = Convert.ToInt32(textBox7.Text);
                int nomspec = Convert.ToInt32(textBox8.Text);
                int kol_1 = Convert.ToInt32(textBox9.Text);
                int kol_2 = Convert.ToInt32(textBox10.Text);
                double kol_3 = Convert.ToDouble(textBox11.Text);
                string query3 = "SELECT nom_row FROM zvazok WHERE (id_za =" + nom + ") and (kod_sp=" + nomspec + ")";
                OleDbDataAdapter command = new OleDbDataAdapter(query3, myConnection);
                DataTable dt = new DataTable();
               command.Fill(dt);
            dataGridView4.DataSource = dt;
            dataGridView4.ColumnHeadersVisible = false;

            k = Convert.ToInt32(dataGridView4[0,0].Value);
            string query4 = "INSERT INTO history(year_sp,count_sp,avg_bal,nom_row) VALUES(" + kol_1 + "," + kol_2 + "," + kol_3 + "," + k + ")";
            OleDbCommand command1 = new OleDbCommand(query4, myConnection);
             command1.ExecuteNonQuery();
                

            MessageBox.Show("Інформацію про кількість вступників додано");
                
               

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.zacladTableAdapter.Fill(this.baseCollegeDataSet.zaclad);
            this.zvazokTableAdapter.Fill(this.baseCollegeDataSet.zvazok);
            this.historyTableAdapter.Fill(this.baseCollegeDataSet.history);
            MessageBox.Show("Інформацію оновлено");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int kod = Convert.ToInt32(textBox12.Text);
            string query = " DELETE FROM history WHERE id=" + kod + "";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
            MessageBox.Show("Дані  видалено");
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

