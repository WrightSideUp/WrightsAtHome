using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class SensorTypeConfiguration : EntityTypeConfiguration<SensorType>
    {
        public SensorTypeConfiguration()
        {
            Property(st => st.Name)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_SensorTypeName", 1) { IsUnique = true }));


            Property(e => e.LastModifiedUserId).IsRequired();
            Property(e => e.LastModified).IsRequired();
        }
    }
}