using System.Collections;
using System.Collections.Generic;
using UnityEngine;




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

    public virtual void AttributeInitialize(Blackboard _attributes) {
        if (_attributes == null) _attributes = new Blackboard();
    }

    public Item Create()
    {
        return new Item(this);
    }
}










