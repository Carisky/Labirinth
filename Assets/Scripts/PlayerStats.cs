using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxLives = 5;
    public int currentLives;

    public int score = 0;

    void Start()
    {
        currentLives = maxLives;
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        if (currentLives < 0)
        {
            currentLives = 0;
        }
    }

    public void GetPoints(int amount)
    {
        score += amount;
    }

    public void Heal(int amount)
    {
        currentLives += amount;
        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }
    }
}
