using System.Collections.Generic;
using WeatherForecastAPI.Infrastructure.AlphaBetaTree;

namespace WeatherForecastAPI.Models.GoBang
{
    public interface IGoBangNextStepAnalyzer
    {
        ValuedGoBangBoard? AnalyzeNextStep(GoBangBoard goBangBoard, int deep);
    }

    public class GoBangNextStepAnalyzer : IGoBangNextStepAnalyzer
    {
        private readonly IGoBangBoardFactory _factory;

        public GoBangNextStepAnalyzer(IGoBangBoardFactory factory)
        {
            _factory = factory;
        }

        public ValuedGoBangBoard? AnalyzeNextStep(GoBangBoard goBangBoard, int deep)
        {
            ValuedGoBangBoard valuedGoBangBoard = new ValuedGoBangBoard(goBangBoard, _factory, new GoBangAnalyzer(goBangBoard));
            AbstractTreeNode<ValuedGoBangBoard> tree = goBangBoard.LastChess.ChessType == GoBangChessType.WhiteChess
                ? TreeNodeCreator<ValuedGoBangBoard>.CreateMinRootNode(valuedGoBangBoard, deep)
                : TreeNodeCreator<ValuedGoBangBoard>.CreateMaxRootNode(valuedGoBangBoard, deep);
            AbstractTreeNode<ValuedGoBangBoard> winnerNode = tree.GetWinnerNode(long.MinValue, long.MaxValue);

            List<GoBangChessPosition> list = new List<GoBangChessPosition>();
            ValuedGoBangBoard? board = null;
            while (winnerNode.ParentNode != null)
            {
                board = winnerNode.Element;
                list.Insert(0, winnerNode.Element.GetPosition());
                winnerNode = winnerNode.ParentNode;
            }
            return board;
        }
    }
}
