using System;
using System.Collections.Generic;
using System.Drawing;
namespace GameSetupForm
{
    public enum EColorEnum
    {
        Purple = 1,
        Red = 2,
        Green = 3,
        Teal = 4,
        Blue = 5,
        Yellow = 6,
        Maroon = 7,
        White = 8
    }

    public static class EColorEnumExtension
    {
        private static readonly Dictionary<EColorEnum, Color> ColorMap = new Dictionary<EColorEnum, Color>
        {
            { EColorEnum.Purple, Color.Purple },
            { EColorEnum.Red, Color.Red },
            { EColorEnum.Green, Color.Green },
            { EColorEnum.Teal, Color.Teal },
            { EColorEnum.Blue, Color.Blue },
            { EColorEnum.Yellow, Color.Yellow },
            { EColorEnum.Maroon, Color.Maroon },
            { EColorEnum.White, Color.White }
        };

        public static Color ToColor(this EColorEnum colorEnum)
        {
            return ColorMap[colorEnum];
        }

        public static EColorEnum ToColorEnum(this Color color)
        {
            foreach (KeyValuePair<EColorEnum, Color> pair in ColorMap)
            {
                if (pair.Value == color)
                    return pair.Key;
            }
            throw new ArgumentException("Color not found", nameof(color));
        }
    }
}
