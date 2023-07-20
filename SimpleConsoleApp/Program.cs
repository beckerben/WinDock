using System;
using System.Threading;
using System.Xml;

namespace SimpleConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Get environment variable
            string piServer = Environment.GetEnvironmentVariable("ENV_PI_SERVER");
            string xmlConfig = Environment.GetEnvironmentVariable("ENV_CONFIG_XML");

            // If the environment variables are null or whitespace, set it to the default value
            if (string.IsNullOrWhiteSpace(piServer))
            {
                piServer = "'Please specify ENV_PI_SERVER environment variable'";
            }

            if (string.IsNullOrWhiteSpace(xmlConfig))
            {
                xmlConfig = @"<?xml version=""1.0"" encoding=""utf-8"" ?><configuration><startup><supportedRuntime version=""v4.0"" sku="".NETFramework,Version=v4.8""/></startup><appSettings><add key=""ENV_TARGET_SERVER"" value=""'Please specify ENV_CONFIG_XML environment variable'""/></appSettings></configuration>";
                
            }

            // Print the server connection message
            Console.WriteLine($"Connecting to the PI server at {piServer} ...");

            // Demonstrate config from file which we'll bind mount

            // Load XML document from the environment variable
            var doc = new XmlDocument();
            doc.LoadXml(xmlConfig);
            string targetServer = doc.SelectSingleNode("//add[@key='ENV_TARGET_SERVER']").Attributes["value"].Value;
            Console.WriteLine($"Connecting to the Target server at {targetServer} ...");

            // Loop indefinitely
            while (true)
            {
                // Print the current timestamp
                Console.WriteLine("Current Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                // Sleep for 5 seconds
                Thread.Sleep(5000);
            }
        }
    }
}
