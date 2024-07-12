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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Bật đèn");
            isLighting = true;
            Light.SetActive(true);

        }

        if(isLighting)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                Debug.Log("Tắt đèn");
                isLighting = false;
                Light.SetActive(false);
            }
        }
        
    }
}
