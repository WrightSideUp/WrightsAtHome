using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class AtHomeDbConfiguration : DbConfiguration
    {
        public AtHomeDbConfiguration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory());
        }
    }
}