using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    public Transform feet;
    public Transform eyes;
    public LayerMask playerMask;
    public LayerMask obstactionMask;

    private Movement movement;
    private Mob mob;

    private RaycastHit2D hit;
    public GameObject target;
    private float distance;
    public bool inRange;


    public float ScopeRadius = 1.0f;
    [SerializeField] private float playerDetectRange = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //intTimer = timer;
        movement = GetComponent<Movement>();
        mob = GetComponent<Mob>();
    }

    void FixedUpdate()
    {
        /*if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
            RaycastDebugger();
        }
*/
        movement.MoveByAxis(0.0f);
        Check();
        //OnScope();
        //if (target != null) { movement.Target(target.transform.position); }
        if (inRange)
        {
            
            movement.Target(target.transform.position);
            //Debug.DrawRay(transform.position, (movement.isFacingRight ? Vector2.right : Vector2.left) * 5.0f, Color.green);
            movement.UpdateFlip();
            EnemyLogic();
        } else
        {
            Patrol();
        }
    }

    void EnemyLogic()
    {
        //distance = movement.GetDirection().x;

        if (mob.InRange() != null)
        {
            mob.Use();
            return;
        }

        movement.MoveFacingDirection(1.0f);
        hit = FacingWall(obstactionMask);
        if (hit)
        {
            movement.Jump();
        }

        
    }

    void Patrol()
    {
        hit = FacingWall(obstactionMask);
        if (hit)
        {
            //movement.Jump();
            Debug.Log("Jump");

            movement.Flip();
        }
        movement.MoveFacingDirection(.5f);
    }

    private void Check()
    {
        //if (inRange) return;
        Collider2D onSight = Physics2D.OverlapCircle(transform.position, ScopeRadius, playerMask);
        //hit =  /*RayCheck(5.0f, raycastMask);*/
        if (onSight)
        {
            //Debug.Log("CHFAH");
            target = onSight.transform.gameObject;
            if (FacingPlayer())
                inRange = true;
            else
                inRange = false;
        }
    }
    private RaycastHit2D FacingWall(int _mask)
    {
        return Physics2D.Raycast(feet.position, (movement.FacingRight() ? Vector2.right : Vector2.left), 0.5f, _mask);
    }

    private bool FacingPlayer()
    {
        Vector2 direction = new Vector2(
        target.transform.position.x - transform.position.x,
            target.transform.position.y - transform.position.y
            ).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(eyes.position, direction, playerDetectRange);
        if (hits.Length == 0) return false;
        if(hits[0].transform.gameObject == target)
        {
            return true;
        }

        return false;
    }


}
