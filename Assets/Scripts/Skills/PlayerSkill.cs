using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector]
    public SkillType skillType = SkillType.None;
    [HideInInspector]
    public int maxSkillUsage = 1;
    [HideInInspector]
    public float skillDelay = 3f;

    CameraSwitching cameraSwitching;
    bool canUseSkill = true;
    public bool CanUseSkill
    {
        get { return canUseSkill; }
        set { canUseSkill = value; }
    }

    private void OnEnable()
    {
        cameraSwitching = FindObjectOfType<CameraSwitching>();
    }

    public void UsingSkill(Action func)
    {
        if (Input.GetKeyDown(this.skillKey))
        {
            this.UsingSkillNow(func);
        }
    }

    public void UsingSkillNow(Action func, bool reduceSkillUsage = true)
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
        yield return new WaitForSeconds(skillDelay);
        OnSkillDeactivate?.Invoke();
        canUseSkill = true;
    }
}
