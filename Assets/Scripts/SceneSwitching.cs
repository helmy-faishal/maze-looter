using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public static SceneSwitching Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchScene(string sceneName, bool isGameScene = false)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (GameSession.Instance != null)
        {
            GameSession.Instance.isGameScene = isGameScene;
        }
        
        SceneManager.LoadScene(sceneName);
    }

    public void PlayGame()
    {
        SwitchScene("Game",true);
    }

    public void MainMenu()
    {
        SwitchScene("MainMenu");
    }

    public void SelectSkillScene()
    {
        SwitchScene("SkillSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void WinScene()
    {
        SwitchScene("WinScene");
    }

    public void LoseScene()
    {
        SwitchScene("LoseScene");
    }
}
