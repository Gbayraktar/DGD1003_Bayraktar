using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [Header("Seviye Bilgisi")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100; // Ýlk seviye atlamak için gereken XP

    // Bu fonksiyonu XP topu çaðýracak
    public void GainExperience(int amount)
    {
        currentXP += amount;
        Debug.Log($"XP KAZANILDI! (+{amount}) | Toplam XP: {currentXP}/{xpToNextLevel}");

        // Level atlama kontrolü
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;

        // Artan XP'yi bir sonraki levele devret (Örn: 110 XP varsa, 10 XP ile baþla)
        currentXP -= xpToNextLevel;

        // Bir sonraki level zorlaþsýn (Her levelde gereken XP %20 artsýn)
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f);

        Debug.Log("--------------------------------");
        Debug.Log($"TEBRÝKLER! LEVEL ATLADIN: {currentLevel}");
        Debug.Log($"Sonraki Level Ýçin Gereken: {xpToNextLevel}");
        Debug.Log("--------------------------------");

        // Buraya ileride "Hasar Artýrma", "Can Fulleme" gibi ödüller ekleyebiliriz.
    }
}
