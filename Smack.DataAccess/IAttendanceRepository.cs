using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smack.Models;

namespace Smack.DataAccess
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(Attendance attendance);
        int CreateAttendance(Attendance attendance);
        IEnumerable<MemberAttendance> GetMemberAttendanceByAttendanceId(int id);
        void SaveMemberAttendance(MemberAttendance memberAttendance);
    }
}
