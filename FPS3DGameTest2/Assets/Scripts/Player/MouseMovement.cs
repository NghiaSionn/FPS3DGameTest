using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Camera cam;
    public float mouseSensitivity = 500f;
    float xRotation = 0f;
    float yRotation = 0f;


    public float topClamp = -30f;
    public float bottomClamp = 30f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);


        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
