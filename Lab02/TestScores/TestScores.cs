using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.TestScores
{
    internal class TestScores
    {
        public int score1 {  get; set; }
        public int score2 { get; set; }
        public int score3 { get; set; }
        public int scoreAvg { get; set; }

        public TestScores(int score1, int score2, int score3)
        {
            this.score1 = score1;
            this.score2 = score2;
            this.score3 = score3;
        }

        public double getTestScoreAverage()
        {
            scoreAvg = (score1 + score2 + score3) / 3;
            return scoreAvg;
        }

        public char getLetterGrade()
        {
            if(scoreAvg >= 90 && scoreAvg <= 100)
            {
                return 'A';
            }
            else if(scoreAvg >= 80 && scoreAvg < 90)
            {
                return 'B';
            }
            else if (scoreAvg >= 70 && scoreAvg < 80)
            {
                return 'C';
            }
            else if(scoreAvg >= 60 &&  scoreAvg < 70)
            {
                return 'D';
            }
            else if(scoreAvg < 60)
            {
                return 'F';
            }
            return 'Z';
        }
    }
}
