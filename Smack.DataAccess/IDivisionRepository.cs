
using System.Collections.Generic;
using Smack.Models;

namespace Smack.DataAccess
{
    public interface IDivisionRepository
    {
        IEnumerable<Division> GetAll();

        IEnumerable<Division> GetAllActive();
    }
}
