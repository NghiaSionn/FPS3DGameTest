using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timeForDestruction;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf(timeForDestruction));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator DestroySelf(float timeForDestruction)
    {
        yield return new WaitForSeconds(timeForDestruction);


        Destroy(gameObject);
    }
}
