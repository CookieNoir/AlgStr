using System.Collections.Generic;

public class KnuthMorrisPratt : Algorithm
{
    private const char uniqueSymbol = '|';

    private int[] PrefixFunction(string text)
    {
        int textLength = text.Length;
        int[] p = new int[textLength];
        p[0] = 0;
        for (int i = 1; i < textLength; ++i)
        {
            int k = p[i - 1];
            while (k > 0 && text[i] != text[k])
            {
                k = p[k - 1];
            }
            if (text[i] == text[k])
            {
                k++;
            }
            p[i] = k;
        }
        return p;
    }

    public override void GetPatternEntries(string text, string pattern, List<int> result)
    {
        int textLength = text.Length;
        int patternLength = pattern.Length;
        int patternLengthDual = patternLength + patternLength;
        int[] p = PrefixFunction(pattern + uniqueSymbol + text);
        for (int i = 0; i < textLength - patternLength + 1; ++i)
        {
            if (p[patternLengthDual + i] == patternLength)
            {
                result.Add(i);
            }
        }
    }
}
