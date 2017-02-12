/*
App Name          :   Sharp Auto Form
Author's Name     :   Rutul Shah
Student ID        :   #200329341
App Creation Date :   02/12/2017 
App Description   :   This application is used to calculate the amount based on the base price of the product. It even calculates the taxes involved and if the used wants a pay-back for his old car
                      It even helps to determine cost if we want to include different features in the car.
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpAutoForm
{
    public partial class AutoCenterForm : Form
    {
        //Initializing Variables
        private decimal _temporaryStoredPrice;
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

        
        /// <summary>
        /// _button_click method is customized method which is initialized if any button is clicked.
        /// It then figures out what kind of button is clicked and perform tasks accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _button_click(object sender, EventArgs e)
        {
            Button AutoCenterButton = (Button)sender;

            if (AutoCenterButton.Text == "Calculate")
            {
                //if the text on button is "Calculate", then call _calculate method
                _calculate();
            }
            else if (AutoCenterButton.Text == "Clear")
            {
                //if the text on button is "Clear", then call _clear method
                _clear();
            }
            else
            {
                // if none of the above text matches, then call _confirmExit method
                this.Close();
            }
        }

        /// <summary>
        /// this method is used to change fonts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _changeFont();
        }

        /// <summary>
        /// this method is used to change font of Base Price and Amount Due.
        /// this method is created so that if in future if we want to add some additional Textbox, to change their font, we can !
        /// </summary>
        private void _changeFont()
        {
            if (fontDialogBox.ShowDialog() == DialogResult.OK)
            {
                BasePriceTextBox.Font = fontDialogBox.Font;
                AmountDueTextBox.Font = fontDialogBox.Font;
            }
        }

        /// <summary>
        /// this method is used to change Background color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _changeColor();
        }

        /// <summary>
        /// this method is used to change color of Base Price and Amount Due.
        /// this method is created so that if in future if we want to add some additional Textbox, to change their color, we can !
        /// </summary>
        private void _changeColor()
        {
            if (colorDialogBox.ShowDialog() == DialogResult.OK)
            {
                BasePriceTextBox.BackColor = colorDialogBox.Color;
                AmountDueTextBox.BackColor = colorDialogBox.Color;
            }
        }

        /// <summary>
        /// this method creates the instance of "AboutSharpAutoForm" page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //intialize about form and show dialog
            AboutSharpAutoForm aboutForm = new AboutSharpAutoForm();
            aboutForm.ShowDialog();
        }

        /// <summary>
        /// this is customized method to determine if any of the checkboxes are checked or unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkBox_CheckedChanged(object sender, EventArgs e)
        {
            //casting the received object as checkbox
            CheckBox checkbox = (CheckBox)sender;

            if (checkbox.Checked)
            {
                switch (checkbox.Text)
                {
                    case "Stereo System":
                        _temporaryStoredPrice += _priceForStereoSystem;
                        break;

                    case "Leather Interior":
                        _temporaryStoredPrice += _priceForLeatherInterior;
                        break;

                    case "Computer Navigation":
                        _temporaryStoredPrice += _priceForComputerNavigation;
                        break;
                }
            }

            else
            {
                switch (checkbox.Text)
                {
                    case "Stereo System":
                        _temporaryStoredPrice -= _priceForStereoSystem;
                        break;

                    case "Leather Interior":
                        _temporaryStoredPrice -= _priceForLeatherInterior;
                        break;

                    case "Computer Navigation":
                        _temporaryStoredPrice -= _priceForComputerNavigation;
                        break;

                }
            }
            //the value in _temporaryStoredPrice needs to be displayed on Additional Options' textbox
            AdditionalOptionsTextBox.Text = _temporaryStoredPrice.ToString("C", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// this customized method is used to determine which radio button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _radio_CheckedChanged(object sender, EventArgs e)
        {
            //casting the received object as radiobutton
            RadioButton radio = (RadioButton)sender;

            if (radio.Checked)
            {
                switch (radio.Text)
                {
                    case "Pearlized":
                        _temporaryStoredPrice += _priceForPearlized;
                        break;

                    case "Customized Detailling":
                        _temporaryStoredPrice += _priceForCustomizedDetailling;
                        break;
                }
            }
            else
            {
                switch (radio.Text)
                {
                    case "Pearlized":
                        _temporaryStoredPrice -= _priceForPearlized;
                        break;

                    case "Customized Detailling":
                        _temporaryStoredPrice -= _priceForCustomizedDetailling;
                        break;
                }
            }
            //the value in _temporaryStoredPrice needs to be displayed on Additional Options' textbox
            AdditionalOptionsTextBox.Text = _temporaryStoredPrice.ToString("C", CultureInfo.CurrentCulture);
        }
        private void _cleartoolstripmenuitem_click(object sender, EventArgs e)
        {
            _clear();
        }


        /// <summary>
        /// this customized method ensures that it clears the whole form when clear button is pressed and bring the form to its original state.
        /// </summary>
        private void _clear()
        {
            //checking if any of the checkbox is checked, if so then uncheck
            foreach (Control control in AdditionalItemsGroupBox.Controls)
            {
                if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                }
            }

            //checking if anything is stored in any of the textboxes, if so clear it 
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();

                }

                //unsure that Trade-In Allowance textbox's value is set to zero
                TradeInAllowanceTextBox.Text = 0.ToString();
                StandardRadioButton.Checked = true;
            }


        }

        /// <summary>
        /// this method is used to exit application from toolstrip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// this method is used to exit application by pressing button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// this method ensures that user has not pressed or click exit button by co-incidence 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _confirmExit(object sender, FormClosingEventArgs e)
        {
            //adding a message box so that it doesn't close the application in case used accidentally presses exit button/ clicks exit
            var value = MessageBox.Show("Do you really want to exit ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (value == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// this method is used for validations
        /// </summary>
        /// <returns></returns>
        private bool _isvalid()
        {
            //initializing
            bool booleanValue = false;

            //checking if baseprice is not null 
            if (string.IsNullOrEmpty(BasePriceTextBox.Text))
            {
                MessageBox.Show("BASE PRICE cannot be null", "Empty Field Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return booleanValue;
            }

            //if calculate button is pressed for the second time, it should avoid comma and dollar sign and should convert back from string to decimal and do the calculation
            string basePriceText = (BasePriceTextBox.Text).Replace("$", "");
            basePriceText = (basePriceText).Replace(",", "");

            string tradeInAllowanceText = (TradeInAllowanceTextBox.Text).Replace("$", "");
            tradeInAllowanceText = (tradeInAllowanceText).Replace(",", "");

            //checking for decimal value
            if (!decimal.TryParse(basePriceText, out _basePrice) || !decimal.TryParse(tradeInAllowanceText, out _tradeIn))
            {
                MessageBox.Show("the data you have entered needs to be a number", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return booleanValue;
            }

            //checking if values are positive
            else if (_basePrice < 0 || _tradeIn < 0)
            {
                MessageBox.Show("Neither of the values can be negative", "Negative Value detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return booleanValue;
            }

            //checking if base price is greater than trade in value
            else if (_basePrice < _tradeIn)
            {
                MessageBox.Show("Trade in Value cannot be more than base price", "Purchase cannot be fulfilled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return booleanValue;
            }
            return booleanValue = true;
        }

        /// <summary>
        /// this method is used to calculate the amount, based on the Base Price and Trade-In Allowance when user selects Calculate button
        /// </summary>
        private void _calculate()
        {
            if (_isvalid())
            {

                //taking value from variables that are already calculated and giving it back to textboxes

                BasePriceTextBox.Text = _basePrice.ToString("C", CultureInfo.CurrentCulture);

                decimal subTotal = _temporaryStoredPrice + _basePrice;
                SubTotalTextBox.Text = subTotal.ToString("C", CultureInfo.CurrentCulture);

                decimal salesTax = subTotal * decimal.Parse("0.13");
                SalesTaxTextBox.Text = salesTax.ToString("C", CultureInfo.CurrentCulture);

                decimal total = subTotal + salesTax;
                TotalTextBox.Text = total.ToString("C", CultureInfo.CurrentCulture);
                
                TradeInAllowanceTextBox.Text = _tradeIn.ToString("C", CultureInfo.CurrentCulture);

                decimal amount = total - _tradeIn;
                AmountDueTextBox.Text = amount.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// this method is used to calculate the amount, based on the Base Price and Trade-In Allowance when user selects Calculate from toolstrip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _calculate();
        }
    }
}
