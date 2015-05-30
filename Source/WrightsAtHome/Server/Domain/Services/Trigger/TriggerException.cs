using System;

namespace WrightsAtHome.BackEnd.Domain.Services.Trigger
{
    public class TriggerException : Exception
    {
        public TriggerException(string msg) : base(msg) { }
    }
}
