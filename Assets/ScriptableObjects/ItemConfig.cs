using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "ItemConfig")]
public class ItemConfig :ScriptableObject
{
    public ItemType type;
    public int increaseAmount;
    public float moveSpeed;
    public float distanceToMoveTowardsPlayer;
    public Sprite sprite; 

    public enum ItemType
    {
        HealthPotion,
        ExperienceGem,
        AmmoBox
    }
}
