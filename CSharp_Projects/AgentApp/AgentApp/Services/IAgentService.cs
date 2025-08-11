using AgentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.Services
{
    public interface IAgentService
    {
        void AddAgent(string name, string city, string gender, double premiumAmount);
        List<Agent> GetAllAgents();
    }
}
