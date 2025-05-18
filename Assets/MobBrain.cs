using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBrain : Brain
{
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected LayerMask obstacleMask;
    [SerializeField] protected LayerMask groundMask;

    protected void AddPropertiesFromMobBrain()
    {
        Animator animator = GetComponent<Animator>();
        Memory.AddProperty("enemyMask", new BlackboardIntProperty(enemyMask));
        Memory.AddProperty("obstacleMask", new BlackboardIntProperty(obstacleMask));
        Memory.AddProperty("groundMask", new BlackboardIntProperty(groundMask));


        Memory.AddAnimatorLinkedProperty("isAttack", new BlackboardBoolProperty(false), animator);

        Memory.AddAnimatorLinkedProperty("fallingSpeed", new BlackboardFloatProperty(0.0f), animator);
        Memory.AddAnimatorLinkedProperty("runningSpeed", new BlackboardFloatProperty(0.0f), animator);
    }
}
