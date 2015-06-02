using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WrightsAtHome.Server.DataAccess.Configuration;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess
{
    public interface IAtHomeDbContext
    {
        IDbSet<Device> Devices { get; set; }

        IDbSet<DeviceState> DeviceStates { get; set; }

        IDbSet<DeviceStateChange> DeviceStateChanges { get; set; }

        IDbSet<SensorType> SensorTypes { get; set; }

        IDbSet<Sensor> Sensors { get; set; }

        IDbSet<SensorReading> SensorReadings { get; set; }

        int SaveChanges();
    }
    
    public class AtHomeDbContext : DbContext, IAtHomeDbContext
    {
        public AtHomeDbContext() : base("AtHomeDbContext") { }

        public IDbSet<Device> Devices { get; set; }

        public IDbSet<DeviceState> DeviceStates { get; set; }

        public IDbSet<DeviceStateChange> DeviceStateChanges { get; set; } 

        public IDbSet<SensorType> SensorTypes { get; set; } 
        
        public IDbSet<Sensor> Sensors { get; set; }

        public IDbSet<SensorReading> SensorReadings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new DeviceConfiguration());
            modelBuilder.Configurations.Add(new DeviceStateConfiguration());
            modelBuilder.Configurations.Add(new DeviceStateChangeConfiguration());
            modelBuilder.Configurations.Add(new SensorTypeConfiguration());
            modelBuilder.Configurations.Add(new SensorConfiguration());
            modelBuilder.Configurations.Add(new SensorReadingConfiguration());
        }
    }
}