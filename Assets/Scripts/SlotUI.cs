using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    Inventory m_inventroy;

    Image m_image;
    [SerializeField] private int m_slotIndex = 0;

    private void Start()
    {
        m_image = GetComponentInParent<Image>();
    }

    private void FixedUpdate()
    {
        Item item = m_inventroy.GetItemAt(m_slotIndex);
        if (item != null) {
            m_image.sprite = item.GetSprite();
        } else
        {
            m_image.sprite = null;
        }
    }

    public void SetInventory(Inventory _inventroy)
    {
        m_inventroy = _inventroy;
    }

    public void SelectItem()
    {
        GameManager.instance.player.HoldItemAt(m_slotIndex);
    }
}
