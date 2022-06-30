using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using POE_Final_GUI.Expenses;

namespace POE_Final_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //--------------------------------------------------------------------
        List<Expense> currentExpenses = new List<Expense>();
        string[] expenseNames = { "Estimated Monthly Tax Deducted", "Groceries", "Water and Lights", "Travel cost (Including petrol)", "Cellphone and Telephone", "Other expenses" };
        double grossIn = 0;
        //--------------------------------------------------------------------

        public MainWindow()
        {
            InitializeComponent();
            //Custom setup for gui
            buyCarGrid.Visibility = Visibility.Hidden;
            this.Title = "Monthly Budget Calculator";
        }

        //----------Validation----------------------------------------------------------------------

        //Runs all validations in order and returns true if all are valid
        bool allValid()
        {
            bool valid = true;
            //Validating buttons
            if (!validateButtons()) { valid = false; }
            //Validating basics
            if (!validateBasics()) { valid = false; }
            //Validating housing
            if (!validateHousing()) { valid = false; }
            //Validating car
            if (!validateCar()) { valid = false; }
            return valid;
        }


        //Validates that the user has made valid selections and the app can continue
        private bool validateButtons() 
        {
            //If the user has not selected buy or rent
            if (rbtnBuy.IsChecked == false && rbtnRent.IsChecked == false) { MessageBox.Show("Please select either Buying or Renting"); return false; }
            return true;
        }

        //Validates the types for all values entered for income, tax and basic expenses like groceries
        private bool validateBasics()
        {
            //Validation for just for the gross in
            try { double z = double.Parse(txtbxGrossIn.Text); txtbxGrossIn.Background = Brushes.White; }
            catch (Exception e) { Console.WriteLine(e); txtbxGrossIn.Text = "Enter a valid value"; txtbxGrossIn.Background = Brushes.Red; return false; }

            //Different textboxes are grouped into grids depending on their function, we then iterate through the children of these grids for validation
            //Validation for basic expenses such as 
            foreach (TextBox t in basicExpensestxtbxgrp.Children)
            {
                try { double x = double.Parse(t.Text); t.Background = Brushes.White; }
                catch (FormatException format) { Console.WriteLine(format); t.Text = "Enter a valid value"; t.Background = Brushes.Red; return false; }
            }
            return true;
        }
        //Validates user input for housing info
        private bool validateHousing() 
        {
            if (rbtnBuy.IsChecked == true)
            {
                foreach (TextBox t in homeloanGrid.Children)
                {
                    try { double x = double.Parse(t.Text); t.Background = Brushes.White; }
                    catch (FormatException format) { Console.WriteLine(format); t.Text = "Enter a valid value"; t.Background = Brushes.Red; return false; }
                }
                return true;
            }
            //If not buying must be renting as we have validated that at least one button is checked
            else 
            {
                try { double z = double.Parse(txtbxRent.Text); txtbxRent.Background = Brushes.White; }
                catch (Exception e) { Console.WriteLine(e); txtbxRent.Text = "Enter a valid value"; txtbxRent.Background = Brushes.Red; return false; }
                return true;
            }
        }
        //Validates user input for car
        private bool validateCar() 
        {
            if (chkbxBuyCar.IsChecked==true) {
                foreach (TextBox t in buyCarGrid.Children.OfType<TextBox>())
                {
                    try { double x = double.Parse(t.Text); t.Background = Brushes.White; }
                    catch (FormatException format) { Console.WriteLine(format); t.Text = "Enter a valid value"; t.Background = Brushes.Red; return false; }
                }
                return true;
            }
            return true;
        }



        //--------------------------------------------------------------------------------------

        //Returns a List of expense objects relating to the basic expenses the user entered
        private List<Expense> GetBasics() 
        {
            ArrayList values = new ArrayList();
            foreach (TextBox t in basicExpensestxtbxgrp.Children) {values.Add(t.Text);}
            List<Expense> returnList = new List<Expense>();
            //Creates a new expense object and adds it to the expense list for each basic expense
            for (int i = 0; i < expenseNames.Length; i++) 
            {
                //We only call this method after validating that all data in basicExpensestxtbxgrp is of type double
                returnList.Add(new Expense(expenseNames[i], Convert.ToDouble(values[i])));
            }
            return returnList;
        }

        //Returns a homeloan expense from user data
        private Expense getHousing() 
        {
            if (rbtnBuy.IsChecked == true)
            {
                return new HomeLoan(
                        Convert.ToDouble(txtbxPurchasePrice.Text),
                        Convert.ToDouble(txtbxDeposit.Text),
                        Convert.ToDouble(txtbxInterestRate.Text),
                        Convert.ToDouble(txtbxMonths.Text)
                    );
            }
            else { return new Expense("Monthly Rental Cost", Convert.ToDouble(txtbxRent.Text)); }
        }

        //Returns an expense list for car
        private Car getCar() 
        {
            
        }

        //--------------------------------------------------------------------------------------

        private void txtbxGrossIn_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtbxOther_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void rbtnRent_Checked(object sender, RoutedEventArgs e)
        {
            txtbxRent.IsEnabled = true;
            foreach (TextBox t in homeloanGrid.Children) { t.IsEnabled = false; }
        }

        //Shows the rental relevant textboxes and hides the home loan relevant ones
        private void rbtnBuy_Checked(object sender, RoutedEventArgs e)
        {
            txtbxRent.IsEnabled = false;
            foreach (TextBox t in homeloanGrid.Children) { t.IsEnabled = true; }
        }

        private void chkbxBuyCar_Checked(object sender, RoutedEventArgs e)
        {
            if (chkbxBuyCar.IsChecked == false) { buyCarGrid.Visibility = Visibility.Hidden; }
            else { buyCarGrid.Visibility = Visibility.Visible; }
        }

        //The 'Next' button on the first page of the app
        //This method is responsible for 
        private void btnFeNextClick(object sender, RoutedEventArgs e)
        {
            //If all user given input is valid
            if (allValid())
            {
                //Sets the gross input
                grossIn = Convert.ToDouble(txtbxGrossIn.Text);
                currentExpenses.AddRange(GetBasics());
                currentExpenses.Add(getHousing());
                //Only add the car expense if the user has chosen to
                if (chkbxBuyCar.IsChecked==true) { currentExpenses.Add(getCar()); }
                Tabs.SelectedIndex++;
            }
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSeBack_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex--;
        }

        private void btnSeNex_Click(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedIndex++;
        }

        private void btnSeBack_Click_1(object sender, RoutedEventArgs e)
        {
            currentExpenses.Clear();
            Tabs.SelectedIndex++;
        }
    }
}
