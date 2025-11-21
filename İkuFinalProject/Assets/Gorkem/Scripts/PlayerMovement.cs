using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("H�z Ayarlar�")]
    public float moveSpeed = 5f; // Karakterin y�r�me h�z�

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isKnockedBack = false; // Geri tepme kontrol�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // E�er darbe ald�ysak (isKnockedBack = true), tu�lar� dinleme
        if (isKnockedBack == true) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // E�er darbe ald�ysak hareket kodunu �al��t�rma, fizi�e b�rak
        if (isKnockedBack == true) return;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // --- ��TE EKS�K OLAN KISIM BURASI ---
    // D��man scripti bu fonksiyonu ar�yor
    public void CallKnockback(float duration, float force, Transform enemyTransform)
    {
        StartCoroutine(KnockbackRoutine(duration, force, enemyTransform));
    }

    IEnumerator KnockbackRoutine(float duration, float force, Transform enemyTransform)
    {
        isKnockedBack = true; // Kontrol� kapat
        rb.linearVelocity = Vector2.zero; // H�z� s�f�rla

        // D��mandan z�t y�ne do�ru f�rlat
        Vector2 direction = (transform.position - enemyTransform.position).normalized;
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration); // Bekle

        rb.linearVelocity = Vector2.zero; // Durdur
        isKnockedBack = false; // Kontrol� geri ver
    }
}

