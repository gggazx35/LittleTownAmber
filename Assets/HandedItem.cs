using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandedItem : MonoBehaviour
{
    protected Item item;
    // Start is called before the first frame update

    
    public virtual void Use(Mob _mob, int _slot)
    {
        Perform(_mob);
        int durability = item.GetItemTag().ReduceDurability();
        if (durability == 0) {
            _mob.GetInventory().RemoveItemAt(_slot);
            Destroy(gameObject);
        }
    }

    public virtual Mob InRange(Mob _mob, int _slot)
    {
        RaycastHit2D hit = _mob.RaycastAt(_mob.GetEnemyMask());
        if (hit) {
            return hit.transform.GetComponent<Mob>();
        }
        return null;
    }

    public void SetItem(Item _item)
    {
        item = _item;
    }

    protected virtual void Perform(Mob _mob)
    {
    }
}
