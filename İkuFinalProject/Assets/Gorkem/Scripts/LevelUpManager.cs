using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [Header("UI Baðlantýsý")]
    public GameObject levelUpPanel; // Panel objesi

    [Header("Player Baðlantýlarý")]
    public PlayerHealth playerHealth;     // Can iþlemleri için
    public PlayerMovement playerMovement; // Yürüme hýzý için
    public PlayerAttacksc playerAttack;     // Saldýrý hýzý ve menzil için

    // Level atlayýnca bu çalýþýr
    public void ShowLevelUpOptions()
    {
        levelUpPanel.SetActive(true); // Paneli aç
        Time.timeScale = 0f;          // Oyunu dondur
    }

    // --- BUTON 1: MAX CAN ARTIRMA ---
    public void UpgradeHealth()
    {
        if (playerHealth != null)
        {
            playerHealth.IncreaseMaxHealth(20); // 20 Can ekle
        }
        ClosePanel();
    }

    // --- BUTON 2: YÜRÜME HIZI ARTIRMA ---
    public void UpgradeSpeed()
    {
        if (playerMovement != null)
        {
            playerMovement.IncreaseMoveSpeed(1f); // Hýzý 1 birim artýr
        }
        ClosePanel();
    }

    // --- BUTON 3: SALDIRI HIZI (MERMÝ SERÝLÝÐÝ) ---
    public void UpgradeFireRate()
    {
        if (playerAttack != null)
        {
            // Bekleme süresinden 0.05 saniye düþ
            playerAttack.PermanentSpeedUpgrade(0.05f);
        }
        ClosePanel();
    }

    // --- BUTON 4: MENZÝL GENÝÞLETME (CIRCLE) ---
    public void UpgradeRange()
    {
        if (playerAttack != null)
        {
            // Menzili 1 birim artýr
            playerAttack.IncreaseRange(1f);
        }
        ClosePanel();
    }

    // Paneli kapatma ve oyunu devam ettirme
    void ClosePanel()
    {
        levelUpPanel.SetActive(false); // Paneli gizle
        Time.timeScale = 1f;           // Oyunu devam ettir
    }
}