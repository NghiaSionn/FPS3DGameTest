using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject thirdCam;
    public GameObject firstCam;
    public int camMode;
    public Weapon weapon;  // Reference to the Weapon script


    // Start is called before the first frame update
    void Start()
    {
        // Optionally, find the Weapon script if not set in the inspector
        if (weapon == null)
        {
            weapon = FindObjectOfType<Weapon>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Check if not scoped before changing camera
            if (!weapon.isScoped)
            {
                
                if (camMode == 1)
                {
                    camMode = 0;
                }
                else
                {
                    camMode += 1;
                }

                StartCoroutine(CamChange());
            }
        }
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (camMode == 0)
        {
            thirdCam.SetActive(true);
            firstCam.SetActive(false);
        }

        if (camMode == 1)
        {
            thirdCam.SetActive(false);
            firstCam.SetActive(true);
        }
    }
}
