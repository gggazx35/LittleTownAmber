using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Vector2 moveAxis;

    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float jumpingPower = 5f;
    [SerializeField] private bool isFacingRight = false;
    protected Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] protected Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveAxis.x * speed, rb.velocity.y);
        if (animator != null)
        {
            if(!isFacingRight)
            {
                animator.SetFloat("Walk", -moveAxis.x);
            } else
            {
                animator.SetFloat("Walk", moveAxis.x);
            }
            //Debug.Log(animator.GetFloat("Walk"));
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
            MoveByAxis(x);
            //StopAttack();
        } else
        {
            MoveByAxis(-x);
        }
    }
}
