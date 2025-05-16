using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainItemQuestUI : MonoBehaviour
{

    [Serializable]
    public struct ItemTypeWithRequiredNumber {
        public ItemType type;
        public int requireAmount;
    }

    [SerializeField] private List<ItemTypeWithRequiredNumber> itemsToObtain = new List<ItemTypeWithRequiredNumber>(); 
    // Start is called before the first frame update
    void Start()
    {
        EventBus.get().Subscribe<InventoryChangeEvent>(GameManager.get().player.gameObject, UpdateQuest);
    }

    void UpdateQuest(InventoryChangeEvent e)
    {
        foreach (var item in itemsToObtain)
        {
            //item.requireAmount = e.Inventory.CountItemNumberWithType(item.type);
        }
        
    }

}
