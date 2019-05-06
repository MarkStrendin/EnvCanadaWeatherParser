using MarkStrendin.EnvCanadaWeatherParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EnvCanadaWeatherParser_ExampleDataScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> validLocations = new List<string>();
            string timestamp = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            List<string> _validLocationPrefixes = new List<string>() { "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT" };            
            Random random = new Random(DateTime.Now.Millisecond);
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
                       
            List<CurrentWeather> results = new List<CurrentWeather>();

            using (HttpClient _httpClient = new HttpClient())
            {
                foreach (string provinceCode in _validLocationPrefixes)
                {
                    for (int x = 00; x <= 200; x++)
                    {
                        string locationCode = provinceCode.ToLower() + "-" + x.ToString();
                        string url = "https://weather.gc.ca/rss/city/" + locationCode + "_e.xml";
                        Console.Write(url + ": ");
                        try
                        {
                            string rawXML = getXMLFeed(url, _httpClient).Result;
                            CurrentWeather thisWeather = parser.ParseXML(rawXML);
                            if (thisWeather != null)
                            {
                                try
                                {
                                    if (!Directory.Exists(timestamp))
                                    {
                                        Directory.CreateDirectory(timestamp);
                                    }

                                    StreamWriter rawXMLStreamWriter = new StreamWriter(timestamp + "/" + locationCode + ".xml");
                                    rawXMLStreamWriter.Write(rawXML);
                                    rawXMLStreamWriter.Flush();
                                }
                                catch (Exception ex) { Console.WriteLine("ERROR WRITING XML FILE: " + ex.Message); }

                                Console.WriteLine(" [SUCCESS]");
                                results.Add(thisWeather);
                                validLocations.Add(locationCode);
                            }
                        }
                        catch
                        {
                            Console.WriteLine(" [FAIL]");
                        }

                        // Wait so we don't overwhelm the server
                        System.Threading.Thread.Sleep(random.Next(100, 500));
                    }
                }
            }

            results.Add(new CurrentWeather() { LocationId = "TEST" });

            string filename = "output-" + timestamp + ".csv";

            Console.WriteLine("Successes: " + results.Count);
            Console.WriteLine("Writing file");

            StreamWriter csvStreamWriter = new StreamWriter(filename);
            csvStreamWriter.WriteLine("\"locationID\",\"locationName\",\"lastupdated\",\"tempC\",\"tempF\",\"tempK\",\"Conditions\",\"Pressure\",\"Humidity\",\"DewPointC\",\"DewPointF\",\"Wind\",\"AirQualityIndex\",\"ObservedAt\",\"SourceURL\",\"WindChillC\",\"WindChillF\"");

            foreach (CurrentWeather weather in results)
            {
                csvStreamWriter.WriteLine(toCSVLine(weather));
            }

            csvStreamWriter.Flush();

            StreamWriter validLocationStreamWriter = new StreamWriter("valid_location_codes-" + timestamp + ".txt");
            foreach (string loc in validLocations)
            {
                validLocationStreamWriter.WriteLine(loc);
            }
            validLocationStreamWriter.Flush();
        }

        private static string toCSVLine(CurrentWeather weather)
        {
            StringBuilder returnMe = new StringBuilder();
            returnMe.Append("\"" + weather.LocationId + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.LocationName + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.LastUpdated + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.TemperatureCelsius + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.TemperatureFahrenheit + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.TemperatureKelvin + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.Conditions + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.Pressure + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.Humidity + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.DewPointCelsius + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.DewPointFahrenheit + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.Wind + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.AirQualityHealthIndex + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.ObservedAt + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.SourceURL + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.WindChillCelsius + "\"");
            returnMe.Append(",");
            returnMe.Append("\"" + weather.WindChillFahrenheit + "\"");
            return returnMe.ToString();
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

        private async static Task<string> getXMLFeed(string url, HttpClient httpClient)
        {
            using (HttpResponseMessage response = await httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            return string.Empty;
        }
    }
}
