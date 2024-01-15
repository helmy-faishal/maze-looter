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
    }

    private void OnDisable()
    {
        this.OnObjectInteracted -= TreasurePickedUp;
    }

    void TreasurePickedUp()
    {
        if (!this.isPickedUp)
        {
            this.isPickedUp = true;
            gameObject.SetActive(false);
            OnTreasurePickedUp?.Invoke();
        }
    }
}
