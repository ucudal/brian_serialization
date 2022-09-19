using System.Text.Json;
using System.Text.Json.Serialization;

namespace Person
{
    public class Person : IJsonConvertible
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }

        [JsonConstructor]
        public Person(string name, string familyName)
        {
            this.Name = name;
            this.FamilyName = familyName;
        }

        public Person(string json)
        {
            this.LoadFromJson(json);
        }

        public string ConvertToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public void LoadFromJson(string json)
        {
            Person deserialized = JsonSerializer.Deserialize<Person>(json);
            this.Name = deserialized.Name;
            this.FamilyName = deserialized.FamilyName;
        }
    }
}