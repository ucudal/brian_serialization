using System.Text.Json;

namespace Person
{
    public class Person
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public Person(string name, string familyName)
        {
            this.Name = name;
            this.FamilyName = familyName;
        }
    }
}