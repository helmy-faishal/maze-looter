using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InteractableType
{
    None,
    Treasure,
    ExitMaze,
}

public class Interactable : MonoBehaviour
{
    public Action OnObjectInteracted;
    public Action OnPlayerEnter;
    public Action OnPlayerExit;

    PlayerInputAction actions;
    protected bool isInteractionInput = true;
    public InputAction inputAction; 

    public bool IsInteractionInput
    {
        get { return isInteractionInput; }
    }

    protected bool isPickedUp = false;
    public bool IsPickedUp { get { return isPickedUp; } }

    protected bool isPlayerNearObject = false;
    public bool IsPlayerNearObject { get {  return isPlayerNearObject; } }

    public virtual void SetAwake()
    {
        actions = new PlayerInputAction();
    }

    private void Awake()
    {
        SetAwake();
    }

    public virtual void SetOnEnable()
    {
        actions.Player.Enable();

        if (isInteractionInput)
        {
            inputAction = actions.Player.Interact;
        }
        else
        {
            inputAction = actions.Player.Skill;
        }

        inputAction.performed += InputPerformedHandler;
    }

    private void OnEnable()
    {
        SetOnEnable();
    }

    public virtual void SetOnDisable()
    {
        actions.Player.Disable();

        if (inputAction != null)
        {
            inputAction.performed -= InputPerformedHandler;
        }

        RemoveAllAction();
    }

    private void OnDisable()
    {
        SetOnDisable();
    }

    void InputPerformedHandler(InputAction.CallbackContext context)
    {
        if (!this.IsPlayerNearObject) return;

        OnObjectInteracted?.Invoke();
    }

    public virtual void SetStart()
    {
        // Start
    }

    private void Start()
    {
        SetStart();
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        OnPlayerEnter?.Invoke();
        isPlayerNearObject = true;

        if (other.TryGetComponent(out PlayerManager manager))
        {
            manager.objectInteracted = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        OnPlayerExit?.Invoke();
        isPlayerNearObject = false;

        if (other.TryGetComponent(out PlayerManager manager))
        {
            manager.objectInteracted = null;
        }
    }
}
