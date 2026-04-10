using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject enemyPrefab;   
    public Transform[] spawnPoints;  

    [Header("Настройки волн")]
    public int currentWave = 0;         
    public int baseEnemyCount = 2;      
    public float timeBetweenWaves = 3f; 

    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool isSpawning = false;

    private void Start()
    {
        
        StartCoroutine(StartNextWave());
    }

    private void Update()
    {
        
        if (isSpawning) return;

        
        activeEnemies.RemoveAll(item => item == null);

        
        if (activeEnemies.Count == 0)
        {
            StartCoroutine(StartNextWave());
        }
    }

    private IEnumerator StartNextWave()
    {
        isSpawning = true;
        currentWave++; 
        Debug.Log("Начинается волна " + currentWave);

        
        yield return new WaitForSeconds(timeBetweenWaves);

        
        
        int enemiesToSpawn = baseEnemyCount + (currentWave * 2); 

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            
            GameObject newEnemy = Instantiate(enemyPrefab, randomPoint.position, randomPoint.rotation);
            activeEnemies.Add(newEnemy);

            
            yield return new WaitForSeconds(0.5f); 
        }

        
        isSpawning = false;
    }
}