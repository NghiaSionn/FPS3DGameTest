using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpstext;
    private float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        UpdateFPSDisplay();
    }

    void UpdateFPSDisplay()
    {
        if (fpstext != null)
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            fpstext.text = text;
        }
    }
}
