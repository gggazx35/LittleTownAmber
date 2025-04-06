using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventroy inventroy;
    public void SetInventory(Inventroy _inventroy)
    {
        foreach (SlotUI slot in transform.GetComponentsInChildren<SlotUI>())
        {
            slot.SetInventory(_inventroy);
        }
    }
}
