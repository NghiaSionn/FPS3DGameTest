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
                CreateBloodSprayEffect(objectWeHit);
            }
            
            
            if (objectWeHit.gameObject.GetComponent<Enemy>().isDead == true)
            {
                CreateBloodDeadEffect(objectWeHit);
            }
                      

            Destroy(gameObject);
        }

        if(objectWeHit.gameObject.CompareTag("Car"))
        {           
            if (objectWeHit.gameObject.GetComponent<Car>().isDead == false)
            {
                objectWeHit.gameObject.GetComponent<Car>().TakeDamage(bulletDamage);              
            }
            else
            {
                CreateCarExplosionEffect(objectWeHit);
            }

            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }
    }


    private void CreateBloodDeadEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject bloodDeadPrefab = Instantiate(
            GlobalRefernces.Instance.bloodDeadEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        bloodDeadPrefab.transform.SetParent(objectWeHit.gameObject.transform);


        Destroy(bloodDeadPrefab,2f);
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


        Destroy(bloodSprayPrefab, 3f);
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

    public void CreateCarExplosionEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject carExplosionPrefabs = Instantiate(
            GlobalRefernces.Instance.carExplosionEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        carExplosionPrefabs.transform.SetParent(objectWeHit.gameObject.transform);


        Destroy(carExplosionPrefabs, 5f);
    }
}
