using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float grenadeDamage;
    [SerializeField] private LayerMask enemyMask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) 
        {
            Explode();

            Destroy(gameObject);

        }
    }

    private void Explode() 
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyMask);

        foreach(Collider2D enemy in hitEnemies) 
        {
            Debug.Log("Damaged Enemy: " + enemy.gameObject.name);

            Debug.Log("Damage Dealt: " + grenadeDamage);

            if (enemy.TryGetComponent<SmallEnemyStats>(out SmallEnemyStats smallEnemy)) 
            {
                smallEnemy.TakeDamage(grenadeDamage);
            }

            else if(enemy.TryGetComponent<HeavyEnemyStats>(out HeavyEnemyStats heavyEnemy)) 
            {
                heavyEnemy.TakeDamage(grenadeDamage);
            }

            else if(enemy.TryGetComponent<HeavyEnemyArmoredStats>(out HeavyEnemyArmoredStats heavyEnemyArmored)) 
            {
                heavyEnemyArmored.TakeDamage(grenadeDamage);
            }
        }        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
