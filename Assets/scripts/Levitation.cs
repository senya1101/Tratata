using UnityEngine;

public class Levitation : MonoBehaviour
{
    [Header("Настройки левитации")]
    public float amplitude = 0.3f; // Высота полета
    public float speed = 2.0f;     // Скорость движения
    
    private float startY;

    void Start()
    {
        // Запоминаем начальную высоту объекта
        startY = transform.position.y;
    }

    void Update()
    {
        // Вычисляем новую позицию по вертикали (Y)
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        
        // Применяем позицию, сохраняя X и Z неизменными
        transform.position = new Vector3(transform.position.x, startY + offset, transform.position.z);
    }
}