using System;
using System.Collections.Generic;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;
using Xunit;

namespace WrightsAtHome.Server.Tests.Unit.Domain.Services.Trigger
{
    class FakeHelpers : ITriggerHelpers
    {
        public string CurrentTime { get; set; }

        public Dictionary<string, decimal> SensorReadings { get; set; }

        public DateTime GetCurrentTime()
        {
            return DateTime.Parse(CurrentTime);
        }

        public decimal GetNumericSensorReading(string sensorName)
        {
            return SensorReadings[sensorName];
        }

        public string GetStringSensorReading(string sensorName)
        {
            throw new NotImplementedException();
        }
    }

    public class TriggerCompilerTest
    {
        [Theory]
        [InlineData("11:00pm", "11:01PM", false)]
        [InlineData("11:01pm", "11:00PM", true)]
        [InlineData("11:15am", "9:50pm", false)]
        [InlineData("11:00:01PM", "9:50PM", true)]
        public void TimeGreaterReturnsTrue(string currentTime, string atTime, bool result)
        {
            // Arrange
            var trigger = new DeviceTrigger {TriggerText = "AT " + atTime};
            var underTest = new TriggerCompiler(new FakeHelpers { CurrentTime = currentTime });

            // Act
            var func = underTest.CompileTrigger(trigger).AtOrWhenFunction;

            // Assert
            Assert.Equal(result, func());
        }

        [Fact]
        public void InvalidTimeStrings()
        {
            // Arrange
            var trigger = new DeviceTrigger { TriggerText = "AT 25:00" };
            var underTest = new TriggerCompiler(new FakeHelpers { CurrentTime = "11:00pm" });

            // Act, Assert
            var ex = Assert.Throws<TriggerException>(() => underTest.CompileTrigger(trigger));
            Assert.Equal("mismatched input '25' expecting TIMECONST", ex.Message);
        }

        [Theory]
        [InlineData("7:00am", "7:01am", "AFTER 1 MINUTE", true)]
        [InlineData("7:00", "7:04", "after 5 minutes", false)]
        [InlineData("1:00pm", "1:59pm", "after 1 hour", false)]
        [InlineData("1:00pm", "2:00pm", "AFTER 1 HOUR", true)]
        [InlineData("9:00pm", "11:59pm", "after 3 HOURS", false)]
        [InlineData("8:59pm", "11:59pm", "after 3 hours", true)]
        [InlineData("1/1/2015 11:00pm", "1/2/2015 1:00am", "after 2 hours", true)]
        [InlineData("1/1/2015 11:00pm", "1/2/2015 12:59am", "after 2 hours", false)]

        public void AfterTests(string startTime, string currentTime, string triggerText, bool result)
        {
            // Arrange
            var trigger = new DeviceTrigger { TriggerText = triggerText };
            var underTest = new TriggerCompiler(new FakeHelpers { CurrentTime = currentTime });

            // Act
            var func = underTest.CompileTrigger(trigger).AfterFunction;

            Assert.Equal(result, func(DateTime.Parse(startTime)));
        }


        [Theory]
        [InlineData("WHEN PoolTemp < 80", true)]
        [InlineData("WHEN PoolTemp <= 78", true)]
        [InlineData("WHEN PoolTemp - AirTemp <= -8", true)]
        [InlineData("WHEN AirTemp + (- PoolTemp) > 10", false)]
        [InlineData("WHEN AirTemp - PoolTemp > 5 AND (LightLevel > 15 OR AirTemp > 80)", true)]
        public void WhenTests(string triggerText, bool expected)
        {
            // Arrange
            var helper = new FakeHelpers
            {
                SensorReadings = new Dictionary<string, decimal>()
                                            {
                                                { "pooltemp", 78 },
                                                { "lightlevel", 15 },
                                                { "airtemp", 86.0m }
                                            }
            };
            var trigger = new DeviceTrigger { TriggerText = triggerText };
            var underTest = new TriggerCompiler(helper);

            // Act
            var func = underTest.CompileTrigger(trigger).AtOrWhenFunction;

            Assert.Equal(expected, func());
        }

        [Theory]
        [InlineData("WHEN PoolTemp < 80", true)]
        [InlineData("WHEN PoolTemp <= 78", true)]
        [InlineData("WHEN PoolTemp - AirTemp <= -8", true)]
        [InlineData("WHEN AirTemp + (- PoolTemp) > 10", false)]
        [InlineData("WHEN AirTemp - PoolTemp > 5 AND (LightLevel > 15 OR AirTemp > 80)", true)]
        public void WhenEndTests(string triggerText, bool expected)
        {
            // Arrange
            var helper = new FakeHelpers
            {
                SensorReadings = new Dictionary<string, decimal>()
                                            {
                                                { "pooltemp", 78 },
                                                { "lightlevel", 15 },
                                                { "airtemp", 86.0m }
                                            }
            };
            var trigger = new DeviceTrigger { TriggerText = triggerText };
            var underTest = new TriggerCompiler(helper);

            // Act
            var func = underTest.CompileTrigger(trigger).AtOrWhenFunction;

            Assert.Equal(expected, func());
        }
    }
}
