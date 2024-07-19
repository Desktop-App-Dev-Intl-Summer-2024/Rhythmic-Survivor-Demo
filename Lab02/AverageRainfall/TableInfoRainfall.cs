using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.AverageRainfall
{
    internal class TableInfoRainfall
    {
        public int Year {  get; set; }
        public double January { get; set; }
        public double February {  get; set; }
        public double March { get; set; }
        public double April { get; set; }
        public double May { get; set; }
        public double June { get; set; }
        public double July { get; set; }
        public double August { get; set; }
        public double September { get; set; }
        public double October { get; set; }
        public double November { get; set; }
        public double December { get; set; }
        public double YearAverage { get; set; }

        public TableInfoRainfall(int year, double january, double february, double march, double april, double may, double june, double july, double august, double september, double october, double november, double december)
        {
            Year = year;
            January = january;
            February = february;
            March = march;
            April = april;
            May = may;
            June = june;
            July = july;
            August = august;
            September = september;
            October = october;
            November = november;
            December = december;
            YearAverage = getYearAvg();
        }

        private double getYearAvg()
        {
            return (January +  February + March + April + May + June + 
                    July + August + September + October + November + December) / 12;
        }
    }
}
