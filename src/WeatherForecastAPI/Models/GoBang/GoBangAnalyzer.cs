using System;
using WeatherForecastAPI.Infrastructure.AlphaBetaTree;

namespace WeatherForecastAPI.Models.GoBang
{
    public interface IGoBangAnalyzer
    {
        GoBangBoard Analyze(GoBangBoard goBangBoard, int deep);
    }

    public class GoBangAnalyzer: IGoBangAnalyzer
    {
        public GoBangBoard Analyze(GoBangBoard goBangBoard, int deep)
        {
            var tree = TreeNodeCreator<GoBangBoard>.CreateMaxRootNode(goBangBoard, deep);
            var winnerNode = tree.GetWinnerNode(long.MinValue, long.MaxValue);
            throw new NotImplementedException();
            //while(winnerNode.ParentNode!.ParentNode != null)
            //{
            //    winnerNode = winnerNode.ParentNode;
            //}

            //return winnerNode.Element;
        }
    }
}
