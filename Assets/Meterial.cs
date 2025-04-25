using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Meterial : MonoBehaviour
{
    [SerializeField] MaterialData materialData;
    Rigidbody2D rb;
    void MobStepped(MobStep e)
    {

    }

    void MobFallen(MobFall e)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EventBus.get().Subscribe<MobStep>(gameObject, MobStepped);
        EventBus.get().Subscribe<MobFall>(gameObject, MobFallen);
    }

    private void OnDestroy()
    {
        EventBus.get().Unsubscribe<MobStep>(gameObject, MobStepped);
        EventBus.get().Unsubscribe<MobFall>(gameObject, MobFallen);
    }
}
