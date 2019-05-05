using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace MarkStrendin.EnvCanadaWeatherParser
{
    public class EnvCanadaCurrentWeatherParser
    {

        private const string _locationTitleFluff = " - Weather - Environment Canada";
        private const string _urlFluff = "https://www.weather.gc.ca/rss/city/";
        private static readonly string[] _removeFromURLToCreateLocalID = new string[] { "" };

        public CurrentWeather ParseXML(string inputXML)
        {
            if (inputXML == null)
            {
                throw new UnexpectedXMLFormatException("XML input is null");
            }

            if (string.IsNullOrEmpty(inputXML))
            {
                throw new UnexpectedXMLFormatException("XML input is empty");
            }

            int fieldsWithFoundValues = 0;

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
                            if (firstLevelElement.Value.Contains(_locationTitleFluff))
                            {
                                returnMe.LocationName = parseLocationTitle(firstLevelElement.Value);
                                fieldsWithFoundValues++;
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
                                    if (attribute.Value.Contains(_urlFluff))
                                    {                                        
                                        returnMe.SourceURL = attribute.Value;
                                        returnMe.LocationId = parseLocalIDFromSourceURL(attribute.Value);
                                        fieldsWithFoundValues++;
                                    } else
                                    {
                                        throw new UnexpectedXMLFormatException("Incorrect XML format - self ref was \"" + attribute.Value + "\", but expected \"" + _urlFluff + "...\"");
                                    }
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
                                        fieldsWithFoundValues++;
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
                                            fieldsWithFoundValues++;
                                            returnMe.Conditions = currentConditionsElement.Replace("<b>Condition:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Temperature:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.TemperatureCelsius = currentConditionsElement.Replace("<b>Temperature:</b>", string.Empty).Replace("&deg;C", string.Empty).Trim();
                                            
                                            // Also calculate Fahrenheit conversion
                                            string tempF = "unknown";
                                            if (decimal.TryParse(returnMe.TemperatureCelsius, out decimal celsiusDecimal))
                                            {
                                                tempF = ((celsiusDecimal * 9) / 5 + 32).ToString();
                                            }
                                            returnMe.TemperatureFahrenheit = tempF;

                                            // Also calculate Kelvin, just for kicks
                                            returnMe.TemperatureKelvin = (celsiusDecimal + (decimal)273.15).ToString();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Pressure / Tendency:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Pressure = currentConditionsElement.Replace("<b>Pressure / Tendency:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Wind Chill:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.WindChillCelsius = currentConditionsElement.Replace("<b>Wind Chill:</b>", string.Empty).Trim();
                                            // Also calculate Fahrenheit conversion
                                            string windF = "unknown";
                                            if (decimal.TryParse(returnMe.WindChillCelsius, out decimal celsiusDecimal))
                                            {
                                                windF = ((celsiusDecimal * 9) / 5 + 32).ToString();
                                            }
                                            returnMe.WindChillFahrenheit = windF;
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Pressure:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Pressure = currentConditionsElement.Replace("<b>Pressure:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Visibility:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Visibility = currentConditionsElement.Replace("<b>Visibility:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Humidity:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Humidity = currentConditionsElement.Replace("<b>Humidity:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Dewpoint:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.DewPointCelsius = currentConditionsElement.Replace("<b>Dewpoint:</b>", string.Empty).Replace("&deg;C", string.Empty).Trim();

                                            // Also calculate Fahrenheit conversion
                                            string dewPointF = "unknown";
                                            if (decimal.TryParse(returnMe.DewPointCelsius, out decimal celsiusDecimal))
                                            {
                                                dewPointF = ((celsiusDecimal * 9) / 5 + 32).ToString();
                                            }
                                            returnMe.DewPointFahrenheit = dewPointF;
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Wind:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Wind = currentConditionsElement.Replace("<b>Wind:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Air Quality Health Index:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.AirQualityHealthIndex = currentConditionsElement.Replace("<b>Air Quality Health Index:</b>", string.Empty).Trim();
                                        }

                                        if (currentConditionsElement.StartsWith("<b>Observed at:</b>"))
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.ObservedAt = currentConditionsElement.Replace("<b>Observed at:</b>", string.Empty).Trim();
                                        }
                                    }

                                }
                            }

                        }
                    }
                }
            }

            if (fieldsWithFoundValues < 2)
            {
                throw new UnexpectedXMLFormatException("XML file did not contain enough valid data (is it the correct XML file?)");
            }

            return returnMe;
        }

        private static string parseLocationTitle(string title)
        {
            if (title.Contains(_locationTitleFluff))
            {
                return title.Substring(0, title.IndexOf(_locationTitleFluff)).Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        private static string parseLocalIDFromSourceURL(string input)
        {
            return input.Replace(_urlFluff, string.Empty).Replace("_e.xml", string.Empty).Replace("_f.xml", string.Empty);
        }



    }
}
