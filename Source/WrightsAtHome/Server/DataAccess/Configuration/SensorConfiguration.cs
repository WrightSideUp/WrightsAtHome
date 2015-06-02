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
                .HasMaxLength(30);

            Property(s => s.ImageName)
                .IsRequired()
                .HasMaxLength(20);

            HasRequired(s => s.SensorType)
                .WithMany(st => st.Sensors)
                .Map(m => m.MapKey("SensorTypeId"));

        }
    }
}