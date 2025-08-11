using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    class Data
    {
        public void Show(string name)
        {
            lock (this)
            {
                Console.Write("Hello  " + name);
                Thread.Sleep(1000);
                Console.WriteLine(" How are You...");
            }
        }
    }
    public class SyncExample1
    {
        Data data;
        SyncExample1(Data data)
        {
            this.data = data;
        }

        public void sansa()
        {
            data.Show("sansa");
        }

        public void arya()
        {
            data.Show("Arya");
        }

        static void Main()
        {
            SyncExample1 syncExample1 = new SyncExample1(new Data());
            ThreadStart th1 = new ThreadStart(syncExample1.sansa);
            ThreadStart th2 = new ThreadStart(syncExample1.arya);
            Thread t1 = new Thread(th1);
            Thread t2 = new Thread(th2);

            t1.Start();
            t2.Start();
        }
    }
}