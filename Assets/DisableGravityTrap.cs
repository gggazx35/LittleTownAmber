using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrapEvent : IEvent
{

}

public class ObjectCountTrapEvent : IEvent
{
    private GameObject[] m_objects;

    public ObjectCountTrapEvent(GameObject[] objects)
    {
        m_objects = objects;
    }
    public int NumberOfObjects()
    {
        return m_objects.Length;
    }
}

public class DisableGravityTrap : MonoBehaviour
{
    [SerializeField] bool ignoreCollision;
    [SerializeField] Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.get().Subscribe<TriggerTrapEvent>(gameObject, OnTrapTrigger);
        EventBus.get().Subscribe<ObjectCountTrapEvent>(gameObject, OnObjectCountTrigger);
    }

    void OnTrapTrigger(TriggerTrapEvent e)
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
        if(ignoreCollision) collider.isTrigger = true;
    }

    void OnObjectCountTrigger(ObjectCountTrapEvent e)
    {
        var rb = GetComponent<Rigidbody2D>();
        if (e.NumberOfObjects() == 1)
        {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y - 1.0f));
        }
        if (e.NumberOfObjects() >= 5)
        {
            OnTrapTrigger(null);
        }
    }
}
