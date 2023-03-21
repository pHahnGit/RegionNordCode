using RestSharp;
using SwapiAPI;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static SemaphoreSlim semaphore = new SemaphoreSlim(3);
    private static Random rnd = new Random();
    private static RestClient restClient = new RestClient();

    public static void Main()
    {
        Task.WaitAll(CreateCalls().ToArray());
    }

    public static IEnumerable<Task> CreateCalls()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        foreach (var item in list)
        {
            yield return GetPerson(item);
        }
    }

    public async static Task GetPerson(int id)
    {
        await semaphore.WaitAsync();

        Console.WriteLine("Starting the request of person id: " + id);

        var request = new RestRequest("https://swapi.dev/api/people/" + id);
        var response = await restClient.GetAsync(request);

        Person person = JsonSerializer.Deserialize<Person>(response.Content);
        
        //Random sleep of the thread
        Thread.Sleep(rnd.Next(1000, 5000));

        Console.WriteLine("Result for person id: " + id);
        Console.WriteLine(person.ToString());

        semaphore.Release();

    }
}

