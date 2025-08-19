using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.Models
{
    public class Agent
    {
        private static int _counter = 1;

        public int AgentId { get; private set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public double PremiumAmount { get; set; }

        public Agent(string name, string city, string gender, double premiumAmount)
        {
            AgentId = _counter++;
            Name = name;
            City = city;
            Gender = gender;
            PremiumAmount = premiumAmount;
        }

        public override string ToString()
        {
            return $"{AgentId}: {Name}, {City}, Premium: {PremiumAmount}";
        }
    }
}
