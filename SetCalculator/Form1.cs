using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetCalculator
{
    public partial class Form1 : Form
    {
        private Set set1 = new Set();
        private Set set2 = new Set();
        private Set set3 = new Set();
        private Set set4 = new Set();

        private int size1 = 0;
        private int size2 = 0;
        private int size3 = 0;
        private int size4 = 0;

        public Form1()
        {
            InitializeComponent();
            SetUiInitState();
        }

        // === init methods ===
        private void SetUiInitState()
        {
            radioButton_Manual1.Checked = true;
            radioButton_Manual2.Checked = true;
            radioButton_Manual3.Checked = true;

            radioButton_Set11.Checked = true;
            radioButton_Set21.Checked = true;

            radioButton_Addition.Checked = true;
        }

        // === check text in text box ===
        private void TextBoxSet_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                List<int> res = new List<int>();
                int ind = int.Parse(tb.Tag.ToString());
                try
                {
                    res = tb.Text.Split(' ').ToList().Select(int.Parse).ToList();
                    SetColorToTextBox(ref tb, Color.White);
                    SetSet(ind, new Set(res));
                }
                catch
                {
                    SetColorToTextBox(ref tb, Color.Red);
                }                
            }
        }

        private void CheckSizeOfSet(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    int res = int.Parse(tb.Text);
                    if (res >= 0 && res <= 41)
                        SetColorToTextBox(ref tb, Color.White);
                    else
                        throw new Exception();
                }
                catch
                {
                    SetColorToTextBox(ref tb, Color.Red);
                }
            }
        }

        private void Auto_Enabled(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Enabled)
            {
                int ind = int.Parse(rb.Parent.Tag.ToString());
                try
                {
                    //int size = int.Parse()
                }
                catch
                {

                }
            }
        }

        // === use correct element ===
        private void SetColorToTextBox(ref TextBox tb, Color color)
        {
            tb.BackColor = color;
        }

        private void SetSet(int n, Set set)
        {
            switch (n)
            {
                case 1: set1 = set; break;
                case 2: set2 = set; break;
                case 3: set3 = set; break;
            }
        }
    }
}
