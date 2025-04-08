using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : Interactive, IHasInventory
{
    Inventroy m_inventory;
    public Inventroy inventroy { get => m_inventory; }
    public int selectedSlot { get => 0; }
    // Start is called before the first frame update
    void Start()
    {
        m_inventory = new Inventroy(1);
    }

    public override void Interact(Mob _mob)
    {
        //Debug.Log(_mob.inventroy.GetItemAt(_mob.selectedSlot).type);
        if (!inventroy.IsFull())
        {
            _mob.MoveHoldingItem(inventroy);
            Debug.Log(inventroy.GetItemAt(0).type);
        }
        else
        {
            Debug.Log("spillll");
            inventroy.SpillAllItems(transform);
        }
    }
}
