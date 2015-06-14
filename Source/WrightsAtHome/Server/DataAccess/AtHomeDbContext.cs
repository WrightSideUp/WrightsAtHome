using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess
{
    public interface IAtHomeDbContext 
    {
        IDbSet<Device> Devices { get; set; }

        IDbSet<DeviceState> DeviceStates { get; set; }

        IDbSet<DeviceStateChange> DeviceStateChanges { get; set; }

        IDbSet<DeviceTrigger> DeviceTriggers { get; set; } 
        
        IDbSet<DeviceTriggerWait> DeviceTriggerWaits { get; set; }

        IDbSet<Sensor> Sensors { get; set; }

        IDbSet<SensorReading> SensorReadings { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity: class;

        int SaveChanges();
    }
    
    public class AtHomeDbContext : DbContext, IAtHomeDbContext
    {
        public AtHomeDbContext() : base("AtHomeDbContext") { }

        public IDbSet<Device> Devices { get; set; }

        public IDbSet<DeviceState> DeviceStates { get; set; }

        public IDbSet<DeviceStateChange> DeviceStateChanges { get; set; }

        public IDbSet<DeviceTrigger> DeviceTriggers { get; set; }

        public IDbSet<DeviceTriggerWait> DeviceTriggerWaits { get; set; }

        public IDbSet<SensorType> SensorTypes { get; set; }

        public IDbSet<Sensor> Sensors { get; set; }

        public IDbSet<SensorReading> SensorReadings { get; set; }

        public override int SaveChanges()
        {
            var changed = ChangeTracker.Entries<IBaseEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(p => p.Entity);

            var now = DateTime.Now;

            foreach (var entity in changed)
            {
                entity.LastModified = now;
                entity.LastModifiedUserId = 0;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}