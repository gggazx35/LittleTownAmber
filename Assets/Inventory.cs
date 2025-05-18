using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryChangeEvent : IEvent
{
    private Inventory inventory;
    public Inventory Inventory => inventory;

    public InventoryChangeEvent(Inventory _inventory)
    {
        inventory = _inventory;
    }
}


public class Inventory : MonoBehaviour
{
    
    public int inventorySize;
    
    
    public Item[] items { get => m_items; }
    [SerializeField] private Item[] m_items;
    [SerializeField] private bool dropItems;

    public void Awake()
    {
        m_items = new Item[inventorySize];
    }

    public Item AddItemObject(ItemObject _itemObject)
    {
        Item item = AddItem(_itemObject.GetItem());
        if (item != null)
        {
            Destroy(_itemObject.gameObject);
        }
        return item;
    }

    /*public Item ExchangeItem(Inventroy _target, int _idx)
    {
        if(!_target.IsFull())
        {
            Debug.Log("not full");
            return _target.AddItem(RemoveItemAt(_idx));
        }
        return null;
    }*/

    public bool IsFull()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public Item AddItem(Item _item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = _item;
                _item.Move(this, i);
                EventBus.get().Publish(gameObject, new InventoryChangeEvent(this));
                return _item;
            }
        }
        return null;
    }



    public Item RemoveItemAt(int i)
    {
        Item item = items[i];
        //item.Move(null, -1);
        items[i] = null;
        EventBus.get().Publish(gameObject, new InventoryChangeEvent(this));
        return item;
    }

    public Item GetItemAt(int i)
    {
        return items[i];
    }


    public void SpillAllItems(Transform _transform)
    {
        for (int i = 0; i < items.Length; i++)
        {
            RemoveItemAt(i)?.SpawnItemObject(_transform);
        }
    }

    public int CountItemNumberWithType(ItemType itemType)
    {
        int count = 0;
        foreach (Item item in items)
        {
            if(item.type == itemType)
            {
                count++;
            }
        }
        return count;
    }

    public int CountItemNumberWithAttribute<T>(ItemType itemType, string _attributeName, T _value)
    {
        int count = 0;
        foreach (Item item in items)
        {
            if (item.type == itemType && item.MatchAttributes(_attributeName, _value))
            {
                count++;
            }
        }
        return count;
    }

    public int CountItemNumberWithAttribute<T>(string _attributeName, T _value)
    {
        int count = 0;
        foreach (Item item in items)
        {
            if (item.MatchAttributes(_attributeName, _value))
            {
                count++;
            }
        }
        return count;
    }

    public void OnDestroy()
    {
        SpillAllItems(gameObject.transform);
    }
}

