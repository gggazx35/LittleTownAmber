using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    Inventroy inventroy;
    Image image;
    [SerializeField] private int slotIndex = 0;

    private void Start()
    {
        image = GetComponentInParent<Image>();
    }

    private void FixedUpdate()
    {
        Item item = inventroy.GetItemAt(slotIndex);
        if (item != null) {
            image.sprite = item.GetSprite();
        }
    }

    public void SetInventory(Inventroy _inventroy)
    {
        inventroy = _inventroy;
    }

    public void SelectItem()
    {
        GameManager.instance.player.SelectItemAt(slotIndex);
    }
}
