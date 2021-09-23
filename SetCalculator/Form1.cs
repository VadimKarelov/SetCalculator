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
        // storage
        private Set set1 = new Set();
        private Set set2 = new Set();
        private Set set3 = new Set();
        private Set set4 = new Set();
        private Set resultSet = new Set();

        // active elements
        private Set firstOperand = new Set();
        private Set secondOperand = new Set();
        private string operation = "";

        private Random rn = new Random();

        public Form1()
        {
            InitializeComponent();
            SetUiInitState();
        }

        // === init methods ===
        private void SetUiInitState()
        {
            SetActiveSet();

            radioButton_Manual1.Checked = true;
            radioButton_Manual2.Checked = true;
            radioButton_Manual3.Checked = true;

            radioButton_Set11.Checked = true;
            radioButton_Set21.Checked = true;

            radioButton_Addition.Checked = true;            
        }

        // === inpu data about sets ===
        private void TextBoxSet_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                List<int> res = new List<int>();
                int ind = int.Parse(tb.Tag.ToString());
                try
                {
                    res = tb.Text.Split(' ').ToList().Select(int.Parse).ToList();
                    Set newSet = new Set(res);
                    if (!CheckSetForUniversum(newSet)) throw new Exception();
                    SetColorToTextBox(ref tb, Color.White);
                    SetSet(ind, newSet);
                    SetActiveSet();
                }
                catch
                {
                    SetColorToTextBox(ref tb, Color.Red);
                }                
            }
        }

        private bool CheckSetForUniversum(Set set)
        {
            foreach (var elem in set.Collection)
            {
                if (elem < Set.UniversumMin || elem > Set.UniversumMax)
                    return false;
            }
            return true;
        }

        private void TextBoxMultiplicity_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    int res = int.Parse(tb.Text);
                    SetColorToTextBox(ref tb, Color.White);
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

        private void Universum_Checked(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                int ind = int.Parse(rb.Parent.Tag.ToString());
                Set newSet = new Set();

                for (int i = Set.UniversumMin; i <= Set.UniversumMax; i++)
                {
                    newSet.Add(i);
                }

                SetSet(ind, newSet);
                SetSetToTextBox(ind, newSet);
            }
        }

        private void Auto_Checked(object sender, EventArgs e)
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

        private void Multiplicity_Checked(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                int ind = int.Parse(rb.Parent.Tag.ToString());
                try
                {
                    // checking
                    rb.BackColor = Color.White;
                    int multiplicity = int.Parse(GetMultiplicity(ind));
                    // generate
                    Set newSet = new Set();
                    for (int i = Set.UniversumMin; i <= Set.UniversumMax; i++)
                    {
                        if (i % multiplicity == 0)
                            newSet.Add(i);
                    }
                    SetSet(ind, newSet);
                    SetSetToTextBox(ind, newSet);
                }
                catch
                {
                    rb.BackColor = Color.Red;
                }
            }
        }

        private void Positive_Checked(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                int ind = int.Parse(rb.Parent.Tag.ToString());
                Set newSet = new Set();
                for (int i = 0; i <= Set.UniversumMax; i++)
                {
                    newSet.Add(i);
                }
                SetSet(ind, newSet);
                SetSetToTextBox(ind, newSet);
            }
        }

        private void Negative_Checked(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                int ind = int.Parse(rb.Parent.Tag.ToString());
                Set newSet = new Set();
                for (int i = Set.UniversumMin; i < 0; i++)
                {
                    newSet.Add(i);
                }
                SetSet(ind, newSet);
                SetSetToTextBox(ind, newSet);
            }
        }

        private void Odd_Checked(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                int ind = int.Parse(rb.Parent.Tag.ToString());
                Set newSet = new Set();
                for (int i = Set.UniversumMin; i <= Set.UniversumMax; i++)
                {
                    if (i % 2 != 0)
                        newSet.Add(i);
                }
                SetSet(ind, newSet);
                SetSetToTextBox(ind, newSet);
            }
        }

        private void Even_Checked(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                int ind = int.Parse(rb.Parent.Tag.ToString());
                Set newSet = new Set();
                for (int i = Set.UniversumMin; i <= Set.UniversumMax; i++)
                {
                    if (i % 2 == 0)
                        newSet.Add(i);
                }
                SetSet(ind, newSet);
                SetSetToTextBox(ind, newSet);
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

        // === extra methods ===
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
            string str = set.ToString();
            // seting
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

        private string GetMultiplicity(int numberOfTextBox)
        {
            switch (numberOfTextBox)
            {
                case 1: return textBox_Multiplicity1.Text;
                case 2: return textBox_Multiplicity2.Text;
                case 3: return textBox_Multiplicity3.Text;
                default: return "0";
            }
        }

        private void SetActiveSet()
        {
            FirstOperand_Changed(radioButton_Set11, new EventArgs());
            FirstOperand_Changed(radioButton_Set12, new EventArgs());
            FirstOperand_Changed(radioButton_Set13, new EventArgs());
            FirstOperand_Changed(radioButton_Set14, new EventArgs());

            SecondOperand_Changed(radioButton_Set21, new EventArgs());
            SecondOperand_Changed(radioButton_Set22, new EventArgs());
            SecondOperand_Changed(radioButton_Set23, new EventArgs());
            SecondOperand_Changed(radioButton_Set24, new EventArgs());
        }

        // === operation ===
        private void FirstOperand_Changed(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                switch (rb.Tag.ToString())
                {
                    case "1": firstOperand = set1; break;
                    case "2": firstOperand = set2; break;
                    case "3": firstOperand = set3; break;
                    case "4": firstOperand = set4; break;
                }
                Calculation();
            }
        }

        private void SecondOperand_Changed(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                switch (rb.Tag.ToString())
                {
                    case "1": secondOperand = set1; break;
                    case "2": secondOperand = set2; break;
                    case "3": secondOperand = set3; break;
                    case "4": secondOperand = set4; break;
                }
                Calculation();
            }
        }

        private void Operation_Changed(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                operation = rb.Tag.ToString();
                Calculation();
            }
        }

        private void Calculation()
        {
            switch (operation)
            {
                case "addition": resultSet = Set.Addition(firstOperand); break;
                case "merge": resultSet = Set.Merge(firstOperand, secondOperand); break;
                case "cross": resultSet = Set.Crossing(firstOperand, secondOperand); break;
                case "diff": resultSet = Set.Difference(firstOperand, secondOperand); break;
                case "symDiff": resultSet = Set.SymetricDifference(firstOperand, secondOperand); break;
            }
            textBox_Result.Text = resultSet.ToString();
        }

        // === saving ===
        private void Save_Click(object sender, EventArgs e)
        {
            if (sender is Button bt)
            {
                textBox_SavedSet.Text = textBox_Result.Text;
                set4 = resultSet;
            }
        }

        private void SetFromSave_Click(object sender, EventArgs e)
        {
            if (sender is Button bt)
            {
                int destination = int.Parse(bt.Tag.ToString());
                SetSet(destination, set4);
                SetSetToTextBox(destination, set4);
                SetActiveSet();
            }
        }
    }
}
