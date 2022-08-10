using System.Collections.Generic;
using System.Linq;

namespace WeatherForecastAPI.Models.GoBang
{
    public class GoBangChessGroupDetailCollection
    {
        private List<GoBangChessGroupDetail> _goBangChessGroupDetailList = new List<GoBangChessGroupDetail>();

        public void AddRange(List<GoBangChessGroupDetail> goBangChessGroupDetailList)
        {
            _goBangChessGroupDetailList.AddRange(goBangChessGroupDetailList);
        }

        public long SumSore(GoBangChessType goBangChessType)
        {
            return _goBangChessGroupDetailList
                .Where(detail => detail.GoBangChessGroupDefinition.GoBangChess == goBangChessType)
                .Sum(detail => detail.GoBangChessGroupDefinition.Score);
        }
    }
}
