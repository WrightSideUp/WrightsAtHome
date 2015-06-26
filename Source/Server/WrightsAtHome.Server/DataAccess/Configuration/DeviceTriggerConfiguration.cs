using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class DeviceTriggerConfiguration : EntityTypeConfiguration<DeviceTrigger>
    {
        public DeviceTriggerConfiguration()
        {
            HasRequired(dt => dt.Device)
                .WithMany(d => d.Triggers)
                .HasForeignKey(t => t.DeviceId);

            HasRequired(dt => dt.ToState)
                .WithMany()
                .HasForeignKey(dt => dt.ToStateId);

            Property(d => d.ToStateId).HasColumnName("DeviceStateId");

            Property(d => d.TriggerText).HasMaxLength(1024).IsRequired();
            Property(d => d.Sequence).IsRequired();
            Property(d => d.IsActive).IsRequired();
            Property(e => e.LastModifiedUserId).IsRequired();
            Property(e => e.LastModified).IsRequired();
        }
    }
}