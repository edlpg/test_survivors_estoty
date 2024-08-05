using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public float playerSpeed = 5.0f;
    public int initialAmmo=50;
    public int maxAmmo=50;
    public float startingHealth = 100;
    public float maxHealth = 100;
    public float shotRate = 0.5f;
    public float lowestShotRate = 0.5f;
    public float enemyDetectionRadius = 10.0f;
    public float timeToDisplayUpgradeDescription = 5.0f;
    public string nameOfDeadScene = "DeadScene";
    public List<Upgrade> upgrades;
}