using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            Property(l => l.Timestamp).IsRequired();
            Property(l => l.Message).HasMaxLength(1024);
            Property(l => l.Exception).HasMaxLength(512);
            Property(l => l.Level).HasMaxLength(10);
            Property(l => l.Logger).HasMaxLength(128);
        }
    }
}