using UnityEngine;

public class TargetingSystem : ITargetingSystem
{
    public Transform GetClosestEnemy(Vector3 position, float detectionRadius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, detectionRadius);
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(hitCollider.transform.position, position);
                if (distance < closestDistance)
                {
                    closest = hitCollider.transform;
                    closestDistance = distance;
                }
            }
        }

        return closest;
    }
}
