using Nancy;
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
            Get["/{attendanceId}/members"] = x => GetMemberAttendanceById(x.attendanceId);
            Put["/members"] = x => SaveMemberAttendance();
        }

        private HttpStatusCode SaveMemberAttendance()
        {
            var memberAttendance = this.Bind<MemberAttendance>();
            _attendanceRepository.SaveMemberAttendance(memberAttendance);
            return HttpStatusCode.OK;
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

        private IEnumerable<MemberAttendance> GetMemberAttendanceById(int attendanceId)
        {
            return _attendanceRepository.GetMemberAttendanceByAttendanceId(attendanceId);
        }

    }
}