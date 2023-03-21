
using RestSharp;
using System.Text.Json;

namespace SwapiAPI;
public class Lookup
{
    //TODO Cache


    static void Main(string[] args)
    {
        RestClient client = new RestClient("https://swapi.dev/api/");

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
                    Console.WriteLine(GetPerson(userInput, client));
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

    private static String GetPerson(string id, RestClient client)
    {
        // Check if the id is a integer
        if (!Int32.TryParse(id, out var personId))
        {
            throw new Exception("The provided ID is not a number");
        }

        //TODO: check the cache for the id

        //TODO: Maybe make this parameterized to combat SQL injection (Or do we trust SWAPI to handle that? )
        var request = new RestRequest("people/" + id);

        var response = client.Get(request);

        //Get the Json from the response
        string json = response.Content;

        //Deserialize into a person object
        Person? person = JsonSerializer.Deserialize<Person>(json);

        return person.ToString();
    }
}