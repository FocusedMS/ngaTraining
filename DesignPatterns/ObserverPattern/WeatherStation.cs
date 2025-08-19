using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    public class WeatherStation
    {
        private List<IWeatherObserver> observers = new List<IWeatherObserver>();
        private float temperature;
        private float humidity;

        public void Register(IWeatherObserver observer)
        {
            observers.Add(observer);
        }

        public void Unregister(IWeatherObserver observer)
        {
            observers.Remove(observer);
        }

        public void SetWeatherData(float temp, float hum)
        {
            temperature = temp;
            humidity = hum;
            NotifyObservers();
        }

        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(temperature, humidity);
            }
        }
    }
}
