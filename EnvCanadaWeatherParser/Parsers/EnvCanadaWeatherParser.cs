using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace MarkStrendin.EnvCanadaWeatherParser
{
    public class EnvCanadaCurrentWeatherParser
    {

        public CurrentWeather ParseXML(string inputXML)
        {
            XDocument doc = XDocument.Parse(inputXML);
            CurrentWeather returnMe = new CurrentWeather();
            foreach (XElement rootElement in doc.Elements())
            {
                foreach (XElement firstLevelElement in rootElement.Elements())
                {
                    // Find the first "title" element to retrieve the location name                    
                    if (firstLevelElement.Name.LocalName.ToLower() == "title")
                    {
                        if (string.IsNullOrEmpty(returnMe.LocationName))
                        {
                            if (firstLevelElement.Value.Contains(" - Weather - Environment Canada"))
                            {
                                returnMe.LocationName = parseLocationTitle(firstLevelElement.Value);
                            }
                        }
                    }

                    // Get the LocalId
                    if (firstLevelElement.Name.LocalName.ToLower() == "link")
                    {
                        if (firstLevelElement.ToString().Contains("rel=\"self\""))
                        {
                            foreach(XAttribute attribute in firstLevelElement.Attributes())
                            {
                                if (attribute.Name.LocalName == "href")
                                {
                                    returnMe.SourceURL = attribute.Value;

                                    returnMe.LocationId = attribute.Value.Replace("https://www.weather.gc.ca/rss/city/", string.Empty).Replace("_e.xml", string.Empty).Replace("_f.xml", string.Empty);
                                }
                            }
                        }
                    }

                    // Find the "entry" element that has a title that starts with "Current Conditions: "
                    if (firstLevelElement.Name.LocalName.ToLower() == "entry")
                    {
                        if (firstLevelElement.Value.Contains("Current Conditions: "))
                        {
                            foreach (XElement childElement in firstLevelElement.Elements())
                            {
                                // Get the last updated timestamp
                                if (childElement.Name.LocalName == "updated")
                                {
                                    DateTime.TryParse(childElement.Value, out DateTime parsedValue);
                                    if (parsedValue > DateTime.MinValue)
                                    {
                                        returnMe.LastUpdated = parsedValue;
                                    }
                                }

                                // Get the conditions block
                                if (childElement.Name.LocalName == "summary")
                                {
                                    List<string> currentConditionsSummary = childElement.Value.Split(new string[] { "<br/>" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                                    foreach (string currentConditionsElement in currentConditionsSummary)
                                    {
                                        if (currentConditionsElement.StartsWith("<b>Condition:</b>"))
                                        {
                                            returnMe.Conditions = currentConditionsElement.Replace("<b>Condition:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Temperature:</b>"))
                                        {
                                            returnMe.TemperatureCelsius = currentConditionsElement.Replace("<b>Temperature:</b>", string.Empty).Replace("&deg;C", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Pressure:</b>"))
                                        {
                                            returnMe.Pressure = currentConditionsElement.Replace("<b>Pressure:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Visibility:</b>"))
                                        {
                                            returnMe.Visibility = currentConditionsElement.Replace("<b>Visibility:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Humidity:</b>"))
                                        {
                                            returnMe.Humidity = currentConditionsElement.Replace("<b>Humidity:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Dewpoint:</b>"))
                                        {
                                            returnMe.DewPointCelsius = currentConditionsElement.Replace("<b>Dewpoint:</b>", string.Empty).Replace("&deg;C", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Wind:</b>"))
                                        {
                                            returnMe.Wind = currentConditionsElement.Replace("<b>Wind:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Air Quality Health Index:</b>"))
                                        {
                                            returnMe.AirQualityHealthIndex = currentConditionsElement.Replace("<b>Air Quality Health Index:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Observed at:</b>"))
                                        {
                                            returnMe.ObservedAt = currentConditionsElement.Replace("<b>Observed at:</b>", string.Empty).Trim();
                                        }
                                    }

                                }
                            }

                        }
                    }
                }
            }
            return returnMe;
        }

        private static string parseLocationTitle(string title)
        {
            if (title.Contains(" - Weather - Environment Canada"))
            {
                return title.Substring(0, title.IndexOf(" - Weather - Environment Canada")).Trim();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
