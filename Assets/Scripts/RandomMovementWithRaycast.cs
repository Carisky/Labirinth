using UnityEngine;

public class RandomMovementWithRaycast : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float range = 3f;
    public float checkInterval = 5f; // Интервал между проверками

    private Vector3 targetPosition;
    private float lastCheckTime;

    void Start()
    {
        SetNewTargetPosition();
    }

    void Update()
    {
        // Передвижение к целевой позиции
        MoveTowardsTarget();

        // Проверка, нужно ли установить новую цель
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;
            if (Vector3.Distance(transform.position, targetPosition) < 1f)
            {
                SetNewTargetPosition();
            }
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        RaycastHit hit;

        // Проверка на наличие препятствий по пути
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (hit.collider.CompareTag("wall"))
            {
                // Если обнаружена стена, установить новую цель
                SetNewTargetPosition();
            }
        }

        // Движение к цели
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void SetNewTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;
        targetPosition = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);

        // Проверка на наличие стен на новой цели
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (targetPosition - transform.position).normalized, out hit, range))
        {
            if (hit.collider.CompareTag("wall"))
            {
                SetNewTargetPosition(); // Попробовать установить новую позицию
            }
        }
    }
}
