using Microsoft.AspNetCore.Mvc;
using WebApiCustomModel.Models;
using WebApiCustomModel.Filters;

namespace WebApiCustomModel.Controllers;

[ApiController]
[Route("api/[controller]")]
[CustomAuthFilter]
public class EmployeeController : ControllerBase
{
    private static List<Employee> _employees = GetStandardEmployeeList();

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<Employee>> Get([FromQuery] bool throwError = false)
    {
        if (throwError) throw new Exception("Demonstration exception");
        return Ok(_employees);
    }

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
    public IActionResult Put(int id, [FromBody] Employee emp)
    {
        var existing = _employees.FirstOrDefault(e => e.Id == id);
        if (existing == null) return NotFound();
        existing.Name = emp.Name;
        existing.Salary = emp.Salary;
        existing.Permanent = emp.Permanent;
        existing.Department = emp.Department;
        existing.Skills = emp.Skills;
        existing.DateOfBirth = emp.DateOfBirth;
        return NoContent();
    }

    private static List<Employee> GetStandardEmployeeList()
    {
        return new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "John",
                Salary = 50000,
                Permanent = true,
                Department = new Department { Id = 1, Name = "HR" },
                Skills = new List<Skill> { new Skill { Name = "Communication", Level = 3 } },
                DateOfBirth = new DateTime(1990,1,1)
            },
            new Employee
            {
                Id = 2,
                Name = "Jane",
                Salary = 60000,
                Permanent = false,
                Department = new Department { Id = 2, Name = "IT" },
                Skills = new List<Skill> { new Skill { Name = "C#", Level = 4 } },
                DateOfBirth = new DateTime(1992,5,23)
            }
        };
    }
}
