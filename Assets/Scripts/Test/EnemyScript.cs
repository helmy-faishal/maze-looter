using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    ParentScript parentScript;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = FindObjectOfType<ParentScript>();

        if (parentScript != null)
        {
            Debug.Log(parentScript.gameObject.name);
            parentScript.OnSkillActive += PlayerSkillActive;
        }
    }

    private void OnDisable()
    {
        if (parentScript != null)
        {
            parentScript.OnSkillActive -= PlayerSkillActive;
        }
    }

    void PlayerSkillActive()
    {
        Debug.Log("Player Skill Active");
    }
}
