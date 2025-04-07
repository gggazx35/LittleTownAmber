using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectRange
{
    public Mob InRange(Mob _mob, int _slot);
}


public class HandedItem : MonoBehaviour
{
    protected Item item;
    // Start is called before the first frame update

    
    public virtual void Use(Mob _mob, int _slot)
    {
        Perform(_mob);
        int durability = item.GetItemTag().ReduceDurability();
        if (durability == 0) {
            _mob.inventroy.RemoveItemAt(_slot);
            Destroy(gameObject);
        }
    }

    public void SetItem(Item _item)
    {
        item = _item;
    }

    protected virtual void Perform(Mob _mob)
    {
    }
}
