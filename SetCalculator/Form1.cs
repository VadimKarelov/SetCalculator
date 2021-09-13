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
        public Form1()
        {
            InitializeComponent();
            SetInitState();
        }

        // === init methods ===
        private void SetInitState()
        {
            radioButton_Manual1.Checked = true;
            radioButton_Manual2.Checked = true;
            radioButton_Manual3.Checked = true;

            // radiobutton operands and operation
        }
    }
}
