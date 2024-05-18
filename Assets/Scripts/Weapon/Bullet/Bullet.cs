using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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


        if (objectWeHit.gameObject.CompareTag("Bear"))
        {
            Debug.Log("Bắn vào chai");
            objectWeHit.gameObject.GetComponent<BearBottle>().Shatter();

        }
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
    }
}
