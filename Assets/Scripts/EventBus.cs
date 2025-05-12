using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEvent
{
}

public struct EventID {
    public Type type;
    public int id;

    public EventID(int _id, Type _type)
    {
        id = _id;
        type = _type;
    }
}
public class EventIDComparser : IEqualityComparer<EventID>
{

    public bool Equals(EventID x, EventID y)
    {
        return x.type == y.type && x.id == y.id;

    }



    public int GetHashCode(EventID id)
    {
        return id.type.GetHashCode() ^ id.id.GetHashCode();

    }

}


public class EventBus : MonoBehaviour
{

    private static EventBus instance = null;
    private Dictionary<Type, List<Delegate>> eventHandlers;
    private Dictionary<EventID, List<Delegate>> uniqueEventHandlers;

    private void Awake()
    {
        instance = this;
        eventHandlers = new Dictionary<Type, List<Delegate>>();
        uniqueEventHandlers = new Dictionary<EventID, List<Delegate>>();
    }

    public static EventBus get()
    {
        Debug.LogWarning(instance);
        return instance;
    }

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        Type eventType = typeof(TEvent);
        if (!eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType] = new List<Delegate>();
        }
        eventHandlers[eventType].Add(handler);
    }

    public void Subscribe<TEvent>(GameObject gObject, Action<TEvent> handler) where TEvent : IEvent
    {
        EventID eventType = new EventID(gObject.GetInstanceID(), typeof(TEvent));
        if (!uniqueEventHandlers.ContainsKey(eventType))
        {
            uniqueEventHandlers[eventType] = new List<Delegate>();
        }
        uniqueEventHandlers[eventType].Add(handler);
    }

    public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        Type eventType = typeof(TEvent);
        if (eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType].Remove(handler);
        }
    }

    public void Unsubscribe<TEvent>(GameObject gObject, Action<TEvent> handler) where TEvent : IEvent
    {
        EventID eventType = new EventID(gObject.GetInstanceID(), typeof(TEvent));
        if (uniqueEventHandlers.ContainsKey(eventType))
        {
            uniqueEventHandlers[eventType].Remove(handler);
        }
    }

    public void Publish<TEvent>(TEvent eventArgs) where TEvent : IEvent
    {
        Type eventType = typeof(TEvent);
        if (eventHandlers.ContainsKey(eventType))
        {
            List<Delegate> handlers = eventHandlers[eventType];
            foreach (var handler in handlers)
            {
                ((Action<TEvent>)handler)(eventArgs);
            }
        }
    }

    public void Publish<TEvent>(GameObject gObject, TEvent eventArgs) where TEvent : IEvent
    {
        EventID eventType = new EventID(gObject.GetInstanceID(), typeof(TEvent));
        if (uniqueEventHandlers.ContainsKey(eventType))
        {
            List<Delegate> handlers = uniqueEventHandlers[eventType];
            foreach (var handler in handlers)
            {
                ((Action<TEvent>)handler)(eventArgs);
            }
        }
    }
}
