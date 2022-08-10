using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherForecastAPI.Models.GoBang
{
    public delegate List<GoBangChessPosition> GetFollowingPointDelegate(List<GoBangChess> chessList);

    public class GoBangChessGroupDefinition
    {
        public int DefinitionId { get; set; }
        public GoBangChessGroupType Type { get; set; }
        public GoBangChessType GoBangChess { get; set; }
        public GoBangChessType EnemyChess { get; set; }
        public List<GoBangChessType> Pattern { get; set; } = null!;
        public long Score { get; set; }
        public bool AlreadyWin { get; set; }
        public bool EnemyMustFollow { get; set; }
        public GetFollowingPointDelegate GetFollowingPosition { get; set; } = null!;
        public bool CouldFollowByAddingFourChess { get; set; }
        public GetFollowingPointDelegate AddToFourChess { get; set; } = null!;

        public GoBangChessGroupDefinition ReverseGoBangChessGroup()
        {
            return new GoBangChessGroupDefinition
            {
                DefinitionId = 100 + DefinitionId,
                Type = Type,
                GoBangChess = ReverseChess(GoBangChess),
                EnemyChess = ReverseChess(EnemyChess),
                Pattern = Pattern.Select(x => ReverseChess(x)).ToList(),
                Score = Score,
                AlreadyWin = AlreadyWin,
                EnemyMustFollow = EnemyMustFollow,
                GetFollowingPosition = GetFollowingPosition,
                CouldFollowByAddingFourChess = CouldFollowByAddingFourChess,
                AddToFourChess = AddToFourChess
            };
        }
        private GoBangChessType ReverseChess(GoBangChessType originalChess)
        {
            return originalChess == GoBangChessType.BlackChess
                ? GoBangChessType.WhiteChess
                : originalChess == GoBangChessType.WhiteChess
                    ? GoBangChessType.BlackChess
                    : originalChess;
        }

        public bool IsMatch(List<GoBangChess> goBangChesses)
        {
            if (goBangChesses.Count != Pattern.Count)
                return false;

            for (int i = 0; i < goBangChesses.Count; i++)
            {
                if (i == 0 && goBangChesses[i].ChessType == GoBangChessType.Wall && Pattern[i] == EnemyChess)
                    continue;

                if (i == goBangChesses.Count - 1 && goBangChesses[i].ChessType == GoBangChessType.Wall && Pattern[i] == EnemyChess)
                    continue;

                if (goBangChesses[i].ChessType != Pattern[i])
                    return false;
            }

            return true;
        }
    }
}
