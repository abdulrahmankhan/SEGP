using System;
using System.Windows.Forms;

namespace SEGP
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
 
        private void button2_Click(object sender, EventArgs e)
        {
            allocations1.connect();
            Controls.Remove(allocations1);
            Controls.Remove(teachers1);
            Controls.Remove(home1);
            Controls.Remove(add_New1);
            new Student().reload();
            Controls.Add(student1);
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Controls.Remove(student1);
            Controls.Remove(home1);
            Controls.Remove(teachers1);
            Controls.Remove(add_New1);
            Controls.Add(allocations1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            allocations1.connect();
            Controls.Remove(student1);
            Controls.Remove(allocations1);
            Controls.Remove(home1);
            Controls.Remove(add_New1);
            new Teachers().reload();
            Controls.Add(teachers1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allocations1.connect();
            Controls.Remove(student1);
            Controls.Remove(allocations1);
            Controls.Remove(teachers1);
            Controls.Remove(add_New1);
            Controls.Add(home1);
            home1.SetBounds(37, 58, 1001, 489);
        }

        private void Main_Load_1(object sender, EventArgs e)
        {
            home1.SetBounds(37, 58, 1001, 489);
            Controls.Remove(student1);
            Controls.Remove(allocations1);
            Controls.Remove(teachers1);
            Controls.Add(home1);
            Controls.Remove(add_New1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Controls.Remove(student1);
            Controls.Remove(allocations1);
            Controls.Remove(teachers1);
            Controls.Remove(home1);
            Controls.Add(add_New1);

        }

        private void add_New1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Login().Show();
        }
    }
}
