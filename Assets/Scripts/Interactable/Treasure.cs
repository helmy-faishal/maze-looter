using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable
{
    public Action OnTreasurePickedUp;

    private void Awake()
    {
        this.isPickable = true;
    }

    private void OnEnable()
    {
        this.OnObjectInteracted += TreasurePickedUp;
        this.OnPlayerEnter += () =>
        {
            this.SetInteractionInfoActive(true,"Press F to picking up Treasure");
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

    void TreasurePickedUp()
    {
        if (!this.isPickedUp)
        {
            this.isPickedUp = true;
            gameObject.SetActive(false);
            OnTreasurePickedUp?.Invoke();
            this.SetInteractionInfoActive(false);
            UIManager.Instance?.SetObjectiveText("- Exit Maze");
        }
    }
}
