using System;
using System.Linq;
using Autofac;
using NLog;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Sensors;

namespace WrightsAtHome.Server.Domain.Services.Jobs
{
    public interface ISensorReadJob
    {
        void GetSensorReadings();
    }
    public class SensorReadingJob : ISensorReadJob
    {
        private readonly ILifetimeScope parentScope;
        private readonly ILogger logger;

        public SensorReadingJob(ILifetimeScope parentScope)
        {
            this.parentScope = parentScope;
            logger = LogManager.GetCurrentClassLogger();
        }

        public void GetSensorReadings()
        {
            try
            {
                logger.Info("Sensor Reading Begun");

                using (var autoFacScope = parentScope.BeginLifetimeScope())
                {
                    var dbContext = autoFacScope.Resolve<IAtHomeDbContext>();
                    var sensorService = autoFacScope.Resolve<ISensorService>();

                    var allSensors = dbContext.Sensors.OrderBy(s => s.Sequence).ToList();

                    foreach (var sensor in allSensors)
                    {
                        try
                        {
                            var currentReading = sensorService.GetCurrentSensorReading(sensor.Id);

                            sensor.Readings.Add(new SensorReading
                            {
                                ReadingDate = DateTime.Now,
                                Value = currentReading
                            });
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Error reading sensor {0}", sensor.Name);
                        }
                    }

                    dbContext.SaveChanges();

                }

                logger.Info("Sensor Reading Ended");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Fatal error reading sensors");
            }
        }
    }
}