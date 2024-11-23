using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float initialSpeed = 0.1f;
    [SerializeField] private float finalSpeed = 1.1f;
    [SerializeField] private float acceleration = 0.2f;
    private float currentSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private float maxDamage;
    [SerializeField] private float midDamage;
    [SerializeField] private float minDamage;

    void Start()
    {
        currentSpeed = initialSpeed;

        rb.velocity = new Vector2(initialSpeed, 0);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed < finalSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        else
        {
            currentSpeed = finalSpeed;
        }

        rb.velocity = new Vector2(currentSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            float Distance = Vector2.Distance(player.position, other.transform.position);

            float Damage = CalculateDamage(Distance);

            if (other.TryGetComponent(out HeavyEnemyArmoredStats heavyEnemyArmored))
            {
                heavyEnemyArmored.TakeDamage(Damage);
            }
            else if (other.TryGetComponent(out HeavyEnemyStats heavyEnemy))
            {
                heavyEnemy.TakeDamage(Damage);
            }
            else if (other.TryGetComponent(out SmallEnemyStats smallEnemy))
            {
                smallEnemy.TakeDamage(Damage);
            }

            Debug.Log("Enemy Damaged: " + other.gameObject);

            Debug.Log("Damage Dealt: " + Damage);

            Destroy(gameObject);
        }
    }

    public float CalculateDamage(float Distance) 
    {
        if (Distance <= 20f) return minDamage;

        if (Distance <= 30f) return midDamage;

        if (Distance <= 43f) return maxDamage;

        return 0f;
    }
}
