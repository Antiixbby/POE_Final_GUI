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
            prevTab();
        }

        private void btnFeNextClick(object sender, RoutedEventArgs e)
        {
            if (validateBasics(GetBasics())) 
            {
                nextTab();
            }
        }

        private void txtbxGrossIn_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Returns an array list of all values entered for basic expenses
        public ArrayList GetBasics() 
        {
            ArrayList list = new ArrayList();
            list.Add(txtbxGrossIn.Text);
            list.Add(txtbxTax.Text);
            list.Add(txtbxGroceries.Text);
            list.Add(txtbxWaterLights.Text);
            list.Add(txtbxTravel.Text);
            list.Add(txtbxPhone.Text);
            list.Add(txtbxOther.Text);
            return list;
        }

        //Validates the types for all values entered for income, tax and basic expenses like groceries
        private bool validateBasics(ArrayList list) 
        {
            foreach (TextBox t in gridFeTxtbxs.Children)
            {
                try { double x = double.Parse(t.Text); }
                catch (FormatException format) { t.Text = "Enter a valid value"; return false; }
            }
            return true;
        }

        private void txtbxOther_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
