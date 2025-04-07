using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    void Save();
    void Load(string path);
}

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
