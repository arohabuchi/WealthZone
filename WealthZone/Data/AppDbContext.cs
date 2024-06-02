using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WealthZone.Models;

namespace WealthZone.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser> 
    {
        //public MyContext(DbContextOptions<MyContext> options) : base(options)
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Portfolio> portfolios { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Comment> comments  { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ///has the id of both stockid and userid
            builder.Entity<Portfolio>(x => x.HasKey(p => new
            {
                p.AppUserId,
                p.StockId
            }));
            ///
            builder.Entity<Portfolio>()
                .HasOne(p => p.appUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(u => u.AppUserId);

            builder.Entity<Portfolio>()
               .HasOne(p => p.stock)
               .WithMany(u => u.Portfolios)
               .HasForeignKey(u => u.StockId);






            List<IdentityRole> roles= new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName="USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }



    }
}
