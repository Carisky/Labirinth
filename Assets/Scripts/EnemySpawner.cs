using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform player; 
    public float spawnDistance = 5f; 
    public int maxEnemies = 30; 

    private List<GameObject> spawnedEnemies = new List<GameObject>(); 
    private GameObject[] spawnPoints; 
    private bool spawnPointsInitialized = false; 

    void Update()
    {
        
        if (!spawnPointsInitialized)
        {
            InitializeSpawnPoints();
            return; 
        }

        
        if (spawnedEnemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }

        
        spawnedEnemies.RemoveAll(enemy => enemy == null);
    }

    void InitializeSpawnPoints()
    {
        
        spawnPoints = GameObject.FindGameObjectsWithTag("floor");

        if (spawnPoints.Length > 0)
        {
            spawnPointsInitialized = true; 
            Debug.Log("Спавн-поинты успешно инициализированы.");
        }
        else
        {
            Debug.LogWarning("Не найдено объектов с тегом 'floor' на сцене! Повторная проверка...");
        }
    }

    void SpawnEnemy()
    {
        
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Нет доступных мест для спавна.");
            return;
        }

        
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        
        if (Vector3.Distance(spawnPoint.transform.position, player.position) > spawnDistance)
        {
            
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position + new Vector3(1, 0.1f, 1), Quaternion.identity);
            spawnedEnemies.Add(newEnemy);
        }
    }
}
