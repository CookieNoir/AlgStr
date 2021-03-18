using System;
using System.Collections.Generic;

public class BaezaYatesGonnet : Algorithm
{
    private void RightShift(ref bool[] bitArray, int arrayLength)
    {
        for (int i = arrayLength - 1; i > 0; --i)
        {
            bitArray[i] = bitArray[i - 1];
        }
        bitArray[0] = false;
    }

    private void Or(ref bool[] bitArray, bool[] mask, int arrayLength)
    {
        for (int i = 0; i < bitArray.Length; ++i)
        {
            bitArray[i] |= mask[i];
        }
    }

    public override void GetPatternEntries(string text, string pattern, List<int> result, ref int comparisons)
    {
        comparisons = 0;
        int textLength = text.Length;
        int patternLength = pattern.Length;
        int i = 0;

        bool[] s = new bool[patternLength];
        for (; i < patternLength; ++i)
        {
            s[i] = true;
        }

        Dictionary<char, bool[]> t = new Dictionary<char, bool[]>();

        for (i = 0; i < patternLength; ++i)
        {
            if (!t.ContainsKey(pattern[i]))
            {
                bool[] newArray = new bool[patternLength];
                Array.Copy(s, newArray, patternLength);
                t.Add(pattern[i], newArray);
            }
            t[pattern[i]][i] = false;
        }

        for (i = 0; i < textLength; ++i)
        {
            RightShift(ref s, patternLength);
            comparisons++;
            if (t.ContainsKey(text[i]))
            {
                Or(ref s, t[text[i]], patternLength);
            }
            else
            {
                for (int k = 0; k < patternLength; ++k)
                {
                    s[k] = true;
                }
            }
            if (!s[patternLength - 1])
            {
                result.Add(i - patternLength + 1);
            }
        }
    }
}
