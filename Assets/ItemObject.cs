using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTag
{
    public bool stackable = false;
    public int durability = -1;
    public int ReduceDurability()
    {
        /*if(durability <= -1)
        {
            return -1;
        }
*/
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


[System.Serializable]
public class Item
{
    public ItemType type;
    private ItemTag itemTag;

    public Item(ItemType _type)
    {
        type = _type;
        itemTag = new ItemTag();
    }

    public Item(ItemType _type, ItemTag _itemTag)
    {
        type = _type;
        itemTag = _itemTag;
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

public class ItemObject : Interactive
{
    Item item;
    // Start is called before the first frame update
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
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

    public override void Interact(Mob _mob)
    {
        if (_mob != null)
        {
            _mob.inventroy.AddItemObject(this);
        }
    }
}
