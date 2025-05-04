using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEditor.PlayerSettings;

public enum ItemType
{
    Letter,
    Key,
    Dagger,
    Book,
    Missing,
    GoldenCoin,
    Paper
}

/*[System.Serializable]
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
*/
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
public struct Sentence
{
    public string text;
}

[System.Serializable]
public struct Dicision
{
    public string text;
    public Dialogue then;
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
    /*public Type GetItemTagType(ItemType _itemType)
    {
        if (ItemRegisty.ContainsKey(_itemType))
        {
            return ItemRegisty[_itemType];
        }
        else
        {
            return typeof(ItemTag);
        }
    }*/

    /*public Item CreateItemFromTag(ItemTag _path)
    {
        
        item = new Item();
        
        return item;
    }*/
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


public class QuestAchieveEvent : IEvent
{
    private QuestData quest;
    public QuestAchieveEvent(QuestData _quest)
    {
        quest = _quest;
    }

    public bool IsQuestAchieved(QuestData data)
    {
        return quest == data;
    }
}

////////////public class QuestRegisterEvent : IEvent { 
////////////    private QuestData quest;
////////////    public QuestRegisterEvent(QuestData _q)
////////////    {
////////////        quest = _q;
////////////    }
////////////}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public GameObject questWindow;
    public GameObject itemObjectPrefeb;
    public InventoryUI inventoryUI;
    public List<QuestData> quests;


    //public ItemConfig test;
    // Start is called before the first frame update

    [SerializeField] private int killCount;
    [SerializeField] private QuestData testQuest;
    private void Awake()
    {
        instance = this;
        killCount = 0;
    }

    private void MobDeath(MobDeathEvent e)
    {
        killCount++;
        if(e.death == player)
        {
            Gameover(e);
        }
    }
    private void Gameover(MobDeathEvent e)
    {
        string headline = "RIP Amber, she was";
        string verb = "died by";

        if (e.cause != null && e.reason == DamageReason.None)
        {
            EventBus.get().Publish(new GameoverEvent($"{headline} {verb} a terrible monster named {e.cause}"));
        }
        if (e.cause != null && e.reason != DamageReason.None)
        {
            switch(e.reason)
            {
                case DamageReason.Stab:
                    verb = "Stabbed by";
                    break;
                case DamageReason.Explosion:
                    verb = "blown up by";
                    break;
                case DamageReason.Fall:
                    verb = "hit the ground too hard while trying to escape from";
                    break;
                case DamageReason.FallingStone:
                    verb = "hit too hard by a stone while she trying to escape from";
                    break;
            }
            EventBus.get().Publish(new GameoverEvent($"{headline} {verb} a terrible monster named {e.cause}"));
        }
        if (e.cause == null && e.reason != DamageReason.None)
        {
            switch (e.reason)
            {
                case DamageReason.Explosion:
                    verb = "blown up by a bomb";
                    break;
                case DamageReason.Fall:
                    verb = "hit the ground too hard";
                    break;
                case DamageReason.FallingStone:
                    verb = " hit too hard by a stone";
                    break;
            }
            EventBus.get().Publish(new GameoverEvent($"{headline} {verb}"));
        }
        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        EventBus.get().Subscribe<MobDeathEvent>(MobDeath);
        AddQuest(testQuest, (QuestData q) => { return $"0/{(q as QuestNumber).RequiredAmount}"; });
    }

    public void AddQuest(QuestData quest, Func<QuestData, string> func)
    {
        quests.Add(quest);
        quest.OnVisualize(func);
    }

    public bool HasQuest(QuestData questData)
    {
        return quests.Contains(questData);
    }

    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    

    public static GameManager get()
    {
        return instance;
    }
}
