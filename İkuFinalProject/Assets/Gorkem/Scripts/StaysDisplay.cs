using UnityEngine;
using TMPro; // TextMeshPro kütüphanesi

public class StatsDisplay : MonoBehaviour
{
    [Header("Ekrandaki Yazýlar")]
    public TextMeshProUGUI hpText;       // Can yazýsý
    public TextMeshProUGUI speedText;    // Hýz yazýsý
    public TextMeshProUGUI fireRateText; // Atýþ hýzý yazýsý
    public TextMeshProUGUI rangeText;    // Menzil yazýsý

    [Header("Player Baðlantýlarý")]
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public PlayerAttacksc playerAttack;

    void Update()
    {
        // MAX CAN YAZISI
        if (playerHealth != null)
        {
            hpText.text = "Max Can: " + playerHealth.maxHealth.ToString();
        }

        // HIZ YAZISI ("F1" = Virgülden sonra tek basamak göster demek. Örn: 5.5)
        if (playerMovement != null)
        {
            speedText.text = "Hýz: " + playerMovement.moveSpeed.ToString("F1");
        }

        // MENZÝL VE ATIÞ HIZI
        if (playerAttack != null)
        {
            // Atýþ hýzý azaldýkça iyidir (bekleme süresi), ama oyuncu bunu anlamayabilir.
            // O yüzden yanýna 'sn' (saniye) yazýyoruz.
            fireRateText.text = "Atýþ Süresi: " + playerAttack.fireRate.ToString("F2") + " sn";

            rangeText.text = "Menzil: " + playerAttack.attackRange.ToString("F1");
        }
    }
}