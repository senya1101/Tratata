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
}