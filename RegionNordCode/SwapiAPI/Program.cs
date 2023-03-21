using RestSharp;
using System.Text.Json;
using System.Runtime.Caching;
using System;

namespace SwapiAPI;
public class Lookup
{

    static void Main(string[] args)
    {
        //Adding a client for API calls
        RestClient client = new RestClient("https://swapi.dev/api/");

        // Cache 
        MemoryCache cache = new MemoryCache("SWAPI");
        
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
                    Console.WriteLine(GetPerson(userInput, client, cache));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong");
                    //Should do some logging here
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    public static string GetPerson(string id, RestClient client, MemoryCache cache)
    {
        // Check if the id is a integer
        if (!Int32.TryParse(id, out var personId))
        {
            throw new Exception("The provided ID is not a number");
        }

        string json = null;

        //TODO: check the cache for the id
        var item = cache.GetCacheItem(id);
        if (item != null)
        {
            json = item.Value.ToString();
        }
        else
        {
            //TODO: Maybe make this parameterized to combat SQL injection (Or do we trust SWAPI to handle that? )
            var request = new RestRequest("people/" + id);

            var response = client.Get(request);
            json = response.Content;

            cache.Add(new CacheItem(id, json), new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(120.0) });
        }

        Person person = JsonSerializer.Deserialize<Person>(json);

        return person.ToString();
    }
}