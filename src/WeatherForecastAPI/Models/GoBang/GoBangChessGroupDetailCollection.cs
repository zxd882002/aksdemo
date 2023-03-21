using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangChessGroupDetailCollection
    {
        private ConcurrentBag<GoBangChessGroupDetail> _goBangChessGroupDetailList = new ConcurrentBag<GoBangChessGroupDetail>();

        public void Add(GoBangChessGroupDetail goBangChessGroupDetail)
        {
            if (goBangChessGroupDetail == null)
            {
                System.Console.WriteLine("INSERT NULL");
            }

            _goBangChessGroupDetailList.Add(goBangChessGroupDetail);
        }

        public long SumSore(GoBangChessType goBangChessType) =>
            _goBangChessGroupDetailList
                .Where(detail => detail.GoBangChessGroupDefinition.GoBangChess == goBangChessType)
                .Sum(detail => detail.GoBangChessGroupDefinition.Score);

        public bool ContainsAlreadyWinDefinition(GoBangChessType goBangChessType) =>
            _goBangChessGroupDetailList
                .Any(detail =>
                    detail.GoBangChessGroupDefinition.GoBangChess == goBangChessType &&
                    detail.GoBangChessGroupDefinition.AlreadyWin == true);

        public List<GoBangChessPosition> GetMustFollowChessPosition(GoBangChessType goBangChessType, GoBangBoard board)
        {
            var mustFollowDetailList = _goBangChessGroupDetailList.Where(x =>
                x.GoBangChessGroupDefinition.EnemyMustFollow &&
                x.GoBangChessGroupDefinition.GoBangChess == goBangChessType);

            List<GoBangChessPosition> mustFollowPositionList = new List<GoBangChessPosition>();
            bool couldFollowByAddingFourChess = true;
            foreach (var chessGroupDetail in mustFollowDetailList)
            {
                couldFollowByAddingFourChess &= chessGroupDetail.GoBangChessGroupDefinition.CouldFollowByAddingFourChess;
                mustFollowPositionList.AddRange(chessGroupDetail.GoBangChessGroupDefinition.GetFollowingPosition(chessGroupDetail.GetDefinitionChesses(board)));
            }

            if (couldFollowByAddingFourChess)
            {
                var detailList = _goBangChessGroupDetailList.Where(x => x.GoBangChessGroupDefinition.GoBangChess == goBangChessType.ReverseChess());
                foreach (var chessGroupDetail in detailList)
                {
                    var addToFourChessPositions = chessGroupDetail.GoBangChessGroupDefinition.AddToFourChess(chessGroupDetail.GetDefinitionChesses(board));
                    if (addToFourChessPositions.Any())
                    {
                        mustFollowPositionList.AddRange(addToFourChessPositions);
                    }
                }
            }

            return mustFollowPositionList;
        }
    }
}
