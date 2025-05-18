using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Patrol : BehaviourNode
{
    TypedID isFacingWall;
    Movement movement;

    float movementAxis;

    public Patrol(string _facingWall, Movement _movement)
    {
        isFacingWall = new TypedID(_facingWall, typeof(BlackboardBoolProperty));
        movement = _movement;
        movementAxis = 0.5f;
    }

    protected override State Execute(Brain ownerBrain)
    {
        var facingWall = ownerBrain.Memory.GetProperty<BlackboardBoolProperty>(isFacingWall);

        if (facingWall.Get())
        {
            movementAxis = -movementAxis;
            facingWall.Set(false);
        }

        movement.Move(movementAxis);

        return State.RUNNING;
    }
}
