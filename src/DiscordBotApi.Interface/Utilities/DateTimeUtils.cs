// -------------------------------------------------------------------------------------------------
// <copyright file="DateTimeUtils.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Utilities;

public static class DateTimeUtils
{
	public static DateTime ToDateTime(double epochTimeSeconds) => DateTime.UnixEpoch.AddSeconds(value: epochTimeSeconds);

	public static int ToEpochTimeMilliseconds(DateTime datetime) =>
		(int)(datetime - DateTime.UnixEpoch).TotalMilliseconds;

	public static double ToEpochTimeSeconds(DateTime datetime) => (datetime - DateTime.UnixEpoch).TotalSeconds;
}