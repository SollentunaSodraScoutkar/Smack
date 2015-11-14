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
                attendance.DtmAttendanceDate = attendance.DtmAttendanceDate.Date;
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
                attendance.DtmAttendanceDate = attendance.DtmAttendanceDate.Date;
                string sql = "SELECT * FROM Attendance where intDivisionId = @intDivisionId and dtmAttendanceDate = @dtmAttendanceDate";
                existingAttendance = sqlConnection.Query<Attendance>(sql, attendance).SingleOrDefault();
            }
            return existingAttendance;
        }

        public void UpdateAttendance(Attendance attendance)
        {
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "UPDATE Attendance SET blnConfirmed = @blnConfirmed WHERE intAttendanceId = @intAttendanceId";
                sqlConnection.Execute(sql, attendance);
            }
        }
        public IEnumerable<MemberAttendance> GetMemberAttendanceByAttendanceId(int id)
        {
            IEnumerable<MemberAttendance> memberAttendance;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = @"SELECT a.intAttendanceId, m.intMemberID, m.chrFirstName, m.chrLastName, md.intDivisionID, ma.blnAttend FROM Member m 
                                    INNER JOIN MemberDivisions md on m.intMemberID = md.intMemberID 
                                    INNER JOIN Attendance a on md.intDivisionId = a.intDivisionId
                                    LEFT JOIN MemberAttendance ma on m.intMemberID = ma.intMemberId AND a.intAttendanceId = ma.intAttendanceId
                                        where a.intAttendanceId = @intAttendanceId and md.dtmStart < GetDate() and(md.dtmEnd is null or md.dtmEnd > GetDate()) and m.intStatus = 1";
                memberAttendance = sqlConnection.Query<MemberAttendance>(sql, new { IntAttendanceId = id });
            }
            return memberAttendance;
        }

        public void SaveMemberAttendance(MemberAttendance memberAttendance)
        {
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM MemberAttendance WHERE intMemberId = @intMemberId AND intAttendanceId = @intAttendanceId";
                var existing = sqlConnection.Query<MemberAttendance>(sql, memberAttendance).SingleOrDefault();
                if (existing == null)
                {
                    sql = "INSERT INTO MemberAttendance(intAttendanceId, intMemberId, blnAttend) VALUES (@intAttendanceId, @intMemberId, @blnAttend)";

                }
                else
                {
                    sql = "UPDATE MemberAttendance SET blnAttend = @blnAttend WHERE intMemberId = @intMemberId AND intAttendanceId = @intAttendanceId";
                }
                sqlConnection.Execute(sql, memberAttendance);
            }

        }
    }
}
