using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections;

namespace SEGP
{
    public partial class Form1 : Form
    {
        MySqlConnection myConnection = new MySqlConnection("SERVER=localhost;DATABASE=segp1;UID=root;Password=");
        List<Student> st_list = new List<Student>();

        List<PAT> pat_list = new List<PAT>();
        int UOB1 ;
        String SStatus = null;
        int alloc;
        int PID;
        String PStatus=null;
        MySqlDataReader reader = null;
        public void connect()
        {
            st_list = null;
            st_list = new List<Student>();
            pat_list = null;
            pat_list = new List<PAT>();

            MySqlCommand command = new MySqlCommand();
            myConnection.Open();
            command = myConnection.CreateCommand();
            command.CommandText = "SELECT* from students";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Student stt = new Student();
                stt.UOB = reader.GetInt32(0);
                stt.name = reader.GetString(1);
                stt.status = reader.GetString(6);
            
                st_list.Add(stt);

            }
            reader.Close();


            command.CommandText = "SELECT * from pat";

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                PAT p = new PAT();
                p.ID = reader.GetInt32(0);
                p.name = reader.GetString(1);
                p.allocations = reader.GetInt16(4);
                p.status = reader.GetString(5);
                pat_list.Add(p);

            }
            reader.Close();
            myConnection.Close();

            
        }
        public void update()
        {
            myConnection.Open();
            MySqlCommand command = new MySqlCommand();
            command = myConnection.CreateCommand();

            int counter = 0;
            Student st;
            foreach (PAT p in pat_list)
            {
                int ID = p.ID;
                int alloc = p.allocations;
                String status = p.status;
                if (status.Equals("Full") && alloc < 8 && counter < st_list.Count)
                {
                    for (int i = alloc; i <= 8 && i <= st_list.Count; i++)
                    {

                        st = st_list[counter];
                        if (st.status.Equals("Not Assigned"))
                        {
                            counter++;
                            int a = i + 1;
                            Console.WriteLine(st.UOB + " " + st.status);
                            String s1 = "Update students SET Status= 'Assigned' WHERE UOB=" + st.UOB + ";";
                            String s2 = "Update pat SET Allocations=" + a + " WHERE ID=" + ID + ";";
                            String s3 = "INSERT into allocations (PAT_ID,UOB) values(" + ID + "," + st.UOB + ");";
                            command.CommandText = s3;
                            command.ExecuteNonQuery();
                            command.CommandText = s1;
                            command.ExecuteNonQuery();
                            command.CommandText = s2;
                            command.ExecuteNonQuery();

                        }
                    }
                    if ((status.Equals("Partial") || status.Equals("Part")) && alloc < 4 && counter < st_list.Count)
                    {

                        for (int i = alloc; i <= 4 && i <= st_list.Count; i++)
                        {
                                st = st_list[counter];
                                if (st.status.Equals("Not Assigned"))
                                {
                                    counter++;

                                    String s1 = "Update students SET status= Assigned WHERE UOB=" + st.UOB + ";";
                                    String s2 = "Update pat SET No.of_allocations=" + i + 1 + " WHERE ID=" + ID + ";";
                                    String s3 = "INSERT into allocations (PAT_ID,UOB) values(" + ID + "," + st.UOB + ");";
                                    command.CommandText = s1;
                                    command.ExecuteNonQuery();
                                    command.CommandText = s2;
                                    command.ExecuteNonQuery();
                                    command.CommandText = s3;
                                    command.ExecuteNonQuery();

                                
                            }
                        }
                    }
                }
            }
            myConnection.Close();            
    }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    
      
        private void button1_Click(object sender, EventArgs e)
        {
       
          
                Console.WriteLine("hello");
                myConnection.Open();
                MySqlCommand command = new MySqlCommand();
                command = myConnection.CreateCommand();

          
                if(((PStatus.Equals("Partial") && alloc<4)||(PStatus.Equals("Full") && alloc < 8)) && SStatus.Equals("Not Assigned"))
                {
                 
                    String s = "insert into allocations values("+PID+","+UOB1+")";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    String s1 = "Update students set status ='Assigned' where uob=" +UOB1;
                    command.CommandText = s1;
                    command.ExecuteNonQuery();
                    String s2 = "Update pat set Allocations = "+(alloc+1)+" where ID = "+PID;
                    command.CommandText = s2;
                    command.ExecuteNonQuery();
                    myConnection.Close();
                    connect();


                }
            else
            {
                Console.WriteLine("select student and pat to assign");
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connect();
            update();

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            int count = 0;

            foreach (PAT pt in pat_list)
            {
                if (pat_list.Count >= dataGridView2.RowCount)
                {
                    if (checkBox2.Checked)
                    {
                        dataGridView2.Rows.Add();

                        dataGridView2.Rows[count].Cells[0].Value = pt.ID;
                        dataGridView2.Rows[count].Cells[1].Value = pt.name;
                        dataGridView2.Rows[count].Cells[2].Value = pt.status;
                        dataGridView2.Rows[count].Cells[3].Value = pt.allocations;
                        count++;
                    }
                    else if ((pt.status.Equals("Full") && pt.allocations < 8) || (pt.status.Equals("Part") && pt.allocations < 4))
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[count].Cells[0].Value = pt.ID;
                        dataGridView2.Rows[count].Cells[1].Value = pt.name;
                        dataGridView2.Rows[count].Cells[2].Value = pt.status;
                        dataGridView2.Rows[count].Cells[3].Value = pt.allocations;
                        count++;
                    }
                }

            }
            count = 0;
            foreach (Student st in st_list)
            {
                Console.WriteLine(st.name);
                if (st_list.Count >= dataGridView1.RowCount)
                {
                    if (checkBox1.Checked)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[count].Cells[0].Value = st.UOB;
                        dataGridView1.Rows[count].Cells[1].Value = st.name;
                        dataGridView1.Rows[count].Cells[2].Value = st.status;
                       
                        count++;
                    }
                    else if (!st.status.Equals("Assigned"))
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[count].Cells[0].Value = st.UOB;
                        dataGridView1.Rows[count].Cells[1].Value = st.name;
                        dataGridView1.Rows[count].Cells[2].Value = st.status;

                        count++;
                    }
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int r = e.RowIndex;
            //Console.WriteLine(dataGridView1.Rows[r].Cells[2].ToString());
            UOB1 =Convert.ToInt32( dataGridView1.Rows[r].Cells[0].Value);
            SStatus = dataGridView1.Rows[r].Cells[2].Value.ToString();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int r = e.RowIndex;
            PID = Convert.ToInt32(dataGridView2.Rows[r].Cells[0].Value);
            PStatus =dataGridView2.Rows[r].Cells[2].Value.ToString();
            alloc = Convert.ToInt32(dataGridView2.Rows[r].Cells[3].Value.ToString());
            


        }
    }

    public class Student
    {

        public int UOB { get; set; }
        public String status { get; set; }
        public String name { get; set; }

    }
    public class PAT
    {
        public int ID { get; set; }

        public int allocations { get; set; }

        public String status { get; set; }

        public String name { get; set; }
    }
}
