using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraChange : MonoBehaviour
{
    public GameObject thirdCam;
    public GameObject firstCam;
    public int camMode;
 


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance?.activeWeaponSlot?.GetComponentInChildren<Weapon>();


        if (Input.GetKeyDown(KeyCode.V))
        {
            // Check if not scoped before changing camera
            if (!activeWeapon.isADS)
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
