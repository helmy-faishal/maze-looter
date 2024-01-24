using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    public bool isGameScene = false;

    public SkillType playerSkillType = SkillType.None;
    public GameObject skillObject;

    private void Awake()
    {
        if (Instance != null)
        {
            this.isGameScene = Instance.isGameScene;
            this.playerSkillType = Instance.playerSkillType;
            this.skillObject = Instance.skillObject;
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (isGameScene)
        {
            SetPlayerSkill();
            isGameScene = false;
        }
    }

    public void SetPlayerSkill()
    {
        SkillType skillType = playerSkillType;

        if (skillType == SkillType.None)
        {
            // Get Random Skill
            skillType = (SkillType)Random.Range(1, Enum.GetNames(typeof(SkillType)).Length);
        }

        AddPlayerSkill(skillType);
    }

    void AddPlayerSkill(SkillType skillType)
    {
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;

        switch (skillType)
        {
            case SkillType.Sprint:
                player.AddComponent<SprintSkill>();
                break;

            case SkillType.Stealth:
                player.AddComponent<StealthSkill>();
                break;

            case SkillType.Revive:
                player.AddComponent<ReviveSkill>();
                break;

            case SkillType.Teleport: 
                TeleportSkill teleport = player.AddComponent<TeleportSkill>();
                teleport.teleportPointObject = skillObject;
                break;

            default: 
                break;
        }
    }
}
