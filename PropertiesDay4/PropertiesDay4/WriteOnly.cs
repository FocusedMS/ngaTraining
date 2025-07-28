using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesDay4
{
    class Vendor
    {
        int vendorId;
        string vendorName;

        public int VendorId
        {
            set { vendorId = value; }
        }

        public string VendorName
        {
            set { vendorName = value; }
        }

        public override string ToString()
        {
            return "Vendor Id " + vendorId + " Vndor Name " + vendorName;
        }
    }

    internal class WriteOnly
    {
        static void Main()
        {
            Vendor vendor = new Vendor();
            vendor.VendorId = 101;
            vendor.VendorName = "ABC Pvt Ltd";

            Console.WriteLine(vendor);
        }
    }
}
