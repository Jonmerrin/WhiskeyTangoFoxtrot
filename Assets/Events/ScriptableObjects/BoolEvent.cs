using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bool Event", menuName = "Events/Bool Event")]
public class BoolEvent : ScriptableObject
{

    public delegate void BoolEventHandler(bool input);
    public event BoolEventHandler Event;


    //Raises the event if there are any handlers attached to it
    public void RaiseEvent(bool input)
    {
        if (Event != null)
        {
            Event.Invoke(input);
        }
    }

    //Checks if an event handler is already registered
    public bool IsHandlerRegistered(BoolEventHandler handler)
    {
        if (Event == null)
        {
            return false;
        }
        foreach (BoolEventHandler registeredHandler in Event.GetInvocationList())
        {
            if (registeredHandler == handler)
            {
                return true;
            }
        }
        return false;
    }

}
