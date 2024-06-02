using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WealthZone.Models;

namespace WealthZone.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        //public MyContext(DbContextOptions<MyContext> options) : base(options)
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Stock> stocks { get; set; }
        public DbSet<Comment> comments  { get; set; }
    }
}
