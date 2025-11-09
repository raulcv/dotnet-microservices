using Microsoft.EntityFrameworkCore;
using ApiGlobal.Models;

namespace ApiGlobal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Adult> Adult { get; set; }

        public DbSet<Child> Child { get; set; }
    }
}