using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthSkill : PlayerSkill
{
    [SerializeField] int skillUsage = 5;
    [SerializeField] float skillDuration = 3f;

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
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);
        SetDetectPlayer();

        this.OnPerformedTap += () => { this.UsingSkillNow(); };
        this.OnPerformedHold += () => { this.UsingSkillNow(); };
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
        this.OnPerformedTap = null;
        this.OnPerformedHold = null;
    }
}
