using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqDemo
{
    internal class Details : IDetails
    {
        public string ShowCompany()
        {
            return "Its from Wipro Company...";
        }

        public string ShowStudent()
        {
            return "Hi Iam Madhu";
        }
    }
}
