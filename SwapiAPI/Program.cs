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
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
        Console.WriteLine("Welcome to the SWAPI API  lookup ");
        Console.WriteLine();
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------");

        MainMenu();
    }

    static void MainMenu()
    {
        Console.WriteLine("Press 1 if want to look up Star Wars Characters by ID");
        Console.WriteLine("Press 2 if you want to look up Star Wars Films by ID");
        Console.WriteLine("Press 3 to Exit");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                PeopleMenu();
                break;
            case "2":
                FilmMenu();
                break;
            case "3":
                Console.WriteLine("Thank you for visiting, Goodbye!");
                break;
            default:
                Console.WriteLine("That is not a valid input");
                MainMenu();
                break;
        }

    }

    private static void FilmMenu()
    {
        throw new NotImplementedException();
    }

    private static void PeopleMenu()
    {
        Console.Clear();
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
        Console.WriteLine("Below is a list of actions that can be performed!");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("type the ID of the Star Wiars Character you want to look up");
        Console.WriteLine("Type '0' to return to main menu.");

        bool running = true;

        while (running)
        {
            Console.WriteLine("Enter the ID of the Star Wars character you want to lookup:");

            var userInput = Console.ReadLine();

            //If the word exit is used, stop the program
            if (userInput.ToLower() == "0")
            {
                running = false;
                Console.Clear();
                MainMenu();
            }
            else if (userInput != null)
            {
                try
                {
                    Console.WriteLine(Person.GetPersonFullLoad(_client, _cache, userInput).ToString());

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



}
