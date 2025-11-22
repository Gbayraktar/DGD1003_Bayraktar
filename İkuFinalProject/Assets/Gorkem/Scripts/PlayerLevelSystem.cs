using TMPro;
using UnityEngine;
using UnityEngine.UI; // Slider için gerekli

public class PlayerLevelSystem : MonoBehaviour
{
    [Header("UI Baðlantýlarý")]
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

    [Header("Seviye Ayarlarý")]
    public int currentLevel = 1;
    public float currentXP = 0;

    [Header("Zorluk Ayarý")]
    public float xpToNextLevel = 10;   // Ýlk seviye için gereken (Örn: 10)
    public float difficultyIncrease = 5; // Her levelde kaç artacak? (Örn: +5)

    void Start()
    {
        UpdateUI();
    }

    public void GainExperience(int amount)
    {
        currentXP += amount;

        // Level atlama kontrolü
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    void LevelUp()
    {
        currentLevel++;

        // 1. Artan XP'yi hesapla (Taþan XP kaybolmasýn)
        // Örn: 12 XP topladýn, hedef 10 ise -> 2 XP cepte kalsýn.
        float overflowXP = currentXP - xpToNextLevel;
        currentXP = overflowXP;

        // 2. ZORLAÞTIRMA KISMI (Senin istediðin yer)
        // Hedefi artýrýyoruz. (10 -> 15 -> 20 -> 25...)
        xpToNextLevel += difficultyIncrease;

        Debug.Log($"LEVEL ATLADIN! Yeni Level: {currentLevel}");
        Debug.Log($"Sýradaki Level Ýçin Gereken: {xpToNextLevel}");

        // Eðer taþan XP, yeni hedefi de geçiyorsa tekrar level atlat (Nadir durum)
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
            xpSlider.value = currentXP;
        }

        if (levelText != null)
        {
            levelText.text = "Level " + currentLevel.ToString();
        }
    }
}


