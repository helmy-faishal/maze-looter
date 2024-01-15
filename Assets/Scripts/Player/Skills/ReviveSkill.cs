using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveSkill : PlayerSkill
{
    [SerializeField] int skillUsage = 1;
    [SerializeField] float skillDuration = 15f;

    private void Awake()
    {
        this.isDetectableWhenActive = false;
        this.skillType = SkillType.Stealth;
        this.skillDelay = skillDuration;
        this.maxSkillUsage = skillUsage;
    }

    void Start()
    {
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);
    }

    public void StartRevive(Action reviveFunc)
    {
        this.UsingSkillImmediately(reviveFunc);
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);
    }
}
