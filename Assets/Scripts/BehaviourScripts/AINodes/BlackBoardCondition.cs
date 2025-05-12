using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoardSetBoolDeco : DecoratorNode
{
    TypedID propertyID;
    bool opposite;
    public BlackBoardSetBoolDeco(string _property, bool _not)
    {
        propertyID = new TypedID(_property, typeof(BlackboardBoolProperty));
        opposite = _not;
    }

    public override State Execute(Brain ownerBrain)
    {
        var property = ownerBrain.Memory.GetProperty<BlackboardBoolProperty>(propertyID);
        if (property.Get())
            return opposite ? State.FAILURE : State.SUCCESS;
        return opposite ? State.SUCCESS : State.FAILURE;
    }
}

public class BlackBoardSetGameObjectDeco : DecoratorNode
{
    TypedID propertyID;
    bool opposite;
    public BlackBoardSetGameObjectDeco(string _property, bool _not)
    {
        propertyID = new TypedID(_property, typeof(BlackboardGameObjectProperty));
        opposite = _not;
    }

    public override State Execute(Brain ownerBrain)
    {
        var property = ownerBrain.Memory.GetProperty<BlackboardGameObjectProperty>(propertyID);
        if (property.Get())
            return opposite ? State.FAILURE : State.SUCCESS;
        
        return opposite ? State.SUCCESS : State.FAILURE;
    }
}
