using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkConnectionManager : MonoBehaviour
{
    [Header("Настройки сцен")]
    [Tooltip("Точное название вашей игровой сцены")]
    public string gameSceneName = "SampleScene"; // Впишите сюда название вашей игровой сцены!

    public void StartHost()
    {
        // 1. Запускаем хост (Сервер + локальный игрок)
        NetworkManager.Singleton.StartHost();
        
        // 2. Сервер загружает игровую сцену. 
        // Все текущие и будущие клиенты автоматически перейдут на неё!
        NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        
        Debug.Log("Хост запущен, переход на игровую сцену...");
    }

    public void StartClient()
    {
        // Клиенту НЕ НУЖНО самому загружать сцену. 
        // Он просто подключается к хосту, и Netcode сам загрузит ему нужный уровень.
        NetworkManager.Singleton.StartClient();
        
        Debug.Log("Попытка подключения к хосту...");
    }
}