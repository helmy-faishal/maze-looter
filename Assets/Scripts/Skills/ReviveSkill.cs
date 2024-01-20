using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
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
        PlayerHealth playerHealth = this.GetComponent<PlayerHealth>();
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

    private void OnDisable()
    {
        this.OnSkillActive = null;
        this.OnSkillDeactivate = null;
    }

    public void StartRevive(Action reviveFunc)
    {
        this.UsingSkillNow(reviveFunc);
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);
    }
}
