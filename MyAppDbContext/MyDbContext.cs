using Microsoft.EntityFrameworkCore;
using ToDo_App_Backend.Models;

namespace ToDo_App_Backend.MyAppDbContext

{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) 
        {
            
        }
        public DbSet<ToDo> ToDos { get; set; }
    }
}
