using System;
using System.Collections.Generic;
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
    public class DeviceStateControllerTests : ApiFixture
    {

        public static IEnumerable<object[]> GetChangeStateData()
        {
            var states = SqlQuery.GetDataTable("SELECT s.BeforeStateId, s.AfterStateId, s.DeviceId FROM DeviceStateChange s " +
                                       "WHERE s.AppliedDate = (SELECT MAX(AppliedDate) FROM DeviceStateChange m WHERE " +
                                       "s.DeviceId = m.DeviceId)");

            return from DataRow row in states.Rows select row.ItemArray;
        }


        [Fact]
        public async void Get()
        {
            var states =
                SqlQuery.GetDataTable("SELECT ds.Id, ds.Name, ds.Sequence, s.DeviceId FROM DeviceStateChange s INNER JOIN " +
                                      "DeviceState ds ON s.AfterStateId = ds.Id WHERE s.AppliedDate = " +
                                      "(SELECT MAX(AppliedDate) FROM DeviceStateChange m WHERE s.DeviceId = m.DeviceId)");

            foreach (DataRow row in states.Rows)
            {
                using (var response = await Client.GetAsync("api/devices/" + row["DeviceId"] + "/state"))
                {
                    Assert.True(response.IsSuccessStatusCode);

                    var result = await response.Content.ReadAsAsync<DeviceStateDto>();

                    Assert.Equal(row["Id"], result.Id);
                    Assert.Equal(row["Name"], result.Name);
                    Assert.Equal(row["Sequence"], result.Sequence);
                }
                
            }
        }

        [Theory, MemberData("GetChangeStateData")]
        public async void ChangeState(int beforeId, int afterId, int deviceId)
        {
            var dto = new DeviceStateChangeDto { StateId = beforeId };

            using (var response = await Client.PostAsJsonAsync("api/devices/" + deviceId + "/state", dto))
            {
                Assert.True(response.IsSuccessStatusCode);

                var change = SqlQuery.GetDataTable("SELECT TOP 1 * FROM DeviceStateChange WHERE DeviceId = " + deviceId +
                                                   " ORDER BY AppliedDate DESC");

                Assert.Equal(1, change.Rows.Count);
                DataRow r = change.Rows[0];

                Assert.Equal(beforeId, r["AfterStateId"]);
                Assert.Equal(afterId, r["BeforeStateId"]);
                Assert.Equal(true, r["WasOverride"]);
                Assert.Equal(DBNull.Value, r["DeviceTriggerId"]);
            }
        }

        [Fact]
        public async void ChangeStateWithBadState()
        {
            var dto = new DeviceStateChangeDto();

            var deviceId = SqlQuery.GetValue<int>("SELECT MIN(Id) FROM Device");

            using (var response = await Client.PostAsJsonAsync("api/devices/" + deviceId + "/state", dto))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                string content = await response.Content.ReadAsStringAsync();

                Assert.True(content.StartsWith("Device State with id 0 not found"));
            }
        }

        [Fact]
        public async void ChangeStateWithBadDevice()
        {
            var dto = new DeviceStateChangeDto();

            using (var response = await Client.PostAsJsonAsync("api/devices/123657465/state", dto))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                string content = await response.Content.ReadAsStringAsync();

                Assert.Equal("Device with id 123657465 not found.", content);
            }
        }
    }
}
