using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Zenject;

public class GameManager : MonoBehaviour
{
    private GameUIManager _gameUI;
    private GameSettings _gameSettings;
    private UpgradeManager _upgradeManager;
    private PlayerManager _playerManager;
    private int currentLevel;
    private int currentExperience;
 
    [Inject]
    public void Construct(GameSettings gameSettings, GameUIManager gameUI,UpgradeManager upgradeManager, PlayerManager playerManager)
    {
        _gameSettings = gameSettings;
        _gameUI = gameUI;
        _upgradeManager = upgradeManager;
        _playerManager = playerManager;
    }

    private void Start()
    {
        InitalizeGameManager();
    }

    private void InitalizeGameManager()
    {
        currentLevel = 0;
        currentExperience = 0;
        _playerManager.SetInitialPlayerValues(_gameSettings);
    }
    
    public void PropTouched(ItemConfig.ItemType type, int value)
    {
        AddPropValues(type,value);
    }

    void AddPropValues(ItemConfig.ItemType type, int value)
    {
        switch (type)
        {
            case ItemConfig.ItemType.AmmoBox:
                _playerManager.AddAmmo(value);
                break;
            case ItemConfig.ItemType.ExperienceGem:
                AddExp(value);
                break;
            default:
                _playerManager.UpdateHealth(value);
                break;
        }
    }

    public void OnEnemyDefeated(int exp, int lootRate)
    {
        AddExp(exp);
        _gameUI.AddKill();
    }

    public void PlayerTakesDamage(float damage)
    {
        _playerManager.PlayerTakesDamage(damage);
    }
   
    private void AddExp(int exp)
    {
        currentExperience += exp; 
   while (currentExperience >= 100)
        {
            currentExperience -= 100; 
            LevelUp();
        }
        _gameUI.UpdateExpBar(currentExperience / 100f);
    }

    void LevelUp()
    {
        _upgradeManager.ApplyUpgrade();
        currentLevel++;
        _gameUI.UpdateLevel(currentLevel);
    }

    public void Dead()
    {
        SceneManager.LoadScene(_gameSettings.nameOfDeadScene);
    }

}


