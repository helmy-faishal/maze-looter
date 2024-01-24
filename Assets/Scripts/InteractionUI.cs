using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject interactionInfo;
    [SerializeField] TMP_Text interactionText;
    [SerializeField] GameObject skillInfo;
    [SerializeField] TMP_Text skillText;
    [SerializeField] GameObject skillIndicator;
    [SerializeField] Image skillFill;
    [SerializeField] Camera _camera;

    public static InteractionUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        interactionInfo.SetActive(false); 
        skillInfo.SetActive(false);
        skillIndicator.SetActive(false);
    }


    private void FixedUpdate()
    {
        canvas.transform.localRotation = _camera.transform.rotation;
    }

    public void SetInteractionInfo(bool active)
    {
        interactionInfo.SetActive(active);
    }

    public void SetSkillInfo(bool active)
    {
        skillInfo.SetActive(active);
    }

    public void SetInteractionText(string text)
    {
        interactionText.text = text;
    }

    public void SetSkillText(string text)
    {
        skillText.text = text;
    }

    public void SetSkillIndicator(bool active)
    {
        skillIndicator.SetActive(active);
    }

    public void SetSkillFill(float value)
    {
        skillFill.fillAmount = value;
    }
}
