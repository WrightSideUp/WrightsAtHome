using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace WrightsAtHome.Server.Domain.Entities
{
    public enum TriggerType
    {
        At = 0,
        When = 1,
        After = 2
    }

    public class TriggerInfo
    {
        public DeviceTrigger Trigger { get; set; }

        public string TriggerText { get; set; }

        public TriggerType TriggerType { get; set; }

        public string EventDescription { get; set; }

        public DateTime TriggerStartTime { get; set; }

        public TimeSpan TriggerAfterDelay { get; set; }

        public Func<bool> AtOrWhenFunction { get; set; }

        public Func<DateTime, bool> AfterFunction { get; set; }
    }
}