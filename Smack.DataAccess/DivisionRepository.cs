using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smack.Models;
using Dapper;

namespace Smack.DataAccess
{
    public class DivisionRepository : RepositoryBase, IDivisionRepository
    {
        public IEnumerable<Division> GetAll()
        {
            IEnumerable<Division> division;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM Division order by intType, varName";
                division = sqlConnection.Query<Division>(sql);
            }
            return division;
        }
        public IEnumerable<Division> GetAllActive()
        {
            IEnumerable<Division> division;
            using (var sqlConnection = GetSqlConnection())
            {
                string sql = "SELECT * FROM Division where blnActive = 1 order by intType, varName";
                division = sqlConnection.Query<Division>(sql);
            }
            return division;
        }
    }
}
