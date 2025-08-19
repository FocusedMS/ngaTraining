using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.Exceptions
{
    public class AgentvalidationException : Exception
    {
        public AgentvalidationException(string message) : base(message) { }
    }
}
