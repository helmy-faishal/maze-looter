using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveSkill : PlayerSkill
{
    [SerializeField] int skillUsage = 1;
    [SerializeField] float skillDuration = 15f;

    public override void SetAwake()
    {
        base.SetAwake();
        this.skillType = SkillType.Stealth;
        this.skillDelay = skillDuration;
        this.maxSkillUsage = skillUsage;
    }

    public override void SetStart()
    {
        base.SetStart();
        PlayerHealth playerHealth = this.player.GetComponent<PlayerHealth>();
        playerHealth.reviveSkill = this;
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);

        SetDetectPlayer();
    }

    void SetDetectPlayer()
    {
        DetectPlayer[] detectPlayers = FindObjectsOfType<DetectPlayer>();

        foreach (DetectPlayer detectPlayer in detectPlayers)
        {
            if (detectPlayer == null) continue;

            this.OnSkillActive += detectPlayer.SetPlayerUndetectable;
            this.OnSkillDeactivate += detectPlayer.SetPlayerDetectable;
        }
    }

    public override void SetDisable()
    {
        base.SetDisable();
        this.OnSkillActive = null;
        this.OnSkillDeactivate = null;
    }

    public void StartRevive(Action reviveFunc)
    {
        this.UsingSkillNow(reviveFunc);
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);
    }
}
