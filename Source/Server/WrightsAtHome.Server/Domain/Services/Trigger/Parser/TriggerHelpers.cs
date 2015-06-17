using System;
using System.Linq;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.Domain.Services.Trigger.Parser
{
    public interface ITriggerHelpers
    {
        DateTime GetCurrentTime();
        
        decimal GetNumericSensorReading(string sensorName);

        string GetStringSensorReading(string sensorName);

        bool TryGetSensorName(string inputSensorName, out string actualSensorName);
    }

    public class TriggerHelpers : ITriggerHelpers
    {
        private readonly IAtHomeDbContext dbContext;

        public TriggerHelpers(IAtHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        public decimal GetNumericSensorReading(string sensorName)
        {
            return 0m;
        }

        public string GetStringSensorReading(string sensorName)
        {
            return string.Empty;
        }

        public bool TryGetSensorName(string inputSensorName, out string actualSensorName)
        {
            var sensor = GetSensor(inputSensorName);

            if (sensor != null)
            {
                actualSensorName = sensor.Name;
                return true;
            }

            actualSensorName = string.Empty;
            return false;
        }

        private Sensor GetSensor(string sensorName)
        {
            return dbContext.Sensors.FirstOrDefault(s => s.Name.ToLower() == sensorName.ToLower());
        }
    }
}
