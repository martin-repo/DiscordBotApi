// -------------------------------------------------------------------------------------------------
// <copyright file="ColorUtils.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Utilities
{
    using System.Drawing;
    using System.Globalization;

    internal static class ColorUtils
    {
        public static int? ColorToInt(Color? color)
        {
            if (color == null)
            {
                return null;
            }

            var colorHex = "0x" + color.Value.R.ToString("X2") + color.Value.G.ToString("X2") + color.Value.B.ToString("X2");
            var colorInt = Convert.ToInt32(colorHex, 16);
            return colorInt;
        }

        public static Color? IntToColor(int? colorInt)
        {
            if (colorInt == null)
            {
                return null;
            }

            var colorHex = colorInt.Value.ToString("X6");
            var red = int.Parse(colorHex[..2], NumberStyles.HexNumber);
            var green = int.Parse(colorHex[2..4], NumberStyles.HexNumber);
            var blue = int.Parse(colorHex[4..], NumberStyles.HexNumber);

            var color = Color.FromArgb(red, green, blue);
            return color;
        }
    }
}