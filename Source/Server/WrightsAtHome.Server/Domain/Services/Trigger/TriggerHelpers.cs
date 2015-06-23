using System;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Sensors;

namespace WrightsAtHome.Server.Domain.Services.Trigger
{
    public interface ITriggerHelpers
    {
        DateTime GetCurrentTime();
        
        decimal GetNumericSensorReading(string sensorName);

        string GetStringSensorReading(string sensorName);

        bool TryGetSensorName(string inputSensorName, out string actualSensorName);

        bool IsValidSensorName(string sensorName);
    }

    public class TriggerHelpers : ITriggerHelpers
    {
        private readonly ISensorCache sensorCache;

        public TriggerHelpers(ISensorCache sensorCache)
        {
            this.sensorCache = sensorCache;
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
            Sensor sensor;

            if (!sensorCache.TryGetSensor(inputSensorName, out sensor))
            {
                actualSensorName = string.Empty;
                return false;
            }

            actualSensorName = sensor.Name;
            return true;
        }

        public bool IsValidSensorName(string sensorName)
        {
            return sensorCache.IsValidSensorName(sensorName);
        }
    }
}
