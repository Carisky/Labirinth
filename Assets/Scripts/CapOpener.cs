using UnityEngine;
using System.Collections;

public class CapOpener : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public Transform cap; // Ссылка на крышку (Cap)
    public float detectionRadius = 1f; // Радиус, в котором игрок активирует открытие
    public float rotationDuration = 2.5f; // Время поворота
    private bool isOpened = false; // Проверка, открыта ли крышка

    private Quaternion closedRotation; // Начальная позиция крышки
    private Quaternion openedRotation; // Открытая позиция крышки

    void Start()
    {
        // Устанавливаем начальные углы крышки
        closedRotation = cap.rotation;
        openedRotation = closedRotation * Quaternion.Euler(0, 0, 45); // Поворот на 45 градусов
    }

    void Update()
    {
        // Проверяем расстояние между игроком и объектом
        float distance = Vector3.Distance(player.position, transform.position);

        // Если игрок в пределах радиуса и крышка не открыта
        if (distance <= detectionRadius && !isOpened)
        {
            StopAllCoroutines(); // Останавливаем предыдущие корутины, чтобы не было конфликта
            StartCoroutine(RotateCap(openedRotation)); // Открыть крышку
            isOpened = true;
        }
        // Если игрок ушёл и крышка открыта
        else if (distance > detectionRadius && isOpened)
        {
            StopAllCoroutines();
            StartCoroutine(RotateCap(closedRotation)); // Закрыть крышку
            isOpened = false;
        }
    }

    IEnumerator RotateCap(Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Quaternion startingRotation = cap.rotation;

        while (elapsedTime < rotationDuration)
        {
            // Линейная интерполяция между начальной и целевой ротацией
            cap.rotation = Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Ждём до следующего кадра
        }

        // Убедиться, что крышка точно установлена на финальный угол
        cap.rotation = targetRotation;
    }
}
