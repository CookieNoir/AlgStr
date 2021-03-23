using System.Collections.Generic;
using UnityEngine;

public class KnuthMorrisPratt : Algorithm
{
    public override void GetPatternEntries(string text, string pattern, List<string> result, ref int comparisons)
    {
        comparisons = 0;

        int textLength = text.Length;
        int patternLength = pattern.Length;
        int[] p = new int[patternLength];
        p[0] = 0;
        int i = 1, j = 0;

        while (i < patternLength)
        {
            comparisons++;
            if (pattern[j] == pattern[i])
            {
                p[i] = j + 1;
                i++;
                j++;
            }
            else
            {
                if (j == 0)
                {
                    p[i] = 0;
                    i++;
                }
                else
                {
                    j = p[j - 1];
                }
            }
        }
        i = 0; j = 0;
        while (i <= textLength - patternLength + j)
        {
            comparisons++;
            while (j < patternLength && text[i] == pattern[j])
            {
                i++;
                j++;
                comparisons++;
            }
            if (j == patternLength)
            {
                result.Add((i - patternLength).ToString());
                comparisons--;
            }
            if (j > 0)
            {
                j = p[j - 1];
            }
            else
            {
                i++;
            }
        }
    }
}
