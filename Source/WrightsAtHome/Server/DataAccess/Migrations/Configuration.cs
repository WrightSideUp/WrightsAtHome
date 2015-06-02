using System.Collections.Generic;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WrightsAtHome.Server.DataAccess.AtHomeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Server\DataAccess\Migrations";
        }

        protected override void Seed(WrightsAtHome.Server.DataAccess.AtHomeDbContext context)
        {
            var stateOff = new DeviceState { Name = "Off", StateNumber = 0 };
            var stateOn = new DeviceState { Name = "On", StateNumber = 1 };
            var stateLow = new DeviceState { Name = "On Low", StateNumber = 1 };
            var stateHigh = new DeviceState { Name = "On High", StateNumber = 2 };

            var states = new List<DeviceState> { stateOff, stateOn, stateLow, stateHigh };

            states.ForEach(s => context.DeviceStates.AddOrUpdate(s));

            var devices = new List<Device>
            {
                new Device(stateOff, stateOn)
                {
                    Name = "Pool Light",
                    ImageName = "PoolLight.png",
                    StartTriggerText = "WHEN CurrentTime >= 6:00pm AND LightLevel <= 5",
                    EndTriggerText = "AT 10:00pm"
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Fountain",
                    ImageName = "Fountain.png",
                    StartTriggerText = "",
                    EndTriggerText = ""
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Landscape Lights - Front",
                    ImageName = "LandscapeLights.png",
                    StartTriggerText = "WHEN CurrentTime >= 6:00pm AND LightLevel <= 5",
                    EndTriggerText = "AT 11:00pm"
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Landscape Lights - Back",
                    ImageName = "LandscapeLights.png",
                    StartTriggerText = "WHEN CurrentTime >= 6:00pm AND LightLevel <= 5",
                    EndTriggerText = "AT 10:00pm"
                },

                new Device(stateOff, stateLow, stateHigh)
                {
                    Name = "PoolPump",
                    ImageName = "PoolPump.png",
                    StartTriggerText = "AT 1:00am",
                    EndTriggerText = "AFTER 4 Hours"
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Pool Heater",
                    ImageName = "PoolHeater.png",
                    StartTriggerText = "WHEN PoolTemp < 82",
                    EndTriggerText = "WHEN PoolTemp > 83"
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Xmas Lights",
                    ImageName = "XmasLights.png",
                    StartTriggerText = "",
                    EndTriggerText = ""
                },

            };

            devices.ForEach(d => context.Devices.AddOrUpdate(d));

            var stTemp = new SensorType { Name = "Temperature" };
            var stLight = new SensorType { Name = "LightLevel" };
            var stDay = new SensorType { Name = "Daylight" };

            var sensorTypes = new List<SensorType> { stTemp, stLight, stDay };

            sensorTypes.ForEach(st => context.SensorTypes.AddOrUpdate(st));

            var senAir = new Sensor { Name = "AirTemp", SensorType = stTemp, ImageName = "thermometer.png", ReadInterval = TimeSpan.Parse("00:10:00") };
            var senPool = new Sensor { Name = "PoolTemp", SensorType = stTemp, ImageName = "thermometer.png", ReadInterval = TimeSpan.Parse("00:10:00") };
            var senLight = new Sensor { Name = "LightLevel", SensorType = stLight, ImageName = "LightSensor.png", ReadInterval = TimeSpan.Parse("00:10:00") };
            var senDay = new Sensor { Name = "Daylight", SensorType = stDay, ImageName = "LightSensor.png", ReadInterval = TimeSpan.Parse("00:10:00") };

            var sensors = new List<Sensor> { senAir, senPool, senLight, senDay };

            sensors.ForEach(s => context.Sensors.AddOrUpdate(s));

            var sensorReadings = new List<SensorReading>
            {
                new SensorReading {ReadingDate = DateTime.Parse("5/31/2015 10:00pm"), ReadingSensor = senAir, Value = 55m },
                new SensorReading {ReadingDate = DateTime.Parse("5/31/2015 10:00pm"), ReadingSensor = senPool, Value = 68m },
                new SensorReading {ReadingDate = DateTime.Parse("5/31/2015 10:00pm"), ReadingSensor = senLight, Value = 0.5m },
                new SensorReading {ReadingDate = DateTime.Parse("5/31/2015 10:00pm"), ReadingSensor = senDay, Value = 0.5m }
            };

            sensorReadings.ForEach(sr => context.SensorReadings.AddOrUpdate(sr));

            context.SaveChanges();
        }
    }
}
