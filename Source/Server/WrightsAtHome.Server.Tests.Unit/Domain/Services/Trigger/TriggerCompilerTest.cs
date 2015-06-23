using System;
using System.Collections.Generic;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger;
using WrightsAtHome.Server.Domain.Services.Trigger.Compiler;
using WrightsAtHome.Server.Tests.Unit.Utility;
using Xunit;

namespace WrightsAtHome.Server.Tests.Unit.Domain.Services.Trigger
{
    public class TriggerCompilerTest
    {
        [Theory]
        [InlineData("11:00pm", "11:01PM", false, TriggerType.At)]
        [InlineData("11:01pm", "11:00PM", true, TriggerType.At)]
        [InlineData("11:15am", "9:50pm", false, TriggerType.At)]
        [InlineData("11:00:01PM", "9:50PM", true, TriggerType.At)]
        public void TimeGreaterReturnsTrue(string currentTime, string atTime, bool funcResult, TriggerType triggerType)
        {
            // Arrange
            var trigger = BuildTestTrigger("AT " + atTime);
            var underTest = new TriggerCompiler(new TriggerHelpersFake { CurrentTime = currentTime });

            // Act
            var info = underTest.CompileTrigger(trigger);

            // Assert
            Assert.Equal(triggerType, info.TriggerType);
            Assert.Equal(DateTime.Parse(atTime), info.TriggerStartTime);
            Assert.Equal(funcResult, info.AtOrWhenFunction());
        }

        [Fact]
        public void InvalidTimeStrings()
        {
            // Arrange
            var trigger = BuildTestTrigger("AT 25:00");
            var underTest = new TriggerCompiler(new TriggerHelpersFake { CurrentTime = "11:00pm" });

            // Act, Assert
            var ex = Assert.Throws<TriggerException>(() => underTest.CompileTrigger(trigger));
            Assert.Equal("mismatched input '25' expecting TIMECONST", ex.Message);
        }

        [Theory]
        [InlineData("7:00am", "7:01am", "AFTER 1 MINUTE", true, 0, 1)]
        [InlineData("7:00", "7:04", "after 5 minutes", false, 0, 5)]
        [InlineData("1:00pm", "1:59pm", "after 1 hour", false, 1, 0)]
        [InlineData("1:00pm", "2:00pm", "AFTER 1 HOUR", true, 1, 0)]
        [InlineData("9:00pm", "11:59pm", "after 3 HOURS", false, 3, 0)]
        [InlineData("8:59pm", "11:59pm", "after 3 hours", true, 3, 0)]
        [InlineData("1/1/2015 11:00pm", "1/2/2015 1:00am", "after 2 hours", true, 2, 0)]
        [InlineData("1/1/2015 11:00pm", "1/2/2015 12:59am", "after 2 hours", false, 2, 0)]

        public void AfterTests(string startTime, string currentTime, string triggerText, bool result, int hours, int minutes)
        {
            // Arrange
            var trigger = BuildTestTrigger(triggerText);
            var underTest = new TriggerCompiler(new TriggerHelpersFake { CurrentTime = currentTime });

            // Act
            var info = underTest.CompileTrigger(trigger);

            Assert.Equal(TriggerType.After, info.TriggerType);
            Assert.Equal(new TimeSpan(hours, minutes, 0), info.TriggerAfterDelay);
            Assert.Equal(result, info.AfterFunction(DateTime.Parse(startTime)));
        }


        [Theory]
        [InlineData("WHEN PoolTemp < 80", true)]
        [InlineData("WHEN PoolTemp <= 78", true)]
        [InlineData("WHEN PoolTemp - AirTemp <= -8", true)]
        [InlineData("WHEN AirTemp + (- PoolTemp) > 10", false)]
        [InlineData("WHEN AirTemp - PoolTemp > 5 AND (LightLevel > 15 OR AirTemp > 80)", true)]
        [InlineData("WHEN AirTemp - PoolTemp > 5 AND not (LightLevel > 15.5 OR AirTemp > 80)", false)]
        [InlineData("WHEN AirTemp - PoolTemp > 5 AND CurrentTime > 8:00pm", true)]
        [InlineData("WHEN AirTemp - PoolTemp > 5 AND currentTime > 8:01pm", false)]
        public void WhenTests(string triggerText, bool expected)
        {
            // Arrange
            var helper = new TriggerHelpersFake
            {
                CurrentTime = "8:01pm",
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
        [InlineData("WHEN PoolTemp < 80", "PoolTemp", 5, 8)]
        [InlineData("WHEN someSENSOR = 1", "someSENSOR", 5, 10)]
        [InlineData("WHEN \nOnNewLine <= 100", "OnNewLine", 6, 9)]
        public void InvalidSensor(string triggerText, string sensor, int start, int length)
        {
            // Arrange
            var trigger = BuildTestTrigger(triggerText);
            var underTest = new TriggerCompiler(new TriggerHelpersFake());

            // Act, Assert
            var ex = Assert.Throws<TriggerException>(() => underTest.CompileTrigger(trigger));
            
            Assert.Equal(start, ex.StartIndex);
            Assert.Equal(length, ex.Length);
            Assert.Equal(string.Format("'{0}' is not a valid sensor name", sensor), ex.Message);
        }

        [Theory]
        [InlineData("at 11:00PM", "AT 11:00pm")]
        [InlineData("after 30 minutes", "AFTER 30 minutes")]
        [InlineData("when AirTemp < 40", "WHEN AirTemp < 40")]
        public void EventDescriptionTests(string triggerText, string expected)
        {
            var helper = new TriggerHelpersFake
            {
                CurrentTime = "10:59pm",
                SensorReadings = new Dictionary<string, decimal> {{"AirTemp", 30m}}
            };
            var trigger = BuildTestTrigger(triggerText);
            var underTest = new TriggerCompiler(helper);

            // Act
            var info = underTest.CompileTrigger(trigger);

            // Assert
            Assert.Equal("Turn On " + expected, info.EventDescription);
            
        }
        
        private DeviceTrigger BuildTestTrigger(string triggerText)
        {
            return new DeviceTrigger
            {
                TriggerText = triggerText,
                ToState = new DeviceState {Name = "On"}
            };
        }
    }
}
