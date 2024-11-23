using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeavyEnemyArmoredStats : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private  Rigidbody2D rb;
    [SerializeField] private float initialSpeed = 1.1f;
    [SerializeField] private float finalSpeed = 3f;
    [SerializeField] private float acceleration = 1.1f;
    private float currentSpeed;
    [SerializeField] private float damage = 30f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider armorBar;
    [SerializeField] private float HP = 150f;
    [SerializeField] private float Armor = 50f; 
    private float targetHealthFillAmount;
    private float targetArmorFillAmount;
    void Start()
    {
        currentSpeed = initialSpeed;
        rb.velocity = new Vector2(initialSpeed, 0);

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (healthBar != null)
        {
            healthBar.value = 1f;
        }

        if (armorBar != null)
        {
            armorBar.value = 1f;
        }
    }

    void Update()
    {
        if (currentSpeed < finalSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;

            if (currentSpeed > finalSpeed)
            {
                currentSpeed = finalSpeed;
            }
        }

        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.velocity = direction * currentSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();

            if (playerManager != null)
            {
                playerManager.TakeDamage(damage);

            }

            Destroy(gameObject); 
        }
    }

    public void TakeDamage(float damage)
    {
        
        if (Armor > 0)
        {
            Armor -= damage;

            if (Armor < 0)
            {
                damage = -Armor;
                Armor = 0;
            }
            else
            {
                damage = 0; 
            }

            targetArmorFillAmount = Armor / 50f;

            if (armorBar != null)
            {
                StartCoroutine(SmoothUpdateArmorBar());
            }
        }

        if (damage > 0)
        {
            HP -= damage;

            if (HP < 0) HP = 0;

            targetHealthFillAmount = HP / 150f;

            if (healthBar != null)
            {
                StartCoroutine(SmoothUpdateHealthBar());
            }
        }

        if (HP <= 0 && Armor <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SmoothUpdateHealthBar()
    {
        float currentFillAmount = healthBar.value;

        float elapsedTime = 0f;

        float duration = 0.5f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            healthBar.value = Mathf.Lerp(currentFillAmount, targetHealthFillAmount, elapsedTime / duration);

            yield return null;
        }

        healthBar.value = targetHealthFillAmount;
    }

    private IEnumerator SmoothUpdateArmorBar()
    {
        float currentFillAmount = armorBar.value;

        float elapsedTime = 0f;

        float duration = 0.5f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            armorBar.value = Mathf.Lerp(currentFillAmount, targetArmorFillAmount, elapsedTime / duration);

            yield return null;
        }

        armorBar.value = targetArmorFillAmount;
    }
}
