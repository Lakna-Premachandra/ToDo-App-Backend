using Microsoft.EntityFrameworkCore;

namespace ToDo_App_Backend.MyDbContext

{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) 
        {
            
        }
        public DbSet<ToDo> ToDos { get; set; }
    }
}
