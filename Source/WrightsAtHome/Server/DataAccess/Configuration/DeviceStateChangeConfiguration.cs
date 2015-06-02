using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class DeviceStateChangeConfiguration : EntityTypeConfiguration<DeviceStateChange>
    {
        public DeviceStateChangeConfiguration()
        {
            Property(sc => sc.AppliedDate).IsRequired();
            Property(sc => sc.WasOverride).IsRequired();

            HasRequired(sc => sc.Device)
                .WithMany(sc => sc.StateChanges)
                .Map(m => m.MapKey("DeviceId"));

            HasRequired(sc => sc.BeforeState)
                .WithMany()
                .Map(m => m.MapKey("BeforeStateId"))
                .WillCascadeOnDelete(false); 

            HasRequired(sc => sc.AfterState)
                .WithMany()
                .Map(m => m.MapKey("AfterStateId"))
                .WillCascadeOnDelete(false);

        }
    }
}