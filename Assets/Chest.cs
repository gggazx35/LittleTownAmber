using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestOpenEvent : IEvent
{
    public ChestOpenEvent()
    {

    }
}

public class Chest : MonoBehaviour
{
    [SerializeField] private List<ItemTag> items;
    // Start is called before the first frame update
    /*private void OnMouseDown()
    {
        if(playerDetector.IsPlayerDetected())
        {
            foreach (var item in items)
            {
                SpawnItemObject(item.Create());
            }
            items.Clear();
        }
    }*/

    private void Start()
    {
        EventBus.get().Subscribe<ChestOpenEvent>(gameObject, ChestOpen);
        Debug.LogWarning($"{gameObject.GetInstanceID()}");

    }

    private void ChestOpen(ChestOpenEvent e)
    {
        Debug.LogWarning("chestOpened");
        SpawnItems();

        EventBus.get().Unsubscribe<ChestOpenEvent>(gameObject, ChestOpen);
    }

    public void SpawnItems()
    {
        //if (playerDetector.IsPlayerDetected())
        //{
        foreach (var item in items)
        {
            item.Create().SpawnItemObject(transform);
            //SpawnItemObject(item.Create());
        }
        items.Clear();
        //}
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

}


