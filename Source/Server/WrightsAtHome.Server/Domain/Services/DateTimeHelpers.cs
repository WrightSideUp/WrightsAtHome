using System;

namespace WrightsAtHome.Server.Domain.Services
{
    // Utility class used to get current date/time.  
    // Used to allow tests to fake.

    public interface IDateTimeHelpers
    {
        DateTime Now { get; }
    }
    
    public class DateTimeHelpers : IDateTimeHelpers
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}