using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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
                .HasMaxLength(30)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_DeviceName", 1) { IsUnique = true }));

            Property(d => d.Sequence)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_DeviceSequence", 1) { IsUnique = true }));

            Property(d => d.ImageName)
                .IsRequired()
                .HasMaxLength(20);

            Property(d => d.LastModified).IsRequired();

            Property(d => d.LastModifiedUserId).IsRequired();
        }
    }
}