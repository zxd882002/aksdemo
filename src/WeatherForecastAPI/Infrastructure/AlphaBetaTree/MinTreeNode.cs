using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Entensions;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public class MinTreeNode<T> : AbstractTreeNode<T>
        where T : IValueableTreeNodeElement<T>
    {
        internal override async Task<AbstractTreeNode<T>> GetWinnerNode(long alpha, long beta)
        {
            ChildNodes = await CreateMaxTreeNode(Element, Deep - 1, this);
            foreach (var childNode in ChildNodes)
            {
                AbstractTreeNode<T> childWinnerNode = await childNode.GetWinnerNode(alpha, beta);

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

            int index = WinnerNodeList.Count.RollDice() - 1;
            return WinnerNodeList[index];
        }

        private async Task<List<AbstractTreeNode<T>>> CreateMaxTreeNode(T element, int deep, AbstractTreeNode<T> parentNode)
        {
            var childElements = (await element.GetChildElements()).OrderBy(x => x.GetValue());
            if (!childElements.Any())
            {
                return new List<AbstractTreeNode<T>>();
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