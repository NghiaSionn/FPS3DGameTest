using UnityEngine;

public class RecoilController : MonoBehaviour
{
    public static RecoilController Instance { get; private set; }

    public float positionRecoilAmount = 0.1f; // Độ mạnh của recoil vị trí
    public float rotationRecoilAmount = 2f; // Độ mạnh của recoil rotation
    public float recoilSpeed = 10f; // Tốc độ áp dụng recoil
    public float recoverySpeed = 5f; // Tốc độ hồi phục về trạng thái bình thường

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 currentRecoilPosition = Vector3.zero;
    private Vector3 currentRecoilRotation = Vector3.zero;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        // Giảm dần giá trị recoil về 0
        currentRecoilPosition = Vector3.Lerp(currentRecoilPosition, Vector3.zero, recoverySpeed * Time.deltaTime);
        currentRecoilRotation = Vector3.Lerp(currentRecoilRotation, Vector3.zero, recoverySpeed * Time.deltaTime);

        // Áp dụng giá trị recoil hiện tại vào position và rotation của camera
        transform.localPosition = originalPosition + currentRecoilPosition;
        transform.localRotation = originalRotation * Quaternion.Euler(currentRecoilRotation);
    }

    public void ApplyRecoil(float recoilY, float recoilRotationX)
    {
        currentRecoilPosition += new Vector3(0, recoilY, 0);
        currentRecoilRotation += new Vector3(recoilRotationX, 0, 0);
    }
}
