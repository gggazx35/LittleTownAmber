using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [SerializeField] AudioSource audioSfx;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public static SoundManager get()
    {
        return instance;
    }

    // Update is called once per frame
    public void Play(AudioClip esfx)
    {
        
    }
}
