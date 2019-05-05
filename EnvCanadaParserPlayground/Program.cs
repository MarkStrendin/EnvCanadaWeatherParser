using MarkStrendin.EnvCanadaWeatherParser;
using System;
using System.IO;
using System.Text;

namespace MarkStrendin.EnvCanadaParserPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();

            // Read in the example file
            StreamReader reader = new StreamReader("./example_data.xml");
            string exampleContent = reader.ReadToEnd();

            // Run it through the parser
            CurrentWeather result = parser.ParseXML(exampleContent);

            // Send the results to the console
            Console.WriteLine(CurrentWeatherToString(result));                        
        }


       

        private static string CurrentWeatherToString(CurrentWeather currentWeather)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("{\n");
            builder.Append("  \"LocationId:\": \"" + currentWeather.LocationId +"\",\n");
            builder.Append("  \"LocationName:\": \"" + currentWeather.LocationName +"\"\n");
            builder.Append("  \"LastUpdated:\": \"" + currentWeather.LastUpdated.ToString() +"\"\n");
            builder.Append("  \"TemperatureCelsius\": \"" + currentWeather.TemperatureCelsius + "\"\n");
            builder.Append("  \"TemperatureFahrenheit\": \"" + currentWeather.TemperatureFahrenheit + "\"\n");
            builder.Append("  \"Conditions\": \"" + currentWeather.Conditions +"\"\n");
            builder.Append("  \"Visibility\": \"" + currentWeather.Visibility +"\"\n");
            builder.Append("  \"Pressure\": \"" + currentWeather.Pressure +"\"\n");
            builder.Append("  \"Humidity\": \"" + currentWeather.Humidity +"\"\n");
            builder.Append("  \"DewPointCelsius\": \"" + currentWeather.DewPointCelsius + "\"\n");
            builder.Append("  \"DewPointFahrenheit\": \"" + currentWeather.DewPointFahrenheit + "\"\n");
            builder.Append("  \"Wind\": \"" + currentWeather.Wind +"\"\n");
            builder.Append("  \"AirQualityHealthIndex\": \"" + currentWeather.AirQualityHealthIndex + "\"\n");
            builder.Append("  \"ObservedAt\": \"" + currentWeather.ObservedAt + "\"\n");
            builder.Append("  \"SourceURL\": \"" + currentWeather.SourceURL + "\"\n");
            builder.Append("}\n");

            return builder.ToString();
        }
    }
}
