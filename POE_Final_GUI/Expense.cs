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
        double price { get; set; }

        public Expense() { }

        public Expense(string newName, double newPrice) 
        {
            this.name = newName;
            this.price = newPrice;
        }

        public override string ToString()
        {
            return "Name: "+this.name+" Price: R"+this.price;
        }
    }
}
