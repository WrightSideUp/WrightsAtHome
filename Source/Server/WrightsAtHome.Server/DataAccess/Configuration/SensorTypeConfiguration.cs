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
                .HasMaxLength(20);

            Property(e => e.LastModifiedUserId).IsRequired();
            Property(e => e.LastModified).IsRequired();
        }
    }
}