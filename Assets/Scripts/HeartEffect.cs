using UnityEngine;

public class HeartEffect : MonoBehaviour
{
    public float floatSpeed = 2f; 
    public float lifeTime = 1.5f; 

    private void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    private void Update()
    {
        
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
    }
}
