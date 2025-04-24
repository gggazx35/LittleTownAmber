using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandedKey : HandedItem
{
    

    private void Start()
    {
    }

    protected override void Perform(Mob _mob)
    {
        RaycastHit2D hit = _mob.RaycastAt(LayerMask.GetMask("Door"));
        if (hit)
        {
            Door door = hit.transform.gameObject.GetComponent<Door>();
            if (door)
            {
                KeyItemTag keyTag = _mob.GetHoldingItem().GetItemTag() as KeyItemTag;
                door.Unlock(keyTag.GetId());
            }
        }
    }
}
