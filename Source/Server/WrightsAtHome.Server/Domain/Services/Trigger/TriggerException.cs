using System;

namespace WrightsAtHome.Server.Domain.Services.Trigger
{
    public class TriggerException : Exception
    {
        public TriggerException(string msg) : base(msg) { }
    }
}
