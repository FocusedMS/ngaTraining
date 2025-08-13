using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    public class WeatherDisplay : IWeatherObserver
    {
        private string _name;

        public WeatherDisplay(string name)
        {
            _name = name;
        }

        public void Update(float temperature, float humidity)
        {
            Console.WriteLine(_name + " Display - Temp: " + temperature + "°C, Humidity: " + humidity + "%");
        }
    }
}