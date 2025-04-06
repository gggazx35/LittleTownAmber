using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statement : MonoBehaviour
{
    List<string> StatementLookup = new List<string>();
    byte[][] interactions = new byte[8][];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RegisterStatement(string _name)
    {

        StatementLookup.Add(_name);
    }

    void setInteractionIgnore(string _source,  string _target)
    {
        int x = StatementLookup.IndexOf(_source);
        int y = StatementLookup.IndexOf(_target);

        interactions[x][y] = 1;
    }
}
