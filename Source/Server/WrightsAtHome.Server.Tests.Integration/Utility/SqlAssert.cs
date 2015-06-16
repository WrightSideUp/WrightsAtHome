
using System.Linq;
using WrightsAtHome.Server.DataAccess;

namespace WrightsAtHome.Tests.Integration.Utility
{
    public class SqlAssert
    {
        public static bool Count(string sql, int expected)
        {
            using (var ctx = new AtHomeDbContext())
            {
                var value = ctx.Database.SqlQuery<int>(sql).ToList()[0];

                return value == expected;
            }
        }
    }
}
