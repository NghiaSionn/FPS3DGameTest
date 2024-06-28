using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;


    private NavMeshAgent navAgent;


    public bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if(HP <= 0)
        {
            int randomValue = Random.Range(0,2);


            if (randomValue == 0)
            {
                
                animator.SetTrigger("DIE1");
                //Destroy(gameObject, 4f);
            }
            else
            {
                animator.SetTrigger("DIE2");
                //Destroy(gameObject, 4f);
            }


            isDead = true;

            //Âm thanh chết
            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieDeath);
        }
        else
        {
            animator.SetTrigger("DAMAGE");

            if (SoundManager.Instance.zombieHurt != null)
            {
                SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieHurt);
            }
            
        }      
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); //Attacking - Stop Attacking


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 100f); //Detection (Start Cahsing)


        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 101f); //Stop chasing
    }


    
}
