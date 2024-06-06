using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CarEnterExitSystem : MonoBehaviour
{

    public MonoBehaviour CarController;
    public Transform Car;
    public Transform Player;

    
    public GameObject PlayerCam;
    public GameObject CarCam;

    public GameObject DriveUi;
    public GameObject playerPosition;
    public GameObject middlePoint;
    public GameObject ammo;

    bool Candrive;



    // Start is called before the first frame update
    void Start()
    {
        CarController.enabled = false;
        DriveUi.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && Candrive)  
        {

            CarController.enabled = true; 
            

            DriveUi.gameObject.SetActive(false);
            playerPosition.gameObject.SetActive(false);
            middlePoint.gameObject.SetActive(false);
            ammo.gameObject.SetActive(false);


            // Here we parent Car with player
            Player.transform.SetParent(Car);
            Player.gameObject.SetActive(false);
           

            // Camera
            PlayerCam.gameObject.SetActive(false);
            CarCam.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            CarController.enabled = false; 


            playerPosition.gameObject.SetActive(true);
            middlePoint.gameObject.SetActive(true);
            ammo.gameObject.SetActive(true);


            // Here We Unparent the Player with Car
            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);

           

            PlayerCam.gameObject.SetActive(true);
            CarCam.gameObject.SetActive(false);
        }
    }


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            DriveUi.gameObject.SetActive(true);
            Candrive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            DriveUi.gameObject.SetActive(false);
            Candrive = false;
        }
    }
}