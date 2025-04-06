using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void RemoveItemAt(int i)
    {
        items[i] = null;
    }

    public Item GetItemAt(int i) {
        return items[i];
    }
}
