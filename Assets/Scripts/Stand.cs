using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : Interactive, IHasInventory
{
    Inventroy m_inventory;
    public Inventroy inventroy { get => (m_inventory != null) ? m_inventory : m_inventory = new Inventroy(1); }
    public int selectedSlot { get => 0; }
    // Start is called before the first frame update
    void Start()
    {
        //m_inventory = new Inventroy(1);
    }

    public override void Interact(Mob _mob)
    {
        //Debug.Log(_mob.inventroy.GetItemAt(_mob.selectedSlot).type);
        if (!inventroy.IsFull() && _mob.GetHoldingItem() != null)
        {
            _mob.MoveHoldingItem(inventroy);
            Debug.Log(inventroy.GetItemAt(selectedSlot).type);
        }
        else
        {
            Debug.Log("spillll");
            inventroy.SpillAllItems(transform);
        }
    }

    public Item GetItem()
    {
        return inventroy.GetItemAt(selectedSlot);
    }
}
