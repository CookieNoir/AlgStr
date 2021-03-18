using System;
using System.Collections.Generic;

public class BoyerMoore : Algorithm
{
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

        int[] suffixArray = new int[patternLength];
        int left = 0, right = patternLength - 1;
        suffixArray[right] = patternLength;

        for (i = right - 1; i >= 0; --i)
        {
            if (i > right && suffixArray[i + patternLength - 1 - left] < i - right)
                suffixArray[i] = suffixArray[i + patternLength - 1 - left];
            else
            {
                if (i < right) right = i;
                left = i;
                comparisons++;
                while (right >= 0 && pattern[right] == pattern[right + patternLength - 1 - left])
                {
                    --right;
                    comparisons++;
                }
                if (right < 0) comparisons--;
                suffixArray[i] = left - right;
            }
        }

        for (i = 0; i < patternLength; ++i)
        {
            goodSuffixes[i] = patternLength;
        }
        j = 0;
        for (i = patternLength - 1; i >= 0; --i)
        {
            if (suffixArray[i] == i + 1)
            {
                for (; j < patternLength - 1 - i; ++j)
                {
                    if (goodSuffixes[j] == patternLength) goodSuffixes[j] = patternLength - 1 - i;
                }
            }
        }
        for (i = 0; i < patternLength - 1; ++i)
        {
            goodSuffixes[patternLength - 1 - suffixArray[i]] = patternLength - 1 - i;
        }

        // Основная часть
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
            else i += Math.Max(goodSuffixes[j], j + (badChars.ContainsKey(text[i + j]) ? badChars[text[i + j]] : 0));
        }
    }
}