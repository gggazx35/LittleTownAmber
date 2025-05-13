using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ChaseTask : BehaviourNode
{
    TypedID target;
    TypedID isAttack;
    TypedID facingWall;
    Movement movement;
    Mob mob;
    public ChaseTask(string _target, string _attack, string _facingWall, Movement _movement, Mob mob)
    {
        target = new TypedID(_target, typeof(BlackboardGameObjectProperty));
        isAttack = new TypedID(_attack, typeof(BlackboardBoolProperty));
        movement = _movement;
        facingWall = new TypedID(_facingWall, typeof(BlackboardBoolProperty));
        this.mob = mob;
    }

    protected override State Execute(Brain ownerBrain)
    {
        var chase = ownerBrain.Memory.GetProperty<BlackboardGameObjectProperty>(target);
        var attack = ownerBrain.Memory.GetProperty<BlackboardBoolProperty>(isAttack);
        var wall = ownerBrain.Memory.GetProperty<BlackboardBoolProperty>(facingWall);

        if (chase == null) {
            movement.Move(0.0f);
            Debug.LogWarning("execute");
            return State.FAILURE; 
        }

        
        var item = mob.GetHoldingItem();
        int mask = mob.GetEnemyMask();
        if (item.GetItemTag() is WeaponItemTag)
        {
            var tag = (WeaponItemTag)item.GetItemTag();
            var obj = Physics2D.OverlapBox(mob.hand.transform.position, new Vector2(10.0f * tag.Range, 1.0f), 0.0f, mask);
            if (obj)
            {
                Debug.LogWarning("yeeeeeeeeeeeeeeeeeeeeeeep");
                return State.SUCCESS;
            }
        }

        movement.Target(chase.Get().transform.position);
        if (movement.GetDirection().x < 0.0f)
        {
            movement.Move(-1.0f);
        } else
            movement.Move(1.0f);

        if(wall.Get())
        {
            movement.Jump();
        }

        //movement.UpdateFlip();

        return State.RUNNING;
    }
}

////public class OnBoundaryDeco : DecoratorNode
////{
////    public override State Execute(Brain ownerBrain)
////    {

////    }
////}