using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherForecastAPI.Models.GoBang
{
    [Obsolete]
    public class ObsoleteGoBangChessGroupDetail
    {
        public GoBangChessGroupDefinition GoBangChessGroupDefinition { get; }
        public List<GoBangChess> Chesses { get; }
        public string Direction { get; }

        public ObsoleteGoBangChessGroupDetail(GoBangChessGroupDefinition goBangChessGroupDefinition, List<GoBangChess> chesses, string direction)
        {
            GoBangChessGroupDefinition = goBangChessGroupDefinition;
            Chesses = chesses;
            Direction = direction;
        }

        /// format
        /// [definitionId] - 3 digit
        /// [direction] - could be "-", "|", "/", "\"
        /// [chesses] 5 * N (chess count) digit => 01023 => row = 1 (01), column = 2 (02), chess type = 3 (wall)
        /// example: "003-090500906109071090810909109100"
        /// 003 - 09050 09061 09071 09081 09091 09100
        /// ^   ^ ^  
        /// ^   ^ [chesses]
        /// ^   [direction]
        /// [definitionId]
        public string ConvertToString()
        {
            StringBuilder sb = new StringBuilder(5 * Chesses.Count);
            foreach (var c in Chesses)
            {
                sb.Append($"{c.Position.Row:00}{c.Position.Column:00}{c.ChessType.Value}");
            }

            return $"{GoBangChessGroupDefinition.DefinitionId:000}{Direction}{sb}";
        }

        public static ObsoleteGoBangChessGroupDetail? ConvertFromString(string s)
        {
            GoBangChessGroupDefinition? definition = default!;
            int definitionId = 0;
            string direction = default!;
            int row = 0;
            int column = 0;
            List<GoBangChess> chesses = new List<GoBangChess>();

            for (int i = 0; i < s.Length; i++)
            {
                if (0 <= i && i <= 2)
                {
                    definitionId = definitionId * 10 + (s[i] - '0');
                    if (i == 2)
                    {
                        definition = GoBangChessGroupDefinitionCollection.GetGoBangChessGroupDefinitionByDefinitionId(definitionId);
                    }
                }
                else if (i == 3)
                {
                    direction = s[i].ToString();
                }
                else
                {
                    int position = (i - 4) % 5;
                    if (0 <= position && position <= 1)
                    {
                        row = row * 10 + (s[i] - '0');
                    }
                    if (2 <= position && position <= 3)
                    {
                        column = column * 10 + (s[i] - '0');
                    }
                    if (position == 4)
                    {
                        int chessType = s[i] - '0';
                        GoBangChess goBangChess = new GoBangChess
                        {
                            Position = new GoBangChessPosition
                            {
                                Row = row,
                                Column = column,
                            },
                            ChessType = GoBangChessType.Parse(chessType),
                        };
                        chesses.Add(goBangChess);
                        row = 0;
                        column = 0;
                    }
                }
            }

            if (definition == null)
            {
                return null;
            }

            return new ObsoleteGoBangChessGroupDetail(definition, chesses, direction);
        }

        public List<GoBangChessPosition> GetMustFollowGoBangChessPosition()
        {
            return GoBangChessGroupDefinition.GetFollowingPosition(Chesses);
        }

        public List<GoBangChessPosition> GetAddToFourChessPosition()
        {
            return GoBangChessGroupDefinition.AddToFourChess(Chesses);
        }
    }
}
