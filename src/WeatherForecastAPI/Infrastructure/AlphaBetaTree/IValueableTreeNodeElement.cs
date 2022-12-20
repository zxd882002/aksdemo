using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public interface IValueableTreeNodeElement<T>
        where T : IValueableTreeNodeElement<T>
    {
        Task<long> GetValue();
        Task<List<T>> GetChildElements();
    }
}