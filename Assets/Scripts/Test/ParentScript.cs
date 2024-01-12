using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentScript : MonoBehaviour
{
    public Action OnSkillActive;

    bool _isDetectable = true;
    bool isSkillActive = false;

    public bool IsDetectable
    {
        get { return _isDetectable; }
        set { _isDetectable = value; }
    }

    public void StartSkill(Action skillFunc)
    {
        if (isSkillActive) return;

        StartCoroutine(StartSkillCoroutine(skillFunc));
    }

    IEnumerator StartSkillCoroutine(Action func)
    {
        isSkillActive = true;
        func?.Invoke();
        OnSkillActive?.Invoke();
        yield return new WaitForSeconds(3);
        isSkillActive = false;
    }


}
