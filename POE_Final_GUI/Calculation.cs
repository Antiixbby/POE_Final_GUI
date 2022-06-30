using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Final_GUI
{
    internal class Calculation
    {
        //Takes in an object holding all values necessary to calculate monthly leftover money and performs the calculation
        public double CalculateMonthlyMoneyLeftover(double income, List<Expense> expenses)
        {

            return income - (GetTotalExpenses(expenses));
        }

        //Gets the total cost for all expenses in the expenses array
        public double GetTotalExpenses(List<Expense> expenses)
        {
            double total = 0;
            foreach (Expense e in expenses)
            {
                total += e.GetCost();
            }
            return total;
        }
    }
}
