using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterConversationEvent : IEvent
{
    public EnterConversationEvent() {
        
    }

}

public class InteractionFetcher : MonoBehaviour
{
    // Start is called before the first frame update
    public void Subscribe(Action<GameObject> action)
    {
        action(gameObject);
    }
}
