using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillButtonHandler : TapHoldButtonHandler
{
    [SerializeField] float holdCooldown = 0.5f;
    [SerializeField] PlayerManager playerManager;

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        this.OnButtonTap += () =>
        {
            ProcessInput(false);
        };
        this.OnButtonHold += () =>
        {
            ProcessInput(true);
        };
        this.OnCancelPress += () =>
        {
            playerManager.skill.OnCanceledHold?.Invoke();
        };
    }

    private void OnDisable()
    {
        this.OnButtonTap = null;
        this.OnButtonHold = null;
        this.OnCancelPress = null;
    }

    void ProcessInput(bool isHold)
    {
        if (isHold)
        {
            playerManager.skill.OnPerformedHold?.Invoke();
        }
        else
        {
            playerManager.skill.OnPerformedTap?.Invoke();

            if (playerManager.objectInteracted != null)
            {
                if (!playerManager.objectInteracted.IsInteractionInput)
                {
                    playerManager.objectInteracted.OnObjectInteracted?.Invoke();
                }
            }

            StartCoroutine(StartCooldownButtonCoroutine());
        }
    }

    IEnumerator StartCooldownButtonCoroutine()
    {
        SetButtonActive(false);
        yield return new WaitForSeconds(holdCooldown);
        SetButtonActive(true);
    }

    void SetButtonActive(bool active)
    {
        button.interactable = active;
        this.isInteractable = active;
    }
}
