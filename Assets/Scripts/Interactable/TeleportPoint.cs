using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : Interactable
{
    private void Awake()
    {
        this.isPickable = true;
        this.interactionKey = KeyCode.E;
    }

    private void OnEnable()
    {
        this.OnObjectInteracted += () =>
        {
            this.SetSkillInfoActive(false);
        };
        this.OnPlayerEnter += () =>
        {
            this.SetSkillInfoActive(true, "Press E to retrieve");
        };
        this.OnPlayerExit += () =>
        {
            this.SetSkillInfoActive(false);
        };
    }

    private void OnDisable()
    {
        this.RemoveAllAction();
    }
}
