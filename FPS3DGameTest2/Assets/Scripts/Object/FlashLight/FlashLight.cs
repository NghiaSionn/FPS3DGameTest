using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject Light;
    public bool isLighting;

    // Start is called before the first frame update
    void Start()
    {
        Light.SetActive(false);
        isLighting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Instance.flashLightChannel.PlayOneShot(SoundManager.Instance.flashLight);
            isLighting = !isLighting; // Chuyển đổi trạng thái của đèn pin
            Light.SetActive(isLighting); // Bật hoặc tắt đèn dựa trên trạng thái
            Debug.Log(isLighting ? "Bật đèn" : "Tắt đèn");
        }
    }
}
