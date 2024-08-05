using UnityEngine;
public interface ITargetingSystem
{
    Transform GetClosestEnemy(Vector3 position, float detectionRadius);
}

public interface IShootingController
{
    void Shoot(Transform target, Transform spawnPoint, GameObject projectilePrefab, GameObject projectilePrefabPoisonous, bool usePoisonousBullets);
}
