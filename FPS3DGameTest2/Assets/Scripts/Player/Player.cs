using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;
    public Animator cameraAnim;


    public TextMeshProUGUI playerHeathUI;
    public GameObject gameOverUI;
    public GameObject revivalButton;


    public bool isDead;


    private void Start()
    {
        playerHeathUI.text = $"Health: {HP}"; 
    }


    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("Player Dead");           
            PlayerDead();
            isDead = true;
            //Destroy(gameObject,4f);
        }
        else
        {
            //Bị đánh
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);


            print("Player Hit");
            StartCoroutine(BloodyScreenEffect());
            playerHeathUI.text = $"Health: {HP}";            
        }
    }


    private void PlayerDead()
    {
        Cursor.lockState = CursorLockMode.None;

        // âm thanh player chết 
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDeath);
      

        GetComponent<PlayerMovement3>().enabled = false;
        GetComponent<MouseMovement>().enabled = false;
        GetComponent<Lean>().enabled = false;


        cameraAnim.SetBool("Dead", true);
        playerHeathUI.gameObject.SetActive(false);


        GetComponent<ScreenBlackOut>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }


    private IEnumerator ShowGameOverUI()
    {      
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);
        revivalButton.gameObject.SetActive(true);


        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.deathMusic);
    }


    private IEnumerator BloodyScreenEffect()
    {
        if (!bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();


        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;


        float duration = 2f;
        float elapsedTime = 0f;


        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);


            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;


            elapsedTime += Time.deltaTime;


            yield return null; ;
        }


        yield return new WaitForSeconds(3f);


        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if(isDead == false)
            {
                TakeDamage(other.GetComponent<ZombieHand>().damage);
            }
            
        }
    }

   
}
