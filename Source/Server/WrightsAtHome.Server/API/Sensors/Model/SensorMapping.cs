using AutoMapper;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.API.Sensors.Model
{
    public static class SensorMapping
    {
        public static void Configure()
        {
            Mapper.CreateMap<Sensor, SensorReadingDto>()
                .ForMember(d => d.SensorType, opt => opt.MapFrom(src => src.SensorType.Name))
                .ForMember(d => d.LargeImageUrl, opt => opt.MapFrom(src => ControllerHelpers.DeviceImageUrlLarge(src.ImageName)))
                .ForMember(d => d.SmallImageUrl, opt => opt.MapFrom(src => ControllerHelpers.DeviceImageUrlSmall(src.ImageName)))
                .ForMember(d => d.Reading, opt => opt.Ignore());
        }
    }
}