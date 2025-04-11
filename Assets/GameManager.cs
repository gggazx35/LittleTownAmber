using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ItemType
{
    Letter,
    Key,
    Dagger,
    Book,
    Missing
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

[System.Serializable]
public struct DialogueConfig
{
    public string path;

    public DialogueConfig(string _path)
    {
        path = _path;
    }

    public string ToPath()
    {
        return Path.Combine("Configs", "Sentences", path);
    }

    public Dialogue Create()
    {
        return LanguageManager.Instance().CreateDialogueFromJson(this);
    }
}

[System.Serializable]
public class Dialogue
{
    public Sentence[] lines;
    public Sentence Read()
    {
        return lines[0];
    }

    public IEnumerator Continue()
    {
        int line = 0;

        while (line < lines.Length)
        {
            yield return lines[line];
            line++;
        }
    }
}


[System.Serializable]
public struct Sentence
{
    public string text;
    public Dicision[] dicisions;
    public bool BindDicisions(IDicisionBox[] _dicisionBoxes)
    {
        Debug.Log($"{text}     {dicisions?.Length}");
        if(dicisions == null || dicisions.Length == 0) { return false; }
        for (int i = 0; i < dicisions.Length; i++) {
            _dicisionBoxes[i]?.Bind(dicisions[i], i);
        }
        return true;
    }
}

[System.Serializable]
public struct Dicision
{
    public string text;
    public string then;
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
        ItemRegisty.Add(ItemType.Book, typeof(BookItemTag));
        ItemRegisty.Add(ItemType.Missing, typeof(ItemTag));
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


public class LanguageManager
{
    private static LanguageManager instance;
    private Dictionary<ItemType, Type> ItemRegisty = new Dictionary<ItemType, Type>();

    public static LanguageManager Instance()
    {
        if (instance == null)
        {
            instance = new LanguageManager();
            instance.Init();
        }
        return instance;
    }

    private void Init()
    {
    }

    public Dialogue CreateDialogueFromJson(DialogueConfig _path)
    {
        string jsondata = Resources.Load<TextAsset>(_path.ToPath()).ToString();
        Debug.Log(jsondata);
        Dialogue dialog = JsonUtility.FromJson<Dialogue>(jsondata);
        
        return dialog;
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

    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }
}
