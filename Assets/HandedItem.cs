using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectRange
{
    public Mob InRange(Mob _mob);
}


public class HandedItem : MonoBehaviour
{
    // Start is called before the first frame update

   
    public Item Unhand(Mob _mob)
    {
        Destroy(gameObject);
        return _mob.GetHoldingItem();
        //return _mob.inventroy.SelectedItem();
    }
    
    public virtual void Use(Mob _mob)
    {
        Perform(_mob);
        Item item = _mob.GetHoldingItem();
        if (item == null) {
            
            Unhand(_mob);
            _mob.inventroy.RemoveItemAt(_mob.selectedSlot);
        }
    }

    protected virtual void Perform(Mob _mob)
    {
    }
}
