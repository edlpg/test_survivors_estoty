using UnityEngine;
public class ShootingController : IShootingController
{
    private PlayerManager _playerManager;

    public ShootingController(PlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    public void Shoot(Transform target, Transform spawnPoint, GameObject projectilePrefab, GameObject projectilePrefabPoisonous, bool usePoisonousBullets)
    {
        if (_playerManager.GetAmmo() > 0)
        {
            _playerManager.UsedAmmo();

            GameObject projectileType = usePoisonousBullets ? projectilePrefabPoisonous : projectilePrefab;
            GameObject projectile = Object.Instantiate(projectileType, spawnPoint.position, spawnPoint.rotation);
            projectile.transform.right = (target.position - projectile.transform.position).normalized;
        }
    }
}
