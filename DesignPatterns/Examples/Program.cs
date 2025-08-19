using FactoryPattern;
using ObserverPattern;
using SingletonPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Singleton Pattern ===");
            Logger logger1 = Logger.Instance;
            Logger logger2 = Logger.Instance;

            logger1.Log("First log entry");
            logger2.Log("Second log entry");
            Console.WriteLine("Same instance? " + object.ReferenceEquals(logger1, logger2));
            Console.WriteLine();

            Console.WriteLine("=== Factory Pattern ===");
            IDocument doc1 = DocumentFactory.CreateDocument("pdf");
            IDocument doc2 = DocumentFactory.CreateDocument("word");
            doc1.Print();
            doc2.Print();
            Console.WriteLine();

            Console.WriteLine("=== Observer Pattern ===");
            WeatherStation station = new WeatherStation();
            WeatherDisplay display1 = new WeatherDisplay("Main");
            WeatherDisplay display2 = new WeatherDisplay("Secondary");

            station.Register(display1);
            station.Register(display2);

            station.SetWeatherData(30.5f, 70f);

            station.Unregister(display2);
            station.SetWeatherData(28.2f, 60f);
        }
    }
}
