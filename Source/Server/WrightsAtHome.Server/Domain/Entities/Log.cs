﻿
using System;

namespace WrightsAtHome.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        
        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }
    }
}