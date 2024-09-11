using UnityEngine;

public class Target : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false;

    private int points = 400;
    public GameObject heartEffectPrefab;
    public AudioClip hitSound; 
    public AudioClip dieSound; 
    private AudioSource audioSource; 

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>(); 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        SpawnHeartEffect();
        PlayHitSound();

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.GetPoints(points);
        }
        audioSource.PlayOneShot(dieSound); 
        Destroy(gameObject);
    }

    private void SpawnHeartEffect()
    {
        if (heartEffectPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0.5f, 0);
            Instantiate(heartEffectPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound); 
        }
    }
}
