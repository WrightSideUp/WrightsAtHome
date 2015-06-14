using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class DeviceStateConfiguration : EntityTypeConfiguration<DeviceState>
    {
        public DeviceStateConfiguration()
        {
            Property(ds => ds.Name).IsRequired().HasMaxLength(10);
            Property(ds => ds.Sequence).IsRequired();
            Property(ds => ds.IsTransitional).IsRequired();
            Property(e => e.LastModifiedUserId).IsRequired();
            Property(e => e.LastModified).IsRequired();

        }
    }
}