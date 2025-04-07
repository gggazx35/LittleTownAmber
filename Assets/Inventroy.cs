using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventroy
{
    Item[] items;

    public Inventroy(int _inventorySize)
    {
        items = new Item[_inventorySize];
    }

    public Item AddItemObject(ItemObject _itemObject)
    {
        Item item = AddItem(_itemObject.GetItem());
        if (item != null) {
            GameObject.Destroy(_itemObject.gameObject);
        }
        return item;
    }

    public Item MoveItemInto(Inventroy _target, int _idx)
    {
        if(!_target.IsFull())
        {
            return _target.AddItem(RemoveItemAt(_idx));
        }
        return null;
    }

    public bool IsFull()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
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
}
