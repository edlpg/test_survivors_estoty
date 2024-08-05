using UnityEngine;

public enum UpgradeType { ShootRate, MaxHealth, MoveSpeed, PoisonedBullets }

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
    public UpgradeType upgradeType;
    public string description;
    public float increaseValue;
    public bool UpgradeOnce;// in the case of PoisonedBullets, Upgrade only need to be added once and therefore cannot have this upgrade again
}
