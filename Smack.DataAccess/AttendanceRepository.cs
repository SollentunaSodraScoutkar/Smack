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
    }
}
