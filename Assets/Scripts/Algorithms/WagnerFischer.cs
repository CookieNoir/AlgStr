using System;
using System.Collections.Generic;

public class WagnerFischer : Algorithm
{
    public override void GetPatternEntries(string text, string pattern, List<int> result, ref int comparisons)
    {
        comparisons = 0;
        int n = text.Length;
        int m = pattern.Length;
        int[,] d = new int[n + 1, m + 1];

        for (int i = 0; i <= m; ++i)
        {
            d[0, i] = i;
        }
        for (int i = 0, ii = 1; i < n; ++i, ++ii)
        {
            d[ii, 0] = ii;
            for (int j = 0, ji = 1; j < m; ++j, ++ji)
            {
                comparisons++;
                if (text[i] == pattern[j])
                {
                    d[ii, ji] = d[i, j];
                }
                else
                {
                    d[ii, ji] = Math.Min(Math.Min(d[ii, j] + 1, d[i, j] + 1), d[i, ji] + 1);
                }
            }
        }
        result.Add(d[n, m]);
    }
}
