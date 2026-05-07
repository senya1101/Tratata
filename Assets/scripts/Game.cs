using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    [Header("UI элементы")]
    public GameObject gameOverPanel; 

    private void Awake()
    {
        
        if (Instance == null) Instance = this;
    }

    
    public void EndGame()
    {
        Debug.Log("Игра окончена!");
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        
        if (spawner != null) // Если спавнер найден
        {
            int currentWave = spawner.currentWave; // Берем номер текущей волны

            int savedRecord = PlayerPrefs.GetInt("MaxWave", 0);

            if (currentWave > savedRecord)
            {
                PlayerPrefs.SetInt("MaxWave", currentWave);
                PlayerPrefs.Save(); 
                Debug.Log("Установлен новый рекорд: " + currentWave);
            }
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); 
        }



        Time.timeScale = 0f; 
    }

    
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void LoadMainMenu()
{
    Time.timeScale = 1f;
    
    SceneManager.LoadScene("MainMenu"); 
}
}