using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    [SerializeField] float damageRaidus = 20f;
    [SerializeField] float explosionForce = 1200f;


    float countdown;


    bool hasExploded = false;
    public bool hasBeenThrown = false;


    public enum ThrowableType
    {
        None,
        Grenade,
        Smoke_Grenade
    }


    public ThrowableType throwableType;


    private void Start()
    {
        countdown = delay;
    }


    private void Update()
    {
        if (hasBeenThrown)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }


    private void Explode()
    {
        GetThrowableEffect();


        Destroy(gameObject);
    }

    private void GetThrowableEffect()
    {
        switch (throwableType)
        {
            case ThrowableType.Grenade:
                GrenadeEffect();
                break;
            case ThrowableType.Smoke_Grenade:
                SmokeGrenadeEffect();
                break;

            default:
                break;
        }
    }

    private void SmokeGrenadeEffect()
    {
        GameObject smokeEffect = GlobalRefernces.Instance.smokeGrenadeEffect;
        Instantiate(smokeEffect, transform.position, transform.rotation);


        SoundManager.Instance.throwablesChannel.PlayOneShot(SoundManager.Instance.grenadeSound);


        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRaidus);
        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
            }
        }
    }

    private void GrenadeEffect()
    {
        GameObject explosionEffect = GlobalRefernces.Instance.grenadeExplosionEffect;
        Instantiate(explosionEffect, transform.position,transform.rotation);


        SoundManager.Instance.throwablesChannel.PlayOneShot(SoundManager.Instance.grenadeSound);


        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRaidus);
        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, damageRaidus);
            }


            if(objectInRange.gameObject.GetComponent<Enemy>())
            {
                objectInRange.gameObject.GetComponent<Enemy>().TakeDamage(100);
            }


            if (objectInRange.gameObject.CompareTag("Player"))
            {
                objectInRange.gameObject.GetComponent<Player>().TakeDamage(75);
            }


            if (objectInRange.gameObject.CompareTag("Bear"))
            {
                objectInRange.gameObject.GetComponent<BearBottle>().Shatter();
                Destroy(gameObject);
            }
        }
    }

    public void CreateCarExplosionEffect()
    {
        GameObject carexplosionEffect = GlobalRefernces.Instance.carExplosionEffect;
        Instantiate(carexplosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRaidus);
        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
            }
        }
    }
}
