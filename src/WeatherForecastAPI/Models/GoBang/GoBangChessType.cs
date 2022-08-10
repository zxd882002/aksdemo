using System;

namespace WeatherForecastAPI.Models.GoBang
{
    public struct GoBangChessType : IEquatable<GoBangChessType>
    {
        public int Value { get; set; }

        public static GoBangChessType Parse(int value)
        {
            return new GoBangChessType { Value = value };
        }

        public static readonly GoBangChessType Blank = new GoBangChessType { Value = 0 };
        public static readonly GoBangChessType BlackChess = new GoBangChessType { Value = 1 };
        public static readonly GoBangChessType WhiteChess = new GoBangChessType { Value = 2 };
        public static readonly GoBangChessType Wall = new GoBangChessType { Value = 3 };

        public bool Equals(GoBangChessType other)
        {
            return Value == other.Value;
        }

        public static bool operator ==(GoBangChessType type1, GoBangChessType type2)
        {
            return type1.Equals(type2);
        }
        public static bool operator !=(GoBangChessType type1, GoBangChessType type2)
        {
            return !type1.Equals(type2);
        }
    }
}