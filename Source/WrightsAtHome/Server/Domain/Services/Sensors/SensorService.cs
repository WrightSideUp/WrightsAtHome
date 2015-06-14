
namespace WrightsAtHome.Server.Domain.Services.Sensors
{
    public interface ISensorService
    {
        decimal GetCurrentSensorReading(int sensorId);
    }
    
    public class SensorService : ISensorService
    {
        public decimal GetCurrentSensorReading(int sensorId)
        {
            switch (sensorId)
            {
                case 1:
                    return 89m;
                case 2:
                    return 85m;
                case 3:
                    return 1.3m;
                case 4:
                    return 1.3m;
                default:
                    return 0m;
            }
        }
    }
}