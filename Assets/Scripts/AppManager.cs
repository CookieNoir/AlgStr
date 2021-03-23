using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public Text inputText;
    public Text patternText;
    public Text outputText;
    public Text comparisonsText;

    public List<Algorithm> algorithms;
    public List<Button> buttons;

    private int comparisons = 0;
    private int activeAlgorithm = 0;
    private List<string> result;

    private void Awake()
    {
        result = new List<string>();
        SetActiveAlgorithm(0);
    }

    public void SetActiveAlgorithm(int index)
    {
        buttons[activeAlgorithm].interactable = true;
        activeAlgorithm = index;
        buttons[activeAlgorithm].interactable = false;
    }

    public void ExecuteActiveAlgorithm()
    {
        string output = algorithms[activeAlgorithm].name + Environment.NewLine + Environment.NewLine;
        if (inputText.text.Length == 0 || patternText.text.Length == 0 || inputText.text.Length < patternText.text.Length)
        {
            output += "Нет вхождений";
        }
        else
        {
            string text = inputText.text.ToUpperInvariant();
            string pattern = patternText.text.ToUpperInvariant();

            result.Clear();
            algorithms[activeAlgorithm].GetPatternEntries(text, pattern, result, ref comparisons);

            if (result.Count == 0)
            {
                output += "Нет вхождений";
            }
            else
            {
                foreach (string i in result)
                {
                    output += i + "  ";
                }
            }
        }
        outputText.text = output;
        comparisonsText.text = "Число сравнений: " + comparisons;
    }
}