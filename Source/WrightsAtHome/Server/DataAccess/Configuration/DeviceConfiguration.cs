using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class DeviceConfiguration : EntityTypeConfiguration<Device>
    {
        public DeviceConfiguration()
        {
            HasMany(d => d.PossibleStates)
                .WithMany(ds => ds.Devices)
                .Map(ps =>
                {
                    ps.MapLeftKey("DeviceId");
                    ps.MapRightKey("DeviceStateId");
                    ps.ToTable("DeviceDeviceStateXref");
                });

            Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(30);

            Property(d => d.ImageName)
                .IsRequired()
                .HasMaxLength(20);

            Property(d => d.StartTriggerText).HasMaxLength(512);

            Property(d => d.EndTriggerText).HasMaxLength(512);
        }
    }
}