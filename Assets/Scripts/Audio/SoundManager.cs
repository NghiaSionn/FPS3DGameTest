using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using static Weapon;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }


    public AudioSource shootingChannel;


    public AudioClip Ak47Shot;
    public AudioClip SniperShot;
 

    public AudioSource reloadSoundAK48;
    public AudioSource reloadSniper;


    public AudioSource emtyshooting;


    public AudioSource throwablesChannel;
    public AudioClip grenadeSound;


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


    public void PlayShootingSound(WeaponModel weapon)
    {
        switch ( weapon )
        {
            case WeaponModel.AK47:
                shootingChannel.PlayOneShot(Ak47Shot);
                break;
            case WeaponModel.Sniper:
                shootingChannel.PlayOneShot(SniperShot);
                break;
        }
    }


    public void PlayReloadSound(WeaponModel weapon) 
    {
        switch (weapon)
        {
            case WeaponModel.AK47:
                reloadSoundAK48.Play();
                break;
            case WeaponModel.Sniper:
                reloadSniper.Play();
                break;
        }
    }
}
