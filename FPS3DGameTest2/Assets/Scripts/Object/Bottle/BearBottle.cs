using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();


    // Start is called before the first frame update
    public void Shatter()
    {
        foreach (Rigidbody part in allParts)
        {
            part.isKinematic = false;
        }

        Destroy(gameObject, 3f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
