using System;
using System.Collections.Generic;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger.Validator;
using WrightsAtHome.Server.Tests.Unit.Utility;
using Xunit;
using Xunit.Sdk;

namespace WrightsAtHome.Server.Tests.Unit.Domain.Services.Trigger
{
    public class TriggerValidatorTest
    {
        [Theory]
        [InlineData("ATT BadTrigger", false, TriggerType.At, 1, "'ATT' is not a valid input", 0, "ATT")]
        [InlineData("AT BadTrigger", false, TriggerType.At, 1, "mismatched input 'BadTrigger' expecting TIMECONST", 3, "BadTrigger")]
        [InlineData("AT 25:00", false, TriggerType.At, 1, "mismatched input '25' expecting TIMECONST", 3, "25")]
        [InlineData("AT 12:61", false, TriggerType.At, 1, "mismatched input '12' expecting TIMECONST", 3, "12")]
        [InlineData("AT 12:53", true, TriggerType.At, 0, "", 0, "")]
        [InlineData("AT 5:27pm", true, TriggerType.At, 0, "", 0, "")]
        [InlineData("AT 5:27am", true, TriggerType.At, 0, "", 0, "")]
        [InlineData("AT 1:27", true, TriggerType.At, 0, "", 0, "")]
        [InlineData("wHeN ValidSensor > 1 AND InvalidSensor < 1", false, TriggerType.When, 1, "'InvalidSensor' is not a valid sensor name", 25, "InvalidSensor")]
        [InlineData("wHeN ValidSensor > 1 AND ((NOT (1-2)", false, TriggerType.When, 1, "mismatched input '' expecting ')'", 36, "")]
        [InlineData("wHeN ValidSensor > 1 AND (NOT (1-2))", true, TriggerType.When, 0, "", 0, "")]
        [InlineData("aftER 12 hoors", false, TriggerType.After, 1, "mismatched input 'hoors' expecting {MINUTES, HOURS}", 9, "hoors")]
        [InlineData("AFTER 42 mins", false, TriggerType.After, 1, "mismatched input 'mins' expecting {MINUTES, HOURS}", 9, "mins")]
        [InlineData("AFTER aa minutes", false, TriggerType.After, 1, "mismatched input 'aa' expecting INT", 6, "aa")]
        [InlineData("AFTER 12 minutes", true, TriggerType.After, 0, "", 0, "")]
        [InlineData("AFTER 42 HoUrS", true, TriggerType.After, 0, "", 0, "")]
        public void InvalidTrigger(string trigger, bool isValid, TriggerType triggerType, int errorCount, string msg, int startNdx, string token)
        {
            // Arrange
            var helpers = new TriggerHelpersFake { SensorReadings = new Dictionary<string, decimal> {{"ValidSensor", 1.0m}} };
            var underTest = new TriggerValidator(helpers);

            // Act
            var result = underTest.ValidateTrigger(trigger);

            // Assert
            Assert.Equal(isValid, result.IsValid);
            Assert.Equal(errorCount, result.Errors.Count);

            if (isValid)
            {
                Assert.Equal(triggerType, result.TriggerType);
            }
            else
            {
                Assert.Equal(msg, result.Errors[0].ErrorMessage);
                Assert.Equal(startNdx, result.Errors[0].StartIndex);
                Assert.Equal(token.Length, result.Errors[0].Length);
                Assert.Equal(token, result.Errors[0].BadToken);
            }
        }
    }
}
