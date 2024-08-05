using UnityEngine;
using UnityEngine.UI;


public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Slider expBar;
    [SerializeField] private Text ammoIndicator;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Text killCounter;
    [SerializeField] private Text level;
    [SerializeField] private Text ammo;
    [SerializeField] private Text upgradeDescription;
    private int kills;

    public void ResetValues(float initHealth)
    {
        UpdateExpBar(0);
        UpdateLevel(0);
        UpdateHealthBar(initHealth);
        ResetKills();
    }
     void ResetKills()
    {
        kills = -1;
        AddKill();
    }

    public void UpdateExpBar(float value)
    {
        expBar.value = value;
    }
    public void UpdateLevel(float value)
    {
        level.text = value.ToString();
    }

    public void UpdateAmmoIndicator(int currentAmmo, int maxAmmo)
    {
        ammoIndicator.text = $"{currentAmmo}/{maxAmmo}";
    }

    public void UpdateHealthBar(float value)
    {
        healthBar.value = value;
    }
    public void UpdateAmmoValue(int value)
    {
        ammo.text = "Ammo: "+value;
    }

    public void AddKill()
    {
        kills++;
        killCounter.text =  kills.ToString();
    }

    public void SetDescriptionText(string description)
    {
        upgradeDescription.text = description;
    }
}

