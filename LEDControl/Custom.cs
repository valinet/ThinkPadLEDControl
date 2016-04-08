using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEDControl
{
    public partial class Custom : Form
    {
        public Custom()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
            try
            {
                int decValue = int.Parse(textBox1.Text, System.Globalization.NumberStyles.HexNumber);
                if (decValue == 255) decValue = -1;
                decValue += 1;
                textBox1.Text = decValue.ToString("X");
                if (textBox1.Text.Length == 1) textBox1.Text = "0" + textBox1.Text;
                textBox1.SelectionStart = 2;
            }
            catch { }
        }
    }
}
