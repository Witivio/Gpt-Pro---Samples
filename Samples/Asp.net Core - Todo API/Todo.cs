namespace Asp.net_Core___Todo_API
{
    public class Todo
    {
        public Todo(string todo)
        {
            this.todo = todo;
        }

        public int id { get; set; }
        public string todo { get; set; }
    }
}
