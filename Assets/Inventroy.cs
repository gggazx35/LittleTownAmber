using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public interface IInventory
{
    Item[] items { get; }
    bool IsFull();
    Item AddItem(Item _item);
    Item GetItemAt(int _i);
    Item RemoveItemAt(int i);
}


public class Inventroy : IInventory
{
    private Item[] m_items;
    public Item[] items { get => m_items; }

    public Inventroy(int _inventorySize)
    {
        m_items = new Item[_inventorySize];
    }

    public Item AddItemObject(ItemObject _itemObject)
    {
        Item item = AddItem(_itemObject.GetItem());
        if (item != null) {
            GameObject.Destroy(_itemObject.gameObject);
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
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = _item;
                return _item;
            }
        }
        return null;
    }
    
    

    public Item RemoveItemAt(int i)
    {
        Item item = items[i];
        items[i] = null;
        return item;
    }

    public Item GetItemAt(int i) {
        return items[i];
    }


    public void SpillAllItems(Transform _transform)
    {
        for (int i = 0; i < items.Length; i++)
        {
            RemoveItemAt(i)?.SpawnItemObject(_transform);
        }
    }
}
