using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Ссылки")]
    public Entity enemy;           // Ссылка на компонент Entity (EnemyBot)
    public Slider healthSlider;    // Ссылка на ползунок здоровья

    private Transform cameraTransform;

    void Start()
    {
        // Находим главную камеру на сцене
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Настраиваем стартовое здоровье
        if (enemy != null && healthSlider != null)
        {
            healthSlider.maxValue = enemy.MaxHealth;
            healthSlider.value = enemy.CurrentHealth;
        }
    }

    // Используем LateUpdate, чтобы полоска поворачивалась ПОСЛЕ того, как враг сдвинулся
    void LateUpdate() 
    {
        // Обновляем значение здоровья
        if (enemy != null && healthSlider != null)
        {
            healthSlider.value = enemy.CurrentHealth;
        }

        // Заставляем полоску всегда смотреть ровно в камеру (эффект Биллборда)
        if (cameraTransform != null)
        {
            transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
                             cameraTransform.rotation * Vector3.up);
        }
    }
}