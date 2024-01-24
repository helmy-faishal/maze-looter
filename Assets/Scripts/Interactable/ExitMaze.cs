using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMaze : Interactable
{
    [HideInInspector]
    public bool canExitMaze = false;
    Treasure treasure;

    private void Awake()
    {
        this.isPickable = false;
    }

    private void Start()
    {
        treasure = FindObjectOfType<Treasure>();
    }

    private void OnEnable()
    {
        this.OnObjectInteracted += ProcessExit;
        this.OnPlayerEnter += () =>
        {
            this.SetInteractionInfoActive(true, "Press F to exit maze");
        };
        this.OnPlayerExit += () =>
        {
            this.SetInteractionInfoActive(false);
        };
    }

    private void OnDisable()
    {
        this.RemoveAllAction();
    }

    void ProcessExit()
    {
        if (treasure == null)
        {
            Debug.LogWarning("Treasure Not Found");
            return;
        }

        canExitMaze = treasure.IsPickedUp;

        if (canExitMaze)
        {
            SceneSwitching.Instance?.WinScene();
        }
        else
        {
            UIManager.Instance?.SetWarningActive("You haven't picked up the Treasure");
        }
    }
}
