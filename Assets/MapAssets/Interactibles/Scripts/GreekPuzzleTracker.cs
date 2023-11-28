using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreekPuzzleTracker
{
    public delegate void PuzzleStep(int stepsComplete);
    public static event PuzzleStep OnStepComplete;

    private static int stepsComplete = 3;
    public static int StepsComplete
    {
        get { return stepsComplete; }
        set
        {
            stepsComplete = value;
            OnStepComplete?.Invoke(value);
        }
    }
}
