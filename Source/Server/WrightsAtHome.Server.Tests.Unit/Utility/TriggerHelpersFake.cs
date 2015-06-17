using System;
using System.Collections.Generic;
using System.Linq;
using WrightsAtHome.Server.Domain.Services;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;

namespace WrightsAtHome.Server.Tests.Unit.Utility
{
    public class TriggerHelpersFake : ITriggerHelpers, IDateTimeHelpers
    {
        public string CurrentTime { get; set; }

        public Dictionary<string, decimal> SensorReadings { get; set; }

        public DateTime GetCurrentTime()
        {
            return DateTime.Parse(CurrentTime);
        }

        public DateTime Now
        {
            get { return GetCurrentTime(); }
        }

        public decimal GetNumericSensorReading(string sensorName)
        {
            return SensorReadings[sensorName];
        }

        public string GetStringSensorReading(string sensorName)
        {
            throw new NotImplementedException();
        }

        public bool TryGetSensorName(string inputSensorName, out string actualSensorName)
        {
            if (SensorReadings == null)
            {
                actualSensorName = string.Empty;
                return false;
            }

            var entry = SensorReadings.FirstOrDefault(p => p.Key.ToLower() == inputSensorName.ToLower());

            if (entry.Equals(default(KeyValuePair<string, decimal>)))
            {
                actualSensorName = string.Empty;
                return false;
            }

            actualSensorName = entry.Key;
            return true;
        }
    }
}
