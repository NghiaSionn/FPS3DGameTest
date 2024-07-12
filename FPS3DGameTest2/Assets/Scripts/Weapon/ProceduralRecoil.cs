using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRecoil : MonoBehaviour
{
    Vector3 currentRotaion, targetRotation, targetPosition, currentPosition, initialGunPosition;
    public Transform cam;


    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;


    [SerializeField] float kickBackZ;


    public float snappiness, returnAmount;


    void Start()
    {
        initialGunPosition = transform.localPosition; 
    }


    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnAmount);
        currentPosition = Vector3.Slerp(currentRotaion,targetRotation, Time.deltaTime * snappiness);
        transform.localRotation = Quaternion.Euler(currentRotaion);


        back();
    }


    public void recoil()
    {
        targetPosition -= new Vector3(0, 0, kickBackZ);
        targetRotation += new Vector3(recoilX, UnityEngine.Random.Range(-recoilY, recoilY), UnityEngine.Random.Range(-recoilZ, recoilZ));

    }


    void back()
    {
        targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * returnAmount);
        targetRotation = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * snappiness);
        transform.localPosition = currentPosition;
    }
}
