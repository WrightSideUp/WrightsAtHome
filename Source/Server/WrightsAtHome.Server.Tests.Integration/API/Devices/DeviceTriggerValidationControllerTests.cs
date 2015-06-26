
using System;
using System.Data;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using WrightsAtHome.Server.API.Devices.Model;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Tests.Integration.Utility;
using Xunit;

namespace WrightsAtHome.Server.Tests.Integration.API.Devices
{
    public class DeviceTriggerValidationControllerTests : ApiFixture
    {
        [Fact]
        public async void GoodTrigger()
        {
            var req = new TriggerValidationRequestDto { TriggerText = "AT 11:00pm" };

            using (var response = await Client.PostAsJsonAsync("api/devices/triggerValidation", req))
            {
                Assert.True(response.IsSuccessStatusCode);
                var dto = await response.Content.ReadAsAsync<TriggerValidationDto>();

                Assert.True(dto.IsValid);
                Assert.Equal("At", dto.TriggerType);
                Assert.Equal(0, dto.Errors.Length);
            }
            
        }

        [Fact]
        public async void BadTrigger()
        {
            var req = new TriggerValidationRequestDto { TriggerText = "ATT 611:00pm" };

            using (var response = await Client.PostAsJsonAsync("api/devices/triggerValidation", req))
            {
                Assert.True(response.IsSuccessStatusCode);
                var dto = await response.Content.ReadAsAsync<TriggerValidationDto>();

                Assert.False(dto.IsValid);
                Assert.Equal(1, dto.Errors.Length);
                Assert.Equal("'ATT' is not a valid input", dto.Errors[0].ErrorMessage);
                Assert.Equal(3, dto.Errors[0].Length);
                Assert.Equal(0, dto.Errors[0].StartIndex);
            }

        }
    }
}
