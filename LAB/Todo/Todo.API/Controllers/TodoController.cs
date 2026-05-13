using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<List<Infrastructure.Todo>>> GetAll()
        {
            try
            {
                var todos = await _todoService.GetAll();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving todos", error = ex.Message });
            }
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Infrastructure.Todo>> GetById(int id)
        {
            try
            {
                var todo = await _todoService.GetById(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todo with ID {id} not found" });
                }
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving todo", error = ex.Message });
            }
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Infrastructure.Todo todo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(todo.Title))
                {
                    return BadRequest(new { message = "Title is required" });
                }

                await _todoService.Add(todo);
                return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating todo", error = ex.Message });
            }
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Infrastructure.Todo todo)
        {
            try
            {
                if (id != todo.Id)
                {
                    return BadRequest(new { message = "ID mismatch" });
                }

                if (string.IsNullOrWhiteSpace(todo.Title))
                {
                    return BadRequest(new { message = "Title is required" });
                }

                var existingTodo = await _todoService.GetById(id);
                if (existingTodo == null)
                {
                    return NotFound(new { message = $"Todo with ID {id} not found" });
                }

                await _todoService.Update(todo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating todo", error = ex.Message });
            }
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var todo = await _todoService.GetById(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todo with ID {id} not found" });
                }

                await _todoService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting todo", error = ex.Message });
            }
        }
    }
}
