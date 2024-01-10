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
            return Ok(_todoRepository.GetTodos());
        }

        /// <summary>
        /// Add or update to the todo list
        /// </summary>
        /// <param name="todo">the todo to add or update</param>
        [HttpPost(Name = "AddOrUpdateTodo")]
        public IActionResult AddOrUpdateTodo([FromBody] Todo todo)
        {
            if (todo == null) return BadRequest();

            var updatedTodo = _todoRepository.GetTodo(todo.Id);

            if(todo.Id == 0 || updatedTodo == null)
            {
                _todoRepository.CreateTodo(todo);
            }
            else
            {
                _todoRepository.UpdateTodo(todo);
            }

            return Ok(todo);
        }
    }
}
