using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    None,
    Treasure,
    ExitMaze,
}

public class Interactable : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F;
    public bool isPickable = false;
    public InteractableType interactableType = InteractableType.None;

    public Action OnObjectInteracted;
    public Action OnPlayerEnter;
    public Action OnPlayerExit;

    protected bool isPickedUp = false;
    public bool IsPickedUp { get { return isPickedUp; } }

    protected bool isPlayerNearObject = false;
    public bool IsPlayerNearObject { get {  return isPlayerNearObject; } }

    protected void SetInteractionInfoActive(bool active, string text = "Press to interaction")
    {
        InteractionUI.Instance?.SetInteractionInfo(active);
        InteractionUI.Instance?.SetInteractionText(text);
    }
    protected void SetSkillInfoActive(bool active, string text = "Press to interaction")
    {
        InteractionUI.Instance?.SetSkillInfo(active);
        InteractionUI.Instance?.SetSkillText(text);
    }

    protected void RemoveAllAction()
    {
        this.OnObjectInteracted = null;
        this.OnPlayerEnter = null;
        this.OnPlayerExit = null;
    }

    private void Update()
    {
        if (interactionKey == KeyCode.None) return;

        if (Input.GetKeyDown(this.interactionKey) && this.isPlayerNearObject)
        {
            this.OnObjectInteracted?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        OnPlayerEnter?.Invoke();
        isPlayerNearObject = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        OnPlayerExit?.Invoke();
        isPlayerNearObject = false;
    }
}
