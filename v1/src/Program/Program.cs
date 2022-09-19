using System;
using System.Text.Json;

namespace Person
{
    class Program
    {
        static void Main(string[] args)
        {
            Person tota;
            tota = new Person("Diego", "Lugano");
            Console.WriteLine($"La 'Tota' {tota.Name} {tota.FamilyName}");
            string json = JsonSerializer.Serialize<Person>(tota);
            Console.WriteLine(json);

            Person faraon;
            faraon = JsonSerializer.Deserialize<Person>("{\"Name\":\"Diego\",\"FamilyName\":\"Godín\"}");
            Console.WriteLine($"El 'Faraón' {faraon.Name} {tota.FamilyName}");
            json = JsonSerializer.Serialize(faraon);
            Console.WriteLine(json);
        }
    }
}