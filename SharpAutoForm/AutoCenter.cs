using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpAutoForm
{
    public partial class AutoCenterForm : Form
    {
        decimal price;
        private const decimal _stereoSystem = 425.76M;
        private const decimal _leatherInterior = 987.41M;
        private const decimal _computerNavigation = 1741.23M;

        private const decimal _pearlized = 345.72M;
        private const decimal _customizedDetailling = 599.99M;
        public AutoCenterForm()
        {
            InitializeComponent();
        }

        private bool _validation()
        {
            bool a = false;
            return a;
        }

        private void button_click(object sender, EventArgs e)
        {
            Button AutoCenterButton = (Button)sender;

            if (AutoCenterButton.Text == "Calculate")
            {

            }
            else if (AutoCenterButton.Text == "Clear")
            {
                _clear();
            }
            else
            {
                this.Close();
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _changeFont();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _changeColor();
        }

        private void _changeFont()
        {
            if (fontDialogBox.ShowDialog() == DialogResult.OK)
            {
                BasePriceTextBox.Font = fontDialogBox.Font;
                AmountDueTextBox.Font = fontDialogBox.Font;
            }
        }

        private void _changeColor()
        {
            if (colorDialogBox.ShowDialog() == DialogResult.OK)
            {
                BasePriceTextBox.ForeColor = colorDialogBox.Color;
                AmountDueTextBox.ForeColor = colorDialogBox.Color;
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program calculates the AMOUNT DUE on a NEW or USED VEHICLE.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;

            if (checkbox.Checked)
            {
                switch (checkbox.Text)
                {
                    case "Stereo System":
                        price += _stereoSystem;
                        break;

                    case "Leather Interior":
                        price += _leatherInterior;
                        break;

                    case "Computer Navigation":
                        price += _computerNavigation;
                        break;
                }
                AdditionalOptionsTextBox.Text = price.ToString("C", CultureInfo.CurrentCulture);
            }

            else
            {
                switch (checkbox.Text)
                {
                    case "Stereo System":
                        price -= _stereoSystem;
                        break;

                    case "Leather Interior":
                        price -= _leatherInterior;
                        break;

                    case "Computer Navigation":
                        price -= _computerNavigation;
                        break;

                }
                AdditionalOptionsTextBox.Text = price.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("I will run!!");
            RadioButton radio = (RadioButton)sender;
            //Debug.WriteLine(radio.Text);
            if (radio.Checked)
            {
                switch (radio.Text)
                {
                    case "Pearlized":
                        price += _pearlized;
                        break;

                    case "Customized Detailling":
                        price += _customizedDetailling;
                        break;
                }
                AdditionalOptionsTextBox.Text = price.ToString("C", CultureInfo.CurrentCulture);
            }
            else
            {
                switch (radio.Text)
                {
                    case "Pearlized":
                        price -= _pearlized;
                        break;

                    case "Customized Detailling":
                        price -= _customizedDetailling;
                        break;
                }
                AdditionalOptionsTextBox.Text = price.ToString("C", CultureInfo.CurrentCulture);
            }
        }
        private void cleartoolstripmenuitem_click(object sender, EventArgs e)
        {
            _clear();
        }

        private void _clear()
        {

            foreach (Control control in AdditionalItemsGroupBox.Controls)
            {
                if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                }
            }

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();

                }

                TradeInAllowanceTextBox.Text = 0.ToString();
                StandardRadioButton.Checked = true;
            }


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _confirmExit(object sender, FormClosingEventArgs e)
        {
            var value = MessageBox.Show("Do you really want to exit ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (value == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
