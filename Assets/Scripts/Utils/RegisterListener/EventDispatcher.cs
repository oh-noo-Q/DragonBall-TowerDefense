using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EventDispatcher : SingletonObject<EventDispatcher>
{

    #region Singleton

    public override void OnCreated()
    {
        _listeners = new Dictionary<EventID, Action<object>>();
    }

    public void OnDestroy()
    {
        ClearAllListener();
    }

    #endregion


    #region Fields

    /// Store all "listener"
    Dictionary<EventID, Action<object>> _listeners = new Dictionary<EventID, Action<object>>();

    #endregion


    #region Add Listeners, Post events, Remove listener

    /// <summary>
    /// Register to listen for eventID
    /// </summary>
    /// <param name="eventID">EventID that object want to listen</param>
    /// <param name="callback">Callback will be invoked when this eventID be raised</para	m>
    public void RegisterListener(EventID eventID, Action<object> callback)
    {
        // checking params
        //		Common.Assert (callback != null, "AddListener, event {0}, callback = null !!", eventID.ToString ());
        //		Common.Assert (eventID != EventID.None, "RegisterListener, event = None !!");
        if (callback == null)
        {
            return;
        }

        if (eventID == EventID.None)
        {
            return;
        }
        // check if listener exist in distionary
        if (_listeners.ContainsKey(eventID))
        {
            // add callback to our collection
            _listeners[eventID] += callback;
        }
        else
        {
            // add new key-value pair
            _listeners.Add(eventID, null);
            _listeners[eventID] += callback;
        }
    }

    /// <summary>
    /// Posts the event. This will notify all listener that register for this event
    /// </summary>
    /// <param name="eventID">EventID.</param>
    /// <param name="sender">Sender, in some case, the Listener will need to know who send this message.</param>
    /// <param name="param">Parameter. Can be anything (struct, class ...), Listener will make a cast to get the data</param>
    public void PostEvent(EventID eventID, object param = null)
    {
        if (!_listeners.ContainsKey(eventID))
        {
            return;
        }

        // posting event
        var callbacks = _listeners[eventID];
        // if there's no listener remain, then do nothing
        if (callbacks != null)
        {
            callbacks(param);
        }
        else
        {
            //				Common.Log("PostEvent {0}, but no listener remain, Remove this key", eventID);
            _listeners.Remove(eventID);
        }
    }

    /// <summary>
    /// Removes the listener. Use to Unregister listener
    /// </summary>
    /// <param name="eventID">EventID.</param>
    /// <param name="callback">Callback.</param>
    public void RemoveListener(EventID eventID, Action<object> callback)
    {
        // checking params
        //		Common.Assert (callback != null, "RemoveListener, event {0}, callback = null !!", eventID.ToString ());
        //		Common.Assert (eventID != EventID.None, "AddListener, event = None !!");

        if (callback == null)
        {
            return;
        }

        if (eventID == EventID.None)
        {
            return;
        }
        if (_listeners.ContainsKey(eventID))
        {
            _listeners[eventID] -= callback;
        }
        else
        {
            //				Common.Warning(false, "RemoveListener, not found key : " + eventID);
        }
    }

    /// <summary>
    /// Clears all the listener.
    /// </summary>
    public void ClearAllListener()
    {
        _listeners.Clear();
    }

    #endregion
    
    
}

#region Extension class
/// <summary>
/// Delare some "shortcut" for using EventDispatcher easier
/// </summary>
public static class EventDispatcherExtension
{
    // Use for registering with EventsManager
    public static void RegisterListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance.RegisterListener(eventID, callback);
    }
    public static void RemoveListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance.RemoveListener(eventID, callback);
    }

    // Post event with param
    public static void PostEvent(this MonoBehaviour listener, EventID eventID, object param)
    {
        EventDispatcher.Instance.PostEvent(eventID, param);
    }

    // Post event with no param (param = null)
    public static void PostEvent(this MonoBehaviour sender, EventID eventID)
    {
        EventDispatcher.Instance.PostEvent(eventID, null);
    }
}
#endregion