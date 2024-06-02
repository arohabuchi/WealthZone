using WealthZone.Models;

namespace WealthZone.Data.Interface
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
