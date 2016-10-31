using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Base;
using System.Data;
using System.Diagnostics;
using DevExpress.XtraReports.UI;

namespace SEGP
{
    public partial class Home : UserControl
    {
        MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=segp1;UID=root;Password=");

        List<Student1> st_lst = new List<Student1>();
        List<PAT> pt_list = new List<PAT>();
        List<Allocate> al_list = new List<Allocate>(); 

        Allocations pat = new Allocations();
        MySqlDataReader reader = null;
        public void readStudent()
        {
            st_lst = pat.connect();
            pt_list = pat.getPatList();

            MySqlCommand command = new MySqlCommand();
            con.Open();
            command = con.CreateCommand();
            command.CommandText = "SELECT* from allocations";
            reader = command.ExecuteReader();
            Allocate al = null;
            while (reader.Read())
            {
                al = new Allocate();
                al.AID = reader.GetInt32(0);
                al.AUOB = reader.GetInt32(1);
                al_list.Add(al);

            }
            reader.Close();
            con.Close();


        }
 

        public Home()
        {
            
            InitializeComponent();
        }
 

        public void reload()
        {
            int counter1=0;
            int counter;
            readStudent();
            ColumnView view = gridControl1.FocusedView as ColumnView;
                            Console.WriteLine( view.RowCount +"beghairat");
            foreach (PAT p in pt_list)
            {
                String s1 = p.name;
                view.AddNewRow();
                int count = view.RowCount - 1;
                view.SetRowCellValue(counter1, view.Columns[0], s1);
                foreach (Student1 student in st_lst)
                {
                    counter = 1;
                    foreach (Allocate al in al_list)
                    {
                        if (p.ID == al.AID && student.UOB == al.AUOB)
                        {
                            String s = student.name;
                            view.SetRowCellValue(counter1, view.Columns[counter], s);

                            counter++;
                        }
                    }
                }
                counter1++;
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            new  Allocations().connect();
            reload();
        }
 
       

        public class Allocate{
            public int AID { get; set; }
            public int AUOB { get; set; }

        }

    
        

        private void Home_Load_1(object sender, EventArgs e)
        {
            pat.connect();
            int counter;
            readStudent();
                
            DataTable dt = new DataTable();
            DataRow dr = null ;
            int count =0;
            foreach (PAT p in pt_list)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("PAT Name", typeof(string));
                    dt.Columns.Add("Student 1", typeof(string));
                    dt.Columns.Add("Student 2", typeof(string));
                    dt.Columns.Add("Student 3", typeof(string));
                    dt.Columns.Add("Student 4", typeof(string));
                    dt.Columns.Add("Student 5", typeof(string));
                    dt.Columns.Add("Student 6", typeof(string));
                    dt.Columns.Add("Student 7", typeof(string));
                    dt.Columns.Add("Student 8", typeof(string));
                     
                }
                dr = dt.NewRow();
                
                dr[0] = p.name;

                    counter = 1;
                foreach (Student1 student in st_lst)
                {
                    foreach (Allocate al in al_list)
                    {
                        if (p.ID == al.AID && student.UOB == al.AUOB)
                        {
                            String s = student.name;
                            dr[counter] = s;
                            
                            counter++;
                        }
                    }
                }
                dt.Rows.Add();
                dt.Rows.InsertAt(dr,count);
                count++;
            }
            gridControl1.DataSource = dt;
            gridControl1.Refresh();

        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                

                DevExpress.XtraGrid.Views.Grid.GridView view = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view != null)
                {
                    view.ExportToPdf("Home.pdf");
           
                    Process pdf = new Process();
                    pdf.StartInfo.FileName = "FoxitReader.exe";
                    pdf.StartInfo.Arguments = "Home.pdf";
                    pdf.Start();
                }
            }
            catch(Exception a)
            {
                MessageBox.Show("Error: "+a);
            }
        }

        
    }
}
