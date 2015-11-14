using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smack.DataAccess;
using Smack.Models;

namespace Smack.Modules
{
    public class DivisionModule :SecureModule
    {
        private IDivisionRepository _divisionRepository;
        private IMemberRepository _memberRepository;

        public DivisionModule(IDivisionRepository divisionRepository, IMemberRepository memberRepository) :base("smack/divisions")
        {
            _divisionRepository = divisionRepository;
            _memberRepository = memberRepository;

            Get["/"] = x => GetAllDivisions();
            Get["/{id}/members"] = x => GetMembers(x.id);
         }

        private IEnumerable<Division> GetAllDivisions()
        {
            return _divisionRepository.GetAllActive();
        }

        private IEnumerable<Member> GetMembers(int intDivisionId)
        {
            return _memberRepository.GetFromDivision(intDivisionId);
        }
    }
}