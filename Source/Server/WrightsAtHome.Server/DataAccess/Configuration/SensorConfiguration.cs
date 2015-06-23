using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class SensorConfiguration : EntityTypeConfiguration<Sensor>
    {
        public SensorConfiguration()
        {
            Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_SensorName", 1) { IsUnique = true }));

            Property(s => s.Sequence)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_SensorSequence", 1) { IsUnique = true }));
            
            Property(s => s.ImageName)
                .IsRequired()
                .HasMaxLength(20);

            HasRequired(s => s.SensorType)
                .WithMany(st => st.Sensors)
                .Map(m => m.MapKey("SensorTypeId"));

            Property(e => e.LastModifiedUserId).IsRequired();
            Property(e => e.LastModified).IsRequired();

        }
    }
}