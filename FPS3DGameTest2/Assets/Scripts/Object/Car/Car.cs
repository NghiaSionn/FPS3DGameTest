using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    public bool isDead;
    private Rigidbody carRigidbody;

    private Vector3 lastHitDirection; // Biến tạm để lưu hướng của lực tác động

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            isDead = true;
            // Âm thanh chết
            SoundManager.Instance.carExplosion.Play();
            SoundManager.Instance.carAlarm.Stop();

            Destroy(gameObject); // Phá hủy xe sau 2 giây để âm thanh phát xong
        }
        else
        {
            // Âm thanh khi nhận sát thương
            if (!SoundManager.Instance.carAlarm.isPlaying)
            {
                SoundManager.Instance.carAlarm.Play();
            }

            // Áp dụng một lực nhỏ để xe bị nhích nhẹ
            carRigidbody.AddForce(-lastHitDirection * 5f, ForceMode.Impulse);
        }
    }


    public void SetHitDirection(Vector3 direction)
    {
        lastHitDirection = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                TakeDamage(other.GetComponent<ZombieHand>().damage);
            }

        }
    }
}
