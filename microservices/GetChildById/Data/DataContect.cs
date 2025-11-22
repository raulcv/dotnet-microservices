using Microsoft.EntityFrameworkCore;
using GetChildById.Models;

namespace GetChildById.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Child> Child { get; set; }
    }
}