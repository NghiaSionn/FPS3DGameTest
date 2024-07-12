using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<MouseMovement>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
