using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;
    public Throwable hoveredThrowable = null;


    public TextMeshProUGUI interactionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        interactionText.gameObject.SetActive(false);


        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        bool weaponHit = false;
        bool ammoBoxHit = false;
        bool throwableHit = false;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            // Weapon
            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {
                weaponHit = true;
                if (hoveredWeapon != objectHitByRaycast.GetComponent<Weapon>())
                {
                    if (hoveredWeapon != null)
                    {
                        hoveredWeapon.GetComponent<Outline>().enabled = false;
                    }
                    hoveredWeapon = objectHitByRaycast.GetComponent<Weapon>();
                    hoveredWeapon.GetComponent<Outline>().enabled = true;
                }


                interactionText.gameObject.SetActive(true); 
                interactionText.text = "Press F";


                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
                }
            }

            // Ammo Box
            if (objectHitByRaycast.GetComponent<AmmoBox>())
            {
                ammoBoxHit = true;
                if (hoveredAmmoBox != objectHitByRaycast.GetComponent<AmmoBox>())
                {
                    if (hoveredAmmoBox != null)
                    {
                        hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    }
                    hoveredAmmoBox = objectHitByRaycast.GetComponent<AmmoBox>();
                    hoveredAmmoBox.GetComponent<Outline>().enabled = true;
                }


                interactionText.gameObject.SetActive(true); 
                interactionText.text = "Press F";


                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);
                    Destroy(objectHitByRaycast.gameObject);
                }
            }


            // Throwable
            if (objectHitByRaycast.GetComponent<Throwable>())
            {
                throwableHit = true;
                if (hoveredThrowable != objectHitByRaycast.GetComponent<Throwable>())
                {
                    if (hoveredThrowable != null)
                    {
                        hoveredThrowable.GetComponent<Outline>().enabled = false;
                    }


                    hoveredThrowable = objectHitByRaycast.GetComponent<Throwable>();
                    hoveredThrowable.GetComponent<Outline>().enabled = true;
                }


                interactionText.gameObject.SetActive(true);
                interactionText.text = "Press F";


                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupThrowable(hoveredThrowable);

                }
            }
        }


        if (!weaponHit && hoveredWeapon != null)
        {
            hoveredWeapon.GetComponent<Outline>().enabled = false;
            hoveredWeapon = null;
        }


        if (!ammoBoxHit && hoveredAmmoBox != null)
        {
            hoveredAmmoBox.GetComponent<Outline>().enabled = false;
            hoveredAmmoBox = null;
        }


        if (!throwableHit && hoveredThrowable != null)
        {
            hoveredThrowable.GetComponent<Outline>().enabled = false;
            hoveredThrowable = null;
        }
    }
}
