using Microsoft.AspNetCore.Mvc;
using WebApiWithSwagger.Models;

namespace WebApiWithSwagger.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private static readonly List<Employee> Employees = new()
    {
        new Employee { Id = 1, Name = "Alice", Position = "Manager", Salary = 80000m },
        new Employee { Id = 2, Name = "Bob", Position = "Clerk", Salary = 30000m }
    };

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Employee>> Get() => Ok(Employees);

    [HttpGet("{id}", Name = "GetEmployee")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Employee> GetById(int id)
    {
        var e = Employees.FirstOrDefault(x => x.Id == id);
        if (e == null) return NotFound();
        return Ok(e);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Create(Employee emp)
    {
        if (emp == null) return BadRequest();
        emp.Id = Employees.Any() ? Employees.Max(x => x.Id) + 1 : 1;
        Employees.Add(emp);
        return CreatedAtRoute("GetEmployee", new { id = emp.Id }, emp);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Employee emp)
    {
        var existing = Employees.FirstOrDefault(x => x.Id == id);
        if (existing == null) return NotFound();
        existing.Name = emp.Name;
        existing.Position = emp.Position;
        existing.Salary = emp.Salary;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = Employees.FirstOrDefault(x => x.Id == id);
        if (existing == null) return NotFound();
        Employees.Remove(existing);
        return NoContent();
    }

    // Example demonstrating ActionName to allow another GET with same verb
    [HttpGet("search")]
    [ActionName("Search")]
    public ActionResult<Employee?> SearchByName([FromQuery] string name)
    {
        var emp = Employees.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (emp == null) return NotFound();
        return Ok(emp);
    }
}
