using System.Collections.Generic;

public class Naive : Algorithm
{
    public override void GetPatternEntries(string text, string pattern, List<string> result, ref int comparisons)
    {
        comparisons = 0;
        int textLength = text.Length;
        int patternLength = pattern.Length;
        for (int i = 0; i < textLength - patternLength + 1; ++i)
        {
            bool match = true;
            for (int j = 0; j < patternLength; ++j)
            {
                comparisons++;
                if (text[i + j] != pattern[j])
                {
                    match = false;
                    break;
                }
            }
            if (match) result.Add(i.ToString());
        }
    }
}