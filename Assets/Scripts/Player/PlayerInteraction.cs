using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Action OnPlayerPickTreasure;

    [SerializeField] Camera _camera;
    [SerializeField] Canvas canvas;
    [SerializeField] TMP_Text messageText;

    GameObject pickableObject;

    bool IsPickableObject
    {
        get { return pickableObject != null; }
    }

    bool isFinishArea = false;
    bool hasPickTreasure = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
        
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F)) return;

        if (isFinishArea)
        {
            ProcessFinish();
        }

        if (IsPickableObject)
        {
            ProcessPickObject();
        }
    }

    private void FixedUpdate()
    {
        canvas.transform.localRotation = _camera.transform.rotation;
    }

    private void ResetCanvas()
    {
        canvas.gameObject.SetActive(false);
        pickableObject = null;
        isFinishArea = false;
    }

    void SetCanvas(string message)
    {
        canvas.gameObject.SetActive(true);
        messageText.text = $"Press F to {message}";
    }

    void SetInteraction(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Finish Area":
                isFinishArea = true;
                SetCanvas("exit maze");
                break;

            default:
                SetPickable(obj);
                break;
        }
    }

    void SetPickable(GameObject obj)
    {
        string enterObject = null;

        switch (obj.tag)
        {
            case "Treasure":
                enterObject = "Treasure";
                break;

            default:
                break;
        }

        if (enterObject == null) return;

        pickableObject = obj;
        SetCanvas($"pick {enterObject}");

    }

    void ProcessFinish()
    {
        if (!hasPickTreasure)
        {
            UIManager.Instance?.SetWarningActive();
        }
        else
        {
            SceneSwitching.Instance?.WinScene();
        }
    }

    void ProcessPickObject()
    {
        if (pickableObject == null) return;

        if (pickableObject.tag == "Treasure")
        {
            hasPickTreasure = true;
        }

        pickableObject.SetActive(false);
        ResetCanvas();
    }

    private void OnTriggerEnter(Collider other)
    {
        SetInteraction(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        ResetCanvas();
    }
}
