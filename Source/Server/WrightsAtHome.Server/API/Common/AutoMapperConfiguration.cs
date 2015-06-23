using AutoMapper;
using WrightsAtHome.Server.API.Devices.Model;
using WrightsAtHome.Server.API.Sensors.Model;

namespace WrightsAtHome.Server.API.Common
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            DevicesMapping.Configure();
            SensorMapping.Configure();

            Mapper.AssertConfigurationIsValid();
        }
    }
}