using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace MarkStrendin.EnvCanadaWeatherParser
{
    public class EnvCanadaCurrentWeatherParser
    {

        private const string _locationTitleFluff_EN = " - Weather - Environment Canada";
        private const string _locationTitleFluff_FR = " - Météo - Environnement Canada";
        private const string _urlFluff_EN = "https://www.weather.gc.ca/rss/city/";
        private const string _urlFluff_FR = "https://www.meteo.gc.ca/rss/city/";

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
                            if (firstLevelElement.Value.Contains(_locationTitleFluff_EN) || firstLevelElement.Value.Contains(_locationTitleFluff_FR))
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
                                    if (
                                        (attribute.Value.Contains(_urlFluff_EN)) ||
                                        (attribute.Value.Contains(_urlFluff_FR))
                                        )
                                    {                                        
                                        returnMe.SourceURL = attribute.Value;
                                        returnMe.LocationId = parseLocalIDFromSourceURL(attribute.Value);
                                        fieldsWithFoundValues++;
                                    } else
                                    {
                                        throw new UnexpectedXMLFormatException("Incorrect XML format - self ref was \"" + attribute.Value + "\", but expected \"" + _urlFluff_EN + "...\" or \"" + _urlFluff_FR + "\"");
                                    }
                                }
                            }
                        }
                    }

                    // Find the "entry" element that has a title that starts with "Current Conditions: "
                    if (firstLevelElement.Name.LocalName.ToLower() == "entry")
                    {
                        if (
                            (firstLevelElement.Value.Contains("Current Conditions: ")) ||
                            (firstLevelElement.Value.Contains("Conditions actuelles: "))
                            )
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
                                        
                                        if (
                                            (currentConditionsElement.StartsWith("<b>Temperature:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Température:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.TemperatureCelsius = currentConditionsElement.Replace("<b>Température:</b>", string.Empty).Replace("<b>Temperature:</b>", string.Empty).Replace("&deg;C", string.Empty).Trim();
                                            
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

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Pressure / Tendency:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Pression / Tendance:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Pressure = currentConditionsElement.Replace("<b>Pression / Tendance:</b>", string.Empty).Replace("<b>Pressure / Tendency:</b>", string.Empty).Trim();
                                        }

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Wind Chill:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Refroidissement éolien:</b>"))
                                            )

                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.WindChillCelsius = currentConditionsElement.Replace("<b>Refroidissement éolien:</b>", string.Empty).Replace("<b>Wind Chill:</b>", string.Empty).Trim();
                                            // Also calculate Fahrenheit conversion
                                            string windF = "unknown";
                                            if (decimal.TryParse(returnMe.WindChillCelsius, out decimal celsiusDecimal))
                                            {
                                                windF = ((celsiusDecimal * 9) / 5 + 32).ToString();
                                            }
                                            returnMe.WindChillFahrenheit = windF;
                                        }

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Pressure:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Pression:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Pressure = currentConditionsElement.Replace("<b>Pression:</b>", string.Empty).Replace("<b>Pressure:</b>", string.Empty).Trim();
                                        }

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Visibility:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Visibilité:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Visibility = currentConditionsElement.Replace("<b>Visibilité:</b>", string.Empty).Replace("<b>Visibility:</b>", string.Empty).Trim();
                                        }

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Humidity:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Humidité:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Humidity = currentConditionsElement.Replace("<b>Humidité:</b>", string.Empty).Replace("<b>Humidity:</b>", string.Empty).Trim();
                                        }

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Dewpoint:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Point de rosée:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.DewPointCelsius = currentConditionsElement.Replace("<b>Point de rosée:</b>", string.Empty).Replace("<b>Dewpoint:</b>", string.Empty).Replace("&deg;C", string.Empty).Trim();

                                            // Also calculate Fahrenheit conversion
                                            string dewPointF = "unknown";
                                            if (decimal.TryParse(returnMe.DewPointCelsius, out decimal celsiusDecimal))
                                            {
                                                dewPointF = ((celsiusDecimal * 9) / 5 + 32).ToString();
                                            }
                                            returnMe.DewPointFahrenheit = dewPointF;
                                        }

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Wind:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Vent:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.Wind = currentConditionsElement.Replace("<b>Vent:</b>", string.Empty).Replace("<b>Wind:</b>", string.Empty).Trim();
                                        }

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Air Quality Health Index:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Cote air santé:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.AirQualityHealthIndex = currentConditionsElement.Replace("<b>Cote air santé:</b>", string.Empty).Replace("<b>Air Quality Health Index:</b>", string.Empty).Trim();
                                        }

                                        if (
                                            (currentConditionsElement.StartsWith("<b>Observed at:</b>")) ||
                                            (currentConditionsElement.StartsWith("<b>Enregistrées à:</b>"))
                                            )
                                        {
                                            fieldsWithFoundValues++;
                                            returnMe.ObservedAt = currentConditionsElement.Replace("<b>Enregistrées à:</b>", string.Empty).Replace("<b>Observed at:</b>", string.Empty).Trim();
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
            if (title.Contains(_locationTitleFluff_EN))
            {
                return title.Substring(0, title.IndexOf(_locationTitleFluff_EN)).Trim();
            }
            else if (title.Contains(_locationTitleFluff_FR))
            {
                return title.Substring(0, title.IndexOf(_locationTitleFluff_FR)).Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        private static string parseLocalIDFromSourceURL(string input)
        {
            return input.Replace(_urlFluff_EN, string.Empty).Replace(_urlFluff_FR, string.Empty).Replace("_e.xml", string.Empty).Replace("_f.xml", string.Empty);
        }



    }
}
