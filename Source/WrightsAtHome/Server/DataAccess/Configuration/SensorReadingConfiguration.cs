using System.Data.Entity.ModelConfiguration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Configuration
{
    public class SensorReadingConfiguration : EntityTypeConfiguration<SensorReading>
    {
        public SensorReadingConfiguration()
        {
            HasRequired(sr => sr.ReadingSensor)
                .WithMany(s => s.Readings)
                .Map(m => m.MapKey("SensorId"))
                .WillCascadeOnDelete();

            Property(s => s.ReadingDate).IsRequired();

            Property(s => s.Value).IsRequired();
        }
    }
}