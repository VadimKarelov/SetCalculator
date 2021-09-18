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

        private Random rn = new Random();

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
            if (sender is RadioButton rb && rb.Checked)
            {
                int ind = int.Parse(rb.Parent.Tag.ToString());
                try
                {
                    // checking
                    rb.BackColor = Color.White;
                    int size = int.Parse(GetSize(ind));
                    if (size < 0 || size > (Math.Abs(Set.UniversumMax) + Math.Abs(Set.UniversumMin) + 1))
                    { throw new Exception(); }
                    // generate
                    Set newSet = AutoSet(size);
                    SetSet(ind, newSet);
                    SetSetToTextBox(ind, newSet);
                }
                catch
                {
                    rb.BackColor = Color.Red;
                }
            }
        }

        private Set AutoSet(int n)
        {
            // to optimaze generation process
            List<int> availableElements = new List<int>();
            Set res = new Set();
            for (int i = Set.UniversumMin; i <= Set.UniversumMax; i++)
            {
                availableElements.Add(i);
            }
            // generation
            while (res.Collection.Count < n)
            {
                int ind = rn.Next(availableElements.Count);
                res.Add(availableElements[ind]);
                availableElements.RemoveAt(ind);
            }
            return res;
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

        private void SetSetToTextBox(int n, Set set)
        {
            // generate string
            string str = "";
            for (int i = 0; i < set.Collection.Count; i++)
            {
                str += set.Collection[i] + " ";
            }
            str = str.Remove(str.Length - 1);
            // set
            switch (n)
            {
                case 1: textBox_Set1.Text = str; break;
                case 2: textBox_Set2.Text = str; break;
                case 3: textBox_Set3.Text = str; break;
            }
        }

        private string GetSize(int numberOfTextBox)
        {
            switch (numberOfTextBox)
            {
                case 1: return textBox_Size1.Text;
                case 2: return textBox_Size2.Text;
                case 3: return textBox_Size3.Text;
                default: return "0";
            }
        }
    }
}
