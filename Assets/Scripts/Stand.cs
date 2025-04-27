using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandPutItemEvent : IEvent
{
    public Item item { get; }
    public StandPutItemEvent(Item _item)
    {
        item = _item;
    }
}

public class Stand : MonoBehaviour
{
    protected Inventory m_inventory;
    //public Inventroy inventroy { get => (m_inventory != null) ? m_inventory : m_inventory = new Inventroy(1); }
    public int selectedSlot { get => 0; }
    // Start is called before the first frame update
    void Start()
    {
        m_inventory = GetComponent<Inventory>();
        EventBus.get().Subscribe<StandPutItemEvent>(gameObject, Put);
        //m_inventory = new Inventroy(1);
    }

    public virtual void Put(StandPutItemEvent e)
    {
        //Debug.Log(_mob.inventroy.GetItemAt(_mob.selectedSlot).type);
        if (!m_inventory.IsFull() && e.item != null)
        {
            e.item.Move(m_inventory, selectedSlot);
            Debug.Log(m_inventory.GetItemAt(selectedSlot).type);
        }
        else
        {
            Debug.Log("spillll");
            m_inventory.SpillAllItems(transform);
        }
    }

    public Item GetItem()
    {
        return m_inventory.GetItemAt(selectedSlot);
    }
}
