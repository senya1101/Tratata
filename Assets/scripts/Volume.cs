using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Volume : MonoBehaviour // Имя класса совпадает с названием твоего файла
{
    public SceneMusic currentSceneMusic; // Сюда перетащим объект с музыкой на сцене

    void Start()
    {
        Slider slider = GetComponent<Slider>();
        
        slider.value = GameSettings.MusicVolume;
        
        // При движении ползунка
        slider.onValueChanged.AddListener(val => 
        {
            GameSettings.MusicVolume = val; // Записываем в память
            
            // Если на сцене есть музыка, сразу меняем ей громкость
            if (currentSceneMusic != null) 
            {
                currentSceneMusic.UpdateVolume(); 
            }
        });
    }
}