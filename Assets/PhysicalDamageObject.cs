using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDamageObject : MonoBehaviour
{
    private Rigidbody2D rb;
    private Health health;
    [SerializeField] private Vector2 exPos;
    [SerializeField] private float fallling;
    [SerializeField] private float fal;
    [SerializeField] private bool isFalling;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

    }

    public float CalcuateDamage()
    {
        return fallling - fal;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0.0f && exPos.y > transform.position.y)
        {
            fallling -= transform.position.y - exPos.y;
            isFalling = true;
        }
        else
        {
            if (isFalling)
            {
                isFalling = false;
                if (fallling > fal)
                {
                    health.TakeDamage(CalcuateDamage());
                    var phy = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
                    phy?.GetComponent<Health>()?.TakeDamage(CalcuateDamage());
                }
                fallling = 0;
            }
        }
        exPos = transform.position;
    }

    /*// Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if(collision.gameObject)
        }
    }*/
}
