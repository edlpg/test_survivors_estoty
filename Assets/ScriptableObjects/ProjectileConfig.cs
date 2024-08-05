using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{
    public float speed;
    public int damage;
    public float lifetime;
    public bool isPoisonous;
    public Sprite sprite;
}
