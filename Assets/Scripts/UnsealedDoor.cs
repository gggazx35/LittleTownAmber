using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsealedDoor : Interactive
{
    public override void Interact(Mob _mob)
    {
        gameObject.SetActive(false);
    }
}
