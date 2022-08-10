namespace WeatherForecastAPI.Infrastructure.Entensions
{
    public class ArrayExtensions
    {
        public static int[] CreateInt(int fromInclude, int toExclude)
        {
            int[] array = new int[toExclude - fromInclude];

            for (int i = fromInclude, j = 0; i < toExclude; i++, j++)
            {
                array[j] = i;
            }

            return array;
        }
    }
}
