using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject connectedObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventBus.get().Publish<TriggerTrapEvent>(connectedObject, null);
    }
}
