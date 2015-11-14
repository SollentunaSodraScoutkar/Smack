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

        public DivisionModule(IDivisionRepository divisionRepository) :base("smack/divisions")
        {
            _divisionRepository = divisionRepository;

            Get["/"] = x => GetAllDivisions(); 
        }

        private IEnumerable<Division> GetAllDivisions()
        {
            return _divisionRepository.GetAllActive();
        }
    }
}