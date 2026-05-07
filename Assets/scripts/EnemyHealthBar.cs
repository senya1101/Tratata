using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("UI Элементы")]
    public Slider healthSlider;
    
    [Header("Кого отслеживаем?")]
    public Entity enemyEntity;

    private Transform cam;
    private Canvas myCanvas; 

    void Start()
    {
        
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }

        
        myCanvas = GetComponent<Canvas>();
        if (myCanvas != null && Camera.main != null)
        {
            myCanvas.worldCamera = Camera.main; 
        }
        
        
        if (enemyEntity == null)
        {
            enemyEntity = GetComponentInParent<Entity>();
        }
            
        
        if (enemyEntity != null && healthSlider != null)
        {
            healthSlider.maxValue = enemyEntity.MaxHealth;
            healthSlider.value = enemyEntity.CurrentHealth;
        }
    }

    void LateUpdate()
    {
        if (enemyEntity != null && healthSlider != null)
        {
            healthSlider.value = enemyEntity.CurrentHealth;
        }

        if (cam != null)
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}