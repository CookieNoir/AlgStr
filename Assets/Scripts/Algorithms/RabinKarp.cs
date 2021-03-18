using System.Collections.Generic;

public class RabinKarp : Algorithm
{
    private const int basement = 59;
    private const int divider = 433494437;

    public override void GetPatternEntries(string text, string pattern, List<int> result, ref int comparisons)
    {
        comparisons = 0;
        int textLength = text.Length;
        int patternLength = pattern.Length;
        int hashText = text[0] % divider;
        int hashPattern = pattern[0] % divider;
        int multiplier = 1;
        int i = 1;

        for (; i < patternLength; ++i)
        {
            hashText = (hashText * basement + text[i]) % divider;
            hashPattern = (hashPattern * basement + pattern[i]) % divider;
            multiplier = (multiplier * basement) % divider;
        }

        for (i = 0; i < textLength - patternLength; ++i)
        {
            if (hashText == hashPattern)
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
                if (match) result.Add(i);
            }
            hashText = (basement * (hashText - multiplier * text[i]) + text[i + patternLength] + divider) % divider;
        }
        if (hashText == hashPattern)
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
            if (match) result.Add(i);
        }
    }
}