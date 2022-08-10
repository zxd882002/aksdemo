using System;
using System.Linq;
using WeatherForecastAPI.Infrastructure.Entensions;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public class MaxTreeNode<T> : AbstractTreeNode<T>
        where T : IValueableTreeNodeElement<T>
    {
        internal override AbstractTreeNode<T> GetWinnerNode(long alpha, long beta)
        {
            if (ChildNodes == null)
            {
                throw new Exception("child node should not be null");
            }

            foreach (var childNode in ChildNodes)
            {
                AbstractTreeNode<T> childWinnerNode = childNode.GetWinnerNode(alpha, beta);

                if (!WinnerNodeList.Any() || Value == childWinnerNode.Value)
                {
                    WinnerNodeList.Add(childWinnerNode);
                }

                if ( Value < childWinnerNode.Value)
                {
                    WinnerNodeList.Clear();
                    WinnerNodeList.Add(childWinnerNode);
                }

                if (alpha < childWinnerNode.Value)
                {
                    alpha = childWinnerNode.Value;
                }

                if (alpha >= beta)
                {
                    break;
                }
            }

            int index = WinnerNodeList.Count.RollDice() - 1;
            return WinnerNodeList[index];
        }
    }
}