using MySql.Data.MySqlClient;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SEGP
{
    public partial class Add_New : UserControl
    {
        
        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=segp1;UID=root;Password=");

        public Add_New()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            String s1 = textBox1.Text;
            String s2 = textBox2.Text;
            String s3 = textBox3.Text;
            String s4 = textBox4.Text;
            String s5 = textBox5.Text;
            String s6 = textBox6.Text;
          
            if (Validation.Validation.checkname(s1) && Validation.Validation.checkname(s2) && Validation.Validation.checkEmail(s3) && Validation.Validation.checkUOB(s5) && Validation.Validation.checkPhoneNo(s4))
            {
                try {
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter("INSERT INTO students VALUES ('" + s5 + "',  '" + s1 + " ',  '" + s2 + "',  '" + s3 + "',  '" + s4 + "',  '" + s6 + "','Not Assigned')", con);
                    sda.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("Data hes been saved!");
                }
                catch
                {
                    MessageBox.Show("Error occured: Inserting to databse has been canceled! ");
                }
            }
            else
            {
                MessageBox.Show("Please provide correct inputs!");
            }
         

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String s7 = textBox7.Text;
            String s8 = textBox8.Text;
          
            String s10 = textBox10.Text;
            String s11 = textBox11.Text;
           if (Validation.Validation.checkname(s7) && Validation.Validation.checkname(s10) && Validation.Validation.checkEmail(s8) &&  Validation.Validation.checkPhoneNo(s11))
            {
                try {
                    con.Open();
                    
                    MySqlCommand command = new MySqlCommand();
                    command = con.CreateCommand();
                    command.CommandText = "SELECT MAX(ID)FROM pat";
                    int ID = Convert.ToInt32(command.ExecuteScalar());
                    MySqlDataAdapter sda = new MySqlDataAdapter("INSERT INTO pat VALUES ('" + (ID + 1) + "','" + s7 + "',  '" + s10 + " ',  '" + s8 + "',  '" + s11 + "','" + "0', '" + comboBox1.Text + "')", con);
                    sda.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("Data hes been saved!");
                }
                catch
                {
                    MessageBox.Show("Error occured: Inserting to databse has been canceled! ");
                }

            }
            else
            {
                MessageBox.Show("Please provide correct inputs!");
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult op = openFileDialog1.ShowDialog();
            if (op == DialogResult.OK)
            {

            updateExcel(openFileDialog1.FileName,1);
            }
        }
        
        public void updateExcel(String FilePath,int ID)
        {

            string conn =   @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+FilePath+";" +
                        @"Extended Properties='Excel 8.0;HDR=Yes;'";

            OleDbConnection connection = new OleDbConnection(conn);
            connection.Open();
            con.Open();

            OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
            using (OleDbDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {

                    try {
                        if (ID == 1)
                        {
                            String ss = dr[4].ToString().Trim('-');

                            String s = "Insert into students values('" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + ss + "','" + dr[5] + "','" + dr[6] + "')";

                            MySqlDataAdapter sda = new MySqlDataAdapter(s, con);
                            sda.SelectCommand.ExecuteNonQuery();
                        }
                        else if (ID == 2)
                        {
                            String ss = dr[4].ToString().Trim('-');

                            String s = "Insert into pat values('" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + ss + "','" + dr[5] + "','" + dr[6] + "')";

                            MySqlDataAdapter sda = new MySqlDataAdapter(s, con);
                            sda.SelectCommand.ExecuteNonQuery();

                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
            }

            con.Close();
            connection.Close();


        }
    }

       

}
