using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIBrain : Brain
{
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] int scopeRadius;
    [SerializeField] Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();

        Memory.AddProperty("enemyMask", new BlackboardIntProperty(enemyMask));
        Memory.AddProperty("obstacleMask", new BlackboardIntProperty(obstacleMask));


        Memory.AddProperty("isFacingWall", new BlackboardBoolProperty(false));

        Memory.AddProperty("target", new BlackboardGameObjectProperty(null));

        SeqenceNode firstNode = new SeqenceNode();
        root = new RootNode(this, firstNode);

        firstNode.AttachService(new DetectEnemyService("enemyMask", "target", scopeRadius));
        //firstNode.AttachService(new DetectWallService("obstacleMask", "isFacingWall", movement));


        var patrol = new Patrol("isFacingWall", movement)
            .AttachDecorator(new BlackBoardSetGameObjectDeco("target", true));
        
        firstNode.AttachChild(patrol); // ( !null(set) + opposite ) == if target == null

        patrol.AttachService(new DetectWallService("obstacleMask", "isFacingWall", movement));
        

        firstNode.AttachChild(new ChaseTask("target", GetComponent<Movement>()));

    }

    
}
