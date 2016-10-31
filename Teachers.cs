using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Base;

namespace SEGP
{
    public partial class Teachers : UserControl
    {
        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=segp1;UID=root;Password=");
        String ss = "select * from pat";
        MySqlDataAdapter dataadapter = null;
        MySqlCommand command = new MySqlCommand();

        public Teachers()
        {
            InitializeComponent();
        }

        public void reload()
        {
            try
            {
                dataadapter = new MySqlDataAdapter(ss, con);

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
        private void Teachers_Load(object sender, EventArgs e)
        {
            reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                DevExpress.XtraGrid.Views.Grid.GridView view = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view != null)
                {
                    view.ExportToPdf("Teacher.pdf");
                    Process pdf = new Process();
                    pdf.StartInfo.FileName = "FoxitReader.exe";
                    pdf.StartInfo.Arguments = "Teacher.pdf";
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
                    int ID = Convert.ToInt32(row[0].ToString());
                    String Name = row[1].ToString();
                    String FName = row[2].ToString();
                    String Email = row[3].ToString();
                    String Contact = row[4].ToString();
                    int alloc = Convert.ToInt32(row[5].ToString());
                    String Status = row[6].ToString();
                    String s = "UPDATE pat set `Name` = '" + Name + "', `Father Name` ='" + FName + "',`Email` = '" + Email + "',`Contact` = '" + Contact + "',`Allocations`='" +
                        alloc + "',`Status`='" + Status + "' WHERE `ID`='" + ID + "'";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                }

                con.Close();
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
 
