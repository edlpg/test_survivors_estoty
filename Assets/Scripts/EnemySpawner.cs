using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    SpawnSettings _spawnSettings;
    [Inject]
    public void Construct(SpawnSettings spawnSettings)
    {
        _spawnSettings = spawnSettings ?? throw new System.ArgumentNullException(nameof(spawnSettings), "SpawnSettings not provided.");
    }
    [Inject] private GameManager gameManager;
    private float timeSinceLastSpawn;
    private float currentSpawnRate;

    void Start()
    {
        if (_spawnSettings == null)
        {
            Debug.LogError("SpawnSettings is null!!");
            return;
        }

        currentSpawnRate = _spawnSettings.initialSpawnRate;
        timeSinceLastSpawn = currentSpawnRate; // Initialize for enemy to spawns immediately
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= currentSpawnRate)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f; // Reset timer

            // Decrease spawn rate
            if (currentSpawnRate > _spawnSettings.minSpawnRate)
            {
                currentSpawnRate -= _spawnSettings.spawnRateIncrease;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (_spawnSettings.enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned!!!");
            return;
        }

        // Generate a random position outside the camera view
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Randomly select an enemy prefab from the list
        int randomIndex = Random.Range(0,_spawnSettings.enemyPrefabs.Count);
        GameObject enemyPrefab = _spawnSettings.enemyPrefabs[randomIndex];
        // Instantiate the GO
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<Enemy>().InjectManuallyGameManager(gameManager);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null) return Vector3.zero;

        float screenXMin = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        float screenXMax = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        float screenYMin = mainCamera.transform.position.y - mainCamera.orthographicSize;
        float screenYMax = mainCamera.transform.position.y + mainCamera.orthographicSize;

        Vector3 spawnPosition = Vector3.zero;

        // Randomly choose one of the four edges of the screen
        int randomEdge = Random.Range(0, 4);

        switch (randomEdge)
        {
            case 0:
                spawnPosition = new Vector3(screenXMin - _spawnSettings.spawnAreaPadding, Random.Range(screenYMin, screenYMax), 0);
                break;
            case 1: 
                spawnPosition = new Vector3(screenXMax + _spawnSettings.spawnAreaPadding, Random.Range(screenYMin, screenYMax), 0);
                break;
            case 2: 
                spawnPosition = new Vector3(Random.Range(screenXMin, screenXMax), screenYMax + _spawnSettings.spawnAreaPadding, 0);
                break;
            case 3: 
                spawnPosition = new Vector3(Random.Range(screenXMin, screenXMax), screenYMin - _spawnSettings.spawnAreaPadding, 0);
                break;
        }

        return spawnPosition;
    }
}
