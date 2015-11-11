using Smack.Models;

namespace Smack.DataAccess
{
    public interface IUserRepository
    {
        User GetById(int userId);
        User GetByEmail(string email);
        User GetByUserName(string varUserName);
    }
}