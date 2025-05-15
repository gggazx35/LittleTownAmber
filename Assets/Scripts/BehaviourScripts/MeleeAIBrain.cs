using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIBrain : Brain
{
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] LayerMask groundMask;
    [SerializeField] int scopeRadius;
    [SerializeField] Movement movement;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        Memory.AddProperty("enemyMask", new BlackboardIntProperty(enemyMask));
        Memory.AddProperty("obstacleMask", new BlackboardIntProperty(obstacleMask));
        Memory.AddProperty("groundMask", new BlackboardIntProperty(groundMask));


        Memory.AddProperty("isFacingWall", new BlackboardBoolProperty(false));
        Memory.AddAnimatorLinkedProperty("isAttack", new BlackboardBoolProperty(false), animator);

        

        Memory.AddProperty("target", new BlackboardGameObjectProperty(null));
        Memory.AddAnimatorLinkedProperty("fallingSpeed", new BlackboardFloatProperty(0.0f), animator);
        Memory.AddAnimatorLinkedProperty("runningSpeed", new BlackboardFloatProperty(0.0f), animator);
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();

        SelectorNode firstNode = new SelectorNode();
        root = new RootNode(this, firstNode);

        firstNode.AttachService(new DetectEnemyService("enemyMask", "target", scopeRadius));
        //firstNode.AttachDecorator(new BlackBoardSetBoolDeco("isAttack", true));
        //firstNode.AttachService(new DetectWallService("obstacleMask", "isFacingWall", movement));


        var patrol = new Patrol("isFacingWall", movement)
            .AttachDecorator(new BlackBoardSetGameObjectDeco("target", true)
            
            );
        
        firstNode.AttachChild(patrol); // ( !null(set) + opposite ) == if target == null

        firstNode.AttachService(new DetectWallService("obstacleMask", "isFacingWall", movement));

        var seq = new SeqenceNode();
        
        seq.AttachChild(new ChaseTask("target", "isAttack", "isFacingWall", GetComponent<Movement>(), GetComponent<Mob>()));
        seq.AttachChild(new TryAttack(GetComponent<UsingWeapon>()));

        firstNode.AttachChild(
            seq
            );
    }

    
}
