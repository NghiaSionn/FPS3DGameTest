using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Vector3 playerVelocity;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot;

    //[SerializeField] private Transform playerCamera;
    [SerializeField] private CharacterController characterController;


    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    //[SerializeField] private float sensitivity;
    [SerializeField] private float gravity = -9.81f;

    [Space]
    [Header("Game Object")]
    [SerializeField] private GameObject crosshair;

    [Space]
    public static float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Lock mouse
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }

        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        //MoveCamera();

        // Update Score Text
        // scoreText.text = "Score: " + score.ToString();

        // Zoom - Feature to Code!!!
        if (Input.GetMouseButtonDown(1))
        {
            if (Camera.main.fieldOfView == 20f)
            {
                Camera.main.fieldOfView = 60f;
            }
            else
            {
                Camera.main.fieldOfView = 20f;
            }
        }
    }

    public void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput).normalized;

        if (characterController.isGrounded)
        {
            playerVelocity.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerVelocity.y = jumpForce;
            }
        }
        else
        {
            playerVelocity.y -= gravity * -2f * Time.deltaTime;
        }

        characterController.Move(MoveVector * speed * Time.deltaTime);
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    //public void MoveCamera()
    //{
    //    xRot -= playerMouseInput.y * sensitivity;
    //    xRot = Mathf.Clamp(xRot, -90f, 90f);

    //    transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);

    //    playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    //}
}
