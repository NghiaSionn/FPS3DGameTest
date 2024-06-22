using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;


    private NavMeshAgent navAgent;


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
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); //Attacking - Stop Attacking


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 18f); //Detection (Start Cahsing)


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 21f); //Stop chasing
    }

}
