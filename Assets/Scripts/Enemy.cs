using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    private float currentHP;
    private Transform player;
    private float lastDamageTime;
    [Inject] private GameManager gameManager;
    bool isPoisoned;
    bool duringCooldownAtack;
  
    void Start()
    {
        InitializeEnemey();
    }
    void Update()
    {
        MoveTowardsPlayer();
    }
    private void InitializeEnemey()
    {
        currentHP = stats.health;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("Player not found! Ensure the player has the tag 'Player'.");
        }
    }

    public void InjectManuallyGameManager(GameManager gm)
    {
        gameManager = gm;
    }
    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, stats.moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandleDamageToPlayer();
        }
    }

    private void HandleDamageToPlayer()
    {
        if (duringCooldownAtack)
            return;

        DamagePlayer();
    }

   private void DamagePlayer()
    {
        gameManager.PlayerTakesDamage(stats.damage);
        duringCooldownAtack = true;
        Invoke("ResetCooldown", stats.damageInterval);
    }

    void ResetCooldown()
    {
        duringCooldownAtack = false;
    }

    public void TakeDamage(int damage, bool isPoisonous)
    {
        currentHP -= damage;
        if (isPoisonous && !isPoisoned)
        {
            ApplyPoisonEffect();
        }

        if (currentHP <= 0)
        {
            HandleDeath();
        }
    }

    private void ApplyPoisonEffect()
    {
        isPoisoned = true;
        Invoke("PoisonDamage", stats.posionDamageInterval);
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void PoisonDamage()
    {
        TakeDamage(stats.damageWhenPoisoned, false);
        Invoke("PoisonDamage", stats.posionDamageInterval);
    }

    private void HandleDeath()
    {
        gameManager.OnEnemyDefeated(stats.expDrop, stats.lootDropRate);
        DropLoot();
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        float dropChance = stats.lootDropRate / 100f;
        if (Random.value <= dropChance)
        {
            if (stats.dropouts != null && stats.dropouts.Count > 0)
            {
                InstanceLoot();
            }
            else
            {
                Debug.LogWarning("No loot items are configured in the Scriptable Object EnemyStats");
            }
        }
    }
    private void InstanceLoot()
    {
        int randomIndex = Random.Range(0, stats.dropouts.Count);
        GameObject loot = Instantiate(stats.dropouts[randomIndex], transform.position, Quaternion.identity);
        loot.GetComponent<Item>().InjectManuallyGameManager(gameManager);
    }

}
