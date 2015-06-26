using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using WrightsAtHome.Server.API.Devices.Model;
using WrightsAtHome.Server.Tests.Integration.Utility;
using Xunit;

namespace WrightsAtHome.Server.Tests.Integration.API.Devices
{
    [Collection("DatabaseCollection")]
    public class DevicesControllerTests : ApiFixture
    {
        [Fact]
        public async void AllDevices()
        {
            int count = SqlQuery.GetValue<int>("SELECT COUNT(*) FROM device");
            
            using (var response = await Client.GetAsync("api/devices"))
            {
                Assert.True(response.IsSuccessStatusCode);
                
                var results = await response.Content.ReadAsAsync<DeviceDto[]>();

                Assert.NotNull(results);
                Assert.Equal(count, results.Length);

                var poolLight = results.FirstOrDefault(d => d.Name == "Pool Light");

                Assert.NotNull(poolLight);

                if (poolLight != null)
                {
                    Assert.Equal("images/devices_large/PoolLight.png", poolLight.LargeImageUrl);
                    Assert.Equal("images/devices_small/PoolLight.png", poolLight.SmallImageUrl);
                    Assert.Equal(1, poolLight.Sequence);
                    Assert.Equal(2, poolLight.PossibleStates.Length);
                    Assert.Equal("Off", poolLight.PossibleStates[0].Name);
                    Assert.Equal("On", poolLight.PossibleStates[1].Name);
                    Assert.True(poolLight.PossibleStates.Select(ps => ps.Id).Contains(poolLight.CurrentStateId));
                }
            }
        }

        [Fact]
        public async void SingleDevice()
        {
            int id = SqlQuery.GetValue<int>("SELECT Id FROM Device WHERE Name = 'Pool Pump'");

            var tableVal = SqlQuery.GetDataTable("SELECT * FROM DEVICE WHERE id = " + id);

            using (var response = await Client.GetAsync("api/devices/" + id))
            {
                Assert.True(response.IsSuccessStatusCode);

                var dto = await response.Content.ReadAsAsync<DeviceDto>();

                Assert.NotNull(dto);

                DataRow r = tableVal.Rows[0];
                
                Assert.Equal("images/devices_large/" + r["ImageName"], dto.LargeImageUrl);
                Assert.Equal("images/devices_small/" + r["ImageName"], dto.SmallImageUrl);
                Assert.Equal(r["Sequence"], dto.Sequence);
                Assert.Equal(3, dto.PossibleStates.Length);
                Assert.Equal("Off", dto.PossibleStates[0].Name);
                Assert.Equal("On Low", dto.PossibleStates[1].Name);
                Assert.Equal("On High", dto.PossibleStates[2].Name);
                Assert.True(dto.PossibleStates.Select(ps => ps.Id).Contains(dto.CurrentStateId));
            }
        }

        [Fact]
        public async void Details()
        {
            int id = SqlQuery.GetValue<int>("SELECT Id FROM Device WHERE Name = 'Landscape Lights - Front'");

            var tableVal = SqlQuery.GetDataTable("SELECT * FROM DEVICE WHERE id = " + id);
            var triggersVal = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger WHERE DeviceId = " + id + " ORDER BY Sequence");

            using (var response = await Client.GetAsync("api/devices/" + id + "/details"))
            {
                Assert.True(response.IsSuccessStatusCode);

                var dto = await response.Content.ReadAsAsync<DeviceDetailsDto>();

                Assert.NotNull(dto);

                DataRow r = tableVal.Rows[0];

                Assert.Equal("images/devices_large/" + r["ImageName"], dto.LargeImageUrl);
                Assert.Equal("images/devices_small/" + r["ImageName"], dto.SmallImageUrl);
                Assert.Equal(r["Sequence"], dto.Sequence);
                Assert.Equal(2, dto.PossibleStates.Length);
                Assert.Equal("Off", dto.PossibleStates[0].Name);
                Assert.Equal("On", dto.PossibleStates[1].Name);
                Assert.True(dto.PossibleStates.Select(ps => ps.Id).Contains(dto.CurrentStateId));

                Assert.Equal(triggersVal.Rows.Count, dto.Triggers.Length);

                for (int i = 0; i<dto.Triggers.Length; i++)
                {
                    var row = triggersVal.Rows[i];
                    var trig = dto.Triggers[i];

                    Assert.Equal(row["TriggerText"], trig.TriggerText);
                    Assert.Equal(row["IsActive"], trig.IsActive);
                    Assert.Equal(row["Sequence"], trig.Sequence);
                    Assert.Equal(row["DeviceStateId"], trig.ToStateId);
                }
            }
            
        }
        
        [Fact]
        public async void DetailsWithState()
        {
            int id = SqlQuery.GetValue<int>("SELECT Id FROM Device WHERE Name = 'Pool Light'");

            var tableVal = SqlQuery.GetDataTable("SELECT * FROM DEVICE WHERE id = " + id);
            var triggersVal = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger WHERE DeviceId = " + id + " ORDER BY Sequence");
            var stateVal = SqlQuery.GetDataTable("SELECT TOP(1) * FROM DeviceStateChange WHERE DeviceId = " + id + " ORDER BY AppliedDate DESC");

            using (var response = await Client.GetAsync("api/devices/" + id + "/details"))
            {
                Assert.True(response.IsSuccessStatusCode);

                var dto = await response.Content.ReadAsAsync<DeviceDetailsDto>();

                Assert.NotNull(dto);

                DataRow r = tableVal.Rows[0];

                Assert.Equal("images/devices_large/" + r["ImageName"], dto.LargeImageUrl);
                Assert.Equal("images/devices_small/" + r["ImageName"], dto.SmallImageUrl);
                Assert.Equal(r["Sequence"], dto.Sequence);
                Assert.Equal(2, dto.PossibleStates.Length);
                Assert.Equal("Off", dto.PossibleStates[0].Name);
                Assert.Equal("On", dto.PossibleStates[1].Name);
                Assert.True(dto.PossibleStates.Select(ps => ps.Id).Contains(dto.CurrentStateId));

                Assert.Equal(triggersVal.Rows.Count, dto.Triggers.Length);

                for (int i = 0; i < dto.Triggers.Length; i++)
                {
                    var row = triggersVal.Rows[i];
                    var trig = dto.Triggers[i];

                    Assert.Equal(row["TriggerText"], trig.TriggerText);
                    Assert.Equal(row["IsActive"], trig.IsActive);
                    Assert.Equal(row["Sequence"], trig.Sequence);
                    Assert.Equal(row["DeviceStateId"], trig.ToStateId);
                }
            }

        }
        
        [Fact]
        public async void Single_NotFound()
        {
            using (var response = await Client.GetAsync("api/devices/44993920"))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.Equal("No Device with Id '44993920' exists", await response.Content.ReadAsStringAsync());
            }
        }

        [Fact]
        public async void Details_NotFound()
        {
            using (var response = await Client.GetAsync("api/devices/44993920/details"))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.Equal("No Device with Id '44993920' exists", await response.Content.ReadAsStringAsync());
            }
        }


    }
}
