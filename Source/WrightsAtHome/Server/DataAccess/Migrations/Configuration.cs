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
            context.Database.ExecuteSqlCommand("DELETE FROM DeviceStateChange");
            context.Database.ExecuteSqlCommand("DELETE FROM DeviceTrigger");
            context.Database.ExecuteSqlCommand("DELETE FROM DeviceStateChange");
            context.Database.ExecuteSqlCommand("DELETE FROM DeviceTrigger");
            context.Database.ExecuteSqlCommand("DELETE FROM DeviceDeviceStateXref");
            context.Database.ExecuteSqlCommand("DELETE FROM Device");
            context.Database.ExecuteSqlCommand("DELETE FROM DeviceState");
            context.Database.ExecuteSqlCommand("DELETE FROM SensorReading");
            context.Database.ExecuteSqlCommand("DELETE FROM Sensor");
            context.Database.ExecuteSqlCommand("DELETE FROM SensorType");
            context.Database.ExecuteSqlCommand("DELETE FROM Log");
            
            var stateOff = new DeviceState { Name = "Off", Sequence = 0 };
            var stateOn = new DeviceState { Name = "On", Sequence = 1 };
            var stateLow = new DeviceState { Name = "On Low", Sequence = 2 };
            var stateHigh = new DeviceState { Name = "On High", Sequence = 3 };

            var states = new List<DeviceState> { stateOff, stateOn, stateLow, stateHigh };

            states.ForEach(s => context.DeviceStates.AddOrUpdate(s));

            var devices = new List<Device>
            {
                new Device(stateOff, stateOn)
                {
                    Name = "Pool Light",
                    ImageName = "PoolLight.png",
                    Sequence = 1,
                    Triggers = new List<DeviceTrigger>
                    {
                        new DeviceTrigger { ToState = stateOn, Sequence = 1, TriggerText = "WHEN CurrentTime >= 6:00pm AND LightLevel <= 5", IsActive = true },
                        new DeviceTrigger { ToState = stateOff, Sequence = 2, TriggerText = "AT 10:00pm", IsActive = true}
                    }
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Fountain",
                    ImageName = "Fountain.png",
                    Sequence = 2
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Landscape Lights - Front",
                    ImageName = "LandscapeLights.png",
                    Sequence = 3,
                    Triggers = new List<DeviceTrigger>
                    {
                        new DeviceTrigger { ToState = stateOn, Sequence = 1, TriggerText = "WHEN CurrentTime >= 6:00pm AND LightLevel <= 5", IsActive = true },
                        new DeviceTrigger { ToState = stateOff, Sequence = 2, TriggerText = "AT 11:00pm", IsActive = true}
                    }
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Landscape Lights - Back",
                    ImageName = "LandscapeLights.png",
                    Sequence = 4,
                    Triggers = new List<DeviceTrigger>
                    {
                        new DeviceTrigger { ToState = stateOn, Sequence = 1, TriggerText = "WHEN CurrentTime >= 6:00pm AND LightLevel <= 5", IsActive = true },
                        new DeviceTrigger { ToState = stateOff, Sequence = 2, TriggerText = "AT 10:00pm", IsActive = true}
                    }
                },

                new Device(stateOff, stateLow, stateHigh)
                {
                    Name = "PoolPump",
                    ImageName = "PoolPump.png",
                    Sequence = 5,
                    Triggers = new List<DeviceTrigger>
                    {
                        new DeviceTrigger { ToState = stateLow, Sequence = 1, TriggerText = "AT 1:00am", IsActive = true },
                        new DeviceTrigger { ToState = stateOff, Sequence = 2, TriggerText = "AFTER 4 Hours", IsActive = true}
                    }
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Pool Heater",
                    ImageName = "PoolHeater.png",
                    Sequence = 6,
                    Triggers = new List<DeviceTrigger>
                    {
                        new DeviceTrigger { ToState = stateLow, Sequence = 1, TriggerText = "WHEN PoolTemp < 82", IsActive = true },
                        new DeviceTrigger { ToState = stateOff, Sequence = 2, TriggerText = "WHEN PoolTemp > 83", IsActive = true}
                    }
                },

                new Device(stateOff, stateOn)
                {
                    Name = "Xmas Lights",
                    ImageName = "XmasLights.png",
                    Sequence = 7
                },

            };

            devices.ForEach(d => context.Devices.AddOrUpdate(d));

            var stTemp = new SensorType { Name = "Temperature" };
            var stLight = new SensorType { Name = "LightLevel" };
            var stDay = new SensorType { Name = "Daylight" };

            var sensorTypes = new List<SensorType> { stTemp, stLight, stDay };

            sensorTypes.ForEach(st => context.SensorTypes.AddOrUpdate(st));

            var senAir = new Sensor { Name = "AirTemp", Sequence = 1, SensorType = stTemp, ImageName = "thermometer.png", ReadInterval = TimeSpan.Parse("00:10:00") };
            var senPool = new Sensor { Name = "PoolTemp", Sequence = 2, SensorType = stTemp, ImageName = "thermometer.png", ReadInterval = TimeSpan.Parse("00:10:00") };
            var senLight = new Sensor { Name = "LightLevel", Sequence = 3, SensorType = stLight, ImageName = "LightSensor.png", ReadInterval = TimeSpan.Parse("00:10:00") };
            var senDay = new Sensor { Name = "Daylight", Sequence = 4, SensorType = stDay, ImageName = "LightSensor.png", ReadInterval = TimeSpan.Parse("00:10:00") };

            var sensors = new List<Sensor> { senAir, senPool, senLight, senDay };

            sensors.ForEach(s => context.Sensors.AddOrUpdate(s));

            var sensorReadings = new List<SensorReading>
            {
                new SensorReading {ReadingDate = DateTime.Parse("5/31/2015 10:00pm"), Sensor = senAir, Value = 55m },
                new SensorReading {ReadingDate = DateTime.Parse("5/31/2015 10:00pm"), Sensor = senPool, Value = 68m },
                new SensorReading {ReadingDate = DateTime.Parse("5/31/2015 10:00pm"), Sensor = senLight, Value = 0.5m },
                new SensorReading {ReadingDate = DateTime.Parse("5/31/2015 10:00pm"), Sensor = senDay, Value = 0.5m }
            };

            sensorReadings.ForEach(sr => context.SensorReadings.AddOrUpdate(sr));

            context.SaveChanges();
        }
    }
}
