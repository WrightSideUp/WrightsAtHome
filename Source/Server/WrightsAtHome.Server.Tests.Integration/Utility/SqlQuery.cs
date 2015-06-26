using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WrightsAtHome.Server.DataAccess;

namespace WrightsAtHome.Server.Tests.Integration.Utility
{
    public static class SqlQuery
    {
        public static T GetValue<T>(string sql)
        {
            using (var ctx = new AtHomeDbContext())
            {
                return ctx.Database.SqlQuery<T>(sql).ToList()[0];
            }
        }

        public static DataTable GetDataTable(string sql)
        {
            DataTable t = new DataTable();
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AtHomeDbContext"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    using (var reader = cmd.ExecuteReader())
                    {
                        t.Load(reader);
                    }
                }
            }

            return t;
        }

        public static int Execute(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AtHomeDbContext"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
