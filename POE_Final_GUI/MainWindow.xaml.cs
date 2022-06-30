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

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnSeBack_Click(object sender, RoutedEventArgs e)
        {
            prevTab();
        }

        //Moves to the next tab
        private void nextTab()
        {
            Tabs.SelectedIndex = Tabs.SelectedIndex + 1;
        }

        private void prevTab()
        {
            Tabs.SelectedIndex = Tabs.SelectedIndex - 1;
        }

        private void btnSeNex_Click(object sender, RoutedEventArgs e)
        {
            nextTab();
        }

        private void btnSeBack_Click_1(object sender, RoutedEventArgs e)
        {
            currentExpenses.Clear();
            prevTab();
        }

        //The 'Next' button on the first page of the app
        //This method is responsible for 
        private void btnFeNextClick(object sender, RoutedEventArgs e)
        {
            
        }

        //Runs all validations in order and returns true if all are valid
        bool allValid() 
        {
            bool valid = true;
            //Validating buttons
            if (!validateButtons()) { valid = false; }
            //Validating basics
            //if () { }
            //Validating housing
            //if () { }
            //Validating car
            //if(){}
            return valid;
        }

        //Validates all values for basic expenses such as 

        //Validates that the user has made valid selections and the app can continue
        private bool validateButtons() 
        {
            //If the user has not selected buy or rent
            if (rbtnBuy.IsChecked == false && rbtnRent.IsChecked == false) { MessageBox.Show("Please select either Buying or Renting"); return false; }
            return true;
        }

        private void txtbxGrossIn_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //--------------------------------------------------------------------------------------

        //Returns an array list of all values entered for basic expenses
        public ArrayList GetBasics() 
        {
            ArrayList list = new ArrayList();
            foreach (TextBox t in basicExpensestxtbxgrp.Children) {list.Add(t.Text);}
            return list;
        }

        //Validates the types for all values entered for income, tax and basic expenses like groceries
        private bool validateInput(ArrayList list) 
        {
            //Validation for just for the gross in
            try { double z = double.Parse(txtbxGrossIn.Text); txtbxGrossIn.Background = Brushes.White; }
            catch (Exception e) { Console.WriteLine(e); txtbxGrossIn.Text = "Enter a valid value" ; txtbxGrossIn.Background = Brushes.Red; return false; }
            
            //Different textboxes are grouped into grids depending on their function, we then iterate through the children of these grids for validation
            //Validation for basic expenses such as 
            foreach (TextBox t in basicExpensestxtbxgrp.Children){
                try { double x = double.Parse(t.Text); t.Background = Brushes.White; }
                catch (FormatException format) { Console.WriteLine(format); t.Text = "Enter a valid value"; t.Background = Brushes.Red; return false; }
            }
            return true;
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

        private void toggleBuyCar(bool toggle) {  }
    }
}
