using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasInventory
{
    Inventroy inventroy { get; }
    int selectedSlot { get; }
}

public interface IMobStat
{
    float maxHealth { get; }
    float health { get; set; }
    float strength { get; }
}

public interface IHumanMob : IHasInventory
{
    Hand hand { get; }
    void HoldItemAt(int i);
}

public interface IDamageable
{
    public void TakeDamage(Mob _cause, float _amount);
}

public class Mob : MonoBehaviour, IHumanMob, IMobStat, IDamageable
{
    protected Inventroy m_inventroy;
    protected int m_selectedSlot;
    [SerializeField] protected float m_defaultReach = 3.0f;


    [SerializeField] protected float m_maxHealth = 20.0f;
    [SerializeField] protected float m_health = 20.0f;
    [SerializeField] protected float m_strength = 1.0f;


    [SerializeField] protected LayerMask m_enemyMask;
    
    [SerializeField] protected Hand m_hand;

    // properties
    public Inventroy inventroy { get => m_inventroy; }
    public Hand hand { get => m_hand; }

    public int selectedSlot { get => m_selectedSlot; }
    public float maxHealth { get => m_maxHealth; }
    public float health 
    { 
        get => m_health; 
        set
        {
            m_health = Mathf.Clamp(value, 0.0f, maxHealth);
        } 
    }
    public float strength { get => m_strength; }


    protected Movement movement;

    // IHumanMob
    public void HoldItemAt(int i)
    {
        hand.GrabItem(inventroy.GetItemAt(i));
        m_selectedSlot = i;
    }

    // IDamageable
    public void TakeDamage(Mob _cause, float _amount)
    {
        m_health = Mathf.Clamp(health - _cause.CalcuateDamage(_amount), 0.0f, maxHealth);
    }

    public int GetEnemyMask()
    {
        return m_enemyMask; 
    }

    public float CalcuateDamage(float _damage)
    {
        return strength + _damage;
    }

    

    public Mob InRange()
    {
        //Debug.Log("FSAIJJOFJNHIOAUGBYIUS");
        if (hand.handedItem is IDetectRange)
        {
            return (hand.handedItem as IDetectRange).InRange(this, selectedSlot);
        }
        return null;
    }

    public void UseItem()
    {
        hand.Use(this, selectedSlot);
    }

    public RaycastHit2D RaycastAt(float distance, int layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.GetDirection(), m_defaultReach * distance, layer);
        Debug.DrawRay(transform.position, movement.GetDirection(), Color.green, m_defaultReach * distance);
        return hit;
    }
    public RaycastHit2D RaycastAt(int layer)
    {
        return RaycastAt(1.0f, layer);
    }

    public Item MoveHoldingItem()
    {
        inventroy.RemoveItemAt(selectedSlot);
        return inventroy.RemoveItemAt(selectedSlot);
    }
}
