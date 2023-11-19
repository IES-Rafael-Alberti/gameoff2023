using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public UnityEvent NextChapter = new UnityEvent();
    public UnityEvent GameOver = new UnityEvent();
    public UnityEvent EndGame = new UnityEvent();

    public void InvokeNextChapter()
    {
        NextChapter?.Invoke();
    }

    /*public void InvokeGameOver()
    {
        GameOver?.Invoke();
    }*/

}