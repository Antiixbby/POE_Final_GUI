using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Final_GUI
{
    internal class Expense
    {
        string name { get; set; }
        double cost { get; set; }

        public Expense() { }

        public Expense(string newName, double newPrice) 
        {
            this.name = newName;
            this.cost = newPrice;
        }

        public double GetCost()
        {
            return this.cost;
        }

        public void SetCost(double cost)
        {
            this.cost = cost;
        }

        public string GetName() 
        {
            return this.name;
        }


        public override string ToString()
        {
            return "Name: "+this.name+" Price: R"+this.cost;
        }
    }
}
