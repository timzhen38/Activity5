using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class Person
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("probability")]
        public double Probability { get; set; }

    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a person's name to predict their gender. Press Enter without writing a name to quit the program.");

                    var name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://api.genderize.io?name=" + name.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var person = JsonConvert.DeserializeObject<Person>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Name: " + person.Name);
                    Console.WriteLine("Gender: " + person.Gender);
                    Console.WriteLine("Probability of the above gender: " + person.Probability);
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid name!");
                }
            }
        }
    }
}