using System;
using System.Drawing;
using System.Windows.Forms;

namespace ALHMI
{

    public partial class Form1 : Form
    {
        private static int FNumber , modeNummer , bytesNummer;
        private bool FNValid = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //this.textBox1.DeselectAll();

            //private int modeNummer;
            
           if (!int.TryParse(textBox1.Text, out FNumber) && textBox1.Text != "")
            {
                fehlerAus("ist keine Nummmer !!");
            } else if (textBox1.TextLength == 6)
            {
                if (FNumber >= 700000)
                {
                    if (FNumber <= 703000)
                    {
                        textBox2.ReadOnly = true;
                        FNumber = FNumber - 700000;
                        modeNummer = FNumber % 8;
                        bytesNummer = (FNumber - modeNummer) / 8;
                        textBox2.Text = "DB126.DBX" + bytesNummer.ToString() + '.' + modeNummer.ToString();
                        FNValid = true;
                        textBox2.ForeColor = Color.Green;
                    } else
                    {
                        fehlerAus("Nummer > 703000");
                    }
                } else
                {
                    fehlerAus("Nummer < 700000");
                }

            } else
            {
                textBox2.ReadOnly = true;
                textBox2.Text = null;
                FNValid = false;
                textBox2.ForeColor = Color.Black;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = HMI.DefaultNr;
            textBox1.SelectionStart = 3;
            textBox1.SelectionLength = 0;
        }



        private void checkValue(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter && FNValid)
            {
                Clipboard.SetText(textBox2.Text);
            }
            else if (e.KeyChar >='0' && e.KeyChar <= '9' || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            } else //if (e.KeyChar != (char)Keys.ControlKey)
            {
                e.Handled = true;
            }
        }

        private void fehlerAus (string FText)
        {
            textBox2.ReadOnly = false;
            FNValid = false;
            textBox2.Text = FText;
            textBox2.ForeColor = Color.Red;            
        }
    }
}
