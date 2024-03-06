using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    [HideInInspector]
    public SkillType skillType = SkillType.None;
    [HideInInspector]
    public GameObject skillPrefab;

    public static SceneSwitching Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            this.skillType = Instance.skillType;
            this.skillPrefab = Instance.skillPrefab;
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
