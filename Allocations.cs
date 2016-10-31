using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace SEGP
{
    public partial class Allocations : UserControl
    {
        public Allocations()
        {
            InitializeComponent();
        }

        MySqlConnection myConnection = new MySqlConnection("SERVER=localhost;DATABASE=segp1;UID=root;Password=");
        List<Student1> st_list = new List<Student1>();

        List<PAT> pat_list = new List<PAT>() ;

        public List<PAT> getPatList()
        {
            return pat_list;
        }
   
        int UOB1;
        String SStatus ;
        int alloc;
        int PID;
        String PStatus  ;
        MySqlDataReader reader = null;
        public List<Student1> connect()
        {
            st_list = null;
            st_list = new List<Student1>();
            pat_list = null;
            pat_list = new List<PAT>();

            MySqlCommand command = new MySqlCommand();
            myConnection.Open();
            command = myConnection.CreateCommand();
            command.CommandText = "SELECT* from students";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Student1 stt = new Student1();
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
                p.allocations = reader.GetInt16(5);
                p.status = reader.GetString(6);
                pat_list.Add(p);

            }
       
            reader.Close();
            myConnection.Close();
            return st_list;


        }
        public void auto()
        {
            myConnection.Open();
            MySqlCommand command = new MySqlCommand();
            command = myConnection.CreateCommand();

            int counter = 0;

            try {
            foreach (PAT p in pat_list)
            {
                int ID = p.ID;
                int alloc = p.allocations;
                String status = p.status;
                    if (status.Equals("Full") && alloc < 8 && counter < st_list.Count)
                    {
                        for (int i = alloc; i <= 8 && i <= st_list.Count; i++)
                        {
                            foreach (Student1 s in st_list)
                            {

                                if (s.status.Equals("Not Assigned") && i < 8)
                                {

                                    String s1 = "Update students SET Status= 'Assigned' WHERE UOB=" + s.UOB + ";";
                                    String s2 = "Update pat SET Allocations=" + (i + 1) + " WHERE ID=" + ID + ";";
                                    String s3 = "INSERT into allocations (PAT_ID,UOB) values(" + ID + "," + s.UOB + ");";
                                    command.CommandText = s3;
                                    command.ExecuteNonQuery();
                                    command.CommandText = s1;
                                    command.ExecuteNonQuery();
                                    command.CommandText = s2;
                                    command.ExecuteNonQuery();
                                    s.status = "Assigned";
                                    i++;
                                    p.allocations = i;

                                }

                            }
                        }
                    }
                    if ((status.Equals("Partial") || status.Equals("Part")) && alloc < 4 && counter < st_list.Count)
                    {

                        for (int i = alloc; i <= 4 && i <= st_list.Count; i++)
                        {
                            foreach (Student1 st in st_list)
                            {
                                if (st.status.Equals("Not Assigned") && i < 4)
                                {
                                    String s1 = "Update students SET Status= 'Assigned' WHERE UOB=" + st.UOB + ";";
                                    String s2 = "Update pat SET Allocations=" + (i + 1) + " WHERE ID=" + ID + ";";
                                    String s3 = "INSERT into allocations (PAT_ID,UOB) values(" + ID + "," + st.UOB + ");";
                                    command.CommandText = s3;
                                    command.ExecuteNonQuery();
                                    command.CommandText = s1;
                                    command.ExecuteNonQuery();
                                    command.CommandText = s2;
                                    command.ExecuteNonQuery();
                                    st.status = "Assigned";
                                    i++;
                                    p.allocations = i;


                                }
                            }
                        }
                    }
                }
            }
                catch(Exception e)
                {
                    MessageBox.Show("Error:"+e);
                }
                finally
                {
                    MessageBox.Show("Data is up to date!");
                }
            myConnection.Close();
        }


        public void manual()
        {
            if (dataGridView1.SelectedRows != null && dataGridView2.SelectedRows!=null)
            {

                myConnection.Open();
                MySqlCommand command = new MySqlCommand();
                command = myConnection.CreateCommand();
                try
                {
                    Console.WriteLine(PStatus);
                    if (((PStatus.Equals("Partial") && alloc < 4) || (PStatus.Equals("Full") && alloc < 8)) && SStatus.Equals("Not Assigned"))
                    {

                        String s = "insert into allocations values(" + PID + "," + UOB1 + ")";
                        command.CommandText = s;
                        command.ExecuteNonQuery();
                        String s1 = "Update students set status ='Assigned' where uob=" + UOB1;
                        command.CommandText = s1;
                        command.ExecuteNonQuery();
                        String s2 = "Update pat set Allocations = " + (alloc + 1) + " where ID = " + PID;
                        command.CommandText = s2;
                        command.ExecuteNonQuery();
                        myConnection.Close();
                        connect();


                    }
                    else if (((PStatus.Equals("Partial") && alloc < 4) || (PStatus.Equals("Full") && alloc < 8)) && SStatus.Equals("Assigned"))
                    {
                        var result = MessageBox.Show("Are you sure you want re-assign this student","Confirm Yes",MessageBoxButtons.YesNo);
                        if(result == DialogResult.Yes)
                        {
                            try {
                                command.CommandText = "select PAT_ID from allocations where UOB='" + UOB1 + "'";
                                int oid = Convert.ToInt32(command.ExecuteScalar().ToString());

                                command.CommandText = "select Name from pat where ID='" + oid + "'";
                                String opat = command.ExecuteScalar().ToString();

                                command.CommandText = "select Name from students where UOB='" + UOB1 + "'";
                                String student = command.ExecuteScalar().ToString();

                                command.CommandText = "select Name from pat where ID='" + PID + "'";
                                String npat = command.ExecuteScalar().ToString();

                                String sms = "Request has been recieved to change the PAT of "+student +" UOB: "+UOB1+" to "+npat+" from "+opat;

                                MailClass mail = new MailClass("rahman2013@namal.edu.pk",sms);

                                command.CommandText = "insert into request values('" + UOB1 + "','" + student + "','" + PID + "','" + npat + "','" + oid + "','" + opat + "')";
                                command.ExecuteNonQuery();

                            }
                            catch(Exception a)
                            {
                                MessageBox.Show("Error:"+a);
                            }

                        }

                    }
                    else if (((PStatus.Equals("Partial") && alloc == 4) || (PStatus.Equals("Full") && alloc == 8)) && SStatus.Equals("Assigned"))
                    {
                        MessageBox.Show("You can't assign Student to this PAT, because allocations to this PAT is already complete.");
                    }
                    else
                    {
                        MessageBox.Show("select student and pat to assign");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("select student and pat to assign");
                }
                myConnection.Close();
            }
            else
            {
                MessageBox.Show("select student and pat to assign");
            }
        }
     

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            auto();
            connect();
            refresh1();

        }


        public void refresh1()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            int count = 0;

            foreach (PAT pt in pat_list)
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
                    else if ((pt.status.Equals("Full") && pt.allocations < 8) || (pt.status.Equals("Partial") && pt.allocations < 4))
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[count].Cells[0].Value = pt.ID;
                        dataGridView2.Rows[count].Cells[1].Value = pt.name;
                        dataGridView2.Rows[count].Cells[2].Value = pt.status;
                        dataGridView2.Rows[count].Cells[3].Value = pt.allocations;
                        count++;
                    }
            }
            count = 0;
            foreach (Student1 st in st_list)
            {
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

        private void Allocations_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.PaleGreen;
            addData();
            connect();
            refresh1();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            manual();
            connect();
            refresh1();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            refresh1();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            refresh1();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {

                int r = e.RowIndex;
                UOB1 = Convert.ToInt32(dataGridView1.Rows[r].Cells[0].Value);
                SStatus = dataGridView1.Rows[r].Cells[2].Value.ToString();
                Console.WriteLine("hello" + SStatus);
            }
            catch(Exception a)
            {
                
            }
        }
        
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                int r = e.RowIndex;
                PID = Convert.ToInt32(dataGridView2.Rows[r].Cells[0].Value);
                PStatus = dataGridView2.Rows[r].Cells[2].Value.ToString();
                alloc = Convert.ToInt32(dataGridView2.Rows[r].Cells[3].Value.ToString());

                Console.WriteLine("Hello" + PStatus);
            }
            catch
            {

            }
        }

        public void addData()
        {
            try
            {
                MySqlDataAdapter dataadapter = new MySqlDataAdapter("select * from request", myConnection);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataadapter.Fill(table);
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = table;
                dataGridView3.DataSource = bindingSource1;
            }
            catch (Exception a)
            {
                MessageBox.Show("Something is Wrong" + a.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                int r = e.RowIndex;
                UOB1 = Convert.ToInt32(dataGridView1.Rows[r].Cells[0].Value);
                SStatus = dataGridView1.Rows[r].Cells[2].Value.ToString();
                Console.WriteLine("hello" + SStatus);
            }
            catch
            {

            }
        }
    }


    public class Student1
    {

        public int UOB { get; set; }
        public String status { get; set; }
        public String name { get; set; }
   

    }
    public class PAT
    {
        public int ID { get; set; }

        public String name { get; set; }

        public int allocations { get; set; }

        public String status { get; set; }

    }
}

