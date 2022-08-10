using WeatherForecastAPI.Infrastructure.Entensions;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public class ValueTreeNode<T> : AbstractTreeNode<T>
        where T : IValueableTreeNodeElement<T>
    {
        internal override long Value => Element.GetValue();

        internal override AbstractTreeNode<T> GetWinnerNode(long alpha, long beta)
        {
            return this;
        }
    }
}