using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMaze : Interactable
{
    [HideInInspector]
    public bool canExitMaze = false;
    Treasure treasure;

    public override void SetAwake()
    {
        this.isInteractionInput = true;
        base.SetAwake();
    }

    public override void SetStart()
    {
        base.SetStart();
        treasure = FindObjectOfType<Treasure>();
    }

    public override void SetOnEnable()
    {
        base.SetOnEnable();
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
