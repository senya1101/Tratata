using UnityEngine;
using UnityEngine.UI; // Обязательно подключаем библиотеку для работы с UI

public class HUDManager : MonoBehaviour
{
    [Header("Ссылки на сущности")]
    public Entity player;   // Сюда перетащим игрока
    public Entity crystal;  // Сюда перетащим колодец (кристалл)

    [Header("Ссылки на UI (Полоски здоровья)")]
    public Slider playerHealthSlider;
    public Slider crystalHealthSlider;

    void Start()
    {
        // Настраиваем максимальные значения ползунков при старте игры
        if (player != null && playerHealthSlider != null)
        {
            playerHealthSlider.maxValue = player.MaxHealth;
            playerHealthSlider.value = player.CurrentHealth;
        }
            
        if (crystal != null && crystalHealthSlider != null)
        {
            crystalHealthSlider.maxValue = crystal.MaxHealth;
            crystalHealthSlider.value = crystal.CurrentHealth;
        }
    }

    void Update()
    {
        // Постоянно обновляем значения ползунков (чтобы они реагировали на урон)
        if (player != null && playerHealthSlider != null)
            playerHealthSlider.value = player.CurrentHealth;

        if (crystal != null && crystalHealthSlider != null)
            crystalHealthSlider.value = crystal.CurrentHealth;
    }
}