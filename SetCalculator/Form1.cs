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
                    SetWarningMessage(ind, "OK");
                    SetSet(ind, new Set(res));
                }
                catch
                {
                    SetWarningMessage(ind, "Не удалось получить множество");
                }
            }
        }

        // === use correct element ===
        private void SetWarningMessage(int n, string text)
        {
            switch (n)
            {
                case 1: label_Warning1.Text = text; break;
                case 2: label_Warning2.Text = text; break;
                case 3: label_Warning3.Text = text; break;
            }
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
