using Microsoft.AspNetCore.Mvc;
using WebApiCrud.Models;

namespace WebApiCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private static List<Employee> _employees = new()
    {
        new Employee { Id = 1, Name = "John", Salary = 50000, Permanent = true },
        new Employee { Id = 2, Name = "Jane", Salary = 60000, Permanent = false }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Employee>> Get() => Ok(_employees);

    [HttpGet("{id}")]
    public ActionResult<Employee> Get(int id)
    {
        var emp = _employees.FirstOrDefault(e => e.Id == id);
        if (emp == null) return NotFound();
        return Ok(emp);
    }

    [HttpPost]
    public ActionResult<Employee> Post([FromBody] Employee emp)
    {
        if (emp == null) return BadRequest();
        emp.Id = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
        _employees.Add(emp);
        return CreatedAtAction(nameof(Get), new { id = emp.Id }, emp);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> Put(int id, [FromBody] Employee emp)
    {
        if (id <= 0) return BadRequest("Invalid employee id");
        var existing = _employees.FirstOrDefault(e => e.Id == id);
        if (existing == null) return BadRequest("Invalid employee id");
        // Update hardcoded list
        existing.Name = emp.Name;
        existing.Salary = emp.Salary;
        existing.Permanent = emp.Permanent;
        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = _employees.FirstOrDefault(e => e.Id == id);
        if (existing == null) return NotFound();
        _employees.Remove(existing);
        return NoContent();
    }
}
