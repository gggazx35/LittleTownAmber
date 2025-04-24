using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Material Data", menuName = "Scriptable Object/Mat Data", order = int.MaxValue)]
public class MaterialData : ScriptableObject
{
    [SerializeField]
    private AudioClip m_sound;
    public AudioClip sound { get { return m_sound; } }
}

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data", order = int.MaxValue)]
public class ItemTag : ScriptableObject
{
    [SerializeField]
    private bool stackable = false;
    public bool Stackable { get { return stackable; } }
    [SerializeField]
    private int maxDurability = -1;
    public int MaxDurability { get { return maxDurability; } }

    [SerializeField] Sprite icon;
    public Sprite Icon { get { return icon; } }

    [SerializeField] GameObject instance;
    public GameObject Instance { get { return instance; } }

    [SerializeField] private ItemType kind;
    public ItemType Kind { get { return kind; } }

    public Item Create()
    {
        return new Item(this);
    }
}

[CreateAssetMenu(fileName = "Key Item Data", menuName = "Scriptable Object/KeyItem Data", order = int.MaxValue)]
public class KeyItemTag : ItemTag
{
    private int id;

    public int Id { get { return id; } }

    public int GetId()
    {
        return id;
    }
}

[CreateAssetMenu(fileName = "Book Item Data", menuName = "Scriptable Object/BookItem Data", order = int.MaxValue)]
public class BookItemTag : ItemTag
{
    public string connectedChapterName;

}

[CreateAssetMenu(fileName = "Weapon Item Data", menuName = "Scriptable Object/Weapon Item Data", order = int.MaxValue)]
public class WeaponItemTag : ItemTag
{
    [SerializeField] float range;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    public float Range { get { return range; } }

    public float Damage { get { return damage; } }
    public float AttackSpeed { get { return attackSpeed; } }
}

