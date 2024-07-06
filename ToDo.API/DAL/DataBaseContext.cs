using Microsoft.EntityFrameworkCore;
using ToDo.API.DAL.Entities;

namespace ToDo.API.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }

        public DbSet<Todo> Todos { get; set; }
    }
}
