namespace VolskSoft.Bibliotheca
{
    using System;
    using System.Configuration;

    public static class Defaults
    {
        public static readonly TimeSpan RetryWaitTimeSpan = TimeSpan.Parse(ConfigurationManager.AppSettings["default:retry-wait-time"]);
        public static readonly int RetryTimes = int.Parse(ConfigurationManager.AppSettings["default:retry-times"]);
    }
}