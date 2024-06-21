using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isActiveWeapon;


    public bool isShooting, readyToShoot;
    bool allowRest = true;
    public float shootingDelay = 2f;

    [Header("Thông số của đạn")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;
    public float spreadIntensity;

    public GameObject muzzleEffect;
    internal Animator animator;

    [Header("Thay đạn")]
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public bool isScoped = false;


    public CameraChange cameraChange;


    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public enum WeaponModel
    {
        AK47,
        Sniper
    }


    public WeaponModel thisWeaponModel;


    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    //[Header("Súng")]
    //public WeaponModel model;

    [Header("Chế độ bắn")]
    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;

      
        if (cameraChange == null)
        {
            cameraChange = FindObjectOfType<CameraChange>();
        }
    }

    void Update()
    {
        if (isActiveWeapon)
        {
            GetComponent<Outline>().enabled = false;


            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.Instance.emtyshooting.Play();
            }


            if (currentShootingMode == ShootingMode.Auto)
            {
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }


            else if (currentShootingMode == ShootingMode.Single)
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }


            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading && WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > 0)
            {
                Debug.Log("Thay Đạn");
                Reload();
            }


            // ngắm
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isScoped = !isScoped;
                if (Camera.main.fieldOfView == 30f)
                {
                    if (cameraChange.camMode == 1)
                    {
                        Camera.main.fieldOfView = 60f;
                        animator.SetBool("STARE", isScoped);
                    }
                }


                else if (Camera.main.fieldOfView == 40f)
                {
                    if (cameraChange.camMode == 0)
                    {
                        Camera.main.fieldOfView = 60f;
                    }
                }


                else
                {
                    if (cameraChange.camMode == 1)
                    {
                        Camera.main.fieldOfView = 30f;
                        animator.SetBool("STARE", isScoped);
                    }
                    else
                    {
                        Camera.main.fieldOfView = 40f;
                    }
                }

            }

            // Nạp đạn tự động
            if (readyToShoot && !isShooting && !isReloading && bulletsLeft <= 0)
            {
                // Reload();
            }

            if (readyToShoot && isShooting && bulletsLeft > 0 && !isReloading)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }

            //if (AmmoManager.Instance.ammoDisplay != null)
            //{
            //    AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst} / {magazineSize / bulletsPerBurst}";
            //} 
        }
    }


    private void FireWeapon()
    {
        if (isReloading) return; // Không bắn khi đang nạp đạn

        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        //SoundManager.Instance.shootingSoundAK47.Play();


        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread();

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(shootingDirection));
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        bullet.AddComponent<Bullet>();
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        if (allowRest)
        {
            Invoke("ResetShot", shootingDelay);
            allowRest = false;
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
        animator.SetTrigger("RELOAD");
        //SoundManager.Instance.reloadSoundAK48.Play();


        SoundManager.Instance.PlayReloadSound(thisWeaponModel);
    }

    private void ReloadCompleted()
    {
        if (WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > magazineSize)
        {
            bulletsLeft = magazineSize;
            WeaponManager.Instance.DescreaseTotalAmmo(bulletsLeft, thisWeaponModel);
        }
        else
        {
            bulletsLeft = WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel);
            WeaponManager.Instance.DescreaseTotalAmmo(bulletsLeft, thisWeaponModel);
        }


        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowRest = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        direction.x += UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        direction.y += UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction.normalized;
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet, 10f);
        
    }
}
