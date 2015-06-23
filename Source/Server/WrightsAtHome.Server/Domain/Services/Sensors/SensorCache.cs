using System.Collections.Concurrent;
using System.Linq;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.Domain.Services.Sensors
{
    public interface ISensorCache
    {
        Sensor GetSensor(string sensorName);

        Sensor GetSensor(int sensorId);

        bool TryGetSensor(string sensorName, out Sensor sensor);

        bool TryGetSensor(int sensorId, out Sensor sensor);

        bool IsValidSensorName(string sensorName);

        void Refresh(IAtHomeDbContext dbContext);
    }

    public class SensorCache : ISensorCache
    {
        private readonly ConcurrentDictionary<int, Sensor> idCache = new ConcurrentDictionary<int, Sensor>();
        private readonly ConcurrentDictionary<string, Sensor> nameCache = new ConcurrentDictionary<string, Sensor>(); 

        public SensorCache(IAtHomeDbContext dbContext)
        {
            Refresh(dbContext);
        }

        public Sensor GetSensor(string sensorName)
        {
            return nameCache[sensorName.ToLower()];
        }

        public Sensor GetSensor(int sensorId)
        {
            return idCache[sensorId];
        }

        public void Refresh(IAtHomeDbContext dbContext)
        {
            var sensors = dbContext.Sensors.ToList();

            nameCache.Clear();
            idCache.Clear();

            foreach (var sensor in sensors)
            {
                nameCache[sensor.Name.ToLower()] = sensor;
                idCache[sensor.Id] = sensor;
            }
        }

        public bool TryGetSensor(string sensorName, out Sensor sensor)
        {
            return nameCache.TryGetValue(sensorName.ToLower(), out sensor);
        }

        public bool TryGetSensor(int sensorId, out Sensor sensor)
        {
            return idCache.TryGetValue(sensorId, out sensor);
        }

        public bool IsValidSensorName(string sensorName)
        {
            return nameCache.ContainsKey(sensorName.ToLower());
        }
    }
}