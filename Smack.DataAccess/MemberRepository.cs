using System;
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

        public IEnumerable<Member> GetFromDivision(int intDivisionId)
        {
            IEnumerable<Member> members;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM Member WHERE intMemberID IN (SELECT intMemberID FROM MemberDivisions where intDivisionId = @intDivisionId and dtmStart < GetDate() and (dtmEnd is null or dtmEnd > GetDate())) and intStatus = 1";
                members = sqlConnection.Query<Member>(sql, new {IntDivisionId = intDivisionId});
            }
            return members;
        }

        public void Save(Member member)
        {
            throw new NotImplementedException("Fix SQL");
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "INSERT INTO Member () VALUES (IntFirstName=@intFirstName...)";
                sqlConnection.Execute(sql, member);
            }
        }
    }
}
