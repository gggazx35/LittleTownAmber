using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class QuestNumberUI : QuestUI
{
    [SerializeField] protected Text amountText;
    [SerializeField] protected int currentAmount = 0;
    // Update is called once per frame
    void Start()
    {
        EventBus.get().Subscribe<InventoryChangeEvent>(GameManager.get().player.gameObject, onChange);
        Visualize();
    }
    
    void onChange(InventoryChangeEvent e)
    {
        //currentAmount = e.Inventory.CountItemNumberWithAttribute(ItemType.Paper, "colorName", "white");
        currentAmount = e.Inventory.CountItemNumberWithType(ItemType.Dagger);
        Visualize();
    }

    protected override void Visualize()
    {
        base.Visualize();
        amountText.text = $"{currentAmount}/{(Quest as QuestNumber).RequiredAmount}";
    }

    private void QuestNumber()
    {
        if (!(Quest is QuestNumber)) return;
        var q = (QuestNumber)Quest;
       // descriptionText.text = $"{q.Description}: {q.CurrentAmount}/{q.RequiredAmount}";
    }
}
