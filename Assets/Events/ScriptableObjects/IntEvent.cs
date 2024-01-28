using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int Event", menuName = "Events/Int Event")]
public class IntEvent : ScriptableObject
{

    public delegate void IntEventHandler(int input);
    public event IntEventHandler Event;


    //Raises the event if there are any handlers attached to it
    public void RaiseEvent(int input)
    {
        if (Event != null)
        {
            Event.Invoke(input);
        }
    }

    //Checks if an event handler is already registered
    public bool IsHandlerRegistered(IntEventHandler handler)
    {
        if (Event == null)
        {
            return false;
        }
        foreach (IntEventHandler registeredHandler in Event.GetInvocationList())
        {
            if (registeredHandler == handler)
            {
                return true;
            }
        }
        return false;
    }

}
