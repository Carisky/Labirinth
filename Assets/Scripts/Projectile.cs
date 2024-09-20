using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    public float rotationSpeed = 360f; // degrees per second
    public static int damage = 3;

    void Start()
    {
        // Уничтожить объект через 10 секунд после появления
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        // Rotate the projectile around its own axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Target>(out var target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
