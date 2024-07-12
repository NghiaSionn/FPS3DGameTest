using UnityEngine;

public class PartColl : MonoBehaviour
{
    private float lastSoundPlayTime;
    public float soundCooldown = 0.1f; // Thời gian chờ giữa các lần phát âm thanh

    private void OnParticleCollision(GameObject other)
    {
        // Kiểm tra thời gian kể từ lần phát âm thanh cuối cùng
        if (Time.time - lastSoundPlayTime >= soundCooldown && !SoundManager.Instance.shootingChannel3.isPlaying)
        {
            SoundManager.Instance.PlayBulletCasingSFX();
            lastSoundPlayTime = Time.time; 
        }
    }
}
