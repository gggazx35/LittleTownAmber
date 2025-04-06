using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    protected Inventroy inventroy;
    //[SerializeField] protected Vector2 direction;
    protected int selectedSlot;
    [SerializeField] protected float defaultReach = 3.0f;


    [SerializeField] protected float maxHealth = 20.0f;
    [SerializeField] protected float health = 20.0f;
    [SerializeField] protected float strength = 1.0f;


    [SerializeField] protected LayerMask enemyMask;
    
    [SerializeField] protected Hand hand;


    protected Movement movement;
    public void SelectItemAt(int i)
    {
        hand.GrabItem(inventroy.GetItemAt(i));
        selectedSlot = i;
    }

    public Inventroy GetInventory()
    {
        return inventroy;
    }

    public RaycastHit2D RaycastAt(float distance, int layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.GetDirection(), defaultReach * distance, layer);
        Debug.DrawRay(transform.position, movement.GetDirection(), Color.green, defaultReach * distance);
        return hit;
    }
    public RaycastHit2D RaycastAt(int layer)
    {
        return RaycastAt(1.0f, layer);
    }

    public int GetEnemyMask()
    {
        return enemyMask; 
    }

    public float CalcuateDamage(float _damage)
    {
        return strength + _damage;
    }

    public void TakeDamage(Mob _cause, float _amount)
    {
        health = Mathf.Clamp(health - _cause.CalcuateDamage(_amount), 0.0f, maxHealth);
        
    }

    public Mob InRange()
    {
        //Debug.Log("FSAIJJOFJNHIOAUGBYIUS");
        return hand.InRange(this, selectedSlot);
    }

    public void Use()
    {
        hand.Use(this, selectedSlot);
    }
}
