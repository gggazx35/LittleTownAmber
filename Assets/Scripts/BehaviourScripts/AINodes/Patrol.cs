using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Patrol : BehaviourNode
{
    TypedID isFacingWall;
    Movement movement;
    public Patrol(string _facingWall, Movement _movement)
    {
        isFacingWall = new TypedID(_facingWall, typeof(BlackboardBoolProperty));
        movement = _movement;
    }

    protected override State Execute(Brain ownerBrain)
    {
        var facingWall = ownerBrain.Memory.GetProperty<BlackboardBoolProperty>(isFacingWall);

        if (facingWall.Get())
        {
            movement.MoveFacingDirection(-.5f);
            facingWall.Set(false);
        }
        else
            movement.MoveFacingDirection(.5f);

        return State.RUNNING;
    }
}
