using System;

namespace WeatherForecastAPI.Models.GoBang
{
    public struct GoBangChessPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public GoBangChessPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public GoBangChessPosition PositiveMove(string direction, int step = 1)
        {
            int directRow = direction switch
            {
                @"|" => 1,
                @"-" => 0,
                @"\" => 1,
                @"/" => -1,
                _ => throw new NotSupportedException()
            };
            int directColumn = direction switch
            {
                @"|" => 0,
                @"-" => 1,
                @"\" => 1,
                @"/" => 1,
                _ => throw new NotSupportedException()
            };
            return Move(directRow, directColumn, step);
        }

        public GoBangChessPosition NegativeMove(string direction, int step = 1)
        {
            int directRow = direction switch
            {
                @"|" => -1,
                @"-" => 0,
                @"\" => -1,
                @"/" => 1,
                _ => throw new NotSupportedException()
            };
            int directColumn = direction switch
            {
                @"|" => 0,
                @"-" => -1,
                @"\" => -1,
                @"/" => -1,
                _ => throw new NotSupportedException()
            };
            return Move(directRow, directColumn, step);
        }

        private GoBangChessPosition Move(int directRow, int directColumn, int step = 1)
        {
            GoBangChessPosition position = new GoBangChessPosition
            {
                Row = Row + directRow * step,
                Column = Column + directColumn * step,
            };

            return position;
        }

        public bool IsInsideBoundage()
        {
            return 0 <= Row && Row <= ObsoleteGoBangBoard.BOARD_SIZE - 1 &&
                0 <= Column && Column <= ObsoleteGoBangBoard.BOARD_SIZE - 1;
        }
    }
}
