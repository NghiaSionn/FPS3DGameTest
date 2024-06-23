using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;


    private void OnCollisionEnter(Collision objectWeHit)
    {
       if (objectWeHit.gameObject.CompareTag("Target"))
        {
            Debug.Log("Trúng mục tiêu");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }


        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Bắn vào tường");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }


        if (objectWeHit.gameObject.CompareTag("Plane"))
        {
            Debug.Log("Bắn vào đất");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }


        if (objectWeHit.gameObject.CompareTag("Bear"))
        {
            Debug.Log("Bắn vào chai");      
            
            objectWeHit.gameObject.GetComponent<BearBottle>().Shatter();
            Destroy(gameObject);

        }


        if (objectWeHit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bắn vào thây ma");


            if(objectWeHit.gameObject.GetComponent<Enemy>().isDead == false)
            {
                objectWeHit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            }
            

            CreateBloodSprayEffect(objectWeHit);
            Destroy(gameObject);
        }
    }

    private void CreateBloodSprayEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(
            GlobalRefernces.Instance.bloodSprayEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        bloodSprayPrefab.transform.SetParent(objectWeHit.gameObject.transform);


        Destroy(bloodSprayPrefab, 10f);
    }


    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalRefernces.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objectWeHit.gameObject.transform);


        Destroy(hole,5f);
    }
}
