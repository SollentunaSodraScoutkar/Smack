using System.Linq;
using Dapper;
using Smack.Models;

namespace Smack.DataAccess
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public User GetById(int userId)
        {
            User user;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM SmedUser WHERE intUserId = @userId";
                user = sqlConnection.Query<User>(sql, new {UserId = userId}).SingleOrDefault();
            }
            return user;
        }

        public User GetByEmail(string email)
        {
            User user;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM SmedUser WHERE varEmail = @email";
                user = sqlConnection.Query<User>(sql, new { Email = email }).SingleOrDefault();
            }
            return user;
        }

        public User GetByUserName(string varUserName)
        {
            User user;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM SmedUser WHERE VarUserName = @varUserName";
                user = sqlConnection.Query<User>(sql, new { VarUserName = varUserName }).SingleOrDefault();
            }
            return user;
        }
    }
}
