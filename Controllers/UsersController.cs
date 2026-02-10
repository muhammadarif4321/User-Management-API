using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // Data sementara (tanpa database)
    private static List<User> users = new List<User>();
    private static int nextId = 1;

    // ================= GET ALL =================
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return Ok(users);
    }

    // ================= GET BY ID =================
    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // ================= POST =================
    [HttpPost]
    public ActionResult<User> Create(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name) ||
            string.IsNullOrWhiteSpace(user.Email))
        {
            return BadRequest("Name and Email are required");
        }

        user.Id = nextId++;
        users.Add(user);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    // ================= PUT =================
    [HttpPut("{id}")]
    public IActionResult Update(int id, User updatedUser)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;

        return NoContent();
    }

    // ================= DELETE =================
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        users.Remove(user);
        return NoContent();
    }
}
