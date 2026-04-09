using UnityEngine;

public class Crystal : Entity
{
    protected override void Die()
    {
        Debug.Log("Кристалл уничтожен! ИГРА ОКОНЧЕНА.");

        gameObject.SetActive(false);

    }
}