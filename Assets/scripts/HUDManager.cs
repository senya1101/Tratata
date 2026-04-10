using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class HUDManager : MonoBehaviour
{
    [Header("Ссылки на сущности")]
    public Entity player;   
    public Entity crystal;  
    public EnemySpawner spawner; 

    [Header("Ссылки на UI (Полоски здоровья)")]
    public Slider playerHealthSlider;
    public Slider crystalHealthSlider;

    [Header("Ссылки на UI (Текст)")]
    public TextMeshProUGUI waveText;

    void Start()
    {
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
        if (player != null && playerHealthSlider != null)
            playerHealthSlider.value = player.CurrentHealth;

        if (crystal != null && crystalHealthSlider != null)
            crystalHealthSlider.value = crystal.CurrentHealth;

        if (spawner != null && waveText != null)
        {
            waveText.text = "ВОЛНА: " + spawner.currentWave;
        }
    }
}