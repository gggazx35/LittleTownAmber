using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIBrain : MobBrain
{
    [SerializeField] int scopeRadius;
    [SerializeField] Movement movement;

    private void Awake()
    {
        AddPropertiesFromMobBrain();

        Memory.AddProperty("isFacingWall", new BlackboardBoolProperty(false));
        Memory.AddProperty("target", new BlackboardGameObjectProperty(null));
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
