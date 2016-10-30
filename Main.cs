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

            this.Controls.Remove(allocations1);
            this.Controls.Remove(teachers1);
            this.Controls.Remove(home1);
            this.Controls.Remove(add_New1);
            this.Controls.Add(student1);
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(student1);
            this.Controls.Remove(home1);
            this.Controls.Remove(teachers1);
            this.Controls.Remove(add_New1);
            this.Controls.Add(allocations1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(student1);
            this.Controls.Remove(allocations1);
            this.Controls.Remove(home1);
            this.Controls.Remove(add_New1);
            this.Controls.Add(teachers1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(student1);
            this.Controls.Remove(allocations1);
            this.Controls.Remove(teachers1);
            this.Controls.Remove(add_New1);
            this.Controls.Add(home1);
            home1.SetBounds(37, 58, 1001, 489);
        }

        private void Main_Load_1(object sender, EventArgs e)
        {
            home1.SetBounds(37, 58, 1001, 489);
            this.Controls.Remove(student1);
            this.Controls.Remove(allocations1);
            this.Controls.Remove(teachers1);
            this.Controls.Add(home1);
            this.Controls.Remove(add_New1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(student1);
            this.Controls.Remove(allocations1);
            this.Controls.Remove(teachers1);
            this.Controls.Remove(home1);
            this.Controls.Add(add_New1);

        }

         
    }
}
