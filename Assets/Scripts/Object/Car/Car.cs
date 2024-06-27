using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private int HP = 100;

    public bool isDead;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            isDead = true;
            //Âm thanh chết
            SoundManager.Instance.carExplosion.Play();
            SoundManager.Instance.carAlarm.Stop();

            
            Destroy(gameObject);
            
        }
        else
        {
            //Âm thanh khi nhận dame
            if (!SoundManager.Instance.carAlarm.isPlaying)
            {
                SoundManager.Instance.carAlarm.Play();
            }
        }
    }


    

}
