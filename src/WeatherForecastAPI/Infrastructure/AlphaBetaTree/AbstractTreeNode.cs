using System.Collections.Generic;
using System.Linq;
using WeatherForecastAPI.Infrastructure.Entensions;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public abstract class AbstractTreeNode<T>
        where T : IValueableTreeNodeElement<T>
    {
        internal T Element { get; set; } = default!;

        public List<AbstractTreeNode<T>> WinnerNodeList { get; set; } = new List<AbstractTreeNode<T>>();

        internal virtual long Value => WinnerNodeList.FirstOrDefault()!.Value;

        internal AbstractTreeNode<T>? ParentNode { get; set; } = null!;

        internal List<AbstractTreeNode<T>>? ChildNodes { get; set; }

        internal abstract AbstractTreeNode<T> GetWinnerNode(long alpha, long beta);
    }
}