using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTask : BehaviourNode
{
    TypedID target;
    Movement movement;
    public ChaseTask(string _target, Movement _movement)
    {
        target = new TypedID(_target, typeof(BlackboardGameObjectProperty));
        movement = _movement;
    }

    protected override State Execute(Brain ownerBrain)
    {
        var chase = ownerBrain.Memory.GetProperty<BlackboardGameObjectProperty>(target);

        if (chase == null) { Debug.LogWarning("execute"); return State.FAILURE; }

        movement.MoveFacingDirection(1.0f);

        return State.RUNNING;
    }
}
