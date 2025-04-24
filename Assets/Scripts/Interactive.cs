using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractive
{
    public void Interact(Mob _mob);
}

public class Interactive : MonoBehaviour, IInteractive
{
    
    public virtual void Interact(Mob _mob)
    {
    } 
}
