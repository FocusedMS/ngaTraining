using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3Sessions
{
    internal class CricketScoreTracker
    {
        static int totalScore;

        public void AddRuns(int runs)
        {
            totalScore += runs;
        }

        static void Main(string[] args)
        {
            CricketScoreTracker firstBatsman = new CricketScoreTracker();
            CricketScoreTracker secondBatsman = new CricketScoreTracker();
            CricketScoreTracker thirdBatsman = new CricketScoreTracker();

            firstBatsman.AddRuns(50);
            secondBatsman.AddRuns(45);
            thirdBatsman.AddRuns(61);

            Console.WriteLine("Total Score: " + totalScore);
        }
    }
}
