using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smack.Models;
using Dapper;

namespace Smack.DataAccess
{
    public class AttendanceRepository : RepositoryBase, IAttendanceRepository
    {
        public int CreateAttendance(Attendance attendance)
        {
            int attendanceId;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "INSERT INTO Attendance (varName, dtmAttendanceDate, intDivisionId, blnConfirmed) VALUES (@varName, @dtmAttendanceDate, @intDivisionId, @blnConfirmed); SELECT @@IDENTITY";
                attendanceId = sqlConnection.Query<int>(sql, attendance).Single();
            }
            return attendanceId;
        }

        public Attendance GetAttendance(Attendance attendance)
        {
            Attendance existingAttendance;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM Attendance where intDivisionId = @intDivisionId and dtmAttendanceDate = @dtmAttendanceDate";
                existingAttendance = sqlConnection.Query<Attendance>(sql, attendance).SingleOrDefault();
            }
            return existingAttendance;
        }

        public IEnumerable<MemberAttendance> GetMemberAttendanceByDivisionId(int intDivisionId)
        {
            IEnumerable<MemberAttendance> memberAttendance;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT m.intMemberID, m.chrFirstName, m.chrLastName, md.intDivisionID, ma.blnAttend FROM Member m LEFT JOIN MemberDivisions md on m.intMemberID = md.intMemberID LEFT JOIN MemberAttendance ma on m.intMemberID = ma.intMemberId where intDivisionId = @intDivisionId and dtmStart < GetDate() and(dtmEnd is null or dtmEnd > GetDate()) and intStatus = 1";
                memberAttendance = sqlConnection.Query<MemberAttendance>(sql, new { IntDivisionId = intDivisionId });
            }
            return memberAttendance;
        }
    }
}
