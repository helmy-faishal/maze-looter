using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public enum SkillType
{
    None,
    Stealth,
    Revive,
    Sprint,
    Teleport
}

public class PlayerSkill : MonoBehaviour
{
    public KeyCode skillKey = KeyCode.E;
    public Action OnSkillActive;
    public Action OnSkillDeactivate;

    public Action OnStartedTap;
    public Action OnStartedHold;

    public Action OnPerformedTap;
    public Action OnPerformedHold;

    public Action OnCanceledTap;
    public Action OnCanceledHold;

    [HideInInspector]
    public SkillType skillType = SkillType.None;
    [HideInInspector]
    public int maxSkillUsage = 1;
    [HideInInspector]
    public float skillDelay = 3f;
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public PlayerManager playerManager;

    PlayerInputAction action;
    CameraSwitching cameraSwitching;
    bool canUseSkill = true;
    public bool CanUseSkill
    {
        get { return canUseSkill; }
    }

    public virtual void SetAwake()
    {
        action = new PlayerInputAction();
    }

    public virtual void SetEnable()
    {
        action.Enable();
    }

    public virtual void SetDisable()
    {
        action.Disable();
    }

    public virtual void SetStart()
    {
        player = transform.parent;
        playerManager = player.GetComponent<PlayerManager>();
        playerManager.skill = this;

        cameraSwitching = FindObjectOfType<CameraSwitching>();
    }

    private void Awake()
    {
        SetAwake();
    }

    private void OnEnable()
    {
        SetEnable();
        action.Player.Skill.started += StartedSkill;
        action.Player.Skill.performed += PerformedSkill;
        action.Player.Skill.canceled += CanceledSkill;
    }

    private void OnDisable()
    {
        SetDisable();
        action.Player.Skill.started -= StartedSkill;
        action.Player.Skill.performed -= PerformedSkill;
        action.Player.Skill.canceled -= CanceledSkill;
    }

    private void Start()
    {
        SetStart();
    }

    public void StartedSkill(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            OnStartedHold?.Invoke();
        }
        else
        {
            OnStartedTap?.Invoke();
        }
    }

    public void PerformedSkill(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            OnPerformedHold?.Invoke();
        }
        else
        {
            OnPerformedTap?.Invoke();
        }
    }

    public void CanceledSkill(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            OnCanceledHold?.Invoke();
        }
        else
        {
            OnCanceledTap?.Invoke();
        }
    }

    public void UsingSkillNow(Action func = null, bool reduceSkillUsage = true)
    {
        if (cameraSwitching != null)
        {
            if (cameraSwitching.isMoving) return;
        }

        if (maxSkillUsage <= 0 || !this.CanUseSkill) return;

        if (!reduceSkillUsage)
        {
            func?.Invoke();
            return;
        }

        maxSkillUsage--;

        this.StartSkill(func);

        UIManager.Instance?.SetSkillText(maxSkillUsage);
    }

    void StartSkill(Action skillFunc)
    {
        if (!canUseSkill) return;

        StartCoroutine(StartSkillCoroutine(skillFunc));
    }

    IEnumerator StartSkillCoroutine(Action func)
    {
        canUseSkill = false;
        func?.Invoke();
        OnSkillActive?.Invoke();
        UIManager.Instance?.StartCooldownSkill(skillDelay);
        yield return new WaitForSeconds(skillDelay);
        OnSkillDeactivate?.Invoke();
        canUseSkill = maxSkillUsage > 0;
    }
}
