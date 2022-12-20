using System.Threading.Tasks;
using WeatherForecastAPI.Infrastructure.Entensions;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public class ValueTreeNode<T> : AbstractTreeNode<T>
        where T : IValueableTreeNodeElement<T>
    {
        internal override long Value => Element.GetValue().Result;

        internal override async Task<AbstractTreeNode<T>> GetWinnerNode(long alpha, long beta)
        {
            return await Task.FromResult(this);
        }
    }
}