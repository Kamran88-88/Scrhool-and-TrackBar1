using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            button1.BackColor = Color.FromArgb(hScrollBar1.Value,hScrollBar2.Value,hScrollBar3.Value);
            label1.Text = hScrollBar1.Value.ToString();
            label2.Text = hScrollBar2.Value.ToString();
            label3.Text = hScrollBar3.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "0";
            label2.Text = "0";
            label3.Text = "0";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar1.Value.ToString();
        }

        //private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        //{
        //    button1.BackColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
        //    
        //}

        //private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        //{
        //    button1.BackColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
        //   
        //}
    }
}
