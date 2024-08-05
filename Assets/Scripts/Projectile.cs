using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{
    public ProjectileConfig config;

    void Start()
    {
        StartProjectille();
    }

    void Update()
    {
        MoveProjectile();
    }

    private void StartProjectille()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = config.sprite;
        Destroy(gameObject, config.lifetime);
    }
    private void MoveProjectile()
    {
        transform.Translate(Vector2.right * config.speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(config.damage, config.isPoisonous);
            }
            Destroy(gameObject);
        }
    }
}

