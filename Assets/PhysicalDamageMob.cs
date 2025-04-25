using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDamageMob : MonoBehaviour
{

    private Mob mob;
    [SerializeField] private Transform head;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        mob = GetComponent<Mob>();
    }

    Collider2D isHeadHit()
    {
        return Physics2D.OverlapCircle(head.position, 0.2f, groundLayer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D physics2D = isHeadHit();
        if (physics2D.GetComponent<Meterial>())
        {
            
        }
    }
}
