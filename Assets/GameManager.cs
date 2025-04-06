using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ItemType
{
    Letter,
    Key,
    Dagger
}

[System.Serializable]
public struct ItemConfig
{
    public string type;
    public string configPath;

    public string ToPath()
    {
        return Path.Combine("Configs", "Items", type, configPath);
    }

    public Item Create()
    {
        return ItemFactory.Instance().CreateItemFromJson(this);
    }
}

public class ItemFactory
{
    private static ItemFactory instance;
    private Dictionary<ItemType, Type> ItemRegisty = new Dictionary<ItemType, Type>();

    public static ItemFactory Instance()
    {
        if(instance == null)
        {
            instance = new ItemFactory();
            instance.Init();
        }
        return instance;
    }

    private void Init()
    {
        ItemRegisty.Add(ItemType.Letter, typeof(ItemTag));
        ItemRegisty.Add(ItemType.Key, typeof(KeyItemTag));
        ItemRegisty.Add(ItemType.Dagger, typeof(WeaponItemTag));
    }
    public Type GetItemTagType(ItemType _itemType)
    {
        if (ItemRegisty.ContainsKey(_itemType))
        {
            return ItemRegisty[_itemType];
        }
        else
        {
            return typeof(ItemTag);
        }
    }

    public Item CreateItemFromJson(ItemConfig _path)
    {
        string jsondata = Resources.Load<TextAsset>(_path.ToPath()).ToString();
        Item item = null;
        ItemType itemType;
        if (System.Enum.TryParse(_path.type, out itemType))
        {
            item = new Item(itemType, (ItemTag)JsonUtility.FromJson(jsondata, GetItemTagType(itemType)));
        }
        return item;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public GameObject itemObjectPrefeb;
    public InventoryUI inventoryUI;
    public ItemConfig test;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        Item item = ItemFactory.Instance().CreateItemFromJson(test);
        Debug.Log($"{item.type}, {item.GetItemTag().durability}");
    }

}
