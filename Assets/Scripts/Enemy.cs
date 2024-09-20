using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1; // Урон, который враг наносит игроку

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, имеет ли объект тег "Player"
        if (other.CompareTag("Player"))
        {
            // Пытаемся получить компонент PlayerStats у объекта
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                // Наносим урон игроку
                playerStats.TakeDamage(damage);
            }
        }
    }
}
