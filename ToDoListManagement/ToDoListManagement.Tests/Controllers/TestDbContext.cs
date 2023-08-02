using Microsoft.EntityFrameworkCore;
using ToDoListManagement.Domain.ToDoItems;

namespace ToDoListManagement.Tests.Controllers
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
