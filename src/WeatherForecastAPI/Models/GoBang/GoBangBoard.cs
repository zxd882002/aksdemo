using System.Collections.Generic;
using System.Linq;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangBoard
    {
        public const int BOARD_SIZE = 15;

        public GoBangChessType[,] Board { get; set; } = new GoBangChessType[BOARD_SIZE, BOARD_SIZE];

        public long BlackChessScore => GoBangChessGroupDetailCollection.SumSore(GoBangChessType.BlackChess);
        public long WhiteChessScore => GoBangChessGroupDetailCollection.SumSore(GoBangChessType.WhiteChess);

        public GoBangChessGroupDetailCollection GoBangChessGroupDetailCollection { get; set; } = new GoBangChessGroupDetailCollection();

        public void InitializeEmptyBoard()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    Board[i, j] = GoBangChessType.Blank;
                }
            }
            GoBangChessGroupDetailCollection = new GoBangChessGroupDetailCollection();
        }

        public GoBangBoard GetBoardInfoAfterPuttingChess(GoBangChess currentChess)
        {
            List<GoBangChessGroupDetail> matchedPositions;
            Board[currentChess.Position.Row, currentChess.Position.Column] = currentChess.Chess;

            GoBangChessGroupDetailCollection.Remove(currentChess);

            matchedPositions = GetMatchLine(currentChess, @"-");
            matchedPositions.ForEach(matchedPosition => GoBangChessGroupDetailCollection.Add(matchedPosition));

            matchedPositions = GetMatchLine(currentChess, @"|");
            matchedPositions.ForEach(matchedPosition => GoBangChessGroupDetailCollection.Add(matchedPosition));

            matchedPositions = GetMatchLine(currentChess, @"\");
            matchedPositions.ForEach(matchedPosition => GoBangChessGroupDetailCollection.Add(matchedPosition));

            matchedPositions = GetMatchLine(currentChess, @"/");
            matchedPositions.ForEach(matchedPosition => GoBangChessGroupDetailCollection.Add(matchedPosition));

            return this;
        }

        private List<GoBangChessGroupDetail> GetMatchLine(GoBangChess currentChess, string direction)
        {
            GoBangChess fromChess = currentChess;
            List<GoBangChessGroupDetail> matchedLines = new List<GoBangChessGroupDetail>();
            bool couldContinue = true;
            do
            {
                GoBangChess nextChess = GetNextChess(fromChess, direction);
                if (nextChess.Chess != GoBangChessType.Wall)
                {
                    couldContinue = false;
                    GoBangChessGroupDefinition[] definitions;

                    if (nextChess.Chess == GoBangChessType.BlackChess)
                    {
                        definitions = GoBangChessGroupDefinitionCollection.AllBlack;
                    }
                    else
                    {
                        definitions = GoBangChessGroupDefinitionCollection.AllWhite;
                    }

                    foreach (GoBangChessGroupDefinition definition in definitions)
                    {
                        int chessCount = definition.Pattern.Length;
                        List<GoBangChess> goBangChesses = GetChesses(fromChess, direction, chessCount);
                        if (ContainsCurrentChess(goBangChesses, currentChess))
                        {
                            couldContinue = true;

                            if (definition.IsMatch(goBangChesses))
                            {
                                matchedLines.Add(new GoBangChessGroupDetail(definition, goBangChesses, direction));
                            }
                        }
                    }
                }

                if (fromChess.Chess == GoBangChessType.Wall)
                {
                    break;
                }

                fromChess = fromChess.NegativeMove(this, direction);
            }
            while (couldContinue);

            return matchedLines;
        }

        private GoBangChess GetNextChess(GoBangChess currentChess, string direction)
        {
            while (true)
            {
                currentChess = currentChess.PositiveMove(this, direction);
                if (currentChess.Chess != GoBangChessType.Blank)
                {
                    return currentChess;
                }
            }
        }

        private List<GoBangChess> GetChesses(GoBangChess fromChess, string direction, int chessCount)
        {
            List<GoBangChess> chessList = new List<GoBangChess>(new[] { fromChess });
            GoBangChess currentChess = fromChess;
            for (int i = 1; i < chessCount; i++)
            {
                currentChess = currentChess.PositiveMove(this, direction);
                chessList.Add(currentChess);

                if (currentChess.Chess == GoBangChessType.Wall)
                {
                    break;
                }
            }
            return chessList;
        }

        private bool ContainsCurrentChess(List<GoBangChess> chesses, GoBangChess currentChess)
        {
            return chesses.Any(c => c.Position.Row == currentChess.Position.Row && c.Position.Column == currentChess.Position.Column);
        }

        public string Serialize()
        {
            return GoBangChessGroupDetailCollection.ConvertToString();
        }

        public void Deserialize(int[][] board, string definition)
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    Board[i, j] = (GoBangChessType)board[i][j];
                }
            }
            GoBangChessGroupDetailCollection.ConvertFromString(definition);
        }
    }
}
