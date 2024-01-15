using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMaze : Interactable
{
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
    }

    private void OnDisable()
    {
        this.OnObjectInteracted -= ProcessExit;
    }

    void ProcessExit()
    {
        if (treasure == null)
        {
            Debug.Log("Treasure Not Found");
            return;
        }

        canExitMaze = treasure.isPickedUp;
    }
}
