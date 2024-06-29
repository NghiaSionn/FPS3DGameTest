using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lean : MonoBehaviour
{
    public Animator cameraAnim;


    public LayerMask layers;
    RaycastHit hit;


    void Update()
    {
        if(Input.GetKey(KeyCode.Q) && !Physics.Raycast(transform.position, -transform.right, out hit, 1f,layers))
        {
            Debug.Log("Nghieng trai");
            cameraAnim.ResetTrigger("idle");
            cameraAnim.ResetTrigger("right");
            cameraAnim.SetTrigger("left");

        }

        else if (Input.GetKey(KeyCode.E) && !Physics.Raycast(transform.position, transform.right, out hit, 1f, layers))
        {
            Debug.Log("Nghieng phai");
            cameraAnim.ResetTrigger("idle");
            cameraAnim.ResetTrigger("left");
            cameraAnim.SetTrigger("right");

        }


        else
        {
            cameraAnim.ResetTrigger("left");
            cameraAnim.ResetTrigger("right");
            cameraAnim.SetTrigger("idle");

        }
    }
}
