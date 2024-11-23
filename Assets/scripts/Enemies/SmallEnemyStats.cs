using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmallEnemyStats : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float initialSpeed = 1.1f;
    [SerializeField] private float finalSpeed = 2f;
    [SerializeField] private float acceleration = 1.1f;
    private float currentSpeed;
    [SerializeField] private float damage = 10f;
    public Slider healthBar;
    public float HP = 100f;
    private float targetFillAmount;
   
    void Start()
    {
        currentSpeed = initialSpeed;
        rb.velocity = new Vector2(initialSpeed, 0);

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (healthBar != null)
        {
            healthBar.value = 1f;
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
        HP -= damage;
        if (HP < 0) HP = 0;

        targetFillAmount = HP / 100f;

        if (healthBar != null)
        {
            StartCoroutine(SmoothUpdateHealthBar());
        }

        if (HP <= 0)
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
            healthBar.value = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / duration);
            yield return null;
        }

        healthBar.value = targetFillAmount;
    }
}
