using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MobBrain
{
    // Start is called before the first frame update
    private void Awake()
    {
        AddPropertiesFromMobBrain();
    }
}
