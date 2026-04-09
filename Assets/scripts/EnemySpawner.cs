using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject enemyPrefab;   
    public Transform[] spawnPoints;   

    private void Start()
    {
        SpawnInitialEnemies();
    }

    private void SpawnInitialEnemies()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(enemyPrefab, point.position, point.rotation);
        }
    }
}