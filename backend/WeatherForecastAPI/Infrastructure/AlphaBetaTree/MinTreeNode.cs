using System.Collections.Generic;
using System.Linq;
using WeatherForecastAPI.Infrastructure.Entensions;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public class MinTreeNode<T> : AbstractTreeNode<T>
        where T : IValueableTreeNodeElement<T>
    {
        internal override AbstractTreeNode<T> GetWinnerNode(long alpha, long beta)
        {
            ChildNodes = CreateMaxTreeNode(Element, Deep - 1, this);
            foreach (var childNode in ChildNodes)
            {
                AbstractTreeNode<T> childWinnerNode = childNode.GetWinnerNode(alpha, beta);

                if (!WinnerNodeList.Any() || Value == childWinnerNode.Value)
                {
                    WinnerNodeList.Add(childWinnerNode);
                }

                if (Value < childWinnerNode.Value)
                {
                    WinnerNodeList.Clear();
                    WinnerNodeList.Add(childWinnerNode);
                }

                if (childWinnerNode.Value < beta)
                {
                    beta = childWinnerNode.Value;
                }

                if (alpha > beta)
                {
                    break;
                }
            }

            if (WinnerNodeList.Count > 1)
            {
                int index = WinnerNodeList.Count.RollDice() - 1;
                return WinnerNodeList[index];
            }
            return WinnerNodeList[0];
        }

        private List<AbstractTreeNode<T>> CreateMaxTreeNode(T element, int deep, AbstractTreeNode<T> parentNode)
        {
            var childElements = element.GetChildElements().Result;

            if (!childElements.Any())
            {
                return new List<AbstractTreeNode<T>>();
            }

            if (deep != 0)
            {
                childElements = childElements.OrderBy(x => x.Value).Take(10).ToList();
            }

            List<AbstractTreeNode<T>> childTreeNodes = new List<AbstractTreeNode<T>>();
            foreach (T childElement in childElements)
            {
                AbstractTreeNode<T> childTreeNode;
                if (deep == 0)
                {
                    // let's stop here, set the child node as leaf node
                    childTreeNode = new ValueTreeNode<T>();
                    childTreeNode.Element = childElement;
                    childTreeNode.ParentNode = parentNode;
                    childTreeNode.Deep = deep;
                }
                else
                {

                    childTreeNode = new MaxTreeNode<T>();
                    childTreeNode.Element = childElement;
                    childTreeNode.ParentNode = parentNode;
                    childTreeNode.Deep = deep;
                }
                childTreeNodes.Add(childTreeNode);
            };

            return childTreeNodes;
        }
    }
}