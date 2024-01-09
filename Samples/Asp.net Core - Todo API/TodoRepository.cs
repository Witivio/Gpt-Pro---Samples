namespace Asp.net_Core___Todo_API
{
    public class TodoRepository : ITodoRepository
    {
        public TodoRepository()
        {
            using (var context = new TodoContext())
            {
                var a = context.Todos.ToList();
            }
        }

        public List<Todo> GetTodos()
        {
            using (var context = new TodoContext())
            {
                return context.Todos.Where(e => e.IsDone == false).ToList();
            }
        }

        public Todo GetTodo(int id)
        {
            using (var context = new TodoContext())
            {
                return context.Todos.SingleOrDefault(e => e.Id == id);
            }
        }

        public void CreateTodo(Todo todo)
        {
            using (var context = new TodoContext())
            {
                context.Todos.Add(todo);
                context.SaveChanges();
            }
        }

        public void UpdateTodo(Todo todo)
        {
            using (var context = new TodoContext())
            {
                context.Todos.Update(todo);
                context.SaveChanges();
            }
        }
    }

    public interface ITodoRepository
    {
        List<Todo> GetTodos();

        Todo GetTodo(int id);

        void CreateTodo(Todo todo);

        void UpdateTodo(Todo todo);
    }
}
