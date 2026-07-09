using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiJwtCors.Models;

namespace WebApiJwtCors.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "POC")] // change to "POC,Admin" to allow Admin role
public class EmployeeController : ControllerBase
{
    private static List<Employee> _employees = new()
    {
        new Employee { Id = 1, Name = "John", Salary = 50000, Permanent = true },
        new Employee { Id = 2, Name = "Jane", Salary = 60000, Permanent = false }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Employee>> Get()
    {
        return Ok(_employees);
    }

    [HttpPut("{id}")]
    public ActionResult<Employee> Put(int id, [FromBody] Employee emp)
    {
        if (id <= 0) return BadRequest("Invalid employee id");
        var existing = _employees.FirstOrDefault(e => e.Id == id);
        if (existing == null) return BadRequest("Invalid employee id");
        existing.Name = emp.Name;
        existing.Salary = emp.Salary;
        existing.Permanent = emp.Permanent;
        return Ok(existing);
    }

    [HttpPost]
    public ActionResult<Employee> Post([FromBody] Employee emp)
    {
        emp.Id = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
        _employees.Add(emp);
        return CreatedAtAction(nameof(Get), new { id = emp.Id }, emp);
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
