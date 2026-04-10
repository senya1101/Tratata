using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Базовые характеристики")]
    public float maxHealth = 100f;

    protected float currentHealth;

    
    public float CurrentHealth => currentHealth; 
    public float MaxHealth => maxHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} получил {amount} урона. Текущее здоровье: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} уничтожен!");
        Destroy(gameObject);
    }
}