using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAttack : DecoratorNode
{
    private UsingWeapon weapon;
    
    public TryAttack(UsingWeapon _weapon)
    {
        weapon = _weapon;
    }

    public override State Execute(Brain ownerBrain)
    {
        if (weapon.IsAttack())
            return State.RUNNING;
        else
        {
            if (weapon.TryAttack()) return State.SUCCESS;
        }
        return State.FAILURE;
    } 
}
