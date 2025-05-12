using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourNodeBase
{
    public enum State
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
}

public abstract class BehaviourNode : BehaviourNodeBase
{
    protected CompositeNode parent;
    protected List<BehaviourService> services;
    protected List<DecoratorNode> decorators;
    protected abstract State Execute(Brain ownerBrain);

    public BehaviourNode()
    {
        services = new List<BehaviourService>();
        decorators = new List<DecoratorNode>();
        parent = null;
    }

    public State Evaluate(Brain ownerBrain)
    {
        State state = State.SUCCESS;
        foreach (DecoratorNode decorator in decorators)
        {
            state = decorator.Execute(ownerBrain);
            if (state == State.FAILURE) return state;
        }
        State x = Execute(ownerBrain);
        if (state != State.RUNNING) state = x;
        
        foreach (BehaviourService service in services) {
            service.Tick(ownerBrain.Timer, ownerBrain);
        }



        return state;
    }

    public BehaviourNode AttachService(BehaviourService service)
    {
        services.Add(service);
        return this;
    }

    public BehaviourNode AttachDecorator(DecoratorNode deco)
    {
        decorators.Add(deco);
        return this;
    }
}

public abstract class DecoratorNode : BehaviourNodeBase
{
    public abstract State Execute(Brain ownerBrain);
}

public class ForceSuccessDeco : DecoratorNode
{
    //public ForceSuccessDeco(DecoratorNode node)
    public override State Execute(Brain ownerBrain)
    {
        return State.SUCCESS;
    }
}

public class LoopDeco : DecoratorNode
{
    int i;
    int current;
    public LoopDeco(int i) { this.current = 0; this.i = i; }
    public override State Execute(Brain ownerBrain)
    {
        if (current < i) {
            current++;
            return State.RUNNING;
        }
        current = 0;
        return State.SUCCESS;
    }
}


public class RootNode : BehaviourNodeBase
{
    public int current = 0;
    public BehaviourNode child;
    private Brain ownerBrain;
    public Brain OwnerBrain => ownerBrain;
    public RootNode(Brain brain, BehaviourNode child)
    {
        ownerBrain = brain;
        this.child = child;
    }
    public void Execute()
    {
        //if(current >= children.Count) return State.SUCCESS;
        child.Evaluate(ownerBrain);
    }
}

public abstract class CompositeNode : BehaviourNode
{
    protected int current;
    protected List<BehaviourNode> children;

    public CompositeNode() : base()
    {
        current = 0;
        children = new List<BehaviourNode>();
    }

    public void Next()
    {
        current++;
    }

    public void AttachChild(BehaviourNode node)
    {
        children.Add(node);
    }
}

public class SeqenceNode : CompositeNode
{
    protected override State Execute(Brain ownerBrain)
    {
        // if running => keep
        // if failed => go back n ret fail
        // ff succ => go back n ret succ
        if (current >= children.Count) { current = 0; return State.SUCCESS; }
        var child = children[current];
        switch (child.Evaluate(ownerBrain))
        {
            case State.RUNNING:
                return State.RUNNING;
            case State.FAILURE:
                return State.FAILURE;
            case State.SUCCESS:
                Next();
                return State.RUNNING;
        }
        return State.FAILURE;
    }
}

public class SelectorNode : CompositeNode
{

    protected override State Execute(Brain ownerBrain)
    {
        // if running => keep
        // if failed => go back n ret fail
        // ff succ => go back n ret succ
        if (current >= children.Count) { current = 0; return State.FAILURE; }
        var child = children[current];
        switch (child.Evaluate(ownerBrain))
        {
            case State.RUNNING:
                return State.RUNNING;
            case State.SUCCESS:
                return State.SUCCESS;
            case State.FAILURE:
                Next();
                return State.RUNNING;
        }
        return State.FAILURE;
    }
}

public abstract class BehaviourService
{
    private float timer;
    protected float interval;
    public BehaviourService() { interval = 0.2f; }
    public BehaviourService(float _interval) { interval = _interval; }
    public void Tick(float deltaSeconds, Brain brain)
    {
        timer += deltaSeconds;
        if (timer >= interval)
        {
            timer = deltaSeconds;
            TickNode(deltaSeconds, brain);
        }
    }
    protected abstract void TickNode(float deltaSeconds, Brain brain);
}