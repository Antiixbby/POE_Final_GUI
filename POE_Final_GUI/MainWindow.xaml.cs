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
        //--------------------------------------------------------------------

        public MainWindow()
        {
            InitializeComponent();
            

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
            //If the values the user has entered are valid
            ArrayList basics = GetBasics();
            if (validateBasics(basics))
            {
                //Create new expense list
                List<Expense> basicExpenses = new List<Expense>();
                //Create the new double array
                double[] userDoubles = new double[basics.Count];
                //Convert our arraylist to doubles and fill our double array
                for (int i = 0; i < basics.Count; i++) { userDoubles[i] = Convert.ToDouble(basics[i]); }
                //Fill our expense array with new objects from the userDoubles and expenseNames arrays
                for(int i = 0; i < expenseNames.Length; i++) { basicExpenses.Add(new Expense(expenseNames[i], userDoubles[i])); }
                //Add that new Expense list to our current expense list
                currentExpenses.AddRange(basicExpenses);
                lstCurrentExpenses.ItemsSource = currentExpenses;
                nextTab();
            }
        }

        private void txtbxGrossIn_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //--------------------------------------------------------------------------------------

        //Returns an array list of all values entered for basic expenses
        public ArrayList GetBasics() 
        {
            ArrayList list = new ArrayList();
            foreach (TextBox t in gridFeTxtbxs.Children) {list.Add(t.Text);}
            return list;
        }

        //Validates the types for all values entered for income, tax and basic expenses like groceries
        private bool validateBasics(ArrayList list) 
        {
            foreach (TextBox t in gridFeTxtbxs.Children)
            {
                try { double x = double.Parse(t.Text); t.Background = Brushes.White; }
                catch (FormatException format) { Console.WriteLine(format); t.Text = "Enter a valid value"; t.Background = Brushes.Red; return false; }
            }
            return true;
        }

        private void txtbxOther_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
