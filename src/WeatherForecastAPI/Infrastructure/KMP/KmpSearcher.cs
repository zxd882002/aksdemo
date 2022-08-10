using System;
using System.Collections.Generic;

namespace WeatherForecastAPI.Infrastructure.KMP
{
    public class KmpSearcher<T>
         where T : IEquatable<T>
    {
        public List<int> Search(List<T> searchFrom, List<T> matchPattern)
        {
            int searchFromLength = searchFrom.Count;
            int matchPatternLength = matchPattern.Count;

            if (searchFromLength < matchPatternLength)
                return new List<int>();

            int[] next = GetNext(matchPattern);

            int i = 0;
            int j = 0;
            List<int> matchedIndexList = new List<int>();
            while (i < searchFromLength && j < matchPatternLength)
            {
                if (searchFrom[i].Equals(matchPattern[j]))
                {
                    i++;
                    j++;

                    if (j == matchPatternLength)
                    {
                        matchedIndexList.Add(i - j);
                        j = 0;
                    }
                }
                else
                {
                    if (next[j] != -1)
                    {
                        j = next[j];
                    }
                    else
                    {
                        j = 0;
                        i++;
                    }
                }
            }
            return matchedIndexList;
        }

        public int[] GetNext(List<T> matchPattern)
        {
            int j = 0;
            int k = -1;
            var next = new int[matchPattern.Count];
            next[0] = -1;
            while (j < matchPattern.Count - 1)
            {
                if (k == -1 || matchPattern[j].Equals(matchPattern[k]))
                {
                    j++;
                    k++;
                    if (matchPattern[j].Equals(matchPattern[k]))
                    {
                        next[j] = next[k];
                    }
                    else
                    {
                        next[j] = k;
                    }
                }
                else
                {
                    k = next[k];
                }
            }

            return next;
        }
    }
}
