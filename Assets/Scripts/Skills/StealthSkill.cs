using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthSkill : PlayerSkill
{
    [SerializeField] int skillUsage = 5;
    [SerializeField] float skillDuration = 3f;

    private void Awake()
    {
        this.isDetectableWhenActive = false;
        this.skillType = SkillType.Stealth;
        this.skillDelay = skillDuration;
        this.maxSkillUsage = skillUsage;
    }

    private void Start()
    {
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

    void Update()
    {
        this.UsingSkill(StartStealth);
    }

    void StartStealth()
    {

    }
}
