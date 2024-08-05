using UnityEngine;
using Unity.UI;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float health;
    public float damage;
    public float damageInterval;
    public float moveSpeed;
    public int expDrop;
    public int lootDropRate;
    public int damageWhenPoisoned;
    public float posionDamageInterval;
    public List<GameObject> dropouts;

}