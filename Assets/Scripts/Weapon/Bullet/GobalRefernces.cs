using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRefernces : MonoBehaviour
{
    public static GlobalRefernces Instance { get; set; }


    public GameObject bulletImpactEffectPrefab;


    public GameObject grenadeExplosionEffect;
    public GameObject smokeGrenadeEffect;
    public GameObject bloodSprayEffect;
    public GameObject bloodDeadEffect;
    public GameObject carExplosionEffect;


    private void Awake()
    {
        if(Instance != null && Instance != this )
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
