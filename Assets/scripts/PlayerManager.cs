using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D rb;
     [SerializeField] private float speed;
    public Image Healhtbar;
    public float HP = 100f;
    public float targetFillAmount;
    public AudioSource source;
    public AudioClip tickleClip;
    public void FixedUpdate() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 Dir = new Vector2 (horizontalInput, 0);

        Dir = Dir.normalized;

        rb.velocity = Dir * speed;
    }

    public void TakeDamage(float damage) 
    {
        HP -= damage;

        if (HP < 0) 
        {
            HP = 0;
     
        }

        targetFillAmount = HP / 100f;

        StartCoroutine(SmoothUpdateHealthBar());

        if(HP > 0) 
        {
            source.PlayOneShot(tickleClip);
        }

        else if (source.isPlaying) 
        {
            source.Stop();
        }

    }

    public IEnumerator SmoothUpdateHealthBar() 
    {
        float currentFillAmount = Healhtbar.fillAmount;

        float elapsedTime = 0f;

        float duration = 0.5f;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;

            Healhtbar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / duration);

            yield return null;
        }

        Healhtbar.fillAmount = targetFillAmount;
    }
}
