using RestSharp;
using System.Text.Json;
using System.Runtime.Caching;
using System;

namespace SwapiAPI;
public class Lookup
{
    private static RestClient _client;
    private static MemoryCache _cache;
    static void Main()
    {
        _client = new RestClient("https://swapi.dev/api/");
        _cache = new MemoryCache("SWAPI");

        bool running = true;
        Console.WriteLine("Welcome to the SWAPI API people lookup ");
        Console.WriteLine("Type 'exit' to stop)");

        while (running)
        {
            Console.WriteLine("Enter the id on the starwars character you want to lookup:");

            var userInput = Console.ReadLine();

            //If the word exit is used, stop the program
            if (userInput.ToLower() == "exit")
            {
                Console.WriteLine("Bye!");
                running = false;
            }
            else if (userInput != null)
            {
                try
                {
                    Console.WriteLine(GetPerson(userInput));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong");
                    //Should do some logging here
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }
    }

    private static string GetPerson(string id)
    {
        // Check if the id is a integer
        if (!Int32.TryParse(id, out var personId))
        {
            throw new Exception("The provided ID is not a number");
        }

        string json = null;

        // Check cache for id
        var item = _cache.GetCacheItem("people/" + id);
        if (item != null)
        {
            return item.Value.ToString();
        }
        else
        {
            var person = Person.GetPersonFullLoad(_client, _cache, id);
            
            return person.ToString();
        }

    }
}