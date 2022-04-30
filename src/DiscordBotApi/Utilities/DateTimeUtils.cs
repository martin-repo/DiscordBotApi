// -------------------------------------------------------------------------------------------------
// <copyright file="DateTimeUtils.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Utilities
{
    public static class DateTimeUtils
    {
        public static DateTime ToDateTime(double epochTimeSeconds)
        {
            return DateTime.UnixEpoch.AddSeconds(epochTimeSeconds);
        }

        public static int ToEpochTimeMilliseconds(DateTime datetime)
        {
            return (int)(datetime - DateTime.UnixEpoch).TotalMilliseconds;
        }

        public static double ToEpochTimeSeconds(DateTime datetime)
        {
            return (datetime - DateTime.UnixEpoch).TotalSeconds;
        }
    }
}