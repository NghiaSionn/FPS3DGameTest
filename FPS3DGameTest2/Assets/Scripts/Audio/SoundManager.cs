using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using static Weapon;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    private bool isPlayingSound;

    [Header("Âm thanh bắn")]
    public AudioSource shootingChannel;
    public AudioSource shootingChannel2;
    public AudioSource shootingChannel3;
    public AudioClip Ak47Shot;
    public AudioClip SniperShot;
    public AudioClip[] hitBody;
    public AudioClip[] hitGlass;
    public AudioClip[] hitMetal;
    public AudioClip[] hitImpact;
    public AudioClip[] hitWood;
    public AudioClip[] bulletCasing;



    [Header("Âm thanh Reload")]
    public AudioSource reloadSoundAK48;
    public AudioSource reloadSniper;

    [Header("Âm thanh rỗng")]
    public AudioSource emtyshooting;

    [Header("Âm thanh Đèn pin")]
    public AudioSource flashLightChannel;
    public AudioClip flashLight;

    [Header("Âm thanh ném")]
    public AudioSource throwablesChannel;
    public AudioClip grenadeSound;

    [Header("Âm thanh Zombie")]
    public AudioSource zombieChannel;
    public AudioSource zombieChannel2;
    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;

    [Header("Âm thanh người chơi")]
    public AudioSource playerChannel;
    public AudioClip playerPickUpWP;
    public AudioClip playerPickUpIT;
    public AudioClip playerHurt;
    public AudioClip playerDeath;
    public AudioClip deathMusic;

    [Header("Âm thanh xe")]
    public AudioSource carChannel;
    public AudioClip carBegin;
    public AudioClip carHorn;
    public AudioSource carEngine;
    public AudioSource carBreake;
    public AudioSource carEngineFast;
    public AudioSource carAlarm;
    public AudioSource carExplosion;

    [Header("Âm thanh cửa")]
    public AudioSource doorChannel;
    public AudioClip openDoor;
    public AudioClip closeDoor;


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
        switch (weapon)
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


    public void PlayRandomHitSound(string targetType)
    {
        AudioClip[] selectedSounds = null;

        switch (targetType)
        {
            case "Body":
                selectedSounds = hitBody;
                break;
            case "Glass":
                selectedSounds = hitGlass;
                break;
            case "Metal":
                selectedSounds = hitMetal;
                break;
            case "Impact":
                selectedSounds = hitImpact;
                break;
            case "Wood":
                selectedSounds = hitWood;
                break;
            default:
                Debug.LogWarning("Unknown target type: " + targetType);
                return;
        }

        if (selectedSounds != null && selectedSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, selectedSounds.Length);
            shootingChannel2.PlayOneShot(selectedSounds[randomIndex]);
        }
    }

    public void PlayBulletCasingSFX()
    {
        if (!isPlayingSound && bulletCasing.Length > 0)
        {
            int randomIndex = Random.Range(0, bulletCasing.Length);
            AudioClip randomClip = bulletCasing[randomIndex];
            shootingChannel3.PlayOneShot(randomClip);
            isPlayingSound = true;
            Invoke("ResetIsPlayingSound", randomClip.length);
        }
    }

    private void ResetIsPlayingSound()
    {
        isPlayingSound = false;
    }
}
