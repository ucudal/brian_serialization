# Universidad Católica del Uruguay
## Facultad de Ingeniería y Tecnologías
### Programación II
Código de ejemplo del tema "objetos, clases y mensajes"

Este código de ejemplo está dividido en dos partes.

### Primera parte

En la primera parte de este ejemplo mostramos cómo utilizar la clase [`JsonSerializer`](https://docs.microsoft.com/es-es/dotnet/api/system.text.json.jsonserializer?view=net-5.0) para convertir nuestros objetos a texto en formato [Json](https://www.json.org/json-en.html) o crear objetos a partir de texto en formato Json. Convertir los objetos a y desde texto en formato Json es útil cuando queremos "transmitir" los objetos de un lugar a otro, porque podemos convertir los objetos a texto en formato Json en el origen, "transmitir" el texto -que suele ser más fácil que "transmitir" objetos-, y luego crear nuevamente los objetos a partir del texto en forma Json en el destino. Cuando decimos "transmitir" nos referimos por ejemplo a pasar el objeto de memoria a disco, o desde un servidor a otro.

> Al proceso de convertir objetos a una representación que se puede transmitir se le llama **serialización**. El texto en formato Json es una representación que se puede transmitir, pero también puede ser texto en formato [XML](https://www.w3.org/XML/), o una representación binaria. Al proceso opuesto de crear objetos a patrir de la representación transmitida se le llama **deserialización**.

Usamos para el ejemplo la clase `Person` que ya hemos utilizado en ejercicios anteriores:

```c#
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
```

> [Ver en repositorio »](https://github.com/ucudal/PII_Person_Serialization/blob/master/v1/src/Library/Person.cs)

El método de clase `string.Deserialize<T>(T)` de la clase `JsonSerializer` es un método genérico en `T` donde `T` puede ser cualquier tipo. Esta declaración implica que cuando invoque ese método pasándo como parámetro del método genérico el tipo `Person` tendré que usar una instancia de `Person` como argumento:

```c#
Person person = new Person("Diego", "Lugano");
Console.WriteLine(JsonSerializer.Serialize<Person>(person);
```

El resultado de ejecutar el fragmento de código anterior imprimiría en la pantalla lo siguiente:

```bash
{"Name":"Diego","FamilyName":"Lugano"}
```

### Segunda parte

En la segunda parte de este ejemplo declaramos una interfaz para definir un tipo con operaciones para convertir cualquier objeto en texto en formato Json:

```c#
public interface IJsonConvertible
{
    string ConvertToJson();

    void LoadFromJson(string json);
}
```

Las clases que implementen esta interfaz utilizarán la clase `JsonSerializer` para convertir el objeto a texto en formato Json mediante la operación `string ConvertToJson()` y la operación `void LoadFromJson(string)` para asignar las propiedades del objeto a partir del texto en formato Json.

> Los objetos de las clases que implementen esa interfaz tendrán el tipo `IJsonConvertible` además del tipo definido por su clase.

Cambiaremos la clase `Person` para que implemente esa interfaz:

```c#
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
```

> [Ver en repositorio »](https://github.com/ucudal/PII_Person_Serialization/blob/master/v2/src/Library/Person.cs)

Como la clase Person implementa la interfaz `IJsonConvertible` debe implementar los métodos `string ConvertToJson()` y  `void LoadFromJson(string)` para las operaciones con la misma firma declaradas en la interfaz.

Por conveniencia, agregamos un constructor `Person(string)` que crea una instancia de `Person` a partir de la representación de ese objeto en formato Json. También por conveniencia declaramos que el constructor `Person(string, string)` que ya existía es el constructor que se utiliza para la serialización, agregando ` [JsonConstructor]` justo antes de la declaración de ese constructor.

Para obtener el texto en formato Json de una instancia de `Person` utilizamos el método `string ConvertToJson()`:

```c#
Person person = new Person("Diego", "Lugano");
Console.WriteLine(person.ConvertToJson());
```

El resultado de ejecutar el fragmento de código anterior es el mismo que antes:

```bash
{"Name":"Diego","FamilyName":"Lugano"}
```

Noten que como las instancias de `Person` también tienen el tipo `IJsonConvertible`, podemos escribir el código anterior así:

```c#
IJsonConvertible person = new Person("Diego", "Lugano");
Console.WriteLine(person.ConvertToJson());
```

Para crear una instancia de `Person` a partir del texto en formato Json utilizamos el nuevo constructor `Person(string)`:

```c#
Person person = new new Person("{\"Name\":\"Diego\",\"FamilyName\":\"Godín\"}")
Console.WriteLine(person.ConvertToJson());
```

> :warning: Noten que como el literal de texto en formato Json tiene el carácter `"` como parte del texto, y ese carácter es el mismo que se utiliza para delimitar las cadenas de texto, tenemos que utilizar `\"` para indicar que esos caracteres se consideren como parte del texto y no del literal.