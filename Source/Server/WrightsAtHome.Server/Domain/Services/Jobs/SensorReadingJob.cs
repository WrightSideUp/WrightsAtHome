using System;
using System.Linq;
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
        private readonly IAtHomeDbContext dbContext;
        private readonly ISensorService sensorService;
        private readonly ILogger logger;

        public SensorReadingJob(IAtHomeDbContext dbContext, ISensorService sensorService)
        {
            this.dbContext = dbContext;
            this.sensorService = sensorService;
            logger = LogManager.GetCurrentClassLogger();
        }

        public void GetSensorReadings()
        {
            try
            {
                logger.Info("Sensor Reading Begun");

                var allSensors = dbContext.Sensors.OrderBy(s => s.Sequence).ToList();

                foreach (var sensor in allSensors)
                {
                    try
                    {
                        var currentReading = sensorService.GetCurrentSensorReading(sensor.Id);

                        dbContext.SensorReadings.Add(new SensorReading
                        {
                            ReadingDate = DateTime.Now,
                            Sensor = sensor,
                            Value = currentReading
                        });
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error reading sensor {0}", sensor.Name);
                    }
                }

                dbContext.SaveChanges();

                logger.Info("Sensor Reading Ended");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Fatal error reading sensors");
            }
        }
    }
}