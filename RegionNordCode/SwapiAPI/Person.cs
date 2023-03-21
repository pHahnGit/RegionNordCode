using System.Xml;

namespace SwapiAPI
{
    public class Person
    {
        public string name { get; set; }
        public string height { get; set; }
        public string mass { get; set; }
        public string hair_color { get; set; }
        public string skin_color { get; set; }
        public string eye_color { get; set; }
        public string birth_year { get; set; }
        public string gender { get; set; }
        public string homeworld { get; set; }
        public List<string> films { get; set; }
        public List<string> species { get; set; }
        public List<string> vehicles { get; set; }
        public List<string> starships { get; set; }
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
    }


}
