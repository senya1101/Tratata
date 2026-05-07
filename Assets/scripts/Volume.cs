using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Volume : MonoBehaviour // Имя класса совпадает с названием твоего файла
{
    public SceneMusic currentSceneMusic; // Сюда перетащим объект с музыкой на сцене

    void Start()
{
    Slider slider = GetComponent<Slider>();
    
    if (currentSceneMusic == null)
    {
        currentSceneMusic = FindFirstObjectByType<SceneMusic>();
    }

    slider.value = GameSettings.MusicVolume;
    
    slider.onValueChanged.AddListener(val => 
    {
        GameSettings.MusicVolume = val; 
        
        if (currentSceneMusic != null) 
        {
            currentSceneMusic.UpdateVolume(); 
        }
    });
}
}