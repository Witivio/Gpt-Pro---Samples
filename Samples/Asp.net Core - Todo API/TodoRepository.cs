using Microsoft.AspNetCore.Http.HttpResults;

namespace Asp.net_Core___Todo_API
{
    public class TodoRepository : ITodoRepository
    {
        public List<Todo> GetAll()
        {
            using (var context = new TodoContext())
            {
                return context.Todos.ToList();
            }
        }

        public void Create(string todo)
        {
            using (var context = new TodoContext())
            {
                context.Todos.Add(new Todo(todo));
                context.SaveChanges();
            }
        }

        public void Delete(string todo)
        {
            using (var context = new TodoContext())
            {
                var todoInBase = context.Todos.SingleOrDefault(e => e.todo == todo);

                if (todoInBase == null) throw new Exception("the todo don't existe in base");

                context.Todos.Remove(todoInBase);
                context.SaveChanges();
            }
        }
    }

    public interface ITodoRepository
    {
        List<Todo> GetAll();
        void Create(string todo);
        void Delete(string todo);
    }
}
