using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Final_GUI.Expenses
{
    internal class Car : Expense
    {
        //Assumes 60 months / 5 years as specified
        int months = 60;
        string modelMake;
        double purchasePrice;
        double totalDeposit;
        double interestRate;
        double insurancePremium;

        //Constructor
        public Car(string modelMake, double price, double deposit, double interest, double insurance) : base("Car Repayments (Insurance + Loan)", 0)
        {
            this.modelMake = modelMake;
            this.purchasePrice = price;
            this.totalDeposit = deposit;
            this.interestRate = interest;
            this.insurancePremium = insurance;
            base.SetCost(CalculateMonthlyLoanRepayment(this.purchasePrice, this.totalDeposit, this.interestRate, this.insurancePremium));
        }

        //Calcualtes the monthly loan repayment for the car using simple interest
        public double CalculateMonthlyLoanRepayment(double newPrice, double newDeposit, double newRate, double insurance)
        {
            double p = newPrice - newDeposit;
            double n = (months / 12);
            //Applies the simple interest formula to our given data and rounds the payment to 2 decimal places
            double ans = Math.Round(((p * (1 + newRate * n)) / months) + insurance, 2);
            Console.WriteLine("Your Monthly Car Loan Repayment will be: R" + ans);
            return ans;
        }
    }
}
