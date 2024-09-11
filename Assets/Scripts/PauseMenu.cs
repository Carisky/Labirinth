using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;  
    private Rect windowRect = new Rect(100, 100, 200, 150);  
    private int windowID = 12345;  

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();  
            }
            else
            {
                Pause();  
            }
        }
    }

    
    void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;  
        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = false;  
    }

    
    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;  
        Cursor.lockState = CursorLockMode.None;  
        Cursor.visible = true;  
    }

    void OnGUI()
    {
        
        if (isPaused)
        {
            windowRect = GUI.Window(windowID, windowRect, DrawPauseMenu, "Pause Menu");
        }
    }

    
    void DrawPauseMenu(int windowID)
    {
        
        if (GUI.Button(new Rect(50, 40, 100, 30), "Resume"))
        {
            Resume();
        }

        
        if (GUI.Button(new Rect(50, 80, 100, 30), "Restart"))
        {
            Time.timeScale = 1f;  
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);  
        }

        
        if (GUI.Button(new Rect(50, 120, 100, 30), "Quit"))
        {
            Application.Quit();  
            Debug.Log("Quit Game!");  
        }

        
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }
}
