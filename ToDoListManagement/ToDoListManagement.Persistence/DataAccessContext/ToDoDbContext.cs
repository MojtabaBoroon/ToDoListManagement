using Microsoft.EntityFrameworkCore;
using ToDoListManagement.Domain.ToDoItems;

namespace ToDoListManagement.Persistence.DataAccessContext
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options): base(options)
        {

        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog =ToDoDb;Integrated Security=True;Encrypt=False");
        }
    }
}
