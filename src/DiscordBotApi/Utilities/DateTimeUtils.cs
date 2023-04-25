// -------------------------------------------------------------------------------------------------
// <copyright file="DateTimeUtils.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Utilities;

public static class DateTimeUtils
{
	public static DateTime ToDateTime(double epochTimeSeconds) => DateTime.UnixEpoch.AddSeconds(value: epochTimeSeconds);

	public static int ToEpochTimeMilliseconds(DateTime datetime) => (int)(datetime - DateTime.UnixEpoch).TotalMilliseconds;

	public static double ToEpochTimeSeconds(DateTime datetime) => (datetime - DateTime.UnixEpoch).TotalSeconds;
}