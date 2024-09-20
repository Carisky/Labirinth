using UnityEngine;

public class HUD : MonoBehaviour
{
    private float deltaTime = 0.0f;
    public Texture2D heartTexture;
    public Texture2D upgradeHealthTexture;  // Картинка для улучшения здоровья
    public Texture2D upgradeSpeedTexture;   // Картинка для улучшения скорости
    public Texture2D upgradeDamageTexture;  // Картинка для улучшения урона
    private PlayerStats playerHealth;

    public static bool isUpgradeMenuOpen = false; // Флаг состояния меню улучшений
    private string interactText = "";

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }

        // Скрываем курсор по умолчанию
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Открытие меню улучшений при нажатии клавиши "Y"
        if (playerHealth != null && playerHealth.upgradeAvailable && Input.GetKeyDown(KeyCode.Y))
        {
            isUpgradeMenuOpen = !isUpgradeMenuOpen; // Переключение состояния меню
            if (isUpgradeMenuOpen)
            {
                // Останавливаем время
                Time.timeScale = 0;

                // Включаем курсор
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                // Возобновляем время
                Time.timeScale = 1;

                // Скрываем курсор
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 12;
        style.normal.textColor = Color.white;

        float fps = 1.0f / deltaTime;
        GUI.Label(new Rect(10, 10, 200, 50), $"FPS: {Mathf.Ceil(fps)}", style);

        float crossSize = 5;
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        GUI.DrawTexture(new Rect(centerX - crossSize, centerY - 1, crossSize * 2, 2), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(centerX - 1, centerY - crossSize, 2, crossSize * 2), Texture2D.whiteTexture);

        if (!string.IsNullOrEmpty(interactText))
        {
            GUIStyle interactStyle = new GUIStyle();
            interactStyle.fontSize = 24;
            interactStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(centerX - 50, centerY + 30, 200, 50), interactText, interactStyle);
        }

        if (playerHealth != null && heartTexture != null)
        {
            float heartSize = 80;
            float padding = 5;
            float startX = Screen.width - (heartSize + padding) * playerHealth.currentLives;
            float startY = 10;

            for (int i = 0; i < playerHealth.currentLives; i++)
            {
                GUI.DrawTexture(new Rect(startX + (heartSize + padding) * i, startY, heartSize, heartSize), heartTexture);
            }
        }
        else if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth component is not assigned!");
        }

        if (playerHealth != null)
        {
            string scoreText = $"Score: {playerHealth.score}";
            GUIStyle scoreStyle = new GUIStyle();
            scoreStyle.fontSize = 20;
            scoreStyle.normal.textColor = Color.white;

            Vector2 scoreSize = scoreStyle.CalcSize(new GUIContent(scoreText));
            float scoreX = Screen.width - scoreSize.x - 10;
            float scoreY = Screen.height - scoreSize.y - 10;

            GUI.Label(new Rect(scoreX, scoreY, scoreSize.x, scoreSize.y), scoreText, scoreStyle);
        }

        // Отображаем сообщение о доступности улучшений
        if (playerHealth != null && playerHealth.upgradeAvailable && !isUpgradeMenuOpen)
        {
            GUIStyle upgradeStyle = new GUIStyle();
            upgradeStyle.fontSize = 24;
            upgradeStyle.normal.textColor = Color.yellow;
            GUI.Label(new Rect(10, Screen.height - 50, 400, 30), "Press 'Y' to upgrade!", upgradeStyle);
        }

        // Отображаем меню улучшений, если оно открыто
        if (isUpgradeMenuOpen)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 200), "Upgrade Menu");

            float iconSize = 64; // Размер иконок улучшений
            float iconY = Screen.height / 2 - iconSize / 2;
            float padding = 20; // Расстояние между иконками

            Rect healthRect = new Rect(Screen.width / 2 - 120, iconY, iconSize, iconSize);
            Rect speedRect = new Rect(Screen.width / 2 - 120 + iconSize + padding, iconY, iconSize, iconSize);
            Rect damageRect = new Rect(Screen.width / 2 - 120 + 2 * (iconSize + padding), iconY, iconSize, iconSize);

            // Отображаем иконки улучшений
            GUI.DrawTexture(healthRect, upgradeHealthTexture);
            GUI.DrawTexture(speedRect, upgradeSpeedTexture);
            GUI.DrawTexture(damageRect, upgradeDamageTexture);

            // Обрабатываем выбор улучшений при нажатии
            if (Event.current.type == EventType.MouseDown)
            {
                Vector2 mousePos = Event.current.mousePosition;

                if (healthRect.Contains(mousePos))
                {
                    // Логика для улучшения здоровья
                    playerHealth.maxLives += 1;
                    playerHealth.currentLives = playerHealth.maxLives;
                    CloseUpgradeMenu();
                }
                else if (speedRect.Contains(mousePos))
                {
                    // Логика для улучшения скорости (можно добавить скорость через другой скрипт)
                    Debug.Log("Speed upgrade selected!");
                    CloseUpgradeMenu();
                }
                else if (damageRect.Contains(mousePos))
                {
                    // Логика для улучшения урона (можно добавить увеличение урона через другой скрипт)
                    Debug.Log("Damage upgrade selected!");
                    Projectile.damage += 2;
                    CloseUpgradeMenu();
                }
            }
        }
    }

    // Закрытие меню улучшений и возобновление игры
    private void CloseUpgradeMenu()
    {
        isUpgradeMenuOpen = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerHealth.upgradeAvailable = false; // Сбрасываем флаг улучшений
    }

    public void ShowInteractText(string text)
    {
        interactText = text;
    }

    public void HideInteractText()
    {
        interactText = "";
    }
}