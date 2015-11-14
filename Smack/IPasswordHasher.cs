using Smack.Models;

namespace Smack
{
    public interface IPasswordHasher
    {
        string CreateHash(string password);
        bool ValidatePassword(User password, User goodHash);
        string CreateToken();
    }
}
