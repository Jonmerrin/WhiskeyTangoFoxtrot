using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Note Pressed Event", menuName = "Events/Note Pressed Event")]
public class NotePressedEvent : ScriptableObject
{

    public delegate void NotePressedEventHandler(NotePressLevels input);
    public event NotePressedEventHandler Event;


    //Raises the event if there are any handlers attached to it
    public void RaiseEvent(NotePressLevels input)
    {
        if (Event != null)
        {
            Event.Invoke(input);
        }
    }

    //Checks if an event handler is already registered
    public bool IsHandlerRegistered(NotePressedEventHandler handler)
    {
        if (Event == null)
        {
            return false;
        }
        foreach (NotePressedEventHandler registeredHandler in Event.GetInvocationList())
        {
            if (registeredHandler == handler)
            {
                return true;
            }
        }
        return false;
    }

}
