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
    private UsingWeapon weapon;

    private RaycastHit2D hit;
    public GameObject target;
    public bool inRange;


    public float ScopeRadius = 1.0f;
    [SerializeField] private float playerDetectRange = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //intTimer = timer;
        movement = GetComponent<Movement>();
        mob = GetComponent<Mob>();
        weapon = GetComponent<UsingWeapon>();
    }

    void FixedUpdate()
    {
        /*if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
            RaycastDebugger();
        }
*/
        movement.Move(0.0f);
        if (weapon.IsAttack()) return;
        Check();
        //OnScope();
        //if (target != null) { movement.Target(target.transform.position); }
        if (inRange)
        {
            
            movement.Target(target.transform.position);
            //Debug.DrawRay(transform.position, (movement.isFacingRight ? Vector2.right : Vector2.left) * 5.0f, Color.green);
            
            EnemyLogic();
        } else
        {
            Patrol();
        }
        movement.PlayerFlip();
    }

    void EnemyLogic()
    {
        //distance = movement.GetDirection().x;

        
        if (!(mob.GetHoldingItem()?.GetItemTag() is WeaponItemTag)) return;
        
        weapon.TryAttack(mob.GetHoldingItem()?.GetItemTag() as WeaponItemTag, playerMask);
        

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

            movement.MoveFacingDirection(-.5f);
            //movement.Flip();
        } else 
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
            if (FacingPlayer()) inRange = true;
            //    inRange = true;
            //else
            //    inRange = false;
        }
        else
        {
             inRange = false;
        }
    }
    private RaycastHit2D FacingWall(int _mask)
    {
        return Physics2D.Raycast(feet.position, movement.FacingRight() ? Vector2.left : Vector2.right, 0.5f, _mask);
    }

    private bool FacingPlayer()
    {
        
        //RaycastHit2D hits = Physics2D.Raycast(eyes.position, direction, playerDetectRange, (-1) - (1 << LayerMask.NameToLayer("Enemy")));
        //if (!hits) return false;
        //if(hits.transform.gameObject == target)
        //{
        //    Debug.Log(hits.transform.gameObject);
        //    return true;
        //}

        for(int i = -1; i < 1; i++)
        {
            Vector2 direction = new Vector2(
            target.transform.position.x - transform.position.x,
            target.transform.position.y + i - transform.position.y
            ).normalized;
            if (g(direction)) return true;
        }

        return false;
    }

    private bool g(Vector2 direction)
    {
        RaycastHit2D hits = Physics2D.Raycast(eyes.position, direction, playerDetectRange, (-1) - (1 << LayerMask.NameToLayer("Enemy")));
        if (!hits) return false;
        if (hits.transform.gameObject == target)
        {
            Debug.Log(hits.transform.gameObject);
            return true;
        }
        return false;
    }

}
