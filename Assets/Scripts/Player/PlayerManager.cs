using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerManager : MonoBehaviour
{
    public PlayerHealth health;
    public PlayerMovement movement;
    public PlayerSkill skill;

    [SerializeField] List<PlayerSkillDetailSO> skillDetail;

    [HideInInspector]
    public Interactable objectInteracted;

    private void Awake()
    {
        if (health == null)
        {
            health = GetComponent<PlayerHealth>();
        }

        if (movement == null)
        {
            movement = GetComponent<PlayerMovement>();
        }
    }

    private void Start()
    {
        GameObject skillPrefab = SceneSwitching.Instance != null ? SceneSwitching.Instance.skillPrefab : null;

        if (skillPrefab == null)
        {
            if (skillDetail.Count == 0)
            {
                Debug.LogError("Skill is empty!");
                return;
            }

            int index = Random.Range(0, skillDetail.Count);
            var skill = skillDetail[index];
            skillPrefab = skill.SkillPrefab;

        }

        Instantiate(skillPrefab, transform);
    }
}
