using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public string sceneToLoad = "Scene_A";
    public float teleportTime = 3f;
    private bool isTeleporting = false;
    private Coroutine teleportCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal");
            if (teleportCoroutine == null)
            {
                teleportCoroutine = StartCoroutine(TeleportAfterDelay(other.gameObject));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the portal");
            if (teleportCoroutine != null)
            {
                StopCoroutine(teleportCoroutine);
                teleportCoroutine = null;
                ResetPlayerCameraEffect(other.gameObject);
            }
        }
    }

    private IEnumerator TeleportAfterDelay(GameObject player)
    {
        float elapsedTime = 0f;

        while (elapsedTime < teleportTime)
        {
            elapsedTime += Time.deltaTime;
            ApplyCameraEffect(player, elapsedTime / teleportTime);
            yield return null;
        }

        Debug.Log("Teleporting player");
        SceneManager.LoadScene(sceneToLoad);
    }

    private void ApplyCameraEffect(GameObject player, float progress)
    {
        // Thêm mã để xoay camera hoặc hiệu ứng say rượu ở đây
        // Ví dụ:
        Camera playerCamera = player.GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(
                Mathf.Sin(Time.time * 10f) * 10f * progress,
                Mathf.Cos(Time.time * 10f) * 10f * progress,
                0f
            );
        }
    }

    private void ResetPlayerCameraEffect(GameObject player)
    {
        Camera playerCamera = player.GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.identity;
        }
    }
}
