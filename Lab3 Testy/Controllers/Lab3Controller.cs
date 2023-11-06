using Lab3_Testy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private static List<Person> _persons = new List<Person>();
    private static int _idCounter = 1;

    // Pobranie wszystkich os�b
    [HttpGet]
    public ActionResult<IEnumerable<Person>> Get()
    {
        return _persons;
    }

    // Pobranie osoby po Id
    [HttpGet("{id}")]
    public ActionResult<Person> Get(int id)
    {
        var person = _persons.FirstOrDefault(p => p.Id == id);
        if (person == null)
        {
            return NotFound();
        }
        return person;
    }

    // Dodanie nowej osoby
    [HttpPost]
    public ActionResult<Person> Post([FromBody] Person person)
    {
        // Sprawdzenie unikalno�ci Id
        if (_persons.Any(p => p.Id == person.Id))
        {
            return Conflict("Id musi byc unikatowe.");
        }

        // Sprawdzenie unikalno�ci FirstName i LastName
        if (_persons.Any(p => p.FirstName == person.FirstName && p.LastName == person.LastName))
        {
            return Conflict("Imie i Nazwisko musi byc unikatowe.");
        }

        // Je�li dane s� unikalne, dodaj osob�
        person.Id = _idCounter++;
        _persons.Add(person);
        return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
    }

    // Aktualizacja osoby
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Person updatedPerson)
    {
        var person = _persons.FirstOrDefault(p => p.Id == id);
        if (person == null)
        {
            return NotFound();
        }

        // Sprawdzenie unikalno�ci FirstName i LastName
        if (_persons.Any(p => p.Id != id && p.FirstName == updatedPerson.FirstName && p.LastName == updatedPerson.LastName))
        {
            return Conflict("Imie i Nazwisko musi byc unikatowe.");
        }

        // Je�li dane s� unikalne, dokonaj aktualizacji
        person.FirstName = updatedPerson.FirstName;
        person.LastName = updatedPerson.LastName;

        return NoContent();
    }

    // Usuni�cie osoby
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var person = _persons.FirstOrDefault(p => p.Id == id);
        if (person == null)
        {
            return NotFound();
        }

        _persons.Remove(person);
        return NoContent();
    }
}
