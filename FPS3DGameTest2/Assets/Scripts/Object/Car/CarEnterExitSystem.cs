using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterExitSystem : MonoBehaviour
{
    public MonoBehaviour CarController;
    public MonoBehaviour Interaction;


    public Transform Car;
    public Transform Player;


    public GameObject PlayerCam;
    public GameObject CarCam;

    public GameObject DriveUi;
    public GameObject playerPosition;
    public GameObject middlePoint;
    public GameObject ammo;
    public GameObject speedCarText;
    

    public WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    bool Candrive;
    bool isDriving;

    // Start is called before the first frame update
    void Start()
    {
        CarController.enabled = false;
        Interaction.enabled = true;
        

        DriveUi.gameObject.SetActive(false);
        isDriving = false;
        speedCarText.gameObject.SetActive(false);
        SoundManager.Instance.carEngineFast.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Weapon activeWeapon = WeaponManager.Instance?.activeWeaponSlot?.GetComponentInChildren<Weapon>();

        // Prevent driving if the player is aiming
        if (activeWeapon != null && activeWeapon.isADS)
        {
            DriveUi.gameObject.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Candrive && !isDriving)  // người chơi có thể lái và chưa ở trong xe
            {
                EnterCar();
            }
            else if (isDriving) // Nếu người chơi đang ở trong xe
            {
                ExitCar();
            }
        }
    }

    void EnterCar()
    {
        SoundManager.Instance.carChannel.PlayOneShot(SoundManager.Instance.carBegin);
        SoundManager.Instance.carAlarm.Stop();
        SoundManager.Instance.carEngineFast.Play();

        Interaction.enabled = false;
        CarController.enabled = true;
        isDriving = true;

        DriveUi.gameObject.SetActive(false);
        playerPosition.gameObject.SetActive(false);
        middlePoint.gameObject.SetActive(false);
        ammo.gameObject.SetActive(false);
        speedCarText.gameObject.SetActive(true);

        // Gắn người chơi vào xe
        Player.transform.SetParent(Car);
        Player.gameObject.SetActive(false);

        // Chuyển đổi camera
        PlayerCam.gameObject.SetActive(false);
        CarCam.gameObject.SetActive(true);
    }

    void ExitCar()
    {
        Interaction.enabled = true;
        SoundManager.Instance.carEngineFast.Stop();


        CarController.enabled = false;
        isDriving = false;

        playerPosition.gameObject.SetActive(true);
        middlePoint.gameObject.SetActive(true);
        ammo.gameObject.SetActive(true);

        // Tách người chơi khỏi xe
        Player.transform.SetParent(null);
        Player.gameObject.SetActive(true);

        // Chuyển đổi camera
        PlayerCam.gameObject.SetActive(true);
        CarCam.gameObject.SetActive(false);

        // Dừng xe lại bằng cách áp dụng lực phanh tối đa
        ApplyBrakes();
    }

    void ApplyBrakes()
    {
        float maxBrakeTorque = 10000000000f; // Đặt giá trị lực phanh tối đa

        frontRightWheelCollider.brakeTorque = maxBrakeTorque;
        frontLeftWheelCollider.brakeTorque = maxBrakeTorque;
        rearLeftWheelCollider.brakeTorque = maxBrakeTorque;
        rearRightWheelCollider.brakeTorque = maxBrakeTorque;
    }

    void OnTriggerStay(Collider col)
    {
        Weapon activeWeapon = WeaponManager.Instance?.activeWeaponSlot?.GetComponentInChildren<Weapon>();

        if (col.gameObject.tag == "Player" && (activeWeapon == null || !activeWeapon.isADS))
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
