using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Bileşenler")]
    private Animator anim;
    private Rigidbody2D rb;
    private Transform player;

    [Header("Hareket & Fizik")]
    public float speed = 3f;           // Yürüme hızı
    public float followRange = 100f;   // Takip mesafesi (Büyük yaptık ki hep kovalasın)
    public float knockbackForce = 5f;  // Geri tepme gücü
    public float stunTime = 0.3f;      // Sersemleme süresi

    [Header("Can & Saldırı")]
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;            // Player'a verdiği hasar

    [Header("Görsel & Ses Efektleri")]
    public GameObject deathEffectPrefab; // Ölünce çıkan partikül (Kan/Patlama)
    public AudioClip deathSound;         // Ölünce çıkan ses

    [Header("Ödüller")]
    public GameObject xpPrefab;        // XP Topu
    public int scoreValue = 100;       // Öldürünce kaç puan?

    [Header("Boss Ayarları")]
    public bool isBoss = false;        // Eğer bu Boss ise işaretle!

    // Kontrol Değişkenleri
    private bool isKnockedBack = false;
    private Vector3 defaultScale;      // Boss'un orijinal boyutunu saklamak için

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // Başlangıç boyutunu kaydet (Boss büyütülmüşse bozulmasın diye)
        defaultScale = transform.localScale;

        // Player'ı bul
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // Player yoksa veya sersemlemişsek hareket etme
        if (player == null || isKnockedBack) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < followRange)
        {
            // HAREKET: Player'a doğru git
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Animasyon: Yürüme
            if (anim != null) anim.SetFloat("Speed", 1f);

            // YÖN DÖNME (Flip) - Boss boyutunu koruyarak
            if (player.position.x > transform.position.x)
            {
                // Sağa bak (Orijinal pozitif boyut)
                transform.localScale = new Vector3(Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
            }
            else
            {
                // Sola bak (Negatif boyut)
                transform.localScale = new Vector3(-Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
            }
        }
    }

    // --- HASAR ALMA ---
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (anim != null) anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // --- ÇARPIŞMA (Player'a Vurma) ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 1. Player'a Hasar Ver
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(damage);

            // 2. Kendini Geri İttir
            StartCoroutine(KnockbackRoutine(collision.transform));
        }
    }

    // Geri Tepme Coroutine'i
    IEnumerator KnockbackRoutine(Transform playerTransform)
    {
        isKnockedBack = true;

        // Player'dan uzağa doğru yön hesapla
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.linearVelocity = Vector2.zero; // Unity 6 için rb.linearVelocity olabilir
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(stunTime);

        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }

    // --- ÖLÜM ---
    void Die()
    {
        // 1. SES EFEKTİ
        if (deathSound != null)
        {
            // PlayClipAtPoint, obje yok olsa bile sesi çalar
            AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);
        }

        // 2. PARTİKÜL EFEKTİ (Particle System)
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            // Efekt prefabında "Stop Action: Destroy" ayarlı olmalı!
        }

        // 3. BOSS ÖZEL DURUMU (ZAFER)
        if (isBoss)
        {
            Debug.Log("BOSS YENİLDİ! OYUN BİTTİ.");

            // Sayacı durdur
            SurvivalTimer timer = FindObjectOfType<SurvivalTimer>();
            if (timer != null) timer.StopTimer();

            // Game Over (Win) Panelini aç
            GameoverManager gm = FindObjectOfType<GameoverManager>();
            if (gm != null) gm.ShowGameOver();
        }

        // 4. ÖDÜLLER (Puan, XP, Loot)
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue);
            ScoreManager.instance.AddKill();
        }

        if (xpPrefab != null)
        {
            Instantiate(xpPrefab, transform.position, Quaternion.identity);
        }

        LootBag lootBag = GetComponent<LootBag>();
        if (lootBag != null) lootBag.DropLoot();

        // 5. YOK OL
        Destroy(gameObject);
    }
}