# Helix
In memory .NET 6 database with disk persistence

## Example

```
using Example;

Helix<List<Person>>.Register();

var persons = Helix<List<Person>>.Get();

var p1 = new Person()
{
    Name = "Person1",
    Age = 50
};

var p2 = new Person()
{
    Name = "Person2",
    Age = 100
};

persons.Add(p1);
persons.Add(p2);

Helix<List<Person>>.Persist();

namespace Example
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
```
