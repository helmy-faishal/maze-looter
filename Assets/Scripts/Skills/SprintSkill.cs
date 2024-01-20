using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class SprintSkill : PlayerSkill
{
    [SerializeField] int skillUsage = 5;
    [SerializeField] float skillDuration = 5f;
    [SerializeField] float speedMultiplier = 1.5f;

    PlayerMovement playerMovement;
    private void Awake()
    {
        this.isDetectableWhenActive = true;
        this.skillType = SkillType.Sprint;
        this.skillDelay = skillDuration;
        this.maxSkillUsage = skillUsage;
    }

    void Start()
    {
        UIManager.Instance?.SetSkillText(this.maxSkillUsage);
        SetSprintSkill();
    }

    void SetSprintSkill()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();

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

    private void OnDisable()
    {
        this.OnSkillActive = null;
        this.OnSkillDeactivate = null;
    }

    void Update()
    {
        this.UsingSkill(null);
    }
}
