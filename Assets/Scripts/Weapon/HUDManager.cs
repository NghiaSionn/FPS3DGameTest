using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }


    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;


    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;


    [Header("Lethal")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;


    [Header("Tactical")]
    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;


    public Sprite emtySlot;
    public Sprite greySlot;


    public GameObject middleDot;


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
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();


        if(activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.thisWeaponModel)}";


            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);


            activeWeaponUI.sprite = GetWeaponSprite(model);


            if(unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
            }

        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";


            ammoTypeUI.sprite = emtySlot;


            activeWeaponUI.sprite = emtySlot;
            unActiveWeaponUI.sprite= emtySlot;
        }


        if(WeaponManager.Instance.lethalsCount <= 0)
        {
            lethalUI.sprite = greySlot;
        }


        if (WeaponManager.Instance.tacticalsCount <= 0)
        {
            tacticalUI.sprite = greySlot;
        }
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        switch(model)
        {
            case Weapon.WeaponModel.AK47:
                return Resources.Load<GameObject>("AK47_Weapon").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.Sniper:
                return Resources.Load<GameObject>("Sniper_Weapon").GetComponent<SpriteRenderer>().sprite;


            default:
                return null;
        }
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.AK47:
                return Resources.Load<GameObject>("Rifle_Ammo").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.Sniper:
                return Resources.Load<GameObject>("Sniper_Ammo").GetComponent<SpriteRenderer>().sprite;


            default:
                return null;
        }
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        foreach(GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if(weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }

        return null;
    }

    internal void UpdateThrowablesUI()
    {
        lethalAmountUI.text = $"{WeaponManager.Instance.lethalsCount}";
        tacticalAmountUI.text = $"{WeaponManager.Instance.tacticalsCount}";


        switch (WeaponManager.Instance.equippedLethalType)
        {
            case Throwable.ThrowableType.Grenade:                
                lethalUI.sprite = Resources.Load<GameObject>("Grenade").GetComponent<SpriteRenderer>().sprite;
                break;
            default:
                break;
        }


        switch (WeaponManager.Instance.equippedTacticalType)
        {
            case Throwable.ThrowableType.Smoke_Grenade:
                tacticalUI.sprite = Resources.Load<GameObject>("Smoke_Grenade").GetComponent<SpriteRenderer>().sprite;
                break;
            default:
                break;
        }
    }
}
