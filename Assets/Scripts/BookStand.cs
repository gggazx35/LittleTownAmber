using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStand : Stand
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact(Mob _mob)
    {
        //Debug.Log(_mob.inventroy.GetItemAt(_mob.selectedSlot).type);
        base.Interact(_mob);
        if (spriteRenderer == null) return;
        if(inventroy.GetItemAt(selectedSlot) == null)
        {
            spriteRenderer.sprite = null;
        }
        else
            spriteRenderer.sprite = inventroy.GetItemAt(selectedSlot).GetSprite();
    }
}
