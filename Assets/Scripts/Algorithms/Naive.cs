using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naive : Algorithm
{
    public override void GetPatternEntries(string text, string pattern, List<int> result)
    {
        int textLength = text.Length;
        int patternLength = pattern.Length;
        for (int i = 0; i < textLength - patternLength + 1; ++i)
        {
            bool match = true;
            for (int j = 0; j < patternLength; ++j)
            {
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