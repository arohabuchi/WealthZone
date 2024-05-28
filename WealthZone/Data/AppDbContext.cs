using Microsoft.EntityFrameworkCore;
using WealthZone.Models;

namespace WealthZone.Data
{
    public class AppDbContext:DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Stock> stocks { get; set; }
        public DbSet<Comment> comments  { get; set; }
    }
}
