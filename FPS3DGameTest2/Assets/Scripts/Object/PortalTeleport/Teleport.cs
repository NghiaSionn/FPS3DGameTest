using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections;


public class Teleport : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Da vao cong tele ");
            LoadSceneWithDelay();
        }
    }

    public void LoadSceneWithDelay()
    {
        StartCoroutine(LoadSceneAfterDelay("Scene_A"));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }
}
