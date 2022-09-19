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
            string json = tota.ConvertToJson();
            Console.WriteLine(json);

            Person faraon;
            faraon = new Person("{\"Name\":\"Diego\",\"FamilyName\":\"Godín\"}");
            Console.WriteLine($"El 'Faraón' {faraon.Name} {faraon.FamilyName}");
            json = faraon.ConvertToJson();
            Console.WriteLine(json);

            IJsonConvertible jsonConvertible = new Person("Gastón", "Pereiro");
            Console.WriteLine(jsonConvertible.ConvertToJson());
        }
    }
}