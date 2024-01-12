using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text skillText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] GameObject enemyChasingObject;
    [SerializeField] GameObject warningObject;
    [SerializeField] float warningDelay = 2f;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        enemyChasingObject.SetActive(false);
        warningObject.SetActive(false);
    }

    public void SetSkillText(int value)
    {
        skillText.text = $"x{value}";
    }

    public void SetHealthText(int value)
    {
        healthText.text = $"x{value}";
    }

    public void SetChasingActive()
    {
        StopAllCoroutines();
        StartCoroutine(NotificationCoroutine(enemyChasingObject));
    }

    public void SetWarningActive()
    {
        StopAllCoroutines();
        StartCoroutine(NotificationCoroutine(warningObject));
    }

    IEnumerator NotificationCoroutine(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(warningDelay);
        obj.SetActive(false);
    }
}
