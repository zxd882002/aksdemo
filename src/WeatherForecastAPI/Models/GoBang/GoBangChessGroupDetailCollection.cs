using System.Collections.Generic;
using System.Linq;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangChessGroupDetailCollection
    {
        private List<GoBangChessGroupDetail> _goBangChessGroupDetailList = new List<GoBangChessGroupDetail>();

        public void Clear() => _goBangChessGroupDetailList.Clear();

        public void AddRange(List<GoBangChessGroupDetail> goBangChessGroupDetailList)
        {
            _goBangChessGroupDetailList.AddRange(goBangChessGroupDetailList);
        }

        public long SumSore(GoBangChessType goBangChessType) =>
            _goBangChessGroupDetailList
                .Where(detail => detail.GoBangChessGroupDefinition.GoBangChess == goBangChessType)
                .Sum(detail => detail.GoBangChessGroupDefinition.Score);


        public bool ContainsAlreadyWinDefinition(GoBangChessType goBangChessType) =>
            _goBangChessGroupDetailList
                .Exists(detail =>
                    detail.GoBangChessGroupDefinition.GoBangChess == goBangChessType &&
                    detail.GoBangChessGroupDefinition.AlreadyWin == true);

        public bool ContainsEnemyMustFollow(GoBangChessType goBangChessType) =>
            _goBangChessGroupDetailList.Any(x =>
                x.GoBangChessGroupDefinition.EnemyMustFollow &&
                x.GoBangChessGroupDefinition.GoBangChess == goBangChessType);

        public List<GoBangChessPosition> GetMustFollowChessPosition(GoBangChessType goBangChessType, GoBangBoard board)
        {
            var whiteMustFollowDetailList = _goBangChessGroupDetailList.Where(x =>
                x.GoBangChessGroupDefinition.EnemyMustFollow &&
                x.GoBangChessGroupDefinition.GoBangChess == goBangChessType);

            List<GoBangChessPosition> mustFollowPositionList = new List<GoBangChessPosition>();
            bool couldFollowByAddingFourChess = true;
            foreach (var chessGroupDetail in whiteMustFollowDetailList)
            {
                couldFollowByAddingFourChess &= chessGroupDetail.GoBangChessGroupDefinition.CouldFollowByAddingFourChess;
                mustFollowPositionList.AddRange(chessGroupDetail.GoBangChessGroupDefinition.GetFollowingPosition(chessGroupDetail.GetDefinitionChesses(board)));
            }

            if (couldFollowByAddingFourChess)
            {
                var detailList = _goBangChessGroupDetailList.Where(x => x.GoBangChessGroupDefinition.GoBangChess == GoBangChessType.WhiteChess);
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
