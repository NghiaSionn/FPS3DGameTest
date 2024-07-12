using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorManager : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;

    private Transform playerTransform;
    private List<Animator> doorAnimators = new List<Animator>(); // List of all door animators

    private void Start()
    {
        playerTransform = Camera.main.transform;

        // Find all objects with the "Door" tag and add their animators to the list
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (var door in doors)
        {
            Animator animator = door.GetComponentInChildren<Animator>(); // Use GetComponentInChildren to find Animator in children
            if (animator != null)
            {
                doorAnimators.Add(animator);
            }
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                intText.SetActive(true);
                Animator currentDoorAnimator = hit.collider.GetComponentInChildren<Animator>();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    ToggleDoorState(currentDoorAnimator);
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

    private void ToggleDoorState(Animator doorAnimator)
    {
        if (doorAnimator == null)
        {
            Debug.LogWarning("No door animator found.");
            return;
        }

        bool isOpen = doorAnimator.GetBool("openDoor");
        doorAnimator.SetBool("openDoor", !isOpen);

        if(!isOpen)
        {
            SoundManager.Instance.doorChannel.PlayOneShot(SoundManager.Instance.openDoor);
        }
        else
        {
            SoundManager.Instance.doorChannel.PlayOneShot(SoundManager.Instance.closeDoor);
        }

        Debug.Log("Toggling door state");
    }
}
