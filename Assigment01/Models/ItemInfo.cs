using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment01.Models
{
    public class ItemInfo
    {
        public string name { get; set; }
        public int id { get; set; }
        public int amount { get; set; }
        public double price { get; set; }
        public int amountLeft { get; set; }

        public double getTotalItem()
        {
            return amount * price;
        }
        public string ToString()
        {
            return name + " Kg: " + amount;
        }
    }
}
