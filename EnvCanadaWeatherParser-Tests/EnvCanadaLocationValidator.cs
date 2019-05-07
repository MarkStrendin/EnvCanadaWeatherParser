using System;
using System.Collections.Generic;
using System.Text;

namespace MarkStrendin.EnvCanadaWeatherParser.Tests
{
    static class EnvCanadaLocationValidator
    {
        private static readonly List<string> _validLocationPrefixes = new List<string>() { "ab", "bc", "mb", "nb", "nl", "nt", "ns", "nu", "on", "pe", "qc", "sk", "yt" };
        public static bool validateLocationCode(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length > 3)
                {
                    if (_validLocationPrefixes.Contains(input.Substring(0, 2)))
                    {
                        if (input.Substring(2, 1) == "-")
                        {
                            string locationNumer = input.Substring(3, input.Length - 3);
                            int.TryParse(locationNumer, out int locationIDNumer);
                            if ((locationIDNumer > 0) && (locationIDNumer < 200))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
