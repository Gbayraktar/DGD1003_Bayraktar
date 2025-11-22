using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Can Ayarlarý")]
    public int maxHealth = 100;  // Baþlangýç caný
    private int currentHealth;   // O anki can

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("OYUN BAÞLADI: Player Caný = " + currentHealth);
    }

    // Bu fonksiyonu düþman çaðýracak
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log($"HASAR ALINDI! Gelen Hasar: {damage} | Kalan Can: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void HealFull()
    {
        // Caný maksimum deðere eþitle
        currentHealth = maxHealth;

        Debug.Log("ÝKSÝR ALINDI! Can Fullendi. Þu anki Can: " + currentHealth);
    }

    void Die()
    {
        Debug.Log("PLAYER ÖLDÜ! (Can <= 0)");

        // Þimdilik sadece objeyi yok ediyoruz
        Destroy(gameObject);
    }
}
