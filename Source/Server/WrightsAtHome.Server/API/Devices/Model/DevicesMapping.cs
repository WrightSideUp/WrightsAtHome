using System;
using AutoMapper;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger.Validator;


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
                .ForMember(d => d.NextEvent, opt => opt.Ignore())
                .ForMember(d => d.CurrentStateId, opt => opt.Ignore());

            Mapper.CreateMap<Device, DeviceDetailsDto>()
                .ForMember(d => d.LargeImageUrl,
                    opt => opt.MapFrom(src => ControllerHelpers.DeviceImageUrlLarge(src.ImageName)))
                .ForMember(d => d.SmallImageUrl,
                    opt => opt.MapFrom(src => ControllerHelpers.DeviceImageUrlSmall(src.ImageName)))
                .ForMember(d => d.LastStateChange,
                    opt =>
                        opt.MapFrom(
                            src =>
                                src.StateChanges != null
                                    ? (src.StateChanges.Count > 0 ? src.StateChanges[0].GetDescription() : string.Empty)
                                    : string.Empty))
                .ForMember(d => d.NextEvent, opt => opt.Ignore())
                .ForMember(d => d.CurrentStateId, opt => opt.Ignore());

            Mapper.CreateMap<TriggerValidationError, TriggerValidationErrorDto>();

            Mapper.CreateMap<TriggerValidationInfo, TriggerValidationDto>()
                .ForMember(d => d.TriggerType,
                    opt => opt.MapFrom(src => Enum.GetName(typeof (TriggerType), src.TriggerType)));
        }
    }
}
