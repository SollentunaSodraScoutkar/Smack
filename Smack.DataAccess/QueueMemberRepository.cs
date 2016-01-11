using System;
using System.Linq;
using Dapper;
using Smack.Models;

namespace Smack.DataAccess
{
    public class QueueMemberRepository : RepositoryBase, IQueueMemberRepository
    {
        public int Add(QueueMember queueMember)
        {
            int id;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "INSERT INTO QueueMemnber (chrFirstName, chrLastName) VALUES (@chrFirstName, @chrLastName); SELECT @@IDENTITY";
                id = sqlConnection.Query<int>(sql, queueMember).Single();
            }
            return id;
        }
    }

    public interface IQueueMemberRepository
    {
        int Add(QueueMember queueMember);
    }
}
