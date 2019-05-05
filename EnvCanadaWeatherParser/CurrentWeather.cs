using System;
using System.Collections.Generic;
using System.Text;

namespace MarkStrendin.EnvCanadaWeatherParser
{
    public class CurrentWeather
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string TemperatureCelcius { get; set; }
        public string Conditions { get; set; }
        public string Visibility { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string DewPoint { get; set; }
        public string Wind { get; set; }
        public string AirQualityHealthIndex { get; set; }
    }
}
