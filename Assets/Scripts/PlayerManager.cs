using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    [Inject] private GameUIManager gameUI;
    [Inject] private PlayerController player;
    private UpgradeManager upgradeManager;

    private int currentAmmo;
    private int maxAmmo;
    private float currentHealth;
    private float maxHealth;

    public void PlayerTakesDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            gameManager.Dead();

        gameUI.UpdateHealthBar(currentHealth / 100f);
    }

    public void UpdateHealth(int sum)
    {
        currentHealth += sum;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        gameUI.UpdateHealthBar(currentHealth / 100f);
    }

    void SetHealth(float health)
    {
        currentHealth = health;
    }
    public void SetAmmo(int val)
    {
        currentAmmo = val;
        gameUI.UpdateAmmoValue(currentAmmo);
    }

    public void AddAmmo(int val)
    {
        currentAmmo += val;
        if (currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;

        gameUI.UpdateAmmoValue(currentAmmo);
    }

    public void UsedAmmo()
    {
        currentAmmo--;
        gameUI.UpdateAmmoValue(currentAmmo);
    }
 
    public int GetAmmo()
    {
        return currentAmmo;
    }
    public void SetInitialPlayerValues(GameSettings gameSettings)
    {
        SetHealthAndAmmoValues(gameSettings);
        player.SetSpeed(gameSettings.playerSpeed);
        player.SetShootRate(gameSettings.shotRate);
        player.SetDistanceToAttack(gameSettings.enemyDetectionRadius);

    }

    private void SetHealthAndAmmoValues(GameSettings gameSettings)
    {
        SetAmmo(gameSettings.initialAmmo);
        maxAmmo = gameSettings.maxAmmo;
        SetHealth(gameSettings.startingHealth);
        maxHealth = gameSettings.maxHealth;
        ResetUI();
    }
    private void ResetUI() 
    {
        gameUI.ResetValues(currentHealth / 100f);
    }
}
