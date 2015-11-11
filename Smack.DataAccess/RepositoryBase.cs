using System.Configuration;
using System.Data.SqlClient;

namespace Smack.DataAccess
{
    public class RepositoryBase
    {
        protected static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        }

    }
}