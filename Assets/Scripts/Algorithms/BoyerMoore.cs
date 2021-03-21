using System;
using System.Collections.Generic;

public class BoyerMoore : Algorithm
{
    /*
    int[] ZFunction(string s)
    {
        int n = s.Length;
        int[] z = new int[n];
        z[0] = 0;
        for (int i = 1, left = 0, right = 0; i < n; ++i)
        {
            if (i <= right)
            {
                z[i] = Math.Min(right - i + 1, z[i-1]);
            }
            while (i + z[i] < n && s[z[i]] == s[i + z[i]])
            {
                z[i]++;
            }
            if (i + z[i] - 1 > right)
            {
                left = i; right = i + z[i] - 1;
            }
        }
        return z;
    }

    int[] NFunction(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        string reversed = new string(charArray);
        int[] z = ZFunction(reversed);
        Array.Reverse(z);
        return z;
    }
    */

    public override void GetPatternEntries(string text, string pattern, List<int> result, ref int comparisons)
    {
        comparisons = 0;

        int textLength = text.Length;
        int patternLength = pattern.Length;
        int i = patternLength - 1, j;

        // BadChars preprocessing
        Dictionary<char, int> badChars = new Dictionary<char, int>();
        for (; i >= 0; --i)
        {
            if (!badChars.ContainsKey(pattern[i])) badChars.Add(pattern[i], i);
        }

        // GoodSuffixes preprocessing
        int[] goodSuffixes = new int[patternLength];

        int[] suffix = new int[patternLength];
        int left = 0, right = patternLength - 1;
        suffix[right] = patternLength;

        for (i = patternLength - 2; i > -1; --i) // suffix calculation
        {
            if (i > right && suffix[i + patternLength - 1 - left] < i - right)
                suffix[i] = suffix[i + patternLength - 1 - left];
            else
            {
                if (i < right) right = i;
                left = i;
                comparisons++;
                while (right > -1 && pattern[right] == pattern[right + patternLength - 1 - left])
                {
                    --right;
                    comparisons++;
                }
                if (right < 0) comparisons--;
                suffix[i] = left - right;
            }
        }

        for (i = 0; i < patternLength; ++i)
        {
            goodSuffixes[i] = patternLength;
        }
        j = 0;
        for (i = patternLength - 2; i > -1; --i)
        {
            if (suffix[i] == i + 1)
            {
                for (; j < patternLength - 1 - i; ++j)
                {
                    if (goodSuffixes[j] == patternLength) goodSuffixes[j] = patternLength - 1 - i;
                }
            }
        }
        for (i = 0; i < patternLength - 1; ++i)
        {
            goodSuffixes[patternLength - 1 - suffix[i]] = patternLength - 1 - i;
        }

        // Main part
        i = 0;
        while (i <= textLength - patternLength)
        {
            j = patternLength - 1;
            comparisons++;
            while (j > -1 && text[i + j] == pattern[j])
            {
                j--;
                comparisons++;
            }
            if (j < 0)
            {
                result.Add(i);
                i += goodSuffixes[0];
                comparisons--;
            }
            else i += Math.Max(goodSuffixes[j], j - (badChars.ContainsKey(text[i + j]) ? badChars[text[i + j]] : -1));
        }
    }
}