using System;
using System.ComponentModel.DataAnnotations;
using Humanizer;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class DeviceStateChange : IBaseEntity
    {
        public int Id { get; set; }

        public Device Device { get; set; }

        public DateTime AppliedDate { get; set; }

        public bool WasOverride { get; set; }

        public DeviceState BeforeState { get; set; }

        public DeviceState AfterState { get; set; }

        public DeviceTrigger FromTrigger { get; set; }

        public string GetDescription()
        {
            // Examples:
            // Changed from 'Off' to 'On' Today at 3:30pm
            // Changed from 'On' to 'Off' Yesterday at 11:00pm
            // Manually Changed from 'Off' to 'On Low' on 6/15 at 3:00am

            return string.Format("{0}Changed from '{1}' to '{2}' {3} at {4:t}",
                WasOverride ? "Manually " : string.Empty,
                BeforeState.Name,
                AfterState.Name,
                AppliedDate.Date.Humanize(),
                AppliedDate);
        }

        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }
    }
}
