using UnityEngine;
using System.Collections; // Coroutine için gerekli

public class PlayerAttacksc : MonoBehaviour
{
    [Header("Saldýrý Ayarlarý")]
    public GameObject bulletPrefab;
    public float attackRange = 3f;
    public LayerMask enemyLayers;

    [Header("Hýz Dengesi")]
    public float fireRate = 0.6f;      // Baþlangýç hýzý (Saniye)
    public float minFireRate = 0.1f;   // Limit: Bundan daha hýzlý olamaz!

    [Header("Görsel Ayar")]
    public Transform visualAura;

    private float nextFireTime = 0f;

    void Update()
    {
        // Ateþ etme zamaný geldiyse
        if (Time.time >= nextFireTime)
        {
            Transform targetEnemy = FindClosestEnemy();

            if (targetEnemy != null)
            {
                Shoot(targetEnemy);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    // --- KALICI GÜÇLENME FONKSÝYONU ---
    // Bu fonksiyon item tarafýndan çaðrýlacak
    public void PermanentSpeedUpgrade(float amount)
    {
        fireRate -= amount;
        if (fireRate < minFireRate) fireRate = minFireRate;
        Debug.Log("SALDIRI HIZI ARTTI!");
    }
    public void IncreaseRange(float amount)
    {
        // 1. Mantýksal menzili artýr (Düþmanlarý daha uzaktan gör)
        attackRange += amount;

        // 2. Görsel halkayý büyüt (Opsiyonel ama çok þýk durur)
        if (visualAura != null)
        {
            // Halkanýn boyutuna ekleme yap
            visualAura.localScale += new Vector3(amount, amount, 0);
        }

        Debug.Log($"MENZÝL ARTTI! Yeni Menzil: {attackRange}");
    }


    // ... (FindClosestEnemy, Shoot ve Gizmos kodlarý ayný kalacak) ...
    // Kopyala-Yapýþtýr kolay olsun diye onlarý tekrar aþaðýya yazýyorum:

    Transform FindClosestEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D enemy in hitEnemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab == null) return;
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(bulletPrefab, transform.position, rotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
