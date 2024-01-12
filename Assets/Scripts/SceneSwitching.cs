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

    public void SwitchScene(string sceneName)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(sceneName);
    }

    public void PlayGame()
    {
        SwitchScene("Game");
    }

    public void MainMenu()
    {
        SwitchScene("MainMenu");
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
