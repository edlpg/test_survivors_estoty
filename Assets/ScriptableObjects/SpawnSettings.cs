using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SpawnSettings", menuName = "SpawnSettings")]
public class SpawnSettings : ScriptableObject
{
    public List<GameObject> enemyPrefabs;
    public float initialSpawnRate = 2f;
    public float spawnRateIncrease = 0.5f;
    public float minSpawnRate = 0.5f;
    public float spawnAreaPadding = 10f;
}
