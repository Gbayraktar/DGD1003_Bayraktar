using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        // Oyun baþladýðýnda kamera nerede duruyorsa, hedef orasý olsun.
        currentPosX = transform.position.x;
    }

    private void Update()
    {
        // Kamerayý yumuþak bir þekilde hedefe taþý
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(currentPosX, transform.position.y, transform.position.z),
            ref velocity, speed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        // Yeni odanýn X pozisyonunu hedef olarak belirle
        currentPosX = _newRoom.position.x;
    }
}