using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smack.Models
{
    public class Attendance
    {
        public int IntAttendanceId { get; set; }
        public string VarName { get; set; }
        public DateTime DtmAttendanceDate { get; set; }
        public int IntDivisionId { get; set; }
        public bool BlnConfirmed { get; set; }
    }
}
