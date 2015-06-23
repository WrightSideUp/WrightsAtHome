using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class DeviceStateConfiguration : EntityTypeConfiguration<DeviceState>
    {
        public DeviceStateConfiguration()
        {
            Property(ds => ds.Name).IsRequired()
                .HasMaxLength(10)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_DeviceStateName", 1) { IsUnique = true }));
            
            Property(ds => ds.Sequence)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_DeviceStateSequence", 1) { IsUnique = true }));

            Property(ds => ds.IsTransitional).IsRequired();
            Property(e => e.LastModifiedUserId).IsRequired();
            Property(e => e.LastModified).IsRequired();

        }
    }
}