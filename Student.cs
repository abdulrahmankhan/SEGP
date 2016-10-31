using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Base;

namespace SEGP
{
    public partial class Student : UserControl
    {
        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=segp1;UID=root;Password=");
        String ss = "select * from Students";
        MySqlCommand command = new MySqlCommand();
        public Student()
        {
            InitializeComponent();
        }
 

        public void reload()
        {
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(ss, con);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataadapter.Fill(table);
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = table;
                gridControl1.DataSource = bindingSource1;
            }
            catch (Exception a)
            {
                MessageBox.Show("Something is Wrong" + a.ToString());
            }
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.reload();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try {
                DevExpress.XtraGrid.Views.Grid.GridView view = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view != null)
                {
                    view.ExportToPdf("Students.pdf");
                    Process pdf = new Process();
                    pdf.StartInfo.FileName = "FoxitReader.exe";
                    pdf.StartInfo.Arguments = "Students.pdf";
                    pdf.Start();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Error: " + a);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColumnView view = gridControl1.FocusedView as ColumnView;
            try
            {
                con.Open();
                for (int i = 0; i < view.RowCount; i++)
                {
                    command = con.CreateCommand();
                    System.Data.DataRow row = view.GetDataRow(i);
                    int UOB = Convert.ToInt32(row[0].ToString());
                    String Name = row[1].ToString();
                    String FName = row[2].ToString();
                    String Email = row[3].ToString();
                    String Contact = row[4].ToString();
                    String Programm = (row[5].ToString());
                    String Status = row[6].ToString();
                    String s = "UPDATE students set `Name` = '" + Name + "', `Father Name` ='" + FName + "',`Email` = '" + Email + "',`Contact` = '" + Contact + "',`Programm`='" +
                        Programm + "',`Status`='" + Status + "' WHERE `UOB`='" + UOB + "'";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Something is Wrong" + a.ToString());
            }
            finally
            {
                MessageBox.Show("Data has been updated!");
            }
            
        }
    }
}
