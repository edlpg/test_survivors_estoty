using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectilePrefabPoisonous;
    private Transform _closestEnemy;
    private float _nextShotTime;
    private float shootingRate;
    private float movementSpeed;
    private float detectionRadius;
    private bool isUsingPoisonousBullets;
    [SerializeField] private InputActionReference inputAction;
    [Inject] private PlayerManager playerManager;
    private ITargetingSystem _targetingSystem;
    private IShootingController _shootingController;
    [Inject]
    public void Construct(ITargetingSystem targetingSystem, IShootingController shootingController)
    {
        _targetingSystem = targetingSystem;
        _shootingController = shootingController;

    }
    void Update()
    {
        MovePlayer();
        RotateTowardsClosestEnemy();

        if (Time.time >= _nextShotTime && _closestEnemy != null)
        {
            Shoot();
            _nextShotTime = Time.time + shootingRate;
        }
    }

    public void SetInitialPlayerValues(float speed,float shootRate, float radius)
    {
        SetSpeed(speed);
        SetShootRate(shootRate);
        SetDistanceToAttack(radius);
    }

    public void IsUsingPoisonousBullets()
    {
        isUsingPoisonousBullets = true;
    }
    public void SetShootRate(float rate)
    {
        shootingRate = rate;
    }

    public void UpdateShootRate(float rate,float lowest)
    {
        shootingRate -= rate;
        if (shootingRate < lowest)
            shootingRate = lowest;
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }
    public void UpdateSpeed(float speed)
    {
        movementSpeed += speed;
    }

    public void SetDistanceToAttack(float radius)
    {
        detectionRadius = radius;
    }

    Vector3 _lastMovementDirection;
    void MovePlayer()
    {
        Vector2 movement = inputAction.action.ReadValue<Vector2>();

        if (movement != Vector2.zero)
        {
            _lastMovementDirection = movement.normalized;
        }
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }


    void RotateTowardsClosestEnemy()
    {
        _closestEnemy = _targetingSystem.GetClosestEnemy(transform.position, detectionRadius);
    
        Vector3 direction;

        if (_closestEnemy != null)
        {
            direction = _closestEnemy.position - transform.position;
        }
        else
        {
            direction = _lastMovementDirection;
        }

        if (direction != Vector3.zero)
        {
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = targetRotation;
        }
    }

    void Shoot()
    {
        // if still have bullets will shoot
        if (_closestEnemy != null && playerManager.GetAmmo()>0)
        {
            _shootingController.Shoot(_closestEnemy, projectileSpawnPoint, projectilePrefab, projectilePrefabPoisonous, isUsingPoisonousBullets);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
