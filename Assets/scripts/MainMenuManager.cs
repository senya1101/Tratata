using UnityEngine;
using UnityEngine.SceneManagement; // Для смены сцен
using TMPro; // Для работы с текстом TextMeshPro

public class MainMenuController : MonoBehaviour
{
    [Header("UI Элементы")]
    public TextMeshProUGUI recordText; // Сюда в инспекторе перетащи объект с текстом рекорда

    private void Start()
    {
        // 1. При запуске меню проверяем, есть ли сохраненный рекорд
        // Ключ "MaxWave" должен совпадать с тем, что мы писали в GameManager
        int maxWave = PlayerPrefs.GetInt("MaxWave", 0);

        // 2. Выводим рекорд на экран
        if (recordText != null)
        {
            if (maxWave > 0)
                recordText.text = "ЛУЧШИЙ РЕЗУЛЬТАТ: " + maxWave + " ВОЛНА";
            else
                recordText.text = "РЕКОРДОВ ПОКА НЕТ";
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("SampleScene"); 
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit(); // Работает в скомпилированной версии (.exe)
    }

    public void ResetRecord()
    {
        PlayerPrefs.DeleteKey("MaxWave");
        if (recordText != null) recordText.text = "РЕКОРД СБРОШЕН";
    }
}