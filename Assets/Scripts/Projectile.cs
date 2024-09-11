using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    public int damage = 1;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
