using Microsoft.AspNetCore.Identity;

namespace WealthZone.Models
{
    public class ApplicationUser:IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
