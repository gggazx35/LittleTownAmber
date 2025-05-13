using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAttack : BehaviourNode
{
    private UsingWeapon weapon;
    
    public TryAttack(UsingWeapon _weapon)
    {
        weapon = _weapon;
    }

    protected override State Execute(Brain ownerBrain)
    {
        if (weapon.IsAttack())
            return State.RUNNING;
        else
        {
            if (!weapon.Attack()) return State.FAILURE;
        }
        return State.SUCCESS;
    } 
}
