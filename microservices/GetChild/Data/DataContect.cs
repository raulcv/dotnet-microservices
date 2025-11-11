using Microsoft.EntityFrameworkCore;
using GetChild.Models;

namespace GetChild.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Child> Child { get; set; }
    }
}