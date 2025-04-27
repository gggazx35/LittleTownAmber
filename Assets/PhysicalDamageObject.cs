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
        fal = fal / (0.5f + (rb.mass / 2));
    }

    private float Damage(float _force)
    { 
        return Mathf.Clamp(_force, 0.01f, 20.0f) / rb.mass;
    }

    public void TakeDamage(float _force, float _mass)
    {
        health.TakeDamage(Damage(_force) * _mass);
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
                    health.TakeDamage(Damage(fallling - fal));/*
                    var phy = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
                    phy?.GetComponent<PhysicalDamageObject>()?.TakeDamage(fallling  - fal, rb.mass);*/
                }
                fallling = 0;
            }
        }
        exPos = transform.position;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var rbx = collision.gameObject.GetComponent<PhysicalDamageObject>();
        if (rbx != null && isFalling)
        {
            if(collision.gameObject.transform.position.y < gameObject.transform.position.y)
            {
                rbx.TakeDamage(fallling - fal, rb.mass);
            }
        }
    }
}
