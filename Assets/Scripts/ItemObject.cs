using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/*[System.Serializable]
public class ItemTag
{
    public bool stackable = false;
    public int durability = -1;
    public int ReduceDurability()
    {
        *//*if(durability <= -1)
        {
            return -1;
        }
*//*
        if (durability > 0)
        {
            durability--;
        }
        
        return durability;
    }

    public int GetDurability() 
    { 
        return durability; 
    }
}

[System.Serializable]
public class KeyItemTag : ItemTag
{
    public int id;

    public int GetId()
    {
        return id;
    }
}

[System.Serializable]
public class BookItemTag : ItemTag
{
    public string connectedChapterName;

}

[System.Serializable]
public class WeaponItemTag : ItemTag
{
    public float range;
    public float damage;
    public float attackSpeed;
    public float GetRange()
    {
        return range;
    }

    public float GetDamage()
    {
        return damage;
    }
}

*/

public class MobUseItemEvent : IEvent
{
    private Mob user;
    private Item item;
    public Mob User { get => user; }
    public Item UsedItem { get => item; }
    public MobUseItemEvent(Item _item, Mob _mob)
    {
        user = _mob;
        item = _item;
    }
}

[System.Serializable]
public class Item
{
    public ItemType type { get => itemTag.Kind; }
    private ItemTag itemTag;
    private int durability;
    private int slot;
    private Inventory inventroy;
    Blackboard attributes = null;

    public Item(ItemTag _itemTag)
    {
        itemTag = _itemTag;
        durability = _itemTag.MaxDurability;
        _itemTag.AttributeInitialize(attributes);
    }

    public bool HasAttributes()
    {
        return attributes != null;
    }

    public bool MatchAttributes<T>(string _name, T _otherAttribute)
    {
        if (HasAttributes())
        {
            var attr = attributes.GetProperty(new TypedID(_name, typeof(T)));
            if (attr is not IBlackboardMatchable<T>) return false;
            return (attr as IBlackboardMatchable<T>).IsMatch(_otherAttribute);
        }
        return false;
    }

    public void Move(Inventory _inventroy, int _slot)
    {
        inventroy = _inventroy;
        slot = _slot;
    }

    public void ReduceDurability()
    {
        durability--;
        if(durability <= 0)
        {
            inventroy.RemoveItemAt(slot);
            
        }
    }

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Sprites/Items/" + type.ToString());
    }

    public GameObject GetPrefeb()
    {
        return Resources.Load<GameObject>("Models/Items/" + type.ToString());
    }

    public GameObject SpawnItemObject(Transform _transform)
    {
        GameObject go = GameObject.Instantiate(GameManager.instance.itemObjectPrefeb,
            _transform.position, _transform.rotation);
        go.GetComponent<ItemObject>().SetItem(this);
        return go;
    }

    public ItemTag GetItemTag()
    {
        return itemTag;
    }
}

public class ItemPickupEvent : IEvent
{
    public Inventory inventroy { get; }
    public ItemPickupEvent(Inventory _inventroy)
    {
        inventroy = _inventroy;
    }
}

public class ItemObject : MonoBehaviour
{
    Item item;
    // Start is called before the first frame update
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        EventBus.get().Subscribe<ItemPickupEvent>(gameObject, PickObject);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        rb.AddForce(Vector2.left * Random.Range(-2.0f, 2.0f), ForceMode2D.Impulse);
        spriteRenderer.sprite = item.GetSprite();
    }

    public void SetItem(Item _item) {
        item = _item;
    }

    public Item GetItem()
    {
        return item;
    }

    public void PickObject(ItemPickupEvent e)
    {
        if (e != null)
        {
            e.inventroy.AddItemObject(this);
        }
    }
}
