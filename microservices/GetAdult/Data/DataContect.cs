using Microsoft.EntityFrameworkCore;
using GetAdult.Models;

namespace GetAdult.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Adult> Adult { get; set; }
    }
}