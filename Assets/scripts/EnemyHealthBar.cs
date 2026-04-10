using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Ссылки")]
    public Entity enemy;           
    public Slider healthSlider;    

    private Transform cameraTransform;

    void Start()
    {
        
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        
        if (enemy != null && healthSlider != null)
        {
            healthSlider.maxValue = enemy.MaxHealth;
            healthSlider.value = enemy.CurrentHealth;
        }
    }

    
    void LateUpdate() 
    {
        
        if (enemy != null && healthSlider != null)
        {
            healthSlider.value = enemy.CurrentHealth;
        }

        
        if (cameraTransform != null)
        {
            transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
                             cameraTransform.rotation * Vector3.up);
        }
    }
}