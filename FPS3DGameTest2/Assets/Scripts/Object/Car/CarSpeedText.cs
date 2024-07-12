using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSpeedText : MonoBehaviour
{
    public TextMeshProUGUI carSpeedText;
    public Rigidbody carRigidbody;
    public AudioSource carEngineAudio;


    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;
    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;


    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = carRigidbody.velocity.magnitude * 2.23693629f;

        carSpeedText.text = speed.ToString("0");

        EngineSound();

    }

    void EngineSound()
    {
        currentSpeed = carRigidbody.velocity.magnitude;
        pitchFromCar = carRigidbody.velocity.magnitude / 50f;

        if (currentSpeed < minSpeed)
        {
            carEngineAudio.pitch = minPitch;
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carEngineAudio.pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            carEngineAudio.pitch = maxPitch;
        }

        
    }
}
