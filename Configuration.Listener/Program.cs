using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using TNDStudios.Utils.Configuration;

namespace Configuration.Listener
{
    class ListenerConfig
    {
        public string Taxonomy { get; set; }
        public string Path { get; set; }
        public ConsoleColor ConsoleForeground { get; set; }
        public ConsoleColor ConsoleBackground { get; set; }
    }

    class Program
    {
        static List<ListenerConfig> config = new List<ListenerConfig>()
        {
            new ListenerConfig()
            { 
                Taxonomy = "main",
                Path = "Tenant1.Application1.Module1",
                ConsoleBackground = ConsoleColor.Yellow,
                ConsoleForeground = ConsoleColor.Black
            },
            new ListenerConfig()
            {
                Taxonomy = "main",
                Path = "Tenant1.Application1.Module2",
                ConsoleBackground = ConsoleColor.DarkBlue,
                ConsoleForeground = ConsoleColor.White

            }
        };

        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            Dictionary<string, string> valueHistory = new Dictionary<string, string>();

            //localhost: 7071
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("Starting Listener");
            Console.WriteLine("Connecting To Application Configuration(s)");

            while (true)
            {
                foreach (ListenerConfig config in config)
                {
                    string key = $"{config.Taxonomy}|{config.Path}";
                    string oldValue = valueHistory.ContainsKey(key) ? valueHistory[key] : String.Empty;
                    string result = httpClient.GetStringAsync($"http://localhost:7071/api/properties/{config.Taxonomy}/{config.Path}?Inherit=true").Result;
                    Dictionary<String, TaxonomyProperty> properties = JsonConvert.DeserializeObject<Dictionary<String, TaxonomyProperty>>(result);

                    Console.BackgroundColor = config.ConsoleBackground;
                    Console.ForegroundColor = config.ConsoleForeground;

                    if (oldValue != result)
                    {
                        Console.WriteLine(key);

                        if (oldValue == String.Empty)
                            Console.WriteLine("New Configuration Recieved");
                        else
                            Console.WriteLine("Configuration Updated");

                        Console.WriteLine(result);
                    }

                    valueHistory[key] = result;

                }
                Thread.Sleep(1000);
            }
        }
    }
}
