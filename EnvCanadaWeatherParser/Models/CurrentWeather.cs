using System;

namespace MarkStrendin.EnvCanadaWeatherParser
{
    public class CurrentWeather
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string TemperatureCelsius { get; set; }
        public string Conditions { get; set; }
        public string Visibility { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string DewPointCelsius { get; set; }
        public string Wind { get; set; }
        public string AirQualityHealthIndex { get; set; }
        public string ObservedAt { get; set; }
        public string SourceURL { get; set; }
        public string TemperatureFahrenheit
        {
            get
            {
                if (!string.IsNullOrEmpty(this.TemperatureCelsius))
                {
                    decimal.TryParse(this.TemperatureCelsius, out decimal celsiusDecimal);
                    return ((celsiusDecimal * 9) / 5 + 32).ToString();

                }               

                return "unknown";
            }
        }

        public string DewPointFahrenheit {
            get
            {
                if (!string.IsNullOrEmpty(this.DewPointCelsius))
                {
                    decimal.TryParse(this.DewPointCelsius, out decimal celsiusDecimal);
                    return ((celsiusDecimal * 9) / 5 + 32).ToString();

                }

                return "unknown";
            }
        }
    }
}
