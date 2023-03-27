using System.Net.Http.Headers;
using System.Xml;
using RestSharp;
using System.Runtime.Caching;
using System.Text.Json;
using System;

namespace SwapiAPI
{
    public class Person
    {
        public bool cacheStateFullObject { get; set; }
        public string name { get; set; }
        public string height { get; set; }
        public string mass { get; set; }
        public string hair_color { get; set; }
        public string skin_color { get; set; }
        public string eye_color { get; set; }
        public string birth_year { get; set; }
        public string gender { get; set; }
        public string homeworld { get; set; }
        //This contains a list of the URLS for the associated films
        public List<string> films { get; set; }
        //This contains a list of the Film objects, if a full load have been made
        public List<Film> loadedFilms { get; set; }
        public List<string> specieUrls { get; set; }
        public List<string> vehicleUrls { get; set; }
        public List<string> starshipUrls { get; set; }
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public string url { get; set; }

        public override string ToString()
        {
            return "Name: " + this.name + "\n" +
                   "Height: " + this.height + "\n" +
                   "Mass: " + this.mass + "\n" +
                   "Hair Color: " + this.hair_color + "\n" +
                   "Skin Color: " + this.skin_color + "\n" +
                   "Eye Color: " + this.eye_color + "\n" +
                   "Birth Year: " + this.birth_year + "\n" +
                   "Gender: " + this.gender + "\n" +
                   "Homeworld: " + this.homeworld + "\n";
        }

        public static Person GetPersonFullLoad(RestClient client, MemoryCache cache, string id)
        {
            //TODO: Maybe make this parameterized to combat SQL injection (Or do we trust SWAPI to handle that? )
            var request = new RestRequest("people/" + id);

            var response = client.Get(request);

            if (response.StatusCode.ToString() == "OK")
            {
                var json = response.Content;

                Person person = JsonSerializer.Deserialize<Person>(json);
                person.GetListItems(client, cache);
                //If it had been an API controller I might have considered caching the URL and done the check if it has been called, before moving through logic (Or used Redis for caching incoming request)
                cache.Add(new CacheItem("people/" + id, person), new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(120.0), });

                return person;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        private void GetListItems(RestClient client, MemoryCache cache)
        {
            GetFilms(this.films, client, cache);
        }

        private void GetFilms(List<String> urls, RestClient client, MemoryCache cache)
        {
            List<Film> result = new List<Film>();
            foreach (var url in urls)
            {
                string requestParameters = url.Replace(client.Options.BaseUrl.ToString(), "");
                Film item = cache.Get(requestParameters) as Film;
                if (item != null && item is Film)
                {
                    result.Add(item);
                }
                else
                {
                    var request = new RestRequest(requestParameters);
                    var response = client.Get(request);
                    var film = JsonSerializer.Deserialize<Film>(response.Content);
                    result.Add(film);
                    cache.Add(new CacheItem(requestParameters, film), new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(120.0), });
                }
            }
            this.loadedFilms = result;
        }
    }


}
