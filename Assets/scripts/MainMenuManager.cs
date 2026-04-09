using UnityEngine;
using UnityEngine.SceneManagement; // Обязательная библиотека для загрузки сцен!

public class MainMenuManager : MonoBehaviour
{
    // Этот метод мы привяжем к кнопке "Играть"
    public void StartGame()
    {
        // Загружаем игровую сцену. 
        // ВНИМАНИЕ: Если твоя игровая сцена называется не "SampleScene", впиши сюда её точное название!
        SceneManager.LoadScene("SampleScene"); 
    }

    // Этот метод мы привяжем к кнопке "Выход"
    public void ExitGame()
    {
        Debug.Log("Игра закрывается..."); // Это мы увидим только в редакторе
        Application.Quit();             // Это сработает в собранной игре (.exe или .apk)
    }
}