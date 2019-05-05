using System;
using System.Collections.Generic;
using System.Text;

namespace MarkStrendin.EnvCanadaWeatherParser
{
    public class UnexpectedXMLFormatException : Exception
    {
        public UnexpectedXMLFormatException() : base() { }
        public UnexpectedXMLFormatException(string message) : base(message) { }
        public UnexpectedXMLFormatException(string message, Exception innerException) : base(message, innerException) { }
    }
}
