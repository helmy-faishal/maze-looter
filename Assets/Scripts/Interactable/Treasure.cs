using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable
{
    public Action OnTreasurePickedUp;

    public override void SetAwake()
    {
        this.isInteractionInput = true;
        base.SetAwake();
    }

    public override void SetOnEnable()
    {
        base.SetOnEnable();
        this.OnObjectInteracted += TreasurePickedUp;
        this.OnPlayerEnter += () =>
        {
            this.SetInteractionInfoActive(true, "Press F to picking up Treasure");
        };
        this.OnPlayerExit += () =>
        {
            this.SetInteractionInfoActive(false);
        };
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
