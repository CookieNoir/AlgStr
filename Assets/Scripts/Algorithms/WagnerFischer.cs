using System;
using System.Collections.Generic;

public class WagnerFischer : Algorithm
{
    public override void GetPatternEntries(string text, string pattern, List<string> result, ref int comparisons)
    {
        comparisons = 0;
        int n = text.Length;
        int m = pattern.Length;

        /*// Memory Efficient Version
        int[] prev = new int[m + 1];
        int[] cur = new int[m + 1];
        for (int i = 0; i <= m; ++i)
        {
            prev[i] = i;
        }
        for (int i = 0; i < n; ++i)
        {
            cur[0] = i + 1;
            for (int j = 0, ji = 1; j < m; ++j, ++ji)
            {
                comparisons++;
                if (text[i] == pattern[j])
                {
                    cur[ji] = prev[j];
                }
                else
                {
                    int remove = prev[ji] + 1;
                    int insertion = cur[j] + 1;
                    int substitution = prev[j] + 1;
                    cur[ji] = Math.Min(Math.Min(remove, insertion), substitution);
                }
            }
            Array.Copy(cur, prev, m + 1);
        }
        result.Add("\r\n" + "Количество изменений:  " + (cur[m]).ToString());
        */

        int[,] d = new int[n + 1, m + 1];
        int i, ii, j, ji;

        for (i = 0; i <= m; ++i)
        {
            d[0, i] = i;
        }
        for (i = 0, ii = 1; i < n; ++i, ++ii)
        {
            d[ii, 0] = ii;
            for (j = 0, ji = 1; j < m; ++j, ++ji)
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

        ii = n;
        i = n - 1;
        ji = m;
        j = m - 1;
        while (ii > 0 && ji > 0)
        {
            if (d[ii, ji] == d[i, ji] + 1)
            {
                ii--; i--;
            }
            else if (d[ii, ji] == d[ii, j] + 1)
            {
                ji--; j--;
            }
            else
            {
                result.Add(String.Format("({0}, {1})", ii, ji));
                ii--; i--; ji--; j--;
            }
        }

        result.Add("\r\nКоличество изменений: " + d[n, m].ToString());
    }
}
