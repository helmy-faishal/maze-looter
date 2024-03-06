using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapHoldButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdThreshold = 0.4f;
    protected bool isInteractable = true;

    public Action OnButtonTap;
    public Action OnButtonHold;
    public Action OnCancelPress;

    bool _isPressed = false;
    float _holdTime = 0f;
    bool _isHoldInvoked = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;

        if (_isHoldInvoked )
        {
            _isHoldInvoked = false;
        }
        else
        {
            OnButtonTap?.Invoke();
        }

        OnCancelPress?.Invoke();

        _holdTime = 0f;
    }

    private void Update()
    {
        if (_isPressed && isInteractable)
        {
            _holdTime += Time.deltaTime;

            if (_holdTime >= holdThreshold && !_isHoldInvoked)
            {
                OnButtonHold?.Invoke();
                _isHoldInvoked = true;
            }
        }
    }
}
