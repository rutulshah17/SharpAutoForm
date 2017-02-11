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
        private decimal _temporaryPrice;
        private const decimal _priceForStereoSystem = 425.76M;
        private const decimal _priceForLeatherInterior = 987.41M;
        private const decimal _priceForComputerNavigation = 1741.23M;

        private const decimal _priceForPearlized = 345.72M;
        private const decimal _priceForCustomizedDetailling = 599.99M;

        private decimal _basePrice;
        private decimal _tradeIn;
        public AutoCenterForm()
        {
            InitializeComponent();
        }

        private void button_click(object sender, EventArgs e)
        {
            Button AutoCenterButton = (Button)sender;

            if (AutoCenterButton.Text == "Calculate")
            {
                _calculate();
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
                       _temporaryPrice += _priceForStereoSystem;
                        break;

                    case "Leather Interior":
                        _temporaryPrice += _priceForLeatherInterior;
                        break;

                    case "Computer Navigation":
                        _temporaryPrice += _priceForComputerNavigation;
                        break;
                }
            }

            else
            {
                switch (checkbox.Text)
                {
                    case "Stereo System":
                        _temporaryPrice -= _priceForStereoSystem;
                        break;

                    case "Leather Interior":
                        _temporaryPrice -= _priceForLeatherInterior;
                        break;

                    case "Computer Navigation":
                        _temporaryPrice -= _priceForComputerNavigation;
                        break;

                }
            }
            AdditionalOptionsTextBox.Text = _temporaryPrice.ToString("C", CultureInfo.CurrentCulture);
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            
            RadioButton radio = (RadioButton)sender;
            
            if (radio.Checked)
            {
                switch (radio.Text)
                {
                    case "Pearlized":
                        _temporaryPrice += _priceForPearlized;
                        break;

                    case "Customized Detailling":
                        _temporaryPrice += _priceForCustomizedDetailling;
                        break;
                }
            }
            else
            {
                switch (radio.Text)
                {
                    case "Pearlized":
                        _temporaryPrice -= _priceForPearlized;
                        break;

                    case "Customized Detailling":
                        _temporaryPrice -= _priceForCustomizedDetailling;
                        break;
                }
            }
            AdditionalOptionsTextBox.Text = _temporaryPrice.ToString("C", CultureInfo.CurrentCulture);
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

        private bool _isvalid()
        {
            decimal value;
            bool booleanValue = false;

            if (string.IsNullOrEmpty(BasePriceTextBox.Text) || string.IsNullOrEmpty(TradeInAllowanceTextBox.Text) || TradeInAllowanceTextBox.Text == "0")
            {
                
                MessageBox.Show("None of the required fields can be null", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return booleanValue;
            }

            else if (!decimal.TryParse(BasePriceTextBox.Text, out value) || !decimal.TryParse(TradeInAllowanceTextBox.Text, out value))
            {
                MessageBox.Show("the data you have entered needs to be a number", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return booleanValue;
            }

            else if (decimal.Parse(BasePriceTextBox.Text) < 0 || decimal.Parse(TradeInAllowanceTextBox.Text) < 0)
            {
                MessageBox.Show("Neither of the values can be negative", "Negative Value detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return booleanValue;
            }

            else if (decimal.Parse(TradeInAllowanceTextBox.Text) > decimal.Parse(BasePriceTextBox.Text)) 
            {
                MessageBox.Show("Trade in Value cannot be more than base price","Purchase cannot be fulfilled",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return booleanValue;
            }
            else
            {
                return booleanValue = true;
            }
            
        }

        private void _calculate()
        {
            bool valueFromIsValid = _isvalid();

            if (valueFromIsValid == true)
            {

                _basePrice = decimal.Parse(BasePriceTextBox.Text);
                BasePriceTextBox.Text = _basePrice.ToString("C", CultureInfo.CurrentCulture);

                decimal subTotal = _temporaryPrice + _basePrice;
                SubTotalTextBox.Text = subTotal.ToString("C", CultureInfo.CurrentCulture);

                decimal salesTax = subTotal * decimal.Parse("0.13");
                SalesTaxTextBox.Text = salesTax.ToString("C", CultureInfo.CurrentCulture);

                decimal total = subTotal + salesTax;
                TotalTextBox.Text = total.ToString("C", CultureInfo.CurrentCulture);

                _tradeIn = decimal.Parse(TradeInAllowanceTextBox.Text);
                TradeInAllowanceTextBox.Text = _tradeIn.ToString("C", CultureInfo.CurrentCulture);

                decimal amount = total - _tradeIn;
                AmountDueTextBox.Text = amount.ToString("C", CultureInfo.CurrentCulture);
            }
        }
    }
}
