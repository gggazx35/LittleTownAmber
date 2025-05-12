using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class DetectEnemyService : BehaviourService
{
    private TypedID enemyMask;
    private TypedID detectedEnemy;
    private int scopeRadius;
    public DetectEnemyService(string _mask, string _enemy, int _scopeRadius)
    {
        enemyMask = new TypedID(_mask, typeof(BlackboardIntProperty));
        detectedEnemy = new TypedID(_enemy, typeof(BlackboardGameObjectProperty));
        scopeRadius = _scopeRadius;
    }

    protected override void TickNode(float deltaSeconds, Brain brain)
    {
        Debug.Log("interval");
        var mask = brain.Memory.GetProperty<BlackboardIntProperty>(enemyMask);
        var target = brain.Memory.GetProperty<BlackboardGameObjectProperty>(detectedEnemy);

        Collider2D onSight = Physics2D.OverlapCircle(brain.transform.position, scopeRadius, mask.Get());

        if (onSight)
        {
            Debug.Log("Detected");
            target.Set(onSight.transform.gameObject);
        }
    }
}

public class DetectWallService : BehaviourService
{
    private TypedID wallMask;
    private TypedID wallDetected;
    private int scopeRadius;
    private Movement movement;
    public DetectWallService(string i_mask, string b_detectWall, Movement _movement)
    {
        wallMask = new TypedID(i_mask, typeof(BlackboardIntProperty));
        wallDetected = new TypedID(b_detectWall, typeof(BlackboardBoolProperty));
        movement = _movement;
    }

    protected override void TickNode(float deltaSeconds, Brain brain)
    {
        Debug.Log("interval");
        var mask = brain.Memory.GetProperty<BlackboardIntProperty>(wallMask);
        var detected = brain.Memory.GetProperty<BlackboardBoolProperty>(wallDetected);

        var wall = Physics2D.Raycast(movement.GroundCheck.position, movement.FacingRight() ? Vector2.left : Vector2.right, 0.5f, mask.Get());

        if (wall)
        {
            detected.Set(true);
        }
        else
            detected.Set(false);
    }
}

