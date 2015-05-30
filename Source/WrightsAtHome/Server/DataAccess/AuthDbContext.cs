using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WrightsAtHome.Server.DataAccess
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext() : base("AuthContext")
        {
        }
    }
}