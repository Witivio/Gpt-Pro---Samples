namespace Asp.net_Core___Todo_API
{
    public class Todo
    {
        public int Id { get; set; }  
        public string Title { get; set; }   
        public string Description { get; set; }  
        public bool IsDone { get; set; } 
                                        

        // Constructeur avec initialisation des propriétés
        public Todo(int id, string title, string description = "", bool isDone = false)
        {
            Id = id;
            Title = title;
            Description = description;
            IsDone = isDone;
        }
    }
}
