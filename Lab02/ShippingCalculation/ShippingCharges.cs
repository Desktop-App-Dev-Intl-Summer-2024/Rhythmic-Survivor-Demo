using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab02.ShippingCalculation
{
    public class ShippingCharges
    {
        public double weight {  get; set; }
        public int distance { get; set; }

        public double calculateShippingCost()
        {
            if (distance <= 0)
            {
                return -1;
            }

            int distanceMultiplicator = distance / 500;
            if(weight < 2)
            {
                return 1.10 * distanceMultiplicator;
            }
            else if(weight >= 2 &&  weight < 6)
            {
                return 2.20 * distanceMultiplicator;
            }
            else if(weight >= 6 && weight < 10)
            {
                return 3.70 * distanceMultiplicator;
            }
            else
            {
                return 4.80 * distanceMultiplicator;
            }
        }
    }
}
