using System;

namespace WrightsAtHome.BackEnd.Domain.Services.Trigger.Parser
{
    public interface ITriggerHelpers
    {
        DateTime GetCurrentTime();
        decimal GetNumericSensorReading(string sensorName);
        string GetStringSensorReading(string sensorName);
    }

    public class TriggerHelpers : ITriggerHelpers
    {
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
    }
}
