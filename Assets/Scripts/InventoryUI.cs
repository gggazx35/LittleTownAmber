using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventroy;
    public void SetInventory(Inventory _inventroy)
    {
        foreach (SlotUI slot in transform.GetComponentsInChildren<SlotUI>())
        {
            slot.SetInventory(_inventroy);
        }
    }
}
