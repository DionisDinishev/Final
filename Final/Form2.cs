using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public partial class Form2 : Form
    {
        public bool exit;
        public int speed;
        public String gameType;
        public DialogResult d;
        public Form2()
        {
            InitializeComponent();
            d = DialogResult.Abort;
            exit = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex==0)
            {
                speed = 75;
            }
            else if(comboBox2.SelectedIndex==1)
            {
                speed = 50;
            }
            else
            {
                speed = 25;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameType = comboBox1.SelectedItem as String;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            exit = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            exit = true;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(exit)
            Application.Exit();
        }
    }
}
