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
        }
    }
}