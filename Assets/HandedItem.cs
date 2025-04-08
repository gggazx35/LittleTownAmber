using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectRange
{
    public Mob InRange(Mob _mob, int _slot);
}


public class HandedItem : MonoBehaviour
{
    // Start is called before the first frame update

    private Item GetItem(Mob _mob)
    {
        return _mob.GetHoldingItem();
    }

    public Item Unhand(Mob _mob, int _slot)
    {
        Destroy(gameObject);
        return _mob.inventroy.RemoveItemAt(_slot);
        //return _mob.inventroy.SelectedItem();
    }
    
    public virtual void Use(Mob _mob, int _slot)
    {
        Perform(_mob);
        int durability = GetItem(_mob).GetItemTag().ReduceDurability();
        if (durability == 0) {
            
            Unhand(_mob, _slot);
        }
    }

    protected virtual void Perform(Mob _mob)
    {
    }
}
