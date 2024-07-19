using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.DistanceTraveled
{
    class DistanceTraveled
    {
        public int speed { get; set; }
        public int time { get; set; }

        public int getDistance()
        {
            return speed * time;
        }
    }
}
