using Xunit;
using MarkStrendin.EnvCanadaWeatherParser;
using System.IO;
using System;

namespace MarkStrendin.EnvCanadaWeatherParser.Tests
{
    public class EnvCanadaWeatherParserTests
    {
        [Fact(DisplayName = "ParseXML should return a CurrentWeather object if the input is empty")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfInputEmpty()
        {
            // Arrange
            string inputXML = string.Empty;

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            var result = parser.ParseXML(inputXML);

            // Assert
            Assert.IsType<CurrentWeather>(result);
        }

        [Fact(DisplayName = "ParseXML should return a CurrentWeather object if the input is null")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfInputNull()
        {
            // Arrange
            string inputXML = null;

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            var result = parser.ParseXML(inputXML);

            // Assert
            Assert.IsType<CurrentWeather>(result);
        }

        [Fact(DisplayName = "ParseXML should return a CurrentWeather object if the input is not XML")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfInputInvalid()
        {
            // Arrange
            string inputXML = "THIS IS SOME INVALID TEXT";

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            var result = parser.ParseXML(inputXML);

            // Assert
            Assert.IsType<CurrentWeather>(result);
        }

        [Fact(DisplayName = "ParseXML should return a CurrentWeather object if the XML has no child elements")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfOnlyBareRootElement()
        {
            // Arrange
            string inputXML = "<root></root>";

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            var result = parser.ParseXML(inputXML);

            // Assert
            Assert.IsType<CurrentWeather>(result);
        }

        [Fact(DisplayName = "ParseXML should return a CurrentWeather object if the XML is completely different than expected")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfXMLFormattedDifferently()
        {
            // Arrange
            string inputXML = "<root><child></child><child attribute=\"value\"/></root>";

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            var result = parser.ParseXML(inputXML);

            // Assert
            Assert.IsType<CurrentWeather>(result);
        }

        [Theory(DisplayName = "ParseXML should return a CurrentWeather object with all test XML Files")]
        [InlineData("TestXML001.xml")]
        [InlineData("TestXML002.xml")]
        [InlineData("TestXML003.xml")]
        [InlineData("TestXML004.xml")]
        [InlineData("TestXML005.xml")]
        [InlineData("TestXML006.xml")]
        public void ParseXML_shouldReturnCurrentWeatherObjectWithTestXMLFile(string filename)
        {
            // Arrange
            StreamReader reader = new StreamReader("./TestXMLFiles/" + filename);
            string inputXML = reader.ReadToEnd();

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            var result = parser.ParseXML(inputXML);

            // Assert
            Assert.IsType<CurrentWeather>(result);
        }

        [Theory(DisplayName = "ParseXML should throw exception if the XML file isn't correct")]
        [InlineData("TestXML-Warnings-001.xml")]
        public void ParseXML_shouldNotReturnCurrentWeatherObjectIfXMLDifferent(string filename)
        {
            // Arrange
            StreamReader reader = new StreamReader("./TestXMLFiles/" + filename);
            string inputXML = reader.ReadToEnd();

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            var result = parser.ParseXML(inputXML);

            // Assert
            Assert.Throws<Exception>(() => parser.ParseXML(inputXML));
        }


    }
}
