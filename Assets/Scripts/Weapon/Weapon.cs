using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class Weapon : MonoBehaviour
{
    public bool isActiveWeapon;
    public int weaponDamage;


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
    public float hipSpreadIntensity;
    public float adsSpreadIntensity;
    // Hệ số nhân của độ giật
    public float recoilMultiplier = 1f;


    public GameObject muzzleEffect;
    internal Animator animator;


    [Header("Thay đạn")]
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;


    [Header("Scope")]
    public float scopedFOV = 15f;
    private float normalFOV;
    public bool isADS;


    public Camera mainCamera;
    public GameObject scopeOverlay;
    //public ProceduralRecoil recoil;
    public RecoilController recoilController;
    public ParticleSystem shellParticle;
 


    [Header("Thay đổi góc nhìn")]
    public CameraChange cameraChange;


    [Header("Spawn trên tay")]
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

    [Header("Chế độ bắn")]
    public ShootingMode currentShootingMode;


    private List<MeshRenderer> weaponMeshRenderers;


    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();
        recoilController = Camera.main.GetComponent<RecoilController>();



        bulletsLeft = magazineSize;
        spreadIntensity = hipSpreadIntensity;
        scopeOverlay.SetActive(false);


        if (cameraChange == null)
        {
            cameraChange = FindObjectOfType<CameraChange>();
        }


        //recoil = mainCamera.GetComponent<ProceduralRecoil>();


        // Collect all mesh renderers of the weapon
        weaponMeshRenderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
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


            // Reload
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading && WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > 0)
            {
                Debug.Log("Thay Đạn");
                Reload();
            }


            // ngắm
            if (Input.GetMouseButtonDown(1))
            {
                switch (thisWeaponModel)
                {
                    case WeaponModel.AK47:
                        EnterADS();
                        break;
                    case WeaponModel.Sniper:
                        StartCoroutine(OnScoped());
                        break;

                    default:
                        break;
                }
            }


            // bỏ ngắm
            if (Input.GetMouseButtonUp(1))
            {
                switch (thisWeaponModel)
                {
                    case WeaponModel.AK47:
                        ExitADS();
                        break;
                    case WeaponModel.Sniper:
                        OnUnscoped();
                        break;
                    default:
                        break;
                }
            }


            // Auto Reload
            if (readyToShoot && !isShooting && !isReloading && bulletsLeft <= 0)
            {
                // Reload();
            }


            if (readyToShoot && isShooting && bulletsLeft > 0 && !isReloading)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }
        }
    }

    private void FireWeapon()
    {
        if (isReloading) return; // Không bắn khi đang nạp đạn


        bulletsLeft--;

        shellParticle.Emit(1);
        

        muzzleEffect.GetComponent<ParticleSystem>().Play();


        if (isADS)
        {
            animator.SetTrigger("RECOIL_ADS");
        }
        else
        {
            animator.SetTrigger("RECOIL");
        }


        SoundManager.Instance.PlayShootingSound(thisWeaponModel);


        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread();


        // Spawn đạn
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(shootingDirection));

        // áp dụng recoil khi bắn 
        float recoilY = Random.Range(0.05f, 0.1f) * recoilMultiplier * RecoilController.Instance.positionRecoilAmount;
        float recoilRotationX = Random.Range(-1f, -2f) * recoilMultiplier * RecoilController.Instance.rotationRecoilAmount;
        RecoilController.Instance.ApplyRecoil(recoilY, recoilRotationX);

       

        // gây damage 
        Bullet bul = bullet.GetComponent<Bullet>();
        bul.bulletDamage = weaponDamage;


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





    private void EnterADS()
    {
        animator.SetTrigger("enterADS");
        isADS = true;
        HUDManager.Instance.middleDot.SetActive(false);
        spreadIntensity = adsSpreadIntensity;
    }


    private void ExitADS()
    {
        animator.SetTrigger("exitADS");
        isADS = false;
        HUDManager.Instance.middleDot.SetActive(true);
        spreadIntensity = hipSpreadIntensity;
    }


    private void OnUnscoped()
    {
        animator.SetTrigger("exitADS");
        isADS = false;
        HUDManager.Instance.middleDot.SetActive(true);
        scopeOverlay.SetActive(false);
        SetWeaponMeshRenderersActive(true);
        spreadIntensity = hipSpreadIntensity;

        mainCamera.fieldOfView = normalFOV;
    }


    IEnumerator OnScoped()
    {
        Debug.Log("Scoped dang bat");


        animator.SetTrigger("enterADS");
        isADS = true;
        HUDManager.Instance.middleDot.SetActive(false);
        scopeOverlay.SetActive(true);
        SetWeaponMeshRenderersActive(false);
        spreadIntensity = adsSpreadIntensity;

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;

        Debug.Log($"FOV dc dat thanh {mainCamera.fieldOfView}");
        yield return null;
    }

    private void SetWeaponMeshRenderersActive(bool isActive)
    {
        foreach (var renderer in weaponMeshRenderers)
        {
            renderer.enabled = isActive;
        }
    }


    private void Reload()
    {
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
        if (isADS)
        {
            animator.SetTrigger("RELOAD_ADS");
        }
        else
        {
            animator.SetTrigger("RELOAD");
        }


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

        // Thêm độ lệch vào hướng bắn
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