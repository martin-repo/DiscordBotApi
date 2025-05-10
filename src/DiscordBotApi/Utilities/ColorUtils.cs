// -------------------------------------------------------------------------------------------------
// <copyright file="ColorUtils.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Drawing;
using System.Globalization;

namespace DiscordBotApi.Utilities;

internal static class ColorUtils
{
	public static int? ColorToInt(Color? color)
	{
		if (color == null)
		{
			return null;
		}

		var colorHex = "0x" +
			color.Value.R.ToString(format: "X2") +
			color.Value.G.ToString(format: "X2") +
			color.Value.B.ToString(format: "X2");
		var colorInt = Convert.ToInt32(value: colorHex, fromBase: 16);
		return colorInt;
	}

	public static Color? IntToColor(int? colorInt)
	{
		if (colorInt == null)
		{
			return null;
		}

		var colorHex = colorInt.Value.ToString(format: "X6");
		var red = int.Parse(s: colorHex[..2], style: NumberStyles.HexNumber);
		var green = int.Parse(s: colorHex[2..4], style: NumberStyles.HexNumber);
		var blue = int.Parse(s: colorHex[4..], style: NumberStyles.HexNumber);

		var color = Color.FromArgb(red: red, green: green, blue: blue);
		return color;
	}
}