using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lab3_Testy
{
    public interface IPersonController
    {
        ActionResult<IEnumerable<Person>> Get();
        ActionResult<Person> Get(int id);
        ActionResult<Person> Post([FromBody] Person person);
        IActionResult Put(int id, [FromBody] Person updatedPerson);
        IActionResult Delete(int id);
    }
}
