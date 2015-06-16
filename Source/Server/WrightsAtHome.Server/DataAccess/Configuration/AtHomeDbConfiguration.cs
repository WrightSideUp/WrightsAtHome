using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class AtHomeDbConfiguration : DbConfiguration
    {
        public AtHomeDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            SetDefaultConnectionFactory(new SqlConnectionFactory());
        }
    }
}