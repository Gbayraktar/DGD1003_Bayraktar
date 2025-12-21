using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    // Unity'den sürükleyip býrakacaðýn Panel
    [SerializeField] private GameObject endGamePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpan obje "Player" etiketine sahipse
        if (collision.CompareTag("Player"))
        {
            // 1. Paneli aç
            endGamePanel.SetActive(true);

            // 2. Oyunu durdur (Arka planda karakter hareket etmeye devam etmesin)
            Time.timeScale = 0f;
        }
    }
}