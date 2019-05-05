using MarkStrendin.EnvCanadaWeatherParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarkStrendin.EnvCanadaParserPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> filenames = new List<string>()
            {
                "TestXML001.xml",
                "TestXML002.xml",
                "TestXML003.xml",
                "TestXML004.xml",
                "TestXML005.xml",
                "TestXML006.xml"
            };

            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();

            foreach(string filename in filenames)
            {
                StreamReader reader = new StreamReader("./TestXMLFiles/" + filename);
                string exampleContent = reader.ReadToEnd();
                CurrentWeather result = parser.ParseXML(exampleContent);
                Console.WriteLine(CurrentWeatherToString(result));
            }

            //https://weather.gc.ca/rss/city/sk-34_e.xml

            Console.WriteLine("Now the live XML feed:");
            string inputXML = getXMLFeed("https://weather.gc.ca/rss/city/sk-34_e.xml").Result;
            Console.WriteLine(CurrentWeatherToString(parser.ParseXML(inputXML)));
        }

        private async static Task<string> getXMLFeed(string url)
        {
            HttpClient httpClient = new HttpClient();
            using (HttpResponseMessage response = await httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();                    
                }
            }
            return string.Empty;
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
            builder.Append("  \"TemperatureKelvin\": \"" + currentWeather.TemperatureKelvin + "\"\n");
            builder.Append("  \"Conditions\": \"" + currentWeather.Conditions +"\"\n");
            builder.Append("  \"Visibility\": \"" + currentWeather.Visibility +"\"\n");
            builder.Append("  \"Pressure\": \"" + currentWeather.Pressure +"\"\n");
            builder.Append("  \"Humidity\": \"" + currentWeather.Humidity + "\"\n");
            builder.Append("  \"WindChillCelsius\": \"" + currentWeather.WindChillCelsius + "\"\n");
            builder.Append("  \"WindChillFahrenheit\": \"" + currentWeather.WindChillFahrenheit + "\"\n");
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
