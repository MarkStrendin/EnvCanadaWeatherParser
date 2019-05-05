using Xunit;
using MarkStrendin.EnvCanadaWeatherParser;
using System.IO;
using System;

namespace MarkStrendin.EnvCanadaWeatherParser.Tests
{
    public class EnvCanadaWeatherParserTests
    {
        [Fact(DisplayName = "ParseXML should throw an exception if the input is empty")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfInputEmpty()
        {
            // Arrange
            string inputXML = string.Empty;

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();

            // Assert
            Assert.Throws<UnexpectedXMLFormatException>(() => parser.ParseXML(inputXML));
        }

        [Fact(DisplayName = "ParseXML should throw an exception if the input is null")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfInputNull()
        {
            // Arrange
            string inputXML = null;

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();

            // Assert
            Assert.Throws<UnexpectedXMLFormatException>(() => parser.ParseXML(inputXML));
        }

        [Fact(DisplayName = "ParseXML should throw an exception if the input is parsable XML")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfInputInvalid()
        {
            // Arrange
            string inputXML = "THIS IS SOME INVALID TEXT";

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();

            // Assert
            Assert.Throws<System.Xml.XmlException>(() => parser.ParseXML(inputXML));
        }

        [Fact(DisplayName = "ParseXML should throw an exception if the XML is XML, but doesn't have the correct format")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfOnlyBareRootElement()
        {
            // Arrange
            string inputXML = "<root></root>";

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            
            // Assert
            Assert.Throws<UnexpectedXMLFormatException>(() => parser.ParseXML(inputXML));
        }

        [Fact(DisplayName = "ParseXML should throw an exception if the XML is completely different than expected")]
        public void ParseXML_shouldReturnCurrentWeatherObjectIfXMLFormattedDifferently()
        {
            // Arrange
            string inputXML = "<root><child></child><child attribute=\"value\"/></root>";

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();

            // Assert
            Assert.Throws<UnexpectedXMLFormatException>(() => parser.ParseXML(inputXML));
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

        [Theory(DisplayName = "ParseXML should throw an exception if you give it a warning XML file instead of a current conditions XML file")]
        [InlineData("TestXML-Warnings-001.xml")]
        public void ParseXML_shouldNotReturnCurrentWeatherObjectIfXMLDifferent(string filename)
        {
            // Arrange
            StreamReader reader = new StreamReader("./TestXMLFiles/" + filename);
            string inputXML = reader.ReadToEnd();

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();

            // Assert
            Assert.Throws<UnexpectedXMLFormatException>(() => parser.ParseXML(inputXML));
        }

        [Theory(DisplayName = "CurrentWeather object returned from ParseXML should not contain \".xml\" in the location ID")]
        [InlineData("TestXML001.xml")]
        [InlineData("TestXML002.xml")]
        [InlineData("TestXML003.xml")]
        [InlineData("TestXML004.xml")]
        [InlineData("TestXML005.xml")]
        [InlineData("TestXML006.xml")]
        public void ParseXML_ReturnedObjectShouldNotContainFilenameInLocationID(string filename)
        {
            // Arrange
            StreamReader reader = new StreamReader("./TestXMLFiles/" + filename);
            string inputXML = reader.ReadToEnd();

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            CurrentWeather result = parser.ParseXML(inputXML);


            // Assert
            Assert.DoesNotContain(".xml", result.LocationId);
        }

        [Theory(DisplayName = "CurrentWeather object returned from ParseXML should not contain \"http://\" in the location ID")]
        [InlineData("TestXML001.xml")]
        [InlineData("TestXML002.xml")]
        [InlineData("TestXML003.xml")]
        [InlineData("TestXML004.xml")]
        [InlineData("TestXML005.xml")]
        [InlineData("TestXML006.xml")]
        public void ParseXML_ReturnedObjectShouldNotContainHTTPInLocationID(string filename)
        {
            // Arrange
            StreamReader reader = new StreamReader("./TestXMLFiles/" + filename);
            string inputXML = reader.ReadToEnd();

            // Act
            EnvCanadaCurrentWeatherParser parser = new EnvCanadaCurrentWeatherParser();
            CurrentWeather result = parser.ParseXML(inputXML);
            
            // Assert
            Assert.DoesNotContain("http://", result.LocationId);
        }

    }
}
