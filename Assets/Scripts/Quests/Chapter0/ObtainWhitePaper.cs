using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainWhitePaper : QuestNumberUI
{
    private void Start()
    {
        EventBus.get().Subscribe(GameManager.get().player.gameObject, (InventoryChangeEvent e) =>
        {
            e.Inventory.CountItemNumberWithAttribute(ItemType.Paper, "colorName", "white");
        });
    }
}
