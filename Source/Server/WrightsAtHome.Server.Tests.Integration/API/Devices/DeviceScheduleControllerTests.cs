using System;
using System.Data;
using System.Net;
using System.Net.Http;
using WrightsAtHome.Server.API.Devices.Model;
using WrightsAtHome.Server.Tests.Integration.Utility;
using Xunit;

namespace WrightsAtHome.Server.Tests.Integration.API.Devices
{
    [Collection("DatabaseCollection")]
    public class DeviceScheduleControllerTests : ApiFixture
    {
        [Fact]
        public async void GetTriggers()
        {
            var tData =
                SqlQuery.GetDataTable(
                    "SELECT * FROM DeviceTrigger WHERE DeviceId = (SELECT Id FROM Device WHERE Name = 'Pool Light') ORDER BY Sequence");

            using (var response = await Client.GetAsync("api/devices/" + tData.Rows[0]["DeviceId"] + "/schedule"))
            {
                Assert.True(response.IsSuccessStatusCode);

                var result = await response.Content.ReadAsAsync<DeviceTriggerDto[]>();

                Assert.Equal(tData.Rows.Count, result.Length);

                for (int i = 0; i < result.Length; i++)
                {
                    DataRow r = tData.Rows[i];
                    var t = result[i];

                    Assert.Equal(r["TriggerText"], t.TriggerText);
                    Assert.Equal(r["Id"], t.Id);
                    Assert.Equal(r["Sequence"], t.Sequence);
                    Assert.Equal(r["DeviceStateId"], t.ToStateId);
                    Assert.Equal(r["IsActive"], t.IsActive);
                }
            }
        }


        [Fact]
        public async void GetTrigger()
        {
            var tData = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger");

            foreach (DataRow r in tData.Rows)
            {
                using (var response = await Client.GetAsync("api/devices/" + r["DeviceId"] + "/schedule/" + r["Id"]))
                {
                    Assert.True(response.IsSuccessStatusCode);

                    var t = await response.Content.ReadAsAsync<DeviceTriggerDto>();

                    Assert.Equal(r["TriggerText"], t.TriggerText);
                    Assert.Equal(r["Id"], t.Id);
                    Assert.Equal(r["Sequence"], t.Sequence);
                    Assert.Equal(r["DeviceStateId"], t.ToStateId);
                    Assert.Equal(r["IsActive"], t.IsActive);
                }
            }
        }

        [Fact]
        public async void UpdateTrigger()
        {
            var tData = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger WHERE DeviceId = (SELECT Id FROM Device WHERE Name = 'Pool Light') ORDER BY Sequence");

            DataRow r = tData.Rows[0];

            var req = new DeviceTriggerDto
            {
                Id = (int) r["Id"],
                DeviceId = (int) r["DeviceId"],
                IsActive = false,
                LastModified = (DateTime) r["LastModified"],
                Sequence = (int) r["Sequence"],
                ToStateId = (int) r["DeviceStateId"],
                TriggerText = "AT 5:00pm"
            };

            using (var response = await Client.PutAsJsonAsync("api/devices/" + req.DeviceId + "/schedule/" + req.Id, req))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                tData = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger WHERE Id = " + req.Id);

                Assert.Equal(1, tData.Rows.Count);

                r = tData.Rows[0];

                Assert.Equal(r["TriggerText"], req.TriggerText);
                Assert.Equal(r["Id"], req.Id);
                Assert.Equal(r["Sequence"], req.Sequence);
                Assert.Equal(r["DeviceStateId"], req.ToStateId);
                Assert.Equal(r["IsActive"], req.IsActive);

                Assert.True((DateTime)r["LastModified"] > req.LastModified);
            }
        }

        [Fact]
        public async void UpdateWithBadDevice()
        {
            var tData = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger WHERE DeviceId = (SELECT Id FROM Device WHERE Name = 'Pool Light') ORDER BY Sequence");

            DataRow r = tData.Rows[1];

            var req = new DeviceTriggerDto
            {
                Id = (int)r["Id"],
                DeviceId = (int)r["DeviceId"],
                IsActive = false,
                LastModified = (DateTime)r["LastModified"],
                Sequence = (int)r["Sequence"],
                ToStateId = (int)r["DeviceStateId"],
                TriggerText = "AT 11:59pm"
            };

            SqlQuery.Execute("Update DeviceTrigger SET LastModified = getdate() WHERE Id = " + req.Id);

            using (var response = await Client.PutAsJsonAsync("api/devices/5458715" + "/schedule/" + req.Id, req))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                string text = await response.Content.ReadAsStringAsync();
                Assert.Equal("No Device with Id '5458715' exists.", text);
            }
        }

        [Fact]
        public async void UpdateWithBadTrigger()
        {
            var tData = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger WHERE DeviceId = (SELECT Id FROM Device WHERE Name = 'Pool Light') ORDER BY Sequence");

            DataRow r = tData.Rows[0];

            var req = new DeviceTriggerDto
            {
                Id = (int)r["Id"],
                DeviceId = (int)r["DeviceId"],
                IsActive = false,
                LastModified = (DateTime)r["LastModified"],
                Sequence = (int)r["Sequence"],
                ToStateId = (int)r["DeviceStateId"],
                TriggerText = "AT 5:00pm"
            };

            using (var response = await Client.PutAsJsonAsync("api/devices/" + req.DeviceId + "/schedule/565461556", req))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                string text = await response.Content.ReadAsStringAsync();
                Assert.Equal("No Schedule with Id '565461556' exists for Device 'Pool Light'", text);
            }
        }

        [Fact]
        public async void CreateTrigger()
        {
            var deviceId = SqlQuery.GetValue<int>("SELECT Id FROM Device WHERE Name = 'Fountain'");
            var stateId = SqlQuery.GetValue<int>("SELECT Id FROM DeviceState WHERE Name = 'On'");
            var req = new DeviceTriggerDto
            {
                DeviceId = deviceId,
                IsActive = true,
                Sequence = 1,
                ToStateId = stateId,
                TriggerText = "AT 7:30pm"
            };

            using (var response = await Client.PostAsJsonAsync("api/devices/" + deviceId + "/schedule", req))
            {
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var dto = await response.Content.ReadAsAsync<DeviceTriggerDto>();

                Assert.Equal(req.IsActive, dto.IsActive);
                Assert.Equal(req.Sequence, dto.Sequence);
                Assert.Equal(req.ToStateId, dto.ToStateId);
                Assert.Equal(req.TriggerText, dto.TriggerText);

                var tData = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger WHERE Id = " + dto.Id);

                Assert.Equal(1, tData.Rows.Count);

                DataRow r = tData.Rows[0];

                Assert.Equal(r["TriggerText"], req.TriggerText);
                Assert.Equal(r["Sequence"], req.Sequence);
                Assert.Equal(r["DeviceStateId"], req.ToStateId);
                Assert.Equal(r["IsActive"], req.IsActive);
                
                // SQL Server date resolution means absolute match not possible, so just make sure they are within one second.
                Assert.True(((DateTime)r["LastModified"] - dto.LastModified).Duration() < new TimeSpan(0, 0, 1)); 

                var location = response.Headers.Location;
                Assert.Equal(Client.BaseAddress + "api/devices/" + deviceId + "/schedule/" + dto.Id, location.ToString());
            }
        }

        [Fact]
        public async void DeleteTrigger()
        {
            var tData = SqlQuery.GetDataTable("SELECT TOP(1) DeviceId, Id FROM DeviceTrigger WHERE DeviceId = (SELECT Id " + 
                                              "FROM Device WHERE Name = 'Pool Light')");

            Assert.True(tData.Rows.Count > 0);

            using (var response = await Client.DeleteAsync("api/devices/" + tData.Rows[0]["DeviceId"] + "/schedule/" + tData.Rows[0]["Id"]))
            {
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                tData = SqlQuery.GetDataTable("SELECT * FROM DeviceTrigger WHERE Id = " + tData.Rows[0]["Id"]);

                Assert.Equal(0, tData.Rows.Count);
            }
        }
    }
}
