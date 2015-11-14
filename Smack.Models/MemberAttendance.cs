using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smack.Models
{
    public class MemberAttendance
    {
        public int IntMemberId { get; set; }
        public string ChrFirstName { get; set; }
        public string ChrLastName { get; set; }
        public int IntDivisionId { get; set; }
        public bool? BlnAttend { get; set; }
    }
}
