using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public interface IValueableTreeNodeElement<T>
        where T : IValueableTreeNodeElement<T>
    {
        long GetValue();
        List<T> GetChildElements();
    }
}