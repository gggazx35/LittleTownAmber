using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintNode : BehaviourNode
{
    private string str;
    public PrintNode(string arg1)
    {
        str = arg1;
    }

    protected override State Execute(Brain brain)
    {
        if(str == null) return State.FAILURE;
        Debug.Log(str);
        return State.SUCCESS;
    }
}

public class WaitForSecondsNode : BehaviourNode
{
    protected float timer;
    protected float seconds;
    public WaitForSecondsNode(float seconds)
    {
        timer = 0.0f;
        this.seconds = seconds;
    }

    protected State WaitFor(Brain brain, float seconds)
    {
        timer += brain.Timer;
        if (seconds <= timer)
        {
            timer = 0.0f;
            return State.SUCCESS;
        }

        return State.RUNNING;
    }

    protected override State Execute(Brain brain)
    {
        return WaitFor(brain, seconds);
    }
}

public class WaitForSecondsMemory : WaitForSecondsNode
{
    private TypedID secondsMemory;
    public WaitForSecondsMemory(float seconds, string memoryName) : base(seconds)
    {
        secondsMemory = new TypedID(memoryName, typeof(BlackboardFloatProperty));
    }

    protected override State Execute(Brain brain)
    {
        return WaitFor(brain, seconds + brain.Memory.GetProperty<BlackboardFloatProperty>(secondsMemory).Get());
    }
}

public class UpdateBlackboardFloatValueService : BehaviourService
{
    TypedID valueId;
    float increaseAmount;

    public UpdateBlackboardFloatValueService(float _interval, string name, float incAmount) : base(_interval) 
    {
        valueId = new TypedID(name, typeof(BlackboardFloatProperty));
        increaseAmount = incAmount;
    }

    protected override void TickNode(float deltaSeconds, Brain brain)
    {
        var val = brain.Memory.GetProperty<BlackboardFloatProperty>(valueId);
        val.Set(val.Get() + increaseAmount);
    }
}

public class Brain : MonoBehaviour
{
    [SerializeField] private float timer;
    private Blackboard memory = new Blackboard();
    public Blackboard Memory => memory;
    public float Timer => timer;
    protected RootNode root;

    private void Start()
    {
        Memory.AddProperty("ReactionSpeed", new BlackboardFloatProperty(2.0f));

        SeqenceNode seqence = new SeqenceNode();
        root = new RootNode(this, seqence);
        seqence.AttachChild(new PrintNode("its good to see you!"));
        seqence.AttachChild(new WaitForSecondsMemory(0.0f, "ReactionSpeed"));
        seqence.AttachChild(new PrintNode("you too!")
            .AttachService(new UpdateBlackboardFloatValueService(0.5f, "ReactionSpeed", 0.5f))
            );
    }


    private void FixedUpdate()
    {
        timer = Time.fixedDeltaTime;
        root.Execute();
    }
}
