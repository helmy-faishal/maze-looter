using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : Interactable
{
    public override void SetAwake()
    {
        this.isInteractionInput = false;
        base.SetAwake();
    }

    public override void SetOnEnable()
    {
        base.SetOnEnable();
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
}
