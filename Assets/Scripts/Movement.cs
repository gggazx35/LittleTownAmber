using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Vector2 moveAxis;

    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float jumpingPower = 5f;
    [SerializeField] private bool isFacingRight = false;
    protected Rigidbody2D rb;
    private Animator animator;
    private Mob mob;
    [SerializeField] private Transform hand;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform headCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] protected Vector2 direction;
    [SerializeField] private Vector2 exPos;

    public Transform GroundCheck => groundCheck;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mob = GetComponent<Mob>();
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveAxis.x * speed, rb.velocity.y);
        //var h = HeadHit();
        //if (h != null)
        //{
        //    Debug.Log("Auto");
        //    mob.TakeDamage(DamageReason.None, h.GetComponent<PhysicalDamageObject>().CalcuateDamage());
        //} 

        


        if (animator != null)
        {
            animator.SetFloat("FallingSpeed", rb.velocity.y);
            if ((transform.position.x > exPos.x || transform.position.x < exPos.x) && (moveAxis.x > 0.0f || moveAxis.x < 0.0f))
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                //if(moveAxis.x > 0.0f || moveAxis.x < 0.0f)
                //{
                //    var push = Physics2D.OverlapCircle(hand.position, 0.2f, groundLayer);
                //    if(push && push.CompareTag("Push"))
                //    {
                //        Debug.Log("pfs");
                //        push.GetComponent<Rigidbody2D>().AddForce(Vector2.left * moveAxis.x, ForceMode2D.Impulse);
                //    }
                //}
                animator.SetBool("Walk", false);
            }
            //Debug.Log(animator.GetFloat("Walk"));
        }
        exPos = transform.position;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    public Collider2D HeadHit()
    {
        return Physics2D.OverlapCircle(headCheck.position, 0.2f, groundLayer);
    }

    protected void PointAt()
    {
        //Vector2 target = new Vector2(Mathf.Abs(_target.x), _target.y);
        if (isFacingRight)
        {
            //hand.right = direction.normalized;
        }
        else
        {
           // hand.right = -(direction.normalized);
        }
    }

    public void Target(Vector3 _target)
    {
        //Vector3 mousePosition = Input.mousePosition;
        //Camera.main.ScreenToWorldPoint(mousePosition);
        direction = new Vector2(
            _target.x - transform.position.x,
            _target.y - transform.position.y
            );
        PointAt();
    }

    public void UpdateFlip()
    {
        if(isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f)
        {
            Flip();
        }
        /*if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Flip();
        }*/
    }

    public void PlayerFlip()
    {
        if (moveAxis.x < 0f && !isFacingRight)
        {
            Flip();
        }

        if (moveAxis.x > 0f && isFacingRight)
        {
            Flip();
        }
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void MoveByAxis(float x)
    {
        moveAxis.x = x;
    }

    public void Move(float x)
    {
        moveAxis.x = x;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpingPower);
        }
    }

    public bool FacingRight()
    {
        return isFacingRight;
    }

    public Vector2 GetDirection()
    {
        return direction.normalized;
    }

    public void MoveFacingDirection(float x)
    {
        if (isFacingRight)
        {
            Move(-x);
            //StopAttack();
        } else
        {
            Move(x);
        }
    }

    private RaycastHit2D FacingWall(int _mask)
    {
        return Physics2D.Raycast(groundCheck.position, FacingRight() ? Vector2.left : Vector2.right, 0.5f, _mask);
    }
}
