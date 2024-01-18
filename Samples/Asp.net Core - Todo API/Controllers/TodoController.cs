using Microsoft.AspNetCore.Mvc;

namespace Asp.net_Core___Todo_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        /// <summary>
        /// Returns a Todo list
        /// </summary>
        /// <returns>A Todo list</returns>
        [HttpGet(Name = "GetTodos")]
        public IActionResult GetTodos()
        {
            return Ok(_todoRepository.GetAll());
        }

        /// <summary>
        /// Update a task to the todo list
        /// </summary>
        /// <param name="todo">the todo to add or update</param>
        [HttpPut(Name = "DeleteTodo")]
        public IActionResult DeleteTodo([FromQuery] string todo)
        {
            if (string.IsNullOrEmpty(todo)) return BadRequest("the given todo is missing");

            try
            {
                _todoRepository.Delete(todo);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Add a task to the todo list
        /// </summary>
        /// <param name="todo">the todo to add or update</param>
        [HttpPost(Name = "AddTodo")]
        public IActionResult AddTodo([FromQuery] string todo)
        {
            if (todo == null) return BadRequest("the given todo is missing");

            _todoRepository.Create(todo);
            return Created();
        }
    }
}
