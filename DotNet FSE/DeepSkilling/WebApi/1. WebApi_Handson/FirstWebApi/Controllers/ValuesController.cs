using Microsoft.AspNetCore.Mvc;
using FirstWebApi.Models;

namespace FirstWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private static readonly List<ValueItem> Items = new()
    {
        new ValueItem { Id = 1, Name = "Value1" },
        new ValueItem { Id = 2, Name = "Value2" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<ValueItem>> Get() => Ok(Items);

    [HttpGet("{id}")]
    public ActionResult<ValueItem> Get(int id)
    {
        var item = Items.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public ActionResult<ValueItem> Post(ValueItem value)
    {
        var id = Items.Any() ? Items.Max(i => i.Id) + 1 : 1;
        value.Id = id;
        Items.Add(value);
        return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, ValueItem value)
    {
        var item = Items.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound();
        item.Name = value.Name;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = Items.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound();
        Items.Remove(item);
        return NoContent();
    }
}
