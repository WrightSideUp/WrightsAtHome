using System;
using AutoMapper;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Devices;

namespace WrightsAtHome.Server.API.Devices.Model
{
    public class DevicesMapping
    {
        public static void Configure()
        {
            Mapper.CreateMap<DeviceState, DeviceStateDto>();

            Mapper.CreateMap<DeviceTrigger, DeviceTriggerDto>()
                .ForMember(d => d.DeviceId, opt => opt.MapFrom(src => src.Device.Id))
                .ForMember(d => d.ToStateId, opt => opt.MapFrom(src => src.ToState.Id));


            Mapper.CreateMap<Device, DeviceDto>()
                .ForMember(d => d.LargeImageUrl,
                    opt => opt.MapFrom(src => ControllerHelpers.DeviceImageUrlLarge(src.ImageName)))
                .ForMember(d => d.SmallImageUrl,
                    opt => opt.MapFrom(src => ControllerHelpers.DeviceImageUrlSmall(src.ImageName)))
                .ForMember(d => d.NextEvent, opt => opt.ResolveUsing<NextEventResolver>().FromMember(src => src.Id))
                .ForMember(d => d.CurrentStateId, opt => opt.ResolveUsing<CurrentStateResolver>().FromMember(src => src.Id));

            Mapper.CreateMap<Device, DeviceDetailsDto>()
                .IncludeBase<Device, DeviceDto>()
                .ForMember(d => d.LastStateChange,
                    opt =>
                        opt.MapFrom(
                            src => (src.StateChanges != null && src.StateChanges.Count > 0)
                                ? src.StateChanges[0].GetDescription()
                                : string.Empty));

            Mapper.CreateMap<TriggerValidationError, TriggerValidationErrorDto>();

            Mapper.CreateMap<TriggerValidationInfo, TriggerValidationDto>()
                .ForMember(d => d.TriggerType,
                    opt => opt.MapFrom(src => Enum.GetName(typeof (TriggerType), src.TriggerType)));
        }
    }

    public class NextEventResolver : ValueResolver<int, string>
    {
        private readonly IDeviceService deviceService;

        public NextEventResolver(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        protected override string ResolveCore(int deviceId)
        {
            return deviceService.GetNextTriggerEvent(deviceId);
        }
    }

    public class CurrentStateResolver : ValueResolver<int, int>
    {
        private readonly IDeviceService deviceService;

        public CurrentStateResolver(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        protected override int ResolveCore(int deviceId)
        {
            return deviceService.GetCurrentDeviceState(deviceId).Id;
        }
    }
}
