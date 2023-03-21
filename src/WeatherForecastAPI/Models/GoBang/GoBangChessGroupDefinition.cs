using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherForecastAPI.Models.GoBang
{
    public delegate List<GoBangChessPosition> GetFollowingPointDelegate(List<GoBangChess> chessList);

    public class GoBangChessGroupDefinition
    {
        public string DefinitionName { get; set; }
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
                DefinitionName = DefinitionName,
                Type = Type,
                GoBangChess = GoBangChess.ReverseChess(),
                EnemyChess = EnemyChess.ReverseChess(),
                Pattern = Pattern.Select(x => x.ReverseChess()).ToList(),
                Score = Score,
                AlreadyWin = AlreadyWin,
                EnemyMustFollow = EnemyMustFollow,
                GetFollowingPosition = GetFollowingPosition,
                CouldFollowByAddingFourChess = CouldFollowByAddingFourChess,
                AddToFourChess = AddToFourChess
            };
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
