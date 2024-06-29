using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;
    bool isOpening;

    private Transform playerTransform;

    private void Start()
    {
       
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        isOpening = false;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {

            if (hit.collider.gameObject.tag == "Door")
            {
                GameObject doorParent = hit.collider.transform.root.gameObject;
                Animator doorAnim = doorParent.GetComponent<Animator>();

                intText.SetActive(true);

                if (!isOpening && Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Giu F");
                    doorAnim.ResetTrigger("idleDoor");
                    doorAnim.ResetTrigger("closeDoor");
                    doorAnim.SetTrigger("openDoor");
                }
                
                if(isOpening && Input.GetKeyUp(KeyCode.F))
                {
                    Debug.Log("Tha F");
                    doorAnim.ResetTrigger("idleDoor");
                    doorAnim.ResetTrigger("openDoor");
                    doorAnim.SetTrigger("closeDoor");
                }

                else
                {
                    doorAnim.ResetTrigger("openDoor");
                    doorAnim.ResetTrigger("closeDoor");
                    doorAnim.SetTrigger("idleDoor");
                }
            }
            else
            {
                intText.SetActive(false);
            }
        }
        else
        {
            intText.SetActive(false);
        }
    }
}