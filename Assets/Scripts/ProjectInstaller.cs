using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public GameSettings gameSettings;
    public SpawnSettings spawnSettings; 

    public override void InstallBindings()
    {
        Container.BindInstance(gameSettings).AsSingle();
        Container.BindInstance(spawnSettings).AsSingle();
        Container.Bind<ITargetingSystem>().To<TargetingSystem>().AsSingle();
        Container.Bind<IShootingController>().To<ShootingController>().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameUIManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Enemy>().FromComponentInHierarchy().AsTransient();
        Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UpgradeManager>().FromComponentInHierarchy().AsSingle();
    }
}
