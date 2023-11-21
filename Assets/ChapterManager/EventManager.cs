using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class EventManager
{
    private Dictionary<EventType, UnityEvent> _events = new Dictionary<EventType, UnityEvent>();

    public EventManager()
    {
        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            _events.Add(type, new UnityEvent());
        };
    }

    public void Invoke(EventType eventType)
    {
        _events[eventType]?.Invoke();
    }
    public void AddListener(EventType eventType, UnityAction method)
    {
        _events[eventType]?.AddListener(method);
    }
    public void RemoveListener(EventType eventType, UnityAction method)
    {
        _events[eventType]?.RemoveListener(method);
    }

    public void InvokeNextChapter()
    {
        Invoke(EventType.NextChapter);
    }

    public void InvokeGameOver()
    {
        Invoke(EventType.NextChapter);
    }

    public void InvokeEndGame()
    {
        Invoke(EventType.NextChapter);
    }

}

public enum EventType
{
    NextChapter,
    GameOver, EndGame
}