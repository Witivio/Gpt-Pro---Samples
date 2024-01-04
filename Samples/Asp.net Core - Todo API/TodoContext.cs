using Microsoft.EntityFrameworkCore;

namespace Asp.net_Core___Todo_API
{
    public class TodoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TodosDb");
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
