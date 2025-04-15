using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactive
{
    [SerializeField] Transform playerTf;
    public override void Interact(Mob _mob)
    {
        if (!(_mob is Player)) return;
        Player player = _mob as Player;
        player.Ladder(playerTf.position);
    }
}
