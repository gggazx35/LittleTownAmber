using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IKeyNeededObject
{ 
    public int keyId { get; }
    
    public void Unlock(int _keyId);
}

public class Door : MonoBehaviour
{
    public int keyId;

    public void Unlock(int _keyId)
    {
        if(keyId == _keyId)
        {
            Debug.Log("Unlocked");
            gameObject.SetActive(false);
        } else
        {
            Debug.Log("Nope");
        }
    }
    
}
