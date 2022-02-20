﻿using Microsoft.Extensions.Logging;

namespace AmbienTown.Utils.Logs
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
    }
}