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
        Calculation calc = new Calculation();
        //--------------------------------------------------------------------

        public MainWindow()
        {
            InitializeComponent();
            //Custom setup for gui
            buyCarGrid.Visibility = Visibility.Hidden;
            savingsGrid.Visibility = Visibility.Hidden;
            this.Title = "Monthly Budget Calculator";
        }


        //Creating the delegate
        public delegate void exceeds(string msg);

        //Checks if all expenses add up to more than 75% of user net income
        public void ExpenseCheck()
        {
            double total = 0;
            foreach (Expense e in currentExpenses)
            {
                total += e.GetCost();
            }
            exceeds del = (string msg) => MessageBox.Show(msg);
            if (total > 0.75 * grossIn) { del.Invoke("Warning! Your monthly costs exceed 75% of your monthly income!"); }
        }

        //----------Validation----------------------------------------------------------------------

        //Runs all validations in order and returns true if all are valid
        bool allValid()
        {
            bool valid = true;
            //Validating buttons
            if (!ValidateButtons()) { valid = false; }
            //Validating basics
            if (!ValidateBasics()) { valid = false; }
            //Validating housing
            if (!ValidateHousing()) { valid = false; }
            //Validating car
            if (!ValidateCar()) { valid = false; }
            return valid;
        }


        //Validates that the user has made valid selections and the app can continue
        private bool ValidateButtons() 
        {
            //If the user has not selected buy or rent
            if (rbtnBuy.IsChecked == false && rbtnRent.IsChecked == false) { MessageBox.Show("Please select either Buying or Renting"); return false; }
            return true;
        }

        //Validates the types for all values entered for income, tax and basic expenses like groceries
        private bool ValidateBasics()
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
        private bool ValidateHousing()
        {
            if (rbtnBuy.IsChecked == true)
            {
                foreach (TextBox t in homeloanGrid.Children)
                {
                    try { double x = double.Parse(t.Text); t.Background = Brushes.White; }
                    catch (FormatException format) { Console.WriteLine(format); t.Text = "Enter a valid value"; t.Background = Brushes.Red; return false; }
                }
                //By this point we know its a double
                //Checking housing interest rate
                if (Convert.ToDouble(txtbxInterestRate.Text) > 100 || Convert.ToDouble(txtbxInterestRate.Text) < 0)
                { txtbxInterestRate.Text = "Enter a valid value between 0 and 100"; txtbxInterestRate.Background = Brushes.Red; return false; }
                else { txtbxInterestRate.Background = Brushes.White; }
                //Checking housing months
                if (Convert.ToDouble(txtbxMonths.Text) > 360 || Convert.ToDouble(txtbxMonths.Text) < 240)
                { txtbxMonths.Text = "Enter a valid value between 240 and 360"; txtbxMonths.Background = Brushes.Red; return false; }
                else { txtbxMonths.Background = Brushes.White; }
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
        private bool ValidateCar()
        {
            if (chkbxBuyCar.IsChecked == true) {
                foreach (TextBox t in buyCarGrid.Children.OfType<TextBox>())
                {
                    try { double x = double.Parse(t.Text); t.Background = Brushes.White; }
                    catch (FormatException format) { Console.WriteLine(format); t.Text = "Enter a valid value"; t.Background = Brushes.Red; return false; }
                }
                if (Convert.ToDouble(txtbxCarRate.Text) > 100 || Convert.ToDouble(txtbxCarRate.Text) < 0)
                { txtbxCarRate.Text = "Enter a valid value"; txtbxCarRate.Background = Brushes.Red; return false; }
                else { txtbxCarRate.Background = Brushes.White; }
                return true;
            }
            return true;
        }

        //validates the savings info
        private bool ValidateSavings()
        {
            bool valid = true;
            //Savings amount
            double savingsAmt;
            if (double.TryParse(txtbxSaveAmount.Text, out savingsAmt)) { txtbxSaveAmount.Background = Brushes.White; }
            else { txtbxSaveAmount.Text = "Invalid value, please try again"; txtbxSaveAmount.Background = Brushes.Red; return false; }
            //Date
            DateTime date;
            if (DateTime.TryParse(txtbxSavingDate.Text, out date)) { txtbxSavingDate.Background = Brushes.White; }
            else { txtbxSavingDate.Text = "Invalid value, please try again"; txtbxSavingDate.Background = Brushes.Red; return false; }
            //Interest rate
            double rate;
            if (double.TryParse(txtbxSavingRate.Text, out rate) && rate > -1 && rate < 101) { txtbxSavingRate.Background = Brushes.White; }
            else { txtbxSavingRate.Text = "Invalid value, please try again"; txtbxSavingRate.Background = Brushes.Red; return false; }

            return valid;
        }


        //---------Gets info from the GUI and returns objects---------------------------------------------------------

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
        private Expense GetHousing() 
        {
            if (rbtnBuy.IsChecked == true)
            {
                HomeLoan loan = new HomeLoan(
                        Convert.ToDouble(txtbxPurchasePrice.Text),
                        Convert.ToDouble(txtbxDeposit.Text),
                        Convert.ToDouble(txtbxInterestRate.Text)/100,
                        Convert.ToDouble(txtbxMonths.Text)
                    );
                //Checking if the loan will be approved
                if (loan.GetCost() > (0.33 * grossIn))
                { MessageBox.Show("Warning: Monthly repayment exceeds 1/3 of your monthly income and is unlikely to be approved!"); }
                return loan;
            }
            else { return new Expense("Monthly Rental Cost", Convert.ToDouble(txtbxRent.Text)); }
        }

        //Returns an expense list for car
        private Car GetCar() 
        {
            return new Car(
                txtbxModelMake.Text,
                Convert.ToDouble(txtbxCarPrice.Text),
                Convert.ToDouble(txtbxCarDeposit.Text),
                Convert.ToDouble(txtbxCarRate.Text)/100,
                Convert.ToDouble(txtbxCarInsurance.Text)
                ); ;
        }

        //-------------------GUI------------------------------------------------------------

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
            if (chkbxBuyCar.IsChecked == false) { buyCarGrid.Visibility = Visibility.Hidden; txtbxModelMake.Visibility = Visibility.Hidden; }
            else { buyCarGrid.Visibility = Visibility.Visible; txtbxModelMake.Visibility = Visibility.Visible; }
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
                
                //Adding expenses to our list
                currentExpenses.AddRange(GetBasics());
                currentExpenses.Add(GetHousing());
                //Only add the car expense if the user has chosen to
                if (chkbxBuyCar.IsChecked==true) { currentExpenses.Add(GetCar()); }

                //Checking if all expenses exceed 75% of income
                ExpenseCheck();

                //Updating the expenses list on the summary page
                lstbxExpenses.ItemsSource = currentExpenses.OrderByDescending(x => x.GetCost()).ToList();
                //Performing calculations and setting monthly money leftover
                txtbxMonthlyMoney.Text = calc.CalculateMonthlyMoneyLeftover(grossIn, currentExpenses)+"";
                //Takes us to the next page
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
            //Clears the list box displaying expenses
            lstbxExpenses.ItemsSource=null;
            //Clears the current expense list
            currentExpenses.Clear();
            btnCalcSavings.IsEnabled = true;
            Tabs.SelectedIndex--;

        }

        private void chkbxSaveUp_Checked(object sender, RoutedEventArgs e)
        {
            if (chkbxSaveUp.IsChecked == false) { savingsGrid.Visibility = Visibility.Hidden;}
            else { savingsGrid.Visibility = Visibility.Visible; }
        }

        private void btnCalcSavings_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSavings()) 
            {
                //Number of months to save
                double savingAmount = Convert.ToDouble(txtbxSaveAmount.Text);
                double months = (((Convert.ToDateTime(txtbxSavingDate.Text).Year - DateTime.Now.Year) * 12) + Convert.ToDateTime(txtbxSavingDate.Text).Month - DateTime.Now.Month);
                double years = Convert.ToDateTime(txtbxSavingDate.Text).Year - DateTime.Now.Year;
                double rate = Convert.ToDouble(txtbxSavingRate.Text) / 100;
                //Calculation
                double ans = Math.Floor((savingAmount * (rate / months)) / (Math.Pow((1 + (rate / months)), months * years) - 1));
                txtbxReqSavings.Text = ans+"";
                Expense savings = new Expense("Monthly Savings", ans);
                //Adding it to our expenses
                currentExpenses.Add(savings);
                lstbxExpenses.ItemsSource = currentExpenses.OrderByDescending(x => x.GetCost()).ToList();
                btnCalcSavings.IsEnabled = false;
                txtbxMonthlyMoney.Text = calc.CalculateMonthlyMoneyLeftover(grossIn, currentExpenses) + "";
            }
        }

        
    }
}
