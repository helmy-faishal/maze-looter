using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    None,
    Stealth,
    Revive
}

public class PlayerSkill : MonoBehaviour
{
    public KeyCode skillKey = KeyCode.E;
    public Action OnSkillActive;
    public Action OnSkillDeactivate;
    public bool isDetectableWhenActive = true;
    public SkillType skillType = SkillType.None;
    public int maxSkillUsage = 1;
    public float skillDelay = 3f;

    bool canUseSkill = true;
    public bool CanUseSkill
    {
        get { return canUseSkill; }
        set { canUseSkill = value; }
    }

    public void UsingSkill(Action func)
    {
        if (maxSkillUsage <= 0 || !this.CanUseSkill) return;

        if (Input.GetKeyDown(this.skillKey))
        {
            maxSkillUsage--;

            this.StartSkill(func);

            UIManager.Instance?.SetSkillText(maxSkillUsage);
        }
    }

    public void StartSkill(Action skillFunc)
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
