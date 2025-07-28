using AgentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.Services
{
    public class AgentService : IAgentService
    {
        private List<Agent> _agents = new List<Agent>();

        public string AddAgent(string name, string city, string gender, double premiumAmount)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                return "Name must be at least 3 characters long.";
            if (gender.ToUpper() != "MALE" && gender.ToUpper() != "FEMALE")
                return "Gender must be 'Male' or 'FEMALE'.";
            if (premiumAmount <= 10000)
                return "Premium amount must be greater than 10,000.";

            var agent = new Agent(name, city, gender.ToUpper(), premiumAmount);
            _agents.Add(agent);
            return $"Agent added with ID: {agent.AgentId}";
        }

        public List<Agent> GetAllAgents()
        {
            return _agents;
        }

        void IAgentService.AddAgent(string name, string city, string gender, double premiumAmount)
        {
            throw new NotImplementedException();
        }
    }
}
