using DatingAppBackend.Entities;

namespace DatingAppBackend.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
