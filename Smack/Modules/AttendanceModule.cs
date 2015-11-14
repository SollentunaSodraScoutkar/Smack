using Nancy.ModelBinding;
using Smack.DataAccess;
using Smack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smack.Modules
{
    public class AttendanceModule : SecureModule
    {
        private IAttendanceRepository _attendanceRepository { get; set; }

        public AttendanceModule(IAttendanceRepository attendanceRepository) : base("smack/attendance")
        {
            _attendanceRepository = attendanceRepository;

            Post["/"] = x => GetAttendance();
            Get["/{divisionId}/members"] = x => GetMemberAttendanceByDivisionId(x.divisionId);
        }

        private Attendance GetAttendance()
        {
            Attendance attendance = this.Bind<Attendance>(); 
            Attendance existingAttendance = _attendanceRepository.GetAttendance(attendance);
            if (existingAttendance != null)
            {
                return existingAttendance;
            }
            attendance.IntAttendanceId = _attendanceRepository.CreateAttendance(attendance);
            return attendance;
        }

        private IEnumerable<MemberAttendance> GetMemberAttendanceByDivisionId(int divisionId)
        {
            return _attendanceRepository.GetMemberAttendanceByDivisionId(divisionId);
        }

    }
}