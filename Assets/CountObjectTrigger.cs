using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountObjectTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private GameObject eventListener;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objects.Add(collision.gameObject);
        EventBus.get().Publish(eventListener, new ObjectCountTrapEvent(objects.ToArray()));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objects.Contains(collision.gameObject))
            objects.Remove(collision.gameObject);
    }
}
