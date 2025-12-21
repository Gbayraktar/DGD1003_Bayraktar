using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için bu kütüphane þart

public class LevelManager : MonoBehaviour
{
    public void LoadNextLevel()
    {
        // Þu anki sahnenin index numarasýný al (Örn: Level 1 -> Index 1)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Bir sonraki sahnenin indexini hesapla
        int nextSceneIndex = currentSceneIndex + 1;

        // Eðer bir sonraki sahne Build Settings listesinde varsa onu yükle
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Eðer sahne kalmadýysa (Oyun bittiyse), Ana Menüye (Index 0) dön
            Debug.Log("Son sahneye gelindi, baþa dönülüyor.");
            SceneManager.LoadScene(0);
        }
    }

    // Ýstersen sadece sahne ismine göre yükleyen bir fonksiyon da ekleyebilirsin
    public void LoadLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}