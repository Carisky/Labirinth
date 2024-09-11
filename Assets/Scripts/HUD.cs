using UnityEngine;

public class HUD : MonoBehaviour
{
    private float deltaTime = 0.0f;
    public Texture2D heartTexture; 
    private PlayerStats playerHealth; 

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
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
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
    }
}
