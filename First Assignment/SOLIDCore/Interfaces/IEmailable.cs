using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDCore.Interfaces
{
    public interface IEmailable
    {
        void Email(string data, string recipientEmail); 
    }
}

