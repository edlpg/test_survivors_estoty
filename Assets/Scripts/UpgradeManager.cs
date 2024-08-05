using UnityEngine;
using System.Collections.Generic;
using Zenject;
public class UpgradeManager : MonoBehaviour
{
    [Inject]private GameUIManager gameUI;
    [Inject] private PlayerController player;
    [Inject] private GameSettings _gameSettings;


    private Upgrade GetRandomUpgrade()
    {
        Upgrade randomUpgrade;
        int randomIndex = Random.Range(0, _gameSettings.upgrades.Count);
        randomUpgrade = _gameSettings.upgrades[randomIndex];
        /*
        if (upgrades[randomIndex].UpgradeOnce)
        {
            upgrades.RemoveAt(randomIndex);
        }
        */
        return randomUpgrade;
    }

    public void ApplyUpgrade()
    {
        Upgrade upgrade = GetRandomUpgrade();
        switch (upgrade.upgradeType)
        {
            case UpgradeType.ShootRate:
                player.UpdateShootRate(upgrade.increaseValue, _gameSettings.lowestShotRate);
                break;
            case UpgradeType.MaxHealth:
                _gameSettings.maxHealth += upgrade.increaseValue;
                break;
            case UpgradeType.MoveSpeed:
                player.UpdateSpeed(upgrade.increaseValue);
                break;
            case UpgradeType.PoisonedBullets:
                player.IsUsingPoisonousBullets();
                break;
        }
        gameUI.SetDescriptionText(upgrade.description);
        Invoke("ResetUpgradeText", _gameSettings.timeToDisplayUpgradeDescription);
    }

    private void ResetUpgradeText()
    {
        gameUI.SetDescriptionText("");
    }

}
