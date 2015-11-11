using System.Collections.Generic;
using System.Linq;
using Dapper;
using Smack.Models;

namespace Smack.DataAccess
{
    public class MemberRepository : RepositoryBase, IMemberRepository
    {
        public IEnumerable<Member> GetAll()
        {
            IEnumerable<Member> members;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM Member";
                members = sqlConnection.Query<Member>(sql);
            }
            return members;
        }
    }
}
