using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Final_GUI.Expenses
{
     class HomeLoan : Expense
    {
        double purchasePrice;
        double totalDeposit;
        //Will be from 0.0 - 100
        double interestRatePercent;
        //Between 240-360
        double numMonthsToRepay;

        //Constructor does not take in expense cost as this will not change
        //We are passing in 0 for expense cost as it will be updated once the constructor has completed
        public HomeLoan(double newPrice, double newDeposit, double newRate, double newMonths) : base("Home Loan", 0)
        {
            this.purchasePrice = newPrice;
            this.totalDeposit = newDeposit;
            this.interestRatePercent = newRate;
            this.numMonthsToRepay = newMonths;
            //Setting the expense cost of the base expense
            base.SetCost(CalculateMonthlyLoanRepayment(this.purchasePrice, this.totalDeposit, this.interestRatePercent, this.numMonthsToRepay));
        }

        //Calculates the monthly repayment of a given loan
        public double CalculateMonthlyLoanRepayment(double newPrice, double newDeposit, double newRate, double newMonths)
        {
            double p = newPrice - newDeposit;
            double n = (newMonths / 12);
            //Applies the simple interest formula to our given data and rounds the payment to 2 decimal places
            double ans = Math.Round((p * (1 + newRate * n)) / newMonths, 2);
            Console.WriteLine("Your Monthly Home Loan Repayment will be: R" + ans);
            return ans;
        }
    }
}
