using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class DeviceTriggerConfiguration : EntityTypeConfiguration<DeviceTrigger>
    {
        public DeviceTriggerConfiguration()
        {
            HasRequired(dt => dt.Device)
                .WithMany(dv => dv.Triggers)
                .Map(m => m.MapKey("DeviceId"));

            HasRequired(dt => dt.ToState)
                .WithMany()
                .Map(m => m.MapKey("DeviceStateId"));

            Property(d => d.TriggerText).HasMaxLength(1024).IsRequired();
            Property(d => d.Sequence).IsRequired();
            Property(d => d.IsActive).IsRequired();
            Property(e => e.LastModifiedUserId).IsRequired();
            Property(e => e.LastModified).IsRequired();
        }
    }
}