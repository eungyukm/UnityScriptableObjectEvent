using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameEvent")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners =
        new List<GameEventListener>();

#if UNITY_EDITOR
    [HideInInspector]
    public string description;
#endif

    public void Raise()
    {
        for(int i= eventListeners.Count -1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if(!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if(eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
