using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public int inventorySize;
    private Item[] m_items;
    public Item[] items { get => m_items; }

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
                return _item;
            }
        }
        return null;
    }



    public Item RemoveItemAt(int i)
    {
        Item item = items[i];
        item.Move(null, -1);
        items[i] = null;
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
}

