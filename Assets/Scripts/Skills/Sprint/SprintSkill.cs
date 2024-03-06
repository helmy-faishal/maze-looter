using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintSkill : PlayerSkill
{
    [SerializeField] int skillUsage = 5;
    [SerializeField] float skillDuration = 5f;
    [SerializeField] float speedMultiplier = 1.5f;

    PlayerMovement playerMovement;

    public override void SetAwake()
    {
        base.SetAwake();
        this.skillType = SkillType.Sprint;
        this.skillDelay = skillDuration;
        this.maxSkillUsage = skillUsage;
    }

    public override void SetStart()
    {
        base.SetStart();
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);
        SetSprintSkill();

        this.OnPerformedTap += () => { this.UsingSkillNow(); };
        this.OnPerformedHold += () => { this.UsingSkillNow(); };
    }

    void SetSprintSkill()
    {
        playerMovement = this.player.GetComponent<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogWarning("PlayerMovement script not found!");
            return;
        }

        this.OnSkillActive += () =>
        {
            playerMovement.SetSpeedMultiplier(this.speedMultiplier);
        };

        this.OnSkillDeactivate += () =>
        {
            playerMovement.SetSpeedMultiplier(1);
        };
    }

    public override void SetDisable()
    {
        base.SetDisable();
        this.OnSkillActive = null;
        this.OnSkillDeactivate = null;
    }
}
