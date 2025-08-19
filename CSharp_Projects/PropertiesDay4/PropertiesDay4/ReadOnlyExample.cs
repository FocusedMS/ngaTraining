using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesDay4
{
    public class Bank
    {
        public int AccountNo { get; } = 12;
        public string BranchName { get; } = "ECIL";
        public string BankName { get; } = "ICICI";
    }
    internal class ReadOnlyExample
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            Console.WriteLine("Account No: " + bank.AccountNo);
            Console.WriteLine("Branch Name: " + bank.BranchName);
            Console.WriteLine("Bank Name: " + bank.BankName);
        }

    }
}
